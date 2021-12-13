// Day 13
var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\13.txt");

// print the content
static void Print(ref bool[,] paper, int hMax, int vMax) {
    for (var y = 0; y < vMax; y++) {
        for (var x = 0; x < hMax; x++) {
            if (paper[x, y]) Console.Write("X"); else Console.Write(".");
        }
        Console.WriteLine();
    }
}

// vertical fold
static void VFold(ref bool[,] paper, int hMax, int line) {
    for (var y = line - 1; y >= 0; y--) {
        for (var x = 0; x < hMax; x++) {
            if (paper[x,y] || paper[x,line + (line-y)]) paper[x,y] = true; else paper[x,y] = false;
        }
    }
}

// horizontal fold
static void HFold(ref bool[,] paper, int vMax, int row) {
    for (var y = 0; y < vMax; y++) {
        for (var x = row-1; x >= 0; x--) {
            if (paper[x, y] || paper[row + (row -x), y]) paper[x, y] = true; else paper[x, y] = false;
        }
    }
}

// retrieve the data
List<Coord> coordList = new();
Queue<string> foldList = new();
foreach (var input in inputs) {
    if (input == "") continue;
    if (input.IndexOf("fold") > -1) { foldList.Enqueue(input.Split(' ')[2]); continue; }
    coordList.Add(new Coord(Int32.Parse(input.Split(',')[0]), Int32.Parse(input.Split(',')[1])));
}

// find the paper size
int hMax = 0, vMax = 0;
foreach (var coord in coordList) {
    if (coord.x > hMax) hMax = coord.x;
    if (coord.y > vMax) vMax = coord.y;
}

// create the paper
vMax += 1;
hMax += 1;
bool[,] paper = new bool[hMax,vMax];
foreach (var coord in coordList) paper[coord.x,coord.y] = true;

// fold everything
int count = 0, sum = 0;
while (foldList.Count > 0) {
    var fold = foldList.Dequeue();
    var tmp = fold.Split('=');
    if (tmp[0] == "y") {
        VFold(ref paper, hMax, Int32.Parse(tmp[1]));
        vMax = Int32.Parse(tmp[1]);
    } else {
        HFold(ref paper, vMax, Int32.Parse(tmp[1]));
        hMax = Int32.Parse(tmp[1]);
    }
    count ++;
    if (count == 1) // Part 1 is only the first fold
        for (var y = 0; y < vMax; y++) for (var x = 0; x < hMax; x++) if (paper[x, y]) sum++;
}
Console.WriteLine("Part 1: {0}", sum);
Console.WriteLine("\nPart 2:");
Print(ref paper, hMax, vMax);


struct Coord {
    public int x; public int y;
    public Coord(int x, int y) { this.x = x; this.y = y; }
}