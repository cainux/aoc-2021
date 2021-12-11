var lines = File.ReadLines("input.txt");

Part01(lines); // Test: 26397

static void Part01(IEnumerable<string> lines)
{
    var illegalChars = new List<char>();
    const string opens = "([{<";
    var closings = new Dictionary<char, char>
    {
        {')', '('},
        {']', '['},
        {'}', '{'},
        {'>', '<'},
    };

    foreach (var line in lines)
    {
        var s = new Stack<char>();

        foreach (var c in line)
        {
            if (opens.Contains(c))
            {
                s.Push(c);
            }
            else
            {
                var popped = s.Pop();
                if (popped == closings[c]) continue;
                illegalChars.Add(c);
                break;
            }
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