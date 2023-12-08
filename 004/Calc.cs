using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public class Calc
{
    Regex cardRegEx = new Regex(@"Card[\s]+([0-9]+): ([0-9\s]+)[|]([0-9\s]+)");

    string[] lines;

    // exposed
    public Card[] Cards { get; set; }
    public List<int> Matches { get; set; } = new List<int>();

    public Calc(string input)
    {
        lines = File.ReadAllLines(input);
    }

    // parse numbers
    public int[] ParseNumbers(string input)
    {
        if (String.IsNullOrEmpty(input))
            return new int[] { };

        return input.Trim().Split(" ").Where(x => !String.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToArray();
    }

    // calc score
    public void CalcScore(Card card)
    {
        int score = 0;

        foreach (var n in card.CardNumbers)
        {
            if (card.WinningNumbers.Contains(n))
            {
                if (score == 0)
                    score++;
                else
                    score = score * 2;

            }
        }

        card.Score = score;
    }

    // total score
    public int TotalScore()
    {
        return Cards.Select(x => x.Score).Sum();
    }

    public int CalcPart2()
    {
        int total = 0;

        for (int i = 0; i < Cards.Length; i++)
        {
            total += CalcPart2(Cards[i], i);
            total++;
            Matches.Add(i + 1);
        }

        return total;
    }

    public int CalcPart2(Card card, int index)
    {
        int x = 0;

        foreach (var n in card.CardNumbers)
        {
            if (card.WinningNumbers.Contains(n))
            {
                x++;
            }
        }


        int total = 0;
        for (int i = 1; i <= x; i++)
        {
            if (index + i < Cards.Length)
            {
                total += CalcPart2(Cards[index + i], index + i);
                total++;
                Matches.Add(index + i + 1);
            }
        }

        return total;
    }

    // run
    public void Run()
    {
        var cards = new List<Card>();

        foreach (var line in lines)
        {
            var match = cardRegEx.Match(line);

            var card = new Card
            {
                Id = int.Parse(match.Groups[1].Value),
                Score = -1,
                WinningNumbers = ParseNumbers(match.Groups[2].Value),
                CardNumbers = ParseNumbers(match.Groups[3].Value)
            };
            CalcScore(card);
            cards.Add(card);
        }



        // set
        Cards = cards.ToArray();
    }
}

public class Card
{
    public string Line { get; set; }
    public int Id { get; set; }
    public int Score { get; set; }
    public int[] WinningNumbers { get; set; }
    public int[] CardNumbers { get; set; }
}