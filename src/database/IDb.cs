using System.Data;

namespace POKE_CLI.database;

public interface IDb {
    
    void SetConnectionString(string connectionString);
    void Open();
    void Close();
    bool Ping();
    DataTable Command(string sql);
}