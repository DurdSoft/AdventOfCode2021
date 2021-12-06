module AdventOfCode2021.Day4

open System

type BoardNumber =
    { Value: int
      Drawn: bool }

type Board =
    { Values: BoardNumber array array }
    with member this.Column idx =
            [| for i = 0 to this.Values.Length - 1 do
                  this.Values.[i].[idx] |]
        
         member this.Row idx =
             this.Values.[idx]
             
         member this.ColumnLength with get() =
             this.Values.[0].Length
             
         member this.RowLength with get() =
             this.Values.Length

         member this.DrawValue value =
            for r = 0 to this.RowLength - 1 do
                for c = 0 to this.ColumnLength - 1 do
                    let cell = this.Values.[r].[c]
                    if cell.Value = value then
                        this.Values.[r].[c] <- { cell with Drawn = true }

let convertToBoard (input : int array array) =
    let numbers = 
        [| for i = 0 to input.Length - 1 do
               input.[i] |> Array.map (fun n -> { Value = n; Drawn = false }) |]
    { Values = numbers }
    

let private hasWon (board : Board) =
    let rec rowsWon idx =
        if idx = board.RowLength then false
        elif board.Row idx |> Array.forall (fun n -> n.Drawn) then true
        else rowsWon (idx + 1)
    
    let rec columnWon idx =
        if idx = board.ColumnLength then false
        elif board.Column idx |> Array.forall (fun n -> n.Drawn) then true
        else columnWon (idx + 1)
    
    rowsWon 0 || columnWon 0

    
let readInputFile() =
    let contents = System.IO.File.ReadAllLines "Day4.txt"
    let drawNumbers = (contents |> Array.head).Split(',')
                      |> Array.map Int32.Parse
                      |> Array.toList
                      
    let boards = contents
                 |> Array.skip 2
                 |> Array.filter (fun r -> r <> "")
                 |> Array.map (fun s -> s.Split(' ')
                                        |> Array.filter (fun c -> c <> "")
                                        |> Array.map Int32.Parse)
                 |> Array.chunkBySize 5
                 |> Array.map convertToBoard
                 |> Array.toList
    
    drawNumbers, boards

[<RequireQualifiedAccess>]
module Part1 =

    let playBingo (numbersToDraw : int list) (boards : Board list) =
        let rec findWinningValue remainingNumbers =
            match remainingNumbers with
            | h :: tail ->
                boards |> List.iter (fun b -> b.DrawValue h)
                let winnerOpt = boards |> List.tryFind hasWon
                
                match winnerOpt with
                | Some b ->
                    let sum = b.Values
                                    |> Array.collect (fun v ->
                                        v
                                        |> Array.filter (fun n -> not n.Drawn)
                                        |> Array.map (fun n -> n.Value))
                                    |> Array.sum
                    sum * h
                | None -> findWinningValue tail
            | _ -> failwith "No winners :("
                
        findWinningValue numbersToDraw
    
[<RequireQualifiedAccess>]
module Part2 =
    type WinningResult =
        { Board: Board
          WinningNumber: int }
    
    let findLastWinner (numbersToDraw : int list) (boards : Board list) =
        let rec lastWinner (remainingNumbers : int list) (boards : Board list) (previousWinner : WinningResult option) =
            match remainingNumbers, boards with
            | _, [ ] -> previousWinner
            | h :: tail, _ ->
                boards |> List.iter (fun b -> b.DrawValue h)
                let winnerOpt = boards |> List.tryFind hasWon
                
                match winnerOpt with
                | Some b -> lastWinner tail (boards |> List.filter (hasWon >> not)) (Some { Board = b; WinningNumber = h })
                | None -> lastWinner tail boards previousWinner
            | _ -> previousWinner
                
        let lastToWin = lastWinner numbersToDraw boards None
        
        match lastToWin with
        | Some result ->
            
            let sum = result.Board.Values
                      |> Array.collect (fun v ->
                          v
                          |> Array.filter (fun n -> not n.Drawn)
                          |> Array.map (fun n -> n.Value))
                      |> Array.sum
            sum * result.WinningNumber
        | None -> failwith "No winners at all :("