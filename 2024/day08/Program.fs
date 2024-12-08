open System.IO

let file = "input"

let maxY = File.ReadAllLines(file) |> Array.length
let maxX = maxY

let input =
    File.ReadAllLines(file)
    |> Array.mapi (fun i x -> x.ToCharArray() |> Array.mapi (fun ii x -> ((ii, i), x)))
    |> Array.collect id
    |> Array.filter (fun x -> (snd x) <> '.')
    |> Array.map (fun ((x, y), c) -> (c, (x, y)))
    |> Array.groupBy fst
    |> Array.map (fun (c, a) -> (c, a |> Array.map snd))
    |> Map.ofArray

let antionodes =
    seq {
        for KeyValue(k, coordinates) in input do
            for x1, y1 in coordinates do
                for x2, y2 in coordinates do
                    if (x1, y1) <> (x2, y2) then
                        let dx, dy = (x1 - x2, y1 - y2)
                        yield (k, (x1 + dx, y1 + dy))
    }


antionodes
|> Seq.filter (fun (_, (x, y)) -> not (x < 0 || x >= maxX || y < 0 || y >= maxY))
|> Seq.distinctBy snd
|> Seq.length
|> printf "Part1: %A\n"

let antionodes2 =
    seq {
        for KeyValue(k, coordinates) in input do
            for x1, y1 in coordinates do
                for x2, y2 in coordinates do
                    if (x1, y1) <> (x2, y2) then
                        let dx, dy = (x1 - x2, y1 - y2)
                        let mutable nx, ny = (x1 + dx, y1 + dy)
                        yield (k, (x1, y1))
                        yield (k, (x2, y2))

                        while (nx >= 0 && nx < maxX && ny >= 0 && ny < maxY) do
                            yield (k, (nx, ny))
                            nx <- nx + dx
                            ny <- ny + dy
    }

antionodes2
|> Seq.sortBy (fun (_, (x, y)) -> y, x)
|> Seq.map snd
|> Seq.distinct
|> Seq.length
|> printf "Part2: %A"
