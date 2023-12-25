
open System
open System.IO

File.ReadAllLines("input")
|> Array.map (_.Split('x', StringSplitOptions.RemoveEmptyEntries))
|> Array.map (fun a -> (int a[0], int a[1], int a[2]))
|> Array.map (fun (l,v,g) -> 2*l*v + 2*v*g + 2*g*l + min (min (l*v) (v*g)) (g*l))
|> Array.sum
|> printfn "Part 1: %A"

File.ReadAllLines("input")
|> Array.map (_.Split('x', StringSplitOptions.RemoveEmptyEntries))
|> Array.map (Array.map int)
|> Array.map Array.sort
|> Array.map (fun x -> x.[0] * 2 + x.[1] * 2 + Array.reduce (*) x)
|> Array.sum
|> printfn "Part 2: %A"






