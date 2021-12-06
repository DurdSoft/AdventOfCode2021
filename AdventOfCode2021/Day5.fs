module AdventOfCode2021.Day5

open System
open System.Text.RegularExpressions

type Direction = {
    X1: int
    Y1: int
    X2: int
    Y2: int
}

let private regex = Regex("(?<x1>\d+),(?<y1>\d+) -> (?<x2>\d+),(?<y2>\d+)")

let directionFromString (input : string) =
    let m = regex.Match(input)
    let x1 = m.Groups.["x1"].Value |> Int32.Parse
    let x2 = m.Groups.["x2"].Value |> Int32.Parse
    let y1 = m.Groups.["y1"].Value |> Int32.Parse
    let y2 = m.Groups.["y2"].Value |> Int32.Parse
    { X1 = x1; X2 = x2; Y1 = y1; Y2 = y2 }

let private createLine (includeDiagonal : bool) (direction : Direction) =
    let line = 
        match direction with
        | d when d.X1 = d.X2 ->
            let startP = Math.Min(d.Y1, d.Y2)
            let endP = Math.Max(d.Y1, d.Y2)
            
            [ for i = startP to endP do (d.X1, i) ]
        | d when d.Y1 = d.Y2 ->
            let startP = Math.Min(d.X1, d.X2)
            let endP = Math.Max(d.X1, d.X2)
            
            [ for i = startP to endP do (i, d.Y1) ]
        | _ when not includeDiagonal ->
            []
        | d ->
            let startX = Math.Min(d.X1, d.X2)
            let startY = Math.Min(d.Y1, d.Y2)
            let endX = Math.Max(d.X1, d.X2)
            let endY = Math.Max(d.Y1, d.Y2)
            
            let xRange = [ for x = startX to endX do x ]
            let yRange = [ for y = startY to endY do y ]
            
            if startX = d.X1 && startY = d.Y1 then
                xRange
                |> List.mapi (fun i x -> x, yRange.[i])
            elif startX = d.X1 && startY = d.Y2 then
                yRange
                |> List.rev
                |> List.mapi (fun i y -> xRange.[i], y)
            elif startX = d.X2 && startY = d.Y1 then
                xRange
                |> List.rev
                |> List.mapi (fun i x -> x, yRange.[i])
                |> List.rev //suspect
            elif startX = d.X2 && startY = d.Y2 then
                xRange
                |> List.mapi (fun i x -> x, yRange.[i])
                |> List.rev
            else
                failwith "Shouldn't happen"
            
    line
    
let private countOfTwoOverlaps (horizontal : bool) (input : Direction list) =
    let allX = input |> List.collect (fun d -> [ d.X1; d.X2 ])
    let allY = input |> List.collect (fun d -> [ d.Y1; d.Y2 ])
    
    let maxX = allX |> List.max
    let maxY = allY |> List.max
    
    let matrix = Array2D.zeroCreate<int32> (maxX + 1) (maxY + 1)
    
    input
    |> List.map (createLine horizontal)
    |> List.iter (fun l ->
        l |> List.iter (fun (x, y) -> matrix.[x, y] <- matrix.[x, y] + 1))
    
    let mutable count = 0
    matrix |> Array2D.iter (fun n -> if n > 1 then count <- count + 1)
    
    count
    
[<RequireQualifiedAccess>]
module Part1 =
    
    let countOfTwoOverlaps (input : Direction list) =
        countOfTwoOverlaps false input
        
[<RequireQualifiedAccess>]
module Part2 =
    
    let countOfTwoOverlaps (input : Direction list) =
        countOfTwoOverlaps true input
        