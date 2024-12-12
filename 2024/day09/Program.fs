open System.IO

type File =
    { Index: int
      Size: int
      FreeSpace: int }

let digits = File.ReadAllText("input").ToCharArray()

let tempInput =
    digits[.. digits.Length - 2]
    |> Array.chunkBySize 2
    |> Array.mapi (fun i x ->
        { Index = i
          Size = (int x[0] - int '0')
          FreeSpace = (int x[1] - int '0') })

let last =
    { Index = (Array.last tempInput).Index + 1
      Size = (digits[digits.Length - 1] |> int) - (int '0')
      FreeSpace = 0 }

let input = Array.append tempInput [| last |]

let rev1 =
    input
    |> Array.skip 1
    |> Array.rev
    |> Array.map (fun x -> Array.create x.Size x.Index)
    |> Array.collect id

let part1 =
    seq {
        let mutable revIndex = 0

        for f in input do
            for _ in [ 1 .. f.Size ] do
                yield f.Index

            for _ in [ 1 .. f.FreeSpace ] do
                yield rev1[revIndex]
                revIndex <- revIndex + 1
    }


part1
|> Seq.take (input |> Array.sumBy (fun x -> x.Size))
|> Seq.indexed
|> Seq.sumBy (fun (i, x) -> x * i |> int64)
|> printf "\n%A\n\n"

let fitIn (file1: File) (file2: File) = file1.FreeSpace >= file2.Size


let part2: (File * File list) List  =
    seq {
        let mutable rev = input |> Array.rev |> List.ofArray
        
        for f in input do
            let bepa = ((f, []), rev)
                        ||> List.scan (fun (af, al) x ->
                            let sizeLeft = f.FreeSpace - (al |> List.sumBy _.Size)
                                                                                                                
                            if sizeLeft >= x.Size then
                                (af, [x] @ al)
                            else
                                (af, al)
                        )
            let last = bepa |> List.last
            let movedIndecies = (snd last) |> List.map _.Index
            rev <- rev |> List.filter (fun x -> movedIndecies |> List.contains x.Index |> not )
            
            yield last
    }
    |> List.ofSeq

let moved = part2 |> List.map snd |> List.collect id |> List.map _.Index |> List.ofSeq

printf "%A" moved

let result2 =
    seq {
        for file, files in part2 do
            let index = if moved |> List.contains file.Index then 0 else file.Index           
            yield! Array.create file.Size index
            let mutable freeSpace = file.FreeSpace
            for innerFile in (files |> List.rev) do
                freeSpace <- freeSpace - innerFile.Size
                yield! Array.create innerFile.Size innerFile.Index
            yield! Array.create freeSpace 0
    }

result2
|> Seq.indexed
|> Seq.sumBy (fun (i, x) -> ( int64 i)  * (int64 x))
|> printf "\n%A"


//incorrect 8592266602739