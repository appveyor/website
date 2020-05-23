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
            <li><a href="#net-core">.NET Core</a></li>
            <li><a href="#node-js">Node.js</a></li>
        </ul>
    </div>
    <div class="columns medium-4">
        <ul>
            <li><a href="#qt">Qt</a></li>
            <li><a href="#golang">Go (Golang)</a></li>
            <li><a href="#java">Java SE Development Kit (JDK)</a></li>
            <li><a href="#mono">Mono</a></li>
            <li><a href="#compilers">Compilers</a></li>
            <li><a href="#ruby">Ruby</a></li>
        </ul>
    </div>
    <div class="columns medium-4">
        <ul>
            <li><a href="#python">Python</a></li>
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
        <th class="rotate"><span>Ubuntu2004</span></th>
    </tr>
    <tr>
        <th id="operating-system" class="section" colspan="4">Operating system</th>
    </tr>
    <tr>
        <td>Ubuntu 20.04 LTS (Focal Fossa)</td>
        <td class="no"></td>
        <td class="no"></td>
        <td class="yes"></td>
    </tr>
    <tr>
        <td>Ubuntu 18.04.4 LTS (Bionic Beaver)</td>
        <td class="yes"></td>
        <td class="no"></td>
        <td class="no"></td>
    </tr>
    <tr>
        <td>Ubuntu 16.04.6 LTS (Xenial Xerus)</td>
        <td class="no"></td>
        <td class="yes"></td>
        <td class="no"></td>
    </tr>
    <tr>
        <th id="powershell" class="section" colspan="4">PowerShell</th>
    </tr>
    <tr><td>PowerShell Core 7.0.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Docker -->
    <tr>
        <th id="docker" class="section" colspan="4">Docker</th>
    </tr>
    <tr><td>Docker 19.03.9</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Version control systems -->
    <tr>
        <th id="version-control-systems" class="section" colspan="4">Version control systems</th>
    </tr>
    <tr><td>Git 2.26.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Git Large File Storage (Git LFS) 2.11.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Mercurial</td><td class="text-center">4.5.3</td><td class="text-center">4.4.1</td><td class="text-center">5.3.1</td></tr>
    <tr><td>Subversion</td><td class="text-center">1.9.7</td><td class="text-center">1.9.3</td><td class="text-center">1.13.0</td></tr>
    <!-- .NET Framework -->
    <tr>
        <th id="net-core" class="section" colspan="4">.NET Core</th>
    </tr>
    <tr><td>.NET Core SDK 5.0.100-preview.3 (v5.0.0-preview.3 runtime) - Preview</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 3.1.300 (3.1.4 runtime) - LTS</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 3.0.103 (3.0.3 runtime) - EOL</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.2.402 (2.2.8 runtime) - EOL</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.805 (2.1.17 runtime) - LTS</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.202 (2.0.9 runtime) - EOL</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 1.1.14 (1.1.13, 1.0.16 runtimes) - EOL</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Node.js -->
    <tr>
        <th id="node-js" class="section" colspan="4">Node.js</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Node Version Manager (<code>nvm</code>) 0.34.0</li>
                <li>Node.js 14.3.0</li>
                <li>Node.js 13.14.0</li>
                <li>Node.js 12.16.3</li>
                <li>Node.js 11.15.0</li>
                <li>Node.js 10.20.1</li>
                <li>Node.js 9.11.2</li>
                <li>Node.js 8.17.0 (default)</li>
                <li>Node.js 7.10.1</li>
                <li>Node.js 6.17.1</li>
                <li>Node.js 5.12.0</li>
                <li>Node.js 4.9.1</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Qt -->
    <tr>
        <th id="qt" class="section" colspan="4">Qt</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>5.14.2 in <code>$HOME/Qt/5.14.2/gcc_64/bin</code></li>
                <li>5.12.8 in <code>$HOME/Qt/5.12.8/gcc_64/bin</code></li>
            </ul>
            <p>Links for latest and major versions:</p>
            <ul>
                <li><code>$HOME/Qt/latest</code> &rarr; <code>$HOME/Qt/5.14.2</code></li>
                <li><code>$HOME/Qt/5.14</code> &rarr; <code>$HOME/Qt/5.14.2</code></li>
                <li><code>$HOME/Qt/5.12</code> &rarr; <code>$HOME/Qt/5.12.8</code></li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Go -->
    <tr>
        <th id="golang" class="section" colspan="4">Go (Golang)</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Go Version Manager (<code>gvm</code>) v1.0.22</li>
                <li>Go 1.14.2</li>
                <li>Go 1.13.10</li>
                <li>Go 1.12.17</li>
                <li>Go 1.11.13</li>
                <li>Go 1.10.8</li>
                <li>Go 1.9.7</li>
                <li>Go 1.8.7</li>
                <li>Go 1.7.6</li>
                <li>Go 1.4</li>
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
                <li>OpenJDK 15 early access 10</li>
                <li>OpenJDK 14</li>
                <li>OpenJDK 13 (13.0.2)</li>
                <li>OpenJDK 12 (12.0.2)</li>
                <li>OpenJDK 11 build 11+28</li>
                <li>OpenJDK 10 build 10+44</li>
                <li>OpenJDK 9 (9.0.4)</li>
                <li>OpenJDK 8 (1.8.0_212)</li>
                <li>OpenJDK 7 (1.7.0_75)</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- mono -->
    <tr>
        <th id="mono" class="section" colspan="4">Mono</th>
    </tr>
    <tr><td>Mono 6.10.0.104</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Mono 6.8.0.105</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Compilers -->
    <tr>
        <th id="compilers" class="section" colspan="4">Compilers</th>
    </tr>
    <tr><td>LLVM (Clang) 10.0.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>LLVM (Clang) 9.0.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 9.3.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 8.4.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 7.5.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 6.5.0</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>GCC 5.5.0</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Ruby -->
    <tr>
        <th id="ruby" class="section" colspan="4">Ruby</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Ruby Version Manager (<code>rvm</code>) 1.29.9</li>
                <li>Ruby 2.7.0</li>
                <li>Ruby 2.6.5</li>
                <li>Ruby 2.5.7</li>
                <li>Ruby 2.4.9</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Ruby 2.3.8</li>
                <li>Ruby 2.2.10</li>
                <li>Ruby 2.1.10</li>
                <li>Ruby 2.0.0-p648</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <!-- Python -->
    <tr>
        <th id="python" class="section" colspan="4">Python</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Python 3.9.0a6 (<code>$HOME/venv3.9.0</code> and <code>$HOME/venv3.9</code>)</li>
                <li>Python 3.8.2 (<code>$HOME/venv3.8.2</code> and <code>$HOME/venv3.8</code>)</li>
                <li>Python 3.7.7 (<code>$HOME/venv3.7.7</code> and <code>$HOME/venv3.7</code>)</li>
                <li>Python 3.6.10 (<code>$HOME/venv3.6.10</code> and <code>$HOME/venv3.6</code>)</li>
                <li>Python 3.5.9 (<code>$HOME/venv3.5.9</code> and <code>$HOME/venv3.5</code>)</li>
                <li>Python 3.4.10 (<code>$HOME/venv3.4.10</code> and <code>$HOME/venv3.4</code>)</li>
                <li>Python 2.7.18 (<code>$HOME/venv2.7.18</code> and <code>$HOME/venv2.7</code>)</li>
                <li>Python 2.6.9 (<code>$HOME/venv2.6.9</code> and <code>$HOME/venv2.6</code>)</li>
                <li>virtualenv 20.0.20</li>
                <li>pip 20.1</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Erlang -->
    <tr>
        <th id="erlang" class="section" colspan="4">Erlang</th>
    </tr>
    <tr><td>Erlang 22.3.4.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Tools -->
    <tr>
        <th id="tools" class="section" colspan="4">Tools</th>
    </tr>
    <tr><td>Yarn 1.22.4</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>p7zip 16.02 (<code>7za</code> utility is in PATH)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>tcl 8.6.0+9</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>wget</td><td class="text-center">1.19.4</td><td class="text-center">1.17.1</td><td class="text-center">1.20.3</td></tr>
    <tr><td>curl</td><td class="text-center">7.58.0</td><td class="text-center">7.47.0</td><td class="text-center">7.68.0</td></tr>
    <tr><td>AWS CLI 1.18.64</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>localstack 0.11.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Azure CLI 2.6.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Google Cloud SDK 293.0.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Packer 1.5.4</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>VirtualBox 6.1.8</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>CMake 3.17.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>vcpkg 2020.02.04</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Ninja 1.8.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Gradle 4.4.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Web browsers -->
    <tr>
        <th id="web-browsers" class="section" colspan="4">Web browsers</th>
    </tr>
    <tr><td>Firefox 76.0.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Chrome 83.0.4103.61</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Databases -->
    <tr>
        <th id="databases" class="section" colspan="4">Databases</th>
    </tr>
    <tr><td>SQL Server 2017 15.0.4033</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>PostgreSQL 12+213</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>MySQL</td><td class="text-center">5.7.30</td><td class="text-center">5.7.30</td><td class="text-center">8.0.20</td></tr>
    <tr><td>MongoDB 4.2.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Services -->
    <tr>
        <th id="services" class="section" colspan="4">Services</th>
    </tr>
    <tr><td>OctoTools 6.17.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Redis 5.0.7</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>RabbitMQ 3.8.3-1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Configuration -->
    <tr>
        <th id="configuration" class="section" colspan="4">Configuration</th>
    </tr>
    <tr><td>Default Locale: LANG</td><td>C.UTF&#8209;8</td><td>C.UTF&#8209;8</td><td>C.UTF&#8209;8</td></tr>
</table>