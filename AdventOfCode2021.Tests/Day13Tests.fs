module AdventOfCode2021.Tests.Day13Tests

open Xunit
open AdventOfCode2021.Day13

let sample = [|
    "6,10"
    "0,14"
    "9,10"
    "0,3"
    "10,4"
    "4,11"
    "6,0"
    "6,12"
    "4,1"
    "0,13"
    "10,12"
    "3,4"
    "3,0"
    "8,4"
    "1,10"
    "2,14"
    "8,10"
    "9,0"
    ""
    "fold along y=7"
    "fold along x=5"
|]


[<Fact>]
let ``Day 13, Part 1 works for sample``() =
    let xys, folds = processInputData sample
    
    let count = Part1.calculateVisibleDots xys folds
    
    Assert.Equal(17, count)
    