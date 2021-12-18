// Day 18
var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\18.txt");

string CarryLeft (string input, int value) {
    string newstr = "";
    bool carried = false;

    // search for normal to add the carry
    for (var i = input.Length-1; i >= 0; i--) {
        if (input[i] == '[' || input[i] == ']' || input[i] == ',' || carried) {
            newstr = input[i].ToString() + newstr;
            continue;
        }

        // find the length of the number
        var len = 1;
        for (var n = 1; n < 5; n++) {
            if (input[i-n] == '[' || input[i-n] == ']' || input[i-n] == ',') break;
            len++;
        }

        // sum
        int newval = Int32.Parse(input.Substring(i-len+1,len)) + value;
        newstr = newval.ToString() + newstr;
        carried = true;
        i -= len - 1;
    }
    return newstr;
}

string CarryRight(string input, int value) {
    string newstr = "";
    bool carried = false;

    // search for normal to add the carry
    for (var i = 0; i < input.Length; i++) {
        if (input[i] == '[' || input[i] == ']' || input[i] == ',' || carried) {
            newstr += input[i].ToString();
            continue;
        }

        // find the length of the number
        var len = 1;
        for (var n = 1; n < 5; n++) {
            if (input[i+n] == '[' || input[i+n] == ']' || input[i+n] == ',') break;
            len++;
        }

        // sum
        int newval = Int32.Parse(input.Substring(i,len)) + value;
        newstr += newval.ToString();
        carried = true;
        i += len - 1;
    }
    return newstr;
}

string Split(string input) {
    for (var i = 0; i < input.Length; i++) {
        // if it's a number
        if (input[i] != '[' && input[i] != ']' && input[i] != ',') {
            // find the number length
            int len = 0;
            for (var l = 1; l < 5; l++)
                if (input[i+l] != '[' && input[i+l] != ']' && input[i+l] != ',') len = l; else break;
            if (len == 0) continue;

            len++;
            int val = Int32.Parse(input.Substring(i, len));
            int valA = (int)Math.Round((float)val / (float)2, 0, MidpointRounding.ToZero);
            int valB = (int)Math.Round((float)val / (float)2, 0, MidpointRounding.AwayFromZero);

            string newstr = input.Substring(0, i);
            newstr += String.Format("[{0},{1}]", valA, valB);
            newstr += input.Substring(i + len, input.Length - (i + len));
            return newstr;
        }
    }
    // if nothing happened
    return input;
}

string Reduce (string input) {
    string reduced = input, old = input;
    while (true) {
        // first explosions
        int depth = 0;
        for (var i = 0; i < reduced.Length - 4; i++) {
            if (reduced[i] == '[') {
                depth++;
                if (depth >= 5) {
                    // check if there's a lower level still
                    bool hasLower = false;
                    for (var n = 1; n < reduced.Length - i; n++) {
                        if (reduced[i + n] == '[') { hasLower = true; break; }
                        if (reduced[i + n] == ']') break; // doesn't have lower
                    }
                    if (hasLower) continue;

                    // find the end of this number and split it
                    var len = -1;
                    for (var n = 1; n < reduced.Length - i; n++) {
                        if (reduced[i + n] == ']') { len = n - 1; break; }
                    }
                    if (len == -1) throw new Exception();

                    // convert the values
                    var tmp = reduced.Substring(i + 1, len).Split(',');
                    var leftvalue = Convert.ToInt32(tmp[0]);
                    var rightvalue = Convert.ToInt32(tmp[1]);

                    // carry the values
                    var leftstring = reduced[0..i];
                    var rightstring = reduced.Substring(i + len + 2, reduced.Length - (i + len + 2));
                    reduced = CarryLeft(leftstring, leftvalue) + "0" + CarryRight(rightstring, rightvalue);

                    break; // only one operation per "cycle"
                }
            }
            if (reduced[i] == ']') depth--;
        }

        // if everything has exploded we need to split
        if (old == reduced) {
            reduced = Split(reduced);
            if (old == reduced) break; // done
        } else {
            old = reduced;
        }
    }
    return reduced;
}


Int64 GetMagnitude (string input) {
    while (true) {
        if (!input.Contains('[')) break;
        var start = input.LastIndexOf('[');
        var end = input.IndexOf(']', start);
        var tmp = input.Substring(start + 1, end - start - 1).Split(',');
        var newstr = input.Substring(0, start);
        newstr += (Int32.Parse(tmp[0]) * 3 + Int32.Parse(tmp[1]) * 2);
        newstr += input.Substring(end + 1, input.Length - end - 1);
        input = newstr;
    }
    return Int64.Parse(input);
}

// Part 1
string sum = inputs[0];
for (var i = 1; i < inputs.Length; i++)
    sum = Reduce(string.Format("[{0},{1}]", sum, inputs[i]));
Console.WriteLine("Part 1: {0}", GetMagnitude(sum));

// Part 2
Int64 max = 0;
for (var a = 0; a < inputs.Length; a++) {
    for (var b = 0; b < inputs.Length; b++) {
        if (a == b) continue;
        var tmp = Reduce(string.Format("[{0},{1}]", inputs[a], inputs[b]));
        var magnitude = GetMagnitude(tmp);
        if (magnitude > max) max = magnitude;
        // reverse
        tmp = Reduce(string.Format("[{0},{1}]", inputs[b], inputs[a]));
        magnitude = GetMagnitude(tmp);
        if (magnitude > max) max = magnitude;
    }
}
Console.WriteLine("Part 2: {0}", max);