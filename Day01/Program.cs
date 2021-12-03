// Day 01

var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\01.txt");

// Part 1
int last = 0;
int count = 0;
foreach (var input in inputs) {
    var val = Int32.Parse(input);
    if (val > last) count++;
    last = val;
}
count--; // because of the first iteration
Console.WriteLine("Part 1: {0}", count);

// Part 2
count = 0;
for (int i = 3; i < inputs.Length; i++) {
    var window1 = Int32.Parse(inputs[i - 3]) + Int32.Parse(inputs[i - 2]) + Int32.Parse(inputs[i - 1]);
    var window2 = Int32.Parse(inputs[i - 2]) + Int32.Parse(inputs[i - 1]) + Int32.Parse(inputs[i]);
    if (window2 > window1) { count++; }
}
Console.WriteLine("Part 2: {0}", count);