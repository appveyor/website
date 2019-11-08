---
layout: docs
title: Software pre-installed on Windows build VMs
---

<!-- markdownlint-disable MD022 MD032 -->
# Software pre-installed on Windows build VMs
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

<div class="row">
    <div class="columns medium-4">
        <ul>
            <li><a href="#operating-system">Operating system</a></li>
            <li><a href="#powershell">PowerShell</a></li>
            <li><a href="#docker">Docker</a></li>
            <li><a href="#wsl">WSL</a></li>
            <li><a href="#version-control-systems">Version control systems</a></li>
            <li><a href="#visual-studio-2008">Visual Studio 2008</a></li>
            <li><a href="#visual-studio-2010">Visual Studio 2010</a></li>
            <li><a href="#visual-studio-2012">Visual Studio 2012</a></li>
            <li><a href="#visual-studio-2013">Visual Studio 2013</a></li>
            <li><a href="#visual-studio-2015">Visual Studio 2015</a></li>
            <li><a href="#visual-studio-2017">Visual Studio 2017</a></li>
            <li><a href="#visual-studio-2019">Visual Studio 2019</a></li>
            <li><a href="#windows-sdks">Windows SDKs</a></li>
        </ul>
    </div>
    <div class="columns medium-4">
        <ul>
            <li><a href="#misc-sdks">Misc SDKs</a></li>
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
        <th class="rotate"><span>Visual Studio 2019</span></th>
    </tr>
    <tr>
        <th id="operating-system" class="section" colspan="5">Operating system</th>
    </tr>
    <tr>
        <td>Windows Server 2012 R2</td>
        <td class="yes"></td>
        <td class="yes"></td>
        <td class="no"></td>
        <td class="no"></td>
    </tr>
    <tr>
        <td>Windows Server 2016</td>
        <td class="no"></td>
        <td class="no"></td>
        <td class="yes"></td>
        <td class="no"></td>
    </tr>
    <tr>
        <td>Windows Server 2019</td>
        <td class="no"></td>
        <td class="no"></td>
        <td class="no"></td>
        <td class="yes"></td>
    </tr>
    <tr>
        <th id="powershell" class="section" colspan="5">PowerShell</th>
    </tr>
    <tr>
        <td>Windows PowerShell 5.1</td>
        <td class="yes"></td>
        <td class="yes"></td>
        <td class="yes"></td>
        <td class="no"></td>
    </tr>
    <tr>
        <td>PowerShell Core 6.2.3</td>
        <td class="no"></td>
        <td class="yes"></td>
        <td class="yes"></td>
        <td class="yes"></td>
    </tr>
    <!-- Docker -->
    <tr>
        <th id="docker" class="section" colspan="5">Docker</th>
    </tr>
    <tr>
        <td>
        <ul>
            <li>
                Docker CE 18.05.0-ce for Windows and Linux with base images:
                <ul>
                    <li>microsoft/windowsservercore<br/>sha256:c06b4bfaf634215ea194e6005450740f3a230b27c510cf8facab1e9c678f3a99</li>
                    <li>microsoft/nanoserver<br/>sha256:8f78a4a7da4464973a5cd239732626141aec97e69ba3e4023357628630bc1ee2</li>
                    <li>microsoft/aspnet<br/>sha256:7cdafe834e1c08ed880cd54183ba33d4760c8b19e651ef1cbff0cf3118684e88</li>
                    <li>microsoft/dotnet<br/>sha256:7e6a3e8272ce3dee0c091bcb78d784b19e0925cf6e61b23b70dc17772580643f</li>
                    <li>microsoft/aspnetcore<br/>sha256:5f964756fae50873c496915ad952b0f15df8ef985e4ac031d00b7ac0786162d0</li>
                    <li>microsoft/aspnetcore-build<br/>sha256:1e4043c2484157e029d6b09a3149ebd01c5122f6b62f0a1b6c6f54b90500ea4b</li>
                </ul>
            </li>
        </ul>
        </td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
        <ul>
            <li>
                Docker EE 19.03.2 for Windows Server in experimental mode (LCOW support) with base images:
                <ul>
                    <li>mcr.microsoft.com/windows/servercore:ltsc2019<br/>sha256:d7e7e3702cbc4d8ea29001a02c1c852d0229a0679d94e017a41c43dbaa01db20</li>
                    <li>mcr.microsoft.com/windows/servercore:ltsc2016<br/>sha256:c1c0c43f28b3d994e5f215946786c34634dcc813aa7afe2f28a968c02ca1de68</li>
                    <li>mcr.microsoft.com/windows/servercore:1803<br/>sha256:9b22618ec9b832f7e74c5461b67761423fea83dd10bf627fe715e232aad0dc1e</li>
                    <li>mcr.microsoft.com/windows/servercore:1709<br/>sha256:aa2c3d4c376a6debbec5625dc15243e7c1112c1ebd603ff63c143b7a8edc32a9</li>
                    <li>mcr.microsoft.com/windows/nanoserver:1809<br/>sha256:408c7ec68f510485ccca904d9ab69c3ad165126b44786d9f2fffbbc4d2d9646b</li>
                    <li>mcr.microsoft.com/windows/nanoserver:1803<br/>sha256:47aa6f7f8ff1a02acc34b9f22689e9c89b3387efdd7c0f2cf6da9db810c0d4de</li>
                    <li>mcr.microsoft.com/windows/nanoserver:1709<br/>sha256:97272be4e3abcd71727095a6ab1948afc4c8412ad0649abfe60912651769a67d</li>
                    <li>mcr.microsoft.com/windows/nanoserver:sac2016<br/>sha256:2b783310e6c82de737e893abd53ae238ca56b5a96e2861558fb9a111d6691ddb</li>
                </ul>
            </li>
        </ul>
        </td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>docker-compose 1.16.1</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>docker-compose 1.23.2</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <!-- WSL -->
    <tr>
        <th id="wsl" class="section" colspan="5">Windows Subsystem for Linux (WSL)</th>
    </tr>
    <tr>
        <td>
        <ul>
            <li>
                Windows Subsystem for Linux Distributions:
                <ul>
                <li>Ubuntu-18.04 (Default)</li>
                <li>Ubuntu-16.04</li>
                <li>openSUSE-42</li>
                </ul>
            </li>
        </ul>
        </td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <!-- Version control systems -->
    <tr>
        <th id="version-control-systems" class="section" colspan="5">Version control systems</th>
    </tr>
    <tr>
        <td>Git 2.17.1 (2) (x64) (with <code>git config --global core.autocrlf input</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>Git 2.21.1 (2) (x64) (with <code>git config --global core.autocrlf input</code>)</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Git Large File Storage (Git LFS) 2.4.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>Git Large File Storage (Git LFS) 2.6.1</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Mercurial 4.1.1 (x86)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Subversion 1.8.17 (x86)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Visual Studio 2008 -->
    <tr>
        <th id="visual-studio-2008" class="section" colspan="5">Visual Studio 2008</th>
    </tr>
    <tr><td>Visual C++ 2008 Express</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <!-- Visual Studio 2010 -->
    <tr>
        <th id="visual-studio-2010" class="section" colspan="5">Visual Studio 2010</th>
    </tr>
    <tr><td>Visual C# 2010 Express</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Visual Basic 2010 Express</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Visual C++ 2010 Express</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Visual Web Developer 2010 Express</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Visual Studio 2010 Service Pack 1</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <!-- Visual Studio 2012 -->
    <tr>
        <th id="visual-studio-2012" class="section" colspan="5">Visual Studio 2012</th>
    </tr>
    <tr><td>Visual Studio Express 2012 for Windows Desktop</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Visual Studio 2012 Update 5</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>SQL Server Data tools for Visual Studio 2012</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <!-- Visual Studio 2013 -->
    <tr>
        <th id="visual-studio-2013" class="section" colspan="5">Visual Studio 2013</th>
    </tr>
    <tr><td>Visual Studio Community 2013 with Update 5</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Visual Studio 2013 SDK</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Python Tools for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Node.js Tools for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>WDK 8</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Visual F# 3.1.2</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Microsoft Visual Studio Installer Projects Extension (`.vdproj` support)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>SQL Server Data tools for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Microsoft SQL Server Data Tools - Business Intelligence for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Office Developer Tools for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Code Contracts for .NET 1.9.10714.2</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <!-- Visual Studio 2015 -->
    <tr>
        <th id="visual-studio-2015" class="section" colspan="5">Visual Studio 2015</th>
    </tr>
    <tr><td>Visual Studio Community 2015 with Update 3</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Universal Windows App Dev Tools for Visual Studio 2015</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Python Tools for Visual Studio 2015</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Node.js Tools 1.2 for Visual Studio 2015</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Visual F# Tools 4.0 RTM</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>WDK 10.0.14393</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>WDK for Windows 10, version 1803 (10.0.17134.1)</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>WDK for Windows 10, version 1903 (10.0.18362.1)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>SQL Server Data Tools (SSDT) 17.0 (14.0.61704.140) for Visual Studio 2015</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Data-Tier Application Framework (17.1 DacFx)</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Azure Service Fabric SDK 3.2 (Runtime 6.3)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Microsoft .NET Portable Library Reference Assemblies 4.6</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Microsoft Visual Studio Installer Projects Extension (`.vdproj` support)</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>SQL Server Data tools for Visual Studio 2015</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Office Developer Tools for Visual Studio 2015</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Code Contracts for .NET 1.9.10714.2</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <!-- Visual Studio 2017 -->
    <tr>
        <th id="visual-studio-2017" class="section" colspan="5">Visual Studio 2017</th>
    </tr>
    <tr><td>Visual Studio Community 2017 version 15.9.17</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>WDK for Windows 10, version 1709</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>SQL Server Data Tools (SSDT) 15.5.2 for Visual Studio 2017</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Visual Studio 2019 -->
    <tr>
        <th id="visual-studio-2019" class="section" colspan="5">Visual Studio 2019</th>
    </tr>
    <tr><td>Visual Studio Community 2019 version 16.3.8</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Visual Studio Community 2019 Preview version 16.4 Preview 2 on <code>Visual Studio 2019 Preview</code> image</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>WDK for Windows 10, version 1809</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>WDK for Windows 10, version 1903</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <!-- Windows SDKs -->
    <tr>
        <th id="windows-sdks" class="section" colspan="5">Windows SDKs</th>
    </tr>
    <tr><td>Microsoft Windows SDK for Windows 7 and .NET Framework 3.5 SP1</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Microsoft Windows SDK for Windows 7 and .NET Framework 3.5 SP1</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Microsoft Windows SDK for Windows 7 and .NET Framework 4</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Windows SDK for Windows 8</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Windows SDK for Windows 8.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Windows Driver Kit Version 7.1.0 (to support ATL)</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Windows Driver Kit 10</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Windows Driver Kit 1809</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Windows 10 SDK 10.0.10586</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Windows 10 SDK 10.0.14393</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Windows 10 SDK 10.0.26624</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Windows 10 SDK 10.0.17763</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Windows 10 SDK 10.0.18362</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Microsoft Expression Blend Software Development Kit (SDK) for .NET 4</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Microsoft Expression Blend Software Development Kit (SDK) for Silverlight 4</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Windows Phone SDK 8.0</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Windows PowerShell 2.0 SDK</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>DirectX SDK (<code>C:\Program Files (x86)\Microsoft DirectX SDK</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Misc SDKs -->
    <tr>
        <th id="misc-sdks" class="section" colspan="5">Misc SDKs</th>
    </tr>
    <tr><td>AWS SDK for .NET with AWS Tools for Windows 3.3.69</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>AWS CLI 1.11.68</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>AWS CLI 1.16.228</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>WiX Toolset 3.11.0.1701</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>WiX Toolset 3.11.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- TypeScript -->
    <tr>
        <th id="typescript" class="section" colspan="5">TypeScript</th>
    </tr>
    <tr><td>TypeScript 1.4 for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>TypeScript 1.5 for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>TypeScript 1.6 for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>TypeScript 1.7 for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>TypeScript 1.8 for Visual Studio 2013</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>TypeScript 2.4.1 for Visual Studio 2015</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>TypeScript 2.1.5 for Visual Studio 2017</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>TypeScript 2.5.2 for Visual Studio 2015/2017</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>TypeScript 2.6 for Visual Studio 2015/2017</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>TypeScript 2.7.2 for Visual Studio 2015/2017</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>TypeScript 2.8.3 for Visual Studio 2015/2017</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>TypeScript 3.0.1 for Visual Studio 2015/2017</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>TypeScript 3.1 for Visual Studio 2017</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Azure SDKs -->
    <tr>
        <th id="azure" class="section" colspan="5">Azure</th>
    </tr>
    <tr><td>Azure SDK 2.3</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Azure SDK 2.4</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Azure SDK 2.5.1</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Azure SDK 2.6</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Azure SDK 2.7.1</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Azure SDK 2.8.1</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Azure SDK 2.9.5</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Azure SDK 2.9.6</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Azure SDK 3.0</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Microsoft Azure Storage Emulator 5.5</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Microsoft Azure Storage Emulator 5.9</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Microsoft Azure PowerShell 5.7</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Microsoft Azure CLI 2.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>DocumentDB Emulator 1.13.58.2</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Azure Cosmos DB Emulator 2.7.0</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Xamarin -->
    <tr>
        <th id="xamarin" class="section" colspan="5">Xamarin</th>
    </tr>
    <tr><td>Xamarin 4.9</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Xamarin 4.11</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Android SDK 4.4 (KitKat) API level 19</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Android SDK 5.0 (Lollipop) API level 21</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Android SDK 5.1 (Lollipop) API level 22</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Android SDK 6.0 (Marshmallow) API level 23</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Android SDK 7.0 (Nougat) API level 24</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Android SDK 7.1 (Nougat) API level 25</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Android SDK 8.0.0 (Oreo) API level 26</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Android SDK 8.1.0 (Oreo) API level 27</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Android SDK 9.0 (Pie) API level 28</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Android SDK 10.0 (Android 10) API level 29</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Android NDK r10e (<code>C:\ProgramData\Microsoft\AndroidNDK64\android-ndk-r10e</code>)</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Android NDK r11c (<code>C:\ProgramData\Microsoft\AndroidNDK64\android-ndk-r11c</code>)</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Android NDK r16b</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Android NDK r17 (<code>C:\ProgramData\Microsoft\AndroidNDK64\android-ndk-r17</code>)</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- .NET Framework -->
    <tr>
        <th id="net-framework" class="section" colspan="5">.NET Framework</th>
    </tr>
    <tr><td>.NET Framework 2.0, 3.0, 3.5, 4.0, 4.5.1, 4.5.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Framework 4.6.0, 4.6.1, 4.6.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Framework 4.7.0</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Framework 4.7.1</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Framework 4.7.2</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Framework 4.8</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 1.0.0-preview2-003121</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 1.0.0-preview2-003131</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 1.0.0-preview2-003156</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 1.0.0-preview2-1-003177</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 1.0.0</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 1.0.1</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 1.0.2</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 1.0.3</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 1.0.4</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 1.1.14</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.0.0</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.0.2</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.0.3</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.14</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.4</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.101</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.103</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.104</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.105</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.200</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.201</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.300</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.301</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.302</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.400</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.401</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.402</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.403</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.500</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.503</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.507</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.603</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.604</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.701</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.801</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.2.100</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.2.103</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.2.107</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.2.108</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.2.109</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.2.203</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.2.204</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.2.206</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.2.301</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.2.402</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 3.0.100</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 3.1.100-preview1-014459 (<code>Visual Studio 2019 Preview</code>)</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <!-- Silverlight -->
    <tr>
        <th id="silverlight" class="section" colspan="5">Silverlight</th>
    </tr>
    <tr><td>Silverlight 5 x64 Developer Runtime</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Microsoft SilverLight 5 SDK</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Microsoft SilverLight 4 SDK</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Boost -->
    <tr>
        <th id="boost" class="section" colspan="5">Boost</th>
    </tr>
    <tr><td>Boost 1.71.0 (<code>C:\Libraries\boost_1_71_0</code>)</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Boost 1.69.0 (<code>C:\Libraries\boost_1_69_0</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Boost 1.67.0 (<code>C:\Libraries\boost_1_67_0</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Boost 1.66.0 (<code>C:\Libraries\boost_1_66_0</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Boost 1.65.1 (<code>C:\Libraries\boost_1_65_1</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Boost 1.64.0 (<code>C:\Libraries\boost_1_64_0</code>)</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Boost 1.63.0 (<code>C:\Libraries\boost_1_63_0</code>)</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Boost 1.62.0 (<code>C:\Libraries\boost_1_62_0</code>)</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Boost 1.60.0 (<code>C:\Libraries\boost_1_60_0</code>)</td><td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Boost 1.59.0 (<code>C:\Libraries\boost_1_59_0</code>)</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Boost 1.58.0 (<code>C:\Libraries\boost_1_58_0</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Boost 1.56.0 (<code>C:\Libraries\boost</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <!-- Node.js -->
    <tr>
        <th id="node-js" class="section" colspan="5">Node.js</th>
    </tr>
    <tr><td colspan="5"><code>8.x</code> is default Node.js installed on build workers.</td></tr>
    <tr><td>Node.js 13.0.0 - 13.0.1 (x86 and x64) - use <code>Current</code> alias for latest <code>13.x</code> release</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 12.0.0 - 12.13.0 (x86 and x64) - use <code>LTS</code> alias for latest <code>12.x</code> release</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 11.0.0 - 11.15.0 (x86 and x64)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 10.0.0 - 10.17.0 (x86 and x64)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 9.0.0 - 9.11.2 (x86 and x64)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 8.0.0 - 8.16.2 (x86 and x64) - default on build workers</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 7.0.0 - 7.10.1 (x86 and x64)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 6.0.0 - 6.17.1 (x86 and x64)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 4.0.0 - 4.9.1 (x86 and x64)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 5.0.0 - 5.12.0 (x86 and x64)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 0.10.26 - 0.10.48 (x86 and x64)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 0.11.12 - 0.11.16 (x86 and x64)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 0.12.0 - 0.12.18 (x86 and x64)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 0.8.25 - 0.8.28 (x86 and x64)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 0.6.21 (x86 and x64)</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>io.js 1.0.0 - 3.3.0 (x86 and x64)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Go -->
    <tr>
        <th id="golang" class="section" colspan="5">Go (Golang)</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Go 1.13.3 x64 (<code>C:\go</code> - default in <code>PATH</code>)</li>
                <li>Go 1.13.3 x86 (<code>C:\go-x86</code>)</li>
                <li>Go 1.13.3 x64 (<code>C:\go113</code>)</li>
                <li>Go 1.13.3 x86 (<code>C:\go113-x86</code>)</li>
                <li>Go 1.12.12 x64 (<code>C:\go112</code>)</li>
                <li>Go 1.12.12 x86 (<code>C:\go112-x86</code>)</li>
                <li>Go 1.11.13 x64 (<code>C:\go111</code>)</li>
                <li>Go 1.11.13 x86 (<code>C:\go111-x86</code>)</li>
                <li>Go 1.10.8 x64 (<code>C:\go110</code>)</li>
                <li>Go 1.10.8 x86 (<code>C:\go110-x86</code>)</li>
                <li>Go 1.9.7 x64 (<code>C:\go19</code>)</li>
                <li>Go 1.9.7 x86 (<code>C:\go19-x86</code>)</li>
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
        <td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Java -->
    <tr>
        <th id="java" class="section" colspan="5">Java SE Development Kit (JDK)</th>
    </tr>
    <tr><td>JDK 1.6 Update 45 (x64) (<code>C:\Program Files\Java\jdk1.6.0\bin</code> - default in <code>PATH</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>JDK 1.6 Update 45 (x86) (<code>C:\Program Files (x86)\Java\jdk1.6.0\bin</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>JDK 1.7 Update 80 (x64) (<code>C:\Program Files\Java\jdk1.7.0\bin</code> - default in <code>PATH</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>JDK 1.7 Update 80 (x86) (<code>C:\Program Files (x86)\Java\jdk1.7.0\bin</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>JDK 1.8 Update 162 (x64) (<code>C:\Program Files\Java\jdk1.8.0</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>JDK 1.8 Update 162 (x86) (<code>C:\Program Files (x86)\Java\jdk1.8.0</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>JDK 1.8 Update 221 (x64) (<code>C:\Program Files\Java\jdk1.8.0</code>)</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>JDK 1.8 Update 221 (x86) (<code>C:\Program Files (x86)\Java\jdk1.8.0</code>)</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>JDK 9.0.4 (x64) (<code>C:\Program Files\Java\jdk9</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>JDK 10.0.1 (x64) (<code>C:\Program Files\Java\jdk10</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>JDK 11.0.2 (x64) (<code>C:\Program Files\Java\jdk11</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>JDK 12 (x64) (<code>C:\Program Files\Java\jdk12</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>JDK 13 (x64) (<code>C:\Program Files\Java\jdk13</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Maven 3.5.4</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Maven 3.6.2</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <!-- Mono -->
    <tr>
        <th id="mono" class="section" colspan="5">Mono</th>
    </tr>
    <tr><td>Mono 4.0.2 SR2</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <!-- Ruby -->
    <tr>
        <th id="ruby" class="section" colspan="5">Ruby (with DevKit)</th>
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
                <li>Ruby 2.4.6-1 x86 (<code>C:\Ruby24\bin</code>)</li>
                <li>Ruby 2.4.6-1 x64 (<code>C:\Ruby24-x64\bin</code>)</li>
                <li>Ruby 2.5.5-1 x86 (<code>C:\Ruby25\bin</code>)</li>
                <li>Ruby 2.5.5-1 x64 (<code>C:\Ruby25-x64\bin</code>)</li>
                <li>Ruby 2.6.3 x86 (<code>C:\Ruby26\bin</code>)</li>
                <li>Ruby 2.6.3 x64 (<code>C:\Ruby26-x64\bin</code>)</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Python -->
    <tr>
        <th id="python" class="section" colspan="5">Python</th>
    </tr>
    <tr><td>Python 2.6.6 x86 (<code>C:\Python26</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 2.6.6 x64 (<code>C:\Python26-x64</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 2.7.15 x86 (<code>C:\Python27</code> - default in <code>PATH</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Python 2.7.15 x64 (<code>C:\Python27-x64</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Python 2.7.17 x86 (<code>C:\Python27</code> - default in <code>PATH</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 2.7.17 x64 (<code>C:\Python27-x64</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.3.5 x86 (<code>C:\Python33</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.3.5 x64 (<code>C:\Python33-x64</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.4.4 x86 (<code>C:\Python34</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.4.4 x64 (<code>C:\Python34-x64</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.5.4 x86 (<code>C:\Python35</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.5.4 x64 (<code>C:\Python35-x64</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.6.6 x86 (<code>C:\Python36</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Python 3.6.6 x64 (<code>C:\Python36-x64</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Python 3.6.8 x86 (<code>C:\Python36</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.6.8 x64 (<code>C:\Python36-x64</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.7.0 x86 (<code>C:\Python37</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Python 3.7.0 x64 (<code>C:\Python37-x64</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Python 3.7.5 x86 (<code>C:\Python37</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.7.5 x64 (<code>C:\Python37-x64</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.8.0 x86 (<code>C:\Python38</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.8.0 x64 (<code>C:\Python38-x64</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Visual C++ Compiler for Python 2.7</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <!-- Miniconda -->
    <tr>
        <th id="miniconda" class="section" colspan="5">Miniconda</th>
    </tr>
    <tr><td>Miniconda2 4.7.12 (Python 2.7.16): <code>C:\Miniconda</code></td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Miniconda2 4.7.12 x64 (Python 2.7.16): <code>C:\Miniconda-x64</code></td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Miniconda2 4.5.11 (Python 2.7.15): <code>C:\Miniconda</code></td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Miniconda2 4.5.11 x64 (Python 2.7.15): <code>C:\Miniconda-x64</code></td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Miniconda3 3.16.0 (Python 3.4.3): <code>C:\Miniconda34</code></td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Miniconda3 3.16.0 x64 (Python 3.4.3): <code>C:\Miniconda34-x64</code></td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Miniconda3 4.2.12 (Python 3.5.2): <code>C:\Miniconda35</code></td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Miniconda3 4.2.12 x64 (Python 3.5.2): <code>C:\Miniconda35-x64</code></td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Miniconda3 4.5.4 (Python 3.6.5): <code>C:\Miniconda36</code></td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Miniconda3 4.5.4 x64 (Python 3.6.5): <code>C:\Miniconda36-x64</code></td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Miniconda3 4.7.12 (Python 3.7.4): <code>C:\Miniconda37</code> or <code>C:\Miniconda3</code></td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Miniconda3 4.7.12 x64 (Python 3.7.4): <code>C:\Miniconda37-x64</code> or <code>C:\Miniconda3-x64</code></td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Miniconda3 4.5.11 (Python 3.7.0): <code>C:\Miniconda37</code> or <code>C:\Miniconda3</code></td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Miniconda3 4.5.11 x64 (Python 3.7.0): <code>C:\Miniconda37-x64</code> or <code>C:\Miniconda3-x64</code></td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <!-- Perl -->
    <tr>
        <th id="perl" class="section" colspan="5">Perl</th>
    </tr>
    <tr>
        <td>Perl 5.20.1.2000 x86 (<code>C:\Perl</code> in <code>PATH</code>)</td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <!-- Erlang -->
    <tr>
        <th id="erlang" class="section" colspan="5">Erlang</th>
    </tr>
    <tr>
        <td>Erlang OTP runtime 20.2 x64 installed into <code>C:\Program Files\erl9.2</code></td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- LLVM -->
    <tr>
        <th id="llvm" class="section" colspan="5">LLVM</th>
    </tr>
    <tr>
        <td>LLVM 9.0.0 x64 (<code>C:\Program Files\LLVM\bin</code> in <code>PATH</code>)</td>
        <td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>LLVM 4.0.0 Compiler Infrastructure libraries (<code>C:\Libraries\llvm-4.0.0</code>)</td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>LLVM 5.0.0 Compiler Infrastructure libraries (<code>C:\Libraries\llvm-5.0.0</code>)</td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <!-- MinGW, MSYS, Cygwin -->
    <tr>
        <th id="mingw-msys-cygwin" class="section" colspan="5">MinGW, MSYS, Cygwin</th>
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
        <td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td>
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
                        <li>7.3.0 x86_64: <code>C:\mingw-w64\x86_64-7.3.0-posix-seh-rt_v5-rev0</code></li>
                        <li>8.1.0 x86_64: <code>C:\mingw-w64\x86_64-8.1.0-posix-seh-rt_v6-rev0</code></li>
                    </ul>
                </li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td>
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
        <td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>Cygwin 3.0.7 (<code>C:\cygwin</code>)</td>
        <td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Cygwin 3.0.7 x64 (<code>C:\cygwin64</code>)</td>
        <td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>MSYS2 (<code>C:\msys64</code>)</td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Qt -->
    <tr>
        <th id="qt" class="section" colspan="5">Qt</th>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.13.2: <code>C:\Qt\5.13.2</code> (<code>C:\Qt\5.13</code> mapped to <code>C:\Qt\5.13.2</code> for backward compatibility)
                <ul>
                <li>MinGW 7.3.0 64 bit: <code>C:\Qt\5.13.2\mingw73_64</code></li>
                <li>MinGW 7.3.0 32 bit: <code>C:\Qt\5.13.2\mingw73_32</code></li>
                <li>msvc2017 32-bit: <code>C:\Qt\5.13.2\msvc2017</code></li>
                <li>msvc2017 64-bit: <code>C:\Qt\5.13.2\msvc2017_64</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.13.2: <code>C:\Qt\5.13.2</code> (<code>C:\Qt\5.13</code> mapped to <code>C:\Qt\5.13.2</code> for backward compatibility)
                <ul>
                <li>MinGW 7.3.0 64 bit: <code>C:\Qt\5.13.2\mingw73_64</code></li>
                <li>MinGW 7.3.0 32 bit: <code>C:\Qt\5.13.2\mingw73_32</code></li>
                <li>msvc2017 32-bit: <code>C:\Qt\5.13.2\msvc2017</code></li>
                <li>msvc2017 64-bit: <code>C:\Qt\5.13.2\msvc2017_64</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.13.2: <code>C:\Qt\5.13.2</code> (<code>C:\Qt\5.13</code> mapped to <code>C:\Qt\5.13.2</code> for backward compatibility)
                <ul>
                <li>MinGW 7.3.0 64 bit: <code>C:\Qt\5.13.2\mingw73_64</code></li>
                <li>MinGW 7.3.0 32 bit: <code>C:\Qt\5.13.2\mingw73_32</code></li>
                <li>msvc2015 64-bit: <code>C:\Qt\5.13.2\msvc2015_64</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.12.5: <code>C:\Qt\5.12.5</code>
                <ul>
                <li>MinGW 7.3.0 64 bit: <code>C:\Qt\5.12.5\mingw73_64</code></li>
                <li>MinGW 7.3.0 32 bit: <code>C:\Qt\5.12.5\mingw73_32</code></li>
                <li>msvc2017 32-bit: <code>C:\Qt\5.12.5\msvc2017</code></li>
                <li>msvc2017 64-bit: <code>C:\Qt\5.12.5\msvc2017_64</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.12.5: <code>C:\Qt\5.12.5</code>
                <ul>
                <li>MinGW 7.3.0 64 bit: <code>C:\Qt\5.12.5\mingw73_64</code></li>
                <li>MinGW 7.3.0 32 bit: <code>C:\Qt\5.12.5\mingw73_32</code></li>
                <li>msvc2015 64-bit: <code>C:\Qt\5.12.5\msvc2015_64</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.11.3: <code>C:\Qt\5.11.3</code> (<code>C:\Qt\5.11</code> mapped to <code>C:\Qt\5.11.3</code> for backward compatibility)
                <ul>
                <li>MinGW 5.3.0 32 bit: <code>C:\Qt\5.11.3\mingw53_32</code></li>
                <li>msvc2015 32-bit: <code>C:\Qt\5.11.3\msvc2015</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.11.3: <code>C:\Qt\5.11.3</code> (<code>C:\Qt\5.11</code> mapped to <code>C:\Qt\5.11.3</code> for backward compatibility)
                <ul>
                <li>msvc2015 64-bit: <code>C:\Qt\5.11.3\msvc2015_64</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.10.1: <code>C:\Qt\5.10.1</code> (<code>C:\Qt\5.10</code> mapped to <code>C:\Qt\5.10.1</code> for backward compatibility)
                <ul>
                <li>msvc2017 64-bit: <code>C:\Qt\5.10.1\msvc2017_64</code></li>
                <li>WinRT ARM v7: <code>C:\Qt\5.10.1\winrt_armv7_msvc2017</code></li>
                <li>WinRT 32-bit: <code>C:\Qt\5.10.1\winrt_x86_msvc2017</code></li>
                <li>WinRT 64-bit: <code>C:\Qt\5.10.1\winrt_x64_msvc2017</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.10.1: <code>C:\Qt\5.10.1</code> (<code>C:\Qt\5.10</code> mapped to <code>C:\Qt\5.10.1</code> for backward compatibility)
                <ul>
                <li>MinGW 5.3.0 32 bit: <code>C:\Qt\5.10.1\mingw53_32</code></li>
                <li>msvc2015 32-bit: <code>C:\Qt\5.10.1\msvc2015</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.10.1: <code>C:\Qt\5.10.1</code> (<code>C:\Qt\5.10</code> mapped to <code>C:\Qt\5.10.1</code> for backward compatibility)
                <ul>
                <li>msvc2015 64-bit: <code>C:\Qt\5.10.1\msvc2015_64</code></li>
                <li>msvc2013 64-bit: <code>C:\Qt\5.10.1\msvc2013_64</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.9.8: <code>C:\Qt\5.9.8</code> (<code>C:\Qt\5.9</code> mapped to <code>C:\Qt\5.9.8</code> for backward compatibility)
                <ul>
                <li>MinGW 5.3.0 32 bit: <code>C:\Qt\5.9.8\mingw53_32</code></li>
                <li>msvc2015 32-bit: <code>C:\Qt\5.9.8\msvc2015</code></li>
                <li>msvc2017 64-bit: <code>C:\Qt\5.9.8\msvc2017_64</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.9.8: <code>C:\Qt\5.9.8</code> (<code>C:\Qt\5.9</code> mapped to <code>C:\Qt\5.9.8</code> for backward compatibility)
                <ul>
                <li>msvc2017 64-bit: <code>C:\Qt\5.9.8\msvc2017_64</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="no"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.9.8: <code>C:\Qt\5.9.8</code> (<code>C:\Qt\5.9</code> mapped to <code>C:\Qt\5.9.8</code> for backward compatibility)
                <ul>
                <li>MinGW 5.3.0 32 bit: <code>C:\Qt\5.9.8\mingw53_32</code></li>
                <li>msvc2015 32-bit: <code>C:\Qt\5.9.8\msvc2015</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.9.8: <code>C:\Qt\5.9.8</code> (<code>C:\Qt\5.9</code> mapped to <code>C:\Qt\5.9.8</code> for backward compatibility)
                <ul>
                <li>msvc2015 64-bit: <code>C:\Qt\5.9.8\msvc2015_64</code></li>
                <li>msvc2013 64-bit: <code>C:\Qt\5.9.8\msvc2013_64</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.7.0: <code>C:\Qt\5.7.0</code>
                <ul>
                <li>MinGW 5.3.0 32 bit: <code>C:\Qt\5.7.0\mingw53_32</code></li>
                <li>msvc2015 32-bit: <code>C:\Qt\5.7.0\msvc2015</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.6.3: <code>C:\Qt\5.6.3</code> (<code>C:\Qt\5.6</code> mapped to <code>C:\Qt\5.6.3</code> for backward compatibility)
                <ul>
                <li>MinGW 4.9.0 32 bit: <code>C:\Qt\5.6.3\mingw49_32</code></li>
                <li>msvc2015 64-bit: <code>C:\Qt\5.6.3\msvc2015_64</code></li>
                <li>msvc2015 32-bit: <code>C:\Qt\5.6.3\msvc2015</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.6.3: <code>C:\Qt\5.6.3</code> (<code>C:\Qt\5.6</code> mapped to <code>C:\Qt\5.6.3</code> for backward compatibility)
                <ul>
                <li>msvc2013 64-bit: <code>C:\Qt\5.6.3\msvc2013_64</code></li>
                <li>msvc2013 32-bit: <code>C:\Qt\5.6.3\msvc2013</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="no"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt 5.8.0: <code>C:\Qt\5.8</code></li>
            <li>Qt 5.7.1: <code>C:\Qt\5.7</code></li>
            <li>Qt 5.6.2: <code>C:\Qt\5.6</code></li>
            <li>Qt 5.5: <code>C:\Qt\5.5</code></li>
            <li>Qt 5.4: <code>C:\Qt\5.4</code></li>
            <li>Qt 5.3: <code>C:\Qt\5.3</code></li>
            </ul>
        </td>
        <td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td>
    </tr>
    <tr>
        <td>Qt Installer Framework 2.0.5</td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>Qt Installer Framework 3.0.6</td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>Qt Installer Framework 3.1.1</td>
        <td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
            <li>Qt tools:
                <ul>
                <li>MinGW 7.3.0: <code>C:\Qt\Tools\mingw730_32</code></li>
                <li>MinGW 5.3.0: <code>C:\Qt\Tools\mingw530_32</code></li>
                <li>MinGW 4.9.2: <code>C:\Qt\Tools\mingw492_32</code></li>
                </ul>
            </li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <!-- Tools -->
    <tr>
        <th id="tools" class="section" colspan="5">Tools</th>
    </tr>
    <tr><td>curl 7.55.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>7-Zip 19.00</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Microsoft Web Platform Installer 5.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>CMake 3.15.5</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>CMake 3.12.2</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>NuGet 2.8.6</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>NuGet 4.9.2</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>NuGet 5.1.0</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Chocolatey v0.10.15</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GitVersion 4.0.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>FxCop 10.0</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>OpenSSL 1.0.2p (32-bit) (<code>C:\OpenSSL-Win32\bin</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>OpenSSL 1.0.2p (64-bit) (<code>C:\OpenSSL-Win64\bin</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>OpenSSL 1.1.0i (32-bit) (<code>C:\OpenSSL-v11-Win32\bin</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>OpenSSL 1.1.0i (64-bit) (<code>C:\OpenSSL-v11-Win64\bin</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>OpenSSL 1.1.1 (32-bit) (<code>C:\OpenSSL-v111-Win32\bin</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>OpenSSL 1.1.1 (64-bit) (<code>C:\OpenSSL-v111-Win64\bin</code>)</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>OpenSSL 1.0.2t (32-bit) (<code>C:\OpenSSL-Win32\bin</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>OpenSSL 1.0.2t (64-bit) (<code>C:\OpenSSL-Win64\bin</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>OpenSSL 1.1.0L (32-bit) (<code>C:\OpenSSL-v11-Win32\bin</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>OpenSSL 1.1.0L (64-bit) (<code>C:\OpenSSL-v11-Win64\bin</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>OpenSSL 1.1.1d (32-bit) (<code>C:\OpenSSL-v111-Win32\bin</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>OpenSSL 1.1.1d (64-bit) (<code>C:\OpenSSL-v111-Win64\bin</code>)</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>NSIS 3.04 (<code>C:\Program Files (x86)\NSIS</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>InnoSetup 5.5.9 Unicode (<code>C:\Program Files (x86)\Inno Setup 5</code>)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>InnoSetup 6.0.2 Unicode (<code>C:\Program Files (x86)\Inno Setup 6</code>)</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Coverity Scan 2017.07</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Coverity Scan 2019.03</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Yarn 1.9.4</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Yarn 1.16.0</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>vcpkg 2019.09.12-nohash</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Octo 6.2.3</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Octo 6.12.0</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Google Cloud SDK (`gcloud.exe` tool)</td><td class="no"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <!-- Test runners -->
    <tr>
        <th id="test-runners" class="section" colspan="5">Test runners</th>
    </tr>
    <tr><td>NUnit 2.6.4 in <code>C:\Tools\NUnit\bin</code></td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>NUnit 3.7.0 in <code>C:\Tools\NUnit3</code></td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>NUnit 3.9.0 in <code>C:\Tools\NUnit3</code></td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>xUnit 1.9.2 in <code>C:\Tools\xUnit</code></td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>xUnit 2.0.0 RTM in <code>C:\Tools\xUnit20</code></td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Machine.Specifications (MSpec)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Web browsers -->
    <tr>
        <th id="web-browsers" class="section" colspan="5">Web browsers</th>
    </tr>
    <tr><td>Internet Explorer 11</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Firefox 62</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Firefox 70</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Chrome 69</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Chrome 78</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Selenium testing -->
    <tr>
        <th id="selenium-testing" class="section" colspan="5">Selenium testing</th>
    </tr>
    <tr><td>Chrome Web Driver 2.41</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Chrome Web Driver 78</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Internet Explorer Web Driver 3.14</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Internet Explorer Web Driver 3.150.1</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Firefox Web Driver (geckodriver) 0.21.0</td><td class="yes"></td><td class="no"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Firefox Web Driver (geckodriver) 0.26</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Databases -->
    <tr>
        <th id="databases" class="section" colspan="5">Databases</th>
    </tr>
    <tr><td>SQL Server 2008 R2 SP2 Express Edition with Advanced Services (x64)</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>SQL Server 2012 SP1 Express with Advanced Services</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>SQL Server 2014 Express with Advanced Services</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>SQL Server 2016 Developer with SP1</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>SQL Server 2017 Developer</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>PostgreSQL 9.3 x64</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>PostgreSQL 9.4 x64</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>PostgreSQL 9.5 x64</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>PostgreSQL 9.6.9 x64</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>PostgreSQL 10.4 x64</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>PostgreSQL 10.6 x64</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>PostgreSQL 11.1 x64</td><td class="no"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>PostgreSQL ODBC drivers</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>MySQL 5.7</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>MySQL ODBC drivers</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>MongoDB 4.0.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Services -->
    <tr>
        <th id="services" class="section" colspan="5">Services</th>
    </tr>
    <tr><td>Internet Information Services (IIS) 8.5</td><td class="yes"></td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>Internet Information Services (IIS) 10</td><td class="no"></td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>MSMQ</td><td class="yes"></td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
</table>
