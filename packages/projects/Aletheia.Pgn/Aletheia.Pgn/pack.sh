#!/bin/zsh

dotnet pack -c Release
FILE=./bin/Release/ChessDB-Aletheia.Pgn.1.0.0-alpha.nupkg
nuget add $FILE -source /Volumes/ChessDBNugetLocal/
