using System.Buffers;

namespace AdventOfCode.Solutions;

public class CubeConundrum : PuzzleSolver
{
    protected override uint Day => 2;

    private static readonly char[] GameSeparatorChar = [':', ','];
    private static readonly char[] ColourStartChar = ['r', 'g', 'b'];
    private static readonly SearchValues<char> GameSearchValues = SearchValues.Create(GameSeparatorChar);
    private static readonly SearchValues<char> ColourSearchValues = SearchValues.Create(ColourStartChar);

    protected override async Task<PuzzleSolution> SolveAsync()
    {
        var games = GetParsedInputLinesAsync(ParseGame);
        uint partOne = 0;
        uint partTwo = 0;

        await foreach (var game in games)
        {
            if (IsValidGame(game))
            {
                partOne += game.Id;
            }
            
            partTwo += game.Red * game.Green * game.Blue;
        }

        return new PuzzleSolution(partOne, partTwo);
    }
    
    private static Game ParseGame(string input)
    {
        var gameLineSpan = input.AsSpan().Trim();
        var idEndIndex = gameLineSpan.IndexOfAny(GameSearchValues);
        var id = uint.Parse(gameLineSpan.Slice(5, idEndIndex - 5));
        
        var setsSpan = gameLineSpan[(idEndIndex + 2)..];
        uint r = 0, g = 0, b = 0;
        var colourSpan = setsSpan;
        var colourStartIndex = colourSpan.IndexOfAny(ColourSearchValues);
        while (colourStartIndex >= 0)
        {
            var value = uint.Parse(colourSpan[..(colourStartIndex - 1)]);
            var colourOffset = colourStartIndex;
            switch (colourSpan[colourStartIndex])
            {
                case 'r':
                    r = Math.Max(r, value);
                    colourOffset += 5;
                    break;
                case 'g':
                    g = Math.Max(g, value);
                    colourOffset += 7;
                    break;
                case 'b':
                    b = Math.Max(b, value);
                    colourOffset += 6;
                    break;
            }

            if (colourOffset >= colourSpan.Length)
            {
                break;
            }
            
            colourSpan = colourSpan[colourOffset..].Trim();
            colourStartIndex = colourSpan.IndexOfAny(ColourSearchValues);
        }
        
        return new Game(id, r, g, b);
    }

    private static bool IsValidGame(Game game)
    {
        return game is {Red: <= 12, Green: <= 13, Blue: <= 14};
    }

    private record Game(uint Id, uint Red, uint Green, uint Blue);
}