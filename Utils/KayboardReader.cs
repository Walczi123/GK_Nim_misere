using System;
using System.Collections.Generic;
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
    }
}
