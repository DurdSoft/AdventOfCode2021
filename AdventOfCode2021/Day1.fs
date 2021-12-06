module AdventOfCode2021.Day1

open System

[<RequireQualifiedAccess>]
module Part1 = 
    type Change =
        | NA
        | Increased
        | Decreased
        | NoChange

    let private x =
        [199;200;208;210;200;207;240;269;260;263]
        |>Seq.pairwise
        |>Seq.filter(fun (a,b)-> b > a)
        |>Seq.length
    
    let private calculateChangeList data =
        let rec loop remaining previous acc =
            match previous, remaining with
            | Some _, [ ] -> acc
            | Some p, h :: tail ->
                let change = 
                    if p < h then Increased
                    elif p = h then NoChange
                    else Decreased
                    
                loop tail (Some h) (acc |> List.insertAtEnd change)
            | None, h :: tail ->
                loop tail (Some h) (acc |> List.insertAtEnd Decreased)
            | None, _ -> [ ]
                
        loop data None []
        
    let calculateNumberOfIncreases input =
        calculateChangeList input
        |> List.filter (fun c -> match c with Increased -> true | _ -> false)
        |> List.length
        
[<RequireQualifiedAccess>]
module Part2 =
    
    let calculateIncreasesWindowed (windowSize : int) (input : int list) =
        // could use List.windowed but the over-engineering is nice, I guess...
        let rec loop i acc =
            match i with
            | x when x + windowSize > input.Length -> acc
            | _ ->
                let average = input |> List.skip i |> List.take windowSize |> List.sum
                loop (i + 1) (acc |> List.insertAtEnd average)
        
        match input.Length with
        | x when x < windowSize -> failwith $"{nameof windowSize} less than length of {nameof input}"
        | _ ->
            loop 0 []
            |> Part1.calculateNumberOfIncreases
        