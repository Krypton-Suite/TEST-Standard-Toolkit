@echo off

dotnet nuget push "../Artefacts/package/Release/*.nupkg" --source "github"