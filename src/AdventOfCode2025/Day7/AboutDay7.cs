using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2025.Day7;

[TestClass]
public class AboutDay7
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

        var result = new Day7().ProcessPartOne(input);

        Assert.AreEqual(21, result);
    }

    [TestMethod]
    public void PartOne()
    {
        var input = ReadInput("input.txt");

        var result = new Day7().ProcessPartOne(input);

        Assert.AreEqual(1553, result);
    }

    [TestMethod]
    public void PartTwo_Micro()
    {
        var input = ReadInput("input_micro.txt");

        var result = new Day7().ProcessPartTwo(input);

        Assert.AreEqual(40L, result);
    }

    [TestMethod]
    public void PartTwo()
    {
        var input = ReadInput("input.txt");

        var result = new Day7().ProcessPartTwo(input);

        Assert.AreEqual(15811946526915L, result);
    }
}