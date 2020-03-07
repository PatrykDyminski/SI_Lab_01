using System;
using System.Numerics;

namespace SI_Lab_01
{

    class Program
    {
       
        static void Main(string[] args)
        {
            
            Vector2[] cities = DataReader.ReadFile();

            /*
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

            Console.WriteLine();
            //-----------------------------------------------------------------------------


            */

            var popSize = 100;
            var generations = 100;
            var crossProb = 0.7;
            var mutProb = 0.1;
            var tourSize = 5;


            var geneToMut = Utils.RandomGene(10);
            Utils.PrintGene(geneToMut);

            var mutatedGene = GeneticSolution.Mutate(geneToMut);
            Utils.PrintGene(geneToMut);
            Utils.PrintGene(mutatedGene);

            Console.WriteLine();
            //-----------------------------------------------------------------------------

            var pop = Utils.RandomPopulation(100, cities.Length);

            var newPop = GeneticSolution.TourSelect(pop, 5, cities);



        }
    }
}
