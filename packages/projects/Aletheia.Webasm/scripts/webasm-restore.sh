#!/bin/bash
set -euxo pipefail

START_DIR = $PWD
cd Artemis.Webasm
dotnet restore
cd $START_DIR