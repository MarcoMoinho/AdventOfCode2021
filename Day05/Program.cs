// Day 05
struct Line {
    public int x1, x2, y1, y2;
}

class Program {

    static int GetOverlapCount(byte[,] diagram, int maxX, int maxY) {
        int sum = 0;
        for (var y = 0; y <= maxY; y++) 
            for (var x = 0; x <= maxX; x++) 
                if (diagram[x, y] > 1) sum++;
        return sum;
    }

    public static void Main(string[] args) {
        var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\05.txt");

        // parse the inputs
        int maxX = 0, maxY = 0;
        var lines = new List<Line>();
        foreach (var input in inputs) {
            var l = new Line();
            var t = input.Split(" -> ");
            l.x1 = Int32.Parse(t[0].Split(',')[0]);
            l.y1 = Int32.Parse(t[0].Split(',')[1]);
            l.x2 = Int32.Parse(t[1].Split(',')[0]);
            l.y2 = Int32.Parse(t[1].Split(',')[1]);
            lines.Add(l);
            if (l.x1 > maxX) maxX = l.x1;
            if (l.y1 > maxY) maxY = l.y1;
            if (l.x2 > maxX) maxX = l.x2;
            if (l.y2 > maxY) maxY = l.y2;
        }

        // create the diagram matrix
        byte[,] diagram = new byte[maxX + 1, maxY + 1];

        // Part 1
        // straight lines
        foreach (var line in lines) {
            if (line.x1 != line.x2 && line.y1 != line.y2) continue; // only straight lines
            // vertical lines
            if (line.x1 == line.x2) {
                for (var y = Math.Min(line.y1,line.y2); y <= Math.Max(line.y1,line.y2); y++) {
                    diagram[line.x1, y] += 1;
                }
            }
            // horizontal lines
            if (line.y1 == line.y2) {
                for (var x = Math.Min(line.x1, line.x2); x <= Math.Max(line.x1, line.x2); x++) {
                    diagram[x, line.y1] += 1;
                }
            }
     
        }
        Console.WriteLine("Part 1: {0}", GetOverlapCount(diagram,maxX,maxY));

        // Part 2
        // include diagonal lines
        foreach (var line in lines) {
            if (line.x1 == line.x2 || line.y1 == line.y2) continue; // only diagonal lines
            var size = Math.Max(line.x1, line.x2) - Math.Min(line.x1, line.x2);
            var xInc = line.x2 > line.x1 ? 1 : -1;
            var yInc = line.y2 > line.y1 ? 1 : -1;
            for (var i = 0; i <= size; i++) {
                diagram[line.x1 + i * xInc, line.y1 + i * yInc] += 1;
            }
        }
        Console.WriteLine("Part 2: {0}", GetOverlapCount(diagram, maxX, maxY));
    }
}


