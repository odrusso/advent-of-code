using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2025.Day0;

[TestClass]
public class AboutDay0
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

        var result = new Day0().ProcessPartOne(input);

        Assert.AreEqual(1187, result);
    }

    [TestMethod]
    public void PartOne()
    {
        var input = ReadInput("input.txt");

        var result = new Day0().ProcessPartOne(input);

        Assert.AreEqual(4601025, result);
    }

    [TestMethod]
    public void PartTwo_Micro()
    {
        var input = ReadInput("input_micro.txt");

        var result = new Day0().ProcessPartTwo(input);

        Assert.AreEqual(1188, result);
    }

    [TestMethod]
    public void PartTwo()
    {
        var input = ReadInput("input.txt");

        var result = new Day0().ProcessPartTwo(input);

        Assert.AreEqual(4601026, result);
    }
}