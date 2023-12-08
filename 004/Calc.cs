using System.Text.RegularExpressions;

public class Calc
{
    Regex cardRegEx = new Regex(@"Card[\s]+([0-9]+): ([0-9\s]+)[|]([0-9\s]+)");

    string[] lines;

    // exposed
    public Card[] Cards { get; set; }

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