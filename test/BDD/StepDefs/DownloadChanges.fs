module StepDefs.Signup

open TickSpec
open System.Threading
open System.IO
open Akkling
open Expecto
open Microsoft.Extensions.Configuration
open Hocon.Extensions.Configuration
open Akkling.Streams

[<Given>]
let ``(.*) changes fetched`` (n:int) =
    ()

[<When>]
let ``the download flow started`` () =
    ()

[<Then>]
let ``(.*) changes should be downloaded`` (n:int) =
    ()

[<When>]
let ``(.*) new change arrives`` (n:int) =
    ()

[<When>]
let ``after some long time passes`` () =
    ()

[<Then>]
let ``total of (.*) changes should be fetched`` (n:int) =
    ()