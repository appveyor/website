---
layout: docs
title: Accessing MacOS build worker via VNC
---

# Accessing MacOS build worker via VNC

AppVeyor starts every build on clean dedicated build worker VM.
Sometimes the best way to troubleshoot a broken build is looking into MacOS VM via VNC.
During the build, you have full "root" access to that VM.

To enable VNC access during the build, you should configure two environment variables (either in `appveyor.yml` or on **Environment** tab of project settings):

* `APPVEYOR_VNC_PASSWORD` - Set RDP password. This variable is optional, with default value generated for the current build worker.
* `APPVEYOR_VNC_BLOCK` - if set to `true`, the build will be blocked until `Delete me to continue build.txt` file is deleted from user's Desktop; otherwise, the build will continue. This variable is optional, with default value `false`.

> Despite the fact `appveyor.yml` settings take over the UI, environment variables are handled differently, i.e. environment variables defined on UI are merged with those defined in `appveyor.yml`.

Add the following Bash command at the place where VNC access should be enabled:

```bash
curl -sflL 'https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-vnc.sh' | bash -e -
```

For example, to enable VNC access at the very beginning of the build, during `init` phase:

```yaml
environment:
  APPVEYOR_VNC_PASSWORD: <your password>

init:
  - sh: curl -sflL 'https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-vnc.sh' | bash -e -
```

VNC connection details for the current build worker will be displayed and build will continue.
Displaying VNC connection details during `init` phase helps troubleshooting stuck builds.

If you need to investigate worker on build finish, add `export APPVEYOR_VNC_BLOCK=true` to display VNC connection details and pause the build until a special "lock" file on VM user home directory is deleted:

```yaml
on_finish:
  - sh: export APPVEYOR_VNC_BLOCK=true
  - sh: curl -sflL 'https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-vnc.sh' | bash -e -
```

Alternatively, you can add `sh: sleep 60m` to postpone finishing a script so that you have more time to investigate with `VNC`.

VNC session is limited by overall build time (60 min).

## Accessing MacOS build worker via SSH

Accessing MacOS build worker via SSH is similar to accessing [Linux build worker](ssh-to-build-worker.md).