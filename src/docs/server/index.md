---
layout: docs
title: Getting started with AppVeyor Server
---

<!-- markdownlint-disable MD022 MD032 -->
# Getting started with AppVeyor Server
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Introduction

**AppVeyor Server** is a downloadable version of hosted AppVeyor CI service that can be installed on your own server. AppVeyor Server can be installed on **Windows**, **Linux** and **macOS** (coming soon) and is integrated with popular source control providers such as GitHub, Bitbucket, GitLab, Azure DevOps or generic Git/Mercurial/SVN repo. Out-of-the-box AppVeyor is able to run builds as processes, or inside Docker containers or Hyper-V/Azure/GCE/AWS virtual machines. With affordable cloud-friendly licensing, AppVeyor Server can be scaled from a simple build server for your team to a powerful multi-cloud CI/CD solution for your entire organization.

## System requirements

### Windows 10

If you are a Windows developer you probably have Windows 10 on your desktop already. **Windows 10 version 1809** (October 2018 Update) or later is the best option, especially if you are going to run Docker builds. Build 1803 (April 2018 Update) is also OK though it's missing some great Docker features such as running containers with process isolation.

### Windows Server

AppVeyor Server can be installed on Windows Server 2016 or above. We strongly recommend the latest **Windows Server 2019 (version 1809)** for your CI/CD server, expecially if you are going to run Docker builds. Windows Server 2019 has base Docker images with the smallest footprint, can run Linux Containers On Windows (LCOW) and has the Windows Subsystem for Linux (WSL) feature - a must for any team developing for multiple platforms.

If you are going to deploy your AppVeyor Server in a cloud we recommend Azure, as their VMs have nested virtualization enabled and as such allow Hyper-V and LCOW. On AWS there is `i3.metal` instance on which Windows Server 2016 with Hyper-V could be deployed. GCP, unfortunately, does not yet support nested virtualization for Windows in their VMs.

### Linux

AppVeyor Server was tested on Ubuntu 16.04 and 18.04 only, but, potentially, can be installed on [any Linux distro supported by .NET Core](https://docs.microsoft.com/en-us/dotnet/core/linux-prerequisites?tabs=netcore2x#supported-linux-versions).

AppVeyor Server is so lightweight that it feels perfectly fine on $10/month DigitalOcean droplet or other "cheap" Linux VM in any cloud.

### Git

Git is only required if you are planning to run "process" builds. For Docker builds AppVeyor base image has Git/Mercurial/Subversion pre-installed. Also, Git would be required to clone [appveyor/build-images](https://github.com/appveyor/build-images) repository if you are going to customize ours or build your own Docker/Azure/AWS/GCP images.

### Docker

Docker is not required, but recommended! AppVeyor has 1st-class integration with **Docker EE** on Windows Server, **Docker Desktop for Windows** (aka Docker CE) on Windows 10 and **Docker CE** on Linux.

* [Install Docker Engine - Enterprise on Windows Servers](https://docs.docker.com/install/windows/docker-ee/)
* [Install Docker Desktop for Windows](https://docs.docker.com/docker-for-windows/install/)
* [Get Docker CE for Ubuntu](https://docs.docker.com/install/linux/docker-ce/ubuntu/)

## Installation

### Windows

AppVeyor Server can be installed on **Windows 7 SP1 or later** and **Windows Server 2008 R2 SP1 or later**.

[Download AppVeyor Server MSI installer]({{ site.data.urls.latest_msi_location }}/{{ site.data.urls.latest_msi_filename }}) and run it.

Once the installation complete AppVeyor Web interface will be opened in a new browser window to continue with AppVeyor configuration.

### Linux

Download the latest [AppVeyor Server Debian package]({{ site.data.urls.latest_deb_location }}/{{ site.data.urls.latest_deb_filename }}) using the following command:

    curl {{ site.data.urls.latest_deb_location }}/{{ site.data.urls.latest_deb_filename }} -o {{ site.data.urls.latest_deb_filename }}

Install AppVeyor Server by running:

    sudo dpkg -i {{ site.data.urls.latest_deb_filename }}

Verify the installed version by running:

    /opt/appveyor/server/appveyor version

Once the installation is complete, open a web browser and navigate to `http://<server_ip>` or `http://<server_ip>:8050` (if port 80 is already taken by another app) to continue with AppVeyor configuration.


## Running your first build

[Work in progress. Stay tuned.]

## Running builds in Azure VMs

You can connect both hosted and on-premise AppVeyor to your own Azure subscription for AppVeyor to instantiate build VMs in it. It has a lot of benefits like having ability to customize your build image, select desired VM size, set custom build timeout and many others.

To simplify setup process for you, we created script which provisions necessary Azure resources, runs Hashicorp Packer to create a basic build image (based on Windows Server 2019), and put all AppVeyor configuration together. After running this script, you should be able to start builds on Azure immediately (and optionally customize your Azure build environment later).

### Install Packer and Azure PowerShell module

Install Packer woth Chocolatey (skip if Packer already installed):

    choco install packer

*Ensure to restart your command line shell or run `refreshenv` so `packer` command is available.
If you do not use Chocolatey, install directly from [Packer](https://www.packer.io/intro/getting-started/install.html) website. In this case ensure that path to `packer` command set correctly and it can be called from the command line shell.*

Install Azure PowerShell module (skip if Azure PowerShell already installed):

    Install-Module -Name Az -AllowClobber

### Get necessary files

Clone `appveyor/build-images` repository (we assume Git is installed):

    git clone https://github.com/appveyor/build-images.git

Switch to the cloned folder

    cd .\build-images

### Run script

If you are in PowerShell, run:

    .\connect-to-azure.ps1

If you are in CMD, run:

    powershell .\connect-to-azure.ps1

## Maintenance

* [Upgrading AppVeyor](/docs/server/maintenance/#upgrading-appveyor-server)
* [Backup/restore AppVeyor](/docs/server/maintenance/#backuprestore-appveyor-server)
* [Troubleshooting](/docs/server/maintenance/#troubleshooting)
