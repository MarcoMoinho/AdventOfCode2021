// Day 14
var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\14.txt");

void Process(ref Dictionary<string, Int64> pairs, ref Dictionary<string, string> rules) {
    var tmp = new Dictionary<string, Int64>();
    foreach (var pair in pairs.Keys) {
        string[] newpairs = new string[2] { pair.Substring(0, 1) + rules[pair], rules[pair] + pair.Substring(1, 1) };
        foreach (var s in newpairs) {
            if (!tmp.ContainsKey(s)) tmp.Add(s, 0);
            tmp[s] += pairs[pair];
        }
    }
    pairs = new Dictionary<string, Int64>(tmp);
}

Int64 GetResult(ref Dictionary<string, Int64> pairs) {
    Dictionary<string, Int64> count = new();
    foreach (var pair in pairs.Keys) {
        if (!count.ContainsKey(pair.Substring(0, 1))) count.Add(pair.Substring(0, 1), 0);
        count[pair.Substring(0, 1)] += pairs[pair];
    }
    return (count.Max(x => x.Value) + 1 - count.Min(x => x.Value));
}

// parse the input file
string template = inputs[0];
Dictionary<string, string> rules = new();
foreach (var input in inputs) {
    if (input.IndexOf(" -> ") < 0) continue;
    rules.Add(input.Split(" -> ")[0], input.Split(" -> ")[1]);
}

// add the initial pairs
Dictionary<string, Int64> pairs = new();
for (var i = 0; i < template.Length - 1; i++) {
    string pair = template.Substring(i, 2);
    if (!pairs.ContainsKey(pair)) pairs.Add(pair,0);
    pairs[pair]++;
}

// execute 10 steps for P1
for (var i = 0; i < 10; i++) Process(ref pairs,ref rules);
Console.WriteLine("Part 1: {0}", GetResult(ref pairs));

// execute 30 more for P2
for (var i = 0; i < 30; i++) Process(ref pairs,ref rules);
Console.WriteLine("Part 2: {0}", GetResult(ref pairs));
