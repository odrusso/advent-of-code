using System.Text.RegularExpressions;
using AdventUtils;
using Button = (long X, long Y);

namespace AdventOfCode2024.Day13;

public class Day13 : AbstractDay
{
    protected override string[] GetLines() =>
        File.ReadAllLines(
            "/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day13/input.txt");

    protected override object ProcessPartOne(string[] input)
    {
        long acc = 0;

        for (int i = 0; i < input.Length; i += 4)
        {
            var thisInput = input[i..(i + 3)];
            var (buttonA, buttonB, prize) = ParseButtons(thisInput);
            acc += GetPoints(buttonA, buttonB, prize);
        }

        return acc;
    }

    protected override object ProcessPartTwo(string[] input)
    {
        long acc = 0;

        for (int i = 0; i < input.Length; i += 4)
        {
            var thisInput = input[i..(i + 3)];
            var (buttonA, buttonB, prize) = ParseButtons(thisInput);
            prize = (X: prize.X + 10000000000000, Y: prize.Y + 10000000000000);
            acc += GetPoints(buttonA, buttonB, prize);
        }

        return acc;
    }

    private static (Button buttonA, Button buttonB, Button prize) ParseButtons(string[] thisInput)
    {
        var buttonAString = Regex.Matches(thisInput[0], @"\d+");
        var a = long.Parse(buttonAString[0].Value);
        var b = long.Parse(buttonAString[1].Value);

        var buttonBString = Regex.Matches(thisInput[1], @"\d+");
        var c = long.Parse(buttonBString[0].Value);
        var d = long.Parse(buttonBString[1].Value);

        var prizeString = Regex.Matches(thisInput[2], @"\d+");
        var t = long.Parse(prizeString[0].Value);
        var s = long.Parse(prizeString[1].Value);

        Button buttonA = (a, b);
        Button buttonB = (c, d);
        Button prize = (t, s);
        return (buttonA, buttonB, prize);
    }

    private static long GetPoints(Button buttonA, Button buttonB, Button prize)
    {
        decimal y = ((decimal) - buttonA.Y * prize.X + buttonA.X * prize.Y) /
                    (buttonA.X * buttonB.Y - buttonA.Y * buttonB.X);

        if (y < 0) return 0; // Unsolvable, out of +ve range
        if (y % 1 != 0) return 0; // Unsolvable, Y isn't an integer

        decimal x = (prize.X - y * buttonB.X) / buttonA.X;

        if (x < 0) return 0; // Unsolvable, out of +ve range
        if (x % 1 != 0) return 0; // Unsolvable, Y isn't an integer

        // Both points are integers and in range, it's the solution

        return (long)(x * 3 + y);
    }
}