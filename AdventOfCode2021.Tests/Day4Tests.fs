module AdventOfCode2021.Tests.Day4Tests

open Xunit
open AdventOfCode2021
open AdventOfCode2021.Day4

let sampleData() =
    
    let board1 = [|
        [| 22; 13; 17; 11;  0; |]
        [| 8;  2; 23;  4; 24; |]
        [| 21;  9; 14; 16;  7; |]
        [| 6; 10;  3; 18;  5; |]
        [| 1; 12; 20; 15; 19; |]
    |]
    
    let board2 = [|
        [| 3; 15;  0;  2; 22; |]
        [| 9; 18; 13; 17;  5;  |]
        [| 19;  8;  7; 25; 23; |]
        [| 20; 11; 10; 24;  4; |]
        [| 14; 21; 16; 12;  6; |]
    |]
    
    let board3 = [|
        [| 14; 21; 17; 24;  4; |]
        [| 10; 16; 15;  9; 19; |]
        [| 18;  8; 23; 26; 20; |]
        [| 22; 11; 13;  6;  5; |]
        [| 2;  0; 12;  3;  7;  |]
    |]
    
    [ board1 |> convertToBoard; board2 |> convertToBoard; board3 |> convertToBoard ]
    
let numbersToBeDrawn =
    [7; 4; 9; 5; 11; 17; 23; 2; 0; 14; 21; 24; 10; 16; 13; 6; 15; 25; 12; 22; 18; 20; 8; 19; 3; 26; 1]

[<Fact>]
let ``Day 4, Part 1 works for sample`` () =
    let result = Part1.playBingo numbersToBeDrawn <| sampleData()
    
    Assert.Equal(4512, result)
    

[<Fact>]
let ``Day 4, Part 2 works for sample`` () =
    let result = Part2.findLastWinner numbersToBeDrawn <| sampleData()
    
    Assert.Equal(1924, result)
    
    