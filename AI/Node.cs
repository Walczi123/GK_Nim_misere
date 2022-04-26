using Nim_misere.Game;

namespace Nim_misere.AI
{
    public class Node
    {
        public int wins;
        public int visits;
        public Node? parent;
        public State state;
        public List<Move> untriedMoves;
        public List<Node> childNodes;
        Move move;

        public Node(State state, Move move, Node? parent = null)
        {
            this.wins = 0;
            this.visits = 0;
            this.parent = parent;
            this.state = state;
            this.move = move;
            this.untriedMoves = this.GetAllPosibleMoves(state);
            this.childNodes = new List<Node>();
        }

        private float GetUctScore()
        {
            double c = Math.Sqrt(2);
            return (float)(this.wins / this.visits + c * Math.Sqrt(Math.Log(this.parent.visits) / this.visits));
        }

        public Node SelectUctChild()
        {
            List<Node> bestChildren = new List<Node>();
            Random random = new Random();
            float bestScore = Single.NegativeInfinity;
            float score;
            Node node;

            for (int i = 0; i < this.childNodes.Count(); i++)
            {
                node = this.childNodes[i];
                score = node.GetUctScore();
                if (score > bestScore)
                {
                    bestScore = score;
                    bestChildren = new List<Node> { node };
                }
                else if (score == bestScore)
                {
                    bestChildren.Add(node);
                }
            }
            return bestChildren[random.Next(bestChildren.Count)];
        }

        internal Node Clone()
        {
            return new Node(this.state.Clone(), this.move, this.parent);
        }

        public Node AddChild(State state, Move move)
        {
            this.untriedMoves.Skip(1);
            var newNode = new Node(state, move, this);
            this.childNodes.Add(newNode);
            return newNode;
        }

        public List<Move> GetAllPosibleMoves(State state)
        {
            List<Move> l = new List<Move>();
            for(int i = 0; i < state.Stacks.Count; i++)
            {
                for(int j = 1; j <= state.Stacks[i]; j++)
                {
                    l.Add(new Move(i + 1, j));
                }
            }
            return l;
        }

        public void Backpropagation(int winnerPlayer)
        {
            if( winnerPlayer == this.state.PlayerMoveFlag)
                this.wins += 1;
            this.visits += 1;
            if (this.parent != null)
                this.parent.Backpropagation(winnerPlayer);
        }

        public Move GetMove()
        {
           return this.move;
        }
    }
}
