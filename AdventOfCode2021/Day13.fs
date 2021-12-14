module AdventOfCode2021.Day13

open System
open System.Text.RegularExpressions

type Fold =
    private
    | X of int
    | Y of int

let private regex = Regex("fold along (?<xy>\w+)=(?<location>\d+)")

let processInputData (data : string array) =
    let stringToXy s i =
        match s with
        | "x" -> X i
        | "y" -> Y i
        | _ -> failwith $"Invalid char '{s}'"
    
    let locations = data
                    |> Seq.takeWhile (fun s -> s <> "")
                    |> Seq.map (fun s ->
                        let split = s.Split(',')
                        split[0] |> Int32.Parse, split[1] |> Int32.Parse)
                    |> Seq.toList
                    
    let instructions = data
                       |> Seq.skipWhile (fun s -> s <> "")
                       |> Seq.skip 1
                       |> Seq.map regex.Match
                       |> Seq.map (fun m -> stringToXy
                                               (m.Groups["xy"].Value)
                                               (m.Groups["location"].Value |> Int32.Parse))
                       |> Seq.toList
    
    locations, instructions

let rec private calculateVisibleDots (folds : Fold list) (locations : (int * int) list)  =
    match folds with
    | [ ] -> locations
             |> Seq.distinct
             |> Seq.toList
             
    | fold :: tail ->
        let remaining, reflected = 
            match fold with
            | X i ->
                let locationsToReflect, remaining = locations
                                                    |> List.partition (fun (x, _) -> x > i)
                remaining, [ for x, y in locationsToReflect do
                               let reflectedX = (2 * i) - x
                               (reflectedX, y) ]
            | Y i ->
                let locationsToReflect, remaining = locations
                                                    |> List.partition (fun (_, y) -> y > i)
                remaining, [ for x, y in locationsToReflect do
                               let reflectedY = (2 * i) - y
                               (x, reflectedY) ]
                
        calculateVisibleDots tail (remaining @ reflected)

[<RequireQualifiedAccess>]
module Part1 =
    
    let calculateVisibleDots (locations : (int * int) list) (folds : Fold list) =
        let points = calculateVisibleDots [ folds |> List.head ] locations  
        
        points |> Seq.length
            
[<RequireQualifiedAccess>]
module Part2 =
    
    let calculateCharactersAndPrint (locations : (int * int) list) (folds : Fold list) =
        let points = calculateVisibleDots folds locations
        let xBound = points |> Seq.map fst |> Seq.sortDescending |> Seq.head
        let yBound = points |> Seq.map snd |> Seq.sortDescending |> Seq.head
        
        for y = 0 to yBound do
            for x = 0 to xBound do
                printf $"%c{if points |> Seq.contains (x, y) then '█' else ' '}"
            printfn ""
    