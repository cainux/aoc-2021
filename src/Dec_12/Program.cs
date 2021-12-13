var lines = File.ReadLines("input.txt");
var paths = new List<Path>();

foreach (var line in lines)
{
    var path = line.Split('-');
    paths.Add(new Path(path[0], path[1]));
}

Part01(paths); // Test: 226

static void Part01(List<Path> area)
{
    var result = new List<string>();

    var points = area.Where(x => x.C1 == "start" || x.C2 == "start").ToArray();

    foreach (var (c1, c2) in points)
    {
        var currentRoute = c1 == "start" ? $"{c1},{c2}" : $"{c2},{c1}";
        var paths = GetPaths(currentRoute, c1 == "start" ? c2 : c1, area);
        result.AddRange(paths);
    }

    foreach (var route in result)
    {
        Console.WriteLine(route);
    }

    Console.WriteLine();
    Console.WriteLine($"Part 1 result: {result.Count} paths");
}

static IEnumerable<string> GetPaths(string currentRoute, string location, List<Path> area)
{
    var result = new List<string>();

    if (location == "end")
    {
        result.Add(currentRoute);
        return result;
    }

    var visitedSmallCaves = currentRoute.Split(',')
        .Where(x => x.ToLower() == x)
        .ToArray();

    var connections = area
        // Paths which involve location
        .Where(x => x.C1 == location || x.C2 == location)
        // Flatten into a single array
        .SelectMany(x => new[] {x.C1, x.C2})
        // Remove current location from list
        .Where(x => x != location)
        // Remove visited small caves
        .Where(x => !visitedSmallCaves.Contains(x))
        .ToArray();

    if (connections.Length <= 0) return result;

    foreach (var connection in connections)
    {
        var paths = GetPaths($"{currentRoute},{connection}", connection, area);
        result.AddRange(paths);
    }

    return result;
}

internal record Path(string C1, string C2);