namespace Itransition.Trainee
{
    public class ProbabilityCalculator
    {
        public double CalculateWinProbability(Dice dice1, Dice dice2)
        {
            var dice1Wins = 0;
            var totalRounds = 0;

            for (var i = 0; i < dice1.Faces.Length; i++)
            {
                for (var j = 0; j < dice2.Faces.Length; j++)
                {
                    totalRounds++;
                    if (dice1.Faces[i] > dice2.Faces[j])
                    {
                        dice1Wins++;
                    }
                }
            }

            return (double)dice1Wins / totalRounds;
        }
    }
}
