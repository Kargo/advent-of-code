using System.Buffers;

namespace AdventOfCode.Utilities.Matching;

public static class SearchValues
{
    private static readonly char[] NumericChars =
    [
        '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
    ];
    
    public static readonly SearchValues<char> NumericSearchValues = System.Buffers.SearchValues.Create(NumericChars);
}