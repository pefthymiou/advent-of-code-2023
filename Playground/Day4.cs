namespace Playground;

internal class Day4
{
    internal static int CalculatePoints(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.TrimEntries);
        var totalPoints = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries).ToArray();
            var numbersPart = parts[1].Split("|", StringSplitOptions.TrimEntries).ToArray();
            var winningNumbers = numbersPart[0]
                .Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                .ToArray();
            var numbers = numbersPart[1]
                .Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                .ToArray();

            var points = numbers.Where(number => winningNumbers.Contains(number))
                .Aggregate(0, (current, _) => current == 0 ? 1 : current * 2);

            totalPoints += points;
        }

        return totalPoints;
    }

    internal static int CalculateScratchcards(string input)
    {
        var totalScratchcards = 0;
        var lines = input.Split('\n');
        List<(int cardNo, int[] winningNumbers, int[] numbers)> cards = [];
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var parts = line.Split(':', StringSplitOptions.TrimEntries).ToArray();
            var numbersPart = parts[1].Split("|", StringSplitOptions.TrimEntries).ToArray();
            var winningNumbers = numbersPart[0]
                .Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            var numbers = numbersPart[1]
                .Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            cards.Add((i + 1, winningNumbers, numbers));
        }

        var index = 0;
        while (index < cards.Count)
        {
            var (cardNo, winningNumbers, numbers) = cards[index];
            totalScratchcards++;
            var matches = numbers.Count(number => winningNumbers.Contains(number));

            for (var i = 1; i <= matches && cardNo + i <= input.Length; i++)
            {
                var cardIndex = cardNo + i - 1;
                var card = lines[cardIndex];
                var parts = card[(card.IndexOf(": ", StringComparison.Ordinal) + 1)..].Split("|", StringSplitOptions.TrimEntries).ToArray();
                var nextWinningNumbers = parts[0]
                    .Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                var nextNumbers = parts[1]
                    .Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                cards.Add((cardNo + i, nextWinningNumbers, nextNumbers));
            }

            index++;
        }

        return totalScratchcards;
    }
}
