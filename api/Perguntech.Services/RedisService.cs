using StackExchange.Redis;
using System.Text.Json;

public class RedisService
{
    private readonly IDatabase _database;

    public RedisService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<string> GetAsync(string key)
    {
        return await _database.StringGetAsync(key);
    }

    public async Task SetAsync(string key, string value)
    {
        await _database.StringSetAsync(key, value);
    }

    public async Task<T> GetObjectAsync<T>(string key) 
    {
        var data = await _database.StringGetAsync(key);
        if (data.IsNullOrEmpty) return default;
        return JsonSerializer.Deserialize<T>(data);
    }

    public async Task SetObjectAsync<T>(string key, T value)
    {
        var data = JsonSerializer.Serialize(value);
        await _database.StringSetAsync(key, data);
    }

}
