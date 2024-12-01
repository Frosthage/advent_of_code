
open System.IO

let vowels = Set.ofArray ("aeiou".ToCharArray())



match [1] with
| x::y::z -> printfn "%A %A %A" x y z
| _ -> printfn "no match"


let exclude = ["ab"; "cd"; "pq"; "xy"]

File.ReadAllLines("input")
|> Array.filter (fun x -> x.ToCharArray() |> Array.filter vowels.Contains |> Array.length >= 3)
|> Array.filter (fun x -> x.ToCharArray() |> Array.pairwise |> Array.exists (fun (a, b) -> a = b))
|> Array.filter (fun x -> exclude |> List.exists x.Contains |> not)
|> Array.length
|> printfn "Part1 %d"

let hasPairTwice(str: string): bool =
    str.ToCharArray()
    |> Array.windowed 2
    |> Array.map (fun x -> System.String(x))
    |> Array.exists (fun x -> abs(str.IndexOf(System.String(x)) - str.LastIndexOf(System.String(x))) > 1)
    

File.ReadAllLines("input2")
|> Array.filter (fun x -> x.ToCharArray() |> Array.windowed 3 |> Array.exists (fun x -> x[0] = x[2]))
|> Array.filter hasPairTwice
|> Array.length
|> printfn "Part2: %A\n"



