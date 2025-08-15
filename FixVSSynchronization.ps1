# PowerShell script to fix Visual Studio synchronization issues
# This script will clean and regenerate Visual Studio cache files

Write-Host "Starting Visual Studio Synchronization Fix..." -ForegroundColor Green

# Step 1: Close any running Visual Studio instances
Write-Host "Step 1: Checking for running Visual Studio instances..." -ForegroundColor Yellow
$vsProcesses = Get-Process -Name "devenv" -ErrorAction SilentlyContinue
if ($vsProcesses) {
    Write-Host "Found running Visual Studio instances. Please close Visual Studio manually and run this script again." -ForegroundColor Red
    exit 1
}

# Step 2: Clean bin and obj folders
Write-Host "Step 2: Cleaning bin and obj folders..." -ForegroundColor Yellow
Get-ChildItem -Path "." -Recurse -Directory -Name "bin" | ForEach-Object {
    $path = Join-Path $PWD $_
    if (Test-Path $path) {
        Write-Host "Removing: $path"
        Remove-Item -Path $path -Recurse -Force -ErrorAction SilentlyContinue
    }
}

Get-ChildItem -Path "." -Recurse -Directory -Name "obj" | ForEach-Object {
    $path = Join-Path $PWD $_
    if (Test-Path $path) {
        Write-Host "Removing: $path"
        Remove-Item -Path $path -Recurse -Force -ErrorAction SilentlyContinue
    }
}

# Step 3: Remove .vs folder (already done)
Write-Host "Step 3: .vs folder already removed" -ForegroundColor Yellow

# Step 4: Remove user-specific files
Write-Host "Step 4: Removing user-specific files..." -ForegroundColor Yellow
Get-ChildItem -Path "." -Recurse -Filter "*.user" | ForEach-Object {
    Write-Host "Removing: $($_.FullName)"
    Remove-Item -Path $_.FullName -Force -ErrorAction SilentlyContinue
}

Get-ChildItem -Path "." -Recurse -Filter "*.suo" | ForEach-Object {
    Write-Host "Removing: $($_.FullName)"
    Remove-Item -Path $_.FullName -Force -ErrorAction SilentlyContinue
}

# Step 5: Fix project file issues
Write-Host "Step 5: Checking project files for common issues..." -ForegroundColor Yellow

# Check for missing System.Resources.Extensions reference
$projectFiles = Get-ChildItem -Path "." -Recurse -Filter "*.csproj"
foreach ($projectFile in $projectFiles) {
    $content = Get-Content -Path $projectFile.FullName -Raw
    if ($content -match "<EmbeddedResource" -and $content -notmatch "System.Resources.Extensions") {
        Write-Host "Project $($projectFile.Name) may need System.Resources.Extensions reference" -ForegroundColor Orange
    }
}

# Step 6: Restore NuGet packages
Write-Host "Step 6: Restoring NuGet packages..." -ForegroundColor Yellow
try {
    & dotnet restore Erp_V1.sln
    Write-Host "NuGet packages restored successfully" -ForegroundColor Green
} catch {
    Write-Host "Warning: NuGet restore encountered issues: $($_.Exception.Message)" -ForegroundColor Orange
}

# Step 7: Create a simple test file to verify synchronization
Write-Host "Step 7: Creating test file for synchronization verification..." -ForegroundColor Yellow
$testContent = @"
// Test file created by FixVSSynchronization.ps1
// This file should appear in Visual Studio after opening the solution
using System;

namespace Erp_V1.Test
{
    public class SynchronizationTest
    {
        public static void TestMethod()
        {
            Console.WriteLine("Visual Studio synchronization is working!");
        }
    }
}
"@

$testFilePath = "Erp_V1\SynchronizationTest.cs"
Set-Content -Path $testFilePath -Value $testContent -Encoding UTF8
Write-Host "Created test file: $testFilePath" -ForegroundColor Green

Write-Host "`nSynchronization fix completed!" -ForegroundColor Green
Write-Host "Next steps:" -ForegroundColor Cyan
Write-Host "1. Open Visual Studio" -ForegroundColor White
Write-Host "2. Open the Erp_V1.sln solution" -ForegroundColor White
Write-Host "3. Check if the new SynchronizationTest.cs file appears in the Erp_V1 project" -ForegroundColor White
Write-Host "4. If files still don't sync, try 'Project -> Show All Files' in Visual Studio" -ForegroundColor White
Write-Host "5. Right-click on any missing files and select 'Include in Project'" -ForegroundColor White

Write-Host "`nPress any key to continue..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")