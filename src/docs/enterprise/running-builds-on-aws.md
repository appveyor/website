---
layout: docs
title: Running builds on Amazon Web Services
---

<!-- markdownlint-disable MD022 MD032 -->
# Running builds on Amazon Web Services
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Create Master VM

* Signin to **AWS Management Console** and select **EC2** under **Compute**
* Navigate to **Instances** and press **Lunch instance**
    * Choose an Amazon Machine Image (AMI). The following AMIs are tested at the moment:
        * Microsoft Windows Server 2016 Base
        * Microsoft Windows Server 2012 R2 Base        
    * Choose an Instance Type
        * SSD storage is recommended
        * m3.medium type minimum recommended
    * Press **Next: Configure Instance Details** button (not **Review and Launch**)
    * Configure Instance Details. Non-default settings:
        * Subnet: select any specific subnet
        * Auto-assign Public IP: Enable
    * Press **Next: Add Storage** button (not **Review and Launch**). Non-default settings (provided instance type with SSD storage was selected):
        * Set size at least 50Gb        
    * Press **Next: Add Storage** button (not **Review and Launch**). Add the following tag:
        * Key: name, value: MasterVm
    * Press **Review and Launch** and then **Lunch**
    * Select existing key pair or create a new key pair in next dialog window and press **Lunch instances**
    
    
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

Login into master VM via RDP.

Follow [these steps](/docs/enterprise/setup-master-vm/) to configure VM and install software required for your build process. It is tested PowerShell scripts which can be simply copy-pasted to PowerShell window (started in privileged mode). Specifically:

* [Basic configuration](/docs/enterprise/setup-master-vm/#basic-configuration) and [Essential 3rd-party software](/docs/enterprise/setup-master-vm/#essential-3rd-party-software) - we strongly recommend to install everything from those sections.
* [Build framework](/docs/enterprise/setup-master-vm/#build-framework) - you can skip one of MSBuild and Visual Studio versions or both if you don't need them.
* [Test framework](/docs/enterprise/setup-master-vm/#test-framework) - you can skip that step if you are running your own custom test script/framework.
* [AppVeyor Build Agent](/docs/enterprise/setup-master-vm/#appveyor-build-agent) and [Configuring agent to run in "Interactive" mode](/docs/enterprise/setup-master-vm/#configuring-agent-to-run-in-interactive-mode) - these steps are mandatory.

Install any additional software required for your builds.

Do not sysprep master VM!

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
