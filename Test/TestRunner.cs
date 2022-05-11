using Nim_misere.AI;
using Nim_misere.Game;
using Nim_misere.Player;
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
            var configs = 0;
            var stacks = 0;
            var step = 0;
            var amounts = 0;
            var amountStep = 0;
            var mctsIter = 0;
            while (true)
            {
                Console.WriteLine("How many times should every configuration be repeated?");
                try
                {
                    configs = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (configs <= 0) Console.WriteLine("The number has to be positive. Choose again.\n");
                else
                {
                    configurations = configs;
                    break;
                }
            }

            while (true)
            {
                Console.WriteLine("Select maximum number of stacks. Every number from 1 to the selected number with step will be tested.");
                try
                {
                    stacks = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (stacks <= 0) Console.WriteLine("The number has to be positive. Choose again.\n");
                else
                {
                    stacksNo = stacks;
                    break;
                }
            }

            while (true)
            {
                Console.WriteLine("Select the step for the number of stacks");
                try
                {
                    step = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (step <= 0) Console.WriteLine("The number has to be positive. Choose again.\n");
                else
                {
                    stacksStep = step;
                    break;
                }
            }

            while (true)
            {
                Console.WriteLine("Select maximum number of elements on a stacks. Every number from 1 to the selected number with step will be tested.");
                try
                {
                    amounts = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (amounts <= 0) Console.WriteLine("The number has to be positive. Choose again.\n");
                else
                {
                    stacksAmounts = amounts;
                    break;
                }
            }

            while (true)
            {
                Console.WriteLine("Select the step for the number of elements on a stacks");
                try
                {
                    amountStep = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (amountStep <= 0) Console.WriteLine("The number has to be positive. Choose again.\n");
                else
                {
                    stacksAmountsStep = amountStep;
                    break;
                }
            }

            while (true)
            {
                Console.WriteLine("Select the number of iterations for MCTS.");
                try
                {
                    mctsIter = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (mctsIter <= 0) Console.WriteLine("The number has to be positive. Choose again.\n");
                else
                {
                    mctsIterations = mctsIter;
                    break;
                }
            }
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
            for (int i = 1; i <= stacksNo; i += stacksStep)
            {
                for (int j = 1; j <= stacksAmounts; j += stacksAmountsStep)
                {
                    for( int k = 0; k < configurations; k++)
                    {
                        var state = new State() { Stacks = Enumerable.Repeat(i, j).ToList() };
                        var game1 = new NimMisereGame(new MCTS(numberOfIteration:mctsIterations), new Optimal(), state);
                        var game2 = new NimMisereGame(new Optimal(), new MCTS(numberOfIteration: mctsIterations), state);
                        game1.Start();
                        WriteResults(game1?.winner?.GetName() ?? throw new Exception("Unexpected result of a game!"), i, j);
                        game2.Start();
                        WriteResults(game2?.winner?.GetName() ?? throw new Exception("Unexpected result of a game!"), i, j);
                        Console.Clear();
                        Console.WriteLine($"{counter}/{limit}");
                        counter += 2;
                    }
                }
            }
        }
    }
}
