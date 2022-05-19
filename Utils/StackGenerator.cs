using Nim_misere.Game;

namespace Nim_misere.Utils
{
    public static class StackGenerator
    {
        public static State GenerateNormal(int stackNumber, int amountNumber)
        {
            var state = new State() { Stacks = new List<int>() };
            for(int i = 0; i < stackNumber; i++)
                state.Stacks.Add(amountNumber);
            return state;
        }

        public static State GenerateRandom(int stackNumber, int maxAmountNumber)
        {
            var state = new State() { Stacks = new List<int>() };
            Random r = new Random();
            for (int i = 0; i < stackNumber; i++)
                state.Stacks.Add(r.Next((int)(maxAmountNumber/2), maxAmountNumber));
            return state;
        }
    }
}
