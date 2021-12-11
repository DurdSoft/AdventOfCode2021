module AdventOfCode2021.Tests.Day11Tests

open System
open Xunit
open AdventOfCode2021.Day11

let sample = [
    "5483143223"
    "2745854711"
    "5264556173"
    "6141336146"
    "6357385478"
    "4167524645"
    "2176841721"
    "6882881134"
    "4846848554"
    "5283751526"
]


[<Fact>]
let ``Day 11, Part 1 works for sample``() =
    let score = sample
                |> mapFile
                |> Part1.calculateNumberOfFlashes 10
    
    Assert.Equal(204, score)
    

[<Fact>]
let ``Day 11, Part 2 works for sample``() =
    let gen = sample
              |> mapFile
              |> Part2.findFirstGenerationSync
    
    Assert.Equal(195, gen)
    