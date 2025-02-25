using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Therapist.Core.Services
{
    public  interface ICacheService
    {
        Task AddToCache(string CacheKey, object Response, TimeSpan ExpireDate);
        Task<string> GetFromCache(string CacheKey);
    }
}
