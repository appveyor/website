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
        <th class="rotate"><span>Ubuntu1604</span></th>
    </tr>
    <tr>
        <th id="operating-system" class="section" colspan="3">Operating system</th>
    </tr>
    <tr>
        <td>Ubuntu 18.04.4 LTS (Bionic Beaver)</td>
        <td class="yes"></td>
        <td class="no"></td>
    </tr>
    <tr>
        <td>Ubuntu 16.04.6 LTS (Xenial Xerus)</td>
        <td class="no"></td>
        <td class="yes"></td>
    </tr>
    <tr>
        <th id="powershell" class="section" colspan="3">PowerShell</th>
    </tr>
    <tr><td>PowerShell Core 7.0.0</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Docker -->
    <tr>
        <th id="docker" class="section" colspan="3">Docker</th>
    </tr>
    <tr>
        <td>Docker 19.03.8</td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Version control systems -->
    <tr>
        <th id="version-control-systems" class="section" colspan="3">Version control systems</th>
    </tr>
    <tr>
        <td>Git 2.26.0</td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Git Large File Storage (Git LFS) 2.10.0</td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr><td>Mercurial 4.4.1</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Mercurial 4.5.3</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Subversion 1.9.3</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Subversion 1.9.7</td><td class="yes"></td><td class="no"></td></tr>
    <!-- .NET Framework -->
    <tr>
        <th id="net-framework" class="section" colspan="3">.NET Framework</th>
    </tr>
    <tr><td>.NET Core SDK 1.1.5</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 1.1.6</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 1.1.7</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 1.1.8</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 1.1.9</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 1.1.10</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 1.1.11</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 1.1.12</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.0.0</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.0.2</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.0.3</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.2</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.3</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.4</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.101</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.103</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.104</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.105</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.200</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.201</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.202</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.805</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.2.402</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 3.0.103</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 3.1.201</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 5.0.100-preview.4.20202.8</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Node.js -->
    <tr>
        <th id="node-js" class="section" colspan="3">Node.js</th>
    </tr>
    <tr>
        <td>
            <p><code>6.x</code> is default Node.js installed on Linux build workers.</p>
        </td>
        <td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>
            <p><code>8.x</code> is default Node.js installed on Linux build workers.</p>
        </td>
        <td class="yes"></td><td class="no"></td>
    </tr>
    <tr><td>Node.js 13.12.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 12.16.1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 11.15.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 10.19.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 9.11.2</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 8.17.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 8.10.0 (installed in system)</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Node.js 7.10.1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 6.17.1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 6.17.1 (installed in system)</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Node.js 5.12.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 4.9.1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node Version Manager (NVM) 0.34.0</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Go -->
    <tr>
        <th id="golang" class="section" colspan="3">Go (Golang)</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Go Version Manager (GVM) v1.0.22</li>
                <li>Go 1.14</li>
                <li>Go 1.13.8</li>
                <li>Go 1.12.17</li>
                <li>Go 1.11.13</li>
                <li>Go 1.10.8</li>
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
                <li>openJDK 7 (1.7.0_75)</li>
                <li>openJDK 8 (1.8.0_212)</li>
                <li>openJDK 9 (9.0.4)</li>
                <li>openJDK 10 build 10+44</li>
                <li>openJDK 11 build 11+28</li>
                <li>openJDK 12 (12.0.2)</li>
                <li>openJDK 13 (13.0.2)</li>
                <li>openJDK 14</li>
                <li>openJDK 15 early access 10</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- mono -->
    <tr>
        <th id="mono" class="section" colspan="3">Mono</th>
    </tr>
    <tr><td>Mono 6.8.0.105</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Visual C# compiler csc 3.5.0-beta1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Mono C# compiler 6.8.0.105</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Mono ASP.NET Web Server xsp4 0.4.0.0</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Compilers -->
    <tr>
        <th id="compilers" class="section" colspan="3">Compilers</th>
    </tr>
    <tr><td>clang 9.0.1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 5.5.0</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>GCC 6.5.0</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>GCC 7.4.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 8.3.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 9.1.0</td><td class="yes"></td><td class="yes"></td></tr>
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
                <li>Ruby 2.4.6</li>
                <li>Ruby 2.5.5</li>
                <li>Ruby 2.6.3</li>
                <li>Ruby 2.7.0</li>
                <li>Ruby HEAD 2.7.0dev</li>
                <li>Ruby Version Manager (RVM) 1.29.9</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Python -->
    <tr>
        <th id="python" class="section" colspan="3">Python</th>
    </tr>
    <tr><td>Python 2 (installed in system)</td><td>2.7.15</td><td>2.7.12</td></tr>
    <tr><td>Python 3 (installed in system)</td><td>3.6.8</td><td>3.5.2</td></tr>
    <tr>
        <td>
            <ul>
                <li>Python 2.7.17 (<code>~/venv2.7.17</code>)</li>
                <li>Python 3.4.10 (<code>~/venv3.4.10</code>)</li>
                <li>Python 3.5.9 (<code>~/venv3.5.9</code>)</li>
                <li>Python 3.6.10 (<code>~/venv3.6.10</code>)</li>
                <li>Python 3.7.0 (<code>~/venv3.7.0</code>)</li>
                <li>Python 3.7.1 (<code>~/venv3.7.1</code>)</li>
                <li>Python 3.7.2 (<code>~/venv3.7.2</code>)</li>
                <li>Python 3.7.3 (<code>~/venv3.7.3</code>)</li>
                <li>Python 3.7.4 (<code>~/venv3.7.4</code>)</li>
                <li>Python 3.7.5 (<code>~/venv3.7.5</code>)</li>
                <li>Python 3.8.0 (<code>~/venv3.8.0</code>)</li>
                <li>Python 3.8.1 (<code>~/venv3.8.1</code>)</li>
                <li>Python 3.8.2 (<code>~/venv3.8.2</code>)</li>
                <li>Python 3.9.0a4 (<code>~/venv3.9.0</code>)</li>
                <li>virtualenv 20.0.7</li>
                <li>pip 20.0.2</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Erlang -->
    <tr>
        <th id="erlang" class="section" colspan="3">Erlang</th>
    </tr>
    <tr><td>Erlang/OTP 18.3</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Erlang/OTP 20.2</td><td class="yes"></td><td class="no"></td></tr>
    <!-- Tools -->
    <tr>
        <th id="tools" class="section" colspan="3">Tools</th>
    </tr>
    <tr><td>Yarn 1.22.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>p7zip 16.02 (<code>7za</code> utility is in PATH)</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>tcl 8.6.0+9</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>wget 1.17.1</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>wget 1.19.4</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>curl 7.47.0</td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>curl 7.58.0</td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>AWS CLI 1.18.8</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>localstack 0.10.7</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Azure CLI-cli 2.1.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Google Cloud SDK 282.0.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Packer 1.5.4</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>VirtualBox 6.1.4</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>cmake 3.17.0-rc1</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Web browsers -->
    <tr>
        <th id="web-browsers" class="section" colspan="3">Web browsers</th>
    </tr>
    <tr><td>Firefox 73.0.1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Chrome 80.0.3987</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Databases -->
    <tr>
        <th id="databases" class="section" colspan="3">Databases</th>
    </tr>
    <tr><td>SQL Server 2017 15.0.4033</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>PostgreSQL 12+213</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>MySQL 5.7.29</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>MongoDB 4.2.2</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Services -->
    <tr>
        <th id="services" class="section" colspan="3">Services</th>
    </tr>
    <tr><td>OctoTools 6.17.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Redis 5.0.7</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>RabbitMQ 3.6.15-1</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Configuration -->
    <tr>
        <th id="configuration" class="section" colspan="3">Configuration</th>
    </tr>
    <tr><td>Default Locale: LANG</td><td>C.UTF-8</td><td>C.UTF-8</td></tr>
</table>