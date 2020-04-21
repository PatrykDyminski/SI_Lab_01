using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SI_Lab_01
{

    class Program
    {
       
        static void Main(string[] args)
        {
            string berlin11 = "berlin11_modified.tsp";
            string fl = "fl417.tsp";
            string kroA150 = "kroA150.tsp";
            string kroA100 = "kroA100.tsp";
            string kroA200 = "kroA200.tsp";
            string berlin52 = "berlin52.tsp";

            string filename = kroA150;
            Vector2[] cities = DataReader.ReadFile(filename);

            var popSize = 600;
            var generations = 1000;
            var crossProb = 0.85f;
            var mutProb = 0.35f;
            var tourSize = 600;

            int cycles = 10;

            var testResult = Utils.runTests(cycles, cities, popSize, generations,crossProb,mutProb,tourSize);
            //var testResult = RandomSolution.RandomAlgorithm2(cities, 1000000);
            //var testResult = GreedySolution.GreedyAlgorithmAll(cities);

            Console.WriteLine("Best: " + testResult.best);
            Console.WriteLine("Worst: " + testResult.worst);
            Console.WriteLine("Avg: " + testResult.avg);
            Console.WriteLine("STD: " + testResult.std);

        }

    }
}
