using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2025.Day9;

[TestClass]
public class AboutDay9
{
    private static string[] ReadInput(string filename, [CallerFilePath] string? filePath = null)
    {
        var directory = Path.GetDirectoryName(filePath);
        var inputPath = Path.Combine(directory!, filename);
        return File.ReadAllLines(inputPath);
    }

    [TestMethod]
    public void PartOne_Micro()
    {
        var input = ReadInput("input_micro.txt");

        var result = new Day9().ProcessPartOne(input);

        Assert.AreEqual(50L, result);
    }

    [TestMethod]
    public void PartOne()
    {
        var input = ReadInput("input.txt");

        var result = new Day9().ProcessPartOne(input);

        Assert.AreEqual(4741848414L, result);
    }

    [TestMethod]
    public void PartTwo_Micro()
    {
        var input = ReadInput("input_micro.txt");

        var result = new Day9().ProcessPartTwo(input);

        Assert.AreEqual(24L, result);
    }

    [TestMethod]
    public void PartTwo()
    {
        var input = ReadInput("input.txt");

        var result = new Day9().ProcessPartTwo(input);

        // 230259758 is too low.
        // 3968815740 is too high!
        // 3043059360 too high!
        Assert.AreEqual(0L, result);
    }
}