using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2025.Day10;

[TestClass]
public class AboutDay10
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

        var result = new Day10().ProcessPartOne(input);

        Assert.AreEqual(7L, result);
    }

    [TestMethod]
    public void PartOne()
    {
        var input = ReadInput("input.txt");

        var result = new Day10().ProcessPartOne(input);

        Assert.AreEqual(422L, result);
    }

    [TestMethod]
    public void PartTwo_Micro()
    {
        var input = ReadInput("input_micro.txt");

        var result = new Day10().ProcessPartTwo(input);

        Assert.AreEqual(33L, result);
    }
    
    [TestMethod]
    public void PartTwo_Simple()
    {
        string[] input = ["[..###] (1,2,3,4) (0,3) (0,1,4) (0,1,3) {32,34,5,22,20}"];

        var result = new Day10().ProcessPartTwo(input);

        Assert.AreEqual(37L, result);
    }    
    
    [TestMethod]
    public void PartTwo_FreeVariable()
    {
        string[] input = ["[#.#....#.] (0,1,2,4,5,6,8) (1,2,3,4,5,6,8) (1,2,4,5,6) (0,2,6,7) (1,2,5) (3,4) (0,2,3,5,8) (0,3,4,5,7,8) (0,1,2,3,5,6,7) (0,1,4,5,6,7,8) {69,65,71,35,38,73,72,60,36}"];

        var result = new Day10().ProcessPartTwo(input);

        Assert.AreEqual(93L, result);
    }

    [TestMethod]
    public void PartTwo()
    {
        var input = ReadInput("input.txt");

        var result = new Day10().ProcessPartTwo(input);

        Assert.AreEqual(16361L, result);
    }
}