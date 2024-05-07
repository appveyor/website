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

**AppVeyor Server** is a downloadable version of hosted AppVeyor CI service that can be installed on your own server. AppVeyor Server can be installed on **Windows**, **Linux** and **macOS** and is integrated with popular source control providers such as GitHub, Bitbucket, GitLab, Azure DevOps, Kiln, Gitea or generic Git/Mercurial/SVN repo. Out-of-the-box AppVeyor is able to run builds as processes, or inside Docker containers or Hyper-V/Azure/GCE/AWS virtual machines. With affordable cloud-friendly licensing, AppVeyor Server can be scaled from a simple build server for your team to a powerful multi-cloud CI/CD solution for your entire organization.

## System requirements

### Windows 10

If you are a Windows developer you probably have Windows 10 on your desktop already. **Windows 10 version 1809** (October 2018 Update) or later is the best option, especially if you are going to run Docker builds. Build 1803 (April 2018 Update) is also OK though it's missing some great Docker features such as running containers with process isolation.

### Windows Server

AppVeyor Server can be installed on Windows Server 2016 or above. We strongly recommend the latest **Windows Server 2019 (version 1809)** for your CI/CD server, especially if you are going to run Docker builds. Windows Server 2019 has base Docker images with the smallest footprint, can run Linux Containers On Windows (LCOW) and has the Windows Subsystem for Linux (WSL) feature - a must for any team developing for multiple platforms.

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

AppVeyor Server can be installed on **Windows 10 or above** and **Windows Server 2016 or above**.

#### Chocolatey Installation

