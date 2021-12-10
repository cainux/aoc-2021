var lines = File.ReadLines("input.txt").ToArray();
var input = new Location[lines[0].Length, lines.Length];

for (var y = 0; y < lines.Length; y++)
for (var x = 0; x < lines[y].Length; x++)
    input[x, y] = new Location(x, y, int.Parse(lines[y][x].ToString()));

Part01(input); // Test: 15
Console.WriteLine();
Part02(input); // Test: 1134

static void Part01(Location[,] area)
{
    var riskLevels = new List<int>();
    var rows = area.GetUpperBound(0);
    var cols = area.GetUpperBound(1);

    for (var y = 0; y <= cols; y++)
    for (var x = 0; x <= rows; x++)
    {
        var v = area[x, y].V;

        if (GetAdjacent(x, y, area, rows, cols).All(a => a.V > v))
            riskLevels.Add(v + 1);
    }

    Console.WriteLine($"Part 1 result: {riskLevels.Sum()}");
}

static List<Location> GetAdjacent(int x, int y, Location[,] area, int rows, int cols)
{
    var result = new List<Location>();

    if (x > 0) result.Add(area[x - 1, y]);
    if (x < rows) result.Add(area[x + 1, y]);
    if (y > 0) result.Add(area[x, y - 1]);
    if (y < cols) result.Add(area[x, y + 1]);

    return result;
}

static void Part02(Location[,] area)
{
    var lowPoints = new List<Location>();
    var rows = area.GetUpperBound(0);
    var cols = area.GetUpperBound(1);

    for (var y = 0; y <= cols; y++)
    for (var x = 0; x <= rows; x++)
    {
        var current = area[x, y];
        var adjacent = GetAdjacent(x, y, area, rows, cols);

        if (adjacent.All(a => a.V > current.V))
            lowPoints.Add(current);
    }

    var basinSizes = new List<int>();

    foreach (var l in lowPoints)
    {
        var basin = GetBasin(l, area, rows, cols);
        basinSizes.Add(basin.Count + 1);
    }

    var top3 = basinSizes.OrderByDescending(x => x).Take(3).ToArray();
    var result = top3.Aggregate(1, (current, next) => current * next);

    Console.WriteLine($"Part 2 result: {result}");
}

static List<Location> GetBasin(Location location, Location[,] area, int rows, int cols)
{
    var result = new List<Location>();

    var adjacents = GetAdjacent(location.X, location.Y, area, rows, cols)
        .Where(x => x.V > location.V && x.V < 9)
        .ToArray();

    if (adjacents.Length <= 0) return result;

    result.AddRange(adjacents);

    foreach (var adjacent in adjacents)
        result.AddRange(GetBasin(adjacent, area, rows, cols));

    return result.Distinct().ToList();
}

internal record Location(int X, int Y, int V);