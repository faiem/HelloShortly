using HelloShortly.UrlDistributerRestApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.UrlDistributerRestApi.Data.Repositories
{
    public interface IShortUrlRepository
    {
        ShortUrl Create(ShortUrl url);
        IList<ShortUrl> Read();
        ShortUrl Find(string id);
        void Update(ShortUrl url);
        void Delete(string id);
    }
}
