---
layout: docs
title: Running builds on Hyper-V
---

<!-- markdownlint-disable MD022 MD032 -->
# Running builds on Hyper-V
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Prepare host machine

### Minimum requirements:

* Windows Server 2012 R2 (Windows 8.1) or higher
* .NET Framework 4.5.2
* Hyper-V role installed
* Enough free memory and disk space to run guest VMs
* Internet connectivity
    * Currently we require outbound Internet connectivity at TCP (not HTTP) level (behind router or NAT). We are working on proxy support, please watch [this](https://github.com/appveyor/ci/issues/1303) issue

### Create Virtual Switch

If Hyper-V host already has **Virtual Switch** of type **External**, which uses Hyper-V Server NIC with access to the Internet, and it is OK to use it for build VMs, please go to [Create Master VM](/docs/enterprise/running-builds-on-hyper-v/#create-master-vm) step

* In **Hyper-V manager** navigate to **Virtual Switch Manager** on right top panel
* In **Virtual Switch Manager** select **External** and press **Create Virtual Switch**
* In **External network** drop-down select NIC which has Internet access
* Enter custom name in the **Name** field

## Create Master VM

* Create new VM in Hyper-V
    * There are no special requirements to VMs disk and memory size. It mostly depends on what your build process requires
    * Ensure Virtual NIC is connected to existing **Virtual Switch** with outbound Internet access or to switch created in [Create Virtual Switch](/docs/enterprise/running-builds-on-hyper-v/#create-virtual-switch) step
    * Operating system should be Windows Server 2012 R2 or higher. It can be freshly installed OS, or existing VHD with pre-installed software

## Setup Master VM

Login into master VM via RDP.

Follow [these steps](/docs/enterprise/setup-master-vm/) to configure VM and install software required for your build process. It is tested PowerShell scripts which can be simply copy-pasted to PowerShell window (started in privileged mode). Specifically:

* [Basic configuration](/docs/enterprise/setup-master-vm/#basic-configuration) and [Essential 3rd-party software](/docs/enterprise/setup-master-vm/#essential-3rd-party-software) - we strongly recommend to install everything from those sections.
* [Build framework](/docs/enterprise/setup-master-vm/#build-framework) - you can skip one of MSBuild and Visual Studio versions or both if you don't need them.
* [Test framework](/docs/enterprise/setup-master-vm/#test-framework) - you can skip that step if you are running your own custom test script/framework.
* [AppVeyor Build Agent](/docs/enterprise/setup-master-vm/#appveyor-build-agent) and [Configuring agent to run in "Interactive" mode](/docs/enterprise/setup-master-vm/#configuring-agent-to-run-in-interactive-mode) - these steps are mandatory.

Install any additional software required for your builds.

Do not sysprep master VM!

## Prepare master VHD

* Shutdown Master VM
* Open **Virtual Hard Disks** folder (it can be found in Hyper-V settings, usually `C:\Users\Public\Documents\Hyper-V\Virtual Hard Disks`) and copy it to the folder AppVeyor will read it from, for example `C:\VHDs`
    * Alternatively original `.vhdx` file of Master VM can be used by AppVeyor. This is OK if Master VM is stopped and only one Hyper-V server if used. In case of many Hyper-V servers, Master VM `.vhdx` should be copied across all Hyper-V servers

## Setting up custom cloud and images in AppVeyor

* Login to AppVeyor portal
* Navigate to your account level **Settings** in the top manu and select **Build environment** option from the drop-down
    * If **Build environment** option is not available, please contact [support@appveyor.com](mailto:support@appveyor.com) and ask to enable **Private build clouds** feature
* Press **Add cloud**, select cloud type **Hyper-V**

**Complete the following settings**:

* **Name**: Name for your private build cloud. Make it meaningful and short to be able to use in YAML configuration
* **Host authorization token**: generate host authorization token or enter it manually
* Virtual machine configuration
    * **CPU cores**: number of virtual CPU cores per VM. We do not have specific requirements, but do not advice overcommit too much. For example, if host machine has 4 logical CPU cores and you will run 2 builds in parallel, it makes sense to assign 2 cores per machine.
    * **RAM, MB**: amount of Virtual machine RAM. Please use amount of free memory on Hyper-V Server and number of parallel builds to calculate.
    * **Virtual machines location**: folder where virtual machines will be created
* Networking
    * **Virtual switch name**: Name of virtual switch created during [Create Virtual Switch](/docs/enterprise/running-builds-on-hyper-v/#create-virtual-switch) step (or existing virtual switch)
    * **Use DHCP** checkbox. If subnet connected with Virtual Network switch NIC has DHCP server in it (most common case), leave this checkbox as is. Otherwise uncheck it and set how IP addresses will be assigned
* Images
    * **IMAGE NAME**: Image name as you want to see it in AppVeyor UI and YAML, for example **VS2013 with WMF3**
    * **VHD PATH**: Path to master VHD on Hyper-V server selected in step [Prepare master VHD](/docs/enterprise/running-builds-on-hyper-v/#prepare-master-vhd)
* Open **Failure strategy** and set the following:
    * **Job start timeout, seconds**: 180 is good enough for modern Hyper-V server. However, if VM creation and build start takes longer for you, please adjust accordingly
    * **Provisioning attempts**: 2 is good for start. Later you may need to change it according to your observations

## Make build worker image available for configuration

* Navigate to **Build environment** > **Build worker images**
* Press **Add image**
* Enter what you set as **IMAGE NAME** in previous step

## Download and install AppVeyor Host agent on Host machine

* [Download location](https://www.appveyor.com/downloads/host-agent/latest/AppveyorHostAgent.msi)
* Installation settings
    * **Host authorization token**: token generated or manually entered during [Setting up custom cloud and images in AppVeyor](/docs/enterprise/running-builds-on-hyper-v/#setting-up-custom-cloud-and-images-in-appveyor) step

## How to route build to your own cloud

At **project** level:

* **UI**:
    * **Settings** > **Environment** > **Build cloud**: Select your private build cloud name from drop-down
    * **Settings** > **Environment** > **Build worker image**: Select your build worker image from drop-down
* **YAML**:

```yaml
build_cloud: <private_build_cloud_name>
image: <private_build_cloud_image>
```

At **project** level:

* Set environment variable "APPVEYOR_BUILD_WORKER_CLOUD" to your private build cloud name
    * This assumes that default and custom build clouds have build worker image with the same name (for example **Visual Studio 2015**)
