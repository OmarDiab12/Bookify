using System.Data;
using Bookify.Application.Abstractions.Data;
using Microsoft.Data.SqlClient;

namespace Bookify.Infrastructure.Data;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;
    
    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IDbConnection CreateConnection()
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();
        
        return connection;
    }
}