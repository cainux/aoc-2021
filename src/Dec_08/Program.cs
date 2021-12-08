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