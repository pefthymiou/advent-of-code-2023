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

    internal int CalculateGearRatios(string[] input)
    {
        var Directions = new (int, int)[] { (0, 1), (1, 0), (0, -1), (-1, 0), (1, 1), (-1, 1), (1, -1), (-1, -1) };
        var sum = 0;
        var width = input[0].Length;
        var height = input.Length;
        var map = new char[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i, j] = input[j][i];
            }
        }

        var asterisks = FindAsterisks();
        sum = CalculateTotal(asterisks);

        return sum;

        Dictionary<(int, int), List<int>> FindAsterisks()
        {
            var currentNumber = 0;
            var asterisks = new Dictionary<(int, int), List<int>>();
            var neighboringAsterisks = new HashSet<(int, int)>();

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    ProcessCharacter(x, y, ref currentNumber, neighboringAsterisks, asterisks);
                }
                CheckCurrentNumber(ref currentNumber, neighboringAsterisks, asterisks);
            }

            return asterisks;
        }

        void CheckCurrentNumber(ref int currentNumber, HashSet<(int, int)> neighboringAsterisks, Dictionary<(int, int), List<int>> asterisks)
        {
            if (currentNumber != 0 && neighboringAsterisks.Count > 0)
            {
                foreach (var point in neighboringAsterisks)
                {
                    if (!asterisks.TryGetValue(point, out List<int>? value))
                    {
                        value = ([]);
                        asterisks[point] = value;
                    }

                    value.Add(currentNumber);
                }
            }
            currentNumber = 0;
            neighboringAsterisks.Clear();
        }

        bool IsWithinBounds(int x, int y) => x >= 0 && x < width && y >= 0 && y < height;

        int CalculateTotal(Dictionary<(int, int), List<int>> asterisks)
        {
            var total = 0;
            foreach (var numbers in asterisks.Values.Where(numbers => numbers.Count == 2))
            {
                total += numbers[0] * numbers[1];
            }

            return total;
        }

        void CheckForAsterisks(int x, int y, HashSet<(int, int)> neighboringAsterisks)
        {
            foreach (var direction in Directions)
            {
                var neighborX = x + direction.Item1;
                var neighborY = y + direction.Item2;
                if (IsWithinBounds(neighborX, neighborY) && map![neighborX, neighborY] == '*')
                {
                    neighboringAsterisks.Add((neighborX, neighborY));
                }
            }
        }

        void ProcessCharacter(int x, int y, ref int currentNumber, HashSet<(int, int)> neighboringAsterisks, Dictionary<(int, int), List<int>> asterisks)
        {
            var character = map![x, y];
            if (char.IsDigit(character))
            {
                currentNumber = currentNumber * 10 + (character - '0');
                CheckForAsterisks(x, y, neighboringAsterisks);
            }
            else
            {
                CheckCurrentNumber(ref currentNumber, neighboringAsterisks, asterisks);
            }
        }
    }
}
