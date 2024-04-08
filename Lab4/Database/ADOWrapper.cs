using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace Lab4.Database;

public class AdoWrapper
{
    private static string? _connectionString;
    
    public AdoWrapper()
    {
        try
        {
            if (_connectionString is null)
                throw new SettingsPropertyNotFoundException("Connection string is uninitialized");

            var connection = new SqlConnection(connectionString: _connectionString);
            
            connection.Open();
        }
        catch (SqlException)
        {
            MessageBox.Show(messageBoxText: "No connection with SQL Database.",
                caption: "Error!",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error,
                defaultResult: MessageBoxResult.OK);
            
            Application.Current.Shutdown();
        }
    }
    
    public static void ConfigureConnectionString(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async void ExecuteNonQuery(string query)
    {
        await using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand(cmdText: query, 
                connection: connection);
            
            await command.ExecuteNonQueryAsync();
        }
    }
    
    public async Task<Dictionary<string, List<string>>> ExecuteReader(string query)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(cmdText: query, 
            connection: connection);

        var resultDict = new Dictionary<string, List<string>>();
            
        await using (var reader = await command.ExecuteReaderAsync())
        {
            var columns = reader.FieldCount;

            // If query result has 0 rows
            if (!reader.HasRows) return resultDict;
                
            for (int i = 0; i < columns; i++)
            {
                resultDict[reader.GetName(i)] = [];
            }
 
            while (await reader.ReadAsync()) // построчно считываем данные
            {
                for (int i = 0; i < columns; i++)
                {
                    var columnTitle = reader.GetName(i);
                        
                    resultDict[columnTitle].Add(reader.GetValue(0).ToString()!);
                }
            }
        }

        return resultDict;
    }
    
    public async Task<double> ExecuteScalar(string query)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(cmdText: query, 
            connection: connection);
            
        var value = (double) (await command.ExecuteScalarAsync() ?? throw new InvalidOperationException());

        return value;
    }
}