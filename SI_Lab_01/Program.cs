using System;
using System.Numerics;

namespace SI_Lab_01
{

    class Program
    {
       
        static void Main(string[] args)
        {
            Vector2[] cities = DataReader.ReadFile();

            int ile = 100000;
                        
            var random = RandomSolution.RandomAlgorithm(cities, ile);
            var randGene = random.gene;
            var randScore = random.score;

            Console.WriteLine("Wyniki Losowego Algorytmu:");
            Console.WriteLine(randScore);
            Utils.PrintGene(randGene);

            Console.WriteLine();
            //-----------------------------------------------------------------------------
            
            var greedy = GreedySolution.GreedyAlgorithm(cities);
            var gredGene = greedy.gene;
            var gredScore = greedy.score;

            Console.WriteLine("Wyniki Algorytmu Zachłannego z losowym punktem startowym:");
            Console.WriteLine(gredScore);
            Utils.PrintGene(gredGene);

        }
    }
}
