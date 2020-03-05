using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SI_Lab_01
{
    class GreedySolution
    {
        public static (int[] gene, float score) GreedyAlgorithm(Vector2[] cities)
        {

            bool[] visited = new bool[cities.Length];

            Random rnd = new Random();
            int firstRandomCity = rnd.Next(cities.Length);

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

    }
}
