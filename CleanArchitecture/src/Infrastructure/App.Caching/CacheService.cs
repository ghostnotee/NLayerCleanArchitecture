using App.Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace App.Caching;

public class CacheService(IMemoryCache memoryCache) : ICacheService
{
    public Task<T?> GetAsync<T>(string cacheKey)
    {
        return Task.FromResult(memoryCache.TryGetValue(cacheKey, out T? cacheItem) ? cacheItem : default);
    }

    public Task SetAsync<T>(string cacheKey, T value, TimeSpan? expiry = null)
    {
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry
        };
        memoryCache.Set(cacheKey, value, cacheOptions);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string cacheKey)
    {
        memoryCache.Remove(cacheKey);
        return Task.CompletedTask;
    }
}