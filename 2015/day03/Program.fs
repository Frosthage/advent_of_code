
open System.IO

let deliver x =
    x
    |> Array.scan (fun acc c ->
        let x,y = acc
        match c with
        | '>' -> (x+1,y)
        | '<' -> (x-1,y)
        | '^' -> (x,y+1)
        | 'v' -> (x,y-1)
        | _ -> raise (System.Exception("Not implemented"))
    ) (0,0)
    |> Set.ofArray

File.ReadAllText("input").ToCharArray()
|> deliver
|> Set.count
|> printfn "Part1: %A"

let santa, robo = File.ReadAllText("input").ToCharArray()
                  |> Array.mapi (fun i c -> (i, c))
                  |> Array.partition (fun (i,_) -> i % 2 = 0)
                                

santa
|> Array.map snd
|> deliver
|> Set.union (robo |> Array.map snd |> deliver)
|> Set.count
|> printfn "Part2: %A"


