#!/bin/bash
set -euxo pipefail

START_DIR = $PWD
cd Artemis.Webasm
dotnet clean
cd $START_DIR