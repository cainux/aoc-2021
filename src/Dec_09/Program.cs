var lines = File.ReadLines("input.txt").ToArray();
var input = new int[lines.Length, lines[0].Length];

for (var r = 0; r < lines.Length; r++)
{
    var line = lines[r];

    for (var c = 0; c < line.Length; c++)
    {
        input[r, c] = int.Parse(line[c].ToString());
    }
}

Part01(input); // Test: 15

static void Part01(int[,] area)
{
    var lowPoints = new List<int>();
    var rows = area.GetUpperBound(0);
    var cols = area.GetUpperBound(1);

    for (var r = 0; r <= rows; r++)
    for (var c = 0; c <= cols; c++)
    {
        var current = area[r, c];
        var adjacent = new List<int>();

        if (r > 0) adjacent.Add(area[r - 1, c]);
        if (r < rows) adjacent.Add(area[r + 1, c]);
        if (c > 0) adjacent.Add(area[r, c - 1]);
        if (c < cols) adjacent.Add(area[r, c + 1]);

        if (adjacent.All(a => a > current))
            lowPoints.Add(current);
    }

    Console.WriteLine($"Part 1 result: {lowPoints.Sum(x => x + 1)}");
}