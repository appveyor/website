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

## Introduction

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