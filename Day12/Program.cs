// Day 12
var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\12.txt");

// recursively find all possible paths
static void GetAllPaths(ref Dictionary<string, List<string>> nodes, string start, string dest,ref List<string> paths, List<string> path, bool part2) {
    if (path.Contains(dest)) return;
    path.Add(start);
    // go through all adjacent nodes
    foreach (var node in nodes[start]) {
        // start and end can only be travelled once
        if (node == "start" || node == "end") if (path.Contains(node)) continue;
        if (part2) {
            // in part 2 we can travel twice to lowercase dungeons
            if (node.ToLower() == node)
                if (path.FindAll(x => x == node).Count() >= 2) continue;
            // but only on one of them, the remaining is still limited to 1 visit
            Dictionary<string, int> count = new();
            foreach (var p in path) {
                if (p.ToUpper() == p) continue;
                if (!count.ContainsKey(p)) count.Add(p, 1); else count[p]++;
            }
            if (count.Count(x => x.Value >= 2) > 1) continue;
        } else {
            // on part 1 lowercase can only be travelled once
            if (node.ToLower() == node && path.Contains(node)) continue;
        }
        if (node == dest) {
            path.Add(dest);
            paths.Add(String.Join(",", path.ToArray()));
            path.Remove(dest);
            continue;
        }
        var tmp = new List<string>(path);
        GetAllPaths(ref nodes, node, dest, ref paths, tmp, part2);
    }
}

// read the inputs
Dictionary<string, List<string>> nodes = new();
foreach (var input in inputs) {
    string[] tmp;
    tmp = new string[2] { input.Split('-')[0], input.Split('-')[1] };   // add one way
    if (!nodes.ContainsKey(tmp[0])) nodes.Add(tmp[0], new());
    if (!nodes[tmp[0]].Contains(tmp[1])) nodes[tmp[0]].Add(tmp[1]);
    tmp = new string[2] { input.Split('-')[1], input.Split('-')[0] };   // add reverse
    if (!nodes.ContainsKey(tmp[0])) nodes.Add(tmp[0], new());
    if (!nodes[tmp[0]].Contains(tmp[1])) nodes[tmp[0]].Add(tmp[1]);
}

// Part 1
List<string> paths = new();
GetAllPaths(ref nodes, "start", "end", ref paths, new(), false);
Console.WriteLine("Part 1: {0}", paths.Count());

// Part 2
paths = new();
GetAllPaths(ref nodes, "start", "end", ref paths, new(), true);
Console.WriteLine("Part 1: {0}", paths.Count());