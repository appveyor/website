---
layout: docs
title: Getting started with AppVeyor Enterprise
---

# Getting started with AppVeyor Enterprise

<!--TOC-->

<p style="color:red;font-weight:bold;">Note about this beta release - it's not intended for installing on production environment.</p>

## Introduction

AppVeyor Enterprise is a downloadable version of AppVeyor CI that can be installed on your own premises behind the firewall. AppVeyor Enterprise is great solution for organizations willing to use AppVeyor for testing their applications in a fully controlled environment. While hosted AppVeyor CI service saves you from managing build environment, AppVeyor Enterprise has its own benefits:

* Compliance with organization security requirements
* Any number of concurrent jobs for maximum parallelism
* Any number of build worker machines on your own hardware
* Any number of worker images under your own Azure subscription.
 

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

AppVeyor is a distributed application consisting of several roles that can be installed on a single or multiple servers across the network.

![AppVeyor General Diagram](/site/images/docs/appveyor-enterprise/appveyor-architecture-general.png)

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

AppVeyor can run builds on build workers of two types:

* **stateful** (or permanent) build workers
* **stateless** (or transient) build workers

**Stateful workers**

Stateful workers are "always on" Build Agent machines for which any changes are preserved between builds. For example, any Chocolatey package installed, any NuGet package downloaded or any database created stay there and "visible" for next builds. While stateful builds can drastically reduce overall build time by having everything ready and pre-heated for consequent builds they require your build scenarios to include "setup" and "teardown" code increasing complexity of your builds. This approach is recommended for builds with minimum environment changes.

**Stateless workers**

Stateless build workers are virtual machines provisioned from template or reset to the initial "clean" state and dedicated to a single build. When the build is finished machine is "decommissioned", i.e. either deleted or reverted to "clean" state and returned to the pool.

Pros:

* Dedicated pristine environment for every build
* Build workers are on during the build only, thus preserving resources and reducing costs.

Cons:

* Additional time is required for provisioning and configuring build worker machines. 


## Supported topologies

### 1-tier

All AppVeyor roles with Redis, SQL Server and Service Bus are installed on a single machine. This is quick and easy way for small teams and individual developers to have their own build server on Azure or AWS virtual machine. 

![AppVeyor All-in-one installation](/site/images/docs/appveyor-enterprise/appveyor-all-in-one.png)


### 2-tier

Web and Worker roles along with dependencies are installed on one server and Build Agent services are installed on multiple servers.

![AppVeyor 2-tier installation](/site/images/docs/appveyor-enterprise/appveyor-2-tier.png)


### Azure Cloud Service

This is fault-tolerant and highly-available AppVeyor solution deployed as Azure Cloud Service inside your own Microsoft Azure subscription. It allows running builds on any cloud or on-premise virtual machines.

Some of the advantages of this "private" cloud deployment:

* Unlimited scale - you control the number of Web and Worker instances and Redis, Storage, Azure SQL plans.
* Practically zero maintenance with highest SLA backed up by Azure.

![AppVeyor in Azure Cloud Service](/site/images/docs/appveyor-enterprise/appveyor-azure-cloud-service.png)

