module TimeControlTests

open System
open FParsec
open Xunit
open TestResources
open Aletheia.Pgn.Parser.TimeControl

module TimeControls =
    let Unspecified = "?"
    let Untimed = "-"
    let Period = "40/3600"
    let SuddenDeath = "420"
    let Increment = "600+10"
    let Hourglass = "*180"
    let CompoundTimeControl = "40/7200:20/3600:900+30"

let parseTimeControl input =
    match run timeControlParser input with
    | Success (res, _, _) -> res
    | Failure (err, _, _) -> raise(Exception(err))

[<Fact>]
let ``Can parse unspecified time control`` () =
    let tc = parseTimeControl TimeControls.Unspecified
    Assert.Equal(1, tc.Length)
    let correctToken =
        match tc.Head with
        | TimeControlDescriptor.Unknown _ -> true
        | _ -> false
    Assert.True(correctToken)

[<Fact>]
let ``Can parse untimed time control`` () =
    let tc = parseTimeControl TimeControls.Untimed
    Assert.Equal(1, tc.Length)
    let correctToken =
        match tc.Head with
        | TimeControlDescriptor.Untimed _ -> true
        | _ -> false
    Assert.True(correctToken)

[<Fact>]
let ``Can parse time control period`` () =
    let tc = parseTimeControl TimeControls.Period
    Assert.Equal(1, tc.Length)
    match tc.Head with
        | TimeControlDescriptor.TimeControlPeriod record ->
            Assert.Equal(40, record.Moves)
            Assert.Equal(3600, record.Seconds)
        | _ -> raise(Exception("Invalid TimeControlDescriptor type."))

[<Fact>]
let ``Can parse sudden death time control`` () =
    let tc = parseTimeControl TimeControls.SuddenDeath
    Assert.Equal(1, tc.Length)
    match tc.Head with
        | TimeControlDescriptor.SuddenDeathTime record ->
            Assert.Equal(420, record.Seconds)
        | _ -> raise(Exception("Invalid TimeControlDescriptor type."))

[<Fact>]
let ``Can parse increment time control`` () =
    let tc = parseTimeControl TimeControls.Increment
    Assert.Equal(1, tc.Length)
    match tc.Head with
        | TimeControlDescriptor.Incremental record ->
            Assert.Equal(600, record.StartingSeconds)
            Assert.Equal(10, record.IncrementSeconds)
        | _ -> raise(Exception("Invalid TimeControlDescriptor type."))

[<Fact>]
let ``Can parse hourglass time control`` () =
    let tc = parseTimeControl TimeControls.Hourglass
    Assert.Equal(1, tc.Length)
    match tc.Head with
        | TimeControlDescriptor.Hourglass record ->
            Assert.Equal(180, record.TimerSeconds)
        | _ -> raise(Exception("Invalid TimeControlDescriptor type."))

[<Fact>]
let ``Can parse time control with multiple periods`` () =
    let tc = parseTimeControl TimeControls.CompoundTimeControl
    Assert.Equal(3, tc.Length)
    match tc.Item 0 with
        | TimeControlDescriptor.TimeControlPeriod record ->
            Assert.Equal(40, record.Moves)
            Assert.Equal(7200, record.Seconds)
        | _ -> raise(Exception("Invalid TimeControlDescriptor type."))
    match tc.Item 1 with
        | TimeControlDescriptor.TimeControlPeriod record ->
            Assert.Equal(20, record.Moves)
            Assert.Equal(3600, record.Seconds)
        | _ -> raise(Exception("Invalid TimeControlDescriptor type."))
    match tc.Item 2 with
        | TimeControlDescriptor.Incremental record ->
            Assert.Equal(900, record.StartingSeconds)
            Assert.Equal(30, record.IncrementSeconds)
        | _ -> raise(Exception("Invalid TimeControlDescriptor type."))

