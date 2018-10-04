---
layout: docs
title: Accessing Windows build worker via Remote Desktop (RDP)
---

# Accessing Windows build worker via Remote Desktop

AppVeyor starts every build on clean dedicated build worker VM.
Sometimes the best way to troubleshoot a broken build is looking into Windows VM via Remote Desktop (RDP).
During the build, you have full "administrator" access to that VM.

Set RDP password in `APPVEYOR_RDP_PASSWORD` environment variable. This variable is optional, with default value generated for the current build worker.
You can configure that in `environment` section of `appveyor.yml` or on project settings UI (preferred way):

![appveyor-rdp-psw-env-var](/assets/img/docs/how-to/appveyor-rdp-psw-env-var.png)

> Despite the fact `appveyor.yml` settings take over the UI, environment variables are handled differently, i.e. environment variables defined on UI are merged with those defined in `appveyor.yml`.
>
> If you manually set `APPVEYOR_RDP_PASSWORD`, you must ensure that the password meets the Windows Server requirements.
> Otherwise the RDP connection will not be opened.
> The requirements are as follows:
>
> * Not contain the user's account name or parts of the user's full name that exceed two consecutive characters
> * Be at least six characters in length
> * Contain characters from three of the following four categories:
>     * English uppercase characters (A through Z)
>     * English lowercase characters (a through z)
>     * Base 10 digits (0 through 9)
>     * Non-alphabetic characters (for example, !, $, #, %)

Add the following PowerShell command at the place where RDP access should be enabled:

```cmd
iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-rdp.ps1'))
```

For example, to enable RDP access at the very beginning of the build, during `init` phase:

```yaml
environment:
  APPVEYOR_RDP_PASSWORD: <your password>

init:
  - ps: iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-rdp.ps1'))
```

RDP connection details for the current build worker will be displayed and build will continue.
Displaying RDP connection details during `init` phase helps troubleshooting stuck builds.

If you need to investigate worker on build finish, add `$blockRdp = $true;` to display RDP connection details and pause the build until a special "lock" file on VM desktop is deleted:

```yaml
on_finish:
  - ps: $blockRdp = $true; iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-rdp.ps1'))
```

RDP session is limited by overall build time (60 min).
