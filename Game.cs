using ConsoleTables;
using Org.BouncyCastle.Crypto.Digests;

namespace Itransition.Trainee
{
    public class Game
    {
        private const int FAIR_RANDOM_MAX_VALUE_FIRST_PLAYER = 1;
        private const int FAIR_RANDOM_MAX_VALUE_ROLL_DICE = 5;
        private const int DICE_SIDES = 6;

        private List<Dice> _diceList;
        private ProbabilityTableBuilder _probabilityTableBuilder;
        private FairRandom _fairRandom;
        private int _firstPlayer;
        private Dice _userDice;
        private Dice _computerDice;

        public Game(List<Dice> diceList)
        {
            _diceList = diceList;
            _probabilityTableBuilder = new(_diceList);
        }

        public void Start()
        {
            DetermineFirstPlayer();
            ChooseDice();
            PlayRound();
        }

        private void DetermineFirstPlayer()
        {
            _fairRandom = new FairRandom(FAIR_RANDOM_MAX_VALUE_FIRST_PLAYER);
            Console.WriteLine($"I selected a random value in the range 0..{FAIR_RANDOM_MAX_VALUE_FIRST_PLAYER} (HMAC={_fairRandom.GetHMAC()})");
            Console.WriteLine("Try to guess my selection: ");
            
            var userGuess = GetUserSelection(["0", "1"]);
            var computerValue = _fairRandom.RevealSecret(out string key);
            Console.WriteLine($"My selection: {computerValue} (KEY={key})");
            _firstPlayer = userGuess == computerValue ? 0 : 1;
        }

        private void ChooseDice()
        {
            _fairRandom = new FairRandom(FAIR_RANDOM_MAX_VALUE_FIRST_PLAYER);
            if (_firstPlayer == 0)
            {
                DisplayDiceOptions();

                Console.WriteLine("Choose your dice (index): ");
                var userChoice = GetUserSelection(Enumerable
                    .Range(0, _diceList.Count)
                    .Select(i => i.ToString())
                    .ToArray());
                _userDice = _diceList[userChoice];
                _diceList.Remove(_userDice);

                _fairRandom.GetHMAC();
                var computerChoice = _fairRandom.RevealSecret(out string key);
                _computerDice = _diceList[computerChoice];
                Console.WriteLine($"My selection: {string.Join(",", _computerDice.Faces)}");

            }
            else
            {
                _fairRandom.GetHMAC();
                var computerChoice = _fairRandom.RevealSecret(out string key);

                _computerDice = _diceList[computerChoice];
                Console.WriteLine($"I make the first move and choose the: {string.Join(",", _computerDice.Faces)}");
                _diceList.Remove(_computerDice);

                DisplayDiceOptions();

                Console.WriteLine("Choose your dice (index): ");
                var userChoice = GetUserSelection(Enumerable
                    .Range(0, _diceList.Count)
                    .Select(i => i.ToString())
                    .ToArray());
                _userDice = _diceList[userChoice];
            }
        }

        private void DisplayDiceOptions()
        {
            var table = new ConsoleTable("Index", "Dice Faces");
            for (int i = 0; i < _diceList.Count; i++)
            {
                table.AddRow(i, string.Join(",", _diceList[i].Faces));
            }
            Console.WriteLine(table);
        }

        private void PlayRound()
        {
            Console.WriteLine("Rolling dice...");
            var userRoll = RollDice(_userDice);
            var computerRoll = RollDice(_computerDice);

            Console.WriteLine($"Your roll: {userRoll}");
            Console.WriteLine($"Computer's roll: {computerRoll}");

            if (userRoll > computerRoll)
            {
                Console.WriteLine("You win!");
            }
            else if (userRoll < computerRoll)
            {
                Console.WriteLine("Computer wins!");
            }
            else
            {
                Console.WriteLine("It's a draw!");
            }
        }

        private int RollDice(Dice dice)
        {
            _fairRandom = new FairRandom(FAIR_RANDOM_MAX_VALUE_ROLL_DICE);
            Console.WriteLine($"I selected a random value in the range 0..{FAIR_RANDOM_MAX_VALUE_ROLL_DICE} (HMAC={_fairRandom.GetHMAC()})");
            Console.WriteLine("Enter your number (0..5): ");

            var userNumber = GetUserSelection(["0", "1", "2", "3", "4", "5"]);
            var computerNumber = _fairRandom.RevealSecret(out string key);

            Console.WriteLine($"My number is {computerNumber} (KEY={key})");
            var resultIndex = (userNumber + computerNumber) % DICE_SIDES;
            Console.WriteLine($"The result is {userNumber} + {computerNumber} = {resultIndex} (mod 6)");

            return dice.Roll(resultIndex);
        }

        private int GetUserSelection(string[] validOptions)
        {
            while (true)
            {
                Console.WriteLine("X - Exit"
                                + "\n? - Help");
                var input = Console.ReadLine()
                    .Trim()
                    .ToLower();
                if (input == "x")
                {
                    Environment.Exit(0);
                }
                if (input == "?")
                {
                    _probabilityTableBuilder.DisplayProbabilityTable();
                    continue;
                }
                if (validOptions.Contains(input))
                {
                    return int.Parse(input);
                }
                Console.WriteLine("Invalid input. Try again");
            }
        }
    }
}
