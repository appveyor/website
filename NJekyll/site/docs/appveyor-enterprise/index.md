---
layout: docs
title: Getting started with AppVeyor Enterprise
---

# Getting started with AppVeyor Enterprise

<!--TOC-->

## Introduction

AppVeyor Enterprise is a downloadable version of AppVeyor CI that can be installed on your own premises behind the firewall. AppVeyor Enterprise is great solution for organizations willing to use AppVeyor for testing their applications, but in a fully controlled environment. While hosted AppVeyor CI service saves you from managing build environment, AppVeyor Enterprise has its own benefits:

* Compliance with strict security requirements of your organization.
* Any number of concurrent jobs for maximum parallelism.
* Any number of build worker machines on your own hardware.
* Any number of worker images under your own Azure subscription.

> **General note about this release. This is beta release and it's not intended for installing on production environment.**
 

## Supported platforms

### Operating Systems

* Windows Server 2012 R2 x64 any edition
* Windows Server 2012 x64 any edition
* Windows 8.1 x64 any edition
* Windows 8 x64 any edition

> Why AppVeyor requires Windows 8/Server 2012 or higher? AppVeyor Web worker requires **WebSockets support** which was added in Windows 8/Server 2012. Windows 7/Server 2008 don't have built-in WebSockets support.

### .NET

* .NET Framework 4.5.2

### SQL Server

* SQL Server 2012 Express (or higher)

### PowerShell

* PowerShell 3.0



## How AppVeyor works

[General diagram - web, worker, build agent, service bus, SQL Server, artifacts storage]

### Web role

AppVeyor Web role is a web application hosting AppVeyor web dashboard, REST API and WebSockets endpoint for real-time build log. Web role communicates with Worker role by sending messages to Service Bus queue.

Web role has the following dependencies:

