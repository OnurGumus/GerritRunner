module Server.App


open System
open Akka.Streams
open Akka.Streams.Dsl
open Akkling
open Akkling.Streams
open Akkling.Streams.Operators
open Akka
open System.Linq
open System.Threading.Tasks
open FSharp.Data
open FsHttp
module Domain = 
    type Config = NA
    type Source = Source<Config,Actor.ICancelable> 
    [<Literal>]
    let ResolutionFolder = __SOURCE_DIRECTORY__

    type Gerrit = JsonProvider<"data/gerrit.json", ResolutionFolder=ResolutionFolder>
 
    type Query = Config -> Async<Gerrit.Root>

    type DownloadCommand  = DownloadCommand
    type Path = Path
    type Download =  DownloadCommand -> Async<Path>
    
    //type Flow = Flow<

let system =
    System.create "streams-sys"
    <| Configuration.defaultConfig ()

let mat = system.Materializer()
//let source: Source<Config,Actor.ICancelable> = failwith ""// Source.tick TimeSpan.Zero (TimeSpan.FromSeconds 1) NA

// let f = Flow.empty |> Flow.asyncMapUnordered (fun x ->)     
//        //|> Async.RunSynchronously

[<EntryPoint>]
let main args =
  //  printf "%A" max
    System.Console.ReadKey() |> ignore
    0