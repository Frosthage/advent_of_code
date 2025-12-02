// See https://aka.ms/new-console-template for more information


var file = "input";
//var file = "example";

var rotations = File.ReadAllLines(file)
    .Select(x => new Rotation(Enum.Parse<Direction>(x[..1], true), int.Parse(x[1..])))
    .ToList();


var newPos = 50;
var zeros = 0;

foreach (var rotation in rotations)
{
    var pos = rotation.Direction == Direction.L ? newPos - rotation.Amount : newPos + rotation.Amount;
    newPos = (pos % 100 + 100) % 100;
    if (newPos == 0) zeros++;
}


Console.WriteLine($"Part1: {zeros}");


newPos = 50;
zeros = 0;
foreach (var rotation in rotations)
{
    var times = (rotation.Amount - rotation.Amount % 100) / 100;
    var pos = rotation.Direction == Direction.L
        ? newPos - rotation.Amount
        : newPos + rotation.Amount;
    zeros += times;

    var amount = rotation.Amount - times * 100;

    if (pos % 100 == 0)
    {
        zeros++;
    }
    else if (newPos != 0 && rotation.Direction == Direction.L && newPos < amount)
    {
        zeros++;
    }
    else if (newPos != 0 && rotation.Direction == Direction.R && newPos + amount > 100)
    {
        zeros++;
    }

    pos = (pos % 100 + 100) % 100;
    newPos = pos;
}


Console.WriteLine($"Part1: {zeros}");


enum Direction
{
    L,
    R
}


record Rotation(Direction Direction, int Amount);