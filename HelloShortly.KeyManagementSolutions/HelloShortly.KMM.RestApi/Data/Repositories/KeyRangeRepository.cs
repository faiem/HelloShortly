using Dapper;
using HelloShortly.KMM.RestApi.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.KMM.RestApi.Data.Repositories
{
    public class KeyRangeRepository : Repository, IKeyRangeRepository
    {
        private readonly ILogger<KeyRangeRepository> _logger;
        public KeyRangeRepository(ILogger<KeyRangeRepository> logger)
        {
            _logger = logger;
            var connectionstring = Environment.GetEnvironmentVariable("ASPNETCORE_DB_CONN");
            _logger.LogInformation(connectionstring);
        }

        public keyRange GetUniqueKeyRangeByIdentifier(string identifier)
        {
            string query = "select * from key_ranges where retrieve_key = @r_key";

            var dbPara = new DynamicParameters();
            dbPara.Add("r_key", identifier);

            var keyRange = Get<keyRange>(query, dbPara, CommandType.Text);

            return keyRange;
        }

        public int UpdateKeyRangeWithUniqueIdentifier(string identifier)
        {
           _logger.LogInformation($"updating key range with identifier : {identifier}");

            var dbPara = new DynamicParameters();
            dbPara.Add("r_key", identifier);
            dbPara.Add("r_time", DateTime.UtcNow, DbType.DateTime);

            string update_query = "UPDATE key_ranges SET is_retrieved = true, retrieve_key = @r_key, retrieved_time=@r_time WHERE CTID IN(SELECT CTID FROM key_ranges WHERE is_retrieved = false and retrieve_key IS NULL LIMIT 1)";

            int updateRowCount = Update<int>(update_query, dbPara, CommandType.Text);

            _logger.LogInformation($"row affected : {updateRowCount}");

            return updateRowCount;
        }
    }
}
