// Day 04
var inputs = System.IO.File.ReadAllLines("..\\..\\..\\..\\04.txt");

// clean input
for (int i = 0; i < inputs.Length; i++) inputs[i] = inputs[i].Trim().Replace("  ", " ");

// get numbers
List<string> allNumbers = new List<string>(inputs[0].Split(','));

// Function to get the number of a winning board (if any), 0 if none found
int GetWinningBoard(List<string> numbers, string[] inputs, List<int>? excludedBoards = null) {
    excludedBoards ??= new List<int>();
    // loop through all the boards
    for (int board = 2; board < inputs.Length; board += 6) {
        if (excludedBoards.Contains(board)) continue; // already won this board
        if (inputs[board] == "") continue; // emptied board
        // check for horizontal match
        for (int row = 0; row < 5; row++) {
            if (numbers.Intersect(inputs[board + row].Split(' ')).Count() == 5) return board;
        }
        // check for vertical match
        for (int col = 0; col < 5; col++) {
            var c = new List<string>(); // vertical list
            for (int row = 0; row < 5; row++) c.Add(inputs[board + row].Split(' ')[col]);
            if (numbers.Intersect(c).Count() == 5) return board;
        }
    }
    return 0;
}

// sum all unmarked numbers in a board
int GetUnmarkedSum(List<string> numbers, string[] inputs, int board) {
    int sum = 0;
    for (int row = 0; row < 5; row++) {
        foreach (var s in inputs[board + row].Split(" ").Except(numbers)) sum += Int32.Parse(s);
    }
    return sum;
}

int firstBoard = 0, lastBoard = 0, lastNumber = 0;
List<int> excludedBoards = new List<int>();
for (int number = 5; number < allNumbers.Count; number++) {
    int board = GetWinningBoard(allNumbers.GetRange(0, number), inputs, excludedBoards);
    if (board == 0) continue;
    // Part 1
    if (firstBoard == 0) {
        firstBoard = board;
        Console.WriteLine("Part 1: {0}", GetUnmarkedSum(allNumbers.GetRange(0, number), inputs, board) * Int32.Parse(allNumbers[number - 1]));
    }
    lastBoard = board; lastNumber = number;
    excludedBoards.Add(board);
    number--; // repeat to clear all boards
}

// Part 2
Console.WriteLine("Part 1: {0}", GetUnmarkedSum(allNumbers.GetRange(0, lastNumber), inputs, lastBoard) * Int32.Parse(allNumbers[lastNumber - 1]));