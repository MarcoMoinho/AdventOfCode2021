// Day 02

var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\02.txt");

// Part 1
int h_pos = 0;
int depth = 0;
foreach (var input in inputs) {
    var cmd = input.Split(" ");
    switch (cmd[0]) {
        case "forward":
            h_pos += Int32.Parse(cmd[1]);
            break;
        case "up":
            depth -= Int32.Parse(cmd[1]);
            break;
        case "down":
            depth += Int32.Parse(cmd[1]);
            break;
    }
}
Console.WriteLine("Part 1: {0}", h_pos * depth);

// Part 2
h_pos = 0;
depth = 0;
int aim = 0;
foreach (var input in inputs) {
    var cmd = input.Split(" ");
    switch (cmd[0]) {
        case "forward":
            h_pos += Int32.Parse(cmd[1]);
            depth += Int32.Parse(cmd[1]) * aim;
            break;
        case "up":
            aim -= Int32.Parse(cmd[1]);
            break;
        case "down":
            aim += Int32.Parse(cmd[1]);
            break;
    }
}
Console.WriteLine("Part 2: {0}", h_pos * depth);