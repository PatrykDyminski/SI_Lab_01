using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;

namespace SI_Lab_01
{

    class Program
    {
        //Niepotrzebne Vector2.Distance robie to samo
        static float Distance(Vector2 v1, Vector2 v2)
        {
            var sum = Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2);
            return (float)Math.Sqrt(sum);
        }

        static int[] RandomGene(int size)
        {
            int[] sortedGene = Enumerable.Range(0, size).ToArray();
            Random rnd = new Random();
            int[] randomGene = sortedGene.OrderBy(x => rnd.Next()).ToArray();

            return randomGene;
        }

        static int[][] RandomPopulation(int popSize, int geneSize)
        {
            int[][] population = new int[popSize][];

            for (int i = 0; i < popSize; i++)
            {
                population[i] = RandomGene(geneSize);
            }

            return population;
        }

        static float BestResultFromPopulation(int[][] pop, Vector2[] cities)
        {
            List<float> results = new List<float>();

            foreach (var geneee in pop)
            {
                /*
                Console.WriteLine();
                Array.ForEach(geneee, Console.Write);
                */

                var dist = SumDistance(geneee, cities);
                //Console.Write(" " + SumDistance(geneee, cities));

                results.Add(dist);

            }

            //Console.WriteLine();

            var minDist = results.Min();

            return minDist;
        }

        static float SumDistance(int[] gene, Vector2[] cities)
        {
            float sum = 0;

            for (int i = 0; i < gene.Length - 1; i++)
            {
                sum += Vector2.Distance(cities[gene[i]], cities[gene[i+1]]);
            }

            sum += Vector2.Distance(cities[gene[0]], cities[gene[cities.Length-1]]);

            return sum;
        }

        static Vector2[] ReadFile()
        {
            const string f = "berlin11_modified.tsp";
            var lines = File.ReadAllLines(f);

            char[] delimiterChars = { ' ', '\t' };

            Vector2[] cities = new Vector2[lines.Length];

            foreach (var line in lines)
            {
                var nums = line.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                var v1 = float.Parse(nums[1], CultureInfo.InvariantCulture.NumberFormat);
                var v2 = float.Parse(nums[2], CultureInfo.InvariantCulture.NumberFormat);

                cities[Int32.Parse(nums[0]) - 1] = new Vector2(v1, v2);
            }

            return cities;
        }

        static void Main(string[] args)
        {
            Vector2[] cities = ReadFile();

            int ile = 10000;

            var randPop = RandomPopulation(ile, cities.Length);

            Console.WriteLine(randPop.Length);

            var minnn = BestResultFromPopulation(randPop, cities);
            Console.WriteLine(minnn);

        }
    }
}
