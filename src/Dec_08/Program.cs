var lines = File.ReadLines("input.txt");
var input = new List<(string[], string[])>();

foreach (var line in lines)
{
    var entry = line.Split('|');
    var signal = entry[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var output = entry[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    input.Add((signal, output));
}

Part01(input); // Test: 26
Console.WriteLine();
Part02(input); // Test: 61229

void Part01(List<(string[], string[])> input)
{
    var result = 0;

    foreach (var (signal, output) in input)
    {
        var numbers = new Dictionary<string, int>();

        foreach (var pattern in signal)
        {
            var ordered = string.Join(string.Empty, pattern.OrderBy(x => x));

            _ = pattern.Length switch
            {
                2 => numbers[ordered] = 1,
                4 => numbers[ordered] = 4,
                3 => numbers[ordered] = 7,
                7 => numbers[ordered] = 8,
                _ => 0
            };
        }

        foreach (var pattern in output)
        {
            var ordered = string.Join(string.Empty, pattern.OrderBy(x => x));

            if (numbers.ContainsKey(ordered))
                result++;
        }
    }

    Console.WriteLine($"Part 1 result: {result}");
}

void Part02(List<(string[], string[])> input)
{
    var result = 0;

    foreach (var (signal, output) in input)
    {
        var orderedPatterns = signal
            .Select(x => string.Join(string.Empty, x.OrderBy(y => y)))
            .ToArray();

        // 🤮
        var one = orderedPatterns.Single(x => x.Length == 2);
        var four = orderedPatterns.Single(x => x.Length == 4);
        var seven = orderedPatterns.Single(x => x.Length == 3);
        var eight = orderedPatterns.Single(x => x.Length == 7);
        var top = seven.Minus(one);
        var nine = orderedPatterns.Single(x => x.Length == 6 && x.Has(four.Combine(top)));
        var bottom = nine.Minus(four.Combine(top));
        var bottomLeft = eight.Minus(nine);
        var two = orderedPatterns.Single(x => x.Length == 5 && x.Combine(bottomLeft) == x);
        var topLeft = nine.Minus(two).Minus(one).Minus(bottom);
        var zero = seven.Combine(topLeft).Combine(bottomLeft).Combine(bottom);
        var bottomRight = zero.Minus(two).Minus(topLeft);
        var topRight = one.Minus(bottomRight);
        var three = eight.Minus(topLeft).Minus(bottomLeft);
        var six = eight.Minus(topRight);
        var five = six.Minus(bottomLeft);

        var decoder = new Dictionary<string, char>
        {
            { zero, '0' },
            { one, '1' },
            { two, '2' },
            { three, '3' },
            { four, '4' },
            { five, '5' },
            { six, '6' },
            { seven, '7' },
            { eight, '8' },
            { nine, '9' }
        };

        var outputValue = output
            .Select(x => string.Join(string.Empty, x.OrderBy(y => y)))
            .Aggregate(string.Empty, (current, o) => current + decoder[o]);

        result += Convert.ToInt32(outputValue);
    }

    Console.WriteLine($"Part 2 result: {result}");
}

static class Extensions
{
    public static string Combine(this string x, string y)
    {
        return string.Join(string.Empty, (x + y).OrderBy(c => c).Distinct());
    }

    public static string Minus(this string x, string y)
    {
        return y.Aggregate(x, (current, c) => current.Replace(c.ToString(), ""));
    }

    public static bool Has(this string x, string y)
    {
        return y.All(x.Contains);
    }
}