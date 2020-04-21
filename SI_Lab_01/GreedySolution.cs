using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SI_Lab_01
{
    class GreedySolution
    {
        public static (int[] gene, float score) GreedyAlgorithm(Vector2[] cities, int startCity)
        {

            bool[] visited = new bool[cities.Length];

            //Random rnd = new Random();
            int firstRandomCity = startCity;

            List<int> gene = new List<int>();

            visited[firstRandomCity] = true;
            gene.Add(firstRandomCity);

            int numVisited = 1;
            int prevCity = firstRandomCity;

            while (numVisited < cities.Length)
            {
                float minCost = float.MaxValue;
                int currIndex = -1;

                for (int i = 0; i < cities.Length; i++)
                {
                    if (!visited[i])
                    {
                        float cost = Vector2.Distance(cities[prevCity], cities[i]);
                        if (cost < minCost)
                        {
                            minCost = cost;
                            currIndex = i;
                        }
                    }
                }

                if (minCost == float.MaxValue)
                {
                    break;
                }
                else
                {
                    prevCity = currIndex;
                    visited[currIndex] = true;
                    gene.Add(currIndex);
                    numVisited += 1;
                }

            }

            var distance = Utils.SumDistance(gene.ToArray(), cities);

            return (gene.ToArray(), distance);
        }


        public static (float best, float worst, float avg, float std) GreedyAlgorithmAll(Vector2[] cities)
        {

            float[] results = new float[cities.Length];

            for(int i = 0; i< cities.Length; i++)
            {
                results[i] = GreedyAlgorithm(cities, i).score;
            }

            float best = results.ToList().Min();
            float worst = results.ToList().Max();
            float avg = results.ToList().Average();
            float std = Utils.StdDev(results.ToList());

            return (best, worst, avg, std);

        }

    }
}
