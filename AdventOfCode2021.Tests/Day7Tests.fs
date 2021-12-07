module AdventOfCode2021.Tests.Day7Tests

open System
open Xunit
open AdventOfCode2021.Day7

let sampleData = [ 16; 1; 2; 0; 4; 2; 7; 1; 2; 14 ]


[<Fact>]
let ``Day 7, Part 1 works for sample``() =
    let count = Part1.calculateFuelCost sampleData
    
    Assert.Equal(37, count)


[<Fact>]
let ``Day 7, Part 2 works for sample``() =
    let count = Part2.calculateFuelCost sampleData
    
    Assert.Equal(168, count)
