// Day 16
Int64 ReadBits(ref Queue<string> bits, int length) {
    string tmp = "";
    for (int i = 0; i < length; i++) tmp += bits.Dequeue().ToString();
    return Convert.ToInt64(tmp, 2);
}

Int64 GetLiteralValue(ref Queue<string> bits) {
    string tmp = "";
    while (true) {
        var leadBit = bits.Dequeue();
        for (var i = 0; i < 4; i++) tmp += bits.Dequeue();
        if (leadBit == "0") break;
    }
    return Convert.ToInt64(tmp, 2);
}

List<Int64> ReadSubPacket(ref Queue<string> bits, int value, bool count, ref int versionSum) {
    var tmp = new List<Int64>();
    while (true) {
        tmp.Add(ReadPacket(ref bits, ref versionSum));
        if (count) {    // if it's count based
            value--;
            if (value == 0) break;
        } else {        // otherwise it's length based
            if (bits.Count == value) break;
        }
    }
    return tmp;
}

Int64 ReadPacket(ref Queue<string> bits, ref int versionSum) {
    // read the packet header
    var version = ReadBits(ref bits, 3);
    var type = ReadBits(ref bits, 3);
    versionSum += (int)version;

    // literal type
    if (type == 4) return GetLiteralValue(ref bits);

    // retrieve subpackets for the other types
    var sub = new List<Int64>();
    var lenType = bits.Dequeue();
    if (lenType == "0") {
        int length = (int)ReadBits(ref bits, 15);
        sub = ReadSubPacket(ref bits, bits.Count - length, false, ref versionSum);
    }
    if (lenType == "1") {
        int count = (int)ReadBits(ref bits, 11);
        sub = ReadSubPacket(ref bits, count, true, ref versionSum);
    }

    // process all other types
    if (type == 0) return sub.Sum();
    if (type == 1) return sub.Aggregate((a, b) => a * b);
    if (type == 2) return sub.Min();
    if (type == 3) return sub.Max();
    if (type == 5) return sub[0] > sub[1] ? 1 : 0;
    if (type == 6) return sub[0] < sub[1] ? 1 : 0;
    if (type == 7) return sub[0] == sub[1] ? 1 : 0;

    throw new Exception();
}

// to convert from hex to binary
Dictionary<string, string> hex = new() { };
var inputsHex = System.IO.File.ReadAllLines("..\\..\\..\\..\\16h.txt");
foreach (var input in inputsHex) {
    var h = input.Split(" = ");
    hex.Add(h[0], h[1]);
}

// read the inputs
Queue<string> bits = new();
var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\16.txt");
foreach (var c in inputs[0].ToCharArray()) {
    foreach (var d in hex[c.ToString()].ToCharArray()) {
        bits.Enqueue(d.ToString());
    }
}

int part1 = 0;
var part2 = ReadPacket(ref bits, ref part1);
Console.WriteLine("Part 1: {0}", part1);
Console.WriteLine("Part 2: {0}", part2);