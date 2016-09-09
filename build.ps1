$ErrorActionPreference = "Stop"
Set-Location $PSScriptRoot
Import-Module ./common -Force

if (Test-Path artifacts) {
	Show-Title "Removing artifacts"
	Remove-Item artifacts -Recurse -Force
}

Show-Title "Restoring .NET packages"
Exec { dotnet restore }

Get-Item src/* | foreach {
	$path = Resolve-Path $_ -Relative
	Show-Title "Building and packing $path"
	Exec { dotnet pack $_ -c Release -o artifacts }
}