---
layout: docs
title: Running builds on Google Compute Engine
---

<!-- markdownlint-disable MD022 MD032 -->
# Running builds on Google Compute Engine
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Create Google Cloud Platform service account

* Open Google Cloud Platform menu and select existing or create new project to use for AppVeyor build environment
* [Create Google Cloud Platform service account and obtain certificate](/docs/enterprise/creating-gcp-service-account/)

## Create Master VM

* Go back to Google Cloud Platform menu and select **Compute engine**
* Navigate to **VM instances** and press **Create**
    * Set descriptive **Name**, for example **master-vm**
    * Optionally change **Zone**, to be the same as build VMs, closer to other GCE or non-GCE resources needed during build, test or deployment
    * Optionally increase number of CPUs or memory
    * Press **Change** in **Boot disk**
        * Select **Windows Server 2012 R2**
        * Select **SSD persistent disk**
    * Press **Select** and then **Create**
    * In drop-down menu near **RDP** select **Create or reset Windows password**
        * Set user name to `appveyor` and press **Set**
        * Save auto-generated password for future use
    * In drop-down menu near **RDP** select **Download RDP file**
        * Click RDP file and in RDP window change username to `appveyor` and use password saved before

## Setup Master VM

* Follow [those steps](/docs/enterprise/setup-master-vm/) to setup software required for build process. It is tested PowerShell scripts which can be simple copy-pasted to PowerShell window (started in privileged mode). Some notes:
    * We strongly recommend to run everything from [Basic configuration](/docs/enterprise/setup-master-vm/#basic-configuration) and [Essential 3rd-party software](/docs/enterprise/setup-master-vm/#essential-3rd-party-software)
    * You can skip one of msbuild and VS version or both if you don't need them in [Build framework](/docs/enterprise/setup-master-vm/#build-framework) step
    * You can skip test framework you do not need in [Test framework](/docs/enterprise/setup-master-vm/#test-framework) step
    * Steps [AppVeyor Build Agent](/docs/enterprise/setup-master-vm/#appveyor-build-agent) and [Tuning for Interactive mode](/docs/enterprise/setup-master-vm/#tuning-for-interactive-mode) are mandatory
    * **Important note:** in Build Agent installation script set `APPVEYOR_MODE` to `GCE`. Alternatively update registry with [Set Build Agent mode to **GCE**](https://github.com/appveyor/ci/blob/master/scripts/enterprise/set_gce_build_agent_mode.ps1)
* Install additional software if needed

## Prepare Master VM snapshot

* Optionally restart Master VM to ensure AppVeyor build agent is started on automatically interactively
* Shutdown Master VM
* In **Compute engine** menu select **Snapshots** and press **Create snapshot**
    * Set descriptive **Name**, for example **build-vm-2017-05-09**. It is handy to include date to manage build images versions
    * Select Master VM created before as a **Source disk**
    * Press **CREATE**

## Setting up custom cloud and images in AppVeyor

* Login to AppVeyor portal
* Navigate to your account name on the top right and select **Build environment** option from drop-down menu
    * If **Build environment** option is not available, please contact [team@appveyor.com](mailto:team@appveyor.com) and ask to enable **Private build clouds** feature
* Press **Add cloud**, select cloud type **Google Compute Engine**

**Complete the following settings**:

* **Name**: Name for your private build cloud. Make it meaningful and short to be able to use in YAML configuration
* **Google account**
    * **Service account email**: **Service account ID** from [Create Google Cloud Platform service account](/docs/enterprise/running-builds-on-gce#create-google-cloud-platform-service-account) step
    * **Service account certificate (Base64)**: content of the file created in [Create Google Cloud Platform service account](/docs/enterprise/running-builds-on-gce#create-google-cloud-platform-service-account) step
    * **Project name**: ID of Google Cloud Platform project selected or created in the beginning
* Virtual machine configuration
    * **Zone name**: select the same as used for Master VM
    * **Machine type (size)**: select depending on performance you need
    * **Tags**: Optionally add tags
* Networking
    * **Network name**: Optionally set custom VPC network
        * Select **Assign external IP address** if VMs need to be accessible from outside
* Images
    * **IMAGE NAME**: Image name as you want to see it in AppVeyor UI and YAML, for example **VS2017 on GCE**
    * **SNAPSHOT OR IMAGE NAME**: Image name as it was set in [Prepare Master VM snapshot](/docs/enterprise/running-builds-on-gce#prepare-master-vm-snapshot) step
    * **SIZE, GB**: VM disk size in Gb
* Open **Failure strategy** and set the following:
    * **Job start timeout, seconds**: 360 is good enough for GCE. However, if VM creation and build start takes longer for you, please adjust accordingly
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
