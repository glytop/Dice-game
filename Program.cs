using Itransition.Trainee;

class Program
{
    private const int LENGTH_OF_DICES = 3;
    public static void Main(string[] args)
    {
        if (args.Length < LENGTH_OF_DICES)
        {
            Console.WriteLine("Error: You must provide at least 3 sets of dice");
            return;
        }

        List<Dice> diceList = new List<Dice>();

        foreach (var arg in args)
        {
            diceList.Add(DiceParser.Parse(arg));
        }

        Game game = new(diceList);
        game.Start();
    }
}