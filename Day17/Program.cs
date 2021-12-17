// Day 17

int FireProbe(int[] target_x, int[] target_y, int x, int y) {
    int xpos = x, ypos = y;
    int max_y = int.MinValue;

    while (true) {
        if (ypos > max_y) max_y = ypos;

        // check for hit
        if (xpos >= target_x[0] && xpos <= target_x[1])
            if (ypos >= target_y[0] && ypos <= target_y[1]) return max_y; // hit

        // check it's still in bounds
        if (ypos < target_y[0]) return int.MinValue; // over bounds
        if (xpos > target_x[1]) return int.MinValue; // over bounds

        // apply drag
        if (x > 0) { x--; xpos += x; }
        y--; ypos += y;
    }
}

var target_x = new int[2] { 102, 157 };
var target_y = new int[2] { -146, -90 };
var max_y = int.MinValue;
var values = new List<(int, int)>();

// brute force this :)
for (var x = 1; x <= target_x[1]; x++)
    for (var y = target_y[0]; y < 500; y++) {
        var hit = FireProbe(target_x, target_y, x, y);
        if (hit > max_y) max_y = hit;
        if (hit > int.MinValue) if (!values.Contains((x,y))) values.Add((x,y));
    }

Console.WriteLine("Part 1: {0}", max_y);
Console.WriteLine("Part 2: {0}", values.Count);