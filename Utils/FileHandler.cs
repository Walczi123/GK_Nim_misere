namespace Nim_misere.Utils
{
    public static class FileHandler
    {
        public static void WriteFile(List<GameResult> results)
        {
            string filename = "../../../output.txt";

            using (StreamWriter sw = File.CreateText(filename))
            {
                int count = 0;
                foreach (var instance in results)
                {

                    sw.Write($"{++count}. ");
                    sw.Write($"{instance.player1};{instance.player1wins};{instance.player2};{instance.player2wins};{instance.stacks}");
                    sw.WriteLine();
                }
            }

        }
    }

    public class GameResult
    {
        public string player1;
        public string player2;
        public int player1wins;
        public int player2wins;
        public string stacks;

        public GameResult(string player1, string player2, string stacks)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.stacks = stacks;
        }

        public void AddReslut(int player1wins, int player2wins)
        {
            this.player1wins = player1wins;
            this.player2wins = player2wins;
        }
    }
}
