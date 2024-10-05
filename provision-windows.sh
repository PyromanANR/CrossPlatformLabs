# Install Chocolatey
Set-ExecutionPolicy Bypass -Scope Process -Force
[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072
iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))

# Install .NET SDK 8.0
choco install dotnet-8.0-sdk -y

# Refresh environment variables
refreshenv

# Verify installation
dotnet --version

# Navigate to the project directory
cd C:\project

# Run LAB4
dotnet run --project LAB4 -- --input LAB4\INPUT.TXT --output LAB4\OUTPUT.TXT

Write-Host "Windows environment setup complete and LAB4 executed"