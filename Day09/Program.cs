// Day 09
var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\09.txt");

// recursively gets the nearby higher locations
static void GetHigherNeighbours(ref byte[,] map, Coord center, ref List<Coord> locations) {
    int x = center.x, y = center.y;
    List<Coord> toCheck = new() { new Coord(x - 1, y), new Coord(x + 1, y), new Coord(x, y - 1), new Coord(x, y + 1) };
    foreach (var coord in toCheck) {
        if (!(0 <= coord.x && coord.x < map.GetLength(0))) continue;    // skip invalid x
        if (!(0 <= coord.y && coord.y < map.GetLength(1))) continue;    // skip invalid y
        if (map[coord.x, coord.y] <= map[center.x, center.y]) continue; // skip lower numbers
        if (map[coord.x, coord.y] == 9) continue;                       // skip 9's
        locations.Add(coord);                                           // add to the list of basin locations
        GetHigherNeighbours(ref map, coord, ref locations);             // get the neighbours of this location
    }
}

// load the matrix
int height = inputs.Count();
int width = inputs[0].Length;
byte[,] map = new byte[height, width];
for (var h = 0; h < inputs.Count(); h++) {
    var chars = inputs[h].ToCharArray();
    for (var w = 0; w < chars.Length; w++)
        map[h, w] = Convert.ToByte(Char.GetNumericValue(chars[w]));
}

var sum = 0;
int[] largest = new int[3];
for (var h = 0; h < height; h++) {
    for (var w = 0; w < width; w++) {
        // skip if not lower than all the surroundings
        if (h > 0) if (map[h - 1, w] <= map[h, w]) continue;
        if (w > 0) if (map[h, w - 1] <= map[h, w]) continue;
        if (h + 1 < height) if (map[h + 1, w] <= map[h, w]) continue;
        if (w + 1 < width) if (map[h, w + 1] <= map[h, w]) continue;  
        sum += map[h, w] + 1;                                           // Part 1
        List<Coord> locations = new() { new Coord(h, w) };              // Part 2:
        GetHigherNeighbours(ref map, new Coord(h, w), ref locations);   // get all higher locations
        locations = locations.Distinct().ToList();                      // remove duplicates
        int minIndx = 0;                                                // store only the 3 largest basins
        for (int i = 1; i < 3; i++)
            if (largest[i] < largest[minIndx]) minIndx = i;
        if (locations.Count > largest[minIndx]) largest[minIndx] = locations.Count;
    }
}
Console.WriteLine("Part 1: {0}", sum);
sum = 1;
foreach (var n in largest) sum *= n;
Console.WriteLine("Part 2: {0}", sum);

struct Coord {
    public int x; public int y;
    public Coord(int x, int y) { this.x = x; this.y = y; }
    public static bool operator ==(Coord left, Coord right) {
        if (left.x == right.x && left.y == right.y) return true; else return false;
    }
    public static bool operator !=(Coord left, Coord right) { return !(left == right); }
}