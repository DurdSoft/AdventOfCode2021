[<AutoOpen>]
module AdventOfCode2021.Extensions

[<RequireQualifiedAccess>]
module List =
    let insertAtEnd item list =
        list |> List.insertAt list.Length item
        
[<RequireQualifiedAccess>]
module File =
    let readAndMapFile (path : string) fn =
        System.IO.File.ReadAllLines(path) |> Array.map fn |> Array.toList
        