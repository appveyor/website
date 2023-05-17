---
layout: docs
title: BYOC - Running builds on macOS
---

<!-- markdownlint-disable MD022 MD032 -->
# Running builds on macOS
{:.no_toc}

AppVeyor BYOC allows connecting an existing Mac computer (your workstation, cloud VM or the server in your LAN) and running builds directly on the host operating system.

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Requirements

* Supported operating system:
    * macOS 10.13 "High Sierra" and later
    * [Homebrew](https://brew.sh/) package manager
* Computer must be connected to the internet;
* Git, Mercurial or Subversion depending on the repository of your choice.

## Assisted setup

Following cloud configuration wizard is the fastest and the easiest way to configure macOS computer to run your builds. At the end of the wizard you'll be given a few commands that you run on the target computer to get it up and running in AppVeyor.

In AppVeyor web portal:

* Select **Self-hosted jobs** in the top menu;
* Click **Add cloud** to start "Add your own cloud" wizard;
* Choose **Your computer**, **macOS** and click **Next**;
* Follow the instructions. `Connect-AppVeyorToComputer` cmdlet ([source](https://github.com/appveyor/build-images/blob/master/Connect-AppVeyorToComputer.ps1)) will configure a new cloud, install AppVeyor Host Agent on the computer and connect it to the cloud.
* Click **Finish** to return to **BYOC** page and make sure the status of created cloud is **Active** meaning Host Agent was able to connect to AppVeyor.
* [Update project settings to run builds on your computer](/docs/byoc/#routing-builds-to-your-cloud).

## Manual setup

For better understanding/control of the process or troubleshooting below are the instructions for manual configuration of builds on macOS computer.

### Add new build cloud

In AppVeyor web portal:

* Select **Self-hosted jobs** in the top menu;
* Click **Add cloud** then choose **Add cloud manually**;
* Choose **Process** cloud type.
* Specify **Cloud name**, for example `Mike's computer` and generate **Host agent authorization token** (or provide your own - it's basically AppVeyor Host Agent identifier and the password it connects to AppVeyor with);
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

### Troubleshooting

#### Stuck workers

Sometimes, on clouds list page you can notice it doesn't display "0/X" in "Usage" column though there are no running build jobs on that cloud.

You can try to "reset" AppVeyor Host Agent state, by deleting its internal database and restarting the service.

Host agent database file `host-agent.db` as well as `host-agent.stderr.log` and `host-agent.stdout.log` logs are located at either `/usr/local/var/opt/appveyor/host-agent` or `/usr/local/var/appveyor/host-agent/` location.

```text
brew services stop appveyor-host-agent
rm /usr/local/var/appveyor/host-agent/host-agent.db
brew services start appveyor-host-agent
```

