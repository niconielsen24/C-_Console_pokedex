namespace POKE_CLI.utils;

public class Config{
    private bool printHelp;
    private bool poke;
    private int pokeId;
    private string pokeName;

    public Config(bool printHelp, bool poke, string pokeName, int pokeId)
    {
        this.printHelp = printHelp;
        this.poke = poke;
        this.pokeId = pokeId;
        this.pokeName = pokeName;
    } 

    public bool GetPrintHelp()
    {
        return this.printHelp;
    }
    public bool GetPoke()
    {
        return this.poke;
    }

    public int GetPokeId(){
        return this.pokeId;
    }

    public string GetPokeName()
    {
        return this.pokeName;
    }
}