module AdventOfCode2021.Tests.Day14Tests


open Xunit
open AdventOfCode2021.Day14

let sample = [|
    "NNCB"
    ""
    "CH -> B"
    "HH -> N"
    "CB -> H"
    "NH -> C"
    "HB -> C"
    "HC -> B"
    "HN -> C"
    "NN -> C"
    "BH -> H"
    "NC -> B"
    "NB -> B"
    "BN -> B"
    "BB -> N"
    "BC -> B"
    "CC -> N"
    "CN -> C"
|]


[<Fact>]
let ``Day 14, Part 1 works for sample``() =
    let template, insertions = processInputData sample
    
    let count = Part2.findElementCountFast 10 template insertions
    
    Assert.Equal(1588L, count)
    

[<Fact>]
let ``Day 14, Part 2 works for sample``() =
    let template, insertions = processInputData sample
    
    let count = Part2.findElementCountFast 40 template insertions
    
    Assert.Equal(2188189693529L, count)
    