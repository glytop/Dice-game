using ConsoleTables;

namespace Itransition.Trainee
{
    public class ProbabilityTableBuilder
    {
        private List<Dice> _diceList;
        private ProbabilityCalculator _probabilityCalculator;

        public ProbabilityTableBuilder(List<Dice> diceList)
        {
            _diceList = diceList;
            _probabilityCalculator = new();
        }

        public void DisplayProbabilityTable()
        {
            var headers = new List<string> { "User Dice v" };
            headers.AddRange(_diceList.Select(dice => string.Join(",", dice.Faces)));

            var table = new ConsoleTable(headers.ToArray());

            for (int i = 0; i < _diceList.Count; i++)
            {
                var row = new List<string> { string.Join(",", _diceList[i].Faces) };

                for (int j = 0; j < _diceList.Count; j++)
                {
                    if (i == j)
                    {
                        row.Add("- (0.3333)");
                    }
                    else
                    {
                        row.Add(_probabilityCalculator.CalculateWinProbability(_diceList[i], _diceList[j]).ToString("0.####"));
                    }
                }

                table.AddRow(row.ToArray());
            }

            Console.WriteLine("Probability of the win for the user:");
            Console.WriteLine(table);
        }

    }
}
