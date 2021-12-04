using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dec_04
{
    class Program
    {
        class Cell
        {
            public int value;
            public bool marked;
        }

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var draw = lines[0].Split(',').Select(x => Convert.ToInt32(x)).ToArray();
            var boards = new List<Cell[,]>();
            var board = new Cell[5, 5];
            var counter = 0;

            foreach (var line in lines.Skip(1).Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                var cells = line
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => new Cell { value = Convert.ToInt32(x), marked = false } )
                    .ToArray();

                for (var i = 0; i < 5; i++)
                {
                    board[counter, i] = cells[i];
                }

                counter++;

                if (counter < 5) continue;

                boards.Add(board);
                board = new Cell[5, 5];
                counter = 0;
            }

            Part01(draw, boards);
        }

        static void MarkMatches(int value, Cell[,] board)
        {
            for (var i = 0; i < 5; i++)
            for (var j = 0; j < 5; j++)
                if (board[i, j].value == value)
                    board[i, j].marked = true;
        }

        static bool HasWon(Cell[,] board)
        {
            for (var i = 0; i < 5; i++)
            {
                var row = Enumerable.Range(0, 5).Select(x => board[i, x]).Where(x => x.marked).ToArray();
                var col = Enumerable.Range(0, 5).Select(x => board[x, i]).Where(x => x.marked).ToArray();
                if (row.Length != 5 && col.Length != 5) continue;
                return true;
            }

            return false;
        }

        static IEnumerable<int> GetUnmarked(Cell[,] board)
        {
            for (var i = 0; i < 5; i++)
            for (var j = 0; j < 5; j++)
            {
                if (!board[i,j].marked)
                    yield return board[i,j].value;
            }
        }

        static void Part01(int[] draw, IList<Cell[,]> boards)
        {
            foreach (var number in draw)
            foreach (var board in boards)
            {
                MarkMatches(number, board);
                if (!HasWon(board)) continue;
                Console.WriteLine($"Number drawn: {number}, Final score: {number * GetUnmarked(board).Sum()}");
                return;
            }
        }
    }
}