using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2025.Day2;

[TestClass]
public class AboutDay2
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

        var result = new Day2().ProcessPartOne(input);

        Assert.AreEqual(1227775554L, result);
    }

    [TestMethod]
    public void PartOne()
    {
        var input = ReadInput("input.txt");

        var result = new Day2().ProcessPartOne(input);

        Assert.AreEqual(30608905813L, result);
    }

    [TestMethod]
    public void PartTwo_Micro()
    {
        var input = ReadInput("input_micro.txt");

        var result = new Day2().ProcessPartTwo(input);

        Assert.AreEqual(4174379265L, result);
    }

    [TestMethod]
    public void PartTwo()
    {
        var input = ReadInput("input.txt");

        var result = new Day2().ProcessPartTwo(input);

        Assert.AreEqual(31898925685L, result);
    }
}