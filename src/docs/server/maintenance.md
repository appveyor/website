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

## Updating AppVeyor Installer

AppVeyor Installer is a PowerShell module which is downloaded and added to your PowerShell profile on each run of the `install.ps1` bootstrap script.

To update AppVeyor Installer module login via RDP into AppVeyor server, open PowerShell console and run the following command:

```posh
iex ((New-Object Net.WebClient).DownloadString('https://www.appveyor.com/downloads/enterprise/install.ps1'))
```

## Backup AppVeyor

To backup AppVeyor installation open PowerShell console and run:

```posh
Backup-AppVeyor
```

AppVeyor Installer will put **SQL database** (`.bak`) and **application settings** (`.reg`) backups into `C:\Program Files\AppVeyor\Backup` directory.

You can create backups in a custom location by running:

```posh
Backup-AppVeyor -Location <your-backup-directory>
```

We recommend doing backups before updating AppVeyor installation or doing server maintenance.

Copy backups from the server to an external location.

### Artifact backups

If you have configured external storage (Azure, AWS or Google) for build artifacts then you are good.

If you have configured local file system for storing build artifacts you should think about periodically copying the contents of that folder to a safe location.


## Updating AppVeyor

You can check which AppVeyor version is currently installed by running:

```posh
Get-AppVeyorVersion
```

To update AppVeyor installation (Web and Worker roles) to the latest available version run:

```posh
Update-AppVeyor
```

To update AppVeyor roles into a specific version:

```posh
Update-AppVeyor -Version <version>
```

Run `Get-AppVeyorVersions` to get the list of all available versions.


# Troubleshooting and support

During installation AppVeyor uses randomly-generated values for security keys, account passwords and other sensitive values. All these values as well as other installer data such as AppVeyor roles and versions installed can be found under this registry key:

    HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Install

When something goes wrong:

* If build real-time log stops working there might be a transient issue with SignalR. Try doing `CTRL+F5` in browser to restart SignalR connection.
* Restart IIS and/or `Appveyor.Worker` services.
* If nothing helped - [report the issue](/support/) to the AppVeyor team. While reporting the issue look into these places for possible errors/warning:
    * Web browser's "Developer tools" console - for any JavaScript-related errors.
    * `AppVeyor` event log in Event Viewer under `Applications and Services Logs\AppVeyor`. Web, Worker and Build Agent roles write logs there.