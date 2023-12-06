using System.Diagnostics;

namespace AdventOfCode;

public abstract class PuzzleSolver
{
    public required SolutionType SolutionType { get; init; }
    protected abstract uint Day { get; }
    
    private string BasePath => SolutionType switch
    {
        SolutionType.Puzzle =>  @"D:\Projects\advent-of-code\puzzle-input",
        SolutionType.Example => @"D:\Projects\advent-of-code\example-input",
        _ => throw new ArgumentOutOfRangeException(nameof(SolutionType), $"Unexpected {nameof(SolutionType)}: {SolutionType}"),
    };
    
    private string FilePath => Path.Combine(BasePath, $"{Day}.txt");
    
    protected IAsyncEnumerable<string> GetInputLines()
    {
        return File.ReadLinesAsync(FilePath);
    }
    
    protected async IAsyncEnumerable<T> GetParsedInputLinesAsync<T>(Func<string, T> parser)
    {
        await foreach (var line in GetInputLines())
        {
            yield return parser(line);
        }
    }
    
    protected async Task<T> GetParsedInputAsync<T>(Func<string[], T> parser)
    {
        var file = await File.ReadAllLinesAsync(FilePath);

        return parser(file);
    }

    protected abstract Task<PuzzleSolution> SolveAsync();

    public async Task RunAsync()
    {
        var startTime = Stopwatch.GetTimestamp();
        var solution = await SolveAsync();
        
        var elapsedTime = Stopwatch.GetElapsedTime(startTime);
        Console.WriteLine($"Part 1: {solution.PartOne}");
        Console.WriteLine($"Part 2: {solution.PartTwo}");
        Console.WriteLine($"Calculation time: {elapsedTime}");
    }
}