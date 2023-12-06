using System.Text;
using AdventOfCode.Utilities.Matching;

namespace AdventOfCode.Solutions;

public class Trebuchet : PuzzleSolver
{
    protected override uint Day => 1;
    
    protected override async Task<PuzzleSolution> SolveAsync()
    {
        var fileLines = GetInputLines();

        var partOne = 0;
        var partTwo = 0;
        
        await foreach (var line in fileLines)
        {
            partOne += GetNumber(line);
            partTwo += GetNumber(CleanLine(line));
        }

        return new PuzzleSolution(partOne, partTwo);
    }
    
    private static string CleanLine(string s)
    {
        var stringBuilder = new StringBuilder (s);
        stringBuilder.Replace("one", "o1e");
        stringBuilder.Replace("two", "t2o");
        stringBuilder.Replace("three", "t3e");
        stringBuilder.Replace("four", "f4u");
        stringBuilder.Replace("five", "f5v");
        stringBuilder.Replace("six", "s6x");
        stringBuilder.Replace("seven", "s7v");
        stringBuilder.Replace("eight", "e8g");
        stringBuilder.Replace("nine", "n9n");
        stringBuilder.Replace("zero", "z0r");
        var result =  stringBuilder.ToString();
        return result;
    }

    private static int GetNumber(string line)
    {
        var span = line.AsSpan();
        var firstIndex = span.IndexOfAny(SearchValues.NumericSearchValues);
        var lastIndex = span.LastIndexOfAny(SearchValues.NumericSearchValues);
        char[] chars = [span[firstIndex], span[lastIndex]];
        return int.Parse(chars);
    }
}