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


## Running builds

In AppVeyor you are starting from creating a **New project**. While adding a project you will be offered to connect AppVeyor to your source control system. There could be multiple *Projects* connected to the same repository - they all can have different configurations and versioning scheme. Every project adds a new webhook to the connected repository.

**Build** is a run of specific project. "New build" button or a call to a project webhook triggers a new build. Build configuration could vary on commit's branch or tag.

Builds are logical container for **jobs**. Every build has at least one job, but build matrix can produce builds with multiple jobs running in parallel or as workflow.

Click **New build** or make a push to project repository to start a new build.

Out-of-the-box AppVeyor is configured to run every build job as a new system process. The build flow in that process is controlled by *AppVeyor Build Agent* and starts with repository cloning. **Process** is the simplest form of builds isolation. It's the easiest way to get started with AppVeyor as it relies on the software/tools/libraries that most probably already installed on your Desktop or build server. However, "process" isolation does not follow "clean environment" principle which assumes the build does not have "harmful" steps (like disk formating or reboots) and finishes with clean-up code (like deleting test database or removing temp files).

AppVeyor build job has a [pre-defined pipeline](/docs/build-configuration/#build-pipeline) like **Clone &rarr; Install &rarr; Build &rarr; Test &rarr; Deploy &rarr; Finalize**, but, of course, each step in that flow could be enabled/disabled, customized or completely replaced with your own script. A job is a minimal building block for complex CI/CI workflows modeled with build matrices where jobs could wait for other jobs, combined into groups and run in parallel.



* Processes

* Docker containers

* Virtual machines

Process cloud is the most simple one, but less practical...

Local cloud, remote cloud.

Build steps (link to exististing AppVeyor documentation)

### Docker

#### Routing builds to a Docker cloud

* TODO

#### Selecting Docker image

* (LCOW, Linux containers mode)
* AppVeyor-flavored image with Build Agent and tools (like PowerShell) (for Windows there are two types of it) or any Docker image from any repository
* What's installed on every image?
* On Windows: warning about the time required to pull images for the first time.
* Supported build steps in agentless image.
* Re-pulling image on every build.
* Sources cloning on/off

* Image with AppVeyor agent - you can have the "fat" image with all required software pre-installed.
* Any image - you can split the job across specialized containers.


#### Complex pipelines with multiple jobs

* Use cases:
    * Test in parallel on different versions/platforms
    * Collect artifacts and then deploy
* Switch to appveyor.yml
* Service-like jobs, e.g. MySQL
* Communication between containers in a single build.
* Job groups, job dependencies, fan-in/-out workflows.
* Shared "bin" folder across build jobs

#### Customizing AppVeyor image

Example of `Dockerfile`:

```Dockerfile
TODO
```


### Azure VMs

[Work in progress. Stay tuned.]

## Maintenance

* [Upgrading AppVeyor](/docs/server/maintenance/#upgrading-appveyor-server)
* [Backup/restore AppVeyor](/docs/server/maintenance/#backuprestore-appveyor-server)
* [Troubleshooting](/docs/server/maintenance/#troubleshooting)