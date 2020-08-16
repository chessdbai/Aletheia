module Aletheia.Pgn.Parser.TimeControl

open System
open FParsec

type TimeControlState = unit

type TimeControlPeriod = {
    Moves: int
    Seconds: int
}

type SuddenDeathPeriod = {
    Seconds: int
}

type IncrementalPeriod = {
    StartingSeconds: int
    IncrementSeconds: int
}

type HourglassPeriod = {
    TimerSeconds: int
}

type TimeControlDescriptor =
    | Unknown of unit
    | Untimed of unit
    | TimeControlPeriod of TimeControlPeriod
    | SuddenDeathTime of SuddenDeathPeriod
    | Incremental of IncrementalPeriod
    | Hourglass of HourglassPeriod

type TimeControl = TimeControlDescriptor list

let unknownTcp =
    pchar '?' |>>
    fun _ ->
        TimeControlDescriptor.Unknown()

let untimedTcp =
    skipChar '-'
    |>> fun _ -> TimeControlDescriptor.Untimed()

let periodTcp =
    pint32 .>>?
    skipChar '/' .>>.
    pint32 |>>
    fun (moves,seconds) -> TimeControlDescriptor.TimeControlPeriod({
        Moves=moves
        Seconds=seconds
    })

let suddenDeathTcp =
    pint32 |>>
    fun seconds ->
        TimeControlDescriptor.SuddenDeathTime({
            Seconds=seconds
        })

let incrementalTcp =
    pint32 .>>?
    skipChar '+' .>>.?
    pint32 |>>
    fun (startingSeconds, incrementSeconds) ->
        TimeControlDescriptor.Incremental({
            StartingSeconds=startingSeconds
            IncrementSeconds=incrementSeconds
        })

let hourglassTcp =
    skipChar '*' >>.
    pint32 |>>
    fun seconds ->
        TimeControlDescriptor.Hourglass({
            TimerSeconds=seconds
        })

let timeControlDescriptorParser : Parser<TimeControlDescriptor, TimeControlState> = choice [
    unknownTcp
    untimedTcp
    periodTcp
    incrementalTcp
    hourglassTcp
    suddenDeathTcp
]

exception TimeControlFormatException of string

let descriptorSeparator = skipChar ':'
let timeControlParser = sepEndBy timeControlDescriptorParser descriptorSeparator
let parseTimeControl timeControlText =
    match run timeControlParser timeControlText with
    | Success (res, _, _) -> res
    | Failure (err, _, _) -> raise(TimeControlFormatException(err))
