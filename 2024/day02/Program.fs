open System.IO

File.ReadAllLines("input")
|> Array.map (fun x -> x.Split(' ') |> Array.map int)
|> Array.filter (fun x ->
    let d = x |> Array.windowed 2 |> Array.map (fun x -> x[1] - x[0])
    Array.forall (fun x -> x > 0) d || Array.forall (fun x -> x < 0) d)
|> Array.filter (fun x ->
    let d = x |> Array.windowed 2 |> Array.map (fun x -> x[1] - x[0])
    Array.forall (fun x -> abs (x) < 4) d)
|> Array.length
|> printf "Part1 %A\n"


File.ReadAllLines("input2")
|> Array.map (fun x -> x.Split(' ') |> Array.map int)
|> Array.filter (fun l ->
    [0..1..l.Length-1]
    |> List.exists (fun i ->
        let d =
            l
            |> Array.indexed
            |> Array.filter (fun (i2, _) -> i <> i2)
            |> Array.map snd
            |> Array.windowed 2
            |> Array.map (fun x -> x[1] - x[0])

        let rule_1 = Array.forall (fun x -> x > 0) d || Array.forall (fun x -> x < 0) d
        
        let rule_2 = Array.forall (fun x -> abs x < 4) d 
        
        rule_1 && rule_2
        )
    )
|> Array.length
|> printf "Part2 %A\n"

