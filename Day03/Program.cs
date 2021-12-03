// Day 03
var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\03.txt");

// Part 1
int str_len = inputs[0].Length;
int[] count = new int[str_len];
foreach (var input in inputs) {
    for (int i = 0; i < input.Length; i++) {
        if (input.ToArray()[i] == '1') count[i]++;
    }
}
string gamma = "", epsilon = "";
for (int i = 0; i < str_len; i++) {
    if (count[i] <= inputs.Length / 2) {
        gamma += "0"; epsilon += "1";
    } else {
        gamma += "1"; epsilon += "0";
    }
}
Console.WriteLine("Part 1: {0}", Convert.ToInt64(gamma,2)*Convert.ToInt64(epsilon,2));

// Part 2
string GetRating(string[] inputs, bool most_common, int pos) {
    int cmn_count = 0;
    char cmn_char;
    string[] new_inputs;
    // get most common
    foreach (var input in inputs) if (input.ToCharArray()[pos] == '1') cmn_count++; 
    if (cmn_count * 2 >= inputs.Length) cmn_char = '1'; else cmn_char = '0';
    // set new array size
    if (most_common) { 
        if (cmn_char == '1') new_inputs = new string[cmn_count]; else new_inputs = new string[inputs.Length - cmn_count];
    } else {
        if (cmn_char == '1') new_inputs = new string[inputs.Length - cmn_count]; else new_inputs = new string[cmn_count];
    }
    // copy the inputs
    int c = 0;
    foreach (var input in inputs) {
        if (most_common  && input.ToCharArray()[pos] == cmn_char) { new_inputs[c] = input; c++; }
        if (!most_common && input.ToCharArray()[pos] != cmn_char) { new_inputs[c] = input; c++; }
    }
    // get next results
    if (cmn_count > 1) return GetRating(new_inputs, most_common, ++pos);
    return new_inputs[0];
}
string oxygen = GetRating(inputs, true, 0), CO2 = GetRating(inputs, false, 0);
Console.WriteLine("Part 2: {0}", Convert.ToInt64(oxygen, 2) * Convert.ToInt64(CO2, 2));