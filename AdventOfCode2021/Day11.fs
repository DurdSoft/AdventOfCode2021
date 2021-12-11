module AdventOfCode2021.Day11

open System
open System.Collections.Generic

let mapFile (content : string list) =
    let contents = content
                   |> List.map (fun s -> s
                                         |> Seq.map  (fun c -> Int32.Parse <| c.ToString())
                                         |> Seq.toArray)
                   |> List.toArray
    
    let matrix = Array2D.zeroCreate
                     (contents |> Array.length)
                     (contents |> Array.head |> Array.length)
    
    for y = 0 to (contents |> Array.length) - 1 do
        for x = 0 to (contents |> Array.head |> Array.length) - 1 do
            matrix[x,y] <- contents[x][y]
            
    matrix

let private simulateStep (state : int[,]) =
    let xBound = Array2D.length1 state - 1
    let yBound = Array2D.length2 state - 1
    let flashedThisGeneration = HashSet<int * int>()
    
    let adjacentPositions x y =
        [| if x <> 0 then (x - 1, y)
           if y <> 0 then (x, y - 1)
           if x <> xBound then (x + 1, y)
           if y <> yBound then (x, y + 1)
           if y <> 0 && x <> 0 then (x - 1, y - 1)
           if y <> 0 && x <> xBound then (x + 1, y - 1)
           if y <> yBound && x <> 0 then (x - 1, y + 1)
           if y <> yBound && x <> xBound then (x + 1, y + 1) |]
    
    let inline incrementCell x y =
        state[x, y] <- state[x, y] + 1 
    
    let rec loop (x : int, y : int) =
        incrementCell x y
        
        if state[x, y] > 9 && flashedThisGeneration.Contains(x,y) |> not then
            flashedThisGeneration.Add(x, y) |> ignore
            let adjacent =
                adjacentPositions x y
                |> Array.filter (fun p -> flashedThisGeneration.Contains(p) |> not)
                
            for pos in adjacent do
                loop pos
    
    state
    |> Array2D.iteri (fun x y _ -> loop (x, y))
    
    state
    |> Array2D.iteri (fun x y v -> if v > 9 then state[x, y] <- 0)
    
    flashedThisGeneration.Count

[<RequireQualifiedAccess>]
module Part1 =
    
    let calculateNumberOfFlashes (steps : int) (initialState : int[,]) =
        let state = initialState |> Array2D.map id
        [ 1..steps ]
        |> List.map (fun _ -> simulateStep state)
        |> List.sum
    
[<RequireQualifiedAccess>]
module Part2 =
    
    let findFirstGenerationSync (initialState : int[,]) =
        let state = initialState |> Array2D.map id
        let totalCells = Array2D.length1 state * Array2D.length2 state
        
        let rec testGeneration (step : int) =
            let totalFlashes = simulateStep state
            if totalFlashes = totalCells then step
            else testGeneration (step + 1)
        
        testGeneration 1