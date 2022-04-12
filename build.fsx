#r "paket:
nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.Core.Target //"
#load ".fake/build.fsx/intellisense.fsx"
open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators

let serverPath =
    Path.getFullName "./src/Server"

let automationPath =
    Path.getFullName "./test/Automation"

Target.initEnvironment ()
let runDotNet cmd workingDir =
    let result =
        DotNet.exec (DotNet.Options.withWorkingDirectory workingDir) cmd ""

    if result.ExitCode <> 0
    then failwithf "'dotnet %s' failed in %s" cmd workingDir

Target.create "Clean" (fun _ ->
    !! "src/**/bin"
    ++ "src/**/obj"
    |> Shell.cleanDirs 
)
Target.create "Build" (fun _ ->
    !! "src/**/*.*proj"
    |> Seq.iter (DotNet.build id)
)

Target.create "WatchServer" (fun _ -> runDotNet "watch run" serverPath)

Target.create "Automation" (fun _ -> runDotNet "run" automationPath)

Target.create "Default" ignore

"Clean"
  ==> "Build"
  ==> "WatchServer" 
  ==> "Default"

"Build" ==> "Automation"

Target.runOrDefault "Default"
