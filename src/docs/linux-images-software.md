---
layout: docs
title: Software pre-installed on Linux build VMs
---

<!-- markdownlint-disable MD022 MD032 -->
# Software pre-installed on Linux build VMs
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

The history of Ubuntu image updates can be found [here](/updates/).

<div class="row">
    <div class="columns medium-4">
        <ul>
            <li><a href="#operating-system">Operating system</a></li>
            <li><a href="#powershell">PowerShell</a></li>
            <li><a href="#docker">Docker</a></li>
            <li><a href="#version-control-systems">Version control systems</a></li>
            <li><a href="#net-framework">.NET Framework</a></li>
            <li><a href="#node-js">Node.js</a></li>
        </ul>
    </div>
    <div class="columns medium-4">
        <ul>
            <li><a href="#golang">Go (Golang)</a></li>
            <li><a href="#java">Java SE Development Kit (JDK)</a></li>
            <li><a href="#mono">Mono</a></li>
            <li><a href="#compilers">Compilers</a></li>
            <li><a href="#ruby">Ruby</a></li>
            <li><a href="#python">Python</a></li>
        </ul>
    </div>
    <div class="columns medium-4">
        <ul>
            <li><a href="#erlang">Erlang</a></li>
            <li><a href="#tools">Tools</a></li>
            <li><a href="#web-browsers">Web browsers</a></li>
            <li><a href="#databases">Databases</a></li>
            <li><a href="#services">Services</a></li>
        </ul>
    </div>
</div>

