#!/usr/bin/env bash
export runApp=0
export buildMode="Debug"

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

echo "########################################"
echo "Cleaning \"${buildMode}\" build directory ..."
dotnet clean -c ${buildMode}
echo "########################################"
echo "Restoring Nuget packages and dependencies ..."
dotnet restore
echo "########################################"
echo "Building \"${buildMode}\" configuration ..."
dotnet build --no-restore -c ${buildMode}
echo "########################################"
if [ "$runApp" -eq 1 ]; then
  echo "Running the app in \"${buildMode}\" mode ..."
  dotnet run -c ${buildMode} --project ./src/
else
  echo "Skipping running the app in \"${buildMode}\" mode ..."
fi
