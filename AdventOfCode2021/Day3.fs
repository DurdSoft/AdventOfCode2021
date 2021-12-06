module AdventOfCode2021.Day3

open System

type private MostLeast =
    | Least
    | Most

let private calculateBitLength (input: string list) =
    (input |> List.head |> Seq.length) - 1
    
let private convertToInt s = Convert.ToInt32(s, 2)

[<RequireQualifiedAccess>]
module Part1 =
    let private calculateCommonBit (index: int) (mostOrLeast : MostLeast) (input: string list) =
        let sortFn = match mostOrLeast with Least -> List.sortByDescending | Most -> List.sortBy
        
        input
        |> List.map (fun s -> s.[index])
        |> List.groupBy id
        |> List.map snd
        |> sortFn (fun l -> l.Length)
        |> List.head
        |> List.head
    
    let calculatePowerConsumption (input : string list) =
        let calculateCommonBit (mostOrLeast : MostLeast) =
            [| for i = 0 to calculateBitLength input do
                calculateCommonBit i mostOrLeast input |]
            |> String
            |> convertToInt
        
        let gamma = calculateCommonBit Most
        let epsilon = calculateCommonBit Least
        
        gamma * epsilon
        
[<RequireQualifiedAccess>]
module Part2 =
    
    let private calculateCommonBit (mostOrLeast : MostLeast) (index: int) (input: string list) =
        let grouping = 
            input
            |> List.map (fun s -> s.[index])
            |> List.groupBy id
            |> List.sortByDescending (fun (_, l) -> l.Length)
            
        let preference = match mostOrLeast with Most -> '1' | Least -> '0'
            
        let mostAndLeastCommon =
            match grouping with
            | [ a; b ] ->
                match mostOrLeast with
                | _ when (a |> snd |> List.length) = (b |> snd |> List.length) -> preference
                | Most -> a |> fst
                | Least -> b |> fst
            | [ a ] -> a |> fst
            | _ -> failwithf $"Bad input: %A{grouping}"
        
        mostAndLeastCommon
    
    let calculateOxygenCo2Rating (input : string list) =
        let calculateCommonBits (mostOrLeast : MostLeast) =
            let rec loop acc idx =
                match acc with
                | [ a ]-> a
                | _ ->
                    let common = calculateCommonBit mostOrLeast idx acc
                    
                    let remainder = acc |> List.filter (fun s -> s.[idx] = common)
                    loop remainder (idx + 1)
                    
            loop input 0
        
        let oxygen = calculateCommonBits Most |> convertToInt
        let co2 = calculateCommonBits Least |> convertToInt
        
        oxygen * co2