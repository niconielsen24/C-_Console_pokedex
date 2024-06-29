using System.Data;
using POKE_CLI.utils;
using POKE_CLI.database;
using POKE_CLI.runnap;

public class Program
{
    public static void Main(string[] args)
    {
        IDb database = new MySqlDb("user=root;password=nicanor24;database=pokemons;host=localhost");
        UI ui = new UI();
        Config conf = ui.HandleInput(args);

        try
        {
            database.Open();
            Console.WriteLine("Open connection.");
            try
            {
                RunApp.Run(conf,database);
            } 
            catch (Exception e)
            {
                Console.WriteLine("Failed Command: " + e.Message);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to open database connection: " + e.Message);
        }
        finally
        {
            database.Close();
            Console.WriteLine("\nClose connection.");
        }
    }
}
