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
            <li><a href="#ruby">Ruby</a></li>
            <li><a href="#python">Python</a></li>
            <li><a href="#erlang">Erlang</a></li>
        </ul>
    </div>
    <div class="columns medium-4">
        <ul>
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
    <tr>
        <td>PowerShell Core 6.0.2</td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Docker -->
    <tr>
        <th id="docker" class="section" colspan="3">Docker</th>
    </tr>
    <tr>
        <td>Docker 18.06.0</td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Version control systems -->
    <tr>
        <th id="version-control-systems" class="section" colspan="3">Version control systems</th>
    </tr>
    <tr>
        <td>Git 2.18.0</td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Git Large File Storage (Git LFS) 2.5.1</td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Mercurial 4.4.1</td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Subversion 1.9.3</td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- .NET Framework -->
    <tr>
        <th id="net-framework" class="section" colspan="3">.NET Framework</th>
    </tr>
    <tr><td>.NET Core 2.0.0 runtime</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 2.0.3 runtime</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 2.0.4 runtime</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 2.0.5 runtime</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 2.0.6 runtime</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 2.0.7 runtime</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core 2.1.0 runtime</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.0.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.0.2</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.0.3</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.2</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.3</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.4</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.101</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.103</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.104</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.105</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.200</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.201</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Node.js -->
    <tr>
        <th id="node-js" class="section" colspan="3">Node.js</th>
    </tr>
    <tr>
        <td>
            <p><code>6.x</code> is default Node.js installed on Linux build workers.</p>
            <ul>
                <li>Node.js 10.9.0</li>
                <li>Node.js 9.11.2</li>
                <li>Node.js 8.11.4</li>
                <li>Node.js 7.10.1</li>
                <li>Node.js 6.14.4</li>
                <li>Node.js 5.12.0</li>
                <li>Node.js 4.9.1</li>
                <li>NVM</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Go -->
    <tr>
        <th id="golang" class="section" colspan="3">Go (Golang)</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Go Version Manager v1.0.22</li>
                <li>Go 1.11</li>
                <li>Go 1.10.4</li>
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
                <li>openJDK 8 1.8.0_151</li>
                <li>openJDK 9 9.0.4</li>
                <li>openJDK 10.0.2</li>
                <li>openJDK 11 build 28</li>
                <li>openJDK 12 early access 9</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Mono -->
    <tr>
        <th id="mono" class="section" colspan="3">Mono</th>
    </tr>
    <tr><td>Mono 5.14.0.177</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Visual C# compiler csc 2.7.0.62620</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Mono C# compiler 5.14.0.177</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Mono ASP.NET Web Server xsp4 0.4.0.0</td><td class="yes"></td><td class="yes"></td></tr>
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
                <li>Ruby 2.3.7</li>
                <li>Ruby 2.4.4</li>
                <li>Ruby 2.5.1</li>
                <li>Ruby HEAD 2.6.0dev</li>
                <li>RVM 1.29.4</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Python -->
    <tr>
        <th id="python" class="section" colspan="3">Python</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Python 2.7.12 (system)</li>
                <li>Python 3.5.2 (system)</li>
                <li>Python 2.7.15</li>
                <li>Python 3.4.8</li>
                <li>Python 3.5.5</li>
                <li>Python 3.6.6</li>
                <li>Python 3.7.0</li>
                <li>virtualenv 15.0.1</li>
                <li>pip 10.0.1</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Erlang -->
    <tr>
        <th id="erlang" class="section" colspan="3">Erlang</th>
    </tr>
    <tr>
        <td>Erlang</td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Tools -->
    <tr>
        <th id="tools" class="section" colspan="3">Tools</th>
    </tr>
    <tr><td>Yarn 1.9.4</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>p7zip 16.02 (<code>7za</code> utility is in PATH)</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>tcl 8.6.0+9</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>wget 1.17.1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>curl 7.47.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>AWS CLI 1.16.8</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>localstack 0.8.7</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Azure CLI-cli 2.0.45</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Packer 1.2.5</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>VirtualBox 5.2.18</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Web browsers -->
    <tr>
        <th id="web-browsers" class="section" colspan="3">Web browsers</th>
    </tr>
    <tr><td>Firefox 62.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Chrome 69.0.3497.81</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Databases -->
    <tr>
        <th id="databases" class="section" colspan="3">Databases</th>
    </tr>
    <tr><td>SQL Server 2017 14.0.3035.2-1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>PostgreSQL 10+192</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>MySQL 5.7.23</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>MongoDB 3.2.20</td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Services -->
    <tr>
        <th id="services" class="section" colspan="3">Services</th>
    </tr>
    <tr><td>Redis 4.0.11</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>RabbitMQ 3.6.15-1</td><td class="yes"></td><td class="yes"></td></tr>
</table>