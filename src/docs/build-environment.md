---
layout: docs
title: Build Environment
---

<!-- markdownlint-disable MD022 MD032 -->
# Build environment
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

Every build runs on a fresh virtual machine which is not shared with other builds and the state of which is not preserved between consequent builds. After the build is finished its virtual machine is decommissioned.

## Build clouds

AppVeyor runs builds on two build clouds:

* Hyper-V
* Google Compute Engine (GCE)

### Hyper-V

Hyper-V cloud is a primary build cloud hosted in RackSpace's San Antonio, TX data center.
It is a pool of pre-heated virtual machines, so, generally, builds start faster on Hyper-V cloud.

### Google Compute Engine

Google Compute Engine (GCE) cloud is a secondary build cloud running in Google Cloud "Central US" zone.
Builds are routed to GCE cloud when they use a custom build worker image not available on Hyper-V cloud.
GCE cloud is also used as a failover solution for Hyper-V cloud.

It usually takes a build around 3-4 minutes to start on GCE environment as this is the time required to provision and boot up build virtual machine.

### Private build cloud

There might be build scenarios that cannot be covered by AppVeyor build workers. For example, some proprietary software should be pre-installed to support your builds
or you need more powerful build VMs or private network access.

AppVeyor allows running builds on your own cloud:

* Hyper-V server (on-premise or hosted)
* Docker server (on-premise or hosted)
* Azure virtual machines
* Amazon Web Services (AWS)
* Google Compute Engine (GCE)

In this scenario AppVeyor service provides UI, orchestration, artifacts storage and NuGet feeds while AppVeyor build agents run on your virtual machines.
Private build clouds are available to customers with [Premium plan](/pricing/) and can be enabled upon request. Please [let us know](mailto:team@appveyor.com) if you are interested.

## Build VM configurations

<table>
  <tr>
    <th>Build cloud / configuration</th>
    <th>CPU</th>
    <th>Memory</th>
  </tr>
  <tr>
    <td>Hyper-V</td>
    <td>2 cores</td>
    <td>4 GB</td>
  </tr>
  <tr>
    <td>GCE</td>
    <td>2 cores</td>
    <td>7.5 GB</td>
  </tr>
</table>

## IP addresses

IP addresses assigned to build workers:

    74.205.54.20
    104.197.110.30
    104.197.145.181
    146.148.85.29
    67.225.139.254
    67.225.138.82
    67.225.139.144

IP address of AppVeyor workers (when deploying from "Environments"):

    138.91.141.243

## Build worker images

*Build worker image* is a template used to provision a virtual machine for your build. Physical implementation of the template depends on the build cloud platform
and can be a master VHD for Hyper-V and Azure, snapshot or image for GCE or AWS.

AppVeyor provides these "standard" build worker images:

* `Visual Studio 2013`
* `Visual Studio 2015`
* `Visual Studio 2017`

