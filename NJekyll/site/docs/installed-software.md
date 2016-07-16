---
layout: docs
title: Build Worker Installed Software
---

# Build Worker installed software

Below is the list of software pre-installed on Build Worker.

<!--TOC-->

## Operating system

* Windows Server 2012 R2 (x64)

## Version control systems

* [Git](https://git-scm.com/download/win) 2.8.2 (x64) (with `git config --global core.autocrlf input`)
	* [Git Large File Storage](https://git-lfs.github.com/) 1.2.0
* [Mercurial](http://mercurial.selenic.com/downloads) 3.1.1 (x86)
* [Subversion](http://www.collab.net/downloads/subversion) 1.8.11 (x86)

## SDKs

* [Microsoft Windows SDK for Windows 7 and .NET Framework 3.5 SP1](https://www.microsoft.com/en-us/download/details.aspx?id=3138)
* [Microsoft Windows SDK for Windows 7 and .NET Framework 4](https://www.microsoft.com/en-us/download/details.aspx?id=8279)
* [Windows SDK for Windows 8](https://msdn.microsoft.com/en-us/library/windows/desktop/hh852363.aspx)
* [Windows SDK for Windows 8.1](https://msdn.microsoft.com/en-us/windows/desktop/bg162891.aspx)
* [Windows Driver Kit Version 7.1.0 (to support ATL)](https://www.microsoft.com/en-us/download/details.aspx?id=11800)
* [Windows Driver Kit 10](https://msdn.microsoft.com/en-us/windows/hardware/dn913721.aspx)
* [Microsoft Expression Blend Software Development Kit (SDK) for .NET 4](https://www.microsoft.com/en-us/download/details.aspx?id=10801)
* [Microsoft Expression Blend Software Development Kit (SDK) for Silverlight 4](https://www.microsoft.com/en-us/download/details.aspx?id=3062)
* [Windows Phone SDK 8.0](https://www.microsoft.com/en-us/download/details.aspx?id=35471)
* [Azure SDKs](https://azure.microsoft.com/en-us/downloads/archive-net-downloads/) 2.2, 2.3, 2.4, 2.5.1, 2.6, 2.7.1, 2.8.1, 2.9
* [Microsoft SilverLight 5 SDK](https://www.microsoft.com/en-us/download/details.aspx?id=28359)
* [Windows PowerShell 2.0 SDK](https://www.microsoft.com/en-ca/download/details.aspx?id=2560)
* [DirectX SDK](https://www.microsoft.com/en-us/download/details.aspx?id=6812) (`C:\Program Files (x86)\Microsoft DirectX SDK`)
* [AWS SDK .NET](https://aws.amazon.com/sdk-for-net/) v3.7.606.0
* [AWS CLI](https://docs.aws.amazon.com/cli/latest/userguide/installing.html#install-msi-on-windows) 1.7.25
* [TypeScript 1.4 for Visual Studio 2013](https://visualstudiogallery.msdn.microsoft.com/2d42d8dc-e085-45eb-a30b-3f7d50d55304)
* [TypeScript 1.5 for Visual Studio 2013](https://visualstudiogallery.msdn.microsoft.com/b1fff87e-d68b-4266-8bba-46fad76bbf22)
* TypeScript 1.6.3 for Visual Studio 2015
* TypeScript 1.7.6 for Visual Studio 2015
* TypeScript 1.8.3 for Visual Studio 2015
* [Office Developer Tools for Visual Studio 2013](https://blogs.msdn.com/b/visualstudio/archive/2014/03/03/announcing-office-developer-tools-for-visual-studio-2013-march-2014-update.aspx)
* [Office Developer Tools for Visual Studio 2015](https://www.visualstudio.com/en-us/features/office-tools-vs.aspx)
* [Microsoft SQL Server Data Tools - Business Intelligence for Visual Studio 2013](https://www.microsoft.com/en-ca/download/details.aspx?id=42313)

## Visual Studio

### Visual Studio 2008

* [Visual C++ 2008 Express](https://go.microsoft.com/?linkid=7729279)

### Visual Studio 2010

* [Visual C# 2010 Express](https://go.microsoft.com/?linkid=9709939)
* [Visual Basic 2010 Express](https://go.microsoft.com/?linkid=9709929)
* [Visual C++ 2010 Express](https://go.microsoft.com/?linkid=9709949)
* [Visual Web Developer 2010 Express](https://go.microsoft.com/fwlink/?LinkID=167874)
* [Visual Studio 2010 Service Pack 1](https://www.microsoft.com/en-us/download/details.aspx?id=23691)

### Visual Studio 2012

* [Visual Studio Express 2012 for Windows Desktop](https://www.microsoft.com/en-us/download/details.aspx?id=34673)
* [TypeScript for Visual Studio 2012](https://www.microsoft.com/en-us/download/details.aspx?id=34790)
* [Visual Studio 2012 Update 5](https://www.microsoft.com/en-ca/download/details.aspx?id=48708)

### Visual Studio 2013

* [Visual Studio Community 2013 with Update 5](http://www.visualstudio.com/products/visual-studio-community-vs)
* [Visual Studio 2013 SDK](http://www.visualstudio.com/downloads/download-visual-studio-vs)
* [Python Tools for Visual Studio 2013](https://github.com/Microsoft/PTVS/releases)
* [Node.js Tools for Visual Studio 2013](https://github.com/Microsoft/nodejstools#readme)

### Visual Studio 2015

* [Visual Studio Community 2015 with Update 3](https://www.visualstudio.com/en-us/downloads/download-visual-studio-vs.aspx)
* Universal Windows App Dev Tools for Visual Studio 2015
* Windows 10 SDK
* [Python Tools for Visual Studio 2015](https://github.com/Microsoft/PTVS/releases)
* [Node.js Tools for Visual Studio 2015](https://github.com/Microsoft/nodejstools#readme)
* [Visual F# Tools 4.0 RTM](https://www.microsoft.com/en-us/download/details.aspx?id=48179)
* [Visual Studio 2015 Installer Projects](https://visualstudiogallery.msdn.microsoft.com/f1cc3f3e-c300-40a7-8797-c509fb8933b9)
* ASP.NET and Web Tools 2015 (RC1 Update1)

Visual Studio Community 2015 RTM with Visual Studio 2015 SDK are installed on a separate build worker image called `Visual Studio 2015`. You can select build worker image in "OS" dropdown on Environment tab of project settings or if you use `appveyor.yml` add that line:

    os: Visual Studio 2015

## Xamarin

[Xamarin Platform](https://xamarin.com/platform) v4.1.1.3 is installed on `Visual Studio 2015` image.

## Languages, libraries, frameworks

* [C++ 11 CTP](https://blogs.msdn.microsoft.com/vcblog/2013/11/18/announcing-the-visual-c-compiler-november-2013-ctp/)
* .NET Framework 2.0, 3.0, 3.5, 4.0, 4.5.1, 4.5.2
* .NET Core 1.0
* [Microsoft .NET Framework 4.5.2 with Developer Pack](https://www.microsoft.com/en-ca/download/details.aspx?id=42637)
* [Visual F# 2.0](https://www.microsoft.com/en-us/download/details.aspx?id=18706)
* [Visual F# Out of Band Release 3.1.2](https://www.microsoft.com/en-us/download/details.aspx?id=44011)
* [Microsoft .NET Portable Library Reference Assemblies 4.6](https://www.microsoft.com/en-us/download/details.aspx?id=40727)
* [Microsoft Visual Studio Installer Projects](https://visualstudiogallery.msdn.microsoft.com/9abe329c-9bba-44a1-be59-0fbf6151054d) extension (`.vdproj` support).
* [WiX](http://wixtoolset.org/) 3.10
* [Silverlight 5 x64 Developer Runtime](https://go.microsoft.com/fwlink/?LinkID=229324)
* SQL Server Data tools for [Visual Studio 2012](https://msdn.microsoft.com/en-us/jj650015) and [2013](https://stackoverflow.com/questions/15556339/how-to-build-sqlproj-projects-on-a-build-server) with `SqlPackage.exe` utility in `C:\Program Files (x86)\Microsoft SQL Server\120\DAC\bin` folder.
* Boost:
    * 1.60.0 (`C:\Libraries\boost_1_60_0`)
	* 1.59.0 (`C:\Libraries\boost_1_59_0`)
	* 1.58.0 (`C:\Libraries\boost_1_58_0`)
	* 1.56.0 (`C:\Libraries\boost`)

### Node.js

`4.4.x` is default Node.js installed on build workers.

* 4.0.0 - 6.2.2 (x86 and x64)
* 0.10.26 - 0.10.44 (x86 and x64)
* 0.11.12 - 0.11.16 (x86 and x64)
* 0.12.0 - 0.12.13 (x86 and x64)
* 0.8.25 - 0.8.28 (x86 and x64)
* Use `LTS` alias for latest `4.x` release
* Use `Stable` alias for latest `5.x` release

Use the following PowerShell command to quickly switch Node.js version:

    Install-Product node <version> [x86|x64]

To switch to the latest `0.x.x` Node.js version (0.12.x) use this PowerShell command:

    Install-Product node 0

To switch to the latest `5.x.x` Node.js version use this PowerShell command:

    Install-Product node ''

### io.js

* 1.0.0 - 3.3.0 (x86 and x64)

Use the following PowerShell command to quickly switch io.js version:

    Install-Product node <version> [x86|x64]
    
To switch to the latest io.js version using this PowerShell command:

    Install-Product node '3'

### Go

* [Go](https://golang.org/dl/)
    * 1.6.2 x64 (`C:\go` - default in `PATH`)
    * 1.6.2 x86 (`C:\go-x86`)
    * 1.6.2 x64 (`C:\go16`)
    * 1.6.2 x86 (`C:\go16-x86`)    
    * 1.5.4 x64 (`C:\go15`)
    * 1.5.4 x86 (`C:\go15-x86`)
    * 1.4.3 x64 (`C:\go14`)
    * 1.4.3 x86 (`C:\go14-x86`)

### Java

* Java SE Development Kit (JDK)
    * [JDK 1.6](http://www.oracle.com/technetwork/java/javase/downloads/java-archive-downloads-javase6-419409.html) Update 45 (x64) (`C:\Program Files\Java\jdk1.6.0\bin` - default in `PATH`)
    * [JDK 1.6](http://www.oracle.com/technetwork/java/javase/downloads/java-archive-downloads-javase6-419409.html) Update 45 (x86) (`C:\Program Files (x86)\Java\jdk1.6.0\bin`)
    * [JDK 1.7](http://www.oracle.com/technetwork/java/javase/downloads/jdk7-downloads-1880260.html) Update 79 (x64) (`C:\Program Files\Java\jdk1.7.0\bin` - default in `PATH`)
    * [JDK 1.7](http://www.oracle.com/technetwork/java/javase/downloads/jdk7-downloads-1880260.html) Update 79 (x86) (`C:\Program Files (x86)\Java\jdk1.7.0\bin`)
    * [JDK 1.8](http://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html) Update 92 (x64) (`C:\Program Files\Java\jdk1.8.0`)
    * [JDK 1.8](http://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html) Update 92 (x86) (`C:\Program Files (x86)\Java\jdk1.8.0`)

### Mono

* [Mono](http://www.mono-project.com/download/) 4.0.2 SR2

### Ruby

* [Ruby](http://rubyinstaller.org/downloads/) with [DevKit](https://github.com/oneclick/rubyinstaller/wiki/Development-Kit)
    * 1.9.3-p551 (`C:\Ruby193\bin` - default in `PATH`) with RubyGems 2.6.3
    * 2.0.0-p648 x86 (`C:\Ruby200\bin`) with RubyGems 2.6.3
    * 2.0.0-p648 x64 (`C:\Ruby200-x64\bin`) with RubyGems 2.6.3
    * 2.1.8 x86 (`C:\Ruby21\bin`) with RubyGems 2.6.3
    * 2.1.8 x64 (`C:\Ruby21-x64\bin`) with RubyGems 2.6.3
    * 2.2.4 x86 (`C:\Ruby22\bin`) with RubyGems 2.6.3
    * 2.2.4 x64 (`C:\Ruby22-x64\bin`) with RubyGems 2.6.3
    * 2.3.0 x86 (`C:\Ruby23\bin`) with RubyGems 2.6.3
    * 2.3.0 x64 (`C:\Ruby23-x64\bin`) with RubyGems 2.6.3   
    * `Bundler 1.11.2` is installed to all Ruby versions

### Python

* [Python](https://www.python.org/downloads/windows/)
    * 2.6.6 x86 (`C:\Python26`)
    * 2.6.6 x64 (`C:\Python26-x64`)
    * 2.7.11 x86 (`C:\Python27` - default in `PATH`)
    * 2.7.11 x64 (`C:\Python27-x64`)
    * 3.3.5 x86 (`C:\Python33`)
    * 3.3.5 x64 (`C:\Python33-x64`)
    * 3.4.3 x86 (`C:\Python34`)
    * 3.4.3 x64 (`C:\Python34-x64`)
    * 3.5.1 x86 (`C:\Python35`)
    * 3.5.1 x64 (`C:\Python35-x64`)
* Miniconda
	* Miniconda2 4.0.5 (Python 2.7.11) - `C:\Miniconda`
	* Miniconda2 4.0.5 x64 (Python 2.7.11): `C:\Miniconda-x64`
	* Miniconda3 3.16.0 (Python 3.4.3): `C:\Miniconda3`
	* Miniconda3 3.16.0 x64 (Python 3.4.3): `C:\Miniconda3-x64`
	* Miniconda3 4.0.5 (Python 3.5.1): `C:\Miniconda35`
	* Miniconda3 4.0.5 x64 (Python 3.5.1): `C:\Miniconda35-x64`
* [Visual C++ Compiler for Python 2.7](https://www.microsoft.com/en-us/download/details.aspx?id=44266)
    
### Perl

* [Perl](https://www.activestate.com/activeperl/downloads) 5.20.1.2000 x86 (`C:\Perl` in `PATH`)

### Erlang

* `Erlang OTP runtime 17.4 x64` installed into `C:\Program Files\erl6.3`

### LLVM

* [LLVM 3.8.0](http://llvm.org/releases/download.html#3.8.0) x64 (`C:\Program Files\LLVM\bin` in `PATH`)

### MinGW, MSYS, Cygwin

* [MinGW/MSYS 4.8.2 32-bit](http://www.mingw.org/) (core components and compilers - `C:\MinGW`)
	* MinGW root directory: `C:\MinGW`
	* MinGW bin directory: `C:\MinGW\bin`
	* MSYS root directory: `C:\MinGW\msys\1.0`
* MinGW-w64 5.3.0 32-bit:
	* MinGW root directory: `C:\mingw-w64\i686-5.3.0-posix-dwarf-rt_v4-rev0`
	* MinGW bin directory: `C:\mingw-w64\i686-5.3.0-posix-dwarf-rt_v4-rev0\mingw32\bin`
* Cygwin (`C:\cygwin`)
* Cygwin 64 (`C:\cygwin64`)
* MSYS2 (`C:\msys64`)

### Qt

* Qt (`C:\Qt`)
	- Qt 5.7: `C:\Qt\5.7`
	  - MinGW 5.3.0 32 bit: `C:\Qt\5.7\mingw53_32`
	  - msvc2015 64-bit: `C:\Qt\5.7\msvc2015_64`
	  - msvc2015 32-bit: `C:\Qt\5.7\msvc2015`      
	  - msvc2013 64-bit: `C:\Qt\5.7\msvc2013_64`
	  - msvc2013 32-bit: `C:\Qt\5.7\msvc2013`
	- Qt 5.6.1: `C:\Qt\5.6`
	  - MinGW 4.9.2 32 bit: `C:\Qt\5.6\mingw49_32`
	  - msvc2015 64-bit: `C:\Qt\5.6\msvc2015_64`
	  - msvc2015 32-bit: `C:\Qt\5.6\msvc2015`      
	  - msvc2013 64-bit: `C:\Qt\5.6\msvc2013_64`
	  - msvc2013 32-bit: `C:\Qt\5.6\msvc2013`
	- Qt 5.5: `C:\Qt\5.5`
	  - MinGW 4.9.2 32 bit: `C:\Qt\5.5\mingw492_32`
	  - msvc2013 64-bit: `C:\Qt\5.5\msvc2013_64`
	  - msvc2013 32-bit: `C:\Qt\5.5\msvc2013`
	- Qt 5.4: `C:\Qt\5.4`
	  - MinGW 4.9.1 (32 bit) OpenGL: `C:\Qt\5.4\mingw491_32`
	  - msvc2013 64-bit OpenGL: `C:\Qt\5.4\msvc2013_64_opengl`
	  - msvc2013 32-bit OpenGL: `C:\Qt\5.4\msvc2013_opengl`
	- Qt 5.3: `C:\Qt\5.3`
	  - MinGW 4.8.2 (32 bit): `C:\Qt\5.3\mingw482_32`
	  - msvc2013 64-bit OpenGL: `C:\Qt\5.3\msvc2013_64_opengl`
	  - msvc2013 32-bit OpenGL: `C:\Qt\5.3\msvc2013_opengl`
	- Tools
      - MinGW 5.3.0: `C:\Qt\Tools\mingw530_32`
	  - MinGW 4.8.2: `C:\Qt\Tools\mingw482_32`
	  - MinGW 4.9.1: `C:\Qt\Tools\mingw491_32`
	  - MinGW 4.9.2: `C:\Qt\Tools\mingw492_32`
* [Qt Installer Framework 2.0.1](https://download.qt.io/official_releases/qt-installer-framework/2.0.1/) 

## Tools

* [7-Zip](http://www.7-zip.org/) 15.14
* [Microsoft Azure PowerShell](https://github.com/Azure/azure-powershell/releases) 1.2.1 (February 2016)
* [Microsoft Azure CLI](https://azure.microsoft.com/en-us/downloads/) 0.9.10
* [CMake](https://cmake.org/download/) 3.5.2
* [NuGet](https://dist.nuget.org/index.html) 2.8.6 on `Visual Studio 2013` image
* [NuGet](https://dist.nuget.org/index.html) 3.4.4 on `Visual Studio 2015` image
* [Chocolatey](https://chocolatey.org/) v0.9.9.8
* [GitVersion](https://www.nuget.org/packages/GitVersion.CommandLine) 3.0.2
* FxCop 10.0 (`C:\Program Files (x86)\Microsoft Fxcop 10.0`)
* [OpenSSL](https://slproweb.com/products/Win32OpenSSL.html) 1.0.2h (32-bit) (`C:\OpenSSL-Win32\bin`)
* [OpenSSL](https://slproweb.com/products/Win32OpenSSL.html) 1.0.2h (64-bit) (`C:\OpenSSL-Win64\bin`)
* [NSIS](http://nsis.sourceforge.net/Download) 2.50 (`C:\Program Files (x86)\NSIS`)

## Testing

### Runners

* [NUnit](http://nunit.org/index.php?p=download) 2.6.4 in `C:\Tools\NUnit\bin`
* [NUnit3](https://github.com/nunit/nunit) 3.2.1 in `C:\Tools\NUnit3\bin`
* [xUnit](https://github.com/xunit/xunit/releases) 1.9.2 in `C:\Tools\xUnit`
* [xUnit](https://www.nuget.org/packages/xunit.runner.console/2.0.0) 2.0.0 RTM in `C:\Tools\xUnit20`
* [Machine.Specifications (MSpec)](http://www.nuget.org/packages/Machine.Specifications)

### Selenium

* [Chrome Web Driver](https://chromedriver.storage.googleapis.com/index.html) 2.22
* [Internet Explorer Web Driver](https://selenium-release.storage.googleapis.com/index.html) 2.53
* [FireFox Web Driver (geckodriver)](https://github.com/mozilla/geckodriver/releases) 0.8.0
* Internet Explorer 11
* FireFox 47.0.1
* Chrome 51.0


## Services and databases

* [SQL Server 2008 R2 SP2 Express Edition with Advanced Services (x64)](https://www.microsoft.com/en-US/download/details.aspx?id=30438)
* [SQL Server 2012 SP1 Express with Advanced Services](https://www.microsoft.com/en-us/download/details.aspx?id=35579)
* [SQL Server 2014 Express with Advanced Services](https://www.microsoft.com/en-us/download/details.aspx?id=42299)
* [SQL Server 2016 Express](https://www.microsoft.com/en-us/download/details.aspx?id=52679)
* [PostgreSQL 9.3 x64](http://www.enterprisedb.com/products-services-training/pgdownload#windows)
* [PostgreSQL 9.4 x64](http://www.enterprisedb.com/products-services-training/pgdownload#windows)
* [PostgreSQL 9.5 x64](http://www.enterprisedb.com/products-services-training/pgdownload#windows)
* [MySQL 5.7](https://dev.mysql.com/downloads/windows/installer/5.7.html)
* [MongoDB 3.0.4](https://www.mongodb.org/downloads)
* Internet Information Services (IIS) 8.5
* MSMQ

## Miscellaneous

* [Code Contracts for .NET](https://visualstudiogallery.msdn.microsoft.com/1ec7db13-3363-46c9-851f-1ce455f66970) 1.9.10714.2
* WinRM client hosts set to `*`

## Getting the list of installed software

You can use the following PowerShell code to get the full list of software installed on build worker:

    $x64items = @(Get-ChildItem "HKLM:SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall")
    $x64items + @(Get-ChildItem "HKLM:SOFTWARE\wow6432node\Microsoft\Windows\CurrentVersion\Uninstall") `
       | ForEach-object { Get-ItemProperty Microsoft.PowerShell.Core\Registry::$_ } `
       | Sort-Object -Property DisplayName `
       | Select-Object -Property DisplayName,DisplayVersion
