using Nim_misere.Game;

namespace Nim_misere.Player
{
    public interface IPlayer
    {
        Move Move(State state);
        string GetWinnerPhrase();
    }

    public class Man : IPlayer
    {
        public string GetWinnerPhrase()
        {
            return "You win, Congratulations!";
        }

        public Move Move(State state)
        {
            Console.WriteLine("Enter stack number and amount of pices: /' 2,3 /'");
            var line = Console.ReadLine();
            var nums = line.Split(',');
            if (Int32.TryParse(nums[0], out var stackNumber) && Int32.TryParse(nums[1], out var amount))
                return new Move(stackNumber, amount);
            return new Move();
        }

        public Man() { }
    }
}
