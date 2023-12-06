using AdventOfCode.Solutions;

namespace AdventOfCode;

internal static class Program
{
    private static Task Main(int day, SolutionType type = SolutionType.Example)
    {
        PuzzleSolver puzzleSolver = day switch
        {
            1 => new Trebuchet {SolutionType = type},
            2 => new CubeConundrum {SolutionType = type},
            3 => new GearRatios {SolutionType = type},
            4 => new Scratchcards {SolutionType = type},
            _ => throw new NotImplementedException("Day not solved yet or invalid")
        };

        return puzzleSolver.RunAsync();
    }
}