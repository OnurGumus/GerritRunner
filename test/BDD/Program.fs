﻿module BDD

open Akkling
open Akkling.Streams
open Akkling.TestKit
open Akka.Actor
open System
open Xunit
open Akkling.Streams.Operators
open Akka.Streams.Dsl
open Akka.Streams
open Akkling.Streams.TestKit.Probes
open Akkling.Streams.TestKit.ManualPublisherProbe
open Akkling.Streams.TestKit.ManualSubscriberProbe
open Akka.TestKit
open System.Threading

let config () = Configuration.parse "akka.logLevel = DEBUG"

let ``Graph DSL operators should work`` () = test (config()) <| fun tck ->
    use mat = tck.Sys.Materializer()
    let probe = manualSubscriberProbe tck
    let oneSec = TimeSpan.FromSeconds 1
    Source.tick oneSec oneSec 1
    |> Source.take 2
    |> Source.runWith mat (Sink.ofSubscriber probe)

    let sub = expectSubscription probe
    sub.Request 1000L
    let sch = tck.Sys.Scheduler :?> TestScheduler

    
    let sleep t = Async.FromContinuations(fun (cont, erFun, _) ->
        sch.ScheduleOnce(t, cont)
    )
    async{
       do! sleep (TimeSpan.FromMilliseconds 2100)
       printf "%A" sch.Now
    }|> Async.Start

    Thread.Sleep(100)
    sch.Advance oneSec
    sch.Advance oneSec
    sch.Advance oneSec
    probe
    |> expectNext 1
    |> expectNext 1
    |> expectComplete
    |> ignore

[<EntryPoint>]
let main v =
    ``Graph DSL operators should work``()
    Console.ReadKey() |> ignore
    0