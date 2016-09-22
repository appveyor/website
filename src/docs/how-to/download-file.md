---
layout: docs
title: Download file
---

<!-- markdownlint-disable MD022 MD032 -->
# How to download file
{:.no_toc}

Sometimes you need to download additional files (installers, libraries, resources, etc.) required by your build process.

There is a number of ways you can use to download file in AppVeyor environment:

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->


## Invoke-WebRequest cmdlet

### HTTP

```powershell
$source = "http://yoursite.com/file.xml"
$destination = "c:\application\data\newdata.xml"
Invoke-WebRequest $source -OutFile $destination
```

### FTP

```powershell
$source = "ftp://yoursite.com/file.xml"
$destination = "c:\application\data\newdata.xml"
Invoke-WebRequest $source -OutFile $destination -Credential ftpUser
```

Examples taken from [this blog post](http://www.powershellatoms.com/basic/download-file-website-powershell/).


## WebClient class

You can use the following PowerShell code to download file using `System.Net.WebClient` class which is part of .NET Framework:

    (New-Object Net.WebClient).DownloadFile('<file_url>', '<local_file_name>')

Where:

* `<file_url>` - URL of remote file
* `<local_file_name>` - **full** local path to downloaded file


## Start-FileDownload cmdlet

AppVeyor build session has built-in `Start-FileDownload` cmdlet for downloading files. There are some advantages of using this cmdlet instead of WebClient class:

* It maintains current directory context
* It allows specifying request timeout.

Command syntax:

    Start-FileDownload <url> [-FileName <string>] [-Timeout <int>]

Timeout value is milliseconds. Default timeout is 300000 (5 minutes).

For example, the following command downloads remote file to the current folder with `installer.msi` name:

    Start-FileDownload 'http://www.myserver.com/packages/installer.msi'

Example usage in `appveyor.yml`:

```yaml
install:
  - ps: Start-FileDownload 'http://www.myserver.com/packages/installer.msi'
```


## AppVeyor command-line utility

AppVeyor command-line utility (`appveyor.exe`) which is a part of [Build Agent API](/docs/build-worker-api/) provides `DownloadFile` command which behaves similar to Start-FileDownload cmdlet.

Command syntax:

    appveyor DownloadFile <url> [-FileName <string>] [-Timeout <int>]

Example:

    appveyor DownloadFile http://www.myserver.com/packages/installer.msi

## Curl

[Curl](https://curl.haxx.se) (`curl.exe`) comes bundled with [Git](https://git-scm.com) and is therefore also available on the build workers.  Users on Unix-like operating systems may be more familiar with this command.

Command syntax:

    curl [-fsSL...] [-o <output-filename>] [-m <timeout-in-seconds>] <url>

For scripting, using `-fsS` (or `-fsSL`) is recommended.  For more info on what the flags do, see the [curl manual](https://curl.haxx.se/docs/manpage.html).  Be aware that if neither `-o` nor `-O` are given, curl will dump the data to standard output.

Example:

    curl -fsS -o installer.msi http://www.myserver.com/packages/installer.msi
