---
title: Deploying by FTP
---

# Deploying by FTP

FTP provider can work in two modes:

* Copy artifacts to remote FTP location
* Expand selected artifact as an application to remote FTP location (deploy as application).

## Provider settings

* **Server** (`server`) - FTP server address, e.g. ftp.myserver.com
* **Username** (`username`)
* **Password** (`password`)
* **Remote folder** (`folder`) - remote FTP folder to copy artifacts to or root of web application.
* **Enable SSL** (`enabled_ssl`) - enable FTPS.
* **Active mode** (`active_mode`) - enable active mode. By default FTP client works in passive (recommended) mode.
* **Artifact** (`artifact`) - name of artifact to copy.
* **Application** (`application`) - name of artifact with application package to expand to remote FTP location.

Configuring in `appveyor.yml`:

    deploy:
      provider: FTP
      server:
      username:
      password:
      folder:
      enable_ssl: true|false (disabled by default)
      active_mode: true|false (disabled by default)
      artifact:
      application:
