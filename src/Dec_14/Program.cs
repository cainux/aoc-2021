var lines = File.ReadLines("input-test.txt").ToArray();
var template = lines[0];
var rules = lines[2..].Select(line => line.Split(" -> ")).ToDictionary(rule => rule[0], rule => rule[1][0]);

Run(template, rules, 40); // Test: 1588 (10 steps), 2188189693529 (40 steps)

static void Run(string template, IReadOnlyDictionary<string, char> rules, int steps)
{
    var pairs = new Dictionary<string, ulong>();
    var letters = new Dictionary<char, ulong>();

    foreach (var c in template)
        letters.AddCrement(c);

    for (var i = 0; i < template.Length - 1; i++)
        pairs.AddCrement($"{template[i]}{template[i + 1]}");

    for (var step = 1; step <= steps; step++)
    {
        var nextStep = new Dictionary<string, ulong>();

        foreach (var (pair, count) in pairs)
        {
            if (!rules.ContainsKey(pair)) continue;

            var insert = rules[pair];

            letters.AddCrement(insert, count);

            foreach (var newPair in new[] { $"{pair[0]}{insert}", $"{insert}{pair[1]}" })
                nextStep.AddCrement(newPair, count);
        }

        pairs = nextStep;
    }

    Console.WriteLine($"Result after {steps} steps: {letters.Max(x => x.Value) - letters.Min(x => x.Value)}");
}

internal static class Extensions
{
    public static void AddCrement<TKey>(this Dictionary<TKey, ulong> dictionary, TKey key, ulong increment = 1) where TKey : notnull
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] += increment;
        else
            dictionary.Add(key, increment);
    }
}