---
layout: docs
title: AppVeyor Enterprise Maintenance Guide
---

<!-- markdownlint-disable MD022 MD032 -->
# AppVeyor Enterprise Maintenance Guide
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Updating AppVeyor installation

RDP into AppVeyor server and open PowerShell console.

To update AppVeyor Installation PowerShell module (AppVeyor Installer) run the following command:

```posh
iex ((New-Object Net.WebClient).DownloadString('https://www.appveyor.com/downloads/enterprise/install.ps1'))
```

To update AppVeyor installation (Web and Worker roles) to the latest available version run:

```posh
Update-AppVeyor
```


## Backup

At the moment AppVeyor does not offer an automatic backups, so you should either do the backup manually
or setup your own backup script/solution.

For AppVeyor backup you should care about the following things:

* Settings
* SQL database
* Artifacts

### Settings

AppVeyor application settings are stored in Registry under the following key:

    HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Server

Make sure you have exported this key and put into safe place once AppVeyor is installed or before doing any maintenance of AppVeyor server.

### SQL database

You should periodically backup `AppveyorCI` SQL Server database with AppVeyor data.

There are three more databases: `SbGatewayDatabase`, `SbManagementDB` and `SbMessageContainer01` used by Microsoft Service Bus (queueing service), but they hold transitional data mostly and can be restored on a
new server by installing Service Bus.

### Artifacts

If you have configured external storage (Azure, AWS or Google) for build artifacts then you are good.

If you have configured local file system for storing build artifacts you should think about periodic copying the contents of that folder to a safe location.

# Troubleshooting and support

During installation AppVeyor uses randomly-generated values for security keys, account passwords and other sensitive values. All these values as well as other installer data such as AppVeyor roles and versions installed can be found under this registry key:

    HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Install

When something goes wrong:

* If build real-time log stops working there might be a transient issue with SignalR. Try doing `CTRL+F5` in browser to restart SignalR connection.
* Restart IIS and/or `Appveyor.Worker` services.
* If nothing helped - [report the issue](/support/) to AppVeyor team. While reporting the issue look into these places for possible errors/warning:
    * Web browser's "Developer tools" console - for any JavaScript-related errors.
    * `AppVeyor` event log in Event Viewer under `Applications and Services Logs\AppVeyor`. Web, Worker and Build Agent roles write logs there.