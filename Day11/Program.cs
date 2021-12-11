// Day 11
var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\11.txt");

// flash the current position, increasing everything around it
static void Flash(ref byte[,] oct, int x, int y) {
    oct[x, y] = 0;
    for (var xx = -1; xx <= 1; xx++) {
        if (!(0 <= x + xx && x + xx < oct.GetLength(0))) continue;
        for (var yy = -1; yy <= 1; yy++) {
            if (!(0 <= y + yy && y + yy < oct.GetLength(1))) continue;
            if (oct[x + xx, y + yy] == 0) continue; // flash only once
            oct[x + xx, y + yy] += 1;
        }
    }
}

static int ExecuteStep(ref byte[,] oct) {
    int h = oct.GetLength(0), w = oct.GetLength(1);
    int flashCounter = 0;
    // increase everything by 1
    for (int y = 0; y < h; y++) for (int x = 0; x < w; x++) oct[x, y] += 1;
    // check for flashes
    bool flashed = true;
    while (flashed) {
        flashed = false;
        for (int y = 0; y < h; y++)
            for (int x = 0; x < w; x++)
                if (oct[x, y] > 9) { Flash(ref oct, x, y); flashed = true; flashCounter++; }
    }
    return flashCounter;
}

int h = inputs.Length;
int w = inputs[0].Length;
byte[,] oct = new byte[h, w];
for (int y = 0; y < h; y++) for (int x = 0; x < w; x++) oct[x, y] = Convert.ToByte(inputs[y].ToCharArray()[x].ToString());

int flashes = 0;
for (var i = 0; i < 100; i++) flashes += ExecuteStep(ref oct);
Console.WriteLine("Part 1: {0}", flashes);

int step = 100;
flashes = 0;
while (flashes < 100) { flashes = ExecuteStep(ref oct); step++; }
Console.WriteLine("Part 2: {0}", step);