using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dec_05
{
    record P(int x, int y);
    record L(P start, P end);

    class Program
    {
        static void Main(string[] args)
        {
            var inputLines = File.ReadAllLines("input.txt");
            var lines = new List<L>();

            foreach (var inputLine in inputLines)
            {
                var coordinates = inputLine.Split(" -> ");
                var start = coordinates[0].Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                var end = coordinates[1].Split(',').Select(x => Convert.ToInt32(x)).ToArray();

                lines.Add(new L(new P(start[0], start[1]), new P(end[0], end[1])));
            }

            var w = lines.Max(l => Math.Max(l.start.x, l.end.x)) + 1;
            var h = lines.Max(l => Math.Max(l.start.y, l.end.y)) + 1;

            Part01(lines, new int[w, h]);
            Part02(lines, new int[w, h]);
        }

        static void Plot(IEnumerable<L> lines, int[,] grid, int part)
        {
            foreach (var ((x1, y1), (x2, y2)) in lines)
            {
                var xd = x2 < x1 ? -1 : 1;
                var yd = y2 < y1 ? -1 : 1;

                if (x1 == x2 || y1 == y2)
                {
                    for (var x = x1; x != x2 + xd; x += xd)
                    for (var y = y1; y != y2 + yd; y += yd)
                        grid[x, y]++;
                }
                else if (part == 2)
                {
                    var y = y1;

                    for (var x = x1; x != x2 + xd; x += xd)
                    {
                        grid[x, y]++;
                        y += yd;
                    }
                }
            }
        }

        static int CountOverlaps(int[,] grid)
        {
            var overlaps = 0;

            for (var x = 0; x <= grid.GetUpperBound(0); x++)
            for (var y = 0; y <= grid.GetUpperBound(1); y++)
                if (grid[x, y] > 1)
                    overlaps++;

            return overlaps;
        }

        static void Part01(IEnumerable<L> lines, int[,] grid)
        {
            Plot(lines, grid, 1);
            Console.WriteLine($"Part 1: Overlapping points: {CountOverlaps(grid)}");
        }

        static void Part02(IEnumerable<L> lines, int[,] grid)
        {
            Plot(lines, grid, 2);
            Console.WriteLine($"Part 2: Overlapping points: {CountOverlaps(grid)}");
        }
    }
}