---
title: How AppVeyor Server is better than other CI/CD solutions
---

AppVeyor Server is a multi-platform, downloadable, lightweight and full-featured version of AppVeyor CI service.

You can use AppVeyor Server as a personal build engine on your desktop for testing build configurations before pushing them to upstream for hosted AppVeyor or you can install it on a cloud VM as a full-featured CI/CD server for your team.

## Get started in minutes

Really… in minutes! No trial-wall, no "let's talk" forms. Just [download](https://www.appveyor.com/on-premise/#download), install and use.
AppVeyor Server is a lightweight app with no dependencies at all and it comes with a **free license for unlimited clouds/projects/pipelines/agents**.
The UI and appveyor.yml are the same as in the hosted version and you can find thousands of examples in [our docs](https://www.appveyor.com/docs/build-configuration/) or [on GitHub](https://github.com/search?q=appveyor.yml&type=Code).


## Save money by paying for build minutes only

Unlike other CI/CD solutions where a self-hosted agent has to be installed on a VM running 24/7, AppVeyor Server provisions a new clean isolated VM for every build and decommissions it when it's done.
Imagine running a build with 100 parallel jobs on 100 VMs and only having to pay for those minutes when the build is running. AppVeyor Server is the only CI/CD that allows such a scenario!


## Build images covered

AppVeyor is the first CI/CD solution to solve the problem of build images - thanks to PowerShell, Chocolatey and Packer!
It's just a [single command](https://www.appveyor.com/docs/server/#azure-vms) to create a master VM image with required software on it and connect to a cloud.


## The best integration with Docker for Windows

AppVeyor works great with Docker for Linux, but it really stands out from other CI solutions when it comes to Windows.
AppVeyor Server provides the best support for mixed builds with Windows and Linux containers. Builds with Windows-only containers, Windows and Linux with LCOW and Linux-only containers - all of these are covered!
[Read more about Docker support with examples](https://www.appveyor.com/docs/server/#docker-1).


## AppVeyor in Kubernetes

For larger teams we offer AppVeyor Server as a highly-available installation in Kubernetes that can be scaled horizontally on demand. We didn't just take an old monolithic stateful codebase and squeeze it into Kubernetes,
but rather, completely re-wrote AppVeyor Server to .NET Core with Docker-based architecture in mind. AppVeyor Kubernetes deployment has a minimum number of moving parts for smoother installation and maintenance.

[Download](https://www.appveyor.com/on-premise/#download), [install](https://www.appveyor.com/docs/server/#installation) and get excited as much as we are!

Enjoy!

Best regards,<br>
AppVeyor team