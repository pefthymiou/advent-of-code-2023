namespace Playground;

public class Day7
{
    internal static long CalculateTotalWinningsPart1(string input)
    {
        var totalWinnings = 0L;
        var hands = input
            .Split('\n', StringSplitOptions.TrimEntries)
            .Select(s =>
            {
                var parts = s.Split(' ');
                return new Hand(parts[0], int.Parse(parts[1]), false);
            })
            .ToList();

        hands.Sort();

        for (int i = 0; i < hands.Count; i++)
        {
            totalWinnings += hands[i].Bid * (i + 1);
        }

        return totalWinnings;
    }
}

public sealed class Hand : IComparable<Hand>
{
    public string Cards { get; }
    public int Bid { get; }
    public HandType Type { get; }
    public bool JokersEnabled { get; }

    public Hand(string cards, int bid, bool jokersEnabled)
    {
        Cards = cards;
        Bid = bid;
        Type = GetHandType(cards.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count()));
        JokersEnabled = jokersEnabled;
    }

    public int CompareTo(Hand? other)
    {
        string cardOrder = JokersEnabled ? "J23456789TQKA" : "23456789TJQKA";

        if ((int)Type > (int)other!.Type)
        {
            return 1;
        }

        if ((int)Type < (int)other.Type)
        {
            return -1;
        }

        for (int i = 0; i < Cards.Length; i++)
        {
            if (cardOrder.IndexOf(Cards[i]) > cardOrder.IndexOf(other.Cards[i]))
            {
                return 1;
            }

            if (cardOrder.IndexOf(Cards[i]) < cardOrder.IndexOf(other.Cards[i]))
            {
                return -1;
            }
        }

        return 0;
    }

    private static HandType GetHandType(Dictionary<char, int> cardCounts)
    {
        bool hasPair = false, hasThree = false;
        foreach (var count in cardCounts.Values)
        {
            if (count == 5)
            {
                return HandType.FiveOfAKind;
            }

            if (count == 4)
            {
                return HandType.FourOfAKind;
            }

            if (count == 3) hasThree = true;
            if (count == 2) hasPair = true;
        }

        if (hasThree && hasPair)
        {
            return HandType.FullHouse;
        }

        if (hasThree)
        {
            return HandType.ThreeOfAKind;
        }

        if (hasPair)
        {
            return cardCounts.Count(c => c.Value == 2) == 2 ? HandType.TwoPair : HandType.OnePair;
        }

        return HandType.HighCard;
    }
}

public enum HandType
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind
}