using System;
using System.IO;

namespace Dec_01
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            Part01(lines);
            Part02(lines);
        }

        static void Part01(string[] lines)
        {
            var counter = 0;

            for (var i = 0; i < lines.Length - 1; i++)
            {
                var line = int.Parse(lines[i]);
                var nextLine = int.Parse(lines[i + 1]);

                if (nextLine > line)
                    counter++;
            }

            Console.WriteLine($"Part 01 result: {counter}");
        }

        static void Part02(string[] lines)
        {
            var counter = 0;

            for (var i = 0; i < lines.Length - 3; i++)
            {
                var batch1 = int.Parse(lines[i]) + int.Parse(lines[i + 1]) + int.Parse(lines[i + 2]);
                var batch2 = int.Parse(lines[i + 1]) + int.Parse(lines[i + 2]) + int.Parse(lines[i + 3]);

                if (batch2 > batch1)
                    counter++;
            }

            Console.WriteLine($"Part 02 result: {counter}");
        }
    }
}