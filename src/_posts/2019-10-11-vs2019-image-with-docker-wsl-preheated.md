---
title: Visual Studio 2019 image with Docker and LCOW, WSL, Qt and pre-heated VMs
---

The latest `Visual Studio 2019` image update added a lot of new exciting stuff you were missing in your builds:

* Docker EE with Linux Containers on Windows (LCOW) support
* Windows Subsystem for Linux (WSL)
* Qt framework

Not only we updated the image, but we moved it to our fast Hyper-V environment with pre-heated VMs and now it will take seconds for new builds to start!
All `Visual Studio 2019` build VMs have nested virtualization enabled.

## Docker with Linux containers support

Docker on `Visual Studio 2019` image allows you to run Windows and Linux containers simultaneously without ever switching between Windows and Linux "modes".
Running a Linux container could be as simple as `docker run --rm busybox echo hello_world`.

## Windows Subsystem for Linux (WSL)

There is WSL installed on `Visual Studio 2019` image with the following Linux distributions:

* Ubuntu-18.04 (Default)
* Ubuntu-16.04
* openSUSE-42

You can run Linux commands inside WSL with a simple `wsl <command>` and you can switch between distributions with [`wslconfig`](https://docs.microsoft.com/en-us/windows/wsl/wsl-config).

## Qt framework

Now you can build and test your Qt applications on `Visual Studio 2019` image. The following Qt releases [installed](/docs/windows-images-software/#qt):

* Qt 5.13.1
* Qt 5.12.5
* Qt 5.9.8

And we have automated the installation of Qt with Packer, so you can use it with [AppVeyor Bring-Your-Own-Cloud solution](/blog/2019/10/01/self-hosted-jobs-on-your-computer-or-in-cloud-vms/)!

Give this new image a try and let us know if you have any questions or suggestions!

## Phasing out "Windows Server 2019" image

If you are using `Windows Server 2019` please switch to `Visual Studio 2019` image as we are not going to evolve/update `Windows Server 2019` image going forward.

## Linux images on pre-heated VMs

With the increased demand for Linux builds we have moved `Ubuntu`, `Ubuntu1804` and `Ubuntu1604` images to Hyper-V environment with pre-heated VMs, so Linux builds start in seconds now!

Best regards,<br>
AppVeyor team