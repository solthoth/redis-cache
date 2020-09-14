using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface ICacheFactory
    {
        Task<T> Get<T>(string key, Func<Task<T>> seed) where T : new();
    }
    public class CacheFactory : ICacheFactory
    {
        private readonly IDistributedCache Cache;
        public CacheFactory(IDistributedCache cache)
        {
            Cache = cache;
        }

        public async Task<T> Get<T>(string key, Func<Task<T>> seed) where T : new()
        {
            var value = await Cache.GetAsync(key) ?? new byte[0];
            if (!IsNull(value))
            {
                var serializedContent = Encoding.UTF8.GetString(value);
                return JsonConvert.DeserializeObject<T>(serializedContent);
            }
            var content = await seed();
            await SaveToCache(key, content);
            return content;
        }

        private bool IsNull(byte[] content)
        {
            return content.Length == 0 || Encoding.UTF8.GetString(content) == "null";
        }

        private async Task SaveToCache<T>(string key, T content)
        {
            var serializedContent = JsonConvert.SerializeObject(content);
            var value = Encoding.UTF8.GetBytes(serializedContent);
            await Cache.SetAsync(key, value, DefaultOptions);
        }

        private DistributedCacheEntryOptions DefaultOptions => new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(15))
                            .SetAbsoluteExpiration(DateTime.Now.AddHours(1));
    }
}