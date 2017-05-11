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

## Enable custom build environment

Currently custom build environment feature is not generally available. It is being enabled for specific accounts per request. Please send an email to [team@appveyor.com](mailto:team@appveyor.com) if you decide to try this feature.

## Select or create Google Cloud Platform project

* Open Google Cloud Platform menu and select existing or create new project to use for AppVeyor build environment

## Set up credentials

### Create account and download certificate

* Open Google Cloud Platform menu navigate to **IAM & Admin**
* Select **Service accounts** and press **Create service account**
    * Set descriptive **Service account name**, for example **Appveyor CI**
        * **Service account ID** will be automatically regenerated, leave it as is
    * Select **Project > Editor** in **Role** menu
    * Check **Furnish a new private key**
        * Select **P12**
    * Press **CREATE**
    * Close **Service account created** window
        * Leave default password unchanged
    * Certificate in P12 format should be saved to local computer
        * Remember it's location and optionally re-save certificate in some secure place

### Convert certificate to Base64 string

* Rune folloing PowerShell commands:
    
```
$bytes = [System.IO.File]::ReadAllBytes("<path-to-P12-file>")
$base64Str = [System.Convert]::ToBase64String($bytes)
[System.IO.File]::WriteAllText("<path-to-result-TXT-file>", $base64Str)
```

* Remember location of the result TXT file. 
    
## Create Master VM

* Go back to Google Cloud Platform menu and select **Compute engine**
* Navigate to **VM instances** and press **Create**
    * Set descriptive **Name**, for example **master-vm**
    * Optionally change **Zone**, to be the same as buiuld VMs, closer to other GCE or non-GCE resources
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

* Optionally restart Master VM to ensure AppVeyor build agent is started on automatically iteractively
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
    * **Service account email**: email address associalted with your Google Cloud account
    * **Service account certificate (Base64)**: content of the file created in [Convert certificate to Base64 string](/docs/enterprise/running-builds-on-gce#convert-certificate-to-base64-string) step
    * **Project name**: ID of Google Cloud Platform project selected or created in the beginning
* Virtual machine configuration
    * **Zone name**: select the same as used for Master VM
    * **Machine type (size)**: select depending on performance you need
<!---    * **Tags**:
* Networking
    * **Network name**:
        * Select **Assign external IP address** if VMs need to be accessible from outside--->
* Images
    * **IMAGE NAME**: Image name as you want to see it in AppVeyor UI and YAML, for example **VS2017 on GCE**
    * **SNAPSHOT OR IMAGE NAME**: Image name as it was set in [Prepare Master VM snapshot]()
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
