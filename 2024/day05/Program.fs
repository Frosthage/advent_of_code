open System.IO

let input = File.ReadAllText("input").Split("\n\n")

let pageOrders =
    input[0].Split("\n")
    |> Array.map _.Split('|')
    |> Array.groupBy (fun x -> x[0])
    |> Array.map (fun (key, group) -> key, group |> Array.map (fun item -> item[1]))
    |> Array.map (fun (k,v) -> (k, v |> Set.ofArray ))
    |> Map.ofArray

let rec inRightOrder (s: string list) =
    match s with
    | x::xs ->
        let set = Map.tryFind x pageOrders |> Option.defaultValue Set.empty
        if xs |> List.forall (fun e -> Set.contains e set ) = false then
            false
        else
            inRightOrder xs
    | [] -> true

input[1].Split("\n")
|> Array.map (fun x -> x.Split(',') |> List.ofArray)
|> Array.filter inRightOrder
|> Array.map (fun x -> x[x.Length / 2])
|> Array.sumBy (fun x -> x |> int)
|> printf "Part1: %A\n"

let compare (a:string) (b:string) =
    let set = Map.tryFind a pageOrders |> Option.defaultValue Set.empty
    if set.Contains b then 1 else -1        

input[1].Split("\n")
|> Array.map (fun x -> x.Split(',') |> List.ofArray)
|> Array.filter (fun x -> inRightOrder x |> not)
|> Array.map (fun x -> x |> List.sortWith compare )
|> Array.map (fun x -> x[x.Length / 2])
|> Array.sumBy (fun x -> x |> int)
|> printf "Part2: %A"
