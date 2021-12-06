// Day 06
var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\06.txt");

Int64[] fish = new Int64[9];
foreach (var i in inputs[0].Split(',')) fish[Int32.Parse(i)] += 1;

for (var day = 0; day < 256; day++) {
    var newFish = fish[0];                              // these will spawn new fish
    Array.Copy(fish, 1, fish, 0, fish.Length - 1);      // move them to the next bucket 
    fish[8] = newFish;                                  // add new fish
    fish[6] += newFish;                                 // respwan old fish
    if (day == 79)  Console.WriteLine("Part 1: {0}", fish.Sum());
}
Console.WriteLine("Part 2: {0}", fish.Sum());