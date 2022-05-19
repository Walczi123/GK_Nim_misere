using Nim_misere.AI;
using Nim_misere.Game;
using Nim_misere.Player;
using Nim_misere.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nim_misere.Test
{
    public class TestRunner
    {
        int configurations;
        int stacksNo;
        int stacksStep;
        int stacksAmounts;
        int stacksAmountsStep;
        int mctsIterations;
        public void Run()
        {
            Configure();
            RunTests();
        }

        private void Configure()
        {
            configurations = KayboardReader.ReadPositiveInteger("How many times should every configuration be repeated?");

            stacksNo = KayboardReader.ReadPositiveInteger("Select maximum number of stacks. Every number from 1 to the selected number with step will be tested.");

            stacksStep = KayboardReader.ReadPositiveInteger("Select the step for the number of stacks");

            stacksAmounts = KayboardReader.ReadPositiveInteger("Select maximum number of elements on a stacks. Every number from 1 to the selected number with step will be tested.");

            stacksAmountsStep = KayboardReader.ReadPositiveInteger("Select the step for the number of elements on a stacks");

            mctsIterations = KayboardReader.ReadPositiveInteger("Select the number of iterations for MCTS.");
        }

        private void WriteResults(string winner, int stacks, int amounts)
        {
            string path = @".\NIM_MISERIE_RESULTS.csv";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Winner;Stacks;Amounts");
                    sw.WriteLine($"{winner};{stacks};{amounts}");
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine($"{winner};{stacks};{amounts}");
                }
            }
        }

        private void RunTests()
        {
            var stacksNoRatio = Math.Ceiling((double)stacksNo / (double)stacksStep);
            var stacksAmountRatio = Math.Ceiling((double)stacksAmounts / (double)stacksAmountsStep);
            var limit = stacksNoRatio * stacksAmountRatio * configurations * 2;
            var counter = 2;
            var OptimalWins = 0;
            for (int i = 1; i <= stacksNo; i += stacksStep)
            {
                for (int j = 1; j <= stacksAmounts; j += stacksAmountsStep)
                {
                    for( int k = 0; k < configurations; k++)
                    {
                        var state = new State() { Stacks = Enumerable.Repeat(i, j).ToList() };
                        var state2 = new State() { Stacks = Enumerable.Repeat(i, j).ToList() };
                        var game1 = new NimMisereGame(new MCTS(numberOfIteration:mctsIterations), new Optimal(), state, false);
                        var game2 = new NimMisereGame(new Optimal(), new MCTS(numberOfIteration: mctsIterations), state2, false);
                        game2.PrintStacks();
                        game1.Start();
                        WriteResults(game1?.winner?.GetName() ?? throw new Exception("Unexpected result of a game!"), i, j);
                        if (game1?.winner?.GetName() == "OPTIMAL") OptimalWins += 1;
                        game2.Start();
                        WriteResults(game2?.winner?.GetName() ?? throw new Exception("Unexpected result of a game!"), i, j);
                        if (game2?.winner?.GetName() == "OPTIMAL") OptimalWins += 1;
                        //Console.Clear();
                        Console.WriteLine($"{counter}/{limit}");
                        counter += 2;
                    }
                }
            }
            Console.WriteLine($"\nOptimal algorithm has won {OptimalWins} times");
            Console.WriteLine($"MCTS algorithm has won {counter -1 - OptimalWins} times");
        }
    }


    public class SetBoardTestRunner
    {
        List<int> stackList = new List<int>();
        int testAmounts;
        int playerOne;
        int playerTwo;
        int mctsIterationsOne;
        int mctsIterationsTwo;

        public void Run()
        {
            Configure();
            RunTests();
        }
        
        private int getPlayer(string playerNumber)
        {
            var player = 0;
            while (true)
            {
                Console.WriteLine($"Which algorithm should be {playerNumber}?\n1 - Optimal\n2 - MCTS");
                try
                {
                    player = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (player != 1 && player != 2) Console.WriteLine("This number is not one of the options. Choose again.\n");
                else
                {
                    return player;
                }
            }
        }


        private int getTestAmount()
        {
            var amount = 0;
            while (true)
            {
                Console.WriteLine("How many tests do you want to run?");
                try
                {
                    amount = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (amount <= 0) Console.WriteLine("The number has to be positive. Choose again.\n");
                else
                {
                    return amount;
                    
                }
            }
        }

        private int getMCTSIterations(string playerNumber)
        {
            var mctsIter = 0;
            while (true)
            {
                Console.WriteLine($"Select the number of iterations for {playerNumber} MCTS.");
                try
                {
                    mctsIter = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (mctsIter <= 0) Console.WriteLine("The number has to be positive. Choose again.\n");
                else
                {
                    return mctsIter;
                    
                }
            }
        }
        private void Configure()
        {
            List<int> stackSizes = new List<int>();

            while (true)
            {
                Console.WriteLine("What board do you want to test? Eg. : /' 2,3,1 /'");
                try
                {
                    var answer = Console.ReadLine();
                    if (answer == null)
                        throw new Exception("Empty answer.");
                    var stacks = answer.Split(",");
                    foreach (var stack in stacks)
                    {
                        var pars = Int32.TryParse(stack, out var res);
                        if (pars == false)
                            throw new Exception("Not a number.");
                        else
                        {
                            if (res < 1) throw new Exception("Not a number.");
                            else
                                stackSizes.Add(res);
                        }
                    }
                }
                catch { Console.WriteLine("This is not a valid option. Choose again.\n"); continue; }

                if (stackSizes.Count <= 0) Console.WriteLine("It's too few. Choose again.\n");
                else
                {
                    stackList = stackSizes;
                    break;
                }
            }

            playerOne = getPlayer("first");
            playerTwo = getPlayer("second");
            
            if (playerOne == 2 && playerTwo == 1)
                mctsIterationsOne = getMCTSIterations("");
            if (playerOne == 1 && playerTwo == 2)
                mctsIterationsTwo = getMCTSIterations("");
            if (playerOne == 2 && playerTwo == 2)
            {
                mctsIterationsOne = getMCTSIterations("first");
                mctsIterationsTwo = getMCTSIterations("second");
            }
            
            if (playerOne == 1 && playerTwo == 1)
                testAmounts = 1;
            else
                testAmounts = getTestAmount();

         }

        private void WriteResults(string winner, int stacks, int amounts)
        {
            string path = @".\NIM_MISERIE_RESULTS.csv";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Winner;Stacks;Amounts");
                    sw.WriteLine($"{winner};{stacks};{amounts}");
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine($"{winner};{stacks};{amounts}");
                }
            }
        }

        private void RunTests()
        {
            var counter = 1;
            var OptimalWins = 0;
            Console.WriteLine('\n');

            if (playerOne == 1 && playerTwo == 1) {
                var state = new State() { Stacks = stackList };
                var game = new NimMisereGame(new Optimal(), new Optimal(), state, false);
                int winner = game.Start();
                Console.WriteLine($"\nAlgorithm that won was playing as {winner}."); 
            }
            else {
                for (int i = 1; i <= testAmounts; i += 1)
                {
                    var state = new State() { Stacks = stackList };
                    var game = new NimMisereGame(new MCTS(numberOfIteration: mctsIterations), new Optimal(), state, false);
                    game.Start();
                    WriteResults(game?.winner?.GetName() ?? throw new Exception("Unexpected result of a game!"), i, 0);
                    if (game?.winner?.GetName() == "OPTIMAL") OptimalWins += 1;
                    Console.WriteLine($"{counter}/{testAmounts}");
                    counter += 1;
                }
            }

            Console.WriteLine($"\nOptimal algorithm has won {OptimalWins} times");
            Console.WriteLine($"MCTS algorithm has won {counter -1 - OptimalWins} times");
        }
    }
}
