---
layout: docs
title: BYOC - Running builds in Docker containers
---

<!-- markdownlint-disable MD022 MD032 -->
# Running builds in Docker containers
{:.no_toc}

AppVeyor BYOC allows connecting an existing computer with Docker (your workstation, cloud VM or the server in your LAN) and running builds in containers.

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Requirements

* Supported Docker host operating systems:
    * Windows 10 Version 1803 and later
    * Windows Server 2019 and later
    * Ubuntu 18.04
    * macOS 10.13 “High Sierra” and later
* Supported container operating systems:
    * Windows Server Core and Windows Nano Server (Windows hosts only)
    * Linux (Windows, Linux and macOS hosts)
* Docker installed and configured. `docker` tool available in PATH.
* Supported Docker editions:
    * Docker Desktop for Windows (Docker CE)
    * Docker Engine Enterprise (Docker EE) on Windows Server
    * Docker CE on Linux
    * Docker Desktop for Mac

## Assisted setup

Following cloud configuration wizard is the fastest and the easiest way to configure Docker computer to run your builds. At the end of the wizard you'll be given a few commands that you run on the target computer to get it up and running in AppVeyor.

In AppVeyor web portal:

* Select **Self-hosted jobs** in the top menu;
* Click **Add cloud** to start "Add your own cloud" wizard;
* Choose **Docker** and then operating system running on Docker host
* Select containers **Base image** to configure and click **Next**;
* Follow the instructions. `Connect-AppVeyorToDocker` cmdlet ([source](https://github.com/appveyor/build-images/blob/master/Connect-AppVeyorToDocker.ps1)) will configure a new cloud, install AppVeyor Host Agent on the computer and connect it to the cloud.
* Click **Finish** to return to **BYOC** page and make sure the status of created cloud is **Active** meaning Host Agent was able to connect to AppVeyor.
* [Update project settings to run builds on your computer](/docs/byoc/#routing-builds-to-your-cloud).