Below you can find the list of [pre-installed software](#pre-installed-software) for each image.

### Visual Studio Preview images

AppVeyor also provides a build image which contains, in place of the Visual Studio 2017 version on the current image, the VS2017 preview relative to that version. 

* `Visual Studio 2017 Preview`

The aim is to stay in sync with the [release rhythm](https://docs.microsoft.com/en-us/visualstudio/productinfo/vs2017-release-rhythm#previews) of VS2017.

## Choosing image for your builds

If the build configuration does not specify build worker image then `Visual Studio 2015` image is used.

You can select a different image on AppVeyor UI ("Environment" tab of project settings) or in `appveyor.yml`:

    image: Visual Studio 2017

> Please note that `appveyor.yml` has [precedence over UI settings](/docs/build-configuration/#appveyoryml-and-ui-coexistence),
> so when `appveyor.yml` exists the image should be specified in YAML, not on UI.

## Using multiple images for the same build

`image` is first class dimension for [build matrix](/docs/build-configuration/#build-matrix), therefore the following YAML configuration will work (and will create 4 build jobs):

```yaml
image:
- Visual Studio 2015
- Visual Studio 2017
environment:
  matrix:
    - MY_VAR: value1
    - MY_VAR: value2
```

Also for some combinations it makes sense to use `APPVEYOR_BUILD_WORKER_IMAGE` "tweak" environment variable, so this configuration will also work (and will create 2 build jobs):

```yaml
environment:
  matrix:
    - APPVEYOR_BUILD_WORKER_IMAGE: Visual Studio 2015
      MY_VAR: value1
    - APPVEYOR_BUILD_WORKER_IMAGE: Visual Studio 2017
      MY_VAR: value2
```

## Image updates

AppVeyor team regularly (once in 2-3 weeks) updates build worker images by installing new or updating existing software.

The history of build worker image updates can be found [here](/updates/).

Before rolling out an image update we perform its extensive testing. However, not all usage scenarios can be covered by our automated tests and
sometimes even a smallest change in the image can break someone's build. If that happened - no worries - you're covered!
We provide an access to "last good" versions of build worker images from previous update:

* `Previous Visual Studio 2013`
* `Previous Visual Studio 2015`
* `Previous Visual Studio 2017`

You can use those images to unblock your builds while we are working together with you to understand the root cause and do a fix by the next image update.

## Pre-installed software

<div class="row">
    <div class="columns medium-4">
        <ul>
            <li><a href="#operating-system">Operating system</a></li>
            <li><a href="#powershell">PowerShell</a></li>
            <li><a href="#docker">Docker</a></li>
            <li><a href="#version-control-systems">Version control systems</a></li>
            <li><a href="#visual-studio-2008">Visual Studio 2008</a></li>
            <li><a href="#visual-studio-2010">Visual Studio 2010</a></li>
            <li><a href="#visual-studio-2012">Visual Studio 2012</a></li>
            <li><a href="#visual-studio-2013">Visual Studio 2013</a></li>
            <li><a href="#visual-studio-2015">Visual Studio 2015</a></li>
            <li><a href="#visual-studio-2017">Visual Studio 2017</a></li>
            <li><a href="#windows-sdks">Windows SDKs</a></li>
            <li><a href="#misc-sdks">Misc SDKs</a></li>
        </ul>
    </div>
    <div class="columns medium-4">
        <ul>
            <li><a href="#typescript">TypeScript</a></li>
            <li><a href="#azure">Azure</a></li>
            <li><a href="#xamarin">Xamarin</a></li>
            <li><a href="#net-framework">.NET Framework</a></li>
            <li><a href="#silverlight">Silverlight</a></li>
            <li><a href="#boost">Boost</a></li>
            <li><a href="#node-js">Node.js</a></li>
            <li><a href="#golang">Go (Golang)</a></li>
            <li><a href="#java">Java SE Development Kit (JDK)</a></li>
            <li><a href="#mono">Mono</a></li>
            <li><a href="#ruby">Ruby</a></li>
            <li><a href="#python">Python</a></li>
        </ul>
    </div>
    <div class="columns medium-4">
        <ul>
            <li><a href="#miniconda">Miniconda</a></li>
            <li><a href="#perl">Perl</a></li>
            <li><a href="#erlang">Erlang</a></li>
            <li><a href="#llvm">LLVM</a></li>
            <li><a href="#mingw-msys-cygwin">MinGW, MSYS, Cygwin</a></li>
            <li><a href="#qt">Qt</a></li>
            <li><a href="#tools">Tools</a></li>
            <li><a href="#test-runners">Test runners</a></li>
            <li><a href="#web-browsers">Web browsers</a></li>
            <li><a href="#selenium-testing">Selenium testing</a></li>
            <li><a href="#databases">Databases</a></li>
            <li><a href="#services">Services</a></li>
        </ul>
    </div>
</div>

<table class="software-list">
    <tr>
        <th>Software installed / Build worker image</th>
        <th class="rotate"><span>Visual Studio 2013</span></th>
        <th class="rotate"><span>Visual Studio 2015</span></th>
        <th class="rotate"><span>Visual Studio 2017</span></th>
    </tr>
    <tr>
        <th id="operating-system" class="section" colspan="4">Operating system</th>
    </tr>
    <tr>
        <td>Windows Server 2012 R2</td>
        <td class="yes"></td>
        <td class="yes"></td>
        <td class="no"></td>
    </tr>
    <tr>
        <td>Windows Server 2016</td>
        <td class="no"></td>
        <td class="no"></td>
        <td class="yes"></td>
    </tr>
    <tr>
        <th id="powershell" class="section" colspan="4">PowerShell</th>
    </tr>
    <tr>
        <td>Windows PowerShell 5.1</td>
        <td class="yes"></td>
        <td class="yes"></td>
        <td class="yes"></td>
    </tr>
    <tr>
        <td>PowerShell Core 6.0.0</td>
        <td class="no"></td>
        <td class="yes"></td>
        <td class="yes"></td>
    </tr>
    <!-- Docker -->
    <tr>
        <th id="docker" class="section" colspan="4">Docker</th>
    </tr>
    <tr>
        <td>
        <ul>
            <li>
                Docker 17.06.1-ee-2 for Windows Containers with base images:
                <ul>
                    <li>microsoft/windowsservercore:10.0.14393.2007<ul>
                        <li>digest: microsoft/windowsservercore@sha256:ebdf8f069e8941803a19bb3da4d70070c9d3b2f77c38476a9132022bab6e59a0</li></ul></li>
                    <li>microsoft/windowsservercore:10.0.14393.1944<ul>
                        <li>digest: microsoft/windowsservercore@sha256:f01583c072c043aa3588da03fb0aef1273342e56aee6ab07bc4693a7d93b2de1</li></ul></li>
                    <li>microsoft/nanoserver:10.0.14393.2007<ul>
                        <li>digest: microsoft/nanoserver@sha256:3d2948c5af9f4bece59b13f199f5bec59d6dc4930fb15aa9b6a223d2ea8d8471</li></ul></li>
                    <li>microsoft/nanoserver:10.0.14393.1944<ul>
                        <li>digest: microsoft/nanoserver@sha256:3331d7e40d93e8a3ea617450701a4f6550c699a673348b82fdc6fc01b9c44500</li></ul></li>
                </ul>
            </li>
        </ul>
        </td><td class="no"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>docker-compose 1.16.1</td><td class="no"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <!-- Version control systems -->
    <tr>
        <th id="version-control-systems" class="section" colspan="4">Version control systems</th>
    </tr>
    <tr>
        <td>Git 2.16.2 (x64) (with <code>git config --global core.autocrlf input</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Git Large File Storage (LFS) 2.4.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Mercurial 4.1.1 (x86)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Subversion 1.8.17 (x86)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Visual Studio 2008 -->
    <tr>
        <th id="visual-studio-2008" class="section" colspan="4">Visual Studio 2008</th>
    </tr>
    <tr><td>Visual C++ 2008 Express</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Visual Studio 2010 -->
    <tr>
        <th id="visual-studio-2010" class="section" colspan="4">Visual Studio 2010</th>
    </tr>
    <tr><td>Visual C# 2010 Express</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Visual Basic 2010 Express</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Visual C++ 2010 Express</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Visual Web Developer 2010 Express</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Visual Studio 2010 Service Pack 1</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Visual Studio 2012 -->
    <tr>
        <th id="visual-studio-2012" class="section" colspan="4">Visual Studio 2012</th>
    </tr>
    <tr><td>Visual Studio Express 2012 for Windows Desktop</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Visual Studio 2012 Update 5</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>SQL Server Data tools for Visual Studio 2012</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Visual Studio 2013 -->
    <tr>
        <th id="visual-studio-2013" class="section" colspan="4">Visual Studio 2013</th>
    </tr>
    <tr><td>Visual Studio Community 2013 with Update 5</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Visual Studio 2013 SDK</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Python Tools for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Node.js Tools for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>WDK 8</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Visual F# 3.1.2</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Microsoft Visual Studio Installer Projects Extension (`.vdproj` support)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>SQL Server Data tools for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Microsoft SQL Server Data Tools - Business Intelligence for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Office Developer Tools for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Code Contracts for .NET 1.9.10714.2</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Visual Studio 2015 -->
    <tr>
        <th id="visual-studio-2015" class="section" colspan="4">Visual Studio 2015</th>
    </tr>
    <tr><td>Visual Studio Community 2015 with Update 3</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Universal Windows App Dev Tools for Visual Studio 2015</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python Tools for Visual Studio 2015</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Node.js Tools 1.2 for Visual Studio 2015</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Visual F# Tools 4.0 RTM</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>WDK 10.0.14393</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>SQL Server Data Tools (SSDT) 17.0 (14.0.61704.140) for Visual Studio 2015</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Data-Tier Application Framework (17.1 DacFx)</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Azure Service Fabric SDK 3.0 (Runtime 6.1)</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Microsoft .NET Portable Library Reference Assemblies 4.6</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Microsoft Visual Studio Installer Projects Extension (`.vdproj` support)</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>SQL Server Data tools for Visual Studio 2015</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Office Developer Tools for Visual Studio 2015</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Code Contracts for .NET 1.9.10714.2</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Visual Studio 2017 -->
    <tr>
        <th id="visual-studio-2017" class="section" colspan="4">Visual Studio 2017</th>
    </tr>
    <tr><td>Visual Studio Community 2017 version 15.6.4</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>WDK for Windows 10, version 1709</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>SQL Server Data Tools (SSDT) 15.5.2 for Visual Studio 2017</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <!-- Windows SDKs -->
    <tr>
        <th id="windows-sdks" class="section" colspan="4">Windows SDKs</th>
    </tr>
    <tr><td>Microsoft Windows SDK for Windows 7 and .NET Framework 3.5 SP1</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Microsoft Windows SDK for Windows 7 and .NET Framework 3.5 SP1</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Microsoft Windows SDK for Windows 7 and .NET Framework 4</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Windows SDK for Windows 8</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Windows SDK for Windows 8.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Windows Driver Kit Version 7.1.0 (to support ATL)</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Windows Driver Kit 10</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Windows 10 SDK 10.0.10586</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Windows 10 SDK 10.0.14393</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Windows 10 SDK 10.0.26624</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Microsoft Expression Blend Software Development Kit (SDK) for .NET 4</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Microsoft Expression Blend Software Development Kit (SDK) for Silverlight 4</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Windows Phone SDK 8.0</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Windows PowerShell 2.0 SDK</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>DirectX SDK (<code>C:\Program Files (x86)\Microsoft DirectX SDK</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Misc SDKs -->
    <tr>
        <th id="misc-sdks" class="section" colspan="4">Misc SDKs</th>
    </tr>
    <tr><td>AWS SDK for .NET with AWS Tools for Windows 3.3.69</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>AWS CLI 1.11.68</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>WiX Toolset 3.11.0.1701</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- TypeScript -->
    <tr>
        <th id="typescript" class="section" colspan="4">TypeScript</th>
    </tr>
    <tr><td>TypeScript 1.4 for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>TypeScript 1.5 for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>TypeScript 1.6 for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>TypeScript 1.7 for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>TypeScript 1.8 for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>TypeScript 2.4.1 for Visual Studio 2015</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>TypeScript 2.1.5 for Visual Studio 2017</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>TypeScript 2.5.2 for Visual Studio 2015/2017</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>TypeScript 2.6 for Visual Studio 2015/2017</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>TypeScript 2.7.2 for Visual Studio 2015/2017</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Azure SDKs -->
    <tr>
        <th id="azure" class="section" colspan="4">Azure</th>
    </tr>
    <tr><td>Azure SDK 2.3</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Azure SDK 2.4</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Azure SDK 2.5.1</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Azure SDK 2.6</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Azure SDK 2.7.1</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Azure SDK 2.8.1</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Azure SDK 2.9.5</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Azure SDK 2.9.6</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Azure SDK 3.0</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Microsoft Azure Storage Emulator 5.3</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Microsoft Azure PowerShell 4.1.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Microsoft Azure CLI 0.9.10</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>DocumentDB Emulator 1.13.58.2</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>CosmoDB Emulator 1.20.91.1</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Xamarin -->
    <tr>
        <th id="xamarin" class="section" colspan="4">Xamarin</th>
    </tr>
    <tr><td>Xamarin 4.8.0</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- .NET Framework -->
    <tr>
        <th id="net-framework" class="section" colspan="4">.NET Framework</th>
    </tr>
    <tr><td>.NET Framework 2.0, 3.0, 3.5, 4.0, 4.5.1, 4.5.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Framework 4.6.0, 4.6.1, 4.6.2</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Framework 4.7.0</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Framework 4.7.1</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 1.0.0 runtime</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core 1.0.1 runtime</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core 1.0.3 runtime</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 1.0.4 runtime</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 1.1.0 runtime</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 1.1.1 runtime</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 1.1.2 runtime</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 2.0.0 runtime</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 2.0.3 runtime</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 2.0.5 runtime</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 2.0.6 runtime</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 1.0.0-preview2-003121</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 1.0.0-preview2-003131</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 1.0.0-preview2-003156</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 1.0.0-preview2-1-003177</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 1.0.0</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 1.0.1</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 1.0.2</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 1.0.3</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 1.0.4</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.0.0</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.0.2</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.0.3</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.4</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.101</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.103</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 1.0.4 &amp; 1.1.1 - Windows Server Hosting</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <!-- Silverlight -->
    <tr>
        <th id="silverlight" class="section" colspan="4">Silverlight</th>
    </tr>
    <tr><td>Silverlight 5 x64 Developer Runtime</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Microsoft SilverLight 5 SDK</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Microsoft SilverLight 4 SDK</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Boost -->
    <tr>
        <th id="boost" class="section" colspan="4">Boost</th>
    </tr>
    <tr><td>Boost 1.66.0 (<code>C:\Libraries\boost_1_66_0</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Boost 1.65.1 (<code>C:\Libraries\boost_1_65_1</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Boost 1.64.0 (<code>C:\Libraries\boost_1_64_0</code>)</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Boost 1.63.0 (<code>C:\Libraries\boost_1_63_0</code>)</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Boost 1.62.0 (<code>C:\Libraries\boost_1_62_0</code>)</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Boost 1.60.0 (<code>C:\Libraries\boost_1_60_0</code>)</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Boost 1.59.0 (<code>C:\Libraries\boost_1_59_0</code>)</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Boost 1.58.0 (<code>C:\Libraries\boost_1_58_0</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Boost 1.56.0 (<code>C:\Libraries\boost</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <!-- Node.js -->
    <tr>
        <th id="node-js" class="section" colspan="4">Node.js</th>
    </tr>
    <tr>
        <td>
            <p><code>4.x</code> is default Node.js installed on build workers.</p>
            <ul>
                <li>Node.js 9.0.0 - 9.8.0 (x86 and x64) - use <code>Current</code> alias for latest <code>9.x</code> release</li>
                <li>Node.js 8.0.0 - 8.10.0 (x86 and x64) - use <code>LTS</code> alias for latest <code>8.x</code> release</li>
                <li>Node.js 7.0.0 - 7.10.1 (x86 and x64)</li>
                <li>Node.js 6.0.0 - 6.13.1 (x86 and x64)</li>
                <li>Node.js 4.0.0 - 4.8.7 (x86 and x64) - default on build workers</li>
                <li>Node.js 5.0.0 - 5.12.0 (x86 and x64)</li>
                <li>Node.js 0.10.26 - 0.10.48 (x86 and x64)</li>
                <li>Node.js 0.11.12 - 0.11.16 (x86 and x64)</li>
                <li>Node.js 0.12.0 - 0.12.18 (x86 and x64)</li>
                <li>Node.js 0.8.25 - 0.8.28 (x86 and x64)</li>
                <li>io.js 1.0.0 - 3.3.0 (x86 and x64)</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Node.js 0.6.21 (x86 and x64)</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <!-- Go -->
    <tr>
        <th id="golang" class="section" colspan="4">Go (Golang)</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Go 1.10.0 x64 (<code>C:\go</code> - default in <code>PATH</code>)</li>
                <li>Go 1.10.0 x86 (<code>C:\go-x86</code>)</li>
                <li>Go 1.10.0 x64 (<code>C:\go110</code>)</li>
                <li>Go 1.10.0 x86 (<code>C:\go110-x86</code>)</li>
                <li>Go 1.9.4 x64 (<code>C:\go19</code>)</li>
                <li>Go 1.9.4 x86 (<code>C:\go19-x86</code>)</li>
                <li>Go 1.8.7 x64 (<code>C:\go18</code>)</li>
                <li>Go 1.8.7 x86 (<code>C:\go18-x86</code>)</li>
                <li>Go 1.7.6 x64 (<code>C:\go17</code>)</li>
                <li>Go 1.7.6 x86 (<code>C:\go17-x86</code>)</li>
                <li>Go 1.6.4 x64 (<code>C:\go16</code>)</li>
                <li>Go 1.6.4 x86 (<code>C:\go16-x86</code>)</li>
                <li>Go 1.5.4 x64 (<code>C:\go15</code>)</li>
                <li>Go 1.5.4 x86 (<code>C:\go15-x86</code>)</li>
                <li>Go 1.4.3 x64 (<code>C:\go14</code>)</li>
                <li>Go 1.4.3 x86 (<code>C:\go14-x86</code>)</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Java -->
    <tr>
        <th id="java" class="section" colspan="4">Java SE Development Kit (JDK)</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>JDK 1.6 Update 45 (x64) (<code>C:\Program Files\Java\jdk1.6.0\bin</code> - default in <code>PATH</code>)</li>
                <li>JDK 1.6 Update 45 (x86) (<code>C:\Program Files (x86)\Java\jdk1.6.0\bin</code>)</li>
                <li>JDK 1.7 Update 79 (x64) (<code>C:\Program Files\Java\jdk1.7.0\bin</code> - default in <code>PATH</code>)</li>
                <li>JDK 1.7 Update 79 (x86) (<code>C:\Program Files (x86)\Java\jdk1.7.0\bin</code>)</li>
                <li>JDK 1.8 Update 162 (x64) (<code>C:\Program Files\Java\jdk1.8.0</code>)</li>
                <li>JDK 1.8 Update 162 (x86) (<code>C:\Program Files (x86)\Java\jdk1.8.0</code>)</li>
                <li>JDK 9.0.4 (x64) (<code>C:\Program Files\Java\jdk9</code>)</li>
                <li>JDK 9.0.4 (x86) (<code>C:\Program Files (x86)\Java\jdk9</code>)</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr><td>Maven 3.3.9</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Mono -->
    <tr>
        <th id="mono" class="section" colspan="4">Mono</th>
    </tr>
    <tr><td>Mono 4.0.2 SR2</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Ruby -->
    <tr>
        <th id="ruby" class="section" colspan="4">Ruby (with DevKit)</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Ruby 1.9.3-p551 (<code>C:\Ruby193\bin</code> - default in <code>PATH</code>)</li>
                <li>Ruby 2.0.0-p648 x86 (<code>C:\Ruby200\bin</code>)</li>
                <li>Ruby 2.0.0-p648 x64 (<code>C:\Ruby200-x64\bin</code>)</li>
                <li>Ruby 2.1.9 x86 (<code>C:\Ruby21\bin</code>)</li>
                <li>Ruby 2.1.9 x64 (<code>C:\Ruby21-x64\bin</code>)</li>
                <li>Ruby 2.2.6 x86 (<code>C:\Ruby22\bin</code>)</li>
                <li>Ruby 2.2.6 x64 (<code>C:\Ruby22-x64\bin</code>)</li>
                <li>Ruby 2.3.3 x86 (<code>C:\Ruby23\bin</code>)</li>
                <li>Ruby 2.3.3 x64 (<code>C:\Ruby23-x64\bin</code>)</li>
                <li>Ruby 2.4.3-1 x86 (<code>C:\Ruby24\bin</code>)</li>
                <li>Ruby 2.4.3-1 x64 (<code>C:\Ruby24-x64\bin</code>)</li>
                <li>Ruby 2.5.0-1 x86 (<code>C:\Ruby25\bin</code>)</li>
                <li>Ruby 2.5.0-1 x64 (<code>C:\Ruby25-x64\bin</code>)</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Python -->
    <tr>
        <th id="python" class="section" colspan="4">Python</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Python 2.6.6 x86 (<code>C:\Python26</code>)</li>
                <li>Python 2.6.6 x64 (<code>C:\Python26-x64</code>)</li>
                <li>Python 2.7.14 x86 (<code>C:\Python27</code> - default in <code>PATH</code>)</li>
                <li>Python 2.7.14 x64 (<code>C:\Python27-x64</code>)</li>
                <li>Python 3.3.5 x86 (<code>C:\Python33</code>)</li>
                <li>Python 3.3.5 x64 (<code>C:\Python33-x64</code>)</li>
                <li>Python 3.4.4 x86 (<code>C:\Python34</code>)</li>
                <li>Python 3.4.4 x64 (<code>C:\Python34-x64</code>)</li>
                <li>Python 3.5.3 x86 (<code>C:\Python35</code>)</li>
                <li>Python 3.5.3 x64 (<code>C:\Python35-x64</code>)</li>
                <li>Python 3.6.4 x86 (<code>C:\Python36</code>)</li>
                <li>Python 3.6.4 x64 (<code>C:\Python36-x64</code>)</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr><td>Visual C++ Compiler for Python 2.7</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Miniconda -->
    <tr>
        <th id="miniconda" class="section" colspan="4">Miniconda</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Miniconda2 4.4.10 (Python 2.7.14): <code>C:\Miniconda</code></li>
                <li>Miniconda2 4.4.10 x64 (Python 2.7.14): <code>C:\Miniconda-x64</code></li>
                <li>Miniconda3 3.16.0 (Python 3.4.3): <code>C:\Miniconda34</code></li>
                <li>Miniconda3 3.16.0 x64 (Python 3.4.3): <code>C:\Miniconda34-x64</code></li>
                <li>Miniconda3 4.2.12 (Python 3.5.2): <code>C:\Miniconda35</code></li>
                <li>Miniconda3 4.2.12 x64 (Python 3.5.2): <code>C:\Miniconda35-x64</code></li>
                <li>Miniconda3 4.4.10 (Python 3.6.4): <code>C:\Miniconda36</code> or <code>C:\Miniconda3</code></li>
                <li>Miniconda3 4.4.10 x64 (Python 3.6.4): <code>C:\Miniconda36-x64</code> or <code>C:\Miniconda3-x64</code></li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Perl -->
    <tr>
        <th id="perl" class="section" colspan="4">Perl</th>
    </tr>
    <tr>
        <td>Perl 5.20.1.2000 x86 (<code>C:\Perl</code> in <code>PATH</code>)</td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Erlang -->
    <tr>
        <th id="erlang" class="section" colspan="4">Erlang</th>
    </tr>
    <tr>
        <td>Erlang OTP runtime 20.2 x64 installed into <code>C:\Program Files\erl9.2</code></td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- LLVM -->
    <tr>
        <th id="llvm" class="section" colspan="4">LLVM</th>
    </tr>
    <tr>
        <td>LLVM 5.0.1 x64 (<code>C:\Program Files\LLVM\bin</code> in <code>PATH</code>)</td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>LLVM 4.0.0 Compiler Infrastructure libraries (<code>C:\Libraries\llvm-4.0.0</code>)</td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>LLVM 5.0.0 Compiler Infrastructure libraries (<code>C:\Libraries\llvm-5.0.0</code>)</td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- MinGW, MSYS, Cygwin -->
    <tr>
        <th id="mingw-msys-cygwin" class="section" colspan="4">MinGW, MSYS, Cygwin</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>
                    MinGW 32-bit 5.3.0 (core components and compilers - <code>C:\MinGW</code>)
                    <ul>
                        <li>MinGW root directory: <code>C:\MinGW</code></li>
                        <li>MinGW bin directory: <code>C:\MinGW\bin</code></li>
                        <li>MSYS root directory: <code>C:\MinGW\msys\1.0</code></li>
                    </ul>
                </li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
                <li>
                    MinGW-w64
                    <ul>
                        <li>5.3.0: <code>C:\mingw-w64\i686-5.3.0-posix-dwarf-rt_v4-rev0</code></li>
                        <li>6.3.0 i686: <code>C:\mingw-w64\i686-6.3.0-posix-dwarf-rt_v5-rev1</code></li>
                        <li>6.3.0 x86_64: <code>C:\mingw-w64\x86_64-6.3.0-posix-seh-rt_v5-rev1</code></li>
                    </ul>
                </li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
                <li>
                    MinGW-w64
                    <ul>
                        <li>7.2.0 x86_64: <code>C:\mingw-w64\x86_64-7.2.0-posix-seh-rt_v5-rev1</code></li>
                    </ul>
                </li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Cygwin 2.10.0 (<code>C:\cygwin</code>)</td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Cygwin 2.10.0 x64 (<code>C:\cygwin64</code>)</td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>MSYS2 (<code>C:\msys64</code>)</td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Qt -->
    <tr>
        <th id="qt" class="section" colspan="4">Qt</th>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.10.1: <code>C:\Qt\5.10.1</code>
                <ul>
                <li>msvc2017 64-bit: <code>C:\Qt\5.10.1\msvc2017_64</code></li>
                <li>WinRT ARM v7: <code>C:\Qt\5.10.1\winrt_armv7_msvc2017</code></li>
                <li>WinRT 32-bit: <code>C:\Qt\5.10.1\winrt_x86_msvc2017</code></li>
                <li>WinRT 64-bit: <code>C:\Qt\5.10.1\winrt_x64_msvc2017</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.10.1: <code>C:\Qt\5.10.1</code>
                <ul>
                <li>MinGW 5.3.0 32 bit: <code>C:\Qt\5.10.1\mingw53_32</code></li>
                <li>msvc2015 32-bit: <code>C:\Qt\5.10.1\msvc2015</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.10.1: <code>C:\Qt\5.10.1</code>
                <ul>
                <li>msvc2015 64-bit: <code>C:\Qt\5.10.1\msvc2015_64</code></li>
                <li>msvc2013 64-bit: <code>C:\Qt\5.10.1\msvc2013_64</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.9.4: <code>C:\Qt\5.9.4</code> (<code>C:\Qt\5.9</code> mapped to <code>C:\Qt\5.9.4</code> for backward compatibility)
                <ul>
                <li>msvc2017 64-bit: <code>C:\Qt\5.9.4\msvc2017_64</code></li>
                <li>WinRT ARM v7: <code>C:\Qt\5.9.4\winrt_armv7_msvc2017</code></li>
                <li>WinRT 32-bit: <code>C:\Qt\5.9.4\winrt_x86_msvc2017</code></li>
                <li>WinRT 64-bit: <code>C:\Qt\5.9.4\winrt_x64_msvc2017</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.9.4: <code>C:\Qt\5.9.4</code> (<code>C:\Qt\5.9</code> mapped to <code>C:\Qt\5.9.4</code> for backward compatibility)
                <ul>
                <li>MinGW 5.3.0 32 bit: <code>C:\Qt\5.9.4\mingw53_32</code></li>
                <li>msvc2015 32-bit: <code>C:\Qt\5.9.4\msvc2015</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.9.4: <code>C:\Qt\5.9.4</code> (<code>C:\Qt\5.9</code> mapped to <code>C:\Qt\5.9.4</code> for backward compatibility)
                <ul>
                <li>msvc2015 64-bit: <code>C:\Qt\5.9.4\msvc2015_64</code></li>
                <li>msvc2013 64-bit: <code>C:\Qt\5.9.4\msvc2013_64</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.8.0: <code>C:\Qt\5.8</code>
                <ul>
                <li>MinGW 5.3.0 32 bit: <code>C:\Qt\5.8\mingw53_32</code></li>
                <li>msvc2015 64-bit: <code>C:\Qt\5.8\msvc2015_64</code></li>
                <li>msvc2015 32-bit: <code>C:\Qt\5.8\msvc2015</code></li>
                <li>msvc2013 64-bit: <code>C:\Qt\5.8\msvc2013_64</code></li>
                <li>msvc2013 32-bit: <code>C:\Qt\5.8\msvc2013</code></li>
                </ul>
            </li>
            <li>Qt 5.7.1: <code>C:\Qt\5.7</code>
                <ul>
                <li>MinGW 5.3.0 32 bit: <code>C:\Qt\5.7\mingw53_32</code></li>
                <li>msvc2015 64-bit: <code>C:\Qt\5.7\msvc2015_64</code></li>
                <li>msvc2015 32-bit: <code>C:\Qt\5.7\msvc2015</code></li>
                <li>msvc2013 64-bit: <code>C:\Qt\5.7\msvc2013_64</code></li>
                <li>msvc2013 32-bit: <code>C:\Qt\5.7\msvc2013</code></li>
                </ul>
            </li>
            <li>Qt 5.6.2: <code>C:\Qt\5.6</code>
                <ul>
                <li>MinGW 4.9.2 32 bit: <code>C:\Qt\5.6\mingw49_32</code></li>
                <li>msvc2015 64-bit: <code>C:\Qt\5.6\msvc2015_64</code></li>
                <li>msvc2015 32-bit: <code>C:\Qt\5.6\msvc2015</code></li>
                <li>msvc2013 64-bit: <code>C:\Qt\5.6\msvc2013_64</code></li>
                <li>msvc2013 32-bit: <code>C:\Qt\5.6\msvc2013</code></li>
                </ul>
            </li>
            <li>Qt 5.5: <code>C:\Qt\5.5</code>
                <ul>
                <li>MinGW 4.9.2 32 bit: <code>C:\Qt\5.5\mingw492_32</code></li>
                <li>msvc2013 64-bit: <code>C:\Qt\5.5\msvc2013_64</code></li>
                <li>msvc2013 32-bit: <code>C:\Qt\5.5\msvc2013</code></li>
                </ul>
            </li>
            <li>Qt 5.4: <code>C:\Qt\5.4</code>
                <ul>
                <li>MinGW 4.9.1 (32 bit) OpenGL: <code>C:\Qt\5.4\mingw491_32</code></li>
                <li>msvc2013 64-bit OpenGL: <code>C:\Qt\5.4\msvc2013_64_opengl</code></li>
                <li>msvc2013 32-bit OpenGL: <code>C:\Qt\5.4\msvc2013_opengl</code></li>
                </ul>
            </li>
            <li>Qt 5.3: <code>C:\Qt\5.3</code>
                <ul>
                <li>MinGW 4.8.2 (32 bit): <code>C:\Qt\5.3\mingw482_32</code></li>
                <li>msvc2013 64-bit OpenGL: <code>C:\Qt\5.3\msvc2013_64_opengl</code></li>
                <li>msvc2013 32-bit OpenGL: <code>C:\Qt\5.3\msvc2013_opengl</code></li>
                </ul>
            </li>
            <li>Tools
                <ul>
                <li>MinGW 5.3.0: <code>C:\Qt\Tools\mingw530_32</code></li>
                <li>MinGW 4.8.2: <code>C:\Qt\Tools\mingw482_32</code></li>
                <li>MinGW 4.9.1: <code>C:\Qt\Tools\mingw491_32</code></li>
                <li>MinGW 4.9.2: <code>C:\Qt\Tools\mingw492_32</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>Qt Installer Framework 3.0.1</td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Tools -->
    <tr>
        <th id="tools" class="section" colspan="4">Tools</th>
    </tr>
    <tr><td>curl 7.55.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>7-Zip 16.04</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Microsoft Web Platform Installer 5.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>CMake 3.11.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>NuGet 2.8.6</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>NuGet 4.3.0</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Chocolatey v0.10.8</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GitVersion 3.6.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>FxCop 10.0</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>OpenSSL 1.0.2L (32-bit) (<code>C:\OpenSSL-Win32\bin</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>OpenSSL 1.0.2L (64-bit) (<code>C:\OpenSSL-Win64\bin</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>NSIS 3.03 (<code>C:\Program Files (x86)\NSIS</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>InnoSetup 5.5.9 (<code>C:\Program Files (x86)\Inno Setup 5</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Coverity Scan 2017.07</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Yarn 1.5.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>vcpkg 0.0.107</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Octo 4.31.3</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Test runners -->
    <tr>
        <th id="test-runners" class="section" colspan="4">Test runners</th>
    </tr>
    <tr><td>NUnit 2.6.4 in <code>C:\Tools\NUnit\bin</code></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>NUnit 3.7.0 in <code>C:\Tools\NUnit3</code></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>xUnit 1.9.2 in <code>C:\Tools\xUnit</code></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>xUnit 2.0.0 RTM in <code>C:\Tools\xUnit20</code></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Machine.Specifications (MSpec)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Web browsers -->
    <tr>
        <th id="web-browsers" class="section" colspan="4">Web browsers</th>
    </tr>
    <tr><td>Internet Explorer 11</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Firefox 59.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Chrome 65.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Selenium testing -->
    <tr>
        <th id="selenium-testing" class="section" colspan="4">Selenium testing</th>
    </tr>
    <tr><td>Chrome Web Driver 2.37</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Internet Explorer Web Driver 3.9</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Firefox Web Driver (geckodriver) 0.19.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Databases -->
    <tr>
        <th id="databases" class="section" colspan="4">Databases</th>
    </tr>
    <tr><td>SQL Server 2008 R2 SP2 Express Edition with Advanced Services (x64)</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>SQL Server 2012 SP1 Express with Advanced Services</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>SQL Server 2014 Express with Advanced Services</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>SQL Server 2016 Developer with SP1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>SQL Server 2017 Developer</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>PostgreSQL 9.3 x64</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>PostgreSQL 9.4 x64</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>PostgreSQL 9.5 x64</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>PostgreSQL 9.6 x64</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>PostgreSQL 10.1 x64</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>PostgreSQL ODBC drivers</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>MySQL 5.7</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>MySQL ODBC drivers</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>MongoDB 3.2.5</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Services -->
    <tr>
        <th id="services" class="section" colspan="4">Services</th>
    </tr>
    <tr><td>Internet Information Services (IIS) 8.5</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Internet Information Services (IIS) 10</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>MSMQ</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
</table>
