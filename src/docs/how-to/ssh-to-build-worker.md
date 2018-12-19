---
layout: docs
title: Accessing Linux build worker via Secure Shell (SSH)
---

# Accessing Linux build worker via Secure Shell

AppVeyor starts every build on clean dedicated build worker VM.
Sometimes the best way to troubleshoot a broken build is looking into Linux VM via Secure Shell (SSH).
During the build, you have full "root" access to that VM.

To enable SSH access during the build, you should configure two environment variables (either in `appveyor.yml` or on **Environment** tab of project settings):

* `APPVEYOR_SSH_KEY` - public portion of your SSH key, for example `ssh-rsa AAAAB3NzaC1yc2EAAAABJQ...6TMCNw==`. This variable is mandatory.
* `APPVEYOR_SSH_BLOCK` - if set to `true`, the build will be blocked until `~/build.lock` is deleted; otherwise, the build will continue. This variable is optional, with default value `false`.

> Despite the fact `appveyor.yml` settings take over the UI, environment variables are handled differently, i.e. environment variables defined on UI are merged with those defined in `appveyor.yml`.

Add the following Bash command at the place where SSH access should be enabled:

```bash
curl -sflL 'https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-ssh.sh' | bash -e -
```

For example, to enable SSH access at the very beginning of the build, during `init` phase:

```yaml
environment:
  APPVEYOR_SSH_KEY: <your ssh public key>

init:
  - sh: curl -sflL 'https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-ssh.sh' | bash -e -
```

SSH connection details for the current build worker will be displayed and build will continue.
Displaying SSH connection details during `init` phase helps troubleshooting stuck builds.

If you need to investigate worker on build finish, add `export APPVEYOR_SSH_BLOCK=true` to display SSH connection details and pause the build until a special "lock" file on VM user home directory is deleted:

```yaml
on_finish:
  - sh: export APPVEYOR_SSH_BLOCK=true
  - sh: curl -sflL 'https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-ssh.sh' | bash -e -
```

Alternatively, you can add `sh: sleep 60m` to postpone finishing a script so that you have more time to investigate with `ssh`.

SSH session is limited by overall build time (60 min).

## Generating SSH key

### ssh-keygen

`ssh-keygen` is available on Linux ([Windows Subsystem for Linux](https://docs.microsoft.com/en-us/windows/wsl/install-win10) on Windows 10) and it comes bundled with Git for Windows and usually located at `C:\Program Files\Git\usr\bin\ssh-keygen.exe`.

The following command generates 2048-bit RSA key and saves its private and public keys into `C:\MyProjects\ssh-key.key` and `C:\MyProjects\ssh-key.key.pub` files respectively:

    "C:\Program Files\Git\usr\bin\ssh-keygen.exe" -t rsa -b 2048 -N "" -C appveyor -f /c/MyProjects/ssh-key.key

### PuTTYgen

You can generate SSH key using "PuTTYgen" GUI tool which is a part of [PuTTY](https://www.putty.org/) - a free SSH client for Windows. To start PuTTYgen click Windows start menu and type `PuTTYgen`. Configure the following parameters:

* Type of key to generate: `RSA`
* Number of bits in a generated key: `2048`

Click **Generate** button then save both public and private keys.
