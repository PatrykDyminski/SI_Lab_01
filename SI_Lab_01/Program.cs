using System;
using System.Numerics;

namespace SI_Lab_01
{

    class Program
    {
       
        static void Main(string[] args)
        {
            string berlin11 = "berlin11_modified.tsp";
            string berlin52 = "berlin52.tsp";

            string filename = berlin52;
            Vector2[] cities = DataReader.ReadFile(filename);

            /*
            int ile = 100000;

            //-----------------------------------------------------------------------------
            // testowanie algorytmu losowego
                        
            var random = RandomSolution.RandomAlgorithm(cities, ile);
            var randGene = random.gene;
            var randScore = random.score;

            Console.WriteLine("Wyniki Losowego Algorytmu:");
            Console.WriteLine(randScore);
            Utils.PrintGene(randGene);

            Console.WriteLine();
            //-----------------------------------------------------------------------------
            // testowanie algorytmu zachłannego

            var greedy = GreedySolution.GreedyAlgorithm(cities);
            var gredGene = greedy.gene;
            var gredScore = greedy.score;

            Console.WriteLine("Wyniki Algorytmu Zachłannego z losowym punktem startowym:");
            Console.WriteLine(gredScore);
            Utils.PrintGene(gredGene);

            Console.WriteLine();
            //-----------------------------------------------------------------------------

            

            //-----------------------------------------------------------------------------
            // testowanie mutacji


            var geneToMut = Utils.RandomGene(10);
            Utils.PrintGene(geneToMut);

            var mutatedGene = GeneticSolution.Mutate(geneToMut);
            Utils.PrintGene(geneToMut);
            Utils.PrintGene(mutatedGene);

            Console.WriteLine();


            //-----------------------------------------------------------------------------
            // testowanie selekcji turniejowej


            var pop = Utils.RandomPopulation(100, cities.Length);
            var newPop = GeneticSolution.TourSelect(pop, 5, cities);


            //-----------------------------------------------------------------------------
            // testowanie krzyżowania


            int[] a = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] b = new int[] { 5,7,4,9,1,3,6,2,8 };

            var x = GeneticSolution.Cross(a, b);
   

            

            //-----------------------------------------------------------------------------
            // testowanie selekcji ruletkowej


            var pop = Utils.RandomPopulation(20, cities.Length);
            var newPop = GeneticSolution.RussianSelect(pop, 5, cities);

            */

            var popSize = 100;
            var generations = 1000;
            var crossProb = 0.85f;
            var mutProb = 0.5f;
            var tourSize = 5;

            var genetic = GeneticSolution.GeneticAlgorithm(cities,popSize,generations,crossProb,mutProb,tourSize);

            Console.WriteLine(genetic.score);
            Utils.PrintGene(genetic.gene);
        }
    }
}
