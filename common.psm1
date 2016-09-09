$ErrorActionPreference = "Stop"

function Exec
{
	param (
		[Parameter(Mandatory=$true,Position=0)]
		[scriptblock]$Command,
		[string]$WorkingDirectory
	)
	if ($WorkingDirectory) {
		Push-Location $WorkingDirectory
	}
	& $Command
	if ($WorkingDirectory) {
		Pop-Location
	}
	Test-LastExitCode
}

function Test-LastExitCode {
	if ($LASTEXITCODE -ne 0) {
		throw "Operation completed with fail exit code " + $LASTEXITCODE
	}
}

function Show-Title {
	param (
		[Parameter(Mandatory=$true,Position=1)]
		[string]$Str,
		[switch]$SkipNewline
	)
	if (!$SkipNewline) { Write-Host }
	Write-Host $Str -f Yellow
}

function Show-Success {
	param (
		[Parameter(Mandatory=$true,Position=1)]
		[string]$Str
	)
	Write-Host $Str -f Green
}

function Remove-ItemForce {
	param (
		[Parameter(Mandatory=$true,Position=1)]
		[string]$Path
	)
	if (Test-Path $Path) {
		Remove-Item $Path -Recurse -Force
	}
}