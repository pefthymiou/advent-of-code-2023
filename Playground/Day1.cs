/*
 * --- Day 1: Trebuchet?! ---
 * You try to ask why they can't just use a weather machine ("not powerful enough") and where they're even sending you ("the sky") and why your map looks mostly blank ("you sure ask a lot of questions") and hang on did you just say the sky ("of course, where do you think snow comes from") when you realize that the Elves are already loading you into a trebuchet ("please hold still, we need to strap you in").
 * As they're making the final adjustments, they discover that their calibration document (your puzzle input) has been amended by a very young Elf who was apparently just excited to show off her art skills. 
 * Consequently, the Elves are having trouble reading the values on the document.
 * The newly-improved calibration document consists of lines of text; each line originally contained a specific calibration value that the Elves now need to recover. 
 * On each line, the calibration value can be found by combining the first digit and the last digit (in that order) to form a single two-digit number.
 * 
 * Consider your entire calibration document. 
 * 
 * What is the sum of all of the calibration values?
 * 
 * Puzzle answer was 54239 (for personal GitHub account).
 * 
 * --- Part Two ---
 * Your calculation isn't quite right. 
 * It looks like some of the digits are actually spelled out with letters: one, two, three, four, five, six, seven, eight, and nine also count as valid "digits".
 * 
 * What is the sum of all of the calibration values?
 * 
 * Your puzzle answer was 55343 (for personal GitHub account).
 * 
 * https://adventofcode.com/2023/day/1
 */

using System.Buffers;

namespace Playground;

internal sealed class Day1
{
    private readonly SearchValues<char> _digitsSearchValues = SearchValues.Create("123456789");
    private readonly Dictionary<string, int> _digits = new()
    {
        ["one"] = 1,
        ["two"] = 2,
        ["three"] = 3,
        ["four"] = 4,
        ["five"] = 5,
        ["six"] = 6,
        ["seven"] = 7,
        ["eight"] = 8,
        ["nine"] = 9,
        ["1"] = 1,
        ["2"] = 2,
        ["3"] = 3,
        ["4"] = 4,
        ["5"] = 5,
        ["6"] = 6,
        ["7"] = 7,
        ["8"] = 8,
        ["9"] = 9,
    };

    internal int CalculateSumOfCalibrationValuesPart1(string text)
    {
        int total = 0;

        string[] lines = text.Split('\n');

        foreach (var line in lines)
        {
            total += ParseDigitsPart1(line);
        }

        return total;
    }

    internal int CalculateSumOfCalibrationValuesPart2(string text)
    {
        int total = 0;

        string[] lines = text.Split('\n');

        foreach (var line in lines)
        {
            total += ParseDigitsPart2(line);
        }

        return total;
    }

    private int ParseDigitsPart1(string line)
    {
        var lineAsSpan = line.AsSpan();
        var firstIndex = lineAsSpan.IndexOfAny(_digitsSearchValues);
        var lastIndex = lineAsSpan.LastIndexOfAny(_digitsSearchValues);

        if (lastIndex == -1)
        {
            lastIndex = firstIndex;
        }

        return Convert.ToInt32($"{lineAsSpan[firstIndex]}{lineAsSpan[lastIndex]}");
    }

    private int ParseDigitsPart2(string input)
    {
        var firstDigit = 0;
        var lastDigit = 0;
        var firstIndex = input.Length;
        var lastIndex = -1;

        foreach (var digit in _digits)
        {
            var index = input.IndexOf(digit.Key);
            if (index < 0)
            {
                continue;
            }

            if (index < firstIndex)
            {
                firstIndex = index;
                firstDigit = digit.Value;
            }

            index = input.LastIndexOf(digit.Key);
            if (index > lastIndex)
            {
                lastIndex = index;
                lastDigit = digit.Value;
            }
        }

        return firstDigit * 10 + lastDigit;
    }
}
