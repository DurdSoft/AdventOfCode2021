module AdventOfCode2021.Day15

open System
open System.Collections.Generic

let processData (data : string array) =
    let riskValues = data |> Array.map (fun s -> s |> Seq.map (string >> int) |> Seq.toArray)
    let xBound = (riskValues |> Array.head |> Array.length)
    let yBound = (riskValues |> Array.length)
    
    let costs = Dictionary()
    
    for x = 0 to xBound - 1 do
        for y = 0 to yBound - 1 do
            costs[(x, y)] <- riskValues[x][y]
    
    costs


let private findPathCost destination (costFn : int * int -> int) (adjacencyFn : int * int -> (int * int) list) =
    let start = (0, 0)
    
    let distances = Dictionary()
    let parents = Dictionary()
    let queue = PriorityQueue()
    
    distances[start] <- 0
    queue.Enqueue(start, 0)
    
    let rec dijkstra() =
        match queue.Count with
        | 0 -> failwithf "Cannot find destination"
        | _ ->
            let v = queue.Dequeue()
            if v = destination then ()
            else
                 let adjacent = adjacencyFn v
                 
                 for u in adjacent do
                     let weight = costFn u
                     let distanceU = distances.GetValueOrDefault(u, Int32.MaxValue)
                     if distanceU > distances[v] + weight then
                         distances[u] <- weight + distances[v]
                         parents[u] <- v
                         queue.Enqueue(u, distances[u])
                 dijkstra()
    dijkstra()
          
    let rec unwindPath n acc =
        match n with
        | x when x = start -> acc
        | _ ->
            let reachedVia = parents[n]
            unwindPath reachedVia (acc |> List.insertAtEnd reachedVia)
    
    let totalCost = unwindPath destination []
                    |> Seq.filter (fun v -> v <> start)
                    |> Seq.append [ destination ]
                    |> Seq.map costFn
                    |> Seq.sum
    totalCost

[<RequireQualifiedAccess>]
module Part1 =
    let findPathCost (costLookup : Dictionary<int * int, int>) =
        let destination = costLookup
                          |> Seq.map (fun kvp -> kvp.Key)
                          |> Seq.maxBy (fun (x, y) -> x + y)
        findPathCost
            destination
            (fun p -> costLookup[p])
            (fun (x, y) ->
                [ if x <> 0 then (x - 1, y)
                  if y <> 0 then (x, y - 1)
                  if x <> 99 then (x + 1, y)
                  if y <> 99 then (x, y + 1) ])
    
[<RequireQualifiedAccess>]
module Part2 =
    let findPathCost (costLookup : Dictionary<int * int, int>) =
        let xCostMax, yCostMax = costLookup
                                 |> Seq.map (fun kvp -> kvp.Key)
                                 |> Seq.maxBy (fun (x, y) -> x + y)
        
        let upScale n s = (n + 1) * s - 1
        
        let destination = upScale xCostMax 5, upScale yCostMax 5
        let xMax, yMax = destination
        let scaleFactor = (xMax + 1) / 5
        
        let calcScale n =
            match n with
            | x when x <= (upScale xCostMax 1) -> 0
            | x when x > (upScale xCostMax 1) && x <= (upScale xCostMax 2) -> 1
            | x when x > (upScale xCostMax 2) && x <= (upScale xCostMax 3) -> 2
            | x when x > (upScale xCostMax 3) && x <= (upScale xCostMax 4) -> 3
            | x when x > (upScale xCostMax 4) && x <= (upScale xCostMax 5) -> 4
            | _ -> failwith "Out of range"
        
        let downScale n = n - (calcScale n) * scaleFactor
        
        findPathCost
            destination
            (fun (x, y) ->
                let baseCost = costLookup[(downScale x, downScale y)]
                let scale = calcScale x + calcScale y
                
                match baseCost + scale with
                | x when x > 9 -> x - 9
                | x -> x)
            (fun (x, y) ->
                [ if x <> 0 then (x - 1, y)
                  if y <> 0 then (x, y - 1)
                  if x <> xMax then (x + 1, y)
                  if y <> yMax then (x, y + 1) ])
        