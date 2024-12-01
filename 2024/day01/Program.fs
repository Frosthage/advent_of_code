open System.IO

let input = File.ReadAllLines("input")

let parse (lines: string array) =
    let parsed = lines |> Array.map _.Split("   ") |> Array.transpose
    (Array.map int parsed[0], Array.map int parsed[1])

let p = parse input
let sorted_c1 = fst p |> Array.sort
let sorted_c2 = snd p |> Array.sort

(Array.zip sorted_c1 sorted_c2)
|> Array.map (fun (a, b) -> abs (int a - int b))
|> Array.sum
|> printf "Part1: %A\n"

let c2_map =
    sorted_c2
    |> Array.groupBy id
    |> Array.map (fun (k, v) -> (k, v |> Array.length))
    |> Map.ofArray

sorted_c1
|> Array.map (fun x -> Map.tryFind x c2_map |> Option.defaultValue 0 |> (*) x)
|> Array.sum
|> printf "Part2: %A\n"
