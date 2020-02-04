---
layout: docs
title: Bring Your Own Cloud or Computer (BYOC)
---

<!-- markdownlint-disable MD022 MD032 -->
# Bring Your Own Cloud or Computer (BYOC)
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## What is BYOC

Bring Your Own Cloud (BYOC) allows running builds on your own infrastructure. Builds could be run inside VMs (Azure, AWS, GCE, Hyper-V), in Docker containers (Windows, Linux and macOS) or directly on the host (Windows, Linux and macOS). BYOC is available for hosted AppVeyor accounts and self-hosted AppVeyor Server installations.

Some of the reasons you may want to run builds on your infrastructure:

* Using under-utilized existing hardware such as an old build server under your desk;
* Using hardware with better performance like your team's workstations or VMs with better/different characteristics (GPU-enabled, memory-, disk-, network-optimized);
* Running builds on your team's Macs or a Mac hosted on Macstadium. While we are hard-working on bringing macOS support to AppVeyor service this might be a viable option for now;
* Custom/proprietary software that cannot be installed during the build;
* Security/compliance requirements like the code or build artifacts not leaving particular cloud/region/zone/network;

## How is it better than "self-hosted agents" in other CI/CD solutions?

AppVeyor really shines at running builds on dynamically provisioned VMs. While AppVeyor also supports running builds on a host directly (aka "self-hosted agent") creating VMs on demand has a number of advantages:

* **Significant savings on a monthly cloud bill**. Build VM is created for the duration of a build job and immediately deleted when this build is over. Major cloud providers have per-minute pricing and as a result you pay only for the "clean" build time. With a "self-hosted agent" approach a VM with an agent is running 24x7 waiting for your builds, consuming electricity or adding to your monthly bill. You are not going to run expensive GPU-enabled instance for 24x7x365, aren't you?
* **Pristine environment on every build**. Build VMs are provisioned from an image or snapshot and never reused for the consequent builds. You get clean and predictable environment every time you run a new build. With "self-hosted agent" approach concurrent builds are running directly on the host operating system, so they interfere, produce leftovers (files, databases, configs, registry settings) or could completely kill the host machine. It makes you think about additional guard/synchronizing/cleanup code in your builds thus wasting your time.
* **Multiple parallelism**. A single build can be run on hundreds of VMs simultaneously for a shorter period of time. Say, you have a suite with 1,000 tests and it takes 1 hour on a single core to run them all. You can run the suite on 10 single-core VMs in parallel and reduce the overall test time to 6 minutes by paying the same amount to the cloud provider!

## What about build VM images?

One of the benefits of hosted AppVeyor service is that you get build VM with a tons of pre-installed software, curated and regularly updated by AppVeyor team. The greatest fear of running builds on your own VMs is a necessity of maintaining build VM images (the template is used to create a VM).

You've been heard! BYOC is not just a feature, but it is a framework and significant part of this framework is the code helping you to create customized build VM images for all platforms and clouds we support: Azure, AWS, GCP, Hyper-V and Docker - thanks to [Packer by HashiCorp](https://www.packer.io/). All scripts and Packer templates used by BYOC framework are [open-source](https://github.com/appveyor/build-images).

## Getting started

To configure your own build environment select **Self-hosted jobs** in the top menu and click **Add cloud**. We created a wizard that will guide you through the process and give you commands based on your selection that should be run on your computer:

<p class="text-center">
  <img src="/assets/img/docs/byoc/add-cloud-wizard.png" alt="Add new build cloud wizard" width="417" height="237">
</p>

Commands are PowerShell cmdlets that are part of [AppVeyorBYOC](https://www.powershellgallery.com/packages/AppVeyorBYOC) module. PowerShell is the universal shell/language that can be installed on [Windows](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell-core-on-windows?view=powershell-6), [Linux](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell-core-on-linux?view=powershell-6) and [macOS](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell-core-on-macos?view=powershell-6).

Below you can find additional notes as well as alternative manual instructions for each of the clouds/platforms supported by BYOC:

<table>
<tr>
    <th>Cloud/Platform</th>
    <th>Builds run in</th>
</tr>
<tr>
    <td><a href="/docs/byoc/windows/"><b>Windows</b></a></td>
    <td>Windows host directly</td>
</tr>
<tr>
    <td><a href="/docs/byoc/linux/"><b>Linux</b></a></td>
    <td>Linux host directly</td>
</tr>
<tr>
    <td><a href="/docs/byoc/mac/"><b>macOS</b></a></td>
    <td>macOS host directly</td>
</tr>
<tr>
    <td><a href="/docs/byoc/docker/"><b>Docker</b></a></td>
    <td>Windows or Linux containers on Windows, Linux or macOS</td>
</tr>
<tr>
    <td><a href="/docs/byoc/azure/"><b>Azure</b></a></td>
    <td>Windows or Linux VMs provisioned in Azure cloud</td>
</tr>
<tr>
    <td><a href="/docs/byoc/aws/"><b>AWS</b></a></td>
    <td>Windows or Linux VMs provisioned in AWS cloud</td>
</tr>
<tr>
    <td><a href="/docs/byoc/gce/"><b>GCE</b></a></td>
    <td>Windows or Linux VMs provisioned in GCE cloud</td>
</tr>
<tr>
    <td><a href="/docs/byoc/hyper-v/"><b>Hyper-V</b></a></td>
    <td>Windows or Linux VMs provisioned on Hyper-V host</td>
</tr>
</table>

## Routing builds to your cloud

To run builds in your own cloud/computer you should update project settings.

If you are configuring AppVeyor project via **UI settings** (no `appveyor.yml` in the repository) open project settings and then click **Environment** tab:

* `Build cloud` - select your private build cloud name;
* `Build worker image` - select your build worker image;
* Save project settings.

If you are configuring AppVeyor project via **appveyor.yml** then UI settings for build cloud and image are ignored.
Specify build cloud and image in `appveyor.yml` instead:

```yaml
build_cloud: <private_build_cloud_name>
image: <private_build_cloud_image>
```

Alternatively, you can configure build cloud and image via **environment variables** (defined either on UI or in `appveyor.yml`).

* `APPVEYOR_BUILD_WORKER_CLOUD` - the name of your build cloud
* `APPVEYOR_BUILD_WORKER_IMAGE` - the name of your build worker image

> NOTE: Build clouds and images configured via environment variables override both UI and `appveyor.yml` settings. This could be useful for quick cloud testing or multi-job builds.

## Build clusters

[TBD] - grouping multiple clouds, failover, etc.
