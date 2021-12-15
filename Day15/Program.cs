// Day 15

// roughly implemented Dijkstra's algorithm
void CalcDistance(ref Dictionary<(int, int), Node> map, (int, int) target) {
    var next = new PriorityQueue<Node, int>();
    map[(0, 0)].distance = 0;
    next.Enqueue(map[(0, 0)], 0);
    
    while (next.Count > 0) {
        var current = next.Dequeue();
        if (current.visited) continue; else current.visited = true;
        if (current.x == target.Item1 && current.y == target.Item2) {
            Console.WriteLine(current.distance);
            break;
        }

        var neighbours = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
        foreach ((int i, int j) in neighbours) {
            var x = current.x + i;
            var y = current.y + j;
            // skip invalid or already visited
            if (!map.ContainsKey((x, y))) continue;
            if (map[(x, y)].visited) continue;
            // check if this path is better
            var alt = current.distance + map[(x, y)].risk;
            if (alt < map[(x, y)].distance) map[(x, y)].distance = alt;
            // add to the queue
            next.Enqueue(map[(x, y)], map[(x, y)].distance);
        }
    }
}


// Part 1
var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\15.txt");
int xMax = inputs[0].Length;
int yMax = inputs.Length;
var map = new Dictionary<(int, int), Node>();
for (var y = 0; y < yMax; y++)
    for (var x = 0; x < xMax; x++)
        map.Add((x, y), new(x, y, Byte.Parse(inputs[y].ToCharArray()[x].ToString())));
Console.WriteLine("Part 1:");
CalcDistance(ref map, (xMax - 1, yMax - 1));


// Part 2
// map multiplied by 5
map = new Dictionary<(int, int), Node>();
var xCount = 0;
var xAdd = 0;
var yCount = 0;
var yAdd = 0;
for (int y = 0; y < yMax * 5; y++) {
    xAdd = 0;
    for (int x = 0; x < yMax * 5; x++) {
        var risk = Byte.Parse(inputs[yCount].ToCharArray()[xCount].ToString());
        // hack to make numbers loop :/
        for (int i = 0; i < xAdd + yAdd; i++) {
            risk++;
            if (risk == 10) risk = 1;
        }
        map.Add((x, y), new(x, y, risk));
        xCount++;
        if (xCount >= xMax) {
            xCount = 0;
            xAdd++;
        }

    }
    yCount++;
    if (yCount >= yMax) {
        yCount = 0;
        yAdd++;
    }
}
Console.WriteLine("Part 2:");
CalcDistance(ref map, (xMax*5 - 1, yMax*5 - 1));


class Node {
    public int x;
    public int y;
    public int risk;
    public int distance = int.MaxValue;
    public bool visited = false;

    public Node(int x, int y, int risk) {
        this.x = x;
        this.y = y;
        this.risk = risk;
    }
}
