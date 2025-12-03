using System.Runtime.InteropServices.JavaScript;

var file = "input";

var parseRanges = File
    .ReadAllText(file)
    .Split(',')
    .Select(x =>
    {
        var parts = x.Split('-');
        long start = long.Parse(parts[0]);
        long end = long.Parse(parts[1]);
        return ULongRange(start, end);
    })
    .SelectMany(x => x)
    .ToList();

var part1 = parseRanges.Where(x =>
    {
        string s = x.ToString();
        if (s.Length % 2 == 1) return false;

        return s[..(s.Length / 2)] == s[(s.Length / 2)..];
    })
    .Sum();

Console.WriteLine($"Part1: {part1}");

var part2 = parseRanges.Where(x =>
    {
        var str = x.ToString();
        for (int i = 1; i <= str.Length / 2; i++)
        {
            var apa = str.Chunk(i).Select(x => new string(x)).ToList();
            if (apa.All(x => x == apa[0]))
            {
                //Console.WriteLine(str);
                return true;
            }
        }

        return false;
    })
    .Sum();

Console.WriteLine($"Part2: {part2}");

static IEnumerable<long> ULongRange(long start, long endInclusive)
{
    for (long i = start; i <= endInclusive; i++)
    {
        yield return i;
    }
}