#!/bin/bash
set -euxo pipefail

START_DIR=$PWD

rm -rf ./dist || true

cd Aletheia.Webasm
dotnet restore
dotnet publish -c Release -r linux-x64
cd $START_DIR
cp -R ./Aletheia.Webasm/bin/Release/netstandard2.1/linux-x64/dist/_framework ./dist

