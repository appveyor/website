---
layout: docs
title: Secure files
---

# Secure files

Sometimes you need to store sensitive information

[Secure-file](https://github.com/appveyor/secure-file) utility can be used for encrypting/decrypting arbitrary files using Rijndael method. It enables you to safely store sensitive data (SSH keys, certificates, etc.) in the source control repository and then use it during the build. 

High-level scenario of using this utility in [AppVeyor CI](http://www.appveyor.com) environment:

- Encrypt file on development machine.
- Commit encrypted file to source control.
- Place "secret" to project environment variable.
- Decrypt file during the build.


## Encrypting file on development machine

From command line download [`secure-file` NuGet package](https://www.nuget.org/packages/secure-file/):

    nuget install secure-file -ExcludeVersion

Encrypt file:

    secure-file\tools\secure-file -encrypt C:\path-to\filename-to-encrypt.ext -secret MYSECRET1234

Encrypted file will be saved in the same directory as input file, but with `.enc` extension added. You can optionally specify output file name with `-out` parameter.

Commit encrypted file to source control.


## Decrypting file in AppVeyor build

Put "secret" value to project environment variables on Environment tab of project settings on in `appveyor.yml` as [secure variable](https://ci.appveyor.com/tools/encrypt):

    environment:
      my_secret:
        secure: BSNfEghh/l4KAC3jAcwAjgTibl6UHcZ08ppSFBieQ8E=

To decrypt the file add these lines to `install` section of your project config:

    install:
    - nuget install secure-file -ExcludeVersion
    - secure-file\tools\secure-file -decrypt path-to\encrypted-filename.ext.enc -secret %my_secret%

> Note that file won't be decrypted on Pull Request builds as secure variables are not set during PR build.

