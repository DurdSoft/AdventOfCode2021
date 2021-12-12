module AdventOfCode2021.Day12

open System
open System.Collections.Generic
open System.Text.RegularExpressions

type Node =
    private
    | Start
    | End
    | SmallCave of string
    | LargeCave of string
    
[<RequireQualifiedAccess>]
module private Node =
    let isSmallCave (n : Node) =
        match n with
        | SmallCave _ -> true
        | _ -> false


type private Path = { Nodes : Node list }
    
[<RequireQualifiedAccess>]
module private Path =
    let hasVisitedSmallCave (path : Path) (n : Node) =
        match n with
        | SmallCave _ -> path.Nodes |> List.contains n
        | _ -> false
                                
    let insertNode (path : Path) (node : Node) =
        { path with Nodes = (path.Nodes |> List.insertAtEnd node) }
    
    let atEnd (path : Path) =
        path.Nodes |> List.contains End
    
    
let private regex = Regex("(?<from>\w+)-(?<to>\w+)")
let private upperCase = [ 'A'..'Z' ] 

let buildAdjacency (input : string list) =
    let parse (s : string) =
        match s with
        | "start" -> Start
        | "end" -> End
        | u when u |> Seq.forall (fun c -> upperCase |> List.contains c) -> LargeCave u
        | s -> SmallCave s
    
    input
    |> Seq.map regex.Match
    |> Seq.collect (fun m ->
        let from = m.Groups["from"].Value
        let to' = m.Groups["to"].Value
        [ (parse from), (parse to'); (parse to'), (parse from)])
    |> Seq.toList
    |> List.groupBy fst
    |> List.map (fun (f, t) -> f, (t |> List.map snd))
    |> Map.ofSeq
    

let private calculateNumberOfPaths (smallCaveFn : Path -> Node -> bool) (adjacency : Map<Node, Node list>) =
    let starts = adjacency
                 |> Map.find Start
                 |> List.map (fun l -> { Nodes = [ Start; l ] })
    
    let mutable foundPaths = 0
    
    let rec findPaths (path : Path) =
        let outPaths =
            adjacency[path.Nodes |> List.last]
            |> List.filter (fun n -> n <> Start && smallCaveFn path n)
            |> List.map (Path.insertNode path)
        
        let notAtEnd = outPaths |> List.filter (Path.atEnd >> not)
        
        foundPaths <- foundPaths + (outPaths.Length - notAtEnd.Length)
        
        notAtEnd |> List.iter findPaths
    
    starts |> List.iter findPaths
    foundPaths
    
[<RequireQualifiedAccess>]
module Part1 =
    
    let calculateNumberOfPaths (adjacency : Map<Node, Node list>) =
        calculateNumberOfPaths (fun p n -> Path.hasVisitedSmallCave p n |> not) adjacency
        
[<RequireQualifiedAccess>]
module Part2 =
    
    let calculateNumberOfPaths (adjacency : Map<Node, Node list>) =
        calculateNumberOfPaths
            (fun p n ->
                if Path.hasVisitedSmallCave p n then
                    let onlyVisitedSmallCaveOnce =
                        p.Nodes
                        |> Seq.filter Node.isSmallCave
                        |> Seq.groupBy id
                        |> Seq.map snd
                        |> Seq.forall (fun v -> v |> Seq.length = 1)
                    onlyVisitedSmallCaveOnce
                else true)
            
            adjacency
        