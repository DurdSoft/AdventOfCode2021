module AdventOfCode2021.Day6

open System.Collections.Generic

[<RequireQualifiedAccess>]
module Part1And2 =
    
    // First implementation - too slow for part 2
    let private calculateNextGeneration (input : int list) =
        let resets = input |> List.filter (fun x -> x = 0) |> List.map (fun _ -> 6)
        let decrements = input |> List.filter (fun x -> x <> 0) |> List.map (fun x -> x - 1)
        let newFishes = resets |> List.map (fun _ -> 8)
        resets @ decrements @ newFishes
    
    let private calculateNextGenerationFast (input : Dictionary<int, int64>) =
        let newCount = input.[0]
        for i = 1 to 8 do
            input.[i - 1] <- input.[i]
            
        input.[6] <- input.[6] + newCount
        input.[8] <- newCount
    
    let simulateFishFor (days : int) (input : int list) =
        let generations = [ for i = 0 to 8 do
                                let countForGeneration = input |> List.filter (fun x -> x = i) |> List.length |> int64
                                (i, countForGeneration) ]
                          |> dict
                          |> Dictionary
            
        let rec loop day =
            match day with
            | 0 ->
                generations |> Seq.map (fun kvp -> kvp.Value) |> Seq.sum
            | _ ->
                calculateNextGenerationFast generations
                loop (day - 1)
        
        loop days
        