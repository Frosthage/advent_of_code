open System.IO
open System.Text.RegularExpressions

let input = File.ReadAllText("input")

Regex.Matches(input, @"mul\((\d+),(\d+)\)")
|> Seq.map (fun x -> (x.Groups[1].Value, x.Groups[2].Value))
|> Seq.map (fun (a, b) -> int (a) * int (b))
|> Seq.sum
|> printf "%A\n"


let dosAndDonts =
    Regex.Matches(input, "(don't\(\)|do\(\))")
    |> Seq.map (fun x -> (x.Index, x.Groups[1].Value))
    |> Seq.insertAt 0 (0, "do()")
    |> Seq.toList
    
let lastInstruction(i: int): string =
    dosAndDonts
    |> List.takeWhile (fun (ii, _) -> ii - i < 0)
    |> List.map snd
    |> List.last

Regex.Matches(input, @"mul\((\d+),(\d+)\)")
|> Seq.map (fun x -> (x.Index, x.Groups[1].Value, x.Groups[2].Value))
|> Seq.filter (fun (i, _, _) -> lastInstruction i = "do()")
|> Seq.map (fun (_, a, b) -> int a * int b)
|> Seq.sum
|> printf "%A"
