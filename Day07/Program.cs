// Day 07
var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\07.txt");

int GetFuelCost(ref int[] crabs, int target, bool cumulative) {
    var sum = 0;
    for (int i = 0; i < crabs.Length; i++) sum += cumulative ? GetCumulative(Math.Abs(crabs[i] - target)) : Math.Abs(crabs[i] - target);
    return sum;
}

int GetCumulative(int number) {
    int sum = 0;
    for (int i = 1; i <= number; i++) sum += i;
    return sum;
}

var crabs = new int[inputs[0].Split(',').Length];
for (int i = 0; i < inputs[0].Split(',').Length; i++) crabs[i] = Int32.Parse(inputs[0].Split(',')[i]);

// Use Median for P1
Array.Sort(crabs);
Console.WriteLine("Part 1: {0}", GetFuelCost(ref crabs, crabs[crabs.Length / 2 - 1], false));

// User average for P2
// This would need a small search around the average value like -5 to +5, but for my inputs -1 is enough :)
Console.WriteLine("Part 2: {0}", GetFuelCost(ref crabs, Convert.ToInt32(crabs.Average()-1), true));