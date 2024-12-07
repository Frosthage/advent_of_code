open System
open System.IO

type Operator =
    | Add
    | Mul
    | Concat
    | Undefined

type Term = { Number: int64; Operator: Operator }

type Equation = { Answer: int64; Terms: Term list }

let input =
    File.ReadAllLines("input")
    |> Array.map _.Split(':')
    |> Array.map (fun x ->
        { Answer = int64 (x[0])
          Terms =
            x[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
            |> Array.map (fun x ->
                { Number = int64 (x)
                  Operator = Undefined })
            |> Array.toList })

let rec equationPermutations (terms: Term list) : Term list list =
    match terms with
    | [ x ] ->
        [ [ { Number = x.Number
              Operator = Undefined } ] ]
    | x :: xs ->
        let m = { x with Operator = Mul }
        let a = { x with Operator = Add }
        let x1 = equationPermutations xs |> List.map (fun x -> [ a ] @ x)
        let x2 = equationPermutations xs |> List.map (fun x -> [ m ] @ x)
        x1 @ x2


let calculate (terms: Term list) =
    terms
    |> List.fold
        (fun acc x ->
            match acc.Operator with
            | Add ->
                { Number = acc.Number + x.Number
                  Operator = x.Operator }
            | Mul ->
                { Number = acc.Number * x.Number
                  Operator = x.Operator }
            | _ -> raise (Exception("sadfsdf")))
        { Number = 0L; Operator = Add }

input
|> List.ofArray
|> List.map (fun x ->
    x.Terms
    |> equationPermutations
    |> List.map (fun xx -> { Answer = x.Answer; Terms = xx }))
|> List.map (fun x ->
    let calculated = x |> List.map (fun x -> calculate x.Terms)
    (x[0].Answer, calculated |> List.map _.Number))
|> List.filter (fun (a, b) -> List.contains a b)
|> List.sumBy fst
|> printf "Part1 %A\n"


let rec equationPermutations2 (terms: Term list) : Term list list =
    match terms with
    | [ x ] ->
        [ [ { Number = x.Number
              Operator = Undefined } ] ]
    | x :: xs  ->
        let m = { x with Operator = Mul }
        let a = { x with Operator = Add }
        let c = { x with Operator = Concat }
        let x1 = xs |> equationPermutations2 |> List.map (fun x -> [ a ] @ x)
        let x2 = xs |> equationPermutations2 |> List.map (fun x -> [ m ] @ x)
        let x3 = xs |> equationPermutations2 |> List.map (fun x -> [ c ] @ x)
        x1 @ x2 @ x3

let calculate2 (terms: Term list) =
    terms
    |> List.fold
        (fun acc x ->
            match acc.Operator with
            | Add ->
                { Number = acc.Number + x.Number
                  Operator = x.Operator }
            | Mul ->
                { Number = acc.Number * x.Number
                  Operator = x.Operator }
            | Concat ->
                { Number = string (acc.Number) + string (x.Number) |> int64
                  Operator = x.Operator
                }
            | _ -> raise (Exception("sadfsdf")))
        { Number = 0L; Operator = Add }


let apa =
    input
    |> List.ofArray
    |> List.map (fun x ->
        x.Terms
        |> equationPermutations2 
        |> List.map (fun xx -> { Answer = x.Answer; Terms = xx }))
    |> List.map (fun x ->
        let calculated = x |> List.map (fun x -> calculate2 x.Terms)
        (x[0].Answer, calculated |> List.map _.Number))
    |> List.filter (fun (a, b) -> List.contains a b)
    |> List.sumBy fst

printf "Part2 %A" apa


//20665830408335L
