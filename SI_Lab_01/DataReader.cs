using System;
using System.Globalization;
using System.IO;
using System.Numerics;

namespace SI_Lab_01
{
    class DataReader
    {
        public static Vector2[] ReadFile()
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
    }
}
