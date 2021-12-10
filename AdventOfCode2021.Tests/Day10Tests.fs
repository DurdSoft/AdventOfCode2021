module AdventOfCode2021.Tests.Day10Tests

open Xunit
open AdventOfCode2021.Day10

let sample = [
    "[({(<(())[]>[[{[]{<()<>>"
    "[(()[<>])]({[<{<<[]>>("
    "{([(<{}[<>[]}>{[]{[(<()>"
    "(((({<>}<{<{<>}{[]{[]{}"
    "[[<[([]))<([[{}[[()]]]"
    "[{[{({}]{}}([{[{{{}}([]"
    "{<[[]]>}<{[{[{[]{()[[[]"
    "[<(<(<(<{}))><([]([]()"
    "<{([([[(<>()){}]>(<<{{"
    "<{([{{}}[<[[[<>{}]]]>[]]" ]

[<Fact>]
let ``Day 10, Part 1 works for sample``() =
    let score = sample
                |> List.map processRow
                |> Part1.calculateSyntaxErrorCost
    
    Assert.Equal(26397, score)
    
[<Fact>]
let ``Day 10, Part 2 works for sample``() =
    let score = sample
                |> List.map processRow
                |> Part2.calculateIncompleteCost
    
    Assert.Equal(288957L, score)
