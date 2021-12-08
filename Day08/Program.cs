// Day 08
var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\08.txt");

// Part 1
int count = 0;
int[] toCount = new int[4] {2,3,4,7};
foreach (var line in inputs) {
    var output = line.Split(" | ")[1].Split(' ');
    count += output.Where(x => toCount.Contains(x.Length)).Count();
}
Console.WriteLine("Part 1: {0}", count);

// Part 2
static string Match(string[] inputs, string segment, int length, int intersections, string? exclude = null) {
    exclude ??= "";
    foreach (var input in inputs) {
        if (input.Length != length || input == exclude) continue;
        if (input.ToCharArray().Intersect(segment.ToCharArray()).Count() == intersections) return input;
    }
    throw new Exception("");
}

int sum = 0;
foreach (var line in inputs) {
    string[] segments = new string[10];
    var input  = line.Split(" | ")[0].Split(' ');
    var output = line.Split(" | ")[1].Split(' ');
    // get the easy ones based on length
    segments[1] = input.Where(x => x.Length == 2).First();
    segments[4] = input.Where(x => x.Length == 4).First();
    segments[7] = input.Where(x => x.Length == 3).First();
    segments[8] = input.Where(x => x.Length == 7).First();
    // get the hard ones, based on previously known ones and partial matches
    segments[3] = Match(input, segments[1], 5, 2);
    segments[9] = Match(input, segments[3], 6, 5);
    segments[6] = Match(input, segments[7], 6, 2);
    segments[5] = Match(input, segments[6], 5, 5);
    segments[0] = Match(input, segments[1], 6, 2, segments[9]);
    segments[2] = Match(input, segments[5], 5, 3);
    // get the number
    string tmp = "";
    foreach (var digit in output) {
        for (var i = 0; i < 10; i++) {
            if (segments[i].Length != digit.Length) continue;
            if (segments[i].ToCharArray().Intersect(digit.ToCharArray()).Count() == digit.Length) tmp += i;
        }
    }
    sum += Int32.Parse(tmp);
}
Console.WriteLine("Part 2: {0}", sum);