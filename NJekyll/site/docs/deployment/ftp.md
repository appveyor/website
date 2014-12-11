---
layout: docs
title: Deploying by FTP
---

# Deploying by FTP

FTP provider supports the following protocols:

* **FTP**
* **FTPS** (FTP over SSL)
* **SFTP** (SSH File Transfer Protocol)

and can work in two modes:

### Copy artifacts to remote FTP location
 
Build artifacts are copied "as is" with preserving artifact folder structure. For example, if you are copying build artifacts `bin\myapp.zip` and `logs\testlog.log` to remote FTP folder `builds\$(appveyor_build_version)\results` both files will be copied as `builds\<version>\results\bin\myapp.zip` and `builds\<version>\results\logs\testlog.log` respectively.

To copy artifacts use `artifact` setting; leave it blank to FTP all artifacts.

### Application deployment

This scenario is used for deploying web application from `.zip` artifact. During deployment AppVeyor downloads artifact, unpacks it to a temporary directory and then copies all files from that directory to remote FTP location preserving directory structure.

To deploy artifact as an application use `application` setting.

## Provider settings

* **Protocol** (`protocol`) - `ftp`, `ftps` or `sftp`; default is `ftp`.
* **Host** (`host`) - FTP server host name or IP address without protocol prefix, for example `ftp.myserver.com` or `43.34.66.4`.
* **Username** (`username`) - FTP user name.
* **Password** (`password`) - FTP user password.
* **Remote folder** (`folder`) - remote FTP folder to copy artifacts to or root of web application.
* **Active mode** (`active_mode`) - enable FTP active mode. Default mode is passive. This setting is ignored for `sftp` protocol.
* **Artifact** (`artifact`) - name of artifact(s) to copy. Leave blank to copy all artifacts.
* **Application** (`application`) - name of artifact with application package to expand to remote FTP location.

Configuring in `appveyor.yml`:

    deploy:
      provider: FTP
      protocol: ftps
      host: ftp.oursite.com
      username: webuser01
      password:
        secure: AABBBCCCCddd123==
      folder: wwwroot
      artifact: /.*\.nupkg/          # deploy on tag push only
