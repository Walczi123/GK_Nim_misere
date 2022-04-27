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
            Console.WriteLine("Enter stack number and amount of pieces: /' 2,3 /'");
            var line = Console.ReadLine();
            var nums = line.Split(',');
            if (Int32.TryParse(nums[0], out var stackNumber) && Int32.TryParse(nums[1], out var amount))
                return new Move(stackNumber, amount);
            return new Move();
        }

        public Man() { }
    }

    public class Optimal : IPlayer
    {
        public string GetWinnerPhrase()
        {
            return "The optimal strategy has won!";
        }

        public Move Move(State state)
        {
            int ones = 0;
            int gr1 = 0;
            int max = 0;
            foreach(var stack in state.Stacks)
            {
                if (stack == 1)
                    ones++;
                else if (stack > 1)
                {
                    gr1++;
                    max = stack;
                }
            }

            if (ones % 2 == 0)
            {
                if (gr1 == 0)
                {
                    return MinimalMove(state);
                }
                else if (gr1 == 1)
                {
                    var st = state.Stacks.FindIndex(x => x == max);
                    return new Move(st + 1, max - 1);
                }
            }
            else
            {
                if (gr1 == 0)
                {
                    return MinimalMove(state);
                }
                else if (gr1 == 1)
                {
                    var st = state.Stacks.FindIndex(x => x == max);
                    return new Move(st + 1, max);
                }
            }

            var allStacksSum = Nim_sum(state);
            if (allStacksSum != 0)
            {
                for (int i = 0; i < state.Stacks.Count; i++)
                {
                    var stackSum = Nim_sum(state.Stacks[i], allStacksSum);
                    if (stackSum < state.Stacks[i])
                        return new Move(i + 1, state.Stacks[i] - stackSum);
                }
                return MinimalMove(state);
            }
            else
                return MinimalMove(state);
        }

        private Move MinimalMove(State state)
        {
            var st = state.Stacks.FindIndex(x => x >= 1);
            return new Move(st + 1, 1);
        }
        private int Nim_sum(State state)
        {
            int res = 0;
            foreach (var stack in state.Stacks)
                res ^= stack;
            return res;
        }

        private int Nim_sum(int int1, int int2)
        {
            return int1 ^ int2;
        }
        public Optimal() { }
    }
}
