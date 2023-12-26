
open System
open System.Security.Cryptography
open System.Text

let md5 = MD5.Create()

let hash (s:string) =
    let bytes = Encoding.ASCII.GetBytes(s)
    let hashedBytes = md5.ComputeHash(bytes)
    BitConverter.ToString(hashedBytes).Replace("-", "").ToLower()
      
let input = "ckczppom"

(Seq.initInfinite id
|> Seq.map (fun x -> (x, hash (input + (string x))))
|> Seq.takeWhile (fun (_, x) -> not (x.StartsWith("00000")))
|> Seq.last
|> fst) + 1
|> printfn "Part 1: %A"

(Seq.initInfinite id
|> Seq.map (fun x -> (x, hash (input + (string x))))
|> Seq.takeWhile (fun (_, x) -> not (x.StartsWith("000000")))
|> Seq.last
|> fst) + 1
|> printfn "Part 2: %A"

