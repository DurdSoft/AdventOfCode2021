module AdventOfCode2021.Tests.Day9Tests

open System
open Xunit
open AdventOfCode2021.Day9

let sampleData = [
    "2199943210"
    "3987894921"
    "9856789892"
    "8767896789"
    "9899965678" ]


[<Fact>]
let ``Day 9, Part 1 works for sample``() =
    
    let count = mapFile sampleData |> Part1.calculateRiskLevel
    
    Assert.Equal(15, count)
    
[<Fact>]
let ``Day 9, Part 2 works for sample``() =
    
    let count = mapFile sampleData |> Part2.calculate3LargestBasins
    
    Assert.Equal(1134, count)
    