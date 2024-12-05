open System.IO

let input = File.ReadAllLines("input") |> Array.map _.ToCharArray()

let xMax = input[0].Length
let yMax = input.Length

let rec getString (x, y) (dx, dy) str depth =
    match (x, y) with
    | -1, _ -> ""
    | _, -1 -> ""
    | x, y when (x = xMax || y = yMax) -> ""
    | x, y ->
        let c = input[y][x]

        if depth = 3 then
            $"{str}{c}"
        else
            getString (x + dx, y + dy) (dx, dy) $"{str}{c}" (depth + 1)

let allDirections =
    [ (1, 0); (0, 1); (-1, 0); (0, -1); (1, 1); (-1, -1); (-1, 1); (1, -1) ]

input
|> Array.mapi (fun iy col -> col |> Array.mapi (fun ix _ -> (iy, ix)))
|> Array.collect id
|> Array.sumBy (fun (x, y) ->
    allDirections
    |> List.filter (fun d -> (getString (x, y) d "" 0) = "XMAS")
    //|> List.map (fun d -> printfn "%A %A %A" x y d)
    |> List.length)
|> printf "Part1 %A\n"


let getX x y = 
    match (x,y) with
    | 0, _ -> ("", "")
    | _, 0 -> ("", "")
    | x, y when (x = xMax-1 || y = yMax-1) -> ("", "")
    | x, y -> ($"{input[y-1][x-1]}{input[y][x]}{input[y+1][x+1]}",$"{input[y+1][x-1]}{input[y][x]}{input[y-1][x+1]}")
    
input
|> Array.mapi (fun iy col -> col |> Array.mapi (fun ix _ -> (iy, ix)))
|> Array.collect id
|> Array.map (fun (x,y)-> ((x,y), getX x y))
|> Array.filter (fun (_, (a,b)) -> (a = "MAS" || a = "SAM") && (b = "MAS" || b = "SAM"))
|> Array.length
|> printf "Part2: %A"


 





