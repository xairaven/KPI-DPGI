using System.Configuration;
using System.Data.SqlClient;

namespace Lab4.Database;

public static class DbConnection
{
    public static SqlConnection? Connection;
    
    public static void Start()
    {
        var connectionString = ConfigurationManager.ConnectionStrings["connectionString_ADO"]
            .ConnectionString;
        
        Connection = new SqlConnection();
        Connection.ConnectionString = connectionString;
        Connection.Open();
    }
}