Import-Module .\CmdletThree\bin\Debug\CmdletThree.dll
Import-Module .\CmdletFour\bin\Debug\CmdletFour.dll

Write-Output([System.Management.Automation.Runspaces.Runspace]::DefaultRunspace.InitialSessionState )

Show-Path3
Show-Path4