using System.Buffers;

namespace AdventOfCode.Solutions;

public class Scratchcards : PuzzleSolver
{
    protected override uint Day => 4;

    private static readonly char[] NumberSeparators = ['|', ':'];
    
    private static readonly SearchValues<char> SeparatorSearchValues = SearchValues.Create(NumberSeparators);

    protected override async Task<PuzzleSolution> SolveAsync()
    {
        var parsedInput = GetInputLines();
        var cardValues = new int[16];
        var partTwo = new List<int>(new int[200]);
        var index = 1;

        await foreach (var line in parsedInput)
        {
            partTwo[index]++;
            var wins = GetWinCount(line);
            cardValues[wins]++;
            foreach (var card in Enumerable.Range(1, wins))
            {
                partTwo[index + card]+= partTwo[index];
            }
            index++;
        }
        
        var partOne = cardValues.Select((value, i) => value * (int)Math.Pow(2, i -1));

        return new PuzzleSolution(partOne.Sum(), partTwo.Sum());
    }
    
    private int GetWinCount(string cardLine)
    {
        var card = cardLine.AsSpan();
        var matches = 0;
        
        var startIndex = card.IndexOfAny(SeparatorSearchValues) + 1;
        card = card[startIndex..];
        
        var separatorIndex = card.IndexOfAny(SeparatorSearchValues);

        var winningNumbers = card[..separatorIndex];
        var cardNumbers = card[(separatorIndex + 1)..];
        var winningNumber = GetWinningNumber(winningNumbers);

        while (winningNumber.Length > 0 && winningNumber.Length < winningNumbers.Length)
        {
            var occurs = cardNumbers.Count(winningNumber);
            matches += occurs;
            winningNumbers = winningNumbers[winningNumber.Length..];
            winningNumber = GetWinningNumber(winningNumbers);
        }
        
        return matches;
    }
    
    private ReadOnlySpan<char> GetWinningNumber(ReadOnlySpan<char> input)
    {
        return input.Length >= 3 ? input[..3] : ReadOnlySpan<char>.Empty;
    }
}