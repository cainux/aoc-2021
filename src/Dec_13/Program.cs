var lines = File.ReadLines("input.txt");
var dots = new List<Dot>();
var folds = new List<FoldInstruction>();

foreach (var line in lines)
{
    if (line.StartsWith("fold"))
    {
        var instruction = line.Remove(0, 11).Split('=');
        folds.Add(new FoldInstruction(instruction[0], Convert.ToInt32(instruction[1])));
    }
    else if (!string.IsNullOrEmpty(line))
    {
        var coords = line.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
        dots.Add(new Dot(coords[0], coords[1]));
    }
}

Part01(dots, folds.First()); // Test: 17
Console.WriteLine();
Part02(dots, folds); // Test: A square

static void Part01(List<Dot> dots, FoldInstruction instruction)
{
    var result = PerformFold(dots, instruction);
    Console.WriteLine($"Part 1 result: {result.Count}");
}

static void Part02(List<Dot> dots, IEnumerable<FoldInstruction> instructions)
{
    var result = instructions.Aggregate(dots, PerformFold);
    Console.WriteLine($"Part 2 result:");
    Plot(result);
}

static List<Dot> PerformFold(List<Dot> dots, FoldInstruction fold)
{
    var (foldAxis, foldPosition) = fold;
    var xAxis = foldAxis == "x";
    var notFolded = dots
        .Where(xAxis ? d => d.X < foldPosition : dot => dot.Y < foldPosition)
        .ToList();

    notFolded.AddRange(dots
        .Where(xAxis ? d => d.X > foldPosition : dot => dot.Y > foldPosition)
        .Select(d => xAxis ? new Dot(FoldValue(d.X, foldPosition), d.Y) : new Dot(d.X, FoldValue(d.Y, foldPosition))));

    return notFolded.Distinct().ToList();
}

static int FoldValue(int location, int position)
{
    return -(location - position) + position;
}

static void Plot(List<Dot> dots)
{
    var mx = dots.Max(dot => dot.X);
    var my = dots.Max(dot => dot.Y);
    var area = new bool[mx + 1, my + 1];

    foreach (var (x, y) in dots)
        area[x, y] = true;

    for (var y = 0; y <= my; y++)
    {
        for (var x = 0; x <= mx; x++)
        {
            Console.Write(area[x, y] ? '#' : ' ');
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

record Dot(int X, int Y);
record FoldInstruction(string axis, int position);