using StackExchange.Redis;
using System;

namespace Perguntech.Data.Services
{
    public class RedisCacheService
    {
        private readonly ConnectionMultiplexer _redis;

        public RedisCacheService(string configuration)
        {
            _redis = ConnectionMultiplexer.Connect(configuration);
        }

        public string Get(string key)
        {
            var db = _redis.GetDatabase();
            return db.StringGet(key);
        }

        public void Set(string key, string value, TimeSpan? expiry = null)
        {
            var db = _redis.GetDatabase();
            db.StringSet(key, value, expiry);
        }

        public void Remove(string key)
        {
            var db = _redis.GetDatabase();
            db.KeyDelete(key);
        }

        public bool Exists(string key)
        {
            var db = _redis.GetDatabase();
            return db.KeyExists(key);
        }
    }
}
