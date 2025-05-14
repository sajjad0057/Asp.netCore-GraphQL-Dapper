using MySql.Data.MySqlClient;
using System.Data;

namespace NestedCrud.API.Data;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _connectionString = _configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
    }

    public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
}
