using System;
using System.IO;
using System.Linq;

namespace Dec_03
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            Part01(lines);
            Console.WriteLine();
            Part02(lines);
        }

        static void Part01(string[] lines)
        {
            var count = lines.Length;
            var width = lines[0].Length;
            var counters = new int[width];

            foreach (var line in lines)
                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i] == '1')
                        counters[i]++;
                }

            var gammaString = counters.Aggregate(string.Empty, (current, c) => current + (c > count / 2 ? '1' : '0'));
            var gamma = Convert.ToInt32(gammaString, 2);

            var epsilonString = counters.Aggregate(string.Empty, (current, c) => current + (c > count / 2 ? '0' : '1'));
            var epsilon = Convert.ToInt32(epsilonString, 2);

            Console.WriteLine("Part 01 Results");
            Console.WriteLine($"Gamma: {gamma}, Epsilon: {epsilon}");
            Console.WriteLine($"Result: {gamma * epsilon}");
        }

        static void Part02(string[] lines)
        {
            var oxygenRating = OxygenRating(0, lines);
            var co2ScrubberRating = CO2ScrubberRating(0, lines);

            Console.WriteLine("Part 02 Results");
            Console.WriteLine($"Oxygen Rating: {oxygenRating}, CO2 Scrubber Rating: {co2ScrubberRating}");
            Console.WriteLine($"Result: {oxygenRating * co2ScrubberRating}");
        }

        static int OxygenRating(int position, string[] lines)
        {
            var winningBit = lines
                .GroupBy(x => x[position])
                .Select(x => new {Bit = x.Key, Count = x.Count()})
                .OrderByDescending(x => x.Count)
                .ThenByDescending(x => x.Bit)
                .Select(x => x.Bit)
                .First();

            var survivingLines = lines
                .Where(x => x[position] == winningBit)
                .ToArray();

            return survivingLines.Length > 1 ? OxygenRating(position + 1, survivingLines) : Convert.ToInt32(survivingLines[0], 2);
        }

        static int CO2ScrubberRating(int position, string[] lines)
        {
            var winningBit = lines
                .GroupBy(x => x[position])
                .Select(x => new {Bit = x.Key, Count = x.Count()})
                .OrderBy(x => x.Count)
                .ThenBy(x => x.Bit)
                .Select(x => x.Bit)
                .First();

            var survivingLines = lines
                .Where(x => x[position] == winningBit)
                .ToArray();

            return survivingLines.Length > 1 ? CO2ScrubberRating(position + 1, survivingLines) : Convert.ToInt32(survivingLines[0], 2);
        }
    }
}