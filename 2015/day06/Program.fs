open System
open System.IO
open System.Text.RegularExpressions


let lights = Array2D.create 1000 1000 0

let input =
    File.ReadAllLines("input")
    |> Array.map (fun x ->
        Regex.Match(x, @"(.+?) (\d+),(\d+).* (\d+),(\d+)"))
    |> Array.map (fun x -> (x.Groups[1].Value, x.Groups[2].Value |> int, x.Groups[3].Value |> int, x.Groups[4].Value |> int, x.Groups[5].Value |> int))
    |> Array.map (fun x ->
        let cmd, x1, y1, x2, y2                       = x
        match cmd with
        | "turn on" -> List.allPairs [x1..x2] [y1..y2] |> List.iter (fun (x,y) -> lights[y,x]<-1)
        | "turn off" -> List.allPairs [x1..x2] [y1..y2] |> List.iter (fun (x,y) -> lights[y,x] <- 0)
        | "toggle" -> List.allPairs [x1..x2] [y1..y2]    |> List.iter (fun (x,y) -> lights[y,x] <- if lights[y,x] = 1 then 0 else 1 )
        | _ -> raise (Exception("lkjdfjklfdg"))
        )

lights |> Seq.cast<int> |> Seq.filter (fun x -> x = 1) |> Seq.length |> printf "Part1: %A\n"


let lights2 = Array2D.create 1000 1000 0

let input2 =
    File.ReadAllLines("input")
    |> Array.map (fun x ->
        Regex.Match(x, @"(.+?) (\d+),(\d+).* (\d+),(\d+)"))
    |> Array.map (fun x -> (x.Groups[1].Value, x.Groups[2].Value |> int, x.Groups[3].Value |> int, x.Groups[4].Value |> int, x.Groups[5].Value |> int))
    |> Array.map (fun x ->
        let cmd, x1, y1, x2, y2                       = x
        match cmd with
        | "turn on" -> List.allPairs [x1..x2] [y1..y2] |> List.iter (fun (x,y) -> lights2[y,x]<- lights2[y,x] + 1)
        | "turn off" -> List.allPairs [x1..x2] [y1..y2] |> List.iter (fun (x,y) -> lights2[y,x] <- Math.Max (lights2[y,x] - 1, 0))
        | "toggle" -> List.allPairs [x1..x2] [y1..y2]    |> List.iter (fun (x,y) -> lights2[y,x] <- lights2[y,x] + 2 )
        | _ -> raise (Exception("lkjdfjklfdg"))
        )

lights2 |> Seq.cast<int> |> Seq.sum |> printf "Part2: %A"




//377891
