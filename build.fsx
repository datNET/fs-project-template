#load @"./._fake/loader.fsx"

open Fake
open RestorePackageHelper
open datNET.Fake.Config

let private _OverrideConfig (parameters : datNET.Targets.ConfigParams) =
  { parameters with
      AccessKey = Nuget.apiKey
      Authors = Release.authors
      Description = Release.description
      OutputPath = Release.outputPath
      Project = Release.project
      Publish = true
      WorkingDir = Release.workingDir
  }

datNET.Targets.initialize _OverrideConfig

Target "RestorePackages" (fun _ ->
  Source.solutionFile
  |> Seq.head
  |> RestoreMSSolutionPackages (fun p ->
      { p with
          Sources = Nuget.sources
          OutputPath = Nuget.restorePath
          Retries = 4 })
)

"MSBuild"           <== [ "Clean"; "RestorePackages" ]
"Test"              <== [ "MSBuild" ]
"Package"           <== [ "Test" ]
"Publish"           <== [ "Package" ]

RunTargetOrDefault "MSBuild"
