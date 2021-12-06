module AdventOfCode2021.Tests.Day1

open Xunit
open AdventOfCode2021

let sampleInput = [
    199
    200
    208
    210
    200
    207
    240
    269
    260
    263
]
    
[<Fact>]
let ``Day 1, Part 1 works for sample`` () =
    
    let answer = Day1.Part1.calculateNumberOfIncreases sampleInput

    Assert.Equal(7, answer)
    

[<Fact>]
let ``Day 1, Part 2 works for sample`` () =
    
    let answer = Day1.Part2.calculateIncreasesWindowed 3 sampleInput

    Assert.Equal(5, answer)