---
layout: docs
title: Testing with C/C++
---

<!-- markdownlint-disable MD022 MD032 -->
# Building C/C++ projects
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Introduction

AppVeyor provides several [build worker images](/docs/build-environment/#build-worker-images) with Visual Studio.

Additionally, LLVM/Clang, MinGW and MinGW-w64 compiler infrastructures are also available.

## Visual Studio

Build configured with standard Visual Studio projects does not require any extra steps to setup build environment.

Build configured with makefiles or any scripts directly invoking compiler executable require enabling the Visual C++ toolset for the command-line builds.

Run the following in the `appveyor.yml`:


### Visual Studio 2022

* For 32-bit target

    ```bat
    call "C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Auxiliary\Build\vcvars32.bat"
    ```

* For 64-bit target

    ```bat
    call "C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Auxiliary\Build\vcvars64.bat"
    ```

### Visual Studio 2019

* For 32-bit target

    ```bat
    call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\VC\Auxiliary\Build\vcvars32.bat"
    ```

* For 64-bit target

    ```bat
    call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\VC\Auxiliary\Build\vcvars64.bat"
    ```

### Visual Studio 2017

* For 32-bit target

    ```bat
    call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvars32.bat"
    ```

* For 64-bit target

    ```bat
    call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvars64.bat"
    ```

### Visual Studio 2015

* For 32-bit target

    ```bat
    call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\vcvarsall.bat" x86
    ```

* For 64-bit target

    ```bat
    call "C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\SetEnv.cmd" /x64
    call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\vcvarsall.bat" x86_amd64
    ```

Now, Visual C++ tools (`cl.exe`, `link.exe` and others) should be available in the `PATH` and
required variables for
[CL environment](https://msdn.microsoft.com/en-us/library/kezkeayy.aspx)
and [LINK environment](https://msdn.microsoft.com/en-us/library/6y6t9esh.aspx)
set properly for command-line builds.

## PlatformToolset

Visual Studio project select a toolchain based on the `<PlatformToolset />` element. To avoid potential problems project files should use the variable `DefaultPlatformToolset` as the value. Hardcoded values, like `v100` (Visual Studio 2010) and `v110` (Visual Studio 2012), can cause problems on some build worker images.

The `PlatformToolset` value should be set after `Microsoft.Cpp.Default.props` properties are imported. An example is shown below.

```xml
<Import Project="$(VCTargetsPath)\Microsoft.Cpp.Default.props" />
<PropertyGroup Label="PlatformToolset">
  <PlatformToolset>$(DefaultPlatformToolset)</PlatformToolset>
</PropertyGroup>
```

If you are building with custom rules which may cause `DefaultPlatformToolset` to be undefined, then you can use the following to ensure `PlatformToolset` has a minimum value.

```xml
<!-- Use DefaultPlatformToolset after Microsoft.Cpp.Default.props -->
<Import Project="$(VCTargetsPath)\Microsoft.Cpp.Default.props" />

<!-- Set DefaultPlatformToolset to v100 (VS2010) if not defined -->
<PropertyGroup Label="EmptyDefaultPlatformToolset">
    <DefaultPlatformToolset Condition=" '$(DefaultPlatformToolset)' == '' ">v100</DefaultPlatformToolset>
</PropertyGroup>

<!-- Use DefaultPlatformToolset to set PlatformToolset -->
<PropertyGroup Label="PlatformToolset">
    <PlatformToolset>$(DefaultPlatformToolset)</PlatformToolset>
</PropertyGroup>
```

### VC++ Packaging Tool

Vcpkg is a package manager that helps to acquire, build and install C/C++ open source dependencies for C/C++ projects built with Visual Studio 2017 or Visual Studio 2015 Update 3.

AppVeyor comes with [VC++ Packaging Tool](https://github.com/Microsoft/vcpkg) pre-installed in `C:\tools\vcpkg` folder (without integration installed by default).

In case newer (than current AppVeyor build worker image contains) **VC++ Packaging Tool** required, it can be installed on-the-fly during `install` stage. For that add the following to the YAML (or similar to UI **Environment** tab if you do not use YAML):

```yaml
install:
- cd C:\Tools\vcpkg
- git pull
- .\bootstrap-vcpkg.bat
- cd %APPVEYOR_BUILD_FOLDER%
```

For example, if a project built with CMake requires SQLite 3 library as a dependency:

* Install required package for both target platforms

```bat
vcpkg install sqlite3:x86-windows
vcpkg install sqlite3:x64-windows
```

* Enable user-wide integration for Visual Studio/MSBuild projects:

```bat
cd c:\tools\vcpkg
vcpkg integrate install
```

* Enable integration for CMake (toolchain file):

```bat
cmake -DCMAKE_TOOLCHAIN_FILE=c:/tools/vcpkg/scripts/buildsystems/vcpkg.cmake ...
```

### Cache installed packages

To enable faster (cached) rebuilds add the following to the cache section in the `appveyor.yml`:

```yaml
cache:
- c:\tools\vcpkg\installed\
```

Read more about using Vcpkg in its [documentation](https://vcpkg.readthedocs.io).
