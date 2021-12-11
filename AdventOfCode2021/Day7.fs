module AdventOfCode2021.Day7

open System

[<RequireQualifiedAccess>]
module Part1 =
    
    let calculateFuelCost (input : int list) =
        let allPositions = input |> List.distinct
        let mutable bestResult = Int32.MaxValue
        
        for position in allPositions do
            let result = input
                         |> List.map (fun i -> i - position |> abs)
                         |> List.sum
            if result < bestResult then bestResult <- result
        
        bestResult

[<RequireQualifiedAccess>]
module Part2 =
    
    let calculateFuelCost (input : int list) =
        let allPositions = [ for x = (input |> List.min) to (input |> List.max) do x ]
        let mutable bestResult = Int32.MaxValue
        
        for position in allPositions do
            let result = input
                         |> List.map (fun i ->
                             let start = min i position
                             let end' = max i position
                             let mutable sum = 0
                             for x = start to end' do
                                 sum <- sum + (x - start)
                             sum)
                         |> List.sum
            if result < bestResult then bestResult <- result
        
        bestResult
