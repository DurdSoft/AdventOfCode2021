open AdventOfCode2021
open System
// For more information see https://aka.ms/fsharp-console-apps
let sw = System.Diagnostics.Stopwatch.StartNew()
let elapsed() =
    let e = sw.Elapsed
    sw.Restart()
    e

let day1Data = File.readAndMapFile "Day1.txt" Int32.Parse
printfn $"[{elapsed()}] Day 1, Part 1: %i{Day1.Part1.calculateNumberOfIncreases day1Data}"
printfn $"[{elapsed()}] Day 1, Part 2: %i{Day1.Part2.calculateIncreasesWindowed 3 day1Data}"

let day2Data = File.readAndMapFile "Day2.txt" id
printfn $"[{elapsed()}] Day 2, Part 1: %i{Day2.Part1.calculateDepth day2Data}"
printfn $"[{elapsed()}] Day 2, Part 2: %i{Day2.Part2.calculateDepthAndAim day2Data}"

let day3Data = File.readAndMapFile "Day3.txt" id
printfn $"[{elapsed()}] Day 3, Part 1: %i{Day3.Part1.calculatePowerConsumption day3Data}"
printfn $"[{elapsed()}] Day 3, Part 1: %i{Day3.Part2.calculateOxygenCo2Rating day3Data}"

let numbersToDraw, boards = Day4.readInputFile()
printfn $"[{elapsed()}] Day 4, Part 1: %i{Day4.Part1.playBingo numbersToDraw boards}"
printfn $"[{elapsed()}] Day 4, Part 1: %i{Day4.Part2.findLastWinner numbersToDraw boards}"

let day5Data = File.readAndMapFile "Day5.txt" Day5.directionFromString
printfn $"[{elapsed()}] Day 5, Part 1: %i{Day5.Part1.countOfTwoOverlaps day5Data}"
printfn $"[{elapsed()}] Day 5, Part 1: %i{Day5.Part2.countOfTwoOverlaps day5Data}"

let day6Data = File.readAndMapFile "Day6.txt" (fun s -> s.Split(',') |> Array.map Int32.Parse |> Array.toList) |> List.collect id
printfn $"[{elapsed()}] Day 6, Part 1: %i{Day6.Part1And2.simulateFishFor 80 day6Data}"
printfn $"[{elapsed()}] Day 6, Part 2: %i{Day6.Part1And2.simulateFishFor 256 day6Data}"

let day7Data = File.readAndMapFile "Day7.txt" (fun s -> s.Split(',') |> Array.map Int32.Parse |> Array.toList) |> List.collect id
printfn $"[{elapsed()}] Day 7, Part 1: %i{Day7.Part1.calculateFuelCost day7Data}"
printfn $"[{elapsed()}] Day 7, Part 2: %i{Day7.Part2.calculateFuelCost day7Data}"

let day8Data = File.readAndMapFile "Day8.txt" Day8.processInputRow
printfn $"[{elapsed()}] Day 8, Part 1: %i{Day8.Part1.calculateDigitCounts (day8Data |> List.collect snd)}"
printfn $"[{elapsed()}] Day 8, Part 2: %i{Day8.Part2.calculateCount day8Data}"

let day9Data = File.readAndMapFile "Day9.txt" id |> Day9.mapFile
printfn $"[{elapsed()}] Day 9, Part 1: %i{Day9.Part1.calculateRiskLevel day9Data}"
printfn $"[{elapsed()}] Day 9, Part 2: %i{Day9.Part2.calculate3LargestBasins day9Data}"

let day10Data = File.readAndMapFile "Day10.txt" Day10.processRow
printfn $"[{elapsed()}] Day 10, Part 1: %i{Day10.Part1.calculateSyntaxErrorCost day10Data}"
printfn $"[{elapsed()}] Day 10, Part 2: %i{Day10.Part2.calculateIncompleteCost day10Data}"

let day11Data = File.readAndMapFile "Day11.txt" id |> Day11.mapFile
printfn $"[{elapsed()}] Day 11, Part 1: %i{Day11.Part1.calculateNumberOfFlashes 100 day11Data}"
printfn $"[{elapsed()}] Day 11, Part 2: %i{Day11.Part2.findFirstGenerationSync day11Data}"

let day12Data = File.readAndMapFile "Day12.txt" id |> Day12.buildAdjacency
printfn $"[{elapsed()}] Day 12, Part 1: %i{Day12.Part1.calculateNumberOfPaths day12Data}"
printfn $"[{elapsed()}] Day 12, Part 2: %i{Day12.Part2.calculateNumberOfPaths day12Data}"

let points, folds = System.IO.File.ReadAllLines "Day13.txt" |> Day13.processInputData
printfn $"[{elapsed()}] Day 13, Part 1: %i{Day13.Part1.calculateVisibleDots points folds}"
printfn $"[{elapsed()}] Day 13, Part 2:"; Day13.Part2.calculateCharactersAndPrint points folds

let templates, instructions = System.IO.File.ReadAllLines "Day14.txt" |> Day14.processInputData
printfn $"[{elapsed()}] Day 14, Part 1: %i{Day14.Part2.findElementCountFast 10 templates instructions}"
printfn $"[{elapsed()}] Day 14, Part 2: %i{Day14.Part2.findElementCountFast 40 templates instructions}"

let costLookup = System.IO.File.ReadAllLines "Day15.txt" |> Day15.processData
printfn $"[{elapsed()}] Day 15, Part 1: %i{Day15.Part1.findPathCost costLookup}"
printfn $"[{elapsed()}] Day 15, Part 2: %i{Day15.Part2.findPathCost costLookup}"
