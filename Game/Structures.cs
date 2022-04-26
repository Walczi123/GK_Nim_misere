namespace Nim_misere.Game
{
    public struct Move
    {
        public int stackNumber;
        public int amount;

        public Move(int stackNumber, int amount)
        {
            this.stackNumber = stackNumber;
            this.amount = amount;
        }

        public override string ToString()
        {
            return $"{stackNumber}, {amount}";
        }
    }
}
