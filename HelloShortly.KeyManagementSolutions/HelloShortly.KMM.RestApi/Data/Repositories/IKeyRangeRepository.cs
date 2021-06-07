using HelloShortly.KMM.RestApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.KMM.RestApi.Data.Repositories
{
    public interface IKeyRangeRepository : IRepository
    {
        int UpdateKeyRangeWithUniqueIdentifier(string identifier);
        keyRange GetUniqueKeyRangeByIdentifier(string identifier);
    }
}
