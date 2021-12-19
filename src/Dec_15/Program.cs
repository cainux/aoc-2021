var lines = File.ReadLines("input.txt").ToArray();

var my = lines.Length;
var mx = lines[0].Length;

var cavern = new Chiton[mx, my];

for (var y = 0; y < lines.Length; y++)
for (var x = 0; x < lines[y].Length; x++)
    cavern[x, y] = new Chiton(x, y, int.Parse(lines[y][x].ToString()));

Run(cavern); // Test: 40

static void Run(Chiton[,] cavern)
{
    var (mx, my) = GetDimensions(cavern);
    var goal = cavern[mx, my];

    var start = new Path(cavern[0, 0].L, 0, 0);

    var open = new List<Path>(new[] {start});
    var closed = new List<Path>();

    while (open.Count > 0)
    {
        var q = open.MinBy(x => x.F)!;
        var successors = GetNeighbours(q.L, cavern);

        foreach (var successor in successors)
        {
            if (successor == goal)
            {
                Console.WriteLine("Goal reached:");
                q.Route.Add(successor);
                Draw(cavern, q);
                Console.WriteLine($"Result: {q.Route.Sum(x => x.Risk)}");
                return;
            }

            var g = q.G + successor.Risk;
            var h = H(successor, goal);
            var f = g + h;

            if (open.Any(x => x.L == successor.L && x.F <= f)) continue;
            if (closed.Any(x => x.L == successor.L && x.F <= f)) continue;

            var p = new Path(successor.L, f, g);

            p.Route.AddRange(q.Route);
            p.Route.Add(successor);

            open.Add(p);
        }

        open.Remove(q);
        closed.Add(q);
    }
}

static List<Chiton> GetNeighbours(Location location, Chiton[,] cavern)
{
    var result = new List<Chiton>();
    var (rows, cols) = GetDimensions(cavern);
    var (x, y) = location;

    var startX = x > 0 ? x - 1 : x;
    var endX = x < cols ? x + 1 : x;
    var startY = y > 0 ? y - 1 : y;
    var endY = y < rows ? y + 1 : y;

    for (var yy = startY; yy <= endY; yy++)
    for (var xx = startX; xx <= endX; xx++)
        if (!(xx == x && yy == y) && (xx == x || yy == y))
            result.Add(cavern[xx, yy]);

    return result;
}

static (int mx, int my) GetDimensions<T>(T[,] c) => (c.GetUpperBound(0), c.GetUpperBound(1));

static int H(Chiton c, Chiton g) => ManhattanDistance(c, g);

static int ManhattanDistance(Chiton current, Chiton goal) => Math.Abs(current.L.X - goal.L.X) + Math.Abs(current.L.Y - goal.L.Y);

static void Draw(Chiton[,] cavern, Path path)
{
    var (mx, my) = GetDimensions(cavern);

    var originalColour = Console.ForegroundColor;

    for (var y = 0; y <= my; y++)
    {
        for (var x = 0; x <= mx; x++)
        {
            var l = new Location(x, y);
            var colour = path.Route.Any(x => x.L == l) || x == 0 && y == 0 ? ConsoleColor.Green : originalColour;

            Console.ForegroundColor = colour;
            Console.Write(cavern[x, y].Risk);
            Console.ForegroundColor = originalColour;
        }

        Console.WriteLine();
    }
}

internal record Location(int X, int Y);

internal class Chiton
{
    public Chiton(int x, int y, int risk)
    {
        L = new Location(x, y);
        Risk = risk;
    }

    public Location L { get; }
    public int Risk { get; }
}

internal class Path
{
    public Path(Location l, int f, int g)
    {
        L = l;
        F = f;
        G = g;
        Route = new List<Chiton>();
    }

    public List<Chiton> Route { get; }
    public Location L { get; }
    public int G { get; }
    public int F { get; }
}