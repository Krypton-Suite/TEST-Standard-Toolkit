# UnitTest-CI: exclude
<#
.SYNOPSIS
    Captures Windows XP Luna Blue/Olive/Silver theme screenshots for PR drafts.
.DESCRIPTION
    Loads Debug net472 Toolkit/Themes assemblies, applies each Luna palette via
    ThemeManager, hosts a small KryptonForm, and saves PrintWindow PNGs under Documents/PR/.
.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-WindowsXPThemesScreenshot.ps1
#>
#Requires -Version 5.1
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')
$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
Register-UnitTestAssemblyResolver -BinDir $bin
Initialize-UnitTestNativeInput

foreach ($name in @('Krypton.Interop.dll', 'Krypton.Resources.dll', 'Krypton.Toolkit.dll', 'Krypton.Themes.dll')) {
    $path = Join-Path $bin $name
    if (-not (Test-Path -LiteralPath $path)) {
        throw "Missing $path - build Krypton.Themes Debug $TargetFramework first."
    }
    [void][Reflection.Assembly]::LoadFrom($path)
}

$outDir = Join-Path $repoRoot 'Documents\PR'
New-Item -ItemType Directory -Force -Path $outDir | Out-Null

$modes = @(
    @{ File = 'windows-xp-themes-luna-blue.png'; ModeName = 'WindowsXPLunaBlue'; Title = 'Windows XP Luna Blue' },
    @{ File = 'windows-xp-themes-luna-olive.png'; ModeName = 'WindowsXPLunaOlive'; Title = 'Windows XP Luna Olive' },
    @{ File = 'windows-xp-themes-luna-silver.png'; ModeName = 'WindowsXPLunaSilver'; Title = 'Windows XP Luna Silver' }
)

$paletteModeType = [Type]::GetType('Krypton.Toolkit.PaletteMode, Krypton.Toolkit', $true)
$managerType = [Type]::GetType('Krypton.Toolkit.KryptonManager, Krypton.Toolkit', $true)
$themeManagerType = [Type]::GetType('Krypton.Toolkit.ThemeManager, Krypton.Toolkit', $true)
$formType = [Type]::GetType('Krypton.Toolkit.KryptonForm, Krypton.Toolkit', $true)
$panelType = [Type]::GetType('Krypton.Toolkit.KryptonPanel, Krypton.Toolkit', $true)
$buttonType = [Type]::GetType('Krypton.Toolkit.KryptonButton, Krypton.Toolkit', $true)
$textType = [Type]::GetType('Krypton.Toolkit.KryptonTextBox, Krypton.Toolkit', $true)
$checkType = [Type]::GetType('Krypton.Toolkit.KryptonCheckBox, Krypton.Toolkit', $true)
$labelType = [Type]::GetType('Krypton.Toolkit.KryptonLabel, Krypton.Toolkit', $true)

$applyTheme = $themeManagerType.GetMethod(
    'ApplyTheme',
    [type[]]@($paletteModeType, $managerType)
)
$manager = [Activator]::CreateInstance($managerType, @())

[System.Windows.Forms.Application]::EnableVisualStyles()

foreach ($entry in $modes) {
    $mode = [Enum]::Parse($paletteModeType, $entry.ModeName)
    [void]$applyTheme.Invoke($null, @($mode, $manager))

    $form = [Activator]::CreateInstance($formType, @())
    $form.Text = $entry.Title
    $form.ClientSize = New-Object System.Drawing.Size(500, 320)
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point(80, 80)

    $panel = [Activator]::CreateInstance($panelType, @())
    $panel.Dock = [System.Windows.Forms.DockStyle]::Fill

    $btn = [Activator]::CreateInstance($buttonType, @())
    $btn.Location = New-Object System.Drawing.Point(24, 24)
    $btn.Size = New-Object System.Drawing.Size(140, 32)
    $btn.Values.Text = 'Sample Button'

    $tb = [Activator]::CreateInstance($textType, @())
    $tb.Location = New-Object System.Drawing.Point(24, 72)
    $tb.Size = New-Object System.Drawing.Size(240, 23)
    $tb.Text = 'Sample text'

    $chk = [Activator]::CreateInstance($checkType, @())
    $chk.Location = New-Object System.Drawing.Point(24, 112)
    $chk.Values.Text = 'Sample Check'

    $lbl = [Activator]::CreateInstance($labelType, @())
    $lbl.Location = New-Object System.Drawing.Point(24, 152)
    $lbl.Values.Text = "PaletteMode: $($entry.ModeName)"

    [void]$panel.Controls.Add($btn)
    [void]$panel.Controls.Add($tb)
    [void]$panel.Controls.Add($chk)
    [void]$panel.Controls.Add($lbl)
    [void]$form.Controls.Add($panel)

    $form.TopMost = $true
    $form.Show()
    $form.Activate()
    $form.BringToFront()
    [void][UnitTestNative]::SetForegroundWindow($form.Handle)
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 500

    $out = Join-Path $outDir $entry.File
    Save-UnitTestWindowPng -Form $form -Path $out
    Write-Host "Saved $out"
    $form.Close()
    $form.Dispose()
}
