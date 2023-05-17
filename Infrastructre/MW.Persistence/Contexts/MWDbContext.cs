using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MW.Persistence.Contexts
{
    public class MWDbContext
    {
        private readonly IConfiguration _configuration;
        public MWDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_configuration.GetConnectionString("MWSql"));
    }
}
