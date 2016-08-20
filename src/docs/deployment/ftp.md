---
layout: docs
title: Deploying by FTP
---

# Deploying by FTP

FTP provider supports the following protocols:

* FTP
* FTPS (FTP over SSL)
* SFTP (SSH File Transfer Protocol)

and can work in two modes:

<!-- markdownlint-disable MD001 -->

### Copy artifacts to remote FTP location

Build artifacts are copied "as is" with preserving artifact folder structure.
For example, if you are copying build artifacts `bin\myapp.zip` and `logs\testlog.log`
to remote FTP folder `builds\$(appveyor_build_version)\results` both files will be copied
as `builds\<version>\results\bin\myapp.zip` and `builds\<version>\results\logs\testlog.log`
respectively.

To copy artifacts use `artifact` setting; leave it blank to FTP all artifacts.

### Application deployment

This scenario is used for deploying web application from `.zip` artifact. During deployment
AppVeyor downloads artifact, unpacks it to a temporary directory and then copies all files
from that directory to remote FTP location preserving directory structure.

To deploy artifact as an application use `application` setting.

<!-- markdownlint-enable MD001 -->

## Provider settings

* **Protocol** (`protocol`) - `ftp`, `ftps` or `sftp`; default is `ftp`.
* **Host** (`host`) - FTP server host name or IP address without protocol prefix, for example `ftp.myserver.com` or `43.34.66.4`.
* **Username** (`username`) - FTP user name.
* **Password** (`password`) - FTP user password.
* **Remote folder** (`folder`) - remote FTP folder to copy artifacts to or root of web application.
* **Active mode** (`active_mode`) - enable FTP active mode. Default mode is passive. This setting is ignored for `sftp` protocol. There are caveats - see below.
* **Artifact** (`artifact`) - name of artifact(s) to copy. Leave blank to copy all artifacts.
* **Application** (`application`) - name of artifact with application package to expand to remote FTP location.

Configuring in `appveyor.yml`:

```yaml
deploy:
  provider: FTP
  protocol: ftps
  host: ftp.oursite.com
  username: webuser01
  password:
    secure: AABBBCCCCddd123==
  folder: wwwroot
  artifact: /.*\.nupkg/          # upload all NuGet packages to release assets
```


## Active mode

**Active-mode FTP** is often referred as "client-managed" session and thus requires a range
of inbound ports opened on FTP client machine to to allow FTP server connections.
You can read more about FTP modes in this article: [https://support.microsoft.com/kb/283679](https://support.microsoft.com/kb/283679)

You can use Active-mode FTP only while deploying from build running on premium environment as you can control firewall on its build workers.

To allow incoming FTP connections add this PowerShell command to *Install* section on Environment tab of AppVeyor project settings or in `install` section of `appveyor.yml`:

    New-NetFirewallRule -DisplayName "Allow Inbound FTP" -Direction Inbound -Program 'C:\Program Files\AppVeyor\BuildAgent\Appveyor.BuildAgent.Interactive.exe' -RemoteAddress Any -Action Allow

**Active mode won't work from Environments as Azure Cloud Service worker roles doing deployment are not accessible from the internet.**
Anyway, we suggest switching to Passive mode unless you have a very strong reason of not doing so.
