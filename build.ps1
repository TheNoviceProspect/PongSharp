param (
    [string] $buildMode = 'Debug',
    [switch] $run = $false
    )

Write-Host "########################################"
Write-Host "Cleaning `"$($buildMode)`" build directory ..."
dotnet clean -c $($buildMode)
Write-Host "########################################"
Write-Host "Restoring Nuget packages and dependencies ..."
dotnet restore
Write-Host "########################################"
Write-Host "Building `"$($buildMode)`" configuration ..."
dotnet build --no-restore -c $($buildMode)
Write-Host "########################################"
if ($run) {
    Write-Host "Running the app in `"$($buildMode)`" mode ..."
    dotnet run -c $($buildMode) --project ./src/
} else {
    Write-Host "Skipping running the app in `"$($buildMode)`" mode ..."
}