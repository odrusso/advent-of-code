using AdventUtils;

namespace AdventOfCode2025.Day3;

public class Day3 : AbstractDay
{
    public override object ProcessPartOne(string[] input)
    {
        long acc = 0;

        foreach (var bankString in input)
        {
            var bank = bankString.Select(c => int.Parse(new string(c, 1))).ToArray();
            long solved = SolveBank(bank, 2);
            acc += solved;
        }

        return acc;
    }

    public override object ProcessPartTwo(string[] input)
    {
        long acc = 0;

        foreach (var bankString in input)
        {
            var bank = bankString.Select(c => int.Parse(new string(c, 1))).ToArray();
            long solved = SolveBank(bank, 12);
            acc += solved;
        }

        return acc;
    }

    private long SolveBank(int[] bank, int size)
    {
        // Start with a 'base case' of having all positions at the tail of the bank occupied, which will be the last 'size' values
        Dictionary<int, int> positions = new();
        for (var position = 0; position < size; position++)
        {
            positions[position] = bank.Length - size + position;
        }

        // Then we work through each position, and move it as far 'left' as possible, such that it takes the biggest number available
        // If there are multiple of the biggest position available, take the most-left one
        // Repeat this for all values in the bank, at which point the value will be maximised
        for (var position = 0; position < size; position++)
        {
            var positionIndex = positions[position];
            var positionValue = bank[positionIndex];

            // The leftmost position we can consider is the position after the last occupied position (or 0 if it's at the front)
            var leftmostPosition = positions.ContainsKey(position - 1) ? positions[position - 1] + 1 : 0;

            // The rightmost position we can consider is the digits current position, as it's already right-most to begin with
            var rightmostPosition = positionIndex;

            // Move right-to-left along the open positions to find the left-most bigger option
            for (int bankIndex = rightmostPosition; bankIndex >= leftmostPosition; bankIndex--)
            {
                var testValue = bank[bankIndex];
                if (testValue >= positionValue)
                {
                    // Update both the running index and the max value
                    positionValue = testValue;
                    positions[position] = bankIndex;
                }
            }
        }

        // Then get the value represented by the positions.
        long acc = 0;
        for (var position = 0; position < size; position++)
        {
            var positionPower = (long)Math.Pow(10, size - position - 1);
            acc += bank[positions[position]] * positionPower;
        }

        return acc;
    }
}