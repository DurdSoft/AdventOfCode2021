module AdventOfCode2021.Day9

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
    
    for x = 0 to (contents |> Array.length) - 1 do
        for y = 0 to (contents |> Array.head |> Array.length) - 1 do
            matrix[x,y] <- contents[x][y]
            
    matrix
     

[<RequireQualifiedAccess>]
module Part1 =
    let calculateRiskLevel (matrix : int [,]) =
        let mutable foundWells = ResizeArray<_>()
        
        let isWell (v : int) (surrounding : int array) =
            surrounding |> Array.forall (fun x -> x > v)
        
        matrix |> Array2D.iteri (fun x y v ->
            let adjacent = match x, y with
                           | 0, 0 -> [| (matrix[x + 1, y])
                                        (matrix[x, y + 1]) |]
                           
                           | 0, _ -> [| (matrix[x + 1, y])
                                        (matrix[x, y - 1])
                                        if y <> matrix.GetLength(1) - 1 then (matrix[x, y + 1]) |]
                           
                           | _, 0 -> [| (matrix[x, y + 1])
                                        (matrix[x - 1, y])
                                        if x <> matrix.GetLength(0) - 1 then (matrix[x + 1, y]) |]
                           
                           | _, _ ->  [| if y <> matrix.GetLength(1) - 1 then (matrix[x, y + 1])
                                         (matrix[x, y - 1])
                                         if x <> matrix.GetLength(0) - 1 then (matrix[x + 1, y])
                                         (matrix[x - 1, y]) |]
                           
            if isWell v adjacent then foundWells.Add(v))
        
        foundWells |> Seq.map (fun w -> w + 1) |> Seq.sum
        
[<RequireQualifiedAccess>]
module Part2 =
    
    let calculate3LargestBasins (matrix : int [,]) =
        let basins = HashSet<(int * int) list>()
        
        let xBound = matrix.GetLength(0) - 1
        let yBound = matrix.GetLength(1) - 1
        
        let adjacentPositionsNot9 x y =
            [| if x <> 0 && matrix[x - 1, y] <> 9 then (x - 1, y)
               if y <> 0 && matrix[x, y - 1] <> 9 then (x, y - 1)
               if x <> xBound && matrix[x + 1, y] <> 9 then (x + 1, y)
               if y <> yBound && matrix[x, y + 1] <> 9 then (x, y + 1) |]
        
        let rec recursivelyFindBasin (next : HashSet<_>) (acc : HashSet<_>) =
            match next with
            | x when x.Count = 0 -> acc
            | _ ->
                acc.UnionWith(next)
                
                let nextLocations = next
                                    |> Seq.map (fun (x', y') -> adjacentPositionsNot9 x' y')
                                    |> Seq.collect id
                                    |> Seq.distinct
                                    |> Seq.filter (fun x -> acc.Contains(x) |> not)
                                    |> HashSet
                                    
                recursivelyFindBasin nextLocations (acc)
        
        matrix
        |> Array2D.iteri (fun x y v ->
            let notExplored = basins |> Seq.exists (fun b -> b |> List.contains (x,y)) |> not
            
            if v <> 9 && notExplored then
                let basin =
                    let acc = HashSet<_>([(x,y)])
                    recursivelyFindBasin (adjacentPositionsNot9 x y |> HashSet) acc
                    |> Seq.sort
                    |> Seq.toList
                basins.Add(basin) |> ignore)
        
        let top3 =
            basins
            |> Seq.toList
            |> List.map List.length
            |> List.sortDescending
            |> List.take 3
            
        top3 |> List.fold (fun s t -> s * t) 1
            
        