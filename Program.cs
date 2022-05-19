using Nim_misere.AI;
using Nim_misere.Game;
using Nim_misere.Player;
using Nim_misere.Test;
using Nim_misere.Utils;

namespace Nim
{
    class Program
    {
        static void Main(string[] args)
        {
            Tests();
            return;

            var stacksAmount = 0;
            List<int> stackSizes = new List<int>();
            var oponent = 0;
            var turn = 0;
            var mode = 0;
            Console.WriteLine("\n\n");

            mode = KayboardReader.ReadOption<int>(
                "Do you want to play the game, run tests or run test on choosen board?\n1 - Play\n2 - Demo test with set board (watch two algorithms play on choosen board)\n3 - Multiple test with set board (get statistics of multiple games between two algorithms)\n4 - Tests",
                new List<int>()
                {
                    1,2,3,4
                }
                );
            if (mode == 2)
            {
                var testRunner = new SetBoardTestRunner();
                testRunner.Run(true);
                Console.WriteLine("\nThe results are saved in the file ./NIM_MISERIE_RESULTS.csv");
                return;
            }
            if (mode == 3)
            {
                var testRunner = new SetBoardTestRunner();
                testRunner.Run(false);
                Console.WriteLine("\nThe results are saved in the file ./NIM_MISERIE_RESULTS.csv");
                return;
            }
            if (mode == 4)
            {
                var testRunner = new TestRunner();
                testRunner.Run();
                Console.WriteLine("\nThe results are saved in the file ./NIM_MISERIE_RESULTS.csv");
                return;
            }


            stacksAmount = KayboardReader.ReadPositiveInteger("How many stacks would you like to have?");

            stackSizes = KayboardReader.ReadStackSizes(stacksAmount);

            oponent = KayboardReader.ReadOption<int>(
                "Which opponent do you choose?\n1 - Optimal\n2 - MCTS",
                new List<int>()
                {
                    1,2
                });

            turn = KayboardReader.ReadOption<int>(
                "Which player would You like to be?\n1 - First\n2 - Second",
                new List<int>()
                {
                                1,2
                });

            Console.WriteLine("\nYour game begins!\n");
            Thread.Sleep(1000);
            Console.Clear();

            var state = new State() { Stacks = stackSizes };
            NimMisereGame game;
            if (oponent == 1)
                if (turn == 1)  
                    game = new NimMisereGame(new Man(), new Optimal(), state, true);
                else
                    game = new NimMisereGame(new Optimal(), new Man(), state, true);
            else
                if (turn == 1)
                    game = new NimMisereGame(new Man(), new MCTS(), state, true);
                else
                    game = new NimMisereGame(new MCTS(), new Man(), state, true); 
            game.Start();

            Console.WriteLine("Bye");
            Console.ReadKey();
        }

        static void Tests()
        {
            var results = new List<GameResult>();
            var state = StackGenerator.GenerateNormal(10, 10);
            var gameReslut1 = new GameResult("MCTS100", "MCTS100", state.ToString());
            int player1wins = 0;
            int player2wins = 0;
            for(int i = 0; i < 10; i++)
            {
                var game = new NimMisereGame(new MCTS(100), new MCTS(100), state.Clone(), false);
                if (game.Start() == 1)
                    player1wins++;
                else
                    player2wins++;
            }
            gameReslut1.AddReslut(player1wins, player2wins);
            results.Add(gameReslut1);



            FileHandler.WriteFile(results);
        }
    }
}
