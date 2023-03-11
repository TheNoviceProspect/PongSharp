param (
    [string] $buildMode = 'Debug',
    [switch] $run = $false
    )
dotnet clean -c $($buildMode); dotnet restore; dotnet build --no-restore -c $($buildMode)

if ($run) {
    dotnet run -c $($buildMode) --project ./src/
}