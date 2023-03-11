param ($buildMode = 'Debug')
dotnet clean -c $($buildMode); dotnet restore; dotnet build --no-restore -c $($buildMode); dotnet run -c $($buildMode) --project ./src/