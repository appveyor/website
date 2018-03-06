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
    * Bitbucket.com Cloud and Bitbucket Server
    * GitLab
    * VSTS (both Git and TFVC repositories)
    * Kiln
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

> AppVeyor for Linux is currently in private beta. Both paid and free AppVeyor accounts are entitled to participate in the beta. Please [contact us by email](mailto:team@appveyor.com) or [post a message on forums](https://help.appveyor.com) to enable Linux image for your account.

During the beta we provide a single build image called `ubuntu` with **Ubuntu Server 16.04.4 LTS** installed. In the section below you can find a full list of software pre-installed on the image.

> Once AppVeyor for Linux is generally available the configuration of build worker VMs can change.

To run your build on `ubuntu` image either add the following line to your `appveyor.yml`:

    image: ubuntu

or if you don't use `appveyor.yml` select `ubuntu` image on **Environment** tab of AppVeyor project settings.

> You can also put project configuration in `.appveyor.yml` dot-file in the root of your repo. AppVeyor searches repo for `appveyor.yml` first and then for `.appveyor.yml`.

When a new build is enqueued for `ubuntu` image it will take around 2-3 minutes to provision and boot a fresh VM for your build as during the beta `ubuntu` builds are running on Google Compute Cloud with no pre-heated instances. Please check [Build environment](https://www.appveyor.com/docs/build-environment/#build-clouds) article for more information about build clouds and their characteristics.

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
image: ubuntu

install:
- npm install

test_script:
- npm test

build: off
```

> `build` phase should be off as, by default, it's set to `MSBuild` mode for automatic discovery and building of .NET Core projects (see the section below).

### Bash and PowerShell

You can use both *Bash* and *PowerShell* commands simultanously to control the build flow.

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

> Environment variable names are cass-sensitive on Linux platform.

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

Test phase could be either your own scrips, be enabled to discover and test .NET COre project(s) or be turned off.

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
      Weite-Host "This is NOT a Linux!"
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

By default, if you ommit `test` section in `appveyor.yml` AppVeyor will assume `Auto` mode working in pair with `MSBuild` build mode. In `Auto` mode AppVeyor will run tests against all test projects found in the solution.

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

The following software is pre-installed on `Ubuntu` image:

* Operating system
    * Ubuntu Server 16.04.4 LTS
* Source control systems
    * git 2.15.1
    * git LFS 2.3.4
    * mercurial 4.4.1
    * subversion 1.9.3
* Node.js
    * Node.JS (system) 6.13.0
    * Nodejs v4.8.7
    * Nodejs v5.12.0
    * Nodejs v6.13.0
    * Nodejs v7.10.1
    * Nodejs v8.9.4
    * Nodejs v9.5.0
    * NVM
* PowerShell
    * PowerShell Core 6.0.1
* .NET Core
    * .NET Core Runtime 2.0.0
    * .NET Core Runtime 2.0.3
    * .NET Core Runtime 2.0.4
    * .NET Core Runtime 2.0.5
    * .NET Core 2.0.0 SDK
    * .NET Core 2.0.2 SDK
    * .NET Core 2.0.3 SDK
    * .NET Core 2.1.2 SDK
    * .NET Core 2.1.3 SDK
    * .NET Core 2.1.4 SDK
* Mono
    * Mono 5.10
* Ruby
    * Ruby 2.0
    * Ruby 2.1
    * Ruby 2.2
    * Ruby 2.3
    * Ruby 2.4
    * Ruby 2.5.0
    * Ruby HEAD 2.6.0dev
    * RVM 1.29.3
* Docker
    * Docker 17.12.0
* Python
    * Python 2.7.12-1
    * Python 3.5.1-2
    * virtualenv 15.0.1
* Java
    * openJDK 7 1.7.0_95
    * openJDK 8 1.8.0_151
    * openJDK 9 9-internal
* Go (Golang)
    * Go 1.7
    * Go 1.8
    * Go 1.9
    * Go 1.10
    * Go Version Manager v1.0.22
* Databases and Services
    * MySQL 5.7.21
    * PostgreSQL 10+190
    * SQL Server 2017 14.0.3022.28-2
    * MongoDB 3.2.19
    * Redis 4.0.8
    * RabbitMQ 3.6.15-1
* Erlang
    * Erlang
* Tools
    * yarn 1.5.1
    * p7zip 16.02
    * ant 1.9.6
    * gcc 4:5.3.1
    * gradle 2.10-1
    * make 4.1-6
    * maven 3.3.9-3
    * tcl 8.6.0+9
    * wget 1.17.1
    * curl 7.47.0