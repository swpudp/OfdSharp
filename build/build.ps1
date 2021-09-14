[CmdletBinding(PositionalBinding=$false)]
param(
    [bool] $CreatePackages=$true,
    [bool] $RunTests = $false,
    [string] $PullRequestNumber
)

Write-Host "Run Parameters:" -ForegroundColor Cyan
Write-Host "  CreatePackages: $CreatePackages"
Write-Host "  RunTests: $RunTests"
Write-Host "  dotnet --version:" (dotnet --version)


$basePath = resolve-path ..
$packageOutputFolder = "$basePath\build\.nupkgs"
$projectPath = "..\src\OfdSharp\OfdSharp.csproj"
$testsPath = "..\src\UnitTests\UnitTests.csproj"

if ($PullRequestNumber) {
    Write-Host "Building for a pull request (#$PullRequestNumber), skipping packaging." -ForegroundColor Yellow
    $CreatePackages = $false
}

Write-Host "Building OfdSharp projects (OfdSharp.csproj traversal)..." -ForegroundColor "Magenta"
dotnet build $projectPath -c Release /p:CI=true
Write-Host "Done building." -ForegroundColor "Green"

if ($RunTests) {
    Write-Host "Running tests: UnitTests.csproj traversal (all frameworks)" -ForegroundColor "Magenta"
    dotnet test $testsPath -c Release --no-build
    if ($LastExitCode -ne 0) {
        Write-Host "Error with tests, aborting build." -Foreground "Red"
        Exit 1
    }
    Write-Host "Tests passed!" -ForegroundColor "Green"
}

if ($CreatePackages) {

    Write-Host "begin get latest version" -ForegroundColor "Green"
    $Response = Invoke-WebRequest -URI "https://api-v2v3search-0.nuget.org/query?q=OfdSharp&prerelease=false&skip=0&take=1"
    $json = $Response.Content | Out-String | ConvertFrom-Json
    $latestVersion=''
    foreach ($version in $json.data.versions) {
        if($version.version -eq '')
        {
            latestVersion = $version.version
        }
        if($version.version -gt $latestVersion){
            $latestVersion = $version.version
        }
    }
    Write-Host "end get latestVersion: " $latestVersion

    $versionTokens = $latestVersion.split(".")
    $buildNumber = [System.Double]::Parse($versionTokens[$versionTokens.Count -1]) 
    $versionTokens[$versionTokens.Count -1] = $buildNumber +1
    $newVersion = [string]::join('.', $versionTokens)

    Write-Host "build newVersion: $newVersion"

    New-Item -ItemType Directory -Path $packageOutputFolder -Force | Out-Null
    Write-Host "Clearing existing $packageOutputFolder..." -NoNewline
    Get-ChildItem $packageOutputFolder | Remove-Item
    Write-Host "done." -ForegroundColor "Green"

    Write-Host "Building all packages" -ForegroundColor "Green"
    dotnet pack $projectPath --no-build -c Release /p:PackageOutputPath=$packageOutputFolder /p:CI=true /p:Version=$newVersion

    $package = JOIN-PATH -Path $packageOutputFolder "*.nupkg"
    Write-Host "upload package :$package"
    dotnet nuget push $package -k "4003d786-cc37-4004-bfdf-c4f3e8ef9b3a" -s "https://api.nuget.org/v3/index.json"
}
Write-Host "Build Complete." -ForegroundColor "Green"