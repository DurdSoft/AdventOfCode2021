module AdventOfCode2021.Tests.Day15Tests

open Xunit
open AdventOfCode2021.Day15

let sample = [|
    "1163751742"
    "1381373672"
    "2136511328"
    "3694931569"
    "7463417111"
    "1319128137"
    "1359912421"
    "3125421639"
    "1293138521"
    "2311944581"
|]


[<Fact>]
let ``Day 15, Part 1 works for sample``() =
    let cost = processData sample
    
    let cost = Part1.findPathCost cost
    
    Assert.Equal(40, cost)
    

[<Fact>]
let ``Day 15, Part 2 works for sample``() =
    let cost = processData sample
    
    let cost = Part2.findPathCost cost
    
    Assert.Equal(315, cost)
