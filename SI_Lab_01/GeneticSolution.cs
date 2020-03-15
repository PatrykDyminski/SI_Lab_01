using System;
using System.Collections.Generic;
using System.IO;
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


            /*
            Console.WriteLine(i);
            Console.WriteLine(j);
            Utils.PrintGene(cut.ToArray());
            Utils.PrintGene(rest.ToArray());
            Utils.PrintGene(cut2.ToArray());
            Utils.PrintGene(rest2.ToArray());
            */


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

        public static int[] Mutate2(int[] genotype)
        {
            Random rnd = new Random();
            int i = rnd.Next(0, genotype.Length);
            int j = rnd.Next(0, genotype.Length);

            if (i > j)
            {
                int temp = i;
                i = j;
                j = temp;
            }

            Array.Reverse(genotype, i, j-i+1);

            return genotype;
        }

        public static int[][] TourSelect(int[][] pop, int tourSize, Vector2[] cities)
        {
            //Console.WriteLine("inputPop");
            //Utils.PrintPopulation(pop);

            var popSize = pop.Length;

            float[] sums = new float[popSize];

            //liczenie wszytkich sum
            for (int i = 0; i < popSize; i++)
            {
                sums[i] = Utils.SumDistance(pop[i], cities);
            }

            //sums.ToList().ForEach(i => Console.WriteLine(i.ToString()));

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
                        //TU COS ZLE MOZE BYC
                        bestIndex = randomSelected[l];
                    }
                }
                selectedPop[i] = pop[bestIndex];
            }

            //Console.WriteLine("selectedPop:");
            //Utils.PrintPopulation(selectedPop);

            return selectedPop;
        }

        public static int[][] RussianSelect(int[][] pop, int epsilon, Vector2[] cities)
        {
            //Console.WriteLine("inputPop");
            //Utils.PrintPopulation(pop);

            float[] fitnessy = new float[pop.Length];

            for(int i = 0; i< pop.Length; i++)
            {
                fitnessy[i] = Utils.SumDistance(pop[i], cities);
            }

            //fitnessy.ToList().ForEach(i => Console.WriteLine(i.ToString()));

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

            return newPop;
        }

        
        public static (int[] gene, float score) GeneticAlgorithm(Vector2[] cities, int popSize, int generations, float crossProb, float mutProb, int tourSize)
        {

            var csv = new StringBuilder();

            var prevPop = Utils.RandomPopulation(popSize, cities.Length);

            float bestScore = float.MaxValue;
            int[] bestGene = new int[cities.Length];

            Random rnd = new Random();

            for (int i = 0; i < generations; i++)
            {
                var newPop = TourSelect(prevPop, tourSize, cities);
                int[][] tempPop = new int[popSize][];

                for (int j = 0; j < newPop.Length; j += 2)
                {
                    if (rnd.NextDouble() < crossProb)
                    {
                        var dzieci = Cross(newPop[j], newPop[j + 1]);
                        tempPop[j] = dzieci.g1;
                        tempPop[j + 1] = dzieci.g2;
                    }
                    else
                    {
                        tempPop[j] = newPop[j];
                        tempPop[j + 1] = newPop[j + 1];
                    }
                }

                for (int k = 0; k < newPop.Length; k++)
                {
                    if (rnd.NextDouble() < mutProb)
                    {
                        tempPop[k] = Mutate2(tempPop[k]);
                    }
                }

                //Utils.PrintPopulation(tempPop);
                prevPop = tempPop.Select(a => a.ToArray()).ToArray();

                var bestInPop = Utils.BestResultFromPopulation(prevPop,cities);
                var worstInPop = Utils.WorstResultFromPopulation(prevPop, cities);
                var avgInPop = Utils.AvgResultFromPopulation(prevPop, cities);

                var newLine = string.Format("{0};{1};{2};{3};;;", i, bestInPop.score, avgInPop, worstInPop.score);
                csv.AppendLine(newLine);

                if (bestInPop.score<bestScore)
                {
                    bestScore = bestInPop.score;
                    bestGene = bestInPop.gene;
                }

            }

            File.WriteAllText("results.csv", csv.ToString());

            return (bestGene, bestScore);
        }


    }
}
