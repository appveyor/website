---
layout: docs
title: Running builds as local process
---

<!-- markdownlint-disable MD022 MD032 -->
# Running builds as local process
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Enable custom build environment

Currently custom build environment feature is not generally available. It is being enabled for specific accounts per request. Please send an email to [team@appveyor.com](mailto:team@appveyor.com) if you decide to try this feature.

## Prepare agent machine

### Minimum requirements:

* Windows Server 2012 R2 (Windows 8.1) or higher
* .NET Framework 4.5.2
* TODO: memory and disk space
* Internet connectivity
    * Currently we require outbound Internet connectivity at TCP (not HTTP) level (behind router or NAT). We are working on proxy support, please watch [this](https://github.com/appveyor/ci/issues/1303) issue

## Setup agent machine software

### Download and install AppVeyor Host agent on agent machine

* [Download location](https://www.appveyor.com/downloads/host-agent/latest/AppveyorHostAgent.msi)
* Installation settings
    * **Host authorization token**: token generated or manually entered during [Setting up custom cloud and images in AppVeyor](/docs/enterprise/running-builds-as-local-process/#setting-up-custom-cloud-and-images-in-appveyor) step

### Download and install AppVeyor build agent on agent machine

* [Download location](http://www.appveyor.com/downloads/build-agent/latest/AppveyorBuildAgent.msi)
    * Accept all default settings duing installation    

### Download and install additional software required by build proccess
* [Setup master VM](/docs/enterprise/setup-master-vm/) documentation is good reference. This documentation was created for cloud environments, and not everything is needed for local agent build process. Please install what is needed for your build scenario. However [Basic configuration](/docs/enterprise/setup-master-vm/#basic-configuration) and [Essential 3rd-party software](/docs/enterprise/setup-master-vm/#essential-3rd-party-software) are required in most of scenarios.
* Steps named [AppVeyor Build Agent](/docs/enterprise/setup-master-vm/#appveyor-build-agent) and [Tuning for Interactive mode](/docs/enterprise/setup-master-vm/#tuning-for-interactive-mode) are not relevant for local agent scenario.


## Setting up custom cloud and images in AppVeyor

* Login to AppVeyor portal
* Navigate to your account name on the top right and select **Build environment** option from drop-down menu
    * If **Build environment** option is not available, please contact [team@appveyor.com](mailto:team@appveyor.com) and ask to enable **Private build clouds** feature
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
