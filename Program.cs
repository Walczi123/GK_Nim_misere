using Nim_misere.AI;
using Nim_misere.Game;
using Nim_misere.Player;
using Nim_misere.Test;

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

            while (true)
            {
                Console.WriteLine("Do you want to play the game or run tests?\n1 - Play\n2 - Tests");
                try
                {
                    mode = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (mode != 1 && mode != 2) Console.WriteLine("This number is not one of the options. Choose again.\n");
                else break;
            }
            if(mode == 2)
            {
                var testRunner = new TestRunner();
                testRunner.Run();
                Console.WriteLine("The results are saved in the file ./NIM_MISERIE_RESULTS.csv");
                return;
            }

            while (true)
            {
                Console.WriteLine("How many stacks would you like to have?");
                try
                {
                    stacksAmount = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (stacksAmount <= 0) Console.WriteLine("It's too few. Choose again.\n");
                else break;
            }

            while (true)
            {
                Console.WriteLine("How many elements should be on the stacks? /'E.g. 2,3,4 /'");
                try
                {
                    var answer = Console.ReadLine();
                    if (answer == null)
                        throw new Exception("Empty answer.");
                    var stacks = answer.Split(",");
                    if (stacks.Length != stacksAmount)
                        throw new Exception("Wrong amount of stacks.");
                    foreach(var stack in stacks)
                    {
                        var pars = Int32.TryParse(stack, out var res);
                        if (pars == false)
                            throw new Exception("Not a number.");
                        else
                            stackSizes.Add(res);
                    }
                }
                catch { Console.WriteLine("This is not a valid option. Choose again.\n"); continue; }

                if (stackSizes.Count <= 0) Console.WriteLine("It's too few. Choose again.\n");
                else break;
            }

            while (true)
            {
                Console.WriteLine("Which opponent do you choose?\n1 - Optimal\n2 - MCTS");
                try
                {
                    oponent = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (oponent != 1 && oponent != 2) Console.WriteLine("This number is not one of the options. Choose again.\n");
                else break;
            }

            while (true)
            {
                Console.WriteLine("Which player would You like to be?\n1 - First\n2 - Second");
                try
                {
                    turn = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (turn != 1 && turn != 2) Console.WriteLine("This number is not one of the options. Choose again.\n");
                else break;
            }

            Console.WriteLine("\nYour game begins!\n");
            Thread.Sleep(1000);
            Console.Clear();

            var state = new State() { Stacks = stackSizes };
            NimMisereGame game;
            if (oponent == 1)
                if (turn == 1)  
                    game = new NimMisereGame(new Man(), new Optimal(), state);
                else
                    game = new NimMisereGame(new Optimal(), new Man(), state);
            else
                if (turn == 1)
                    game = new NimMisereGame(new Man(), new MCTS(), state);
                else
                    game = new NimMisereGame(new MCTS(), new Man(), state); 
            game.Start();

            Console.WriteLine("Bye");
            Console.ReadKey();
        }
    }
}
