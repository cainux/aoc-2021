using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dec_06
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var fishes = input.Split(',').Select(x => Convert.ToInt32(x)).ToArray();

            Run(fishes, 80);
        }

        static void Run(int[] fishes, int days)
        {
            for (var i = 0; i < days; i++)
            {
                fishes = Tick(fishes);
            }

            Console.WriteLine($"After {days} Days: {fishes.Length}");
        }

        static int[] Tick(int[] fishes)
        {
            var babies = new List<int>();

            for (var i = 0; i < fishes.Length; i++)
            {
                fishes[i]--;

                if (fishes[i] < 0)
                {
                    babies.Add(8);
                    fishes[i] = 6;
                }
            }

            var result = fishes.ToList();

            if (babies.Any())
            {
                result.AddRange(babies);
            }

            return result.ToArray();
        }
    }
}