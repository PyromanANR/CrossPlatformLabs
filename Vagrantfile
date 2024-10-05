# -*- mode: ruby -*-
# vi: set ft=ruby :

# Define the host IP addresses
hosts = {
  "ubuntu" => "192.168.88.10",
  "windows" => "192.168.88.11",
  "mac" => "192.168.88.12"
}

Vagrant.configure("2") do |config|
  # Ubuntu Machine Configuration
  config.vm.define "ubuntu" do |ubuntu|
    ubuntu.vm.box = "bento/ubuntu-22.04"
    ubuntu.vm.network :private_network, ip: hosts["ubuntu"]
    ubuntu.vm.provider "virtualbox" do |v|
      v.name = "Ubuntu VM"
      v.memory = "2048"
      v.cpus = 2
    end
    ubuntu.vm.synced_folder ".", "/home/vagrant/project"
    ubuntu.vm.provision "shell", path: "provision-ubuntu.sh"
  end

  # Windows Machine Configuration
  config.vm.define "windows" do |windows|
    windows.vm.box = "gusztavvargadr/windows-10"
    windows.vm.network :private_network, ip: hosts["windows"]
    windows.vm.provider "virtualbox" do |v|
      v.name = "Windows VM"
      v.memory = "4096"
      v.cpus = 2
    end
    windows.vm.synced_folder ".", "C:/project"
    windows.vm.provision "shell", path: "provision-windows.sh"
  end

  # Mac Machine Configuration
  config.vm.define "mac" do |mac|
    mac.vm.box = "ramsey/macos-catalina"
    mac.vm.network :private_network, ip: hosts["mac"]
    mac.vm.provider "virtualbox" do |v|
      v.name = "Mac VM"
      v.memory = "4096"
      v.cpus = 2
    end
    mac.vm.synced_folder ".", "/Users/vagrant/project"
    mac.vm.provision "shell", path: "provision-mac.sh"
  end
end