<table class="software-list">
    <tr>
        <th>Software installed / Build worker image</th>
        <th class="rotate"><span>Ubuntu</span></th>
        <th class="rotate"><span>Ubuntu1804</span></th>
    </tr>
    <tr>
        <th id="operating-system" class="section" colspan="3">Operating system</th>
    </tr>
    <tr>
        <td>Ubuntu 16.04.4 LTS (Xenial Xerus)</td>
        <td class="yes"></td>
        <td class="no"></td>
    </tr>
    <tr>
        <td>Ubuntu 18.04 LTS (Bionic Beaver)</td>
        <td class="no"></td>
        <td class="yes"></td>
    </tr>
    <tr>
        <th id="powershell" class="section" colspan="3">PowerShell</th>
    </tr>
    <tr><td>PowerShell Core 6.1.1</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Docker -->
    <tr>
        <th id="docker" class="section" colspan="3">Docker</th>
    </tr>
    <tr>
        <td>Docker 18.09.0</td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Version control systems -->
    <tr>
        <th id="version-control-systems" class="section" colspan="3">Version control systems</th>
    </tr>
    <tr>
        <td>Git 2.20.0</td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Git Large File Storage (Git LFS) 2.6.1</td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr><td>Mercurial 4.4.1</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Mercurial 4.5.3</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Subversion 1.9.3</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Subversion 1.9.7</td><td class="no"></td><td class="yes"></td></tr>
    <!-- .NET Framework -->
    <tr>
        <th id="net-framework" class="section" colspan="3">.NET Framework</th>
    </tr>
    <tr><td>.NET Core 2.0.0 runtime</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core 2.0.3 runtime</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core 2.0.4 runtime</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core 2.0.5 runtime</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core 2.0.6 runtime</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core 2.0.7 runtime</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 2.0.9 runtime</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core 2.1.0-rc1 runtime</td><td class="no"></td><td class="no"></td></tr>
    <tr><td>.NET Core 2.1.3 runtime</td><td class="no"></td><td class="no"></td></tr>
    <tr><td>.NET Core 2.1.4 runtime</td><td class="no"></td><td class="no"></td></tr>
    <tr><td>.NET Core 2.1 runtime version 2.1.6</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 2.2 runtime version 2.2.1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.0.0</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.0.2</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.0.3</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.2</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.3</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.4</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.101</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.103</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.104</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.105</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.200</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.201</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.202</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.300-rc1</td><td class="no"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.300-preview2</td><td class="no"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1 version 2.1.503</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.2 version 2.1.102</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Node.js -->
    <tr>
        <th id="node-js" class="section" colspan="3">Node.js</th>
    </tr>
    <tr>
        <td>
            <p><code>6.x</code> is default Node.js installed on Linux build workers.</p>
        </td>
        <td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <p><code>8.x</code> is default Node.js installed on Linux build workers.</p>
        </td>
        <td class="no"></td><td class="yes"></td>
    </tr>
    <tr><td>Node.js 11.6.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 10.15.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 9.11.2</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 8.15.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 8.10.0 (installed in system)</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Node.js 7.10.1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 6.16.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 6.14.4 (installed in system)</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Node.js 5.12.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 4.9.1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node Version Manager (NVM) 4.9.1</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Go -->
    <tr>
        <th id="golang" class="section" colspan="3">Go (Golang)</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Go Version Manager (GVM) v1.0.22</li>
                <li>Go 1.11.2</li>
                <li>Go 1.10.5</li>
                <li>Go 1.9.7</li>
                <li>Go 1.8.7</li>
                <li>Go 1.7.6</li>
                <li>Go 1.4</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Java -->
    <tr>
        <th id="java" class="section" colspan="3">Java SE Development Kit (JDK)</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>openJDK 7 1.7.0_95</li>
                <li>openJDK 8 1.8.0_181</li>
                <li>openJDK 9 9.0.4</li>
                <li>openJDK 10.0.2</li>
                <li>openJDK 11.0.1</li>
                <li>openJDK 12 early access 27</li>
                <li>openJDK 13 early access 3</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Mono -->
    <tr>
        <th id="mono" class="section" colspan="3">Mono</th>
    </tr>
    <tr><td>Mono 5.16.0.225</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Visual C# compiler csc 2.8.2.62916</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Mono C# compiler 5.16.0.225</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Mono ASP.NET Web Server xsp4 0.4.0.0</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Compilers -->
    <tr>
        <th id="compilers" class="section" colspan="3">Compilers</th>
    </tr>
    <tr><td>clang 6.0.1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 5.5.0</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>GCC 7.3.0</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>GCC 7.4.0</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>GCC 8.1.0</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>GCC 8.2.0</td><td class="no"></td><td class="yes"></td></tr>
    <!-- Ruby -->
    <tr>
        <th id="ruby" class="section" colspan="3">Ruby</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Ruby 2.0.0-p648</li>
                <li>Ruby 2.1.10</li>
                <li>Ruby 2.2.10</li>
                <li>Ruby 2.3.8</li>
                <li>Ruby 2.4.5</li>
                <li>Ruby 2.5.3</li>
                <li>Ruby 2.6.0</li>
                <li>Ruby HEAD 2.7.0dev</li>
                <li>Ruby Version Manager (RVM) 1.29.7</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Python -->
    <tr>
        <th id="python" class="section" colspan="3">Python</th>
    </tr>
    <tr><td>Python 2 (installed in system)</td><td>2.7.12</td><td>2.7.15</td></tr>
    <tr><td>Python 3 (installed in system)</td><td>3.5.2</td><td>3.6.7</td></tr>
    <tr>
        <td>
            <ul>
                <li>Python 2.7.15</li>
                <li>Python 3.4.9</li>
                <li>Python 3.5.6</li>
                <li>Python 3.6.7</li>
                <li>Python 3.7.0</li>
                <li>Python 3.7.1</li>
                <li>virtualenv 15.1.0</li>
                <li>pip 18.1</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Erlang -->
    <tr>
        <th id="erlang" class="section" colspan="3">Erlang</th>
    </tr>
    <tr><td>Erlang/OTP 18.3</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Erlang/OTP 20.2</td><td class="no"></td><td class="yes"></td></tr>
    <!-- Tools -->
    <tr>
        <th id="tools" class="section" colspan="3">Tools</th>
    </tr>
    <tr><td>Yarn 1.12.3</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>p7zip 16.02 (<code>7za</code> utility is in PATH)</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>tcl 8.6.0+9</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>wget 1.17.1</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>wget 1.19.4</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>curl 7.47.0</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>curl 7.58.0</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>AWS CLI 1.16.87</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>localstack 0.8.10</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Azure CLI-cli 2.0.54</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Packer 1.3.3</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>VirtualBox 5.2.22</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>cmake 3.13.1</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Web browsers -->
    <tr>
        <th id="web-browsers" class="section" colspan="3">Web browsers</th>
    </tr>
    <tr><td>Firefox 64.0.2</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Chrome 71.0.3578</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Databases -->
    <tr>
        <th id="databases" class="section" colspan="3">Databases</th>
    </tr>
    <tr><td>SQL Server 2017 14.0.3048</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>SQL Server 2017 14.0.3045</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>PostgreSQL 11+197</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>MySQL 5.7.24</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>MongoDB 3.2.21</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>MongoDB 3.6.3</td><td class="no"></td><td class="yes"></td></tr>
    <!-- Services -->
    <tr>
        <th id="services" class="section" colspan="3">Services</th>
    </tr>
    <tr><td>Redis 5.0.3</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>RabbitMQ 3.6.15-1</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Configuration -->
    <tr>
        <th id="configuration" class="section" colspan="3">Configuration</th>
    </tr>
    <tr><td>Default Locale: LANG</td><td>C.UTF-8</td><td>C.UTF-8</td></tr>
</table>