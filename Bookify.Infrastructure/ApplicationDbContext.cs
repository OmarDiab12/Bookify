using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Exceptions;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Bookify.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private static readonly JsonSerializerSettings JsonSerializeSettings = new()
    {    
        TypeNameHandling = TypeNameHandling.All
    };

    private readonly IDateTimeProvider _dateTimeProvider;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeProvider dateTimeProvider) : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            AddDomainEventsAsOutboxMessages();

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch (DbUpdateConcurrencyException e)
        {
            throw new ConcurrencyException("concurrency exception occured", e);
        }
    }

    private void AddDomainEventsAsOutboxMessages()
    {
        var outboxMessages = ChangeTracker.Entries<Entity>().Select(entry => entry.Entity).SelectMany(entity =>
        {
            var domainEvents = entity.GetDomainEvents();
            entity.ClearDomainEvents();
            return domainEvents;
        })
            .Select(domainEvent => new OutboxMessage(
                                                        Guid.NewGuid(),
                                                        _dateTimeProvider.UtcNow, 
                                                        domainEvent.GetType().Name, 
                                                        JsonConvert.SerializeObject(domainEvent, JsonSerializeSettings)
                                                    ))
            .ToList();

        AddRange(outboxMessages);
    }
}