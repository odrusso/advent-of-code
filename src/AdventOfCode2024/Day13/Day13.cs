using System.Text.RegularExpressions;
using AdventUtils;
using Button = (long X, long Y);

namespace AdventOfCode2024.Day13;

public class Day13 : AbstractDay
{
    public override object ProcessPartOne(string[] input)
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

    public override object ProcessPartTwo(string[] input)
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
        long yNum = buttonA.X * prize.Y - buttonA.Y * prize.X;
        long yDenom = buttonA.X * buttonB.Y - buttonA.Y * buttonB.X;
        
        if (yNum > 0 && yDenom < 0 || yNum < 0 && yDenom > 0) return 0; // Will result in -nve solution if they have opposite signs
        if (yNum % yDenom != 0) return 0; // Not divisible, would result in decimal solution

        long y = yNum / yDenom;
        
        long xNum = prize.X - y * buttonB.X;
        
        if (xNum < 0) return 0; // We know denom is always +ve, so if numerator is -nve, this will be -ve
        if (xNum % buttonA.X != 0) return 0; // Not an integer solution
        
        long x = xNum / buttonA.X;

        return x * 3 + y;
    }
}