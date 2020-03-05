using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SI_Lab_01
{

    class RandomSolution
    {
        public static (int[] gene, float score) RandomAlgorithm(Vector2[] cities, int ile)
        {
            var randPop = Utils.RandomPopulation(ile, cities.Length);
            var found = Utils.BestResultFromPopulation(randPop, cities);

            return (found.gene, found.score);

        }

    }
}
