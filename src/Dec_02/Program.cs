using System;
using System.IO;

namespace Dec_02
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
            var horizontalPosition = 0;
            var depth = 0;

            foreach (var line in lines)
            {
                var parts = line.Split(" ");
                var command = parts[0];
                var value = int.Parse(parts[1]);

                switch (command)
                {
                    case "forward":
                        horizontalPosition += value;
                        break;

                    case "up":
                        depth -= value;
                        break;

                    case "down":
                        depth += value;
                        break;

                    default:
                        throw new Exception($"Unrecognised Command: {command}");
                }
            }

            Console.WriteLine("Part 01 Results");
            Console.WriteLine($"Horizontal Position: {horizontalPosition}, Depth: {depth}");
            Console.WriteLine($"Result: {horizontalPosition * depth}");
        }

        static void Part02(string[] lines)
        {
            var horizontalPosition = 0;
            var depth = 0;
            var aim = 0;

            foreach (var line in lines)
            {
                var parts = line.Split(" ");
                var command = parts[0];
                var value = int.Parse(parts[1]);

                switch (command)
                {
                    case "forward":
                        horizontalPosition += value;
                        depth += aim * value;
                        break;

                    case "up":
                        aim -= value;
                        break;

                    case "down":
                        aim += value;
                        break;

                    default:
                        throw new Exception($"Unrecognised Command: {command}");
                }
            }

            Console.WriteLine("Part 02 Results");
            Console.WriteLine($"Horizontal Position: {horizontalPosition}, Depth: {depth}");
            Console.WriteLine($"Result: {horizontalPosition * depth}");
        }
    }
}