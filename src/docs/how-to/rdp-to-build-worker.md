---
layout: docs
title: Accessing build worker via Remote Desktop (RDP)
---

# Accessing build worker via Remote Desktop

AppVeyor starts every build on clean dedicated build worker VM. Sometimes the best way to troubleshoot broken build is looking into build VM via Remote Desktop. During the build you have full "administrator" access to that VM and can access it via Remote Desktop (RDP).

Set RDP password in `APPVEYOR_RDP_PASSWORD` environment variable. You can configure that in `environment` section of `appveyor.yml` or on project settings UI (preferred way):

![appveyor-rdp-psw-env-var](/assets/img/docs/how-to/appveyor-rdp-psw-env-var.png)

To get RDP details for the current build worker add this line to `init` phase of your build:

```yaml
init:
  - ps: iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-rdp.ps1'))
```

Remote Desktop connection details will be displayed and build will continue. Displaying RDP connection during `init` phase helps troubleshooting stuck builds.

If you need to investigate worker on build finish add `$blockRdp = $true;` to display Remote Desktop connection details and pause the build until a special "lock" file on VM desktop is deleted:

```yaml
on_finish:
  - ps: $blockRdp = $true; iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-rdp.ps1'))
```

Your RDP session is limited by overall build time (60 min).
