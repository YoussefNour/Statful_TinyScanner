using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            scanner sc = new scanner();
            List<Token> tokens = new List<Token>();
            tokens = sc.GetTokens(@"D:\Projects\C#\scannerRobear\ConsoleApp2\code.txt");
            foreach(Token t in tokens)
            {
                t.printtoken();
            }

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

            Console.ReadLine();
        }

    }
}
