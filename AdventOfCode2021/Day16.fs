module AdventOfCode2021.Day16

open System

type Literal = {
    Version : int
    PacketTypeId : int
    Value : uint64
    LocationEnd : int
}

type Operator = {
    Version : int
    PacketTypeId : int
    LengthTypeId : int
    LocationEnd : int
}

type Packet =
    | Literal of Literal
    | Operator of Operator * Packet list
    
[<RequireQualifiedAccess>]
module private Packet =
    let rec lastLocation (packet : Packet) =
        match packet with
        | Literal l -> l.LocationEnd
        | Operator(_, packets) ->
            packets
            |> List.map lastLocation
            |> List.max
    
    let rec lastLocations (packets : Packet list) =
        packets |> List.map lastLocation |> List.max
        
    let rec versionNumberSum (packets : Packet list) =
        packets
        |> List.sumBy (fun p ->
            match p with
            | Literal literal -> literal.Version
            | Operator(operator, packets) -> operator.Version + (versionNumberSum packets))
    
let private convertBinaryStringToUInt64 str = Convert.ToUInt64(str, 2)
let private convertBinaryStringToInt str = Convert.ToInt32(str, 2)

let private convertHexToBinary (hex : string) =
    let convertChar c =
        Convert.ToString(Convert.ToInt32(c, 16), 2).PadLeft(4, '0')
        
    hex
    |> Seq.map (fun c -> c |> string |> convertChar)
    |> String.Concat
    
let rec private parseHex (hex : string) =
    let program = convertHexToBinary hex
    
    let rec emitLiteralValues (loc : int) (acc : string list) (isLast : bool) =
        if isLast then (acc |> String.Concat |> convertBinaryStringToUInt64), loc else
        let last = program[loc] = '0'
        let value = program[loc..loc + 4]
        emitLiteralValues (loc + 5) (acc |> List.insertAtEnd value) last
    
    let rec innerParse (pc : int) (emitted : Packet list) (haltAt : int)=
        if pc >= haltAt then emitted 
        else
            
        let packetVersion = program[pc..pc + 2] |> convertBinaryStringToInt
        let packetTypeId = program[pc + 3..pc + 5] |> convertBinaryStringToInt
        
        match packetTypeId with
        | 0 when program[pc..] |> Seq.forall (fun c -> c = '0') -> emitted
            
        | 4 ->
            let value, pcPos  = emitLiteralValues (pc + 6) [] false
            
            let literal = { Version = packetVersion
                            PacketTypeId = packetTypeId
                            Value = value
                            LocationEnd = pcPos } |> Literal
            
            (emitted |> List.insertAtEnd literal)
            
        | _ when program[pc + 6] = '0' ->
            let bitsInSubPackets = program[pc + 7..pc + 21] |> convertBinaryStringToInt
            
            let rec generatePackets subPc halt acc =
                match innerParse subPc [] halt |> List.tryHead with
                | Some p -> generatePackets (Packet.lastLocation p) halt (acc |> List.insertAtEnd p)
                | None -> acc
            
            let subPackets = generatePackets (pc + 22) (pc + 22 + bitsInSubPackets) []
            
            let operator = 
                ({ Version = packetVersion
                   PacketTypeId = packetTypeId
                   LengthTypeId = 0
                   LocationEnd = (pc + 22 + bitsInSubPackets) }, subPackets) |> Operator
            (emitted |> List.insertAtEnd operator)
            
        | _ when program[pc + 6] = '1' ->
            let numberOfSubPackets = program[pc + 7..pc + 17] |> convertBinaryStringToInt
            
            let mutable subPc = pc + 18
            let subPackets = [ for _ = 1 to numberOfSubPackets do
                                  let p = innerParse subPc [] haltAt |> List.head
                                  subPc <- Packet.lastLocation p
                                  p ]

            let lastLocation = Packet.lastLocations subPackets
            
            let operator = 
                ({ Version = packetVersion
                   PacketTypeId = packetTypeId
                   LengthTypeId = 0
                   LocationEnd = lastLocation }, subPackets) |> Operator
            (emitted |> List.insertAtEnd operator)
            
        | _ -> invalidArg "packetTypeId" $"{packetTypeId}"
    
    innerParse 0 [] program.Length
    
[<RequireQualifiedAccess>]
module Part1 =
    
    let calculateVersionSum (hex : string) =
        parseHex hex |> Packet.versionNumberSum
    
[<RequireQualifiedAccess>]
module Part2 =
    
    let evaluate (hex : string) =
        let packets = parseHex hex
        let packets = packets |> List.head
        
        let rec evalPacket (packet : Packet) =
            match packet with
            | Operator(operator, packets) when operator.PacketTypeId = 0 ->
                packets |> List.sumBy evalPacket
                
            | Operator(operator, packets) when operator.PacketTypeId = 1 ->
                packets |> List.map evalPacket |> List.reduce (*)
            
            | Operator(operator, packets) when operator.PacketTypeId = 2 ->
                packets |> List.map evalPacket |> List.min
                
            | Operator(operator, packets) when operator.PacketTypeId = 3 ->
                packets |> List.map evalPacket |> List.max
            
            | Operator(operator, [ p1; p2 ] ) when operator.PacketTypeId = 5 ->
                if (evalPacket p1) > (evalPacket p2) then 1UL else 0UL
                
            | Operator(operator, [ p1; p2 ] ) when operator.PacketTypeId = 6 ->
                if (evalPacket p1) < (evalPacket p2) then 1UL else 0UL
            
            | Operator(operator, [ p1; p2 ] ) when operator.PacketTypeId = 7 ->
                if (evalPacket p1) = (evalPacket p2) then 1UL else 0UL
                
            | Literal l -> l.Value
            
            | _ -> failwith "Unknown?!"
        
        packets |> evalPacket
    ()