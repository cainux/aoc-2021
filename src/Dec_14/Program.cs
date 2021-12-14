using System.Text;

var lines = File.ReadLines("input-test.txt").ToArray();
var template = lines[0];
var rules = lines[2..].Select(line => line.Split(" -> ")).ToDictionary(rule => rule[0], rule => rule[1][0]);

Run(template, rules); // Test: 1588 (10 steps), 2188189693529 (40 steps)

static void Run(string template, Dictionary<string, char> rules, int steps = 10)
{
    var result = template;

    for (var i = 1; i <= steps; i++)
    {
        result = Polymerize(result, rules);
    }

    var rankings = result.GroupBy(x => x).Select(x => new {x.Key, x.ToList().Count}).ToArray();
    var top = rankings.Max(x => x.Count);
    var bottom = rankings.Min(x => x.Count);

    Console.WriteLine($"Result after {steps} steps: {top - bottom}");
}

static string Polymerize(string template, Dictionary<string, char> rules)
{
    var sb = new StringBuilder();

    for (var i = 0; i < template.Length - 1; i++)
    {
        var pair = $"{template[i]}{template[i + 1]}";

        if (rules.ContainsKey(pair))
        {
            sb.Append($"{template[i]}{rules[pair]}");
        }
        else
        {
            sb.Append(pair);
        }

        if (i + 2 == template.Length)
        {
            sb.Append(template[i + 1]);
        }
    }

    return sb.ToString();
}