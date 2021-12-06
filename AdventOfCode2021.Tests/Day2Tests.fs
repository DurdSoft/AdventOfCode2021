module AdventOfCode2021.Tests.Day2

open Xunit
open AdventOfCode2021

let instructions = [
    "forward 5"
    "down 5"
    "forward 8"
    "up 3"
    "down 8"
    "forward 2"
]

[<Fact>]
let ``Day 2, Part 1 works for sample``() =
    
    let depth = Day2.Part1.calculateDepth instructions
    
    Assert.Equal(150, depth)
    

[<Fact>]
let ``Day 2, Part 2 works for sample``() =
    
    let result = Day2.Part2.calculateDepthAndAim instructions
    
    Assert.Equal(900, result)