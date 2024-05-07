---
layout: docs
title: Build cache
---

<!-- markdownlint-disable MD022 MD032 -->
# Caching data between builds
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## What is build cache

AppVeyor runs every build on a clean virtual machine. Virtual machine state is not preserved between builds which means every build downloads sources,
installs NuGet packages, Node.js modules, Ruby gems or pulls dependencies.

*Build cache* allows you to preserve contents of selected directories and files between project builds.

Use cases:

* NuGet `packages` folder
* Local or global `node_modules` directory with `npm` packages
* Ruby gems
* Large data files or build dependencies
* Chocolatey packages


## How it works

Build cache is a virtual cloud storage automatically created for the project. When you configure cached directories/files the following events occur:


### Restore cache after repository sources fetched

For every cache item AppVeyor checks if its archive exists in build cache and if it does AppVeyor downloads archive and unpack it to original location.


### Save/update cache before build finishes

For every cache item AppVeyor calculates checksum and if checksum does not match to the one in build cache or archive not found AppVeyor compresses directory or file and pushes archive to a cloud storage.

AppVeyor uses fastest compression level to minimize I/O and effectively deal with large number of small files. Build cache storage is physically located in the same datacenter as build workers.

**Note**: By default, saving cache is disabled in Pull Request builds. Use `Save build cache in Pull Requests` checkbox on the **General** tab of project settings if you need to save cache during PR builds. This setting is not exposed in YAML to prevent unauthorized cache modifications.

### Cache dependencies

It is possible to specify a dependency for each cache item:

```yaml
cache:
  - C:\ProgramData\chocolatey\lib -> appveyor.yml
```

This will mean invalidate `C:\ProgramData\chocolatey\lib` whenever `appveyor.yml` is changed. Dependency check is performed on build start and cache item won't be restored if dependency changed. At the end of successful build cache item will be updated.

If there are multiple dependencies for a cache item they can be specified as a comma separated list:

```yaml
cache:
  - c:\tools\vcpkg\installed\ -> restore.ps1, build.ps1
```

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

Caching Chocolatey packages/installations might be tricky as it depends on whether the package is "portable" (installed from a zip - [7zip.commandline](https://chocolatey.org/packages/7zip.commandline) is a good example) or "native" (installed from MSI to `Program Files`, for example [git.install](https://chocolatey.org/packages/git.install)). Read more about [distinction between installable and portable applications](https://github.com/chocolatey/choco/wiki/ChocolateyFAQs#what-distinction-does-chocolatey-make-between-an-installable-and-a-portable-application). **The solution below works for "portable" packages only.**

`C:\Users\appveyor\AppData\Local\Temp\chocolatey\` is used as a temp location for downloading packages and, generally, shouldn't be cached.

Portable packages are installed to `C:\ProgramData\chocolatey\lib` and "shim" for executable is added to `C:\ProgramData\chocolatey\bin`. So, the solution for caching portable packages could be:

```yaml
cache:
  - C:\ProgramData\chocolatey\bin -> appveyor.yml
  - C:\ProgramData\chocolatey\lib -> appveyor.yml
```

If your build logic is in some `my_build.cmd` you can use it as a dependency instead of `appveyor.yml`.


## Cache size (beta)

With the introduction of the new cache we are also changing the way it's metered.

The total size of build cache is limited per account and depends on the plan.

Total cache size per account consist of all caches from all projects. If projects contain more than one job in [build matrix](/docs/build-configuration/#build-matrix) or in [parallel testing](/docs/parallel-testing), each job has its own separate cache, which cannot be shared with other jobs or projects.

<table class="centered">
<tr>
    <th>Free</th>
    <th>Basic</th>
    <th>Pro</th>
    <th>Premium</th>
</tr>
<tr>
    <td>1 GB</td>
    <td>1 GB</td>
    <td>5 GB</td>
    <td>20 GB</td>
</tr>
</table>

It's a hard quota which means the build will fail while trying to upload cache item exceeding the quota.<br>
The maximum size of a single cache entry is still limited, but increased to 3 GB.


## Cache speed vs size (beta)

The new cache uses `7z` to compress/uncompress files before transferring them to the cache storage.
We chose `7z` over built-in .NET compression library because it's generally faster, produces smaller archives and works with hidden files out-of-the-box.

While compressing cache item, by default AppVeyor uses `7z` with `zip` algorithm and compression level `1` ("Fastest") thus producing archive faster, but with larger size (`-tzip -mx=1` args).
However, you can change compression behavior of `7z` by providing your own command line args in `APPVEYOR_CACHE_ENTRY_ZIP_ARGS` environment variable.
For example, to enable `LZMA` compression method with the highest possible compression ratio set this variable to `-t7z -m0=lzma -mx=9`.

## Cleaning up cache

There is a number of ways you can remove unused/broken cache items from the cache.

### Remove cache entry from build config

Just comment out cache entry in `appveyor.yml` (or remove on UI) and re-run the build:

```yaml
cache:
  #- packages
```

Upon successful completion of the build cache item will be deleted from the cache.

### Add/change dependency

If your cache is defined like this:

```yaml
cache:
  - packages
```

add the dependency to `packages` entry:

```yaml
cache:
  - packages -> appveyor.yml    # or any other file
```

and the next time the build is run the cache item will be invalidated/deleted in the beginning of the build.

### REST API

You can use AppVeyor REST API to delete build cache of specific project.
Use CURL, Postman, Fiddler, PowerShell or your favorite web debugging tool to run the following query
(with [Authorization header](/docs/api/#authentication) of course):

    DELETE https://ci.appveyor.com/api/projects/{accountName}/{projectSlug}/buildcache

### Skipping cache operations for specific build

You can skip cache restore or save stages with the following tweak environment variables:

* `APPVEYOR_CACHE_SKIP_RESTORE` - set to `true` to disable cache restore
* `APPVEYOR_CACHE_SKIP_SAVE` - set to `true` to disable cache update

It can be done conditionally, for example:

```yaml
init:
- ps: IF ($env:APPVEYOR_REPO_BRANCH -eq "develop") {$env:APPVEYOR_CACHE_SKIP_SAVE = "true"}
```

### Saving cache for failed build

By default build cache is being saved only for successful build. If it should be saved for failed builds too, set `APPVEYOR_SAVE_CACHE_ON_ERROR` to `true`.
