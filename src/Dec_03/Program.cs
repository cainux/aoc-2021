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
    }
}