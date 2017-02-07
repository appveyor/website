---
layout: docs
title: Setup Master VM
---

<!-- markdownlint-disable MD022 MD032 -->
# Setup Master VM
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Setup Master VM

Login to Master VM via RDP (if RDP session was closed since previous step).
Please note that profile of user you are loggen in with, will be used during each build run. Therefore if you do not like this user (for example you want it's name look more suitable for service, not a person), please create new user and re-login before contunue with next steps.

### Basic configuration

Steps below are result of our experience of making Windows Server VMs work in AppVeyor build environment.
Every step is represented by separate PowerShell script to make this task simple, but still leave you easy way to select what script to run.

* [Disable Server Manager auto-start](https://github.com/appveyor/ci/blob/master/scripts/enterprise/disable_servermanager.ps1)
* [Set PowerShell execution policy to unrestricted](https://github.com/appveyor/ci/blob/master/scripts/enterprise/enable_powershell_unrestricted.ps1)
* [Disable UAC](https://github.com/appveyor/ci/blob/master/scripts/enterprise/disable_uac.ps1)
* [Disable WER](https://github.com/appveyor/ci/blob/master/scripts/enterprise/disable_wer.ps1)
* [Disable IE ESC](https://github.com/appveyor/ci/blob/master/scripts/enterprise/disable_ie_esc.ps1)
* [Allow connecting to any host via WinRM](https://github.com/appveyor/ci/blob/master/scripts/enterprise/update_winrm_allow_hosts.ps1)
 <!--
 Disable unnecessary Windows services and Scheduler tasks
 Disable Windows automatic maintenance
 Disable Windows Updates
 -->

### Essential 3rd-party software

* [Add-Path helper cmdlets](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_path_utils.ps1)
* [7-Zip](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_7zip.ps1)
* [Chocolatey](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_chocolatey.ps1)
* [Web Platform Installer](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_webpi.ps1)
* [NuGet](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_nuget.ps1)
* [Git](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_git.ps1)
* [Git LFS](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_git_lfs.ps1)

### Build framework

If you are using .NET stack you probably need some version of MSBuild and/or Visual Studio. Please use scripts below to install what you need. Please follow order in which scripts are listed (Visual Studio after MSBuild of the same generation an newer products after older ones) to be on the safe side.

* [MSBuild 2013](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_msbuild_tools_2013.ps1)
* [Visual Studio 2013](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_vs2013.ps1)
* [MSBuild 2015](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_msbuild_tools_2015.ps1)
* [Visual Studio 2015](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_vs2015.ps1)

Or install other build framework of your choice.

### Test framework

Run one or more of below scripts to install and/or enable test framework of your choice

* [VSTest console](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_vstest_console_logger.ps1)
* [NUnit 2.x](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_nunit.ps1)
* [NUnit 3.x](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_nunit3.ps1)
* [xUnit 1.9.2](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_xunit_192.ps1)
* [xUnit 2.x](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_xunit_20.ps1)
* [MSpec](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_mspec.ps1)

### AppVeyor Build Agent

* [Download and install AppVeyor Build Agent](https://github.com/appveyor/ci/blob/master/scripts/enterprise/install_appveyor_build_agent.ps1)

  At this moment you have to decide if AppVeyor build agent will run in **interactive** (as a startup application) or **headless** (as a windows service) mode. Though **headless** mode seems more "server-style" and stable from the first look, we recommend **interactive** mode. One of most important advantages of **interactive** mode is ability to run UI tests seamlessly. Potential advantages of **headless** mode like ability to run build without user logon or automatic service recovery is not that important during short life cycle of build VM.

* **Hyper-V only**: [Set Build Agent mode to **Hyper-V**](https://github.com/appveyor/ci/blob/master/scripts/enterprise/set_hyperv_build_agent_mode.ps1)

#### Tuning for Interactive mode

* [Enable auto-logon](https://github.com/appveyor/ci/blob/master/scripts/enterprise/enable_auto_logon.ps1)
* [Add Appveyor Build Agent to auto-run](https://github.com/appveyor/ci/blob/master/scripts/enterprise/add_appveyor_build_agent_to_auto_run.ps1)
* Restart VM to ensure that changes auto-logon/run works as expected. Next time you log on, you should not see usual profile loading screen and AppVeyor build Agent should be already started (you will see it on the task bar).

#### Tuning for Headless mode

[TBD]

#### Troubleshooting Build Agent

   If you hit any issues with AppVeyor Build Agent start, first place to look is AppVeyor event log, which is located at **Applications and Services Logs** > **AppVeyor**.