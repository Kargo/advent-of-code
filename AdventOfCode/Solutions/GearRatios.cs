using System.Buffers;

namespace AdventOfCode.Solutions;

public class GearRatios : PuzzleSolver
{
    protected override uint Day => 3;

    private static readonly char[] PartChars =
    [
        '!', '@', '#', '$', '&', '?', '=', '*', '+', '-', '/', '%'
    ];
    
    private static readonly SearchValues<char> PartsSearchValues = SearchValues.Create(PartChars);
    
    protected override async Task<PuzzleSolution> SolveAsync()
    {
        var parsedInput = await GetParsedInputAsync(ParseInput);
        var partOne = 0;
        var partTwo = 0;

        for (var i = 0; i < parsedInput.Length; i++)
        {
            var chars = parsedInput[i];
            for (var j = 0; j < chars.Length; j++)
            {
                var symbol = chars[j];
                if (PartsSearchValues.Contains(symbol))
                {
                    var gears = new List<int>(4);
                    ProcessAdjacentCells(parsedInput, i, j, (c, x, y) =>
                    {
                        if (char.IsDigit(c))
                        {
                            var fullNumber = GetFullNumber(parsedInput, x, y);
                            if (symbol == '*')
                            {
                                gears.Add(fullNumber);
                            }
                            partOne += fullNumber;
                        }
                    });
                    
                    if (gears.Count == 2)
                    {
                        partTwo += gears[0] * gears[1];
                    }
                }
            }
        }

        return new PuzzleSolution(partOne, partTwo);
    }
    
    private int GetFullNumber(char[][] matrix, int x, int y)
    {
        int startIndex = x, endIndex = x + 1;
        var line = matrix[y].AsSpan();
        for (;startIndex > 0; startIndex--)
        {
            if (!char.IsDigit(line[startIndex-1]))
            {
                break;
            }
        }
        
        for (;endIndex < line.Length; endIndex++)
        {
            if (!char.IsDigit(line[endIndex]))
            {
                break;
            }
        }

        var result = int.Parse(line[startIndex..endIndex]);
        line[startIndex..endIndex].Fill('.');
        return result;
    }
    
    private void ProcessAdjacentCells<T>(T[][] matrix, int i, int j, Action<T, int, int> process)
    {
        int[] xDir = {-1, 0, 1, 1, 1, 0, -1, -1};
        int[] yDir = {-1, -1, -1, 0, 1, 1, 1, 0};
        
        for (var direction = 0; direction < 8; direction++)
        {
            int newRow = i + xDir[direction], newCol = j + yDir[direction];
            
            if (newRow >= 0 && newRow < matrix.Length && newCol >=0 && newCol < matrix[0].Length)
            {
                process(matrix[newRow][newCol], newCol, newRow);
            }
        }
    }
    
    private static char[][] ParseInput(string[] input)
    {
        return input
            .Select(x => x.ToCharArray())
            .ToArray();
    }
}