module AdventOfCode2021.Tests.Day3Tests

open Xunit
open AdventOfCode2021

let input = [
    "00100"
    "11110"
    "10110"
    "10111"
    "10101"
    "01111"
    "00111"
    "11100"
    "10000"
    "11001"
    "00010"
    "01010"
]

[<Fact>]
let ``Day 3, Part 1 works for sample`` () =
    let powerConsumption = Day3.Part1.calculatePowerConsumption input
    
    Assert.Equal(198, powerConsumption)
    
[<Fact>]
let ``Day 3, Part 2 works for sample`` () =
    let oxygenCo2 = Day3.Part2.calculateOxygenCo2Rating input
    
    Assert.Equal(230, oxygenCo2)
    