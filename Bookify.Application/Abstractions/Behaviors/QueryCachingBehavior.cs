using Bookify.Application.Abstractions.Caching;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookify.Application.Abstractions.Behaviors
{
    internal sealed class QueryCachingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICachedQuery
        where TResponse : Result
    {

        private readonly ICashService _cache;

        private readonly ILogger<QueryCachingBehavior<TRequest, TResponse>> _logger;
        public QueryCachingBehavior(ICashService cache, ILogger<QueryCachingBehavior<TRequest, TResponse>> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse? cachedResult = await _cache.GetAsync<TResponse>(request.CacheKey, cancellationToken);
            string name = typeof(TRequest).Name;
            if (cachedResult is not null)
            {
                _logger.LogInformation("Cache hit for {Query} ", name);
                return cachedResult;
            }
            _logger.LogInformation("Cache miss for {Query} ", name);

            var result = await next();

            if (result.IsSuccess)
            {
                await _cache.SetAsync(request.CacheKey, result, request.Expiration, cancellationToken);
            }
            return result;
        }
    }
}
