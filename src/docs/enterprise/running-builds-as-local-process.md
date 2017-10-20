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

## Prepare agent machine

### Minimum requirements:

* Windows Server 2012 R2 (Windows 8.1) or higher
* .NET Framework 4.5.2
* Disk space should be enough to clone repository and store build artefacts
* Internet connectivity
    * Currently we require outbound Internet connectivity at TCP (not HTTP) level (behind router or NAT). We are working on proxy support, please watch [this](https://github.com/appveyor/ci/issues/1303) issue

## Setup agent machine software

### Download and install AppVeyor Host agent on agent machine

* [Download location](https://www.appveyor.com/downloads/host-agent/latest/AppveyorHostAgent.msi)
* Installation settings
    * **Host authorization token**: token generated or manually entered during [Setting up custom cloud and images in AppVeyor](/docs/enterprise/running-builds-as-local-process/#setting-up-custom-cloud-and-images-in-appveyor) step

### Optionally configure Host agent to run in "Interactive" mode

This setting can be useful if you need to run UI tests.

* [Enable auto-logon](https://github.com/appveyor/ci/blob/master/scripts/enterprise/enable_auto_logon.ps1) [[raw](https://raw.githubusercontent.com/appveyor/ci/master/scripts/enterprise/enable_auto_logon.ps1)]
* [Add Appveyor Host Agent to auto-run](https://github.com/appveyor/ci/blob/master/scripts/enterprise/add_appveyor_host_agent_to_auto_run.ps1) [[raw](https://raw.githubusercontent.com/appveyor/ci/master/scripts/enterprise/add_appveyor_host_agent_to_auto_run.ps1)]

Restart computer to ensure that changes auto-logon/run works as expected.

### Download and install AppVeyor build agent on agent machine

* [Download location](https://www.appveyor.com/downloads/build-agent/latest/AppveyorBuildAgent.msi)
    * Accept all default settings during installation

### Download and install additional software required by build process

Follow [these steps](/docs/enterprise/setup-master-vm/) to configure VM and install software required for your build process. It is tested PowerShell scripts which can be simply copy-pasted to PowerShell window (started in privileged mode). Specifically:

* [Basic configuration](/docs/enterprise/setup-master-vm/#basic-configuration) and [Essential 3rd-party software](/docs/enterprise/setup-master-vm/#essential-3rd-party-software) - we strongly recommend to install everything from those sections.
* [Build framework](/docs/enterprise/setup-master-vm/#build-framework) - you can skip one of MSBuild and Visual Studio versions or both if you don't need them.
* [Test framework](/docs/enterprise/setup-master-vm/#test-framework) - you can skip that step if you are running your own custom test script/framework.
* [AppVeyor Build Agent](/docs/enterprise/setup-master-vm/#appveyor-build-agent) and [Configuring agent to run in "Interactive" mode](/docs/enterprise/setup-master-vm/#configuring-agent-to-run-in-interactive-mode) - these steps are mandatory.

Install any additional software required for your builds.

## Setting up custom cloud and images in AppVeyor

* Login to AppVeyor portal
* Navigate to your account name on the top right and select **Build environment** option from drop-down menu
    * If **Build environment** option is not available, please contact [team@appveyor.com](mailto:team@appveyor.com) and ask to enable **Private build clouds** feature
* Press **Add cloud**, select cloud type **Process**

**Complete the following settings**:

* **Name**: Name for your local process environment. Make it meaningful and short to be able to use in YAML configuration.
* **Host authorization token**: generate host authorization token or enter it manually.
* **Workers capacity**: In local process context this means number of AppVeyor build agents could be spin up in parallel.
    * Note that every build agent consumes about at least 15 Mb of memory, with additional overhead which depends on build tool is being used.
    * CPU consumption is also can vary depending on specific build tool/scenario
    * Number of parallel build cannot be greater than what is allowed in AppVeyor plan regardless of **Workers capacity** setting.
* **Project builds directory**: Set folder to be used to clone and run builds on agent machine
* **Build Agent directory**: leave it blanc is Agent installation happened with default settings, otherwise set accordingly.
* Open **Failure strategy** and set the following:
    * **Job start timeout, seconds**: 180 should good enough for modern server. However, please feel free to increase according to your observation with specific machine.
    * **Provisioning attempts**: 2 is good for start. Later you may need to change it according to your observations

## Make build worker image available for configuration

Though *image* term does not fit into local process scenario, it is required to set some *image* to be able to wire specific environment to specific project.

* Navigate to **Build environment** > **Build worker images**
* Press **Add image**
* Enter any name you like as **IMAGE NAME**

## How to route build to your own cloud

At **project** level:

* **UI**:
    * **Settings** > **Environment** > **Build cloud**: Select your local process environment name from drop-down
    * **Settings** > **Environment** > **Build worker image**: Select your image name set in previous step from drop-down
* **YAML**:

```yaml
build_cloud: <process_environment_name>
image: <process_environment_image>
```
