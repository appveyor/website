---
layout: docs
title: Build Worker Installed Software
---

# Build Worker installed software

Below is the list of software pre-installed on Build Worker.

## Operating system

* Windows Server 2012 R2 (x64)

## Version control systems

* [Git](http://git-scm.com/download/win) 1.9.5 (x86) (with `git config --global core.autocrlf input`)
* [Mercurial](http://mercurial.selenic.com/downloads) 3.1.1 (x86)

## SDKs

* [Microsoft Windows SDK for Windows 7 and .NET Framework 3.5 SP1](http://www.microsoft.com/en-us/download/details.aspx?id=3138)
* [Microsoft Windows SDK for Windows 7 and .NET Framework 4](http://www.microsoft.com/en-us/download/details.aspx?id=8279)
* [Windows SDK for Windows 8](http://msdn.microsoft.com/en-us/library/windows/desktop/hh852363.aspx)
* [Windows SDK for Windows 8.1](http://msdn.microsoft.com/en-us/windows/desktop/bg162891.aspx)
* [Windows Driver Kit Version 7.1.0 (to support ATL)](http://www.microsoft.com/en-us/download/details.aspx?id=11800)
* [Microsoft Expression Blend Software Development Kit (SDK) for .NET 4](http://www.microsoft.com/en-us/download/details.aspx?id=10801)
* [Microsoft Expression Blend Software Development Kit (SDK) for Silverlight 4](http://www.microsoft.com/en-us/download/details.aspx?id=3062)
* [Windows Phone SDK 8.0](http://www.microsoft.com/en-us/download/details.aspx?id=35471)
* [Azure SDKs](http://azure.microsoft.com/en-us/downloads/archive-net-downloads/) 2.2, 2.3, 2.4
* [AWS SDK .NET](http://aws.amazon.com/sdk-for-net/)
* [Microsoft SilverLight 5 SDK](http://www.microsoft.com/en-us/download/details.aspx?id=28359)
* [Windows PowerShell 2.0 SDK](http://www.microsoft.com/en-ca/download/details.aspx?id=2560)
* [DirectX SDK](http://www.microsoft.com/en-us/download/details.aspx?id=6812) (`C:\Program Files (x86)\Microsoft DirectX SDK`)


## Visual Studio 2008

* [Visual C++ 2008 Express](http://go.microsoft.com/?linkid=7729279)

## Visual Studio 2010

* [Visual C# 2010 Express](http://go.microsoft.com/?linkid=9709939)
* [Visual Basic 2010 Express](http://go.microsoft.com/?linkid=9709929)
* [Visual C++ 2010 Express](http://go.microsoft.com/?linkid=9709949)
* [Visual Web Developer 2010 Express](http://go.microsoft.com/fwlink/?LinkID=167874)
* [Visual Studio 2010 Service Pack 1](http://www.microsoft.com/en-us/download/details.aspx?id=23691)

## Visual Studio 2012

* [Visual Studio Express 2012 for Windows Desktop](http://www.microsoft.com/en-us/download/details.aspx?id=34673)
* [Visual Studio Express 2012 for Web](http://www.microsoft.com/en-us/download/details.aspx?id=30669)
* [Visual Studio Express 2012 for Windows 8](http://www.microsoft.com/en-us/download/details.aspx?id=30664)
* [TypeScript for Visual Studio 2012](http://www.microsoft.com/en-us/download/details.aspx?id=34790)

## Visual Studio 2013

* [Visual Studio Express 2013 for Windows Desktop with Update 3](http://www.microsoft.com/en-us/download/details.aspx?id=43733)
* [Visual Studio Express 2013 for Windows with Update 3](http://www.microsoft.com/en-us/download/details.aspx?id=43729)
* [Visual Studio Express 2013 for Web with Update 3](http://www.microsoft.com/en-us/download/details.aspx?id=43722 )

## Languages, libraries, frameworks

* [C++ 11 CTP](http://blogs.msdn.com/b/vcblog/archive/2013/11/18/announcing-the-visual-c-compiler-november-2013-ctp.aspx)
* .NET Framework 2.0, 3.0, 3.5, 4.0, 4.5.1, 4.5.2
* [Microsoft .NET Framework 4.5.2 with Developer Pack](http://www.microsoft.com/en-ca/download/details.aspx?id=42637)
* [Visual F# Out of Band Release 3.1.2](http://www.microsoft.com/en-us/download/details.aspx?id=44011)
* [Microsoft .NET Portable Library Reference Assemblies 4.6](http://www.microsoft.com/en-us/download/details.aspx?id=40727)
* [WiX](http://wixtoolset.org/) 3.8
* [Silverlight 5 x64 Developer Runtime](http://go.microsoft.com/fwlink/?LinkID=229324)
* SQL Server Data tools for [Visual Studio 2012](http://msdn.microsoft.com/en-us/jj650015) and [2013](http://stackoverflow.com/questions/15556339/how-to-build-sqlproj-projects-on-a-build-server) with `SqlPackage.exe` utility in `C:\Program Files (x86)\Microsoft SQL Server\120\DAC\bin` folder.
* [Node.js](http://nodejs.org/dist/) 0.10.30 x86:
    * 0.10.26 - 32 (x86 and x64)
    * 0.11.12 - 13 (x86 and x64)
    * 0.8.25 - 28 (x86 and x64)
    * Use the following PowerShell command to quickly switch Node.js version: `Install-Product node <version> [x86|x64]`
    * Active default version in `PATH` is 0.10.32
* [Go](http://golang.org/dl/) 1.3 x64
* Java SE Development Kit (JDK)
    * [JDK 1.7](http://www.oracle.com/technetwork/java/javase/downloads/jdk7-downloads-1880260.html) x64 (`C:\Program Files\Java\jdk1.7.0\bin` - default in `PATH`)
    * [JDK 1.7](http://www.oracle.com/technetwork/java/javase/downloads/jdk7-downloads-1880260.html) x86 (`C:\Program Files (x86)\Java\jdk1.7.0\bin`)
    * [JDK 1.8](http://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html) x64 (`C:\Program Files\Java\jdk1.8.0`)
    * [JDK 1.8](http://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html) x86 (`C:\Program Files (x86)\Java\jdk1.8.0`)
* [Mono](http://www.go-mono.com/mono-downloads/download.html) 3.2.3
* [Ruby](http://rubyinstaller.org/downloads/) with [DevKit](http://github.com/oneclick/rubyinstaller/wiki/Development-Kit)
    * 1.9.3 x86 (`C:\Ruby193` - default in `PATH`)
    * 2.0.0 x86 (`C:\Ruby200`)
    * 2.0.0 x64 (`C:\Ruby200-x64`)
    * 2.1.3 x86 (`C:\Ruby21`)
    * 2.1.3 x64 (`C:\Ruby21-x64`)
* [Python](https://www.python.org/downloads/windows/)
    * 2.7.8 x86 (`C:\Python27` - default in `PATH`)
    * 2.7.8 x64 (`C:\Python27-x64`)
    * 3.3.5 x86 (`C:\Python33`)
    * 3.3.5 x64 (`C:\Python33-x64`)
    * 3.4.1 x86 (`C:\Python34`)
    * 3.4.1 x64 (`C:\Python34-x64`)
* [MinGW](http://www.mingw.org/) (core components and compilers - `C:\MinGW`)

## Tools

* [Windows Azure PowerShell](https://github.com/Azure/azure-sdk-tools/releases) 0.8.7.1 (August 2014)
* [Windows Azure CLI](http://azure.microsoft.com/en-us/downloads/)
* [CMake](http://www.cmake.org/cmake/resources/software.html) 2.8
* [NuGet](http://docs.nuget.org/docs/start-here/installing-nuget) 2.8.3
* [Chocolatey](http://chocolatey.org/) v0.9.8.27
* [GitVersion](http://chocolatey.org/packages/GitVersion.Portable) 2.0.0
* FxCop 10.0 (`C:\Program Files (x86)\Microsoft Fxcop 10.0`)

## Testing

* [NUnit](http://nunit.org/index.php?p=download) 2.6.3
* [xUnit](https://github.com/xunit/xunit/releases) 1.9.2
* [xUnit](https://github.com/xunit/xunit/releases) 2.0.0
* [Machine.Specifications (MSpec)](http://www.nuget.org/packages/Machine.Specifications)

## Selenium browser testing

* [Chrome Web Driver](http://chromedriver.storage.googleapis.com/index.html) 2.10
* [Internet Explorer Web Driver](http://selenium-release.storage.googleapis.com/index.html) 2.42
* Internet Explorer 11
* FireFox 30.0 x86
* Chrome 36.0.1985.125


## Services and databases

* [SQL Server 2008 R2 SP2 Express Edition with Advanced Services (x64)](http://www.microsoft.com/en-US/download/details.aspx?id=30438)
* [SQL Server 2012 SP1 Express with Advanced Services](http://www.microsoft.com/en-us/download/details.aspx?id=35579)
* [SQL Server 2014 Express with Advanced Services](http://www.microsoft.com/en-us/download/details.aspx?id=42299)
* [PostgreSQL 9.3 x64](http://www.enterprisedb.com/products-services-training/pgdownload#windows)
* [MySQL 5.6 x64](http://dev.mysql.com/downloads/windows/installer/5.6.html)
* Internet Information Services (IIS) 8.5
* MSMQ

## Miscellaneous

* [7-Zip](http://www.7-zip.org/) 9.20
* [Code Contracts for .NET](http://visualstudiogallery.msdn.microsoft.com/1ec7db13-3363-46c9-851f-1ce455f66970)
* WinRM client hosts set to `*`

## Getting the list of installed software

You can use the following PowerShell code to get the full list of software installed on build worker:

    $x64items = @(Get-ChildItem "HKLM:SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall")
    $x64items + @(Get-ChildItem "HKLM:SOFTWARE\wow6432node\Microsoft\Windows\CurrentVersion\Uninstall") `
       | ForEach-object { Get-ItemProperty Microsoft.PowerShell.Core\Registry::$_ } `
       | Sort-Object -Property DisplayName `
       | Select-Object -Property DisplayName,DisplayVersion
