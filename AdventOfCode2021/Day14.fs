module AdventOfCode2021.Day14

open System
open System.Collections.Generic
open System.Text
open System.Text.RegularExpressions
open Microsoft.FSharp.Core.Operators.Checked

type PairInsertion = char * char * char

let private regex = Regex("(?<from>\w+) -> (?<c>\w+)")

let processInputData (data : string array) =
    let template = data
                   |> Array.head
    
    let instructions = data
                       |> Seq.skip 2
                       |> Seq.map regex.Match
                       |> Seq.map (fun m ->
                           let c1 = m.Groups["from"].Value |> Seq.head
                           let c2 = m.Groups["from"].Value |> Seq.last
                           let c3 = m.Groups["c"].Value |> Seq.head
                           PairInsertion (c1, c2, c3))
                       |> Seq.toList
    template, instructions

/// Slow implementation
[<RequireQualifiedAccess>]
module Part1 =
    
    let findElementCount (steps : int) (template : string) (insertions : PairInsertion list) =
        let allIndexesOf (substring : string) (s : string) =
            let rec loop (substring : string) (s : string) (idx : int) (indexes : ResizeArray<_>) =
                match idx with
                | -1 -> indexes |> Seq.toList
                | x when x = s.Length - 1 -> indexes |> Seq.toList
                | _ ->
                    let nextIdx = s.IndexOf(substring, idx + 1)
                    indexes.Add(idx)
                    loop substring s nextIdx indexes
                        
            let firstIndex = s.IndexOf(substring)
            let indexes = ResizeArray()
            loop substring s firstIndex indexes
        
        let rec applyInsertionsForSteps (step : int) (acc : string) =
            match step with
            | 0 -> acc
            | _ ->
                let builder, _ = insertions
                               |> Seq.filter (fun (c1, c2, _) -> acc.Contains($"{c1}{c2}"))
                               |> Seq.collect (fun (c1, c2, c)  ->
                                   allIndexesOf $"{c1}{c2}" acc 
                                   |> List.map (fun i -> i, c))
                               |> Seq.sortBy fst
                               |> Seq.fold
                                   (fun (sb : StringBuilder, charsInserted) (idx, c) ->
                                        let insertPosition = charsInserted + idx + 1
                                        sb.Insert(insertPosition, c), charsInserted + 1)
                                   (StringBuilder(acc), 0)
                
                applyInsertionsForSteps (step - 1) (builder.ToString())
        
        let counts = 
            applyInsertionsForSteps steps template
            |> Seq.groupBy id
            |> Seq.sortByDescending (snd >> Seq.length)
            |> Seq.map (snd >> Seq.toList)
            |> Seq.toList
        
        (counts |> List.head |> List.length) - (counts |> List.last |> List.length) 
        
[<RequireQualifiedAccess>]
module Part2 =

    let findElementCountFast (steps : int) (template : string) (insertions : PairInsertion list) =
        let groupToDictionary s = s |> Seq.groupBy id
                                    |> Seq.map (fun (p, g) -> p, g |> Seq.length |> uint64)
                                    |> dict
                                    |> Dictionary
        
        let pairings = template
                       |> Seq.pairwise
                       |> groupToDictionary
                       
        let counts = template
                     |> groupToDictionary

        for pair in template |> Seq.pairwise do
            pairings[pair] <- pairings.GetValueOrDefault(pair) + 1UL
        
        [ 1..steps ]
        |> List.iter (fun _ ->
            insertions
            |> List.filter (fun (c1, c2, _) -> pairings.GetValueOrDefault((c1, c2)) > 0UL)
            |> List.collect (fun (c1, c2, c3) ->
                let value = pairings.GetValueOrDefault((c1, c2))
                let a = (c1, c3), value
                let b = (c3, c2), value
                pairings[(c1, c2)] <- 0UL
                counts[c3] <- counts.GetValueOrDefault(c3) + value
                [ a; b ])
            |> List.iter (fun (k, v) -> pairings[k] <- pairings.GetValueOrDefault(k) + v))
        
        let max = counts |> Seq.map (fun kv -> kv.Value) |> Seq.max
        let min = counts |> Seq.map (fun kv -> kv.Value) |> Seq.min
        
        max - min
        