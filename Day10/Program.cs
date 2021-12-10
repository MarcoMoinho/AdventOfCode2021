// Day 10
var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\10.txt");

// list of opening pair for each closing one, also works as a list of closing pairs
Dictionary<char,char> pairs = new() {{')','('}, {']','['}, {'}','{'}, {'>','<'}};
// scores for Part 1 and 2
Dictionary<char, int> scores1 = new() {{')',3}, {']',57}, {'}',1197}, {'>',25137}};
Dictionary<char, int> scores2 = new() {{'(', 1}, {'[',2}, {'{',3}, {'<',4}};

var part1 = 0;
List<Int64> part2 = new ();
foreach (var input in inputs) {
    var chars = input.ToCharArray();
    Stack<char> open = new ();
    bool valid = false;
    for (int i = 0; i < chars.Length; i++) {
        valid = true;
        // if it's an opening bracket we push it to the stack
        if (!pairs.ContainsKey(chars[i])) { open.Push(chars[i]); continue; }
        // if not the correct closing bracket we calculate score for P1 and stop processing this line
        if (open.Peek() != pairs[chars[i]]) { part1 += scores1[chars[i]]; valid = false; break; }
        // bracket closed so we can remove it
        open.Pop();
    }
    if (!valid) continue;
    // calc part 2 score
    Int64 score = 0;
    while (open.Count > 0) score = score * 5 + scores2[open.Pop()];
    part2.Add(score);
}

Console.WriteLine("Part 1: {0}", part1);
part2.Sort();
Console.WriteLine("Part 2: {0}", part2[part2.Count/2]);