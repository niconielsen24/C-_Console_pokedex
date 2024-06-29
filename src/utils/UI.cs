namespace POKE_CLI.utils;

public class UI
{
    private Dictionary<string, string> optionDict = new Dictionary<string, string>();
    private List<(string, string, int)> options = new List<(string, string, int)>();

    public UI()
    {
        options = new List<(string, string, int)>();
        options.Add(("-h", "--help", 0));
        options.Add(("-p", "--pokemon", 1));
        options.Add(("-i", "--id_num", 1));
    }

    public Config HandleInput(string[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            if (options != null)
            {
                foreach ((string shortOption, string longOption, int requiresArg) in options)
                {
                    if (shortOption == args[i] || longOption == args[i])
                    {
                        string name = args[i];
                        if (requiresArg == 0)
                        {
                            optionDict?.Add(name,"");
                        }
                        else
                        {
                            if (i + 1 < args.Length && !args[i + 1].StartsWith('-'))
                            {
                                optionDict?.Add(name, args[i + 1]);
                                i++;
                            }
                            else
                            {
                                Console.WriteLine($"Argument missing for option {name}");
                            }
                        }
                    }
                }
            }
        }
        
        bool printHelp = optionDict.ContainsKey("-h") || optionDict.ContainsKey("--help");
        bool pokemonS = optionDict.ContainsKey("-p");
        bool pokemonL = optionDict.ContainsKey("--pokemon");
        bool pokemonIdS = optionDict.ContainsKey("-i");
        bool pokemonIdL = optionDict.ContainsKey("--id_num");
        bool pokemon =  pokemonS || pokemonL || pokemonIdS || pokemonIdL;
        string pokeName = "";
        int pokeId = 0;
        if(pokemon && pokemonS)
        {
            pokeName = optionDict["-p"];
        }
        else if (pokemon && pokemonL)
        {
            pokeName = optionDict["--pokemon"];
        }
        else if (pokemon && pokemonIdS)
        {
            pokeId = int.Parse(optionDict["-i"]);
        }
        else if (pokemon && pokemonIdL)
        {
            pokeId = int.Parse(optionDict["--id_num"]);
        }

        Config config = new Config(printHelp,pokemon,pokeName,pokeId);

        return config;
    }
}
