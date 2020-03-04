using System;
using System.IO;

namespace SI_Lab_01
{
    class Program
    {
        static void Main(string[] args)
        {
            const string f = "berlin11_modified.tsp";

            var lines = File.ReadAllLines(f);

            foreach(var line in lines)
            {
                Console.WriteLine(line);
            }
            
        }
    }
}
