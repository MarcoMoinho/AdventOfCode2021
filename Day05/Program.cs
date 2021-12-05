// Day 05
class Program {
    struct Line {
        public int x1, x2, y1, y2;
        public Line(string[] str) {
            var v = Array.ConvertAll(str, item => Int32.Parse(item));
            x1 = v[0]; y1 = v[1]; x2 = v[2]; y2 = v[3];
        }
    }

    static int GetOverlapCount(byte[,] diagram, int maxX, int maxY) {
        int sum = 0;
        for (var y = 0; y <= maxY; y++) 
            for (var x = 0; x <= maxX; x++) 
                if (diagram[x, y] > 1) sum++;
        return sum;
    }

    static void DrawLine(ref byte[,] diagram, Line line) {
        var size = Math.Max(line.x1, line.x2) - Math.Min(line.x1, line.x2);
        size = Math.Max(size, Math.Max(line.y1, line.y2) - Math.Min(line.y1, line.y2));
        var xInc = line.x2 > line.x1 ? 1 : line.x2 == line.x1 ? 0 : -1;
        var yInc = line.y2 > line.y1 ? 1 : line.y2 == line.y1 ? 0 : -1;
        for (var i = 0; i <= size; i++) diagram[line.x1 + i * xInc, line.y1 + i * yInc] += 1;
    }

    public static void Main(string[] args) {
        var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\05.txt");

        // parse the inputs
        int maxX = 0, maxY = 0;
        var lines = new List<Line>();
        foreach (var input in inputs) {
            var l = new Line(input.Replace(" -> ",",").Split(','));
            lines.Add(l);
            if (l.x1 > maxX) maxX = l.x1;
            if (l.y1 > maxY) maxY = l.y1;
            if (l.x2 > maxX) maxX = l.x2;
            if (l.y2 > maxY) maxY = l.y2;
        }

        // create the diagram matrix
        byte[,] diagram = new byte[maxX + 1, maxY + 1];

        // Part 1
        foreach (var line in lines) {
            if (line.x1 != line.x2 && line.y1 != line.y2) continue; // only straight lines
            DrawLine(ref diagram, line);
        }
        Console.WriteLine("Part 1: {0}", GetOverlapCount(diagram,maxX,maxY));

        // Part 2
        foreach (var line in lines) {
            if (line.x1 == line.x2 || line.y1 == line.y2) continue; // only diagonal lines
            DrawLine(ref diagram, line);
        }
        Console.WriteLine("Part 2: {0}", GetOverlapCount(diagram, maxX, maxY));
    }
}