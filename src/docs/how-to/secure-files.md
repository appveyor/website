---
layout: docs
title: Secure files
---

# Secure files

Sometimes you need to store sensitive information in your repositories.

The [Secure-file](https://github.com/appveyor/secure-file) utility can be used for encrypting/decrypting arbitrary files using the Rijndael method. It enables you to safely store sensitive data (SSH keys, certificates, etc.) in the source control repository and then use it during the build.

High-level scenario of using this utility in the [AppVeyor CI](https://www.appveyor.com) environment:

* Encrypt the file on the development machine.
* Commit the encrypted file to source control.
* Place the "secret" into a project environment variable.
* Decrypt the file during the build.

System requirements:

* Windows or Linux
* .NET Core Runtime 2.0

## Encrypting file on development machine

Download `secure-file` utility by running the following command on development machine:

<div class="code-tabs">
<ul>
    <li class="current">Windows</li><li>Linux</li>
</ul>
<div markdown="1" class="current">
```posh
iex ((New-Object Net.WebClient).DownloadString('https://raw.githubusercontent.com/appveyor/secure-file/master/install.ps1'))
```
</div>
<div markdown="1">
```bash
curl -sflL 'https://raw.githubusercontent.com/appveyor/secure-file/master/install.sh' | bash -e -
```
</div>
</div>

To encrypt a file:

<div class="code-tabs">
<ul>
    <li class="current">Windows</li><li>Linux</li>
</ul>
<div markdown="1" class="current">
```posh
appveyor-tools\secure-file -encrypt C:\path-to\filename-to-encrypt.ext -secret MYSECRET1234
```
</div>
<div markdown="1">
```bash
./appveyor-tools/secure-file -encrypt /path-to/filename-to-encrypt.ext -secret MYSECRET1234
```
</div>
</div>

Encrypted file will be saved in the same directory as the input file, but with the `.enc` extension added. You can optionally specify output file name with the `-out` parameter.

After that commit the encrypted file to source control.


## Decrypting files during an AppVeyor build

Put the "secret" value to the project environment variables on the _Environment_ tab of the project settings or in the `appveyor.yml` as a [secure variable](https://ci.appveyor.com/tools/encrypt):

```yaml
environment:
  my_secret:
    secure: BSNfEghh/l4KAC3jAcwAjgTibl6UHcZ08ppSFBieQ8E=
```

To decrypt the file, add these lines to the `install` section of your project config:

```yaml
install:
  - ps: iex ((New-Object Net.WebClient).DownloadString('https://raw.githubusercontent.com/appveyor/secure-file/master/install.ps1'))
  - cmd: appveyor-tools\secure-file -decrypt path-to\encrypted-filename.ext.enc -secret %my_secret%
  - sh: ./appveyor-tools/secure-file -decrypt path-to/encrypted-filename.ext.enc -secret $my_secret
```

The line starting with `cmd:` will run on Windows-based images only and the line starting with `sh:` on Linux.

> Note that file won't be decrypted on Pull Request builds as secure variables are not set during PR build.
