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

* Login to **AWS Management Console** and select **EC2** under **Compute**.
* Navigate to **Instances** and press **Lunch instance**.
    * Choose an Amazon Machine Image (AMI). The following AMIs are tested at the moment:
        * Microsoft Windows Server 2016 Base.
        * Microsoft Windows Server 2012 R2 Base.
    * Choose an Instance Type.
        * **SSD** storage is strondly recommended.
        * **m3.medium** instance type is minimum recommended.
    * Press **Next: Configure Instance Details** button (not **Review and Launch**).
    * Configure Instance Details. Non-default settings:
        * Subnet: select any specific subnet.
        * Auto-assign Public IP: Enable.
    * Press **Next: Add Storage** button (not **Review and Launch**).
        * Set size at least 50Gb.
    * Press **Next: Add Tags** button (not **Review and Launch**). Add the following tag:
        * Key: name, value: MasterVm.
    * Press **Review and Launch** and then **Lunch**.
    * Select existing key pair or create a new key pair in next dialog.
        * Save key pair file (`*pem` file) in secure location.
        * Press **Lunch instances**.
    * Press **Lunch instances** and wait until instance state for **MasterVM** instance is **running**.
    * Press **Connect**.
        * Press **Get password** and use key pair file saved earlier to retrieve a password.
        * Press **Back** and then **Download Remote Desktop file**.
        * User RDP file and password to connect to the VM.
    * Press **Start** button, type `Computer management` and navigate to **Local users and groups** / **Users**.
    * Create a new user:
        * Username: `appveyor`. You can use custom user name too. Just ensure that all subsequent VM configuration steps performed while being logged as this user.
        * Password: `<your password>`.
        * Uncheck *User must change password at next logon*.
        * Check *Password never expires*.

## Setup Master VM

Login into Master VM via RDP (as user created before).

Follow [these steps](/docs/enterprise/setup-master-vm/) to configure VM and install software required for your build process. It is tested PowerShell scripts which can be simply copy-pasted to PowerShell window (started in privileged mode). Specifically:

* [Basic configuration](/docs/enterprise/setup-master-vm/#basic-configuration) and [Essential 3rd-party software](/docs/enterprise/setup-master-vm/#essential-3rd-party-software) - we strongly recommend to install everything from those sections.
* [Build framework](/docs/enterprise/setup-master-vm/#build-framework) - you can skip one of MSBuild and Visual Studio versions or both if you don't need them.
* [Test framework](/docs/enterprise/setup-master-vm/#test-framework) - you can skip that step if you are running your own custom test script/framework.
* [AppVeyor Build Agent](/docs/enterprise/setup-master-vm/#appveyor-build-agent) and [Configuring agent to run in "Interactive" mode](/docs/enterprise/setup-master-vm/#configuring-agent-to-run-in-interactive-mode) - these steps are mandatory.

Install any additional software required for your builds.

**Do not sysprep Master VM!**

## Prepare Master VM image

* Optionally restart Master VM to ensure AppVeyor build agent is started automatically interactively.
* Navigate to **Instances**, right-click **MasterVM**, select **Instance state** and then **Stop**.
* Wait till **MasterVM** state is **stopped**.
* Right-click **MasterVM**, select **Image** and press **Create image**.
* Provide descriptive name for the image, for example **Master-image-1**.
* Press **Create**.
* Ensure image created by checking **Instances** > **AMIs** view.

## Setting up custom cloud and images in AppVeyor

* Login to AppVeyor portal.
* Navigate to your account name on the top right and select **Build environment** option from drop-down menu
    * If **Build environment** option is not available, please contact [team@appveyor.com](mailto:team@appveyor.com) and ask to enable **Private build clouds** feature.
* Press **Add cloud**, select cloud type **Amazon EC2**.

**Complete the following mandatory settings**:

* **Name**: Name for your private build cloud. Make it meaningful and short to be able to use in YAML configuration or AppVeyor UI.
* **AWS account**
    * **Access Key ID**: Get it **My Security Credentials** > **Access keys (access key ID and secret access key)** menu in **AWS Management Console**.
    * **Secret access key**: Get it **My Security Credentials** > **Access keys (access key ID and secret access key)** menu in **AWS Management Console**.
* **Virtual machine configuration**
    * **Region**: AWS region Master VM and image created in. It can be found in the top right corner of the AWS Console UI.
    * **Machine size**: AMI instance type you selected when created Master VM.
    * **Security group ID**: select or create new security group in **Network & Security** > **Security Groups** view.
    * **Key pair name**: key pair created or selected when VM was created. You can find it in **Network & Security** > **Key Pairs** view.
* **Networking settings**
    * **Subnet ID**: subnet created or selected when VM was created. You can find it in **Network & Security** > **Network Interfaces** view.
* **Images**
    * **IMAGE NAME**: Image name as you want to see it in AppVeyor UI and YAML, for example **Windows Server 2016 on EC2**
    * **IMAGE ID**: Id of image created earlier (AMI ID). You can find it in **Images** > **AMIs** view.
* **Failure strategy**:
    * **Job start timeout, seconds**: 360 is good enough for EC2. However, if VM creation and build start takes longer for you, please adjust accordingly
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
