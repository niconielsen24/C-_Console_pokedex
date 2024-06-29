using System.Data;
using MySqlConnector;

namespace POKE_CLI.database;

public class MySqlDb : IDb
{
    private MySqlConnection? connection;
    private string? connectionString;

    private bool isOpen = false;

    public MySqlDb(string connectionString)
    {
        SetConnectionString(connectionString);
    }

    public void Open() 
    {
        try 
        {
            if (connection != null)
            {
                connection.Open();
                isOpen = true;
            }
            else 
            {
                Console.WriteLine("Cannot open null connection.");
            }
        } 
        catch (Exception e) 
        {
            Console.WriteLine("Failed to open connection: " + e.Message);
        }
    }

    public void Close()
    {
        try 
        {
            if (connection != null)
            {
                connection.Close();
                isOpen = false;
            }
            else 
            {
                Console.WriteLine("Cannot close null connection.");
            }
        } 
        catch (Exception e) 
        {
            Console.WriteLine("Failed to close connection: " + e.Message);
        }
    }

    public bool Ping()
    {
        if (connection != null)
        {
            return connection.Ping();
        }
        Console.WriteLine("Null Connection");
        return false;
    }

    public void SetConnectionString(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Connection string cannot be null or empty", nameof(connectionString));
        }

        var builder = new MySqlConnectionStringBuilder(connectionString);
        this.connectionString = builder.ToString();
        connection = new MySqlConnection(this.connectionString);
    }

    public override string ToString()
    {
        if (connection != null) 
        {
            return connection.ToString();
        }
        else 
        {
            return "Null Connection.";
        }
    }

    public DataTable Command(string sql)
    {
        string validatedSql = ValidateSql(sql);
        DataTable dataTable = new DataTable();
        if (isOpen)
        {

            try 
            {
                var cmd = new MySqlCommand(validatedSql, connection);
                var adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dataTable);
                Console.WriteLine("Successfully executed query");
            }
            catch (Exception e) 
            {
                Console.WriteLine("Could not execute query: ",e);
            }
        }
        else
        {
            Console.WriteLine("Connection is closes, cannot execute query.");
        }
        return dataTable;
    }

    private static string ValidateSql(string sql)
    {
        if (sql.Trim().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
        {
            return sql;
        }
        else
        {
            throw new ArgumentException("Invalid SQL command.");
        }
    }
}
