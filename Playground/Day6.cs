using System.Text;

namespace Playground;

internal class Day6
{
    internal static int CalculateTotalWaysOfWinningPart1(string input)
    {
        var totalWays = 1;
        var records = input.Split('\n', StringSplitOptions.TrimEntries);

        var times = records[0][(records[0].IndexOf(':') + 1)..].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var distances = records[1][(records[1].IndexOf(':') + 1)..].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        for (var i = 0; i < times.Length; i++)
        {
            var waysToWinRace = CalculateWaysToWin(times[i], distances[i]);
            totalWays *= waysToWinRace;
        }

        return totalWays;

        static int CalculateWaysToWin(int totalTime, int recordDistance)
        {
            var waysToWin = 0;

            for (int i = 1; i < totalTime; i++)
            {
                var travelTime = totalTime - i;
                var travelDistance = i * travelTime;

                if (travelDistance > recordDistance)
                {
                    waysToWin++;
                }
            }

            return waysToWin;
        }
    }

    internal static long CalculateTotalWaysOfWinningPart2(string input)
    {
        var waysToWin = 0L;
        
        var records = input.Split('\n', StringSplitOptions.TrimEntries);
        var times = records[0][(records[0].IndexOf(':') + 1)..].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var distances = records[1][(records[1].IndexOf(':') + 1)..].Split(" ", StringSplitOptions.RemoveEmptyEntries);

        var maxTime = CreateLongFromArrayElements(times);
        var maxDistance = CreateLongFromArrayElements(distances);

        for (var i = 1L; i < maxTime; i++)
        {
            var travelTime = maxTime - i;
            var travelDistance = i * travelTime;
            if (travelDistance > maxDistance)
            {
                waysToWin++;
            }
        }

        return waysToWin;

        static long CreateLongFromArrayElements(string[] array)
        {
            var sb = new StringBuilder();
            foreach (var item in array)
            {
                sb.Append(item);
            }
            return long.Parse(sb.ToString());
        }
    }
}
