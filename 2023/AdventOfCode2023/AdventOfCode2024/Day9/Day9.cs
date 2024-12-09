using AdventUtils;

namespace AdventOfCode2024.Day9;

public class Day9 : AbstractDay
{
    private record Block(int BlockId, int Length)
    {
        public bool Free => BlockId == -1;
        public bool Data => !Free;
    }

    protected override string[] GetLines() =>
        File.ReadAllLines(
            $"/Users/oscar/Projects/advent-of-code/2023/AdventOfCode2023/AdventOfCode2024/Day9/input.txt");

    protected override object ProcessPartOne(string[] input)
    {
        var disk = input.Single();

        var n = disk
            .Select((v, i) => new { BlockId = i / 2, Length = int.Parse(v.ToString()) })
            .Select((res, i) =>
                i % 2 == 0
                    ? Enumerable.Repeat(res.BlockId, res.Length).ToArray()
                    : Enumerable.Repeat(-1, res.Length).ToArray())
            .SelectMany(a => a).ToArray();

        List<int> tidyDisk = [];

        int forwardScanIndex = 0;
        int backScanIndex = n.Length - 1;

        while (forwardScanIndex <= backScanIndex)
        {
            int head = n[forwardScanIndex];
            int tail = n[backScanIndex];

            if (head != -1)
            {
                // Head is a value, so does tail.
                tidyDisk.Add(head);
                forwardScanIndex++;
                continue;
            }

            if (tail != -1)
            {
                // Head is free space, tail has a value.
                tidyDisk.Add(tail);
                backScanIndex--;
                forwardScanIndex++;
                continue;
            }

            // Head is free space, tail free space. Do nothing, move tail down.
            backScanIndex--;
        }

        return tidyDisk.Select((id, pos) => new { Id = (long)id, Pos = (long)pos }).Sum(v => v.Pos * v.Id);
    }

    protected override object ProcessPartTwo(string[] input)
    {
        var diskIn = input.Single();
        var disk = diskIn.Select((v, i) => new Block(i % 2 == 0 ? i / 2 : -1, int.Parse(v.ToString()))).ToList();

        HashSet<int> movedBlockIds = [];

        for (int backScanIndex = disk.Count - 1; backScanIndex >= 0; backScanIndex--)
        {
            Block tail = disk[backScanIndex];

            if (tail.Free) continue;
            if (movedBlockIds.Contains(tail.BlockId)) continue;

            int forwardScanIndex = 0;
            while (forwardScanIndex <= backScanIndex)
            {
                Block head = disk[forwardScanIndex];

                if (head.Data || head.Length < tail.Length)
                {
                    forwardScanIndex++;
                    continue;
                }

                if (head.Length == tail.Length)
                {
                    disk[forwardScanIndex] = disk[backScanIndex];
                    disk[backScanIndex] = tail with { BlockId = -1 };
                    movedBlockIds.Add(head.BlockId);
                    break;
                }

                if (head.Length > tail.Length)
                {
                    disk[forwardScanIndex] = disk[backScanIndex];
                    disk.Insert(forwardScanIndex + 1, new Block(-1, head.Length - tail.Length));
                    disk[backScanIndex + 1] = tail with { BlockId = -1 };
                    movedBlockIds.Add(head.BlockId);
                    break;
                }

                forwardScanIndex++;
            }
        }

        // I'm sure this could be more terse.
        return disk
            .Select(b => Enumerable.Repeat(b.Free ? 0 : b.BlockId, b.Length))
            .SelectMany(a => a)
            .Select((v, pos) => new { Id = (long)v, Pos = (long)pos })
            .Sum(v => v.Pos * v.Id);
    }
}