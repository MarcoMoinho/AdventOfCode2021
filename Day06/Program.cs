// Day 06
var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\06.txt");

Int64 SumFish(ref Int64[] fish) {
    Int64 sum = 0;
    for (var i = 0; i < 9; i++) sum += fish[i];
    return sum;
}

Int64[] fish = new Int64[9];
foreach (var i in inputs[0].Split(',')) fish[Int32.Parse(i)] += 1;

for (var day = 0; day < 256; day++) {
    var newFish = fish[0];                              // these will spawn new fish
    for (int d = 1; d < 9; d++) fish[d-1] = fish[d];    // move them to the next bucket
    fish[8] = newFish;                                  // add new fish
    fish[6] += newFish;                                 // respwan old fish
    if (day == 79)  Console.WriteLine("Part 1: {0}", SumFish(ref fish));
    if (day == 255) Console.WriteLine("Part 2: {0}", SumFish(ref fish));
}