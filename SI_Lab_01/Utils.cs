using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SI_Lab_01
{
    class Utils
    {

        public static int[] RandomGene(int size)
        {
            int[] sortedGene = Enumerable.Range(0, size).ToArray();
            Random rnd = new Random();
            int[] randomGene = sortedGene.OrderBy(x => rnd.Next()).ToArray();

            return randomGene;
        }

        public static int[][] RandomPopulation(int popSize, int geneSize)
        {
            int[][] population = new int[popSize][];

            for (int i = 0; i < popSize; i++)
            {
                population[i] = RandomGene(geneSize);
            }

            return population;
        }

        public static (int[] gene, float score) BestResultFromPopulation(int[][] pop, Vector2[] cities)
        {
            List<float> results = new List<float>();

            float minn = float.MaxValue;
            int[] bestGene = new int[cities.Length];

            foreach (var gene in pop)
            {
                var dist = SumDistance(gene, cities);

                if (dist < minn)
                {
                    minn = dist;
                    bestGene = gene;
                }
                //results.Add(dist);
            }

            //var minDist = results.Min();

            return (bestGene, minn);
        }

        public static float SumDistance(int[] gene, Vector2[] cities)
        {
            float sum = 0;

            for (int i = 0; i < gene.Length - 1; i++)
            {
                sum += Vector2.Distance(cities[gene[i]], cities[gene[i + 1]]);
            }

            sum += Vector2.Distance(cities[gene[0]], cities[gene[cities.Length - 1]]);

            return sum;
        }

        public static void PrintGene(int[] gene)
        {
            foreach (var elem in gene)
            {
                Console.Write(elem + " ");
            }
            Console.WriteLine();
        }

    }
}
