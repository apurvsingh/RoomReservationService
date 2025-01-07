using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace RoomReservation.Application.Services.Redis
{
    public interface IRedisCacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task RemoveAsync(string key);
    }

    internal class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _redisDatabase;
        private readonly ILogger _logger;

        public RedisCacheService (IConnectionMultiplexer multiplexer, ILogger<RedisCacheService> logger)
        {
            _logger = logger;
            _redisDatabase = multiplexer.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var cachedData = await _redisDatabase.StringGetAsync(key);
            if(cachedData.IsNullOrEmpty)
            {
                _logger.LogInformation($"Cache miss for key: {key}");
                return default;
            }

            _logger.LogInformation($"Cache hit for key: {key}");
            return JsonSerializer.Deserialize<T>(cachedData);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            _logger.LogInformation($"Cache set for key: {key}");
            var serializedData = JsonSerializer.Serialize(value);
            await _redisDatabase.StringSetAsync(key, serializedData, expiry);
        }

        public async Task RemoveAsync(string key)
        {
            _logger.LogInformation($"Cache removed for key: {key}");
            await _redisDatabase.KeyDeleteAsync(key);
        }
    }
}
