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

            var stacksAmount = 0;
            List<int> stackSizes = new List<int>();
            var oponent = 0;
            var turn = 0;
            var mode = 0;
            Console.WriteLine("\n\n");

            mode = KayboardReader.ReadOption<int>(
                "Do you want to play the game, run tests or run test on choosen board?\n1 - Play\n2 - Demo test with set board (watch two algorithms play on choosen board)\n3 - Multiple test with set board (get statistics of multiple games between two algorithms)\n4 - Automatic Batch Tests",
                new List<int>()
                {
                    1,2,3,4
                }
                );
            if (mode == 2)
            {
                var testRunner = new SetBoardTestRunner();
                testRunner.Run(true);
                return;
            }
            if (mode == 3)
            {
                var testRunner = new SetBoardTestRunner();
                testRunner.Run(false);
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

            //#region mcts 100 vs mcst 100
            //Console.WriteLine("\n Start mcts 100 vs mcst 100");
            //var gameReslut1 = new GameResult("MCTS100", "MCTS100", "random");
            int player1wins = 0;
            int player2wins = 0;
            //for(int i = 0; i < 1000; i++)
            //{
            //    var game = new NimMisereGame(new MCTS(100), new MCTS(100), StackGenerator.GenerateRandom(10, 10), false);
            //    if (game.Start() == 1)
            //        player1wins++;
            //    else
            //        player2wins++;
            //}
            //gameReslut1.AddReslut(player1wins, player2wins);
            //results.Add(gameReslut1);
            //#endregion

            //#region mcts 500 vs mcst 500
            //Console.WriteLine("\n Start mcts 500 vs mcst 500");
            //var gameReslut2 = new GameResult("MCTS500", "MCTS500", "random");
            //player1wins = 0;
            //player2wins = 0;
            //for (int i = 0; i < 1000; i++)
            //{
            //    var game = new NimMisereGame(new MCTS(500), new MCTS(500), StackGenerator.GenerateRandom(10, 10), false);
            //    if (game.Start() == 1)
            //        player1wins++;
            //    else
            //        player2wins++;
            //}
            //gameReslut2.AddReslut(player1wins, player2wins);
            //results.Add(gameReslut2);
            //#endregion

            //#region mcts 1000 vs mcst 1000
            //Console.WriteLine("\n Start mcts 1000 vs mcst 1000");
            //var gameReslut3 = new GameResult("MCTS1000", "MCTS1000", "random");
            //player1wins = 0;
            //player2wins = 0;
            //for (int i = 0; i < 1000; i++)
            //{
            //    var game = new NimMisereGame(new MCTS(1000), new MCTS(1000), StackGenerator.GenerateRandom(10, 10), false);
            //    if (game.Start() == 1)
            //        player1wins++;
            //    else
            //        player2wins++;
            //}
            //gameReslut3.AddReslut(player1wins, player2wins);
            //results.Add(gameReslut3);
            //#endregion

            //#region optimal vs mcst 100
            //Console.WriteLine("\n Start optimal vs mcst 100");
            //var gameReslut4 = new GameResult("Optimal", "MCTS100", "random");
            //player1wins = 0;
            //player2wins = 0;
            //for (int i = 0; i < 1000; i++)
            //{
            //    var game = new NimMisereGame(new Optimal(), new MCTS(100), StackGenerator.GenerateRandom(10, 10), false);
            //    if (game.Start() == 1)
            //        player1wins++;
            //    else
            //        player2wins++;
            //}
            //gameReslut4.AddReslut(player1wins, player2wins);
            //results.Add(gameReslut4);
            //#endregion

            //#region optimal vs mcst 500
            //Console.WriteLine("\n Start optimal vs mcst 500");
            //var gameReslut5 = new GameResult("Optimal", "MCTS500", "random");
            //player1wins = 0;
            //player2wins = 0;
            //for (int i = 0; i < 1000; i++)
            //{
            //    var game = new NimMisereGame(new Optimal(), new MCTS(500), StackGenerator.GenerateRandom(10, 10), false);
            //    if (game.Start() == 1)
            //        player1wins++;
            //    else
            //        player2wins++;
            //}
            //gameReslut5.AddReslut(player1wins, player2wins);
            //results.Add(gameReslut5);
            //#endregion

            //#region optimal vs mcst 1000
            //Console.WriteLine("\n Start optiaml vs mcst 500");
            //var gameReslut6 = new GameResult("Optimal", "MCTS1000", "random");
            //player1wins = 0;
            //player2wins = 0;
            //for (int i = 0; i < 1000; i++)
            //{
            //    var game = new NimMisereGame(new Optimal(), new MCTS(1000), StackGenerator.GenerateRandom(10, 10), false);
            //    if (game.Start() == 1)
            //        player1wins++;
            //    else
            //        player2wins++;
            //}
            //gameReslut6.AddReslut(player1wins, player2wins);
            //results.Add(gameReslut6);
            //#endregion

            //#region mcst 100 vs optimal
            //Console.WriteLine("\n Start mcst 100 vs optimal");
            //var gameReslut7 = new GameResult("MCTS100","Optimal", "random");
            //player1wins = 0;
            //player2wins = 0;
            //for (int i = 0; i < 1000; i++)
            //{
            //    var game = new NimMisereGame(new MCTS(100), new Optimal(), StackGenerator.GenerateRandom(10, 10), false);
            //    if (game.Start() == 1)
            //        player1wins++;
            //    else
            //        player2wins++;
            //}
            //gameReslut7.AddReslut(player1wins, player2wins);
            //results.Add(gameReslut7);
            //#endregion

            //#region mcst 500 vs optimal
            //Console.WriteLine("\n Start mcst 500 vs optimal");
            //var gameReslut8 = new GameResult("MCTS500", "Optimal", "random");
            //player1wins = 0;
            //player2wins = 0;
            //for (int i = 0; i < 1000; i++)
            //{
            //    var game = new NimMisereGame(new MCTS(500), new Optimal(), StackGenerator.GenerateRandom(10, 10), false);
            //    if (game.Start() == 1)
            //        player1wins++;
            //    else
            //        player2wins++;
            //}
            //gameReslut8.AddReslut(player1wins, player2wins);
            //results.Add(gameReslut8);
            //#endregion

            //#region mcst 1000 vs optimal
            //Console.WriteLine("\n Start mcst 1000 vs optimal");
            //var gameReslut9 = new GameResult("MCTS1000", "Optimal", "random");
            //player1wins = 0;
            //player2wins = 0;
            //for (int i = 0; i < 1000; i++)
            //{
            //    var game = new NimMisereGame(new MCTS(1000), new Optimal(), StackGenerator.GenerateRandom(10, 10), false);
            //    if (game.Start() == 1)
            //        player1wins++;
            //    else
            //        player2wins++;
            //}
            //gameReslut9.AddReslut(player1wins, player2wins);
            //results.Add(gameReslut9);
            //#endregion

            //#region optimal vs optimal
            //Console.WriteLine("\n Start optimal vs optimal");
            //var state1 = StackGenerator.GenerateNormal(5, 10);
            //var gameReslut10 = new GameResult("Optimal", "Optimal", state1.ToString());
            //player1wins = 0;
            //player2wins = 0;
            //for (int i = 0; i < 20; i++)
            //{
            //    var game = new NimMisereGame(new Optimal(), new Optimal(), state1.Clone(), false);
            //    if (game.Start() == 1)
            //        player1wins++;
            //    else
            //        player2wins++;
            //}
            //gameReslut10.AddReslut(player1wins, player2wins);
            //results.Add(gameReslut10);
            //#endregion

            //#region optimal vs optimal
            //Console.WriteLine("\n Start optimal vs optimal");
            //var state2 = new State() { Stacks = new List<int>() { 4, 5, 8, 9}};
            //var gameReslut11 = new GameResult("Optimal", "Optimal", state2.ToString());
            //player1wins = 0;
            //player2wins = 0;
            //for (int i = 0; i < 20; i++)
            //{
            //    var game = new NimMisereGame(new Optimal(), new Optimal(), state2.Clone(), false);
            //    if (game.Start() == 1)
            //        player1wins++;
            //    else
            //        player2wins++;
            //}
            //gameReslut11.AddReslut(player1wins, player2wins);
            //results.Add(gameReslut11);
            //#endregion

            #region optimal vs mcst 100
            Console.WriteLine("\n Start optimal vs mcst 100");
            var gameReslut12 = new GameResult("Optimal", "MCTS100", "random3");
            player1wins = 0;
            player2wins = 0;
            for (int i = 0; i < 1000; i++)
            {
                var game = new NimMisereGame(new Optimal(), new MCTS(100), StackGenerator.GenerateRandom(3, 3), false);
                if (game.Start() == 1)
                    player1wins++;
                else
                    player2wins++;
            }
            gameReslut12.AddReslut(player1wins, player2wins);
            results.Add(gameReslut12);
            #endregion

            #region optimal vs mcst 500
            Console.WriteLine("\n Start optimal vs mcst 500");
            var gameReslut13 = new GameResult("Optimal", "MCTS500", "random3");
            player1wins = 0;
            player2wins = 0;
            for (int i = 0; i < 1000; i++)
            {
                var game = new NimMisereGame(new Optimal(), new MCTS(500), StackGenerator.GenerateRandom(3, 3), false);
                if (game.Start() == 1)
                    player1wins++;
                else
                    player2wins++;
            }
            gameReslut13.AddReslut(player1wins, player2wins);
            results.Add(gameReslut13);
            #endregion

            #region optimal vs mcst 1000
            Console.WriteLine("\n Start optiaml vs mcst 500");
            var gameReslut14 = new GameResult("Optimal", "MCTS1000", "random3");
            player1wins = 0;
            player2wins = 0;
            for (int i = 0; i < 1000; i++)
            {
                var game = new NimMisereGame(new Optimal(), new MCTS(1000), StackGenerator.GenerateRandom(3, 3), false);
                if (game.Start() == 1)
                    player1wins++;
                else
                    player2wins++;
            }
            gameReslut14.AddReslut(player1wins, player2wins);
            results.Add(gameReslut14);
            #endregion

            #region mcst 100 vs optimal
            Console.WriteLine("\n Start mcst 100 vs optimal");
            var gameReslut15 = new GameResult("MCTS100", "Optimal", "random3");
            player1wins = 0;
            player2wins = 0;
            for (int i = 0; i < 1000; i++)
            {
                var game = new NimMisereGame(new MCTS(100), new Optimal(), StackGenerator.GenerateRandom(3, 3), false);
                if (game.Start() == 1)
                    player1wins++;
                else
                    player2wins++;
            }
            gameReslut15.AddReslut(player1wins, player2wins);
            results.Add(gameReslut15);
            #endregion

            #region mcst 500 vs optimal
            Console.WriteLine("\n Start mcst 500 vs optimal");
            var gameReslut16 = new GameResult("MCTS500", "Optimal", "random3");
            player1wins = 0;
            player2wins = 0;
            for (int i = 0; i < 1000; i++)
            {
                var game = new NimMisereGame(new MCTS(500), new Optimal(), StackGenerator.GenerateRandom(3, 3), false);
                if (game.Start() == 1)
                    player1wins++;
                else
                    player2wins++;
            }
            gameReslut16.AddReslut(player1wins, player2wins);
            results.Add(gameReslut16);
            #endregion

            #region mcst 1000 vs optimal
            Console.WriteLine("\n Start mcst 1000 vs optimal");
            var gameReslut17 = new GameResult("MCTS1000", "Optimal", "random3");
            player1wins = 0;
            player2wins = 0;
            for (int i = 0; i < 1000; i++)
            {
                var game = new NimMisereGame(new MCTS(1000), new Optimal(), StackGenerator.GenerateRandom(3, 3), false);
                if (game.Start() == 1)
                    player1wins++;
                else
                    player2wins++;
            }
            gameReslut17.AddReslut(player1wins, player2wins);
            results.Add(gameReslut17);
            #endregion

            #region mcts 100 vs mcst 100
            Console.WriteLine("\n Start mcts 100 vs mcst 100");
            var gameReslut1 = new GameResult("MCTS100", "MCTS100", "random3");
            player1wins = 0;
            player2wins = 0;
            for (int i = 0; i < 1000; i++)
            {
                var game = new NimMisereGame(new MCTS(100), new MCTS(100), StackGenerator.GenerateRandom(3, 3), false);
                if (game.Start() == 1)
                    player1wins++;
                else
                    player2wins++;
            }
            gameReslut1.AddReslut(player1wins, player2wins);
            results.Add(gameReslut1);
            #endregion

            #region mcts 500 vs mcst 500
            Console.WriteLine("\n Start mcts 500 vs mcst 500");
            var gameReslut2 = new GameResult("MCTS500", "MCTS500", "random3");
            player1wins = 0;
            player2wins = 0;
            for (int i = 0; i < 1000; i++)
            {
                var game = new NimMisereGame(new MCTS(500), new MCTS(500), StackGenerator.GenerateRandom(3, 3), false);
                if (game.Start() == 1)
                    player1wins++;
                else
                    player2wins++;
            }
            gameReslut2.AddReslut(player1wins, player2wins);
            results.Add(gameReslut2);
            #endregion

            #region mcts 1000 vs mcst 1000
            Console.WriteLine("\n Start mcts 1000 vs mcst 1000");
            var gameReslut3 = new GameResult("MCTS1000", "MCTS1000", "random3");
            player1wins = 0;
            player2wins = 0;
            for (int i = 0; i < 1000; i++)
            {
                var game = new NimMisereGame(new MCTS(1000), new MCTS(1000), StackGenerator.GenerateRandom(3, 3), false);
                if (game.Start() == 1)
                    player1wins++;
                else
                    player2wins++;
            }
            gameReslut3.AddReslut(player1wins, player2wins);
            results.Add(gameReslut3);
            #endregion

            FileHandler.WriteFile(results);
        }
    }
}
