---
layout: docs
title: BYOC - Running builds on macOS with Parallels Desktop
---

<!-- markdownlint-disable MD022 MD032 -->
# Running builds on macOS with Parallels Desktop
{:.no_toc}

AppVeyor BYOC allows connecting an existing Mac computer (your workstation, cloud VM or the server in your LAN) and running builds in isolated Parallels Desktop VMs.

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Requirements

* Supported operating system:
    * macOS 10.14 "Mojave" and later
    * [Homebrew](https://brew.sh/) package manager
    * Parallels Desktop Pro for Mac
    * Parallels Virtualization SDK for Mac required by Packer. You can download the latest DMG bundle by this [link](http://www.parallels.com/download/pvsdk/)
* Computer must be connected to the internet;

## Manual setup

### Prepare macOS VM image

In Parallels Control Center GUI click "+" button to create new VM. In appeared windows browse Free Systems and click on "Install MacOS Using the recovery Partition". It will create VM and start Mac OS Recovery. It will show "MacOS Utilities" window first. Click Reinstall MacOS and follow furhter instructions. During installation of Mac OS configure `appveyor` user.



### Add new build cloud

In AppVeyor web portal:

* Select **Self-hosted jobs** in the top menu;
* Click **Add cloud** then choose **Add cloud manually**;
* Choose **Parallels** cloud type.
* Specify **Cloud name**, for example `Mike's computer` and generate **Host agent authorization token** (or provide your own - it's basically AppVeyor Host Agent identifier and the password it connects to AppVeyor with);
* [TBD] - configuring image
* Click **Add** to create the cloud.

### Add new build worker image

* On **BYOC** page navigate to **Images** and click **Add image**;
* Specify `macOS` as **Name** and choose `macOS` in **OS type** dropdown;
* Click **Add** to create the image.

### Install AppVeyor Host Agent

AppVeyor Host Agent is a lightweight service running on your macOS machine that connects to AppVeyor and runs your builds.

Using `brew` tool install the latest AppVeyor Host Agent with your `<host-authorization-token>` and AppVeyor URL (if connecting to a self-hosted AppVeyor Server installation):

    HOMEBREW_HOST_AUTH_TKN=<host-authorization-token> HOMEBREW_APPVEYOR_URL=https://ci.appveyor.com brew install appveyor/brew/appveyor-host-agent

Start Host Agent service:

    brew services start appveyor-host-agent

Make sure the service is running:

    brew services list

### Changing Host Agent authorization token

If you need to change Host Agent authorization token to connect the agent to a different cloud you can update its value in `/usr/local/etc/opt/appveyor/host-agent/appsettings.json` file.

After changing authorization token stop Host Agent service:

    brew services stop appveyor-host-agent

delete Host Agent database file `host-agent.db` in `/usr/local/var/opt/appveyor/host-agent` directory and start Host Agent service again:

    brew services start appveyor-host-agent
