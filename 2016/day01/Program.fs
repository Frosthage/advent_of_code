
open System.IO

let mutable input =
    File.ReadAllText("input").Split(", ")

type Moved = {
    Position: int*int
    Direction: int*int    
}

let rotate (x,y) t=
    match t with
    | 'L' -> (-y,x)
    | 'R' -> (y, -x)
    | _ -> failwith $"not supported {t}"
    

({Position= (0,0); Direction=(0,1)}, input)
||> Array.fold (fun acc xx ->
    let turned = xx[0]
    let distance = int xx[1..] 
    let nx, ny = rotate (acc.Direction) turned
    let px, py = acc.Position
    let r = { Position=(px + nx * distance, py + ny * distance);Direction=(nx,ny)}
    r
    )
|> (fun x ->
    abs (fst x.Position) + abs (snd x.Position)
    )
|> printfn "Part 1: %A"

    
let positions =
    ({Position= (0,0); Direction=(0,1)}, input)
    ||> Array.scan (fun acc xx ->
        let turned = xx[0]
        let distance = int xx[1..] 
        let nx, ny = rotate (acc.Direction) turned
        let px, py = acc.Position
        let r = { Position=(px + nx * distance, py + ny * distance);Direction=(nx,ny)}
        r
        )
    |> Array.map _.Position
    |> Array.windowed 2
    |> Array.map (fun p ->
        let x1, y1 = p[0]
        let x2, y2 = p[1]
        let result = if x1 = x2 then
                         if y2 > y1 then
                             [|y1..y2|] |> Array.map (fun y -> (x1, y) )
                         else
                             [|y1.. -1..y2|] |> Array.map (fun y -> (x1, y) )
                     else
                         if x2 > x1 then
                             [|x1..x2|] |> Array.map (fun x -> (x, y1) )
                         else
                             [|x1.. -1..x2|] |> Array.map (fun x -> (x, y1) )
        result[0..result.Length-2]
    )
    |> Array.collect id

positions
|> Array.groupBy id
|> Array.find (fun (_,v) -> v |> Seq.length > 1)
|> fst
|> (fun (x,y) ->
    printfn "Part 2: %A" (abs x + abs y)
)
    

