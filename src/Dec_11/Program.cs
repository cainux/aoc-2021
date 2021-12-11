var lines = File.ReadLines("input.txt").ToArray();
var input = new Octopus[lines[0].Length, lines.Length];

for (var y = 0; y < lines.Length; y++)
for (var x = 0; x < lines[y].Length; x++)
    input[x, y] = new Octopus(x, y, int.Parse(lines[y][x].ToString()));

Part01(input); // Test: 1656
Console.WriteLine();

static void Part01(Octopus[,] area)
{
    Console.WriteLine("Before any steps:");
    Dump(area);

    var steps = 100;
    var result = 0;

    for (var s = 1; s <= steps; s++)
    {
        foreach (var octopus in area)
        {
            if (octopus.F) continue;
            if (++octopus.V > 9)
                Flash(octopus, area);
        }
        Console.WriteLine($"After step {s}:");
        Dump(area);
        result += CountAndReset(area);
    }

    Console.WriteLine($"Part 1 result: {result}");
}

static void Dump(Octopus[,] area)
{
    var (rows, cols) = GetDimensions(area);
    var foregroundColour = Console.ForegroundColor;
    var flashColour = ConsoleColor.Green;

    for (var y = 0; y <= rows; y++)
    {
        for (var x = 0; x <= cols; x++)
        {
            var current = area[x, y];
            if (current.F) Console.ForegroundColor = flashColour;
            Console.Write(current.V);
            Console.ForegroundColor = foregroundColour;
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

static int CountAndReset(Octopus[,] area)
{
    var flashCount = 0;

    foreach (var octopus in area)
    {
        if (!octopus.F) continue;
        octopus.F = false;
        flashCount++;
    }

    return flashCount;
}

static void Flash(Octopus octopus, Octopus[,] area)
{
    octopus.V = 0;
    octopus.F = true;

    var neighbours = GetNeighbours(octopus, area);

    if (neighbours.Count <= 0) return;

    foreach (var neighbour in neighbours.Where(n => !n.F))
    {
        neighbour.V++;

        if (neighbour.V > 9)
            Flash(neighbour, area);
    }
}

static List<Octopus> GetNeighbours(Octopus octopus, Octopus[,] area)
{
    var result = new List<Octopus>();
    var (rows, cols) = GetDimensions(area);
    var (x, y) = octopus.L;

    var startX = x > 0 ? x - 1 : x;
    var endX = x < cols ? x + 1 : x;
    var startY = y > 0 ? y - 1 : y;
    var endY = y < rows ? y + 1 : y;

    for (var yy = startY; yy <= endY; yy++)
    for (var xx = startX; xx <= endX; xx++)
        if (!(xx == x && yy == y))
            result.Add(area[xx, yy]);

    return result;
}

static (int rows, int cols) GetDimensions(Octopus[,] area)
{
    return (area.GetUpperBound(0), area.GetUpperBound(1));
}

internal record Location(int X, int Y);

internal class Octopus
{
    public Octopus(int x, int y, int v)
    {
        L = new Location(x, y);
        V = v;
        F = false;
    }

    public Location L { get; }
    public int V { get; set; }
    public bool F { get; set; }
    public override string ToString() => $"{L.X},{L.Y} - {V}:{F}";
}