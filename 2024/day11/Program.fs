﻿open System.IO

let input = File.ReadAllText("input").Split(' ') |> Array.map int64 |> List.ofArray

let input2 =
    
    (Map.empty, input)
    ||> List.fold (fun acc x ->
        let count = Map.tryFind x acc |> Option.defaultValue 0L
        Map.add x (count + 1L) acc)

let count blink =
    (input2, [ 1..blink ])
    ||> List.fold (fun acc _ ->
        acc
        |> Map.toList
        |> List.map (fun (k, v) ->
            let xstr = string k

            let newK =
                if k = 0 then
                    [ 1L ]
                else if xstr.Length % 2 = 0 then
                    let x1 = int64 xstr[0 .. ((xstr.Length - 1) / 2)]
                    let x2 = int64 xstr[xstr.Length / 2 ..]
                    [ x1; x2 ]
                else
                    [ k * 2024L ]

            List.allPairs newK [ v |> int64 ])
        |> List.collect id
        |> List.groupBy fst
        |> List.map (fun (k, v) -> k, v |> List.sumBy snd)
        |> Map.ofList)
    |> Map.toList
    |> List.sumBy snd

printf "Part1: %A\n" (count 25)
printf "Part2: %A" (count 75)
