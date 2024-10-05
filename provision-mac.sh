#!/bin/bash

# Install Homebrew
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"

# Add Homebrew to PATH
echo 'eval "$(/opt/homebrew/bin/brew shellenv)"' >> /Users/vagrant/.zprofile
eval "$(/opt/homebrew/bin/brew shellenv)"

# Install .NET SDK 8.0
brew install --cask dotnet-sdk

# Verify installation
dotnet --version

# Navigate to the project directory
cd /Users/vagrant/project

# Run LAB4
dotnet run --project LAB4 -- --input LAB4/INPUT.TXT --output LAB4/OUTPUT.TXT

echo "Mac environment setup complete and LAB4 executed"