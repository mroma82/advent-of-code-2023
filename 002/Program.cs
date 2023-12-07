
// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");



Game parseGame(string s)
{    
    var m = new Regex("Game ([0-9]+):(.*)").Match(s);
    var blueRegEx = new Regex("([0-9]+) blue");
    var redRegEx = new Regex("([0-9]+) red");
    var greenRegEx = new Regex("([0-9]+) green");

    var id=m.Groups[1].Value;
    var grabs =m.Groups[2].Value;

    var tmp = new List<Grab>();
    foreach(var grab in grabs.Split(';'))
    {
        var grabo = new Grab{ Content = grab};

        Console.WriteLine($" ===> Grab: {grab}");

        var mm = blueRegEx.Match(grab);
        if(mm.Success) {
            Console.WriteLine($" =====> Blue: {mm.Groups[1].Value}");
            grabo.Blue = int.Parse(mm.Groups[1].Value);
        }
        mm = redRegEx.Match(grab);
        if(mm.Success){
            Console.WriteLine($" =====> Red: {mm.Groups[1].Value}");
            grabo.Red = int.Parse(mm.Groups[1].Value);
        }
        mm = greenRegEx.Match(grab);
        if(mm.Success) {
            Console.WriteLine($" =====> Green: {mm.Groups[1].Value}");
            grabo.Green = int.Parse(mm.Groups[1].Value);
        }

        tmp.Add(grabo);
    }


    return new Game{ Id = int.Parse(id), Grabs = tmp.ToArray()};
}

bool isPossible(Game g)
{
    return !g.Grabs.Any(grab => grab.Red > 12 || grab.Green > 13 || grab.Blue > 14);
}

(int b, int r, int g) findMinReq(Game g)
{
    return (
        g.Grabs.Max(x => x.Blue),
        g.Grabs.Max(x => x.Red),
        g.Grabs.Max(x => x.Green)
    );
}

int possibleTotal = 0;
int powerMinReq = 0;

foreach(var x in File.ReadLines("input.txt"))
{
    Console.WriteLine(x);
    var g = parseGame(x);

    Console.WriteLine($" => {g.Id}");
    if(isPossible(g))
    {
        Console.WriteLine(" ==> Is Possible");
        possibleTotal += g.Id;
    }

    (int minBlue, int minRed, int minGreen) = findMinReq(g);
    Console.WriteLine($" ==> MinReq: {minBlue}, {minRed}, {minGreen}");
    powerMinReq += (minBlue * minRed * minGreen);
}

Console.WriteLine($"Possible Game Id Total: {possibleTotal}");
Console.WriteLine($"Power Min Req: {powerMinReq}");

public class Game
{
    public int Id {get;set;}    
    public Grab[] Grabs{get;set;}
}

public class Grab
{
    public string Content {get;set;}
    public int Red{get;set;}
    public int Blue{get;set;}
    public int Green{get;set;}
}