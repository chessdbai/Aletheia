#!/bin/zsh

dotnet pack -c Release
FILE=./bin/Release/ChessDB-Aletheia.Pgn.Parser.1.0.0-alpha.nupkg
nuget add $FILE -source /Volumes/ChessDBNugetLocal/
