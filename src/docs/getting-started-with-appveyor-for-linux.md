---
layout: docs
title: Getting started with AppVeyor for Linux
---

<!-- markdownlint-disable MD022 MD032 -->
# Getting started with AppVeyor for Linux
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Introduction

Some benefits of running Linux builds on AppVeyor:

* Multiple source control systems supported:
    * GitHub.com and GitHub Enterprise
    * Bitbucket.com and Bitbucket Server
    * GitLab.com and GitLab Enterprise
    * Azure DevOps (both Git and TFVC repositories)
    * Kiln
    * Gitea
    * Any other Git, Mercurial or Subversion repository
* Configure CI for your projects via UI or in dot-file.
* Full `sudo` access to VM running build
* Promoted deployments - build once and deploy the same packages to multiple environments.
* Official out-of-the-box .NET Core support - automatic build, test and packaging of .NET Core projects.
* SQL Server 2017 for Linux.
* Bash and PowerShell Core for build flow control.
* Side-by-side configuration for both Windows and Linux builds.
* Pre-installed Docker service.
* Built-in NuGet server.
* Single cross-platform [Build Worker API](https://www.appveyor.com/docs/build-worker-api/) as on Windows (messages, test results).

## Running your build on Linux

To run your build on `Ubuntu` image either add the following line to your `appveyor.yml`:

    image: Ubuntu

or if you don't use `appveyor.yml` select `Ubuntu` image on **Environment** tab of AppVeyor project settings.

> You can also put project configuration in `.appveyor.yml` dot-file in the root of your repo. AppVeyor searches repo for `appveyor.yml` first and then for `.appveyor.yml`.

## Build configuration basics

### General build flow

* `init` scripts
* Update `/etc/hosts` file
* Clone repository
* Restore build cache
* Configure stack
* `install` scripts
* Start services
* Patch version in `.csproj` and `AssemblyInfo.cs` files
* "build" phase
* "test" phase
* "package" phase
* "deploy" phase
* `on_success` scripts
* `on_failure` scripts (if the build has failed)
* `on_finish` scripts

### Quick start

Below is a minimal `appveyor.yml` to test Node.js project:

```yaml
image: Ubuntu

install:
- npm install

test_script:
- npm test

build: off
```

> `build` phase should be off as, by default, it's set to `MSBuild` mode for automatic discovery and building of .NET Core projects (see the section below).

### Bash and PowerShell

You can use both *Bash* and *PowerShell* commands simultaneously to control the build flow.

To run Bash command either put it with `sh:` prefix or without it, for example the following two commands will be run in Bash shell:

```yaml
install:
- ls -al
- sh: sudo apt-get update
```

To run command in PowerShell session use `ps:` or `pwsh:` prefixes, for example:

```yaml
test_script:
- ps: Write-Host "Hello, world!"
```

AppVeyor keeps the same Bash and PowerShell shells for the duration of the build and all commands run in the same context. That means, for example, that local variable defined on `install` stage is available on later stages:

```yaml
init:
- ps: $my_variable = 'Test value'

test_script:
- ps: Write-Host "This is $my_variable"
```

AppVeyor exchanges **environment variables** and **current directory** between Bash and PowerShell shells. Environment variable defined in Bash command is immediately available in PowerShell command next to it and vice versa:

```yaml
install:
- MY_VAR=test
- ps: $env:MY_VAR
```

> Environment variable names are case-sensitive on Linux platform.

### Premature termination of the build process

`false` command in Bash "gracefully" terminates the build with "red" status by running `on_failure` and `on_finish` commands.

`throw "some error message"` statement in PowerShell "gracefully" terminates the build with "red" status by running `on_failure` and `on_finish` commands.

`exit 0` in Bash immediately terminates the build with "green" status; without running `on_success` and `on_finish` commands.

`exit <non-zero>` in Bash immediately terminates the build with "red" status without running `on_failure` and `on_finish` commands.

`appveyor exit` in Bash "gracefully" terminates build with "green" status by running `on_success` and `on_finish` commands.

`Exit-AppveyorBuild` in PowerShell "gracefully" terminates build with "green" status by running `on_success` and `on_finish` commands.

### Configuring language stack

AppVeyor for Linux provides a new `stack` definition for quick configuration of languages and services used by your build/tests:

```yaml
stack: <language|service> [version], <language|service> [version], ...
```

For example, to enable the latest Node.js 9.x and MySQL add this to your `appveyor.yml`:

```yaml
stack: node 9, mysql
```

The following languages can be configured in `stack`:

* `node <version>` - select Node.js
* `go <version>` - Golang
* `ruby <version>` - Ruby
* `jdk <version>` - Java
* `python <version>` - Python

The following services can be configured in `stack`:

* `docker`
* `mongodb`
* `mssql`
* `mysql`
* `postgresql` (or `pgsql`)
* `rabbitmq`
* `redis`

### "build" phase

Build phase could be either your own scripts, be enabled to build .NET Core project(s) or be turned off.

For your own scripts use `build_script` section, for example:

```yaml
build_script:
- mvn install
```

For automated building .NET Core projects please see ".NET Core support" section below.

To disable build phase completely put this:

```yaml
build: off
```

### "test" phase

Test phase could be either your own scripts, be enabled to discover and test .NET COre project(s) or be turned off.

For your own scripts use `test_script` section, for example:

```yaml
test_script:
- npm test
```

For automated testing of .NET Core projects please see ".NET Core support" section below.

To disable test phase completely put this:

```yaml
test: off
```

## Running Windows and Linux builds side-by-side

You can use the same `appveyor.yml` to control builds running on both Windows and Linux platforms.

First, start from adding a matrix of build images. For example, to run build on both `Visual Studio 2015` and `Ubuntu` images add the following:

```yaml
image:
  - Visual Studio 2015
  - Ubuntu
```

With matrix you can [specialize configuration for different jobs](/docs/build-configuration/#specializing-matrix-job-configuration) or keep flat configuration using approaches described below.

Prefix command with `cmd:` to run it on Windows image only:

    - cmd: echo Hey, I'm displayed on Windows only!

Prefix command with `sh:` to run it on Linux image only:

    - sh: printf "I'll be shown on Linux!"

Do not prefix command to run it on both Windows and Linux. You have to make sure the command is good for both Windows batch files and Bash:

    - dir
    - echo I'm running on both Windows and Linux!

PowerShell commands prefixed with `ps:` and `pwsh:` (on Linux they both run as PowerShell Core) run on both Windows and Linux, however you can distinguish between platforms by using the following PowerShell variables:

* `$isLinux` is `$true` on Linux
* `$isWindows` is `$true` on Windows

For example, the following command will print different message for the same build running on Windows and Linux:

```yaml
- ps: |
    if ($isLinux) {
      Write-Host "This is Linux!"
    } else {
      Write-Host "This is NOT a Linux!"
    }
```

AppVeyor also introduces two new environment variables defining platform:

* `CI_WINDOWS` is `true` if the build is running on Windows-based image; otherwise `false`.
* `CI_LINUX` is `true` if the build is running on Linux-based image; otherwise `false`.

You can use these environment variables in bash, Bash and PowerShell commands.

Also, there `APPVEYOR_YML_DISABLE_PS_LINUX` tweak environment variable that disables execution of PowerShell commands on Linux-based images, for example:

```yaml
environment:
  APPVEYOR_YML_DISABLE_PS_LINUX: true

install:
- ps: Write-Host "This command won't be run on Linux"
- sh: printf "This command will be run on Linux only"
```

## Docker

Ubuntu build workers have Docker service pre-installed.

To enable Docker add this line to `appveyor.yml`:

```yaml
services:
- docker
```

Then you can use Docker in your build, for example:

```yaml
test_script:
- docker run hello-world
```

## .NET Core support

AppVeyor for Linux has built-in first-class support for building, testing, packaging and deploying .NET Core applications and libraries.

To enable automatic discovery and building of .NET Core solution add to `appveyor.yml`:

```yaml
build:
  verbosity: minimal
```

The construction above will make AppVeyor looking for `.sln` file in the root of the repository first and then recursively in sub-directories.

To specify direct path to `.sln` or `.csproj` file add:

```yaml
build:
  project: MySolution.sln
  verbosity: minimal
```

> By default, AppVeyor uses the latest .NET Core SDK installed to build the project, however you can pin-point exact version of SDK with [global.json](https://docs.microsoft.com/en-us/dotnet/core/tools/global-json) file in the root of your repo.

Build configuration (`Debug` or `Release`) can be specified as:

```yaml
configuration: Release
```

Similarly to patching version attributes in `AssemblyInfo.*` files AppVeyor for Linux can patch `<Version>` elements in `.csproj` files.
To enable `.csproj` file patching add:

```yaml
dotnet_csproj:
  patch: true
  file: '**\*.csproj'
  version: '{version}'
  package_version: '{version}'
  assembly_version: '{version}'
  file_version: '{version}'
  informational_version: '{version}'
```

> Note that `<Version>` and other version-related elements should exist already in `.csproj` file - AppVeyor won't add them for you.

AppVeyor provides automatic packaging for the following types of .NET Core projects:

* Any .NET Core project with "Generate NuGet package on build" option enabled on "Package" tab enabled.
* ASP.NET Core web application.
* .NET Core console application.

You can enable automatic packaging by adding this to `appveyor.yml`:

```yaml
build:
  project: MySolution.sln
  publish_nuget: true
  publish_aspnet_core: true
  publish_core_console: true
  verbosity: minimal
```

NuGet packages will be automatically published to [account and project NuGet feeds](https://www.appveyor.com/docs/nuget/#account-nuget-feed).

Both ASP.NET Core and .NET Core console projects will be published to `.zip` files and pushed to build artifacts. You can deploy them later with one of the supported deployment methods described below.

By default, if you omit `test` section in `appveyor.yml` AppVeyor will assume `Auto` mode working in pair with `MSBuild` build mode. In `Auto` mode AppVeyor will run tests against all test projects found in the solution.

## SQL Server 2017 for Linux

With AppVeyor for Linux you can test your ASP.NET Core applications on Linux platform with SQL Server 2017 for Linux.

To start SQL Server 2017 for Linux service add this to your `appveyor.yml`:

```yaml
services:
- mssql
```

SQL Server 2017 instance details:

* Host: `localhost`
* Password: `Password12!`

`sqlcmd` command line is available to execute commands against SQL Server instance. For example, to print SQL Server version use this:

```yaml
init:
- sqlcmd -S localhost -U SA -P Password12! -Q 'select @@VERSION'
```

## Accessing build VM via SSH

There is an article about how to [access Linux build worker via SSH](/docs/how-to/ssh-to-build-worker/).

## Cache

[Build cache](https://www.appveyor.com/docs/build-cache/) works the same as for Windows build workers.

## Artifacts

[Packaging artifacts](https://www.appveyor.com/docs/packaging-artifacts/) works the same as for Windows build workers.

## Deployment

AppVeyor Build Agent for Linux has [similar deployment functionality](https://www.appveyor.com/docs/deployment/) as on Windows.

The following deployment providers are currently supported by AppVeyor for Linux:

* [Environment](https://www.appveyor.com/docs/deployment/environment/)
* [NuGet](https://www.appveyor.com/docs/deployment/nuget/)
* [Azure Blob](https://www.appveyor.com/docs/deployment/azure-blob/)
* [Azure App Service Zip Push](https://www.appveyor.com/docs/deployment/azure-app-service-zip-push-deploy/)
* [Webhook](https://www.appveyor.com/docs/deployment/webhook/)
* [GitHub Releases](https://www.appveyor.com/docs/deployment/github/)
* [Bintray](https://www.appveyor.com/docs/deployment/bintray/)
* [Amazon S3](https://www.appveyor.com/docs/deployment/amazon-s3/)
* [FTP](https://www.appveyor.com/docs/deployment/ftp/)

## Pre-installed software

[Software pre-installed on Linux build VMs](/docs/linux-images-software/)