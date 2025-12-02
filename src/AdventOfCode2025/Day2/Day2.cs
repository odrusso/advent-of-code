using AdventUtils;

namespace AdventOfCode2025.Day2;

public class Day2 : AbstractDay
{
    public override object ProcessPartOne(string[] input)
    {
        var ranges = input.Single().Split(",");

        long runningSum = 0;

        foreach (var range in ranges)
        {
            var rangeLower = range.Split('-').Select(long.Parse).ElementAt(0);
            var rangeUpper = range.Split('-').Select(long.Parse).ElementAt(1);

            runningSum += GetInvalidIdsFromRange(rangeLower, rangeUpper);
        }

        return runningSum;
    }

    public override object ProcessPartTwo(string[] input)
    {
        var ranges = input.Single().Split(",");

        long runningSum = 0;

        foreach (var range in ranges)
        {
            var rangeLower = range.Split('-').Select(long.Parse).ElementAt(0);
            var rangeUpper = range.Split('-').Select(long.Parse).ElementAt(1);

            runningSum += GetInvalidIdsFromRangeWithRepeats(rangeLower, rangeUpper);
        }

        return runningSum;
    }

    private static long GetInvalidIdsFromRange(long rangeLower, long rangeUpper)
    {
        long innerRunningSum = 0;

        for (long i = rangeLower; i <= rangeUpper; i++)
        {
            // We're going through every possible ID here...
            if (IsPalindrome(i))
            {
                innerRunningSum += i;
            }
        }

        return innerRunningSum;
    }

    private static bool IsPalindrome(long value)
    {
        var valueString = value.ToString();

        // No odd length palindromes
        if (valueString.Length % 2 != 0) return false;

        var firstHalf = valueString[..(valueString.Length / 2)];
        var secondHalf = valueString[(valueString.Length / 2)..];

        return firstHalf == secondHalf;
    }

    private static long GetInvalidIdsFromRangeWithRepeats(long rangeLower, long rangeUpper)
    {
        long innerRunningSum = 0;

        for (long i = rangeLower; i <= rangeUpper; i++)
        {
            // We're going through every possible ID here...
            if (IsRepeat(i))
            {
                innerRunningSum += i;
            }
        }

        return innerRunningSum;
    }

    private static bool IsRepeat(long value)
    {
        var valueString = value.ToString();

        // We need the factors of the length to be able to split it into chunks
        var factors = GetAllDivisors(valueString.Length);

        foreach (var factor in factors)
        {
            var chunkLength = valueString.Length / factor;
            var chunks = valueString.Chunk(chunkLength).Select(c => new string(c)).ToArray();
            if (chunks.Distinct().Count() == 1) return true;
        }

        return false;
    }

    private static List<int> GetAllDivisors(int length)
    {
        var divisors = new List<int>();
        
        // We start at 2 because we don't care about 0 (div by 0, eww) or 1 (div by 1, not interesting)
        for (int i = 2; i <= length; i++)
        {
            if (length % i == 0)
            {
                divisors.Add(i);
            }
        }

        return divisors;
    }
}