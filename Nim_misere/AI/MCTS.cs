using Nim_misere.Game;
using Nim_misere.Player;

namespace Nim_misere.AI
{
    public class MCTS : IPlayer
    {
        int numberOfIteration;
        public MCTS(int numberOfIteration = 100) { this.numberOfIteration = numberOfIteration;  }
        public State StateAfterMove(State state, Move move)
        {
            state.Stacks[move.stackNumber - 1] -= move.amount;
            state.PlayerMoveFlag = state.PlayerMoveFlag % 2 + 1;
            return state;
        }

        public Move Move(State initialState)
        {
            var rootnode = new Node(initialState.Clone(), new Move());
            Random random = new Random();
            State iterationState;
            Node node;
            for (int i = 0; i < this.numberOfIteration; i++)
            {
                node = rootnode;
                iterationState = node.state;
                #region Selection
                while (node.untriedMoves.Count == 0 && node.childNodes.Count != 0)
                {
                    node = node.SelectUctChild();
                }
                #endregion
                #region Expansion         
                if (node.untriedMoves.Any())
                {
                    Move move = node.untriedMoves[random.Next(node.untriedMoves.Count)];
                    iterationState = this.StateAfterMove(iterationState, move);
                    node = node.AddChild(iterationState.Clone(), move);
                }
                #endregion
                #region Playout
                var playoutNode = node.Clone();
                while (true)
                {
                    List<Move> allPosibleMoves = playoutNode.GetAllPosibleMoves(playoutNode.state);
                    if (allPosibleMoves.Any())
                    {
                        var move = allPosibleMoves[random.Next(allPosibleMoves.Count)];
                        iterationState = this.StateAfterMove(iterationState, move);
                        playoutNode = new Node(iterationState.Clone(), move);
                        continue;
                    }
                    break;
                }
                #endregion
                #region Backpropagation
                node.Backpropagation(iterationState.PlayerMoveFlag);
                #endregion 
            }
            var m = rootnode.childNodes.OrderByDescending(child => child.visits).First().GetMove();
            Console.WriteLine($"MCTS makes move : {m.ToString()}");
            return m;
        }

        public string GetWinnerPhrase()
        {
            return "MCTS wins !!!";
        }
    }
}
