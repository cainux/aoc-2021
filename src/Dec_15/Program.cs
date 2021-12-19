var lines = File.ReadLines("input.txt").ToArray();

var my = lines.Length;
var mx = lines[0].Length;

var cavern = new Chiton[mx, my];

for (var y = 0; y < lines.Length; y++)
for (var x = 0; x < lines[y].Length; x++)
    cavern[x, y] = new Chiton(x, y, int.Parse(lines[y][x].ToString()));

var actualCavern = Expand(cavern, 5);

Run(actualCavern); // Test pt1: 40 pt2: 315

static void Run(Chiton[,] cavern)
{
    var (mx, my) = GetBounds(cavern);
    var goal = cavern[mx, my];

    var start = new Path(cavern[0, 0].L, 0, 0);

    var open = new Dictionary<Location, Path> {[start.L] = start};
    var closed = new Dictionary<Location, Path>();

    while (open.Count > 0)
    {
        var q = open.MinBy(x => x.Value.F).Value;
        var successors = GetNeighbours(q.L, cavern);

        foreach (var successor in successors)
        {
            if (successor == goal)
            {
                // Console.WriteLine("Goal reached:");
                q.Route.Add(successor);
                // Draw(cavern, q);
                Console.WriteLine($"Result: {q.Route.Sum(x => x.Risk)}");
                return;
            }

            var g = q.G + successor.Risk;
            var h = H(successor, goal);
            var f = g + h;

            if (open.ContainsKey(successor.L) && open[successor.L].F <= f) continue;
            if (closed.ContainsKey(successor.L) && closed[successor.L].F <= f) continue;

            var p = new Path(successor.L, f, g);

            p.Route.AddRange(q.Route);
            p.Route.Add(successor);

            open[p.L] = p;
        }

        open.Remove(q.L);
        closed[q.L] = q;
    }
}

static Chiton[,] Expand(Chiton[,] cavern, int repeat)
{
    var (mx, my) = GetBounds(cavern);
    var sizeX = mx + 1;
    var sizeY = my + 1;
    var newCavern = new Chiton[sizeX * repeat, sizeY * repeat];

    for (var y = 0; y <= my; y++)
    for (var x = 0; x <= mx; x++)
    {
        newCavern[x, y] = new Chiton(x, y, cavern[x, y].Risk);
    }

    for (var y = 0; y < sizeY * repeat; y++)
    for (var x = 0; x < sizeX * repeat; x++)
    {
        if (x <= mx && y <= my) continue;

        var newRisk = 0;

        if (x > mx)
            newRisk = newCavern[x - sizeX, y].Risk;
        else if (y > my)
            newRisk = newCavern[x, y - sizeY].Risk;

        newRisk++;

        if (newRisk > 9)
            newRisk = 1;

        newCavern[x, y] = new Chiton(x, y, newRisk);
    }

    return newCavern;
}

static List<Chiton> GetNeighbours(Location location, Chiton[,] cavern)
{
    var result = new List<Chiton>();
    var (rows, cols) = GetBounds(cavern);
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

static (int mx, int my) GetBounds<T>(T[,] c) => (c.GetUpperBound(0), c.GetUpperBound(1));

static int H(Chiton c, Chiton g) => ManhattanDistance(c, g);

static int ManhattanDistance(Chiton current, Chiton goal) => Math.Abs(current.L.X - goal.L.X) + Math.Abs(current.L.Y - goal.L.Y);

static void Draw(Chiton[,] cavern, Path path)
{
    var (mx, my) = GetBounds(cavern);

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