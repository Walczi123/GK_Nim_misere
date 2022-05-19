using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nim_misere.Utils
{
    public static class KayboardReader
    {
        public static int ReadPositiveInteger(string question)
        {
            int number;
            while (true)
            {
                Console.WriteLine(question);
                try
                {
                    number = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (number <= 0) Console.WriteLine("The number has to be positive. Choose again.\n");
                else
                {
                    return number;
                }
            }
        }

        public static int ReadNonnegativeInteger(string question)
        {
            int number;
            while (true)
            {
                Console.WriteLine(question);
                try
                {
                    number = Convert.ToInt32(Console.ReadLine());
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (number < 0) Console.WriteLine("The number has to be nonnegative. Choose again.\n");
                else
                {
                    return number;
                }
            }
        }

        public static T ReadOption<T>(string question, List<T> options) where T: IConvertible
        {
            T item;
            while (true)
            {
                Console.WriteLine(question);
                try
                {
                    item = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
                }
                catch { Console.WriteLine("This is not a number. Choose again.\n"); continue; }

                if (!options.Contains(item)) Console.WriteLine("This number is not one of the options. Choose again.\n");
                else
                {
                    return item;
                }
            }
        }

        public static List<int> ReadStackSizes(int stacksAmount)
        {
            List<int> stackSizes = new List<int>();
            while (true)
            {
                Console.WriteLine("How many elements should be on the stacks? /'E.g. 2,3,4 /'");
                try
                {
                    stackSizes = new List<int>();
                    var answer = Console.ReadLine();
                    if (answer == null)
                        throw new Exception("Empty answer.");
                    var stacks = answer.Split(",");
                    if (stacks.Length != stacksAmount)
                        throw new Exception("Wrong amount of stacks.");
                    foreach (var stack in stacks)
                    {
                        var pars = Int32.TryParse(stack, out var res);
                        if (pars == false)
                            throw new Exception("Not a number.");
                        else
                        {
                            if (res > 0)
                                stackSizes.Add(res);
                            else
                                throw new Exception("Negative number.");
                        }
                    }
                }
                catch { Console.WriteLine("This is not a valid option. Choose again.\n"); continue; }

                if (stackSizes.Count <= 0) Console.WriteLine("It's too few. Choose again.\n");
                else return stackSizes;
            }
        }
    }
}
