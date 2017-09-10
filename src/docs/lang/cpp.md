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
and [LINK enviornment](https://msdn.microsoft.com/en-us/library/6y6t9esh.aspx)
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

If you are building with custom rules which may cause `DefaultPlatformToolset` to be undefined, then you can use the following to ensure `PlatformToolset` has a minumum value.

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
