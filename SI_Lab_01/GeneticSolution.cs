using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SI_Lab_01
{
    class GeneticSolution
    {
        public static (int[] g1, int[] g2) Cross(int[] parent1, int[] parent2)
        {
            int length = parent1.Length;

            int[] g1 = new int[length];
            int[] g2 = new int[length];

            Random rnd = new Random();
            int i = rnd.Next(0, length);
            int j = rnd.Next(i, length);

            List<int> cut = parent1.ToList<int>().GetRange(i, j-i+1);
            List<int> rest = parent2.ToList<int>();
            rest = rest.Except(cut).ToList();
            rest.InsertRange(i,cut);
            g1 = rest.ToArray();

            List<int> cut2 = parent2.ToList<int>().GetRange(i, j - i + 1);
            List<int> rest2 = parent1.ToList<int>();
            rest2 = rest2.Except(cut2).ToList();
            rest2.InsertRange(i, cut2);
            g2 = rest2.ToArray();

            Console.WriteLine(i);
            Console.WriteLine(j);
            Utils.PrintGene(cut.ToArray());
            Utils.PrintGene(rest.ToArray());
            Utils.PrintGene(cut2.ToArray());
            Utils.PrintGene(rest2.ToArray());

            return (g1, g2);
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

        public static int[][] RussianSelect(int[][] pop, int epsilon, Vector2[] cities)
        {
            Console.WriteLine("inputPop");
            Utils.PrintPopulation(pop);


            float[] fitnessy = new float[pop.Length];

            for(int i = 0; i< pop.Length; i++)
            {
                fitnessy[i] = Utils.SumDistance(pop[i], cities);
            }

            fitnessy.ToList().ForEach(i => Console.WriteLine(i.ToString()));

            float maxDistance = fitnessy.ToList().Max();

            float[] helperArray = new float[pop.Length];

            helperArray[0] = maxDistance - fitnessy[0] + epsilon;

            for(int j = 1; j < pop.Length; j++)
            {
                helperArray[j] = helperArray[j - 1] + maxDistance - fitnessy[j] + epsilon;
            }


            Random rnd = new Random();

            int[][] newPop = new int[pop.Length][];

            for (int k = 0; k<pop.Length; k++)
            {
                var randomNum = rnd.Next(0, (int)helperArray.Last());


                /*
                if (randomNum < helperArray[0])
                {
                    newPop[k] = 0;
                }
                else
                {
                    for (int l = 0; l < pop.Length - 1; l++)
                    {
                        if (helperArray[l] <= randomNum && randomNum < helperArray[l + 1])
                        {
                            newPop[k] = l + 1;
                        }
                    }
                }

                */

                var indexOfGene = Array.IndexOf(helperArray, helperArray.First(x => x >= randomNum));

                newPop[k] = pop[indexOfGene];

            }

            Console.WriteLine("selectedPop:");
            Utils.PrintPopulation(newPop);

            return newPop;
        }

        /*
        public static (int[] gene, float score) GeneticAlgorithm(Vector2[] cities, int popSize, int generations, float crossProb, float mutProb, int tourSize)
        {


            return;
        }

        */


    }
}
