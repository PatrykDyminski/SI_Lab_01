using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SI_Lab_01
{

    class RandomSolution
    {
        public static (float best, float worst, float avg, float std, int[] gene) RandomAlgorithm(Vector2[] cities, int ile)
        {
            var randPop = Utils.RandomPopulation(ile, cities.Length);
            var found = Utils.BestResultFromPopulation(randPop, cities);

            float[] results = new float[ile];

            for (int i = 0; i < ile; i++)
            {
                results[i] = Utils.SumDistance(randPop[i], cities);
            }

            float best = results.ToList().Min();
            float worst = results.ToList().Max();
            float avg = results.ToList().Average();
            float std = Utils.StdDev(results.ToList());

            return (best, worst, avg, std, found.gene);
        }

        public static (float best, float worst, float avg, float std) RandomAlgorithm2(Vector2[] cities, int ile)
        {
            float[] results = new float[ile];

            for (int i = 0; i < ile; i++)
            {
                var randomGene = Utils.RandomGene(cities.Length);
                results[i] = Utils.SumDistance(randomGene, cities);
            }

            float best = results.ToList().Min();
            float worst = results.ToList().Max();
            float avg = results.ToList().Average();
            float std = Utils.StdDev(results.ToList());

            return (best, worst, avg, std);
        }


    }
}
