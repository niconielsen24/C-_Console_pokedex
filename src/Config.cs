public class Config{
    private bool printHelp;
    private bool poke;
    private string pokeName;

    public Config(bool printHelp, bool poke, string pokeName)
    {
        this.printHelp = printHelp;
        this.poke = poke;
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

    public string GetPokeName()
    {
        return this.pokeName;
    }
}