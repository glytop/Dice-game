namespace Itransition.Trainee
{
    public class DiceParser
    {
        public static Dice Parse(string input)
        {
            int[] faces = input
                .Split(',')
                .Select(x =>
                {
                    if (!int.TryParse(x, out int number))
                    {
                        Console.WriteLine($"Error: Dice face '{x}' is not a valid integer");
                        Environment.Exit(0);
                    }
                    return number;
                })
                .ToArray();
            return new Dice(faces);
        }
    }
}