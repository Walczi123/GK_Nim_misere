using Nim_misere.Player;

namespace Nim_misere.Game
{
    public class State
    {
        public List<int> Stacks { get; set; }
        public int PlayerMoveFlag = 1;

        public bool AreEmpty()
        {
            return !this.Stacks.Where(x => x != 0).Any();
        }

        public State Clone()
        {
            return new State() { Stacks = new List<int>(this.Stacks) };
        }
        public void UpdateState(Move move)
        {
            this.Stacks[move.stackNumber - 1] -= move.amount;
        }
    }

    public class NimMisereGame
    {
        private IPlayer player1;
        private IPlayer player2;
        private State state;

        public NimMisereGame(IPlayer player1, IPlayer player2, State state)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.state = state;
        }

        public void Start()
        {
            IPlayer currentPlayer = this.player1;
            while (!this.state.AreEmpty())
            {
                PrintStacks();
                var move = currentPlayer.Move(this.state);
                this.state.UpdateState(move);
                currentPlayer = SwitchPlayer(currentPlayer);
            }

            Console.WriteLine(currentPlayer.GetWinnerPhrase());
        }

        private IPlayer SwitchPlayer(IPlayer currentPlayer)
        {
            if (currentPlayer == this.player1)
            {
                this.state.PlayerMoveFlag = 2;
                return this.player2;
            }
            else
            {
                this.state.PlayerMoveFlag = 1;
                return this.player1;
            }             
        }

        private void PrintStacks()
        {
            foreach( var value in this.state.Stacks )
                Console.Write($"{value} ");
            Console.Write("\n");
        }
    }

    static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
