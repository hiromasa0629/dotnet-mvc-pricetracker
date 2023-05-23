#!/bin/bash

export PATH="$PATH:/root/.dotnet/tools"

dotnet ef database update

dotnet out/assessment.dll --urls http://0.0.0.0:5283