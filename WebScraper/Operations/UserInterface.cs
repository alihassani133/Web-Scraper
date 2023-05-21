using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.Operations
{
    class UserInterface
    {
        public static void Waiting()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nPlease wait...\n");
            Console.ResetColor();
            Thread.Sleep(2000);
        }
        public static void Done()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nProcess done!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
        }
    }
}
