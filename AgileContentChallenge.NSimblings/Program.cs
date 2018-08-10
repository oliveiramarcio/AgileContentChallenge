using AgileContentChallenge.DecReprSenior;
using System;

namespace AgileContentChallenge.NSimblings
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("...::: Agile Content Challenge :::...\r\n");
            Console.WriteLine("* 1 - DecReprSenior:\r\n");
            Console.WriteLine("- N Simblings for {0} are {1}", 535, NSimblingsSolution.Solution(535));
            Console.WriteLine("- N Simblings for {0} are {1}", 123, NSimblingsSolution.Solution(123));
            Console.WriteLine("- N Simblings for {0} are {1}", int.MaxValue, NSimblingsSolution.Solution(int.MaxValue));
            Console.WriteLine("- N Simblings for {0} are {1}", 100000000, NSimblingsSolution.Solution(100000000));
            Console.WriteLine("- N Simblings for {0} are {1}", 392, NSimblingsSolution.Solution(392));
            Console.WriteLine("- N Simblings for {0} are {1}\r\n", 213, NSimblingsSolution.Solution(213));
            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}