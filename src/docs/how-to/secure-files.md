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


## Encrypting file on development machine

From the command line download the [`secure-file` NuGet package](https://www.nuget.org/packages/secure-file/):

    nuget install secure-file -ExcludeVersion

File encryption:

    secure-file\tools\secure-file -encrypt C:\path-to\filename-to-encrypt.ext -secret MYSECRET1234

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
  - nuget install secure-file -ExcludeVersion
  - secure-file\tools\secure-file -decrypt path-to\encrypted-filename.ext.enc -secret %my_secret%
```

> Note that file won't be decrypted on Pull Request builds as secure variables are not set during PR build.
