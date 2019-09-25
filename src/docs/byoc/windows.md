---
layout: docs
title: BYOC - Running builds on Windows
---

<!-- markdownlint-disable MD022 MD032 -->
# Running builds on Windows
{:.no_toc}

AppVeyor BYOC allows connecting an existing Windows computer (your workstation, cloud VM or the server in your LAN) and running builds directly on the host operating system.

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Requirements

* Supported operating system:
    * Windows 10 Version 1607 x64 and later
    * Windows Server 2012 R2 SP1 x64 and later
    * Windows 8.1 x64
    * Windows 7 SP1 x64
* Computer must be connected to the internet;
* Git, Mercurial or Subversion depending on the repository of your choice.

## Assisted setup

Following cloud configuration wizard is the fastest and the easiest way to configure Windows computer to run your builds. At the end of the wizard you'll be given a few commands that you run on the target computer to get it up and running in AppVeyor.

In AppVeyor web portal:

* Select **BYOC** in the top menu;
* Click **Add cloud** to start "Add your own cloud" wizard;
* Choose **Your computer**, **Windows** and click **Next**;
* Follow the instructions. `Connect-AppVeyorToComputer` cmdlet ([source](https://github.com/appveyor/build-images/blob/master/Connect-AppVeyorToComputer.ps1)) will configure a new cloud, install AppVeyor Host Agent on the computer and connect it to the cloud.
* Click **Finish** to return to **BYOC** page and make sure the status of created cloud is **Active** meaning Host Agent was able to connect to AppVeyor.
* [Update project settings to run builds on your computer](/docs/byoc/#routing-builds-to-your-cloud).

## Manual setup

For better understanding/control of the process or troubleshooting below are the instructions for manual configuration of builds on Windows computer.

### Add new build cloud

In AppVeyor web portal:

* Select **BYOC** in the top menu;
* Click **Add cloud** then choose **Add cloud manually**;
* Choose **Process** cloud type.
* Specify **Cloud name**, for example `Mike's computer` and generate **Host agent authorization token** (or provide your own - it's basically AppVeyor Host Agent identifier and the password it connects to AppVeyor with);
* Click **Add** to create the cloud.

### Add new build worker image

* On **BYOC** page navigate to **Images** and click **Add image**;
* Specify `Windows` as **Name** and choose `Windows` in **OS type** dropdown;
* Click **Add** to create the image.

### Install AppVeyor Host Agent

AppVeyor Host Agent is a lightweight service running on your Windows machine that connects to AppVeyor and runs your builds.

[Download the latest AppVeyor Host Agent](https://www.appveyor.com/downloads/appveyor/appveyor-host-agent.msi) and follow the installation wizard.

Alternatively, use this PowerShell script (run in elevated mode) to download and install Host Agent service:

```posh
$auth_token = '<your-host-authorization-token-here>'
$appveyor_url = 'https://ci.appveyor.com' # change to your AppVeyor URL if connecting to a self-hosted AppVeyor Server installation

(New-Object Net.WebClient).DownloadFile("https://www.appveyor.com/downloads/appveyor/appveyor-host-agent.msi", "$env:temp\appveyor-host-agent.msi")
cmd /c msiexec /i "$env:temp\appveyor-host-agent.msi" /quiet APPVEYOR_URL=$appveyor_url HOST_AUTHORIZATION_TOKEN=$auth_token
```

Make sure the service is running:

    Get-Service Appveyor.HostAgent

### Changing Host Agent authorization token

If you need to change Host Agent authorization token to connect the agent to a different cloud you can update `Computer\HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\HostAgent\AuthorizationToken` value in Windows Registry.

After changing authorization token stop Host Agent service:

    Stop-Service Appveyor.HostAgent

delete Host Agent database file `host-agent.db` in `%ProgramData%\AppVeyor\HostAgent` directory and start Host Agent service again:

    Start-Service Appveyor.HostAgent