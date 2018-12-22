---
layout: docs
title: Build Environment
---

<!-- markdownlint-disable MD022 MD032 -->
# Build environment
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

Every build runs on a fresh virtual machine which is not shared with other builds and the state of which is not preserved between consequent builds. After the build is finished its virtual machine is decommissioned.

## Build clouds

AppVeyor runs builds on two build clouds:

* Hyper-V
* Google Compute Engine (GCE)

### Hyper-V

Hyper-V cloud is a primary build cloud hosted in LiquidWeb's Lansing, Michigan data center.
It is a pool of pre-heated virtual machines, so, generally, builds start faster on Hyper-V cloud.

### Google Compute Engine

Google Compute Engine (GCE) cloud is a secondary build cloud running in Google Cloud "Central US" zone.
Builds are routed to GCE cloud when they use a custom build worker image not available on Hyper-V cloud.
GCE cloud is also used as a failover solution for Hyper-V cloud.

It usually takes a build around 3-4 minutes to start on GCE environment as this is the time required to provision and boot up build virtual machine.

### Private build cloud

There might be build scenarios that cannot be covered by AppVeyor build workers. For example, some proprietary software should be pre-installed to support your builds
or you need more powerful build VMs or private network access.

AppVeyor allows running builds on your own cloud:

* Hyper-V server (on-premise or hosted)
* Docker server (on-premise or hosted)
* Azure virtual machines
* Amazon Web Services (AWS)
* Google Compute Engine (GCE)

In this scenario AppVeyor service provides UI, orchestration, artifacts storage and NuGet feeds while AppVeyor build agents run on your virtual machines.
Private build clouds can be enabled upon request. Please [let us know](mailto:team@appveyor.com) if you are interested.

## Build VM configurations

<table>
  <tr>
    <th>Build cloud / configuration</th>
    <th>CPU</th>
    <th>Memory</th>
    <th>Nested virtualization</th>
  </tr>
  <tr>
    <td>Hyper-V</td>
    <td>2 cores</td>
    <td>4 GB</td>
    <td>-</td>
  </tr>
  <tr>
    <td>Hyper-V "4-cores"</td>
    <td>4 cores</td>
    <td>7 GB</td>
    <td>Enabled</td>
  </tr>
  <tr>
    <td>GCE</td>
    <td>2 cores</td>
    <td>7.5 GB</td>
    <td>-</td>
  </tr>
</table>

## IP addresses

IP addresses assigned to build VMs in Google data center (`us-central1` region):

    104.197.110.30
    104.197.145.181

IP addresses assigned to build VMs in LiquidWeb data center (Lansing, MI):

    67.225.138.82
    67.225.139.254
    67.225.165.66
    67.225.165.168
    67.225.165.171
    67.225.165.175
    67.225.165.183
    67.225.165.185
    67.225.165.193
    67.225.165.198
    67.225.165.200
    67.225.251.78
    67.225.251.79
    67.225.251.95
    67.225.251.96
    67.225.251.97
    67.225.251.98

IP addresses assigned to build VMs in AWS data center (`US West (Oregon)` region):

    34.208.156.238
    34.209.164.53
    34.216.199.18
    52.43.29.82
    52.89.56.249
    54.200.227.141

IP address of AppVeyor Cloud in Azure data center (`West US` region) - when deploying with "Environments":

    138.91.141.243

## Build worker images

*Build worker image* is a template used to provision a virtual machine for your build. Physical implementation of the template depends on the build cloud platform
and can be a master VHD for Hyper-V and Azure, snapshot or image for GCE or AWS.

AppVeyor provides these "standard" build worker images:

* Windows images:
    * `Visual Studio 2013`
    * `Visual Studio 2015`
    * `Visual Studio 2017`


* Linux images:
    * `Ubuntu` or `Ubuntu1604` - Ubuntu 16.04
    * `Ubuntu1804` - Ubuntu 18.04

Below you can find the list of [pre-installed software](#pre-installed-software) for all images.

### Visual Studio Preview images

AppVeyor also provides a build image which contains, in place of the Visual Studio 2017 version on the current image, the VS2017 preview relative to that version.

* `Visual Studio 2017 Preview`

The aim is to stay in sync with the [release rhythm](https://docs.microsoft.com/en-us/visualstudio/productinfo/vs2017-release-rhythm#previews) of Visual Studio 2017.

## Choosing image for your builds

If the build configuration does not specify build worker image then `Visual Studio 2015` image is used.

You can select a different image on AppVeyor UI ("Environment" tab of project settings) or in `appveyor.yml`:

    image: Visual Studio 2017

or to build on Linux:

    image: ubuntu

> Please note that `appveyor.yml` has [precedence over UI settings](/docs/build-configuration/#appveyoryml-and-ui-coexistence),
> so when `appveyor.yml` exists the image should be specified in YAML, not on UI.

## Using multiple images for the same build

`image` is first class dimension for [build matrix](/docs/build-configuration/#build-matrix), therefore the following YAML configuration will work (and will create 6 build jobs):

```yaml
image:
- Visual Studio 2015
- Visual Studio 2017
- Ubuntu
environment:
  matrix:
    - MY_VAR: value1
    - MY_VAR: value2
```

Also for some combinations it makes sense to use `APPVEYOR_BUILD_WORKER_IMAGE` "tweak" environment variable, so this configuration will also work (and will create 3 build jobs):

```yaml
environment:
  matrix:
    - APPVEYOR_BUILD_WORKER_IMAGE: Visual Studio 2015
      MY_VAR: value1
    - APPVEYOR_BUILD_WORKER_IMAGE: Visual Studio 2017
      MY_VAR: value2
    - APPVEYOR_BUILD_WORKER_IMAGE: Ubuntu
      MY_VAR: value3
```

## Image updates

AppVeyor team regularly (once in 2-3 weeks) updates build worker images by installing new or updating existing software.

The history of build worker image updates can be found [here](/updates/).

Before rolling out an image update we perform its extensive testing. However, not all usage scenarios can be covered by our automated tests and
sometimes even a smallest change in the image can break someone's build. If that happened - no worries - you're covered!
We provide an access to "last good" versions of build worker images from previous update:

* Windows "previous" images:
    * `Previous Visual Studio 2013`
    * `Previous Visual Studio 2015`
    * `Previous Visual Studio 2017`


* Linux "previous" images:
    * `Previous Ubuntu` which is the same `Previous Ubuntu1604`
    * `Previous Ubuntu1604`
    * `Previous Ubuntu1804`

You can use those images to unblock your builds while we are working together with you to understand the root cause and do a fix by the next image update.

## Pre-installed software

The following pages contain the up-to-date list of software pre-installed on build worker VMs:

* [Windows images](/docs/windows-images-software/)
* [Linux images](/docs/linux-images-software/)
