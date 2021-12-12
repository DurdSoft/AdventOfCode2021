module AdventOfCode2021.Tests.Day12Tests


open Xunit
open AdventOfCode2021.Day12


let sample = [
    "fs-end"
    "he-DX"
    "fs-he"
    "start-DX"
    "pj-DX"
    "end-zg"
    "zg-sl"
    "zg-pj"
    "pj-he"
    "RW-he"
    "fs-DX"
    "pj-RW"
    "zg-RW"
    "start-pj"
    "he-WI"
    "zg-he"
    "pj-fs"
    "start-RW"
]

[<Fact>]
let ``Day 12, Part 1 works for sample``() =
    let paths = buildAdjacency sample |> Part1.calculateNumberOfPaths
    
    Assert.Equal(226, paths)
    

[<Fact>]
let ``Day 12, Part 2 works for sample``() =
    let paths = buildAdjacency sample |> Part2.calculateNumberOfPaths
    
    Assert.Equal(3509, paths)
