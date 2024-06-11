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

In Parallels Control Center GUI click "+" button to create a new VM. Browse Free Systems and click on "Install MacOS Using the recovery Partition". It will create VM and start MacOS Recovery. It will show "MacOS Utilities" window first. Click Reinstall MacOS and follow further instructions. During installation of Mac OS configure `appveyor` user.

When MacOS is installed login as `appveyor` user and continue with the following steps:

1. In a terminal:
    * Run `sudo bash` to get root privileges
    * Add user `appveyor` to sudoers with `NOPASSWD`: `echo -e 'appveyor\tALL=(ALL)\tNOPASSWD: ALL\nDefaults:appveyor        !requiretty' > /etc/sudoers.d/appveyor`
    * Set Computer Sleep to "Never" with command: `sudo systemsetup -setcomputersleep Never`
2. In System Preferences:
    * Click Sharing icon and enable "Remote login" (sshd) for Administrators group.
    * Click "Users & Groups" icon and then click on Login Options. Turn on Automatic login for `appveyor` user.
3. Install Parallels Guest Tools by clicking yellow triangle at top right corner of VM's window. This will mount Parallels Guest Tools dvd into VM. Click on it and proceed with installation. After installation it will require to restart VM.
4. In Host's terminal optimize VM: `prlctl set <VMNAME> --pause-idle off --faster-vm on --nested-virt on --auto-compress off --adaptive-hypervisor on --isolate-vm on`

Now its time to run [Packer](https://packer.io/) to install software into build image.

On host machine's terminal window while in user's Home directory:

1. Download [Packer](https://packer.io/downloads.html), unpack it and copy packer executable to `/usr/local/bin` (or any other directory in your PATH variable): `cp packer /usr/local/bin/packer`.
2. Clone build-images repository: `git clone https://github.com/appveyor/build-images.git`, change current directory to the repository e.g. `cd build-repository`
3. Unregister VM prepared earlier: `prlctl unregister <VMNAME>`
4. Locate VM's folder, usually it's `$HOME/Parallels/<VMNAME>.pvm`. Copy to clipboard full path to the VM's folder for the next step.
5. Prepare [var-file](https://packer.io/docs/templates/user-variables.html#from-a-file) for Packer named `vault.json` with sensitive values:

    ```json
    {
    "pvm_path": "VM's Location on host's disk",
    "appVeyorUrl": "https://ci.appveyor.com or URL to Appveyor Server",
    "hostAuthorizationToken": "Host agent authorization token",
    "appleIdUser": "YOUR APPLE ID",
    "appleIdPasswd": "YOUR APPLE ID Password"
    }
    ```

6. Run `packer build --only=parallels-pvm  --var-file=vault.json macos.json`. Packer will create another PVM in output-parallels-pvm subfolder. Packer will name that PVM according to pattern `packer-{{macos_version}}-{{timestamp}}` e.g. `packer-catalina-201911280809`
7. move that PVM to `~/Parallels` folder: `mv output-parallels-pvm/packer-{{macos_version}}-{{timestamp}}.pvm ~/Parallels`
8. register PVM: `prlctl register ~/Parallels/packer-{{macos_version}}-{{timestamp}}.pvm`
9. and rename PVM to whatever you like. For example next command will rename it to `macos-_prod`: `prlctl set packer-{{macos_version}}-{{timestamp}} --name macos_prod`. Note, this command will rename VM's name and Vm's subfoler in `~/Parallels`.
10. VM's name should be put into Appveyor's build cloud configuration.

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