[Install Chocolatey package manager](https://chocolatey.org) if not already installed.

Run the following command:

    choco install appveyor-server

Once the Chocolatey package installation completes, the AppVeyor Web interface will be opened in a new browser window to continue with AppVeyor configuration.

#### Manual Installation

[Download AppVeyor Server MSI installer]({{ site.url }}/downloads/appveyor/appveyor-server.msi) and run it.

Once the installation complete AppVeyor Web interface will be opened in a new browser window to continue with AppVeyor configuration.

### Linux

Download the latest [AppVeyor Server Debian package]({{ site.url }}/downloads/appveyor/appveyor-server.deb) using the following command:

    curl -L {{ site.url }}/downloads/appveyor/appveyor-server.deb -o appveyor-server_{{ site.data.versions.appveyor_version }}_amd64.deb

Install AppVeyor Server by running:

    sudo dpkg -i appveyor-server_{{ site.data.versions.appveyor_version }}_amd64.deb

Verify the installed version by running:

    /opt/appveyor/server/appveyor version

Once the installation is complete, open a web browser and navigate to `http://<server_ip>` or `http://<server_ip>:8050` (if port 80 is already taken by another app) to continue with AppVeyor configuration.

### macOS

AppVeyor Server can be installed on macOS 10.13 "High Sierra" and later versions.

Install [Homebrew package manager](https://brew.sh/) if not already installed.

Add AppVeyor tap repo, install AppVeyor Server formulae and start the service:

    brew tap appveyor/brew
    brew install appveyor-server
    brew services start appveyor-server

Once AppVeyor service is started open `http://localhost:8050` in your browser to continue AppVeyor setup.

## Running builds

In AppVeyor you are starting from creating a **New project**. While adding a project you will be offered to connect AppVeyor to your source control system. Multiple *Projects* can be connected to the same repository - they all can have different configurations and versioning scheme. Every project adds a new webhook to the connected repository.

*Build* is a run of specific project. "New build" button or a call to a project webhook triggers a new build. Build configuration could vary on commit's branch or tag.

Builds are logical container for *build jobs*. Every build has at least one job, but build matrix can produce builds with multiple jobs running in parallel or as workflow.

Click **New build** or make a push to project repository to start a new build.

Out-of-the-box AppVeyor is configured to run every build job as a new system process. The build flow in that process is controlled by *AppVeyor Build Agent* and starts with repository cloning. **Process** is the simplest form of builds isolation. It's the easiest way to get started with AppVeyor as it relies on the software/tools/libraries that most probably already installed on your Desktop or build server. However, "process" isolation does not follow "clean environment" principle which assumes the build does not have "harmful" steps (like disk formatting or reboots) and finishes with clean-up code (like deleting test database or removing temp files).

AppVeyor build job has a [pre-defined pipeline](/docs/build-configuration/#build-pipeline) like **Clone &rarr; Install &rarr; Build &rarr; Test &rarr; Package &rarr; Deploy &rarr; Finalize**, but, of course, each step in that flow could be enabled/disabled, customized or completely replaced with your own script. A job is a minimal building block for complex CI/CI workflows modeled with build matrices where jobs could wait for other jobs, combined into groups and run in parallel.

### Docker

To run builds inside Docker containers Docker engine must be installed on AppVeyor Server or remote machine (TODO: give more information about AppVeyor Host Agent).

On Linux you install Docker CE ([Docker installation guide for Ubuntu](https://docs.docker.com/install/linux/docker-ce/ubuntu/)).

On Windows you can choose between [Docker Enterprise Edition (Docker EE)](https://docs.docker.com/install/windows/docker-ee/) or [Docker Desktop for Windows](https://docs.docker.com/docker-for-windows/install/) (which includes Docker CE with tools). Docker EE is a typical approach for Windows Server and with Windows Server 2019 and Hyper-V enabled you can configure LCOW to run Windows and Linux containers in a single build (for example, .NET Fremework tests using Redis or MySQL). However, Docker Desktop for Windows which is usually installed on Windows 10 (but could also be installed on Windows Server) in addition to LCOW gives "Linux containers" mode where containers run inside "MobyLinux" Hyper-V VM. MobyLinux VM gives you "pure" Docker on Linux experience, but once switched to Linux containers you can't run Windows containers at the same time.

AppVeyor instantly supports all Docker engines in all modes on any platform!

You can use these handful scripts for unattended Docker installation:

* Docker EE on Windows Server: [Enable Hyper-V, Containers and WSL](https://github.com/appveyor/build-images/blob/master/scripts/Windows/install_docker_hyperv_wsl_features.ps1) and [Configure Docker and LCOW](https://github.com/appveyor/build-images/blob/master/scripts/Windows/install_docker.ps1).
* Docker CE on Linux: ["Install using the convenience script" section at the bottom](https://docs.docker.com/install/linux/docker-ce/ubuntu/).

#### Routing builds to a Docker cloud

To run your build in Docker container open AppVeyor project settings and click **Environment** tab. Select **Docker** in **Build cloud** dropdown. Save project settings.

Every build job will be run in a fresh container which is immediately disposed on job completion. Docker containers give you clean, isolated and safe environments to run your tests and do any experimenting.

*Build cloud* in AppVeyor is an abstraction which allows to choose *where* and *how* the build job is executed. Build clouds implementations for Process, Docker and various clouds basically have two methods like "Provision worker" and "Decommission worker". By selecting the cloud in build configuration you route builds to a specific local or remote worker provisioner.

#### Selecting Docker image

Docker builds can either be run with AppVeyor-provided images or any image from any repository. AppVeyor images have a "minimal" set of pre-installed tools (like Git to clone the repo and PowerShell Core), but more important they have AppVeyor Build Agent installed and set as an "entrypoint" to support [build job pipeline](/docs/build-configuration/#build-pipeline).

You can find the tags of all AppVeyor images on [Docker Hub](https://hub.docker.com/r/appveyor/build-image/tags).

For Linux there is only one image based on [Ubuntu 18.04](https://github.com/appveyor/build-images/blob/master/docker/linux-build-image.Dockerfile) and for Windows there are "lightweight" [nanoserver images](https://github.com/appveyor/build-images/blob/master/docker/windows-nanoserver-build-image.Dockerfile) and more "heavy" and full-featured [windowsservercore images](https://github.com/appveyor/build-images/blob/master/docker/windows-servercore-build-image.Dockerfile).

Upon installation of AppVeyor Server there are two "Windows" and "Linux" images are configured in Docker cloud settings on Windows and only "Linux" image on Linux.

> Out-of-the-box Docker cloud in AppVeyor is configured to run Windows builds on nanoserver-based image (so, no .NET Framework or Chocolatey there) - it's pulled faster giving you smoother first-time experience. Though it could take some time depending on VM specs/network, so be patient :) You can update the image at any time to servercore-based or your own (more on this later).

Now, to run a build in **any** image open project settings, navigate to **Environment** and add `docker_image` environment variable with full image name as a value, for example:

    docker_image: node:10

There is a difference between the build running with **agent-less** image versus build running with AppVeyor image. In agent-less container only the following parts of build configuration will be executed:

* **Cloning** repository;
* Script from **Build** tab;
* Script from **Test** tab;
* **On build success script** from General tab;
* **On build error script** from General tab;
* **On build finish script** from General tab;

What if you need to **pull the latest version of image on every build run**? Add the following environment variable:

    docker_pull: always

Proceed to the next section to know how to build complex CI/CD workflows with services, parallel and sequential jobs.

#### Complex pipelines with multiple jobs

Configuring the build on UI is good for the start and probably enough for most of the projects, but for more sophisticated scenarios you should move build configuration to a YAML file.

There could be multiple real-world scenarios solved:

* Run containers in parallel testing against multiple versions of the language/framework or a platform.
* Chaining jobs: run container A, then B, then C on-by-one.
* Fan-out scenarios where one job does some sort of preparation (say, building solution) while other jobs wait for it and then start in parallel.
* Fan-in scenarios where one job waits for other jobs running in parallel and then process their results, for example pushing artifacts.

To get the taste of YAML configuration check-in `.appveyor.yml` file with the following contents to the root of project repository:

```yaml
build_cloud: docker

environment:
  matrix:

  - job_name: Node.js 8 tests
    docker_image: node:8

  - job_name: Node.js 12 tests
    docker_image: node:12

for:
-
  matrix:
    only:
      - job_name: Node.js 8 tests
  test_script:
  - node --version
  - npm --version

-
  matrix:
    only:
      - job_name: Node.js 12 tests
  test_script:
  - node --version
  - npm --version
```

Here we declared two jobs - one for running tests inside `node:8` container and another inside `node:12`. Both jobs will be run in parallel and display `node` and `npm` versions.

Now, let's say our tests depend on Redis, so we have to add 3rd container running Redis:

```yaml
build_cloud: docker

environment:
  matrix:

  - job_name: Node.js 8 tests
    job_group: tests
    docker_image: node:8

  - job_name: Node.js 12 tests
    job_group: tests
    docker_image: node:12

  - job_name: redis
    docker_image: redis
    job_allow_cancellation: true

for:
-
  matrix:
    only:
      - job_group: tests
  test_script:
  - node --version
  - npm --version
```

Some new things here:

* With `job_group` variable we grouped both tests into `tests` group and then defined a common test script below.
* A job with Redis container is called `redis` - this is going to be a network alias that can be used to connect Redis service from other containers. All containers in a build get their network aliases assigned as a `job_name` value converted to a slug. For example, in the example above container with Node 8 tests will have network alias `node-js-8-tests`.
* By adding `job_allow_cancellation: true` to `redis` job we are telling AppVeyor that it's OK to cancel that job when all other jobs are succeeded/failed. `job_allow_cancellation` attribute could be added to any job, not just service-like containers like Redis, MySQL, ElasticSearch. For example, it could be another Node.js container doing some polling in a loop during the entire build.

Fan-in/-out and jobs chaining:

```yaml
build_cloud: docker

environment:
  matrix:

  - job_name: Build
    docker_image: alpine

  - job_name: Tests A
    job_group: tests
    job_depends_on: Build
    docker_image: node:12

  - job_name: Tests B
    job_group: tests
    job_depends_on: Build
    docker_image: node:12

  - job_name: Package
    job_depends_on: tests

  - job_name: Deploy
    job_depends_on: Package

# The rest is cut for brevity
...
```

The entire `.appveyor.yml` can be [downloaded here](https://gist.githubusercontent.com/FeodorFitsner/bd04fdcb7df10089a16aebb9af5658e6/raw/fa0e23a54fb05df38bc511c70b61ad5547d33a55/job-depends-on.yml).

Here we can see a new job attribute `job_depends_on` that tells AppVeyor to start a job when preceding job  or a group are finished. You can provide a comma-separated list of job or group names in `job_depends_on` value.

There is a common "bin" directory available inside all containers of the same build. It's mapped as `/appveyor/bin` in Linux containers and `C:\appveyor\bin` in Windows containers. The files copied to that folder are visible to all containers within the build. "Bin" directory can be used as a communication and synchronization tool in complex builds. For example, one job could build a solution and put compiled program into `bin` folder. Then other containers could start tests in binaries from "bin" folder and, finally, another job could take the binaries and upload them as artifacts.

By default, all containers (except the ones with `docker_image` attribute and without `build_script` or `test_script` sections) will be checking out repository contents as a a first step. To disable repository cloning on job start add `clone: off` to job definition:

```yaml
...

-
  matrix:
    only:
      - job_group: tests
  clone: off
  test_script:
  - node --version
  - npm --version

...
```


#### Customizing AppVeyor image

You can customize AppVeyor build agent image with your custom software/tools and then run Docker builds in it.

The following example takes `appveyor/build-image:minimal-windowsservercore-ltsc2019` as the base image with Chocolatey and AppVeyor Build Agent pre-installed and additionally installs [JDK 11](https://chocolatey.org/packages/jdk11) and [Maven](https://chocolatey.org/packages/maven).

Create `Dockerfile` with the following contents:

```Dockerfile
FROM appveyor/build-image:minimal-windowsservercore-ltsc2019

RUN choco install -y jdk11 && \
    choco install -y maven
```

That's it! Now from the same directory run `docker build` to build the image tagged as `my-company/my-custom-image`:

    docker build -t my-company/my-custom-image .

Login to AppVeyor, navigate to **Account &rarr; Build environment**, open **Docker** cloud settings and update Windows "Docker image" to `my-company/my-custom-image`.

### Azure VMs

You can connect both hosted and on-premise AppVeyor to your own Azure subscription to allow AppVeyor instantiating build VMs in it. It has a lot of benefits like having ability to customize your build image, select desired VM size, set custom build timeout and many others.

To simplify setup process we created a script which provisions necessary Azure resources, runs Hashicorp Packer to create a basic build image (based on Windows Server 2019), and puts all AppVeyor configuration together. After running that script you immediately will be able to start builds on Azure (and optionally customize your Azure build environment later).

#### Ensure AppVeyor URL is available from the Internet

Skip this step if you are connecting hosted AppVeyor to your Azure subscription, as its URL (`https://ci.appveyor.com`) is always available from the Internet.

For AppVeyor Server you need to ensure that build worker VMs can establish a connection to AppVeyor server. Therefore, it's public URL should be resolvable with public DNS and listen on HTTPS port 443 (or HTTP port 80, though clear HTTP is not recommended nowadays). You can change the URL and set up SSL (AppVeyor Server has built-in free `Let's Encrypt` and custom certificates support) at `system/settings`.

#### Install Packer and Azure PowerShell module

Install Packer with Chocolatey (skip if Packer already installed):

    choco install packer

*Ensure to restart your command line shell or run `refreshenv` so `packer` command is available.*

*If you do not use Chocolatey, install directly from [Packer](https://www.packer.io/intro/getting-started/install.html) website. In this case ensure that path to `packer` command is set correctly and it can be called from the command line shell.*

Install Azure PowerShell module (skip if Azure PowerShell already installed):

    Install-Module -Name Az -AllowClobber

#### Get necessary files

Clone `appveyor/build-images` repository (we assume Git is installed):

    git clone https://github.com/appveyor/build-images.git

Switch to the cloned folder:

    cd .\build-images

#### Optionally add custom software installaton scripts

Default build worker image will be created with the most essential software installed. You can check it by exploring `minimal-windows-server.json` file. If you need to run your own software installation scripts on top of it, simple copy them to `scripts\Windows\custom-scripts`. Note that it should be a PowerShell scripts.

#### Run provisioning script

If you are in PowerShell session run:

    .\connect-to-azure.ps1

If you are in CMD session run:

    powershell .\connect-to-azure.ps1

Script will ask to enter required information and make a few selections. It is the recommended way of running this script for the first time. However if you prefer script to run without any interactions, you can pass all required parameters to it:

    .\connect-to-azure.ps1 -appveyor_api_key <appveyor_account_api_key> -appveyor_url <appveyor_url> -azure_location <azure_location> -azure_vm_size <azure_vm_size> -skip_disclaimer -use_current_azure_login

*Note that script expects short notation for both `-azure_location` and `-azure_vm_size` parameters, like `westus` and `Standard_D2s_v3` (not their display names).*

For more advanced options call `get-help .\connect-to-azure.ps1 -detailed` or check script source code.

#### Final steps

Wait for the script to complete. It should take about an hour depending on VM size and optional software installation scripts addition. Upon completion script will print build worker image name (which is `Windows Server 2019 on Azure` if you do not change it with the `-image_description` parameter). Just set `image: Windows Server 2019 on Azure` in `appveyor.yml` (or select `Windows Server 2019 on Azure` in the **Settings > Environment** tab if you use UI), and you can start building on Azure!

## Maintenance

* [Upgrading AppVeyor](/docs/server/maintenance/#upgrading-appveyor-server)
* [Backup/restore AppVeyor](/docs/server/maintenance/#backuprestore-appveyor-server)
* [Troubleshooting](/docs/server/maintenance/#troubleshooting)
