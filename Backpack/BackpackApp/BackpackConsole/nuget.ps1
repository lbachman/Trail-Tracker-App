# PowerShell script to install Entity Framework Core packages

# Define an array of package names
$packages = @(
    "Microsoft.EntityFrameworkCore",
    "Microsoft.EntityFrameworkCore.Tools",
    "Microsoft.EntityFrameworkCore.Design",
    "Microsoft.EntityFrameworkCore.Sqlite"
)

# Loop through each package and install it using dotnet CLI
foreach ($package in $packages) {
    Write-Host "Installing $package..." -ForegroundColor Green
    dotnet add package $package
}

Write-Host "All packages installed successfully." -ForegroundColor Cyan
