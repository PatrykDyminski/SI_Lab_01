using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SI_Lab_01
{
    class GeneticSolution
    {
        public static void Cross()
        {

        }

        public static int[] Mutate(int[] genotype)
        {
            Random rnd = new Random();
            int i = rnd.Next(0, genotype.Length);
            int j = rnd.Next(0, genotype.Length);
            int v = genotype[i];
            genotype[i] = genotype[j];
            genotype[j] = v;

            return genotype;
        }

        public static int[][] TourSelect(int[][] pop, int tourSize, Vector2[] cities)
        {
            Console.WriteLine("inputPop");
            Utils.PrintPopulation(pop);

            var popSize = pop.Length;

            float[] sums = new float[popSize];

            //liczenie wszytkich sum
            for (int i = 0; i < popSize; i++)
            {
                sums[i] = Utils.SumDistance(pop[i], cities);
            }

            sums.ToList().ForEach(i => Console.WriteLine(i.ToString()));

            int[][] selectedPop = new int[popSize][];

            //wybór tylu osobników ile jest w populacji
            for (int i = 0; i < popSize; i++)
            {
                Random rnd = new Random();

                int[] randomSelected = new int[tourSize];

                //losowanie osobników do selekcji
                for (int j = 0; j < tourSize; j++)
                {
                    int k = rnd.Next(0, popSize);
                    randomSelected[j] = k;
                }

                //wybór najlepszego z losowo wybranych
                int bestIndex = 0;
                float bestScore = sums[randomSelected[0]];
                for(int l = 1; l < tourSize; l++)
                {
                    float tempScore = sums[randomSelected[l]];
                    if (tempScore < bestScore)
                    {
                        bestScore = tempScore;
                        bestIndex = l;
                    }
                }
                selectedPop[i] = pop[bestIndex];
            }

            Console.WriteLine("selectedPop:");
            Utils.PrintPopulation(selectedPop);

            return selectedPop;
        }
        
        public static void RussianSelect()
        {

        }

        /*
        public static (int[] gene, float score) GeneticAlgorithm(Vector2[] cities, int popSize, int generations, float crossProb, float mutProb, int tourSize)
        {


            return;
        }

        */


    }
}
