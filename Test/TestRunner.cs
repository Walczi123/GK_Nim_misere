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
        int stacksSizeLimit;
        int stacksAmountsNumber;
        int mctsIterations;
        public void Run()
        {
            Configure();
            RunTests();
        }

        private void Configure()
        {
            configurations = KayboardReader.ReadPositiveInteger("How many times should every test case with a player relying on random numbers be repeated?");

            stacksNo = KayboardReader.ReadPositiveInteger("The tests will be run for many test cases. The test cases will have different number of stacks - ranging from 1 to N. Please, select maximum number of stacks - N.");

            stacksStep = KayboardReader.ReadPositiveInteger("In order to reduce the number of test cases, you can select the step for number of stacks. E.g. hhe test will be run for the number of stacks 1, 1+step, 1+2*step, 1+3*step, ... .If you want to run the tests for all consecutive numbers choose the step equal to 1. Please, select the step value. ");

            stacksSizeLimit = KayboardReader.ReadPositiveInteger("The tests will be run for number of stacks specified above. The sizes of the stack will be randomly chosen. Please, select the maximum size of a stack. This number will be applied to all stacks.");

            stacksAmountsNumber = KayboardReader.ReadPositiveInteger("In order to check multiple setups of stacks' sizes, it is enabled to select how many test cases with randomized stacks' sizes should be run for every configuration with specified number of stacks. Please, select the number of test cases.");

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
            var limit = stacksNoRatio * stacksAmountsNumber * configurations * 3;
            var counter = 2;
            var OptimalWins = 0;
            for (int i = 1; i <= stacksNo; i += stacksStep)
            {
                for (int j = 1; j <= stacksAmountsNumber; j ++)
                {
                    List<int> stacks = new List<int>();
                    for ( int k = 0; k < configurations; k++)
                    {
                        var state = new State() { Stacks = Enumerable.Repeat(i, j).ToList() };
                        var state2 = new State() { Stacks = Enumerable.Repeat(i, j).ToList() };
                        var state3 = new State() { Stacks = Enumerable.Repeat(i, j).ToList() };
                        var game1 = new NimMisereGame(new MCTS(numberOfIteration:mctsIterations), new Optimal(), state, false);
                        var game2 = new NimMisereGame(new Optimal(), new MCTS(numberOfIteration: mctsIterations), state2, false);
                        var game3 = new NimMisereGame(new MCTS(numberOfIteration: mctsIterations), new MCTS(numberOfIteration: mctsIterations), state3, false);
                        game1.Start();
                        WriteResults(game1?.winner?.GetName() ?? throw new Exception("Unexpected result of a game!"), i, j);
                        if (game1?.winner?.GetName() == "OPTIMAL") OptimalWins += 1;
                        game2.Start();
                        WriteResults(game2?.winner?.GetName() ?? throw new Exception("Unexpected result of a game!"), i, j);
                        if (game2?.winner?.GetName() == "OPTIMAL") OptimalWins += 1;
                        game3.Start();
                        WriteResults(game3?.winner?.GetName() ?? throw new Exception("Unexpected result of a game!"), i, j);
                        if (game3?.winner?.GetName() == "OPTIMAL") OptimalWins += 1;
                        Console.WriteLine($"{counter}/{limit}");
                        counter += 2;
                    }
                    var state4 = new State() { Stacks = Enumerable.Repeat(i, j).ToList() };
                    var game4 = new NimMisereGame(new Optimal(), new Optimal(), state4, false);
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
        int mctsIterations;

        public void Run()
        {
            Configure();
            RunTests();
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

            playerOne = KayboardReader.ReadIntegerFromOptions("Which algorithm should be first?\n1 - Optimal\n2 - MCTS", new List<int> { 1, 2 });
            playerTwo = KayboardReader.ReadIntegerFromOptions("Which algorithm should be second?\n1 - Optimal\n2 - MCTS", new List<int> { 1, 2 });

            if ((playerOne == 1 && playerTwo == 2) || (playerOne == 2 && playerTwo == 1))
                mctsIterations = KayboardReader.ReadPositiveInteger("Select the number of iterations for MCTS.");
            if (playerOne == 2 && playerTwo == 2)
            {
                mctsIterationsOne = KayboardReader.ReadPositiveInteger("Select the number of iterations for first MCTS.");
                mctsIterationsTwo = KayboardReader.ReadPositiveInteger("Select the number of iterations for second MCTS.");
            }
            
            if (playerOne == 1 && playerTwo == 1)
                testAmounts = 1;
            else
                testAmounts = KayboardReader.ReadPositiveInteger("How many tests do you want to run?");

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
            var FirstWins = 0;
            Console.WriteLine('\n');

            if (playerOne == 1 && playerTwo == 1) {
                var state = new State() { Stacks = stackList };
                var game = new NimMisereGame(new Optimal(), new Optimal(), state, false);
                int winner = game.Start();
                Console.WriteLine($"\nAlgorithm that won was playing as {winner}."); 
            }
            else if (playerOne == 1 && playerTwo == 2) {
                for (int i = 1; i <= testAmounts; i += 1)
                {
                    var state = new State() { Stacks = stackList.Clone().ToList() };
                    var game = new NimMisereGame(new Optimal(), new MCTS(numberOfIteration: mctsIterations), state, false);
                    int winner = game.Start();
                    if (game?.winner?.GetName() == "OPTIMAL") OptimalWins += 1;
                }
                Console.WriteLine($"\nOptimal algorithm has won {OptimalWins} times");
                Console.WriteLine($"MCTS algorithm has won {testAmounts - OptimalWins} times");
            }
            else if (playerOne == 2 && playerTwo == 1)
            {
                for (int i = 1; i <= testAmounts; i += 1)
                {
                    var state = new State() { Stacks = stackList.Clone().ToList() };
                    var game = new NimMisereGame(new MCTS(numberOfIteration: mctsIterations), new Optimal(), state, false);
                    int winner = game.Start();
                    if (game?.winner?.GetName() == "OPTIMAL") OptimalWins += 1;
                }
                Console.WriteLine($"\nMCTS algorithm has won {testAmounts - OptimalWins} times");
                Console.WriteLine($"Optimal algorithm has won {OptimalWins} times");
            }
            else
            {
                for (int i = 1; i <= testAmounts; i += 1)
                {
                    var state = new State() { Stacks = stackList.Clone().ToList() };
                    var game = new NimMisereGame(new MCTS(numberOfIteration: mctsIterationsOne), new MCTS(numberOfIteration: mctsIterationsTwo), state, false);
                    int winner = game.Start();
                    if (winner == 1) FirstWins += 1;
                }
                Console.WriteLine($"\nFirst player has won {FirstWins} times");
                Console.WriteLine($"Second player has won {testAmounts - FirstWins} times");
            }


        }
    }
}
