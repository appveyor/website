---
layout: docs
title: Custom build images
---

<!-- markdownlint-disable MD022 MD032 -->
# Custom build images
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

> Custom images is a beta feature. Please request your trial on [this page](https://ci.appveyor.com/pricing).

You can install your own software and build dependencies on top of AppVeyor-provided base images (Windows, Linux) and then use resulting image for further builds.
By baking dependencies into the image you can drastically reduce the duration of your builds.

Custom images enable new use cases that were not possible before in a hosted CI/CD:

* speed up builds drastically by preserving VM state between builds;
* update image software at your own pace - pin specific versions or be on a cutting edge;
* securely store certificates and other secrets within the image;
* migrate on-premise CI workflows with proprietary, licensed, manually-installed, legacy software to a hosted CI.

## Building shared image in a dedicated project

You can decide to build a few images with a maximum of pre-installed products/tools/libraries that will be shared across multiple projects or teams.

For the shared image it is recommended having a separate repository with all installation scripts and `appveyor.yml`.

Software can be installed during the build in a multiple ways:

* [Chocolatey](https://chocolatey.org/)
* PowerShell, batch, Bash scripts
* Remote Desktop (RDP) or SSH

Below is the sample `appveyor.yml` to build custom image with `win-sqlsrv-2019` name:

```yaml
# base image
image: Visual Studio 2019

environment:
  APPVEYOR_BAKE_IMAGE: win-sqlsrv-2019    # custom image name

install:
- choco install sql-server-express
```

In the example above we use `Visual Studio 2019` as a base image, install SQL Server 2019 Express using Chocolatey and then bake a new image named `win-sqlsrv-2019`. Defining `APPVEYOR_BAKE_IMAGE` environment variable triggers image bake process. `APPVEYOR_BAKE_IMAGE` could be defined statically to bake a new image every time or dynamically in a build script.

To use that new image with SQL Server 2019 pre-installed in other projects specify its name in `appveyor.yml`:

```yaml
image: win-sqlsrv-2019
```

As you see the process of building a new image is not different from regular builds. You can use all [appveyor.yml features](/docs/build-configuration/), [run tests](/docs/running-tests/) to make sure the software is installed properly, [write messages using API](/docs/build-worker-api/) and [push artifacts](/docs/packaging-artifacts/) with installation logs.

### Using Remote Desktop (RDP)

If the product you need does not support unattended installation and requires manual installation via UI you can login into running VM via RDP, do the work and the continue baking process.

To block image build process and enable RDP access add the following into `appveyor.yml`:

```yaml
# base image
image: Visual Studio 2019

environment:
  APPVEYOR_BAKE_IMAGE: win-sqlsrv-2019    # custom image name
  APPVEYOR_RDP_PASSWORD: <your password>

# enable RDP
install:
- ps: $blockRdp = $true; iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-rdp.ps1'))
```

Put your strong RDP password to `APPVEYOR_RDP_PASSWORD` environment variable.

### Using SSH

For build custom Linux-based images you might need to use SSH to get the job done:

```yaml
image: ubuntu

environment:
  APPVEYOR_BAKE_IMAGE: ubuntu-custom
  APPVEYOR_SSH_KEY: <your ssh public key>
  APPVEYOR_SSH_BLOCK: true

install:
- sh: curl -sflL 'https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-ssh.sh' | bash -e -
```

The build will be blocked until a special "lock" file on VM user home directory is deleted.

## Project-specific build snapshots

Project-specific images (build spanshots) could serve as the next-gen build cache. However, instead of downloading zip with cache contents and unpacking it on every build all dependencies are baked into the image itself.

The difference from building a shared image is that image is being baked during project build every time you need to preserve the state of VM between builds.

Image bake logic is being put under `on_image_bake` section in `appveyor.yml` and is being called if `APPVEYOR_BAKE_IMAGE` environment variable is set. The following `appveyor.yml` with pseudo PowerShell code installs dependencies if they don't exist and bakes the image on `master` branch only:

```yaml
image: Visual Studio 2019

install:
- ps: |
    if (-not (Test-Path "some-directory")) {
      # download dependencies and install them into "some-directory"
    }

build_script:
- ps: Write-Host "Your build logic comes here"

after_build:
- ps: |
    if ($env:APPVEYOR_REPO_BRANCH -eq 'master') {
      $env:APPVEYOR_BAKE_IMAGE = 'my-vs2019-image'
    }

on_image_bake:
- ps: # put some logic to prepare the dependencies before baking the image, e.g. copy folders, cleanup Registry, deleting databases, etc.
```

## Image baking process

Before making a snapshot of build VM AppVeyor deletes sources clone directory (`C:\projects\` on Windows and `~/projects`), so make sure the files you need to bake into the image reside outside of those directories.