// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

var partNumRegEx = new Regex("([0-9]+)");
var symbolRegEx = new Regex(@"([\#\$\%\&\*/\+\-\=\@])");

// parse part number
PartNumber[] parsePartNumbers(string input, int row)
{
    var matches = partNumRegEx.Matches(input);


    return matches.Select(x =>
    {
        var grp = x.Groups[1];

        var num = int.Parse(grp.Value);

        return new PartNumber
        {
            Number = num,
            Row = row,
            StartIndex = grp.Index,
            EndIndex = grp.Index + grp.Length - 1
        };
    }).ToArray();
}

// parse symbols
PartNumber[] parseSymbols(string input, int row)
{
    var matches = symbolRegEx.Matches(input);


    return matches.Select(x =>
    {
        var grp = x.Groups[1];

        return new PartNumber
        {
            Symbol = grp.Value,
            Row = row,
            StartIndex = grp.Index
        };
    }).ToArray();
}


// define part numbers
var partNumbers = new List<PartNumber>();
var symbols = new List<PartNumber>();

var chars = new List<char>();
var fil = File.ReadLines("input.txt");
int row = 1;
foreach (var line in fil)
{
    //Console.WriteLine(line);

    var dis = line.ToCharArray().Distinct();
    chars.AddRange(dis.Where(x => !chars.Contains(x)));

    var _partNumbers = parsePartNumbers(line, row);
    var _symbols = parseSymbols(line, row);
    partNumbers.AddRange(_partNumbers);
    symbols.AddRange(_symbols);

    //var x = String.Join(",", _partNumbers.Select(x => $"{x.Number}-{x.StartIndex}-{x.EndIndex}").ToList());
    var x = String.Join(",", _symbols.Select(x => $"{x.Symbol}-{x.StartIndex}").ToList());
    ////Console.WriteLine($" =====> {x}");
    row++;
}

// get possible
(int, int)[] findPossible(PartNumber partNumber)
{
    // get the possible row, index values
    var possible = new List<(int, int)>();

    // above, below
    for (var i = partNumber.StartIndex - 1; i <= partNumber.EndIndex + 1; i++)
    {
        possible.Add((partNumber.Row - 1, i));
        possible.Add((partNumber.Row + 1, i));
    }

    // left, right
    possible.Add((partNumber.Row, partNumber.StartIndex - 1));
    possible.Add((partNumber.Row, partNumber.EndIndex + 1));

    // return 
    return possible.ToArray();
}

// go through parts
var matchedParts = new List<int>();
var matchedTotal = 0;
foreach (var partNumber in partNumbers)
{
    // get the possible row, index values
    var possible = findPossible(partNumber);

    // show
    /*
    Console.WriteLine($"Checking possible: {partNumber.Number} at Row {partNumber.Row}, {partNumber.StartIndex}=>{partNumber.EndIndex}");
    foreach ((int r, int i) in possible)
    {
        Console.WriteLine($" ==> {r}, {i}");
    }*/

    // check if a possible match
    foreach ((int r, int i) in possible)
    {
        if (symbols.Any(x => x.Row == r && x.StartIndex == i))
        {
            Console.WriteLine(" ==> MATCH!");
            matchedParts.Add(partNumber.Number);
            matchedTotal += partNumber.Number;

        }
        else
        {
            Console.WriteLine(" ==> No MATCH!");
        }
    }
}




var gearRatioTotal = 0;

// go through all *
foreach (var sym in symbols.Where(x => x.Symbol == "*"))
{
    // matched parts
    var gearMatchParts = new List<PartNumber>();
    foreach (var part in partNumbers)
    {
        var possible = findPossible(part);
        if (possible.Any(x => x.Item1 == sym.Row && x.Item2 == sym.StartIndex))
        {
            Console.WriteLine(" ==> GEAR MATCH!");
            gearMatchParts.Add(part);
        }
        else
        {
            Console.WriteLine(" ==> No MATCH!");
        }
    }

    if (gearMatchParts.Count > 1)
    {
        Console.WriteLine(" ==> IS GEAR RATIO!");
        int res = 1;
        foreach (var g in gearMatchParts)
            res = res * g.Number;
        gearRatioTotal += res;
    }
}

// show results
Console.WriteLine($"Day 3, Part 1: {matchedTotal}");
Console.WriteLine($"Day 3, Part 2: {gearRatioTotal}");


class PartNumber
{
    public int Number { get; set; }
    public string Symbol { get; set; }

    public int Row { get; set; }
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
}