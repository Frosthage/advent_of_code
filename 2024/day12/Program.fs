﻿open System.IO


let input = File.ReadAllLines("example2") |> Array.map _.ToCharArray()

let length = input.Length

let rec buildGraph x y c (nodes: Set<int * int>) =
    if x < 0 || y < 0 || x >= length || y >= length then
        nodes
    else if input[y][x] <> c then
        nodes
    else if Set.contains (x, y) nodes then
        nodes
    else
        let mutable newNodes = Set.add (x, y) nodes
        newNodes <- buildGraph (x + 1) y c newNodes
        newNodes <- buildGraph (x - 1) y c newNodes
        newNodes <- buildGraph x (y + 1) c newNodes
        newNodes <- buildGraph x (y - 1) c newNodes
        newNodes

let countBorders x y (nodes: Set<int * int>) =
    let borderNodes = Set.ofList [ (x + 1, y); (x - 1, y); (x, y + 1); (x, y - 1) ]
    (Set.difference borderNodes nodes).Count

List.allPairs [ 0 .. length - 1 ] [ 0 .. length - 1 ]
|> List.fold
    (fun state (x, y) ->
        let c = input[y][x]
        
        //printfn "%A %A %A" x y c       
        let g = buildGraph x y c Set.empty
        let v = Map.tryFind c state |> Option.defaultValue [ g ]
        Map.add c (g :: v) state)
    Map.empty<char, List<Set<int * int>>>
|> Map.toList
|> List.map (fun (c, s) ->
    let distinctSets = s |> List.distinct

    let count = distinctSets |> List.map _.Count

    let borders =
        distinctSets
        |> List.map (fun x -> x |> Set.toList |> List.map (fun (xx, yy) -> countBorders xx yy x) |> List.sum)

    let mul = List.map2 (fun x y -> x * y) count borders |> List.sum
    (c, mul))
|> List.sumBy snd
|> printfn "Part 1: %A"


List.allPairs [ 0 .. length - 1 ] [ 0 .. length - 1 ]
|> List.fold
    (fun state (x, y) ->
        let c = input[y][x]
        let g = buildGraph x y c Set.empty
        let v = Map.tryFind c state |> Option.defaultValue [ g ]
        Map.add c (g :: v) state)
    Map.empty<char, List<Set<int * int>>>
|> Map.toList
|> List.map (fun (c, s) -> (c, s |> List.distinct))
|> List.map (fun (c, s) ->
    (c,
     s
     |> List.map (fun x ->
         x
         |> Set.toList
         |> List.map (fun (x, y) -> double x, double y)
         |> List.map (fun (x, y) ->
             [ (x - 0.5, y - 0.5)
               (x + 0.5, y - 0.5)
               (x - 0.5, y + 0.5)
               (x + 0.5, y + 0.5) ]))))
|> List.map (fun (c,  es) ->
    let amountOfCorners (corners: (double * double) list) =
        corners
        |> List.groupBy id
        |> List.map (fun (k, v) -> (k, v |> List.length))
        |> List.filter (fun (_, v) -> v = 1 || v = 3)
        //|> List.length

    List.map (fun x -> (c, x, amountOfCorners (x |> List.collect id))) es)
|> List.collect id
//|> List.map (fun (a,b,c) -> (a,b |> List.length, c))
//|> List.map (fun (c, s, es) -> (c, s, es, s * es))
//|> List.sumBy (fun (_, b, c) -> b*c)
|> printfn "Part 2: %A"
