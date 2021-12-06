open AdventOfCode2021
open System
// For more information see https://aka.ms/fsharp-console-apps

let day1Data = File.readAndMapFile "Day1.txt" Int32.Parse
printfn $"Day 1, Part 1: %i{Day1.Part1.calculateNumberOfIncreases day1Data}"
printfn $"Day 1, Part 2: %i{Day1.Part2.calculateIncreasesWindowed 3 day1Data}"

let day2Data = File.readAndMapFile "Day2.txt" id
printfn $"Day 2, Part1: %i{Day2.Part1.calculateDepth day2Data}"
printfn $"Day 2, Part2: %i{Day2.Part2.calculateDepthAndAim day2Data}"

let day3Data = File.readAndMapFile "Day3.txt" id
printfn $"Day 3, Part1: %i{Day3.Part1.calculatePowerConsumption day3Data}"
printfn $"Day 3, Part1: %i{Day3.Part2.calculateOxygenCo2Rating day3Data}"

let numbersToDraw, boards = Day4.readInputFile()
printfn $"Day 4, Part1: %i{Day4.Part1.playBingo numbersToDraw boards}"
printfn $"Day 4, Part1: %i{Day4.Part2.findLastWinner numbersToDraw boards}"

let day5Data = File.readAndMapFile "Day5.txt" Day5.directionFromString
printfn $"Day 5, Part1: %i{Day5.Part1.countOfTwoOverlaps day5Data}"
printfn $"Day 5, Part1: %i{Day5.Part2.countOfTwoOverlaps day5Data}"

let day6Data = File.readAndMapFile "Day6.txt" (fun s -> s.Split(',') |> Array.map Int32.Parse |> Array.toList) |> List.collect id
printfn $"Day 6, Part1: %i{Day6.Part1.simulateFishFor 80 day6Data}"
printfn $"Day 6, Part2: %i{Day6.Part1.simulateFishFor 256 day6Data}"