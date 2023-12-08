// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


var testCalc = new Calc("test.txt");
testCalc.Run();

// check
foreach (var card in testCalc.Cards)
{
    Console.WriteLine($"{card.Id} = {card.Score} - {card.Line}");
    Console.WriteLine($" ===> Winning: {String.Join(",", card.WinningNumbers)}");
    Console.WriteLine($" ===> Card: {String.Join(",", card.CardNumbers)}");
}

Console.WriteLine($"Test Total: {testCalc.TotalScore()}");

// run
var calc = new Calc("input.txt");
calc.Run();

Console.WriteLine($"Day 4, Part 1: {calc.TotalScore()}");