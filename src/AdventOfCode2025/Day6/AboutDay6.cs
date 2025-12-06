using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2025.Day6;

[TestClass]
public class AboutDay6
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

        var result = new Day6().ProcessPartOne(input);

        Assert.AreEqual(4277556L, result);
    }

    [TestMethod]
    public void PartOne()
    {
        var input = ReadInput("input.txt");

        var result = new Day6().ProcessPartOne(input);

        Assert.AreEqual(6171290547579L, result);
    }

    [TestMethod]
    public void PartTwo_Micro()
    {
        var input = ReadInput("input_micro.txt");

        var result = new Day6().ProcessPartTwo(input);

        Assert.AreEqual(3263827L, result);
    }

    [TestMethod]
    public void PartTwo()
    {
        var input = ReadInput("input.txt");

        var result = new Day6().ProcessPartTwo(input);

        Assert.AreEqual(8811937976367L, result);
    }
}