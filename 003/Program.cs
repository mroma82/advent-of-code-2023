// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

var partNumRegEx = new Regex("([0-9]+)");

// define part numbers
var partNumbers = new List<PartNumber>();
var symbols = new List<PartNumber>();
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
            EndIndex = grp.Length
        };
    }).ToArray();

}

var chars = new List<char>();
var fil = File.ReadLines("input.txt");
int row = 1;
foreach (var line in fil)
{
    Console.WriteLine(line);

    var dis = line.ToCharArray().Distinct();
    chars.AddRange(dis.Where(x => !chars.Contains(x)));

    var _partNumbers = parsePartNumbers(line, row);
    var x = String.Join(",", _partNumbers.Select(x => $"{x.Number}-{x.StartIndex}-{x.EndIndex}").ToList());
    partNumbers.AddRange(_partNumbers);
    Console.WriteLine($" =====> {x}");
    row++;
}

chars.Sort();
Console.WriteLine(chars.ToArray());

class PartNumber
{
    public int Number { get; set; }

    public int Row { get; set; }
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
}