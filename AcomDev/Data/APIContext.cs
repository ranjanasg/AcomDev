using MySqlConnector;
using System.Data;

namespace AcomDev.Data
{
    public class APIContext
    {
        private readonly IConfiguration _configuration;

        private readonly string? _connectionString;
        public APIContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
