using System;
using System.IO;
using System.Linq;

namespace Dec_07
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var crabs = input.Split(',').Select(x => Convert.ToInt32(x)).ToArray();

            Part01(crabs); // (test: 37)
            Console.WriteLine();
            Part02(crabs); // (test: 168)
        }

        static void Part01(int[] crabs)
        {
            var result = Enumerable.Range(crabs.Min(), crabs.Max())
                .Select(x => crabs.Sum(crab => Math.Abs(crab - x)))
                .Min();

            Console.WriteLine($"Part 1 result: {result}");
        }

        static void Part02(int[] crabs)
        {
            var result = Enumerable.Range(crabs.Min(), crabs.Max())
                .Select(x => crabs.Sum(crab => Enumerable.Range(1, Math.Abs(crab - x)).Sum()))
                .Min();

            Console.WriteLine($"Part 2 result: {result}");
        }
    }
}