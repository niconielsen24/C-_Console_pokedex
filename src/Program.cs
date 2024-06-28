
using System.Data;

public class Program
{
    public static void Main(string[] args)
    {
        var database = new MySqlDb("user=root;password=nicanor24;database=pokemons;host=localhost");
        var ui = new UI();
        Config conf = ui.HandleInput(args);

        try
        {
            database.Open();
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
        }
    }
}
