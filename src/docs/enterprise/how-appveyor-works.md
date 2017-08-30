---
layout: docs
title: How AppVeyor works
---

<!-- markdownlint-disable MD022 MD032 -->
# How AppVeyor works
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

AppVeyor is a distributed application consisting of several roles that can be installed on a single or multiple servers across the network.

![AppVeyor General Diagram](/assets/img/docs/enterprise/appveyor-architecture-general.png)

## Web role

AppVeyor Web role is a web application hosting AppVeyor web dashboard, REST API and WebSockets endpoint for real-time build log. Web role communicates with Worker role by sending messages to Service Bus queue.

Web role has the following dependencies:

* **IIS** 8.0 or higher with ASP.NET 4.5 and WebSockets installed
* **SQL Server** with AppVeyor database
* **Redis** server for application cache and [SignalR backplane](https://www.asp.net/signalr/overview/performance/scaleout-with-redis)
* **Service Bus** for communicating with Worker role (see below).

Installation notes:

* Web role is installed into `C:\Program Files\AppVeyor\Web`.
* AppVeyor installs Web role to `Default Web Site` (site with ID=1). Original web site content is not deleted - just root folder is changed, but make sure **you don't have production website in "Default Web Site"**.
* Website application pool should have should have "Integrated" pipeline with .NET 4.0 enabled.
* By default, AppVeyor installer configures `C:\AppVeyor\Artifacts` local folder for storing build artifacts. If you change that folder make sure there is "Modify" permission for application pool identity set on it.



## Worker role

Worker role is responsible for processing long-running jobs. It's a Windows service running under "LocalSystem" account.

Worker role has the following dependencies:

* **SQL Server** with AppVeyor database
* **Redis** server for application cache
* **Service Bus** for communicating with Web role.

Installation notes:

* Worker role is installed into `C:\Program Files\AppVeyor\Worker`.
* Worker service name: `Appveyor.Worker`.
* If you are changing the location of artifacts folder make sure Worker service identity has "Modify" permissions set for that folder.


## Build Agent

Build agent executes build jobs on local or remote computers.

Build Agent has the following dependencies:

* HTTP(S) access to Web role.
* Git, Mercurial or/and Subversion command-line clients in `PATH` depending on source controls of your projects.

Installation notes:

* Build Agent is installed into `C:\Program Files\AppVeyor\BuildAgent`.
* Build Agent service name: `Appveyor.BuildAgent`
* Service should be running under account which is a member of local `Administrators` group.


## Stateful vs stateless builds

AppVeyor can run builds on build workers of two types:

* **stateful** (or permanent) build workers
* **stateless** (or transient) build workers

### Stateful workers

Stateful workers are "always on" Build Agent machines for which any changes are preserved between builds. For example, any Chocolatey package installed, any NuGet package downloaded or any database created stay there and "visible" for next builds. While stateful builds can drastically reduce overall build time by having everything ready and pre-heated for consequent builds they require your build scenarios to include "setup" and "teardown" code increasing complexity of your builds. This approach is recommended for builds with minimum environment changes.

### Stateless workers

Stateless build workers are virtual machines provisioned from template or reset to the initial "clean" state and dedicated to a single build. When the build is finished machine is "decommissioned", i.e. either deleted or reverted to "clean" state and returned to the pool.

Pros:

* Dedicated pristine environment for every build
* Build workers are on during the build only, thus preserving resources and reducing costs.

Cons:

* Additional time is required for provisioning and configuring build worker machines.
