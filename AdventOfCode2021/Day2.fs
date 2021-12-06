module AdventOfCode2021.Day2

open System

module private Common =
    type Direction =
        | Forward of int
        | Down of int
        | Up of int
        
    let parseDirection (direction: string) =
        let d = direction |> Seq.takeWhile (fun c -> c <> ' ') |> String.Concat
        let mag = direction |> Seq.skipWhile (fun c -> c <> ' ') |> Seq.skip 1 |> String.Concat |> Int32.Parse 
        
        match d with
        | "forward" -> Forward mag
        | "down" -> Down mag
        | "up" -> Up mag
        | _ -> failwith $"Unknown direction {direction}"
        
open Common

[<RequireQualifiedAccess>]
module Part1 =
    let calculateDepth (directions: string list) =
        let parsed = directions |> List.map parseDirection
        
        let mutable depth = 0
        let mutable direction = 0
        
        parsed
        |> List.iter (fun d ->
            match d with
            | Forward m -> direction <- direction + m
            | Down m -> depth <- depth + m
            | Up m -> depth <- depth - m)
        
        depth * direction
        
    let readInputFromFile() =
        System.IO.File.ReadAllLines("Day2.txt")
        |> Array.toList
        
[<RequireQualifiedAccess>]
module Part2 =
    let calculateDepthAndAim (directions: string list) =
        let parsed = directions |> List.map parseDirection
        
        let mutable depth = 0
        let mutable direction = 0
        let mutable aim = 0
        
        parsed
        |> List.iter (fun d ->
            match d with
            | Forward m ->
                direction <- direction + m
                depth <- depth + (aim * m)
            | Down m ->
                aim <- aim + m
            | Up m ->
                aim <- aim - m)
        
        depth * direction
        