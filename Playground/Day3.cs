using System.Text.RegularExpressions;

namespace Playground;

internal sealed partial class Day3
{
    private readonly Regex _numbersRegex = NumbersRegex();
    private readonly Regex _charsRegex = CharsRegex();
    private readonly (int, int)[] _directions = [(-1, 0), (1, 0), (0, -1), (0, 1)];

    [GeneratedRegex(@"\d+", RegexOptions.Compiled)]
    private static partial Regex NumbersRegex();

    [GeneratedRegex(@"^\d+", RegexOptions.Compiled)]
    private static partial Regex CharsRegex();

    internal int CalculatePartNumbers(string[] lines)
    {
        var sum = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            Dictionary<int, char> previousLineSymbols = [];
            Dictionary<int, char> nextLineSymbols = [];

            var line = lines[i];
            var numberMatches = _numbersRegex.Matches(line);
            var currentLineSymbols = GetSymbolsInLine(line);

            if (i > 0)
            {
                previousLineSymbols = GetSymbolsInLine(lines[i - 1]);
            }

            if (i < lines.Length - 1)
            {
                nextLineSymbols = GetSymbolsInLine(lines[i + 1]);
            }

            sum += numberMatches
                .Where(m => 
                    IsSymbolAdjacent(m, currentLineSymbols) || 
                    IsSymbolAdjacent(m, previousLineSymbols) || 
                    IsSymbolAdjacent(m, nextLineSymbols))
                .Sum(m => int.Parse(m.Value));
        }


        return sum;

        static Dictionary<int, char> GetSymbolsInLine(ReadOnlySpan<char> input)
        {
            Dictionary<int, char> symbols = [];
            for (int i = 0; i < input.Length; i++)
            {
                var @char = input[i];
                if (!char.IsDigit(@char) && @char != '.')
                {
                    symbols[i] = @char;
                }
            }

            return symbols;
        }

        bool IsSymbolAdjacent(Match match, IReadOnlyDictionary<int, char> symbols)
        {
            var min = match.Index;
            var max = min + match.Length - 1;
            return symbols.Keys.Any(k => k >= min - 1 && k <= max + 1);
        }
    }

    internal int Calculate(string[] input)
    {
        var sum = 0;

        return sum;

        int CalculateRatio(IReadOnlyList<string> rows, int x, int y, int width, int height)
        {
            List<int> adjacentNumbers = [];

            return adjacentNumbers.First();
        }
    }
}
