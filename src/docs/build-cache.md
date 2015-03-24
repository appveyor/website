---
layout: docs
title: Build cache
---

# Caching data between builds

## What is build cache

AppVeyor runs every build on a clean virtual machine. Virtual machine state is not preserved between build which means every build downloads sources, install NuGet packages, Node.js modules, Ruby gems or pulls dependencies.

*Build cache* allows you to preserve contents of selected directories and files between project builds.

Use cases:

 - NuGet `packages` folder
 - Local or global `node_modules` directory with `npm` packages
 - Ruby gems
 - Large data files or build dependencies
 - Chocolatey packages


## How it works

Build cache is a virtual cloud storage automatically created for the project. When you configure cached directories/files the following events occur:


### Restore cache after repository sources fetched

For every cache item AppVeyor checks if its archive exists in build cache and if it does AppVeyor downloads archive and unpack it to original location.


### Save/update cache before build finishes

For every cache item AppVeyor calculates checksum and if checksum does not match to the one in build cache or archive not found AppVeyor compresses directory of file and pushes archive to a cloud storage.

AppVeyor uses fastest compression level to minimize I/O and effectively deal with large number of small files. Build cache storage is physically located in the same datacenter as build workers. **Resulting archive should not exceed 100 MB.**


### Cache dependencies

It is possible to specify a dependency for each cache item:

```yaml
cache:
  - C:\ProgramData\chocolatey\lib -> appveyor.yml
```

This will mean invalidate `C:\ProgramData\chocolatey\lib` whenever `appveyor.yml` is changed. Dependency check is performed on build start and cache item won't be restored if dependency changed. At the end of successful build cache item will be updated.


## Configuring cache items

On Environment tab of project settings you can specify directories and files that must be preserved between builds. Both absolute and relative paths are supported.

In `appveyor.yml`:

```yaml
cache:
  - packages -> **\packages.config      # preserve "packages" directory in the root of build folder but will reset it if packages.config is modified
  - projectA\libs
  - node_modules                        # local npm modules
  - '%APPDATA%\npm-cache'               # npm cache
  - '%USERPROFILE%\.nuget\packages -> **\project.json'  # project.json cache
```

Note the use of single quotes around the entire line, when environment variables are used.

## Caching Chocolatey packages

> Caching Chocolatey packages/installations might be tricky as it depends on whether the package is "portable" (installed from a zip - [7zip.commandline](https://chocolatey.org/packages/7zip.commandline) is a good example) or "native" (installed from MSI to `Program Files`, for example [git.install](https://chocolatey.org/packages/git.install)). Read more about [distinction between installable and portable applications](https://github.com/chocolatey/choco/wiki/ChocolateyFAQs#what-distinction-does-chocolatey-make-between-an-installable-and-a-portable-application). **The solution below works for "portable" packages only.**

`C:\Users\appveyor\AppData\Local\Temp\chocolatey\` is used as a temp location for downloading packages and, generally, shouldn't be cached.

Portable packages are installed to `C:\ProgramData\chocolatey\lib` and "shim" for executable is added to `C:\ProgramData\chocolatey\bin`. So, the solution for caching portable packages could be:

```yaml
cache:
  - C:\ProgramData\chocolatey\bin -> appveyor.yml
  - C:\ProgramData\chocolatey\lib -> appveyor.yml
```

If your build logic is in some `my_build.cmd` you can use it as a dependency instead of `appveyor.yml`.
