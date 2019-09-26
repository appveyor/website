---
layout: docs
title: BYOC - Running builds in Hyper-V VMs
---

<!-- markdownlint-disable MD022 MD032 -->
# Running builds in Hyper-V virtual machines
{:.no_toc}

AppVeyor BYOC allows connecting an existing server with Hyper-V (your Windows 10 workstation, cloud VM or the server in your LAN) and running builds in Windows or Linux VMs.

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Requirements

* Supported **Hyper-V host** operating systems:
    * Windows 10 Version 1803 and later
    * Windows Server 2016 and later
    * Azure [Dsv3-series](https://docs.microsoft.com/en-us/azure/virtual-machines/windows/sizes-general#dsv3-series-1) VMs with nested virtualization enabled
* Supported **guest** operating systems:
    * Windows Server 2019
    * Windows Server 2016
    * Ubuntu 18.04
* Hyper-V feature enabled on the host

## Assisted setup

Following cloud configuration wizard is the fastest and the easiest way to configure Docker computer to run your builds. At the end of the wizard you'll be given a few commands that you run on the Hyper-V host to get it up and running in AppVeyor.

In AppVeyor web portal:

* Select **Self-hosted jobs** in the top menu;
* Click **Add cloud** to start "Add your own cloud" wizard;
* Choose **Hyper-V**;
* Select **Base image** to configure image builder and click **Next**;
* Follow the instructions. `Connect-AppVeyorToHyperV` cmdlet ([source](https://github.com/appveyor/build-images/blob/master/Connect-AppVeyorToHyperV.ps1)) will build master image (.vhdx), configure a new cloud, install AppVeyor Host Agent on the computer and connect it to the cloud.
* Click **Finish** to return to **BYOC** page and make sure the status of created cloud is **Active** meaning Host Agent was able to connect to AppVeyor.
* [Update project settings to run builds on your computer](/docs/byoc/#routing-builds-to-your-cloud).