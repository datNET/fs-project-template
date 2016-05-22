namespace datNET.Fake.Config
open Fake
open Fake.EnvironmentHelper
open System.IO

module Common =
  let rootDir = Directory.GetCurrentDirectory()

module Source =
  open Common

  let solutionFile = !! (Path.Combine(rootDir, "*.sln"))

module Build =
  let testAssemblies = !! "tests/**/*.Tests.dll" -- "**/obj/**/*.Tests.dll"
  let dotNetVersion = "4.5"
  let mSBuildArtifacts = !! "src/**/bin/**/*.*" ++ "src/**/obj/**/*.*"

module Nuget =
  let apiKey = environVar "NUGET_API_KEY"
  let packageDirName = "nupkgs"
  let restorePath = "packages"
  let sources = [ "https://nuget.org/api/v2" ]

module Release =
  let items = !! "**/bin/Release/*"

  let project = "datNET.ProjectTemplate"
  let nuspec = project + ".nuspec"
  let authors = [ ]
  let description = "TODO: Add a description"
  let workingDir = "bin"
  let outputPath = workingDir
