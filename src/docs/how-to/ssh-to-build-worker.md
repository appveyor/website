---
layout: docs
title: Accessing Linux build worker via SSH
---

# Accessing Linux build worker via SSH

AppVeyor starts every build on clean dedicated build worker VM. If you need to troubleshoot a broken build you can access running Linux VM via SSH with a full "root" access.

To enable SSH access during the build you should configure two environment variables (either in `appveyor.yml` or on **Environment** tab of project settings):

* `APPVEYOR_SSH_KEY` - public portion of your SSH key, for example `ssh-rsa AAAAB3NzaC1yc2EAAAABJQ...6TMCNw==`. This is mandatory variable.
* `APPVEYOR_SSH_BLOCK` - if set to `true` SSH connection details will be displayed and the build will be blocked until `~/build.lock` is deleted; otherwise SSH details will be displayed and the build will continue. This variable is optional with default value `false`.

> Despite the fact `appveyor.yml` settings takes over the UI environment variables are handled differently, i.e. environment variables defined on UI are getting merged with those one defined in `appveyor.yml`.

Add the following command at the place where SSH access should be enabled:

    curl -sflL 'https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-ssh.sh' | bash -e -

For example, to enable SSH access at the very beginning of the build, during `init` phase:

```yaml
init:
- sh: curl -sflL 'https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-ssh.sh' | bash -e -

environment:
  APPVEYOR_SSH_KEY: <ssh public key>
  APPVEYOR_SSH_BLOCK: true
```

SSH session is limited by allotted build time.

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
