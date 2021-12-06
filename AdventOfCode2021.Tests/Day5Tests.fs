module AdventOfCode2021.Tests.Day5Tests

open Xunit
open AdventOfCode2021
open AdventOfCode2021.Day5

let private sample = [
    "0,9 -> 5,9"
    "8,0 -> 0,8"
    "9,4 -> 3,4"
    "2,2 -> 2,1"
    "7,0 -> 7,4"
    "6,4 -> 2,0"
    "0,9 -> 2,9"
    "3,4 -> 1,4"
    "0,0 -> 8,8"
    "5,5 -> 8,2"
]

[<Fact>]
let ``Day 5, Part 1 works for sample``() =
    let directions = sample |> List.map directionFromString
    
    let count = Part1.countOfTwoOverlaps directions
    
    Assert.Equal(5, count)

[<Fact>]
let ``Day 5, Part 2 works for sample``() =
    let directions = sample |> List.map directionFromString
    
    let count = Part2.countOfTwoOverlaps directions
    
    Assert.Equal(12, count)
