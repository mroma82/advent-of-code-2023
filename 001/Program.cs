// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var getFirstDigitPart1 = new Func<string, int, int>((s, dir) =>
{

    foreach (char x in dir < 0 ? s.Reverse() : s.ToCharArray())
    {
        if (char.IsDigit(x))
            return int.Parse(x.ToString());
    }
    return 0;
});

var getFirstDigitPart2 = new Func<string, int, int>((s, dir) =>
{

    
    var arr = dir < 0 ? s.Reverse().ToArray() : s.ToCharArray();
    for (var i = 0; i < arr.Count(); i++)
    {
        var ch = arr[i];
        if (char.IsDigit(ch))
            return int.Parse(ch.ToString());

        var sub = dir > 0 ? s.Substring(i).ToLower() : s.Substring(s.Length - i -1).ToLower();
        foreach (var n in Enumerable.Range(0, 10))
        {
            var chk = "";
            switch (n)
            {
                case 1: chk = "one"; break;
                case 2: chk = "two"; break;
                case 3: chk = "three"; break;
                case 4: chk = "four"; break;
                case 5: chk = "five"; break;
                case 6: chk = "six"; break;
                case 7: chk = "seven"; break;
                case 8: chk = "eight"; break;
                case 9: chk = "nine"; break;
            }

            if(chk != "")
            {
                if(sub.StartsWith(chk))
                {
                    return n;
                }
            }
        }
    }
    return 0;
});

var fil = File.ReadLines("input.txt");

Console.WriteLine(fil.Count());
int total = 0;
foreach (var line in fil)
{
    int first = getFirstDigitPart2(line, 1);
    int last = getFirstDigitPart2(line, -1);
    total += first * 10 + last;
    Console.WriteLine(line);
    Console.WriteLine($"==> {first}, {last}");
}

Console.WriteLine($"Total = {total}");