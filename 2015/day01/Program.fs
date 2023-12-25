
open System.IO

File.ReadAllText("input").ToCharArray()
|> Array.fold (fun acc c -> if c = '(' then acc + 1 else acc - 1) 0
|> printfn "Part 1: %d"


File.ReadAllText("input").ToCharArray()
|> Array.scan (fun acc c -> if c = '(' then acc + 1 else acc - 1) 0
|> Array.findIndex (fun x -> x = -1)
|> printfn "Part 2: %d"





