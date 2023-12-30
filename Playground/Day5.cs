namespace Playground;

internal class Day5
{
    internal static long CalculateLowestLocationNumber(List<string> input)
    {
        var seeds = input[0].Split(" ").Skip(1).Select(long.Parse).ToArray();
        var mapIds = input.Where(x => x.Contains("map")).ToArray();
        var maps = mapIds.Select(mapId => ParseMap(input, mapId)).ToList();
        return seeds.Select(seed => maps.Aggregate(seed, ProcessMap)).Prepend(long.MaxValue).Min();
    }

    internal static long CalculateLowestLocationNumberFromSeedRange(List<string> input)
    {
        var lowestLocationNumber = long.MaxValue;
        var ranges = ParseSeedRanges(input[0].Split(" ").Skip(1).Select(long.Parse).ToArray());
        var mapIds = input.Where(x => x.Contains("map")).ToArray();
        var maps = mapIds.Select(mapId => ParseMap(input, mapId)).ToList();

        foreach (var range in ranges)
        {
            for (var i = 0; i < range.Item2; i++)
            {
                var currentSeed = range.Item1 + i;
                currentSeed = maps.Aggregate(currentSeed, ProcessMap);
                lowestLocationNumber = Math.Min(lowestLocationNumber, currentSeed);
            }
        }

        return lowestLocationNumber;
    }

    private static List<(long, long)> ParseSeedRanges(IReadOnlyList<long> input)
    {
        List<(long, long)> ranges = [];

        for (var i = 0; i < input.Count; i += 2)
        {
            if (i + 1 < input.Count)
            {
                ranges.Add((input[i], input[i + 1]));
            }
        }
        return ranges;
    }

    private static List<(long, long, long)> ParseMap(List<string> input, string mapId)
    {
        List<(long, long, long)> map = [];

        var start = input.IndexOf(mapId) + 1;
        var tempList = input.GetRange(start, input.Count - start);
        var end = tempList.FindIndex(x => x.Contains("map"));
        var count = end == -1 ? tempList.Count : end - 1;
        var lines = tempList.GetRange(0, count);

        map.AddRange(lines.Select(line => line.Split(' '))
            .Select(parts => (long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2]))));

        return map;
    }

    private static long ProcessMap(long seed, List<(long, long, long)> map)
    {
        foreach (var entry in map.Where(entry => seed >= entry.Item2 && seed < entry.Item2 + entry.Item3))
        {
            return entry.Item1 + (seed - entry.Item2);
        }

        return seed;
    }
}
