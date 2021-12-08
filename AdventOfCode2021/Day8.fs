module AdventOfCode2021.Day8

open System

let processInputRow (s : string) =
    let split = s.Split(' ')
    let beforePipe = split |> Array.takeWhile (fun s -> s <> "|") |> Array.toList
    let afterPipe = split |> Array.skipWhile (fun s -> s <> "|") |> Array.skip 1 |> Array.toList
    beforePipe, afterPipe

[<RequireQualifiedAccess>]
module Part1 =
    
    let calculateDigitCounts (input : string list) =
        input
        |> List.filter (fun s -> match s.Length with
                                 | 2 | 3 | 4 | 7 -> true
                                 | _ -> false )
        |> List.length
        
[<RequireQualifiedAccess>]
module Part2 =
    let private calculateSingleCount (beforePipe : string list) (afterPipe : string list) =
        let findSignatureByLength l = beforePipe |> List.find (fun s -> s.Length = l)
        
        let signatureMatch (s1: char seq) (s2: char seq) =
            (s1 |> Seq.sort |> String.Concat) = (s2 |> Seq.sort |> String.Concat)
        
        let superSetOfSignature (s1 : string) (superSet : string) =
            let superSetChars = superSet |> Seq.sort |> Seq.toList
            s1 |> Seq.forall (fun c -> superSetChars |> List.contains c)
        
        let signature1 = findSignatureByLength 2 
        let signature4 = findSignatureByLength 4
        let signature7 = findSignatureByLength 3
        let signature8 = findSignatureByLength 7
        let signature9And0 = beforePipe |> List.filter (fun s -> s.Length = 6 && superSetOfSignature signature7 s)
        let signature6 = beforePipe |> List.find (fun s -> s.Length = 6 && signature9And0 |> List.contains s |> not)
        let signature9 = signature9And0 |> List.find (superSetOfSignature signature4)
        let signature0 = signature9And0 |> List.find (fun s -> s <> signature9)
        let signature3 = beforePipe |> List.find (fun s -> s.Length = 5 && superSetOfSignature signature1 s)
        let topLeft = signature9 |> Seq.find (fun c -> signature3 |> Seq.contains c |> not)
        let signature5 = beforePipe |> List.find (fun s -> s.Length = 5 && s |> Seq.contains topLeft)
        let signature2 = beforePipe |> List.find (fun s -> s.Length = 5 && s <> signature5 && s <> signature3)

        let signatureValues = 
            [ signature0, "0"
              signature1, "1"
              signature2, "2"
              signature3, "3"
              signature4, "4"
              signature5, "5"
              signature6, "6"
              signature7, "7"
              signature8, "8"
              signature9, "9" ]
        
        afterPipe
        |> List.map (fun s -> (signatureValues |> List.find (fun (si, _) -> signatureMatch si s)) |> snd)
        |> String.Concat
        |> Int32.Parse
        
        
    let calculateCount (rows : (string list * string list) list) =
        rows
        |> List.map (fun (b, a) ->  calculateSingleCount b a)
        |> List.sum
        