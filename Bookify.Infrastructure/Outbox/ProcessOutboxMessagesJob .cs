using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Data;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Outbox
{
    internal sealed class ProcessOutboxMessagesJob : IJob
    {
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IPublisher _publisher;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly OutboxOptions _outboxOptions;
        private readonly ILogger<ProcessOutboxMessagesJob> _logger;

        public ProcessOutboxMessagesJob(
            ISqlConnectionFactory sqlConnectionFactory,
            IPublisher publisher,
            IDateTimeProvider dateTimeProvider,
            IOptions<OutboxOptions> outboxOptions,
            ILogger<ProcessOutboxMessagesJob> logger)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _publisher = publisher;
            _dateTimeProvider = dateTimeProvider;
            _logger = logger;
            _outboxOptions = outboxOptions.Value;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Beginning to process outbox messages");

            using var connection = _sqlConnectionFactory.CreateConnection();
            using var transaction = connection.BeginTransaction();

            var outboxMessages = await GetOutboxMessagesAsync(connection, transaction);

            foreach(var outbox in outboxMessages)
            {
                Exception exception = null;
                try
                {
                    var domainEvent= JsonConvert.DeserializeObject(outbox.Content, _jsonSerializerSettings);
                    await _publisher.Publish(domainEvent, context.CancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing outbox message with id {OutboxMessageId}", outbox.Id);

                    exception = ex;
                }

                await UpdateOutboxMessageAsync(connection, transaction, outbox, exception);
            }

            transaction.Commit();
            _logger.LogInformation("Finished processing outbox messages");

        }

        private async Task UpdateOutboxMessageAsync(
            IDbConnection connection,
            IDbTransaction transaction,
            OutboxMessageResponse outboxMessage,
            Exception? exception)
        {
            const string sql = @"
                UPDATE outbox_messages
                SET processed_on_utc = @ProcessedOnUtc,
                    error = @Error
                WHERE id = @Id";

            await connection.ExecuteAsync(
                sql,
                new
                {
                    outboxMessage.Id,
                    ProcessedOnUtc = _dateTimeProvider.UtcNow,
                    Error = exception?.ToString()
                },
                transaction: transaction);
        }

        private async Task<IReadOnlyList<OutboxMessageResponse>> GetOutboxMessagesAsync(
            IDbConnection connection,
            IDbTransaction transaction)
        {
            var sql = $"""
                SELECT TOP ({_outboxOptions.BatchSize}) id, content
                FROM outbox_messages WITH (UPDLOCK, ROWLOCK, READPAST)
                WHERE processed_on_utc IS NULL
                ORDER BY occured_on_utc
                """;

            var outboxMessages = await connection.QueryAsync<OutboxMessageResponse>(
                sql,
                transaction: transaction);

            return outboxMessages.ToList();
        }

        internal sealed record OutboxMessageResponse(Guid Id, string Content);
    }
}
