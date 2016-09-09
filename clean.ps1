$ErrorActionPreference = "Stop"
Set-Location $PSScriptRoot
Import-Module ./common -Force

if (Test-Path artifacts) {
	Write-Output "Cleaning artifacts"
	Remove-Item artifacts -Recurse -Force
}

Get-Item src/* | foreach {
	$path = Resolve-Path $_ -Relative
	Push-Location $_
	Write-Output "Cleaning $path"
	try {
		Remove-ItemForce bin
		Remove-ItemForce obj
		Remove-ItemForce project.lock.json
	}
	finally {
		Pop-Location
	}
}