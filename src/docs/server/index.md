---
layout: docs
title: Getting started with AppVeyor Server
---

<!-- markdownlint-disable MD022 MD032 -->
# Getting started with AppVeyor Server
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Introduction

**AppVeyor Server** is a downloadable version of hosted AppVeyor CI service that can be installed on your own server. AppVeyor Server can be installed on **Windows**, **Linux** and **macOS** (coming soon) and it's integrated with popular source control providers such GitHub, Bitbucket, GitLab, Azure DevOps or generic Git/Mercurial/SVN repo. Out-of-the-box AppVeyor is able to run builds as processes, inside Docker containers or Hyper-V/Azure/GCE/AWS virtual machines. With affordable cloud-friendly licensing AppVeyor Server can be scaled from a simple build server for your team to a powerful multi-cloud CI/CD solution for your entire organization.

## System requirements

### Windows 10

[TBD]

What builds are supported, what recommended?

Docker for Windows? Experimental mode vs MobyLinuxVM.

Git

### Windows Server

Why 2019 is better (LCOW, lighter image sizes).

Docker EE or CE? What's the difference?

Which cloud provider is better?

### Linux

AppVeyor Server was tested on Ubuntu 16.04 and 18.04 only, but, potentially, can be installed on [any Linux distro supported by .NET Core](https://docs.microsoft.com/en-us/dotnet/core/linux-prerequisites?tabs=netcore2x#supported-linux-versions).

Works perfectly fine on $10 DigitalOcean droplet or Linux VM in any cloud.

### Git

Git is only required if you work  if you are going to run local builds. If you are going to run Docker builds AppVeyor images have Git/Hg/SVN pre-installed.

### Docker

Docker is not required, but recommended! AppVeyor has 1st-class support for **Docker EE** on Windows Server, **Docker Desktop for Windows** (aka Docker CE) on Windows 10 and **Docker CE** on Linux.

* [Install Docker Engine - Enterprise on Windows Servers](https://docs.docker.com/install/windows/docker-ee/)
* [Install Docker Desktop for Windows](https://docs.docker.com/docker-for-windows/install/)
* [Get Docker CE for Ubuntu](https://docs.docker.com/install/linux/docker-ce/ubuntu/)

## Installation

### Windows

AppVeyor Server can be installed on **Windows 7 SP1 or later** and **Windows Server 2008 R2 SP1 or later**.

[Download AppVeyor Server MSI installer](https://www.appveyor.com/downloads/appveyor-server/latest/windows/appveyor-server.msi) and run it.

Once the installation complete AppVeyor Web interface will be opened in a new browser window to continue with AppVeyor configuration.

### Linux

[Download AppVeyor Server DEB package (link required)](https://www.appveyor.com) and install `.deb` package:

    sudo apt install ./appveyor-server-7.0.2000.deb

Once the installation complete open web browser and navigate to `http://<server-ip>` or `http://<server-ip>:8050` (if port 80 is already taken by another app) to continue with AppVeyor configuration.

## Running your first build

[TBD]

What are process, Docker or VM isolation.

## Running builds in Azure VMs

* Default build clouds, images, storage, etc.

## Upgrading AppVeyor Server

### Windows

To check currently installer version of AppVeyor navigate to `http://<appveyor-url>/system/settings/license` page. Alternatively, installation version can be seen on Windows "Add or remove programs" screen.

To upgrade AppVeyor download the latest `appveyor-server.msi` installer. Right-click it in Windows Explorer and check the version on dialog "Details" tab. Run the installer to upgrade AppVeyor. Once the service is started and web interface available navigate to "License" settings page to verify the version.

### Linux

[TBD]

## Backup/restore AppVeyor Server

### Windows

Default AppVeyor Server installation keeps all application data (database, artifacts and certificates) in `%PROGRAMDATA%\AppVeyor\Server` directory and all application settings under `HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Server` registry key.

To backup AppVeyor installation export `HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Server` registry key and then copy both exported `.reg` file and entire data directory to a safe location.

To restore AppVeyor re-install it with the latest `.msi`, import backup `.reg` file by overwriting existing values and restore contents of data directory.

### Linux

[TBD]


## Troubleshooting

### Windows

#### Installation location

AppVeyor Server is 64-bit application and can be run on 64-bit OS only.

AppVeyor Server is installed into `%PROGRAMFILES%\AppVeyor\Server` directory.

The new `Appveyor.Server` Windows service is created pointing to `appveyor-server.exe` executable. Service runs under `appveyor` user account which is created by installer and added to local `Administrators` group. The name of service account can be customized with `APPVEYOR_USER_NAME` and `APPVEYOR_USER_PASSWORD` installer variables.

#### Settings

AppVeyor settings are stored under `HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Server` registry key. The values under that key are preserved during AppVeyor Server updates.

#### Data

AppVeyor database, SSL certificate (if configured), build artifacts and other data are stored in `%PROGRAMDATA%\AppVeyor\Server` directory. The data directory stays intact on AppVeyor updates and after uninstall. Data directory path can be customized with `APPVEYOR_DATA_DIR` installer variable.

By default, AppVeyor stores data in SQLite database `%PROGRAMDATA%\AppVeyor\Server\appveyor-server.db`. All sensitive data in the database is encrypted with "master key". Master key and its salt are stored in `HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Server\Security.MasterKey` and `HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Server\Security.MasterKeySalt` registry values respectively. These values are automatically generated on the first AppVeyor install and preserved during updates, however they will be deleted on AppVeyor uninstall. Both keys can be customized during installation by `APPVEYOR_MASTER_KEY` and `APPVEYOR_MASTER_KEY_SALT` installer variables.

> NOTE: If master key and its salt are lost AppVeyor database will be unusable!

#### SQL Server database

AppVeyor Server can be configured to store its data in SQL Server database. During the installation specify the following installer variables:

* `APPVEYOR_DATABASE_PROVIDER=SQLServer`
* `APPVEYOR_SQLSERVER_CONNECTION_STRING=Server=<ip-or-host>;Database=<database>;User ID=<user>;Password=<password>`

After installation provider and connection string can be changed in the registry values `Database.Provider` and `Database.SQLiteConnectionString` respectively.

#### PostgreSQL database

AppVeyor Server can be configured to store its data in PostgreSQL database. During the installation specify the following installer variables:

* `APPVEYOR_DATABASE_PROVIDER=PostgreSQL`
* `APPVEYOR_POSTGRESQL_CONNECTION_STRING=Host=<host-or-ip>;Port=5432;Database=<database>;Username=<user>;Password=<password>`

After installation provider and connection string can be changed in the registry values `Database.Provider` and `Database.PostgreSqlConnectionString` respectively.

#### Logs

AppVeyor Server logs are written to Windows Event log. They can be viewed in Event Viewer under **Applications and Services Logs -> AppVeyor**. It's recommended to increase the size of AppVeyor event log (for example, to 10Mb) to keep more logging data.

#### Web bindings

By default, AppVeyor web app is trying to bind to `80` and `443` ports for `http` and `https` protocols respectively. If one of these ports is taken by another application AppVeyor will bind to `8050` and `8051` ports. Binding is created on all (`*`) interfaces, so AppVeyor web can be accessed with both loopback (e.g. `localhost`) and external (if configured) addresses. AppVeyor web interface ports can be customized with `APPVEYOR_HTTP_PORT` and `APPVEYOR_HTTPS_PORT` installer variables. After installation ports can be changed in the registry under `HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Server` key.

A new Windows advanced firewall rule `AppVeyor Server` is configured by AppVeyor installer which allows access to `%PROGRAMFILES%\AppVeyor\Server\appveyor-server.exe` application.

#### Custom installation

To run AppVeyor Server installer with custom parameters and create installation log use the following command:

    msiexec /i appveyor-server.msi /L*V appveyor-install.log PARAM_1=VAL_1 PARAM_2=VAL_2 ...

For example, to install AppVeyor with SQL Server database:

    msiexec /i appveyor-server.msi /L*V appveyor-install.log APPVEYOR_DATABASE_PROVIDER=SQLServer APPVEYOR_SQLSERVER_CONNECTION_STRING=<connection-string>

Full list of installer variables and their default values:

* `APPVEYOR_USER_NAME`: `appveyor`
* `APPVEYOR_USER_PASSWORD`: `<random>`
* `APPVEYOR_DATA_DIR`: `%ProgramData%\AppVeyor\Server`
* `APPVEYOR_DATABASE_PROVIDER`: `SQLite`
* `APPVEYOR_SQLITE_CONNECTION_STRING`: `Data Source=[DataDir]\appveyor-server.db`
* `APPVEYOR_POSTGRESQL_CONNECTION_STRING`: `<empty>`
* `APPVEYOR_SQLSERVER_CONNECTION_STRING`: `<empty>`
* `APPVEYOR_MASTER_KEY`: `<random>`
* `APPVEYOR_MASTER_KEY_SALT`: `<random>`
* `APPVEYOR_HTTP_PORT`: `80`
* `APPVEYOR_HTTPS_PORT`: `443`

### Docker builds

#### Known issues

* `nanoserver`-based build images do not contain Mercurial (`hg.exe`) and Subversion (`svn.exe`). For Bitbucket Mercurial repositories you can set `shallow_clone: true`;
* `ssh` utility is not working on `nanoserver`-based build image preventing cloning of private repositories via SSH - only cloning via HTTPS is working;

Network "blips" with `net::ERR_NETWORK_CHANGED` error in Chrome. Disable Proxy "Automatic detect settings" - ADD SCREENSHOT.

* [Windows 10 release history](https://docs.microsoft.com/en-us/windows/windows-10/release-information)
* [Windows Container Version Compatibility](https://docs.microsoft.com/en-us/virtualization/windowscontainers/deploy-containers/version-compatibility)
