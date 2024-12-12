open System.IO

let input =
    File.ReadAllLines("input")
    |> Array.map (fun x -> x.ToCharArray() |> Array.map (fun x -> x.ToString() |> int))

let max = Array.length input

let positions = Array.allPairs [|0..max-1|] [|0..max-1|]

let findNext (x,y) height =
    [(x-1, y); (x, y-1); (x+1, y); (x,y+1)]
    |> Array.ofList
    |> Array.filter (fun (x,y) -> x >=0 && x<max && y>=0 && y<max)
    |> Array.map (fun (x,y) -> ((x,y), input[y][x]))
    |> Array.filter (fun (_, h) -> h = height)
    |> Array.map fst
                         
let rec countTrailheads (x,y) =
    let height = input[y][x]
    let next = findNext (x,y) (height+1)
    if height = 9 then
        [|(x,y)|]
    else if (next |> Array.length) = 0 then
        [||]
    else
        next
        |> Array.map countTrailheads
        |> Array.collect id
        
positions
|> Array.filter (fun (x,y) -> input[y][x] = 0)
|> Array.sumBy (fun x -> countTrailheads x |> Array.distinct |> Array.length)
|> printf "Part1: %A"  
        
positions
|> Array.filter (fun (x,y) -> input[y][x] = 0)
|> Array.sumBy (fun x -> countTrailheads x |> Array.length)
|> printf "\nPart2: %A"   