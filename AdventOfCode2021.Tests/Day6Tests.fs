module AdventOfCode2021.Tests.Day6Tests

open Xunit
open AdventOfCode2021
open AdventOfCode2021.Day6

let sampleData = [ 3; 4; 3; 1; 2 ]

[<Fact>]
let ``Day 6, Part 1 works for sample``() =
    let count = Part1And2.simulateFishFor 18 sampleData
    
    Assert.Equal(26L, count)


[<Fact>]
let ``Day 6, Part 2 works for sample``() =
    let count = Part1And2.simulateFishFor 256 sampleData
    
    Assert.Equal(26984457539L, count)
