@echo off

dotnet nuget push "../Artefacts/Release/*.nupkg" --source "github"