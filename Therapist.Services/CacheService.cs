using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Therapist.Core.Services;

namespace Therapist.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
           _database = redis.GetDatabase();
        }
        public async Task AddToCache(string CacheKey, object Response, TimeSpan ExpireDate)
        {
           if(Response == null) return;
           var options = new JsonSerializerOptions()
           {
               PropertyNamingPolicy = JsonNamingPolicy.CamelCase
           };
            var JsonResponse= JsonSerializer.Serialize(Response , options);
          await   _database.StringSetAsync(CacheKey, JsonResponse, ExpireDate);
        }

        public async Task<string> GetFromCache(string CacheKey)
        {
            if (string.IsNullOrEmpty(CacheKey)) return null;
           var result= await  _database.StringGetAsync(CacheKey);

            if (result.IsNullOrEmpty) return null;
            return result;
        }
    }
}
