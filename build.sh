#!/usr/bin/env bash
if [ $# -eq 0 ]; then
    export buildMode="Debug"
    export runApp=0
fi

while [ $# -gt 0 ]; do
  case "$1" in
    -buildMode)
      export buildMode="${2}"
      shift
      ;;
    -run)
      export runApp=1
      ;;
    -verbose)
      export verbose_option=1
      ;;
    *)
      printf "ERROR: Invalid Parameter \"$1\" \n"
      exit 1
  esac
  shift
done

dotnet clean -c ${buildMode}; dotnet restore; dotnet build --no-restore -c ${buildMode}

if [$runApp -eq 1] then
  dotnet run -c ${buildMode} --project ./src/
fi