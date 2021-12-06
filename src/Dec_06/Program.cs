using System;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Dec_06
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var initial = input.Split(',').Select(x => Convert.ToUInt16(x)).ToArray();

            var fishCounters = new BigInteger[9];

            for (var i = 0; i < fishCounters.Length; i++)
            {
                fishCounters[i] = initial.Count(x => x == i);
            }

            for (var d = 1; d <= 256; d++)
            {
                var zeroes = fishCounters[0];

                for (var i = 0; i < fishCounters.Length - 1; i++)
                {
                    fishCounters[i] = fishCounters[i + 1];
                }

                fishCounters[8] = 0;

                if (zeroes > 0)
                {
                    fishCounters[6] += zeroes;
                    fishCounters[8] = zeroes;
                }
            }

            var result = fishCounters.Aggregate(BigInteger.Zero, (current, fishCounter) => current + fishCounter);

            Console.WriteLine($"Result: {result}");
        }
    }
}