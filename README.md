# PongSharp

This is a simple (I hope) implementation of Pong in CSharp.

I'm using Raylib as the underlying gfx library.

Please remember that I am a hobbyist trying his hand at progamming, not an actual dev.

## Build Status

[![.NET](https://github.com/TheNoviceProspect/PongSharp/actions/workflows/dotnet.yml/badge.svg?branch=main&event=push)](https://github.com/TheNoviceProspect/PongSharp/actions/workflows/dotnet.yml)

## Build and run

Clone this repository, and then run

On Linux : `./build.sh -buildMode "Debug"` or `./build.sh -buildMode "Release"`  
On Windows : `./build.ps1 -buildMode "Debug"` or `./build.ps1 -buildMode "Release"`

If you ommit the `-buildMode` parameter the scripts will assume "Debug" builds

You can also add the `-run` parameter to the build script and the app just starts right after a successful build in the same mode as above.