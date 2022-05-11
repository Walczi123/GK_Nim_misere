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
            var stackSize = 0;
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

                if (mode != 1 && mode != 2) Console.WriteLine("This number is not amount options. Choose again.\n");
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

                if (stacksAmount <= 0) Console.WriteLine("It's too little. Choose again.\n");
                else break;
            }

            while (true)
            {
                Console.WriteLine("How many elements should be on one stack?");
                try
                {
                    stackSize = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (stackSize < 0) Console.WriteLine("It's too short. Choose again.\n");
                else break;
            }

            while (true)
            {
                Console.WriteLine("Which oponent do you choose?\n1 - Optimal\n2 - MCTS");
                try
                {
                    oponent = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (oponent != 1 && oponent != 2) Console.WriteLine("This number is not amount options. Choose again.\n");
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

                if (turn != 1 && turn != 2) Console.WriteLine("This number is not amount options. Choose again.\n");
                else break;
            }

            Console.WriteLine("\nYour game begins!\n");
            Thread.Sleep(1000);
            Console.Clear();

            var state = new State() { Stacks = Enumerable.Repeat(stackSize, stacksAmount).ToList() };
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
