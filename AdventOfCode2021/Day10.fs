module AdventOfCode2021.Day10

open System.Collections.Generic

type SystemChar =
    private
    | OpeningChar of char
    | ClosingChar of char

let private openingChars = [ '['; '('; '{'; '<' ]

let processRow (input : string) =
    input
    |> Seq.map (fun c -> if openingChars |> List.contains c then OpeningChar c else ClosingChar c)
    |> Seq.toList

let private charMap = [ ('(', ')'); ('[', ']'); ('{', '}'); ('<', '>'); ] |> readOnlyDict

let private tryFindIllegalCharacter (input : SystemChar list) =
    let opening = Stack<char>()
    
    let rec tryFindIllegalCharacterLoop (remaining : SystemChar list) =
        match remaining with
        | [] -> Error opening
        | h :: tail ->
            match h with
            | OpeningChar c ->
                opening.Push(c)
                tryFindIllegalCharacterLoop tail
            | ClosingChar c ->
                let bracketToClose = opening.Pop()
                if c <> charMap[bracketToClose] then
                    Ok c
                else
                    tryFindIllegalCharacterLoop tail
            
    let charOpt = tryFindIllegalCharacterLoop input
    charOpt


[<RequireQualifiedAccess>]
module Part1 =
    let private syntaxCost = [ (')', 3); (']', 57); ('}', 1197); ('>', 25137); ] |> readOnlyDict
    
    let calculateSyntaxErrorCost (input : SystemChar list list) =
        input
        |> List.map tryFindIllegalCharacter
        |> List.map (fun x -> match x with Ok c -> Some c | Error _ -> None)
        |> List.choose id
        |> List.groupBy id
        |> List.map snd
        |> List.map (fun chars -> chars.Length * syntaxCost[chars |> List.head])
        |> List.sum

[<RequireQualifiedAccess>]
module Part2 =
    
    let private syntaxCost = [ (')', 1L); (']', 2L); ('}', 3L); ('>', 4L); ] |> readOnlyDict
    
    let private calculateIncompleteCostForLine (stack : Stack<_>) =
        stack
        |> Seq.toList
        |> List.fold (fun s t -> 5L * s + (syntaxCost[charMap[t]])) 0L
    
    let calculateIncompleteCost (input : SystemChar list list) =
        let scores = 
            input
            |> List.map (fun l -> match tryFindIllegalCharacter l with Ok _ -> None | Error stack -> Some stack)
            |> List.choose id
            |> List.map calculateIncompleteCostForLine
            |> List.sort
            
        scores |> List.item (scores.Length / 2)