* **IIS** 8.0 or higher with ASP.NET 4.5 and WebSockets installed
* **SQL Server** with AppVeyor database
* **Redis** server for application cache and [SignalR backplane](http://www.asp.net/signalr/overview/performance/scaleout-with-redis)
* **Service Bus** for communicating with Worker role (see below). 

Installation notes:

* Web role is installed into `C:\Program Files\AppVeyor\Web`.
* AppVeyor installs Web role to `Default Web Site` (site with ID=1). Original web site content is not deleted - just root folder is changed, but make sure **you don't have production website in "Default Web Site"**.
* Website application pool should have should have "Integrated" pipeline with .NET 4.0 enabled.
* By default, AppVeyor installer configures `C:\AppVeyor\Artifacts` local folder for storing build artifacts. If you change that folder make sure there is "Modify" permission for application pool identity set on it.



### Worker role

Worker role is responsible for processing long-running jobs. It's a Windows service running under "LocalSystem" account.

Worker role has the following dependencies:

* **SQL Server** with AppVeyor database
* **Redis** server for application cache
* **Service Bus** for communicating with Web role.

Installation notes:

* Worker role is installed into `C:\Program Files\AppVeyor\Worker`.
* Worker service name: `Appveyor.Worker`.
* If you are changing the location of artifacts folder make sure Worker service identity has "Modify" permissions set for that folder.


### Build Agent

Build agent executes build jobs on local or remote computers.

Build Agent has the following dependencies:

* HTTP(S) access to Web role.
* Git, Mercurial or/and Subversion command-line clients in `PATH` depending on source controls of your projects.  

Installation notes:

* Build Agent is installed into `C:\Program Files\AppVeyor\BuildAgent`.
* Build Agent service name: `Appveyor.BuildAgent`
* Service should be running under account which is a member of local `Administrators` group.


###  Stateful vs stateless builds

[TBD]

### Concurrent jobs

[TBD]



## Supported topologies

### 1-tier

All-in-one

[Diagram]


### 2-tier

Web and worker on one machine Build Agents on others

[Diagram]


### Azure Cloud Service

[Diagram]

What’s that? What are benefits? What is required (subscription)?

Contact us…

### High-availability deployment

[Diagram]

What is out of scope
Guide is in progress. Contact us if you are interested.



## Installing AppVeyor

### Prerequisites

* .NET 4.5.2 - is installed by AppVeyor installer or can be installed manually from [here](http://www.microsoft.com/en-ca/download/details.aspx?id=42642).
* IIS (Web Role) with ASP.NET 4.5 and WebSockets enabled.
* SQL Server with Mixed security mode enabled. If not installed AppVeyor installer will install SQL Server 2014 Express x64 as `SQL2014` instance.
* Redis - is installed by AppVeyor installer of can be installed manually on port `6379`.
* Service Bus for Windows 1.1 - is installed by AppVeyor installer or can be installed manually using Web Platform Installer (Web PI).
* SMTP server details or [Mailgun](http://www.mailgun.com/) account - for sending build email notifications.
* [Microsoft Build Tools 2013](https://www.microsoft.com/en-us/download/details.aspx?id=40760) or Visual Studio 2013 - for building .NET apps.

### Getting installation script

AppVeyor installer is implemented as PowerShell module and used for installing and upgrading AppVeyor components and dependencies. 

To install AppVeyor installer module open PowerShell console and enter the following command:

    iex ((new-object net.webclient).DownloadString('http://www.appveyor.com/downloads/on-premise/install.ps1'))

The module is installed into `C:\Program Files\AppVeyor\Modules\appveyor-installer` directory and `C:\Program Files\AppVeyor\Modules` is added into `$PSModulePath` variable.

Next time you open PowerShell console AppVeyor installer module will be automatically loaded into PS session.


### Installing Web and Worker roles

For installing AppVeyor roles use `Install-AppVeyor` cmdlet. `Install-AppVeyor` parameters:

* `-Roles` - AppVeyor roles to install. Supported values: `Web`, `Worker`, `BuildAgent`, `Server` (install `Web` and `Worker`) and `All` (install `Web`, `Worker` and `BuildAgent`). If this parameters is not specified all three roles will be installed.
* `-Force` - answer "yes" to all questions.
* `-SqlServer` - SQL Server instance name that will be used for AppVeyor and Service Bus databases. For example, `localhost` or `(local)\SQLExpress`.
* `-Version` - AppVeyor version to install. If not specified the most recent version will be installed. To get the list of all available versions use `Get-AppVeyorVersions` cmdlet.
* `-Express` - used for unattended "all-in-one" installation on clean machine. Perfect solution for provisioning your own build server on Azure or AWS VM. Installs all AppVeyor dependencies and AppVeyor components.

**Examples**

For unattended installation of `Web`, `Worker`, `BuildAgent` roles with all dependencies such as IIS, SQL Server, Service Bus, Redis, Git, NuGet, etc. use this command:

    Install-AppVeyor -Force

Installing AppVeyor with existing SQL Server:

    Install-AppVeyor -Force -SQLServer '(local)\SQLEXPRESS'

If Worker role is successfully installed you should see `Appveyor.Worker` service running.

AppVeyor installer installs Web role to "Default Web Site". When the installation of Web role is finished open `http://localhost` in web browser and follow the wizard to create AppVeyor administrator account.


### Installing Build Agent

[TBD]


## Updating AppVeyor

For updating AppVeyor roles use `Update-AppVeyor` cmdlet. `Update-AppVeyor` parameters:

* `-Roles` - AppVeyor roles to update. Supported values: `Web`, `Worker`, `BuildAgent`, `Server` (install `Web` and `Worker`) and `All` (install `Web`, `Worker` and `BuildAgent`). If this parameters is not specified all three roles will be updated.
* `-Version` - version to update roles to. If not specified the most recent version will be used.
* `-Force` - answer "yes" to all questions.

The following command updates all roles to the most recent version:

    Update-AppVeyor

### Troubleshooting

During installation AppVeyor uses randomly-generated values for security keys, account passwords and other sensitive values. All these values as well as other installer data such as AppVeyor roles and versions installed can be found under this registry key:

    HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Install



## Configuring AppVeyor

### Artifact storage

[TBD]

### Email notifications

[TBD]

### Non-UI system settings

Location in the Registry.


## Known issues

* Service Bus for Windows Server 1.1 doesn’t start if .NET 4.6 installed


### Troubleshooting

* F5 in browser if there is a problem with SignalR
* “AppVeyor” event log

## How to configure builds on Azure VMs

Creating master image
install build agent
change URL
specify worker mode
make sure it connects to AppVeyor
create storage account for VMs
create storage account for build cache
Configuring server
Azure subscription ID and certificate
Add Azure build regions
Update plan (enable Force Azure)
Add image (“Windows Server 2012 R2” is default one)

## How to configure builds on Hyper-V VMs

[TBD]


