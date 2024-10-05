#!/bin/bash

# Update and upgrade the system
sudo apt-get update
sudo apt-get upgrade -y

# Install dependencies
sudo apt-get install -y wget apt-transport-https software-properties-common

# Add Microsoft package repository
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

# Install .NET SDK 8.0
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0

# Verify installation
dotnet --version

# Navigate to the project directory
cd /home/vagrant/project

# Run LAB4
dotnet run --project LAB4 -- --input LAB4/INPUT.TXT --output LAB4/OUTPUT.TXT

echo "Ubuntu environment setup complete and LAB4 executed"