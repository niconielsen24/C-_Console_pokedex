using System.Data;

public class RunApp{
    public static void Run(Config config, IDb database){
        if(config.GetPrintHelp())
        {
            PrintHelp();
            System.Environment.Exit(0);
        }
        if(config.GetPoke())
        {
            var pokeInfo = GetPokeInfo(database,config.GetPokeName());
            foreach(DataRow row in pokeInfo.Rows)
            {
                Console.WriteLine($@"Name                   : {row["name"]}");
                Console.WriteLine($@"Id                     : {row["poke_id"]}");
                Console.WriteLine($@"Base xp                : {row["base_xd"]}");
                Console.WriteLine($@"Base hp                : {row["base_hp"]}");
                Console.WriteLine($@"Base attack            : {row["base_attack"]}");
                Console.WriteLine($@"Base defense           : {row["base_defense"]}");
                Console.WriteLine($@"Base special attack    : {row["base_sp_attack"]}");
                Console.WriteLine($@"Base special defense   : {row["base_sp_defense"]}");
                Console.WriteLine($@"Height                 : {row["height"]}");
                Console.WriteLine($@"Weight                 : {row["weight"]}");
                Console.WriteLine($@"Speed                  : {row["speed"]}");
            }
        }
    }
    private static void PrintHelp()
    {
        Console.WriteLine("pokemon: Display information about Pokémon.");
    }
    private static DataTable GetPokeInfo(IDb database, string pokeName)
    {
        string sqlQuery = $"SELECT * FROM pokemon WHERE name='{pokeName}'";
        return database.Command(sqlQuery);
    }
}