[Contact us](http://www.appveyor.com/support) if you are interested in trying out this solution.


## Installing AppVeyor

### Prerequisites

* .NET 4.5.2 - is installed by AppVeyor installer or can be installed manually from [here](http://www.microsoft.com/en-ca/download/details.aspx?id=42642).
* IIS (Web Role) with ASP.NET 4.5 and WebSockets enabled.
* SQL Server with Mixed security mode enabled. If not installed AppVeyor installer will install SQL Server 2014 Express x64 as `SQL2014` instance.
* Redis - is installed by AppVeyor installer or can be installed manually on port `6379`.
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

Installing AppVeyor Web and Worker roles only (if you are going to run builds on a different machines):

    Install-AppVeyor -Roles Server -Force

Installing AppVeyor with existing SQL Server:

    Install-AppVeyor -Force -SQLServer '(local)\SQLEXPRESS'

If Worker role is successfully installed you should see `Appveyor.Worker` service running.

AppVeyor installer installs Web role to "Default Web Site". When the installation of Web role is finished open `http://localhost` in web browser and follow the wizard to create AppVeyor administrator account.


### Installing Build Agent

To install the latest version of Build Agent service on remote machine use the following command:

    Install-AppVeyor -Roles BuildAgent -Force

Next, you should configure Build Agent to connect AppVeyor Web role running on another server. Run `regedit` and find `HKEY_LOCAL_MACHINE\SOFTWARE\AppVeyor\Build Agent` key. Update the following values under that key:

* `ApplicationUrl` - Web role URL, for example `http://<another-computer>` or `http://my-ci-server.cloudapp.net`.
* `AuthorizationToken` - authorization token of Web role. Can be found on Web role machine under `HKEY_LOCAL_MACHINE\SOFTWARE\AppVeyor\Server` key.

You can change the values of other parameters like `DeleteBuildFolderOnFinish`, `MaxConcurrentJobs` and `ProjectsDirectory`.

The only reserved parameters that cannot be changed are `Mode` and `WorkersQueueName`.



## Updating AppVeyor

For updating AppVeyor roles use `Update-AppVeyor` cmdlet. `Update-AppVeyor` parameters:

* `-Roles` - AppVeyor roles to update. Supported values: `Web`, `Worker`, `BuildAgent`, `Server` (install `Web` and `Worker`) and `All` (install `Web`, `Worker` and `BuildAgent`). If this parameters is not specified all three roles will be updated.
* `-Version` - version to update roles to. If not specified the most recent version will be used.
* `-Force` - answer "yes" to all questions.

The following command updates all roles to the most recent version:

    Update-AppVeyor


## Configuring AppVeyor

To review and change AppVeyor configuration settings login as AppVeyor administrator and select **System Settings** in the account menu. 

### Artifact storage

For artifacts storage you can choose between **Local file system** and **Azure Blob storage**.

By default, AppVeyor configures Local File System with `C:\AppVeyor\Artifacts` root directory.  

### Email notifications

The following services are supported for sending out build notifications:

* SMTP server
* Mailgun service
* SendGrid (in development)

**SMTP server**

Installing and configuring your own SMTP service is out-of-scope of this guide. However, for testing purposes you can try using SMTP server of your current email provider. For example, if you have Gmail account use these settings:

* Server: smtp.gmail.com:587
* Username: <your-gmail-email@gmail.com>
* Password: <your-password>
* Requires SSL: yes

**Mailgun**

[Mailgun](http://www.mailgun.com/) is a managed email sending/receiving provider. You can send up to 10,000 messages per month for [free](http://www.mailgun.com/pricing).

> AppVeyor is not affiliated with Mailgun in any way. We recommend it because we use it ourselves in production and love it.

**SendGrid**

[Let us know](http://www.appveyor.com/support) if you are interested in trying it out.

### Non-UI system settings

Some AppVeyor settings such as database or Service Bus connection strings cannot be changed on UI. These and other settings are stored in the registry under these keys:

    HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Server
    HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\BuildAgent



## Known issues

* Service Bus for Windows Server 1.1 doesn't work if .NET 4.6 installed which basically means AppVeyor Web and Worker roles cannot be installed on a computer with Visual Studio 2015 installed. However, you can have another server with Visual Studio 2015 installed and AppVeyor Build Agent installed.


## Troubleshooting

During installation AppVeyor uses randomly-generated values for security keys, account passwords and other sensitive values. All these values as well as other installer data such as AppVeyor roles and versions installed can be found under this registry key:

    HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Install

When something goes wrong:

* If build real-time log stops working there might be a transient issue with SignalR. Do F5 in browser to restart SignalR connection.
* Restart IIS and/or `Appveyor.Worker` and/or `Appveyor.BuildAgent` services.
* Nothing helps - [report the issue](http://www.appveyor.com/support) to AppVeyor team. While reporting the issue look into these places for possible errors/warning:
	* Web browser's "Developer tools" - for any JavaScript-related errors.
	* `AppVeyor` event log in Event Viewer under `Applications and Services Logs\AppVeyor`. Web, Worker and Build Agent roles write logs there.

## Running builds on Azure VMs

> This section is still under construction - please [let us know](http://www.appveyor.com/support) if you are interested to run "stateless" builds on Azure VMs.

Setup process outline:

* Create "master" Azure VM
* Install build agent on master VM
	* Change URL to point Web role
	* Change agent mode to `Azure`
	* Start BuildAgent service to make sure it connects to AppVeyor Web role
* Create Azure storage account for VMs
* Create Azure storage account for build cache
* Configuring server
	* Azure subscription ID and certificate
	* Add Azure build region
	* Update plan (enable `ForceAzure`)
	* Add "Windows Server 2012 R2" image as default


## Running builds on Hyper-V VMs

> This section is still under construction - please [let us know](http://www.appveyor.com/support) if you are interested to run "stateless" builds on Hyper-V VMs.

Setup process outline:

* Create "master" Hyper-V VM
* Install build agent on master VM
	* Change URL to point Web role
	* Change agent mode to `HyperV`
	* Start BuildAgent service to make sure it connects to AppVeyor Web role
