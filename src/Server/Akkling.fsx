#r "nuget: Akka.Serialization.Hyperion"
#r "nuget: Akka.Streams"
#r "nuget: Akkling"
#r "nuget: Akkling.Streams"

open System
open Akka.Streams
open Akka.Streams.Dsl
open Akkling
open Akkling.Streams
open Akkling
open Akkling.Streams
open Akkling.Streams.Operators
open Akka.Actor
open System
open Akka.Streams.Dsl
open Akka.Streams


let system =
    System.create "streams-sys"
    <| Configuration.defaultConfig ()

let mat = system.Materializer()

/// VISUAL Explanation:
(**
                +-------------------------------+
                |           pickMaxOf3          |
                |  +----------+                 |
      zip1.in0 ====>          |   +---------+   |
                |  |   zip1   ====>         |   |
      zip1.in1 ====>          |   |         |   |
                |  +----------+   |   zip2  ====> zip2.out
      zip2.in1 ===================>         |   |
                |                 +---------+   |
                +-------------------------------+
    *)
let pickMaxOf3 :IGraph<UniformFanInShape<int,int>,obj> =
    Graph.create
    <| fun b ->
        graph b {
            let! zip1 = ZipWith.create max<int>
            let! zip2 = ZipWith.create max<int>

            b.From zip1.Out =>> zip2.In0 |> ignore

            return UniformFanInShape(zip2.Out, zip1.In0, zip1.In1, zip2.In1)
        }



let max =
    Sink.head<int>
    |> Graph.create1 (fun b sink ->
        graph b {
            let! pm3 = pickMaxOf3

            let! s1 = Source.singleton 1
            let! s2 = Source.singleton 2
            let! s3 = Source.singleton 3

            b.From s1 =>> pm3.In(0) |> ignore
            b.From s2 =>> pm3.In(1) |> ignore
            b.From s3 =>> pm3.In(2) |> ignore

            b.From(pm3.Out) =>> sink |> ignore
        })
    |> Graph.runnable
    |> Graph.run mat
    |> Async.RunSynchronously

printf "%i" max
