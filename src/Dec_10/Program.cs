using System.Numerics;

var lines = File.ReadLines("input.txt");

Part01(lines); // Test: 26397
Console.WriteLine();
Part02(lines); // Test: 288957

static void Part01(IEnumerable<string> lines)
{
    var illegalChars = new List<char>();
    const string opens = "([{<";
    var closes = new Dictionary<char, char>
    {
        {')', '('},
        {']', '['},
        {'}', '{'},
        {'>', '<'},
    };

    foreach (var line in lines)
    {
        var stack = new Stack<char>();

        foreach (var c in line)
            if (opens.Contains(c))
                stack.Push(c);
            else
            {
                var popped = stack.Pop();
                if (popped == closes[c]) continue;
                illegalChars.Add(c);
                break;
            }
    }

    var score = illegalChars.Sum(c => c switch
    {
        ')' => 3,
        ']' => 57,
        '}' => 1197,
        '>' => 25137
    });

    Console.WriteLine($"Part 1 result: {score}");
}

static void Part02(IEnumerable<string> lines)
{
    var lineCompletions = new List<string>();
    var opens = new Dictionary<char, char>
    {
        {'(', ')'},
        {'[', ']'},
        {'{', '}'},
        {'<', '>'}
    };
    var closes = new Dictionary<char, char>
    {
        {')', '('},
        {']', '['},
        {'}', '{'},
        {'>', '<'}
    };

    foreach (var line in lines)
    {
        var stack = new Stack<char>();
        var discard = false;

        foreach (var c in line)
        {
            if (opens.ContainsKey(c))
                stack.Push(c);
            else
            {
                var popped = stack.Pop();
                if (popped == closes[c]) continue;
                discard = true;
                break;
            }
        }

        if (discard) continue;

        var completion = string.Empty;

        while (stack.TryPop(out var c))
            completion += opens[c];

        lineCompletions.Add(completion);
    }

    var lineScores = new List<BigInteger>();

    foreach (var completion in lineCompletions)
    {
        BigInteger lineScore = 0;

        foreach (var c in completion)
        {
            lineScore *= 5;
            lineScore += c switch
            {
                ')' => 1,
                ']' => 2,
                '}' => 3,
                '>' => 4
            };
        }

        lineScores.Add(lineScore);
    }

    lineScores.Sort(); // 🤷‍

    var score = lineScores[lineScores.Count / 2];

    Console.WriteLine($"Part 2 result: {score}");
}