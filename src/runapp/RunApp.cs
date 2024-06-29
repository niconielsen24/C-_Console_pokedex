using System.Data;
using POKE_CLI.database;
using POKE_CLI.utils;

namespace POKE_CLI.runnap;
public class RunApp{
    public static void Run(Config config, IDb database){
        if(config.GetPrintHelp())
        {
            PrintHelp();
            System.Environment.Exit(0);
        }
        if(config.GetPoke() && config.GetPokeName() != "")
        {
            var pokeInfo = GetPokeInfo(database,config.GetPokeName());            
            var pokeTypes = GetPokeTypes(database,config.GetPokeName());
            var pokeAbilities = GetPokeAbilities(database,config.GetPokeName());
            var pokeMoves = GetPokeMoves(database,config.GetPokeName());
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
                Console.Write($"Types                  : ");
            foreach(DataRow row in pokeTypes.Rows)
            {
                Console.Write($@"{row["name"]} ");
            }

            Console.WriteLine($"\nAbilities : ");
            foreach(DataRow row in pokeAbilities.Rows)
            {
                Console.Write($@"{row["name"]}, ");
            }     
            Console.WriteLine($" -- {config.GetPokeName()} has these moves : ");
            foreach(DataRow row in pokeMoves.Rows)
            {
                Console.Write($@"{row["name"]}, ");
            }
        } 
        else if (config.GetPoke() && config.GetPokeId() != 0)
        {
            var pokeInfo = GetPokeInfo(database,config.GetPokeId());
            var pokeTypes = GetPokeTypes(database,config.GetPokeId());
            var pokeAbilities = GetPokeAbilities(database,config.GetPokeId());
            var pokeMoves = GetPokeMoves(database,config.GetPokeId());
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
                Console.Write($"Types                  : ");
            foreach(DataRow row in pokeTypes.Rows)
            {
                Console.Write($@"{row["name"]} ");
            }

            Console.WriteLine($"\nAbilities : ");
            foreach(DataRow row in pokeAbilities.Rows)
            {
                Console.Write($@"{row["name"]}, ");
            }     
            Console.WriteLine($" -- {GetPokeName(database,config.GetPokeId())} has these moves : ");
            foreach(DataRow row in pokeMoves.Rows)
            {
                Console.Write($@"{row["name"]}, ");
            }
            
        }
    }
    private static void PrintHelp()
    {
        Console.WriteLine(@"dotnet run -- [OPTIONS]");
        Console.WriteLine(@"-h, --help                     : Shows this help message and exit.");
        Console.WriteLine(@"-p <name> , --pokemon <name>   : Finds data related to that pokemon, shows it and exit.");
        Console.WriteLine(@"-i <name> , --id_num <name>    : Finds data related to that pokemon id, shows it and exit.");
    }
    private static DataTable GetPokeInfo(IDb database, string pokeName)
    {
        string sqlQuery = $"SELECT * FROM pokemon WHERE name='{pokeName}'";
        return database.Command(sqlQuery);
    }
    private static DataTable GetPokeInfo(IDb database, int pokeId)
    {
        string sqlQuery = $"SELECT * FROM pokemon WHERE poke_id='{pokeId}'";
        return database.Command(sqlQuery);
    }
    private static DataTable GetPokeMoves(IDb database, string pokeName){
        string sqlQuery = 
        $@"SELECT * FROM moves
        WHERE move_id IN (
            SELECT move_id FROM pokemon_moves
            WHERE poke_id=(
                SELECT poke_id FROM pokemon
                WHERE name='{pokeName}'
                )
            );";
        return database.Command(sqlQuery);
    }

    private static DataTable GetPokeMoves(IDb database, int pokeId){
        string sqlQuery =
        $@"SELECT * FROM moves
        WHERE move_id IN (
            SELECT move_id FROM pokemon_moves
            WHERE poke_id={pokeId}
            )";
        return database.Command(sqlQuery);
    }

    private static DataTable GetPokeAbilities(IDb database, string pokeName){
        string sqlQuery =
        $@"SELECT * FROM abilities
        WHERE move_id IN (
            SELECT ability_id FROM pokemon_abilities
            WHERE poke_id=(
                SELECT poke_id FROM pokemon
                WHERE name='{pokeName}'
                )
            )";
        return database.Command(sqlQuery);
    }

    private static DataTable GetPokeAbilities(IDb database, int pokeId)
    {
        string sqlQuery =
        $@"SELECT * FROM abilities
        WHERE ability_id IN (
            SELECT move_id FROM pokemon_abilities
            WHERE poke_id={pokeId}
            )";
        return database.Command(sqlQuery);
    }

    private static DataTable GetPokeTypes(IDb database, string pokeName){
        string sqlQuery =
        $@"SELECT * FROM types
        WHERE type_id IN (
            SELECT type_id FROM pokemon_types
            WHERE poke_id=(
                SELECT poke_id FROM pokemon
                WHERE name='{pokeName}'
                )
            )";
        return database.Command(sqlQuery);
    }

    private static DataTable GetPokeTypes(IDb database, int pokeId)
    {
        string sqlQuery =
        $@"SELECT * FROM types
        WHERE type_id IN (
            SELECT type_id FROM pokemon_types
            WHERE poke_id={pokeId}
            )";
        return database.Command(sqlQuery);
    }

    private static string GetPokeName(IDb database, int pokeId)
    {
        string sqlQuery = $"SELECT name FROM pokemon WHERE poke_id={pokeId}";
        var res = database.Command(sqlQuery);
        if (res.Rows.Count > 0)
        {
            var name = res.Rows[0]["name"].ToString();
            return name ?? string.Empty;
        }

    return string.Empty;
    }

}