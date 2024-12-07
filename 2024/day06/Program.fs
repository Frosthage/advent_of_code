open System.IO

let input = File.ReadAllLines("input") |> Array.map _.ToCharArray()

let maxY = input.Length
let maxX = input[0].Length

let rotate (x: int, y: int) =
    match (x, y) with
    | 1, 0 -> (0, 1)
    | 0, 1 -> (-1, 0)
    | -1, 0 -> (0, -1)
    | 0, -1 -> (1, 0)
    | _ -> failwith "todo"

let startPos =
    input
    |> Array.mapi (fun i y -> y |> Array.mapi (fun ii c -> ((ii, i), c)))
    |> Array.collect id
    |> Array.filter (fun ((x, y), c) -> c = '^')
    |> Array.map fst


let rec traverse x y (dx, dy) (steps: int) (visits: Set<int * int>) =
    let nx = x + dx
    let ny = y + dy
    let inc = if visits.Contains(x, y) then 0 else 1
    let newVisits = visits.Add(x, y)

    match (nx, ny) with
    | -1, _ -> steps
    | _, -1 -> steps
    | nx, ny when (nx = maxX || ny = maxY) -> steps
    | nx, ny ->
        let c = input[ny][nx]

        if c = '#' then
            let newDx, newDy = rotate (dx, dy)
            traverse (x + newDx) (y + newDy) (newDx, newDy) (steps + inc) newVisits
        else
            traverse nx ny (dx, dy) (steps + inc) newVisits

let x, y = startPos[0]

printf "Part1: %A\n" (traverse x y (0, -1) 1 Set.empty)

let rec traverse2 x y (dx, dy) (rx, ry) (visits: Set<int * int * int * int>) =
    let newVisits = Set.add (x, y, dx, dy) visits
    match (x,y) with
    | -1, _ -> false
    | _, -1 -> false
    | x, y when (x = maxX || y = maxY) -> false
    | x,y when Set.contains(x,y,dx,dy) visits -> true
    | x,y ->
        let c = input[y][x]
        if c = '#' || (x,y) = (rx, ry) then
            let dx2, dy2 = (dx,dy) |> rotate |> rotate
            let nx, ny = (x+dx2, y+dy2)
            let dx3, dy3 = (dx, dy) |> rotate
            traverse2 (nx+dx3) (ny+dy3) (dx3, dy3) (rx, ry) newVisits
        else
            traverse2 (x+dx) (y+dy) (dx,dy) (rx,ry) newVisits
            
            
let rec trail x y (dx, dy) (visits: Set<int * int>) =
    let nx = x + dx
    let ny = y + dy
    let newVisits = visits.Add(x, y)

    match (nx, ny) with
    | -1, _ -> newVisits
    | _, -1 -> newVisits
    | nx, ny when (nx = maxX || ny = maxY) -> newVisits
    | nx, ny ->
        let c = input[ny][nx]

        if c = '#' then
            let newDx, newDy = rotate (dx, dy)
            trail (x + newDx) (y + newDy) (newDx, newDy) newVisits
        else
            trail nx ny (dx, dy) newVisits

(trail x y (0,-1) Set.empty)
|> Set.toArray
|> Array.map (fun (x,y) -> ((x,y), input[y][x]))
|> Array.filter (fun xx -> (fst xx) <> (x,y))
|> Array.map fst
|> Array.Parallel.filter (fun (rx, ry) -> traverse2 x y (0, -1) (rx, ry) Set.empty)
|> Array.length
|> printf "Part2: %A"

