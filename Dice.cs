namespace Itransition.Trainee
{
    public class Dice
    {
        public int[] Faces { get; }
        private const int LENGTH_OF_FACES = 6;

        public Dice(int[] faces)
        {
            if (faces.Length != LENGTH_OF_FACES)
            {
                Console.WriteLine("Error: A dice must have exactly six faces");
                Environment.Exit(0);
            }
            Faces = faces;
        }

        public int Roll(int index)
        {
            return Faces[index];
        }
    }
}