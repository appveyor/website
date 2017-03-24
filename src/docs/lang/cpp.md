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