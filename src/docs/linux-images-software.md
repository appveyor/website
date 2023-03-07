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
            <li><a href="#qt">Qt</a></li>
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
            <li><a href="#mobile">Mobile SDKs</a></li>
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
    <tr><td>PowerShell Core 7.3.3</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Docker -->
    <tr>
        <th id="docker" class="section" colspan="4">Docker</th>
    </tr>
    <tr><td>Docker 23.0.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Version control systems -->
    <tr>
        <th id="version-control-systems" class="section" colspan="4">Version control systems</th>
    </tr>
    <tr><td>Git 2.39.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Git Large File Storage (Git LFS) 3.2.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Mercurial</td><td class="text-center">4.5.3</td><td class="text-center">4.4.1</td><td class="text-center">5.3.1</td></tr>
    <tr><td>Subversion</td><td class="text-center">1.9.7</td><td class="text-center">1.9.3</td><td class="text-center">1.13.0</td></tr>
    <!-- .NET Framework -->
    <tr>
        <th id="net-core" class="section" colspan="4">.NET Core</th>
    </tr>
    <tr><td>.NET Core SDK 7.0.201 (7.0.3 runtime)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 6.0.406 (6.0.14 runtime)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 5.0.408 (5.0.17 runtime)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 3.1.420 (3.1.26 runtime) - LTS</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 3.0.103 (3.0.3 runtime) - EOL</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.2.402 (2.2.8 runtime) - EOL</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET Core SDK 2.1.818 (2.1.30 runtime) - EOL</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
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
                <li>Node.js 19.7.0</li>
                <li>Node.js 18.14.2</li>
                <li>Node.js 17.9.1</li>
                <li>Node.js 16.19.1</li>
                <li>Node.js 15.14.0</li>
                <li>Node.js 14.21.3</li>
                <li>Node.js 13.14.0</li>
                <li>Node.js 12.22.12</li>
                <li>Node.js 11.15.0</li>
                <li>Node.js 10.24.1</li>
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
                <li>6.4.2 in <code>$HOME/Qt/6.4.2/gcc_64/bin</code></li>
                <li>6.3.1 in <code>$HOME/Qt/6.3.1/gcc_64/bin</code></li>
                <li>6.2.4 in <code>$HOME/Qt/6.2.4/gcc_64/bin</code></li>
                <li>5.15.2 in <code>$HOME/Qt/5.15.2/gcc_64/bin</code></li>
            </ul>
            <p>Links for latest and major versions:</p>
            <ul>
                <li><code>$HOME/Qt/6.4</code> &rarr; <code>$HOME/Qt/6.4.2</code></li>
                <li><code>$HOME/Qt/6.3</code> &rarr; <code>$HOME/Qt/6.3.1</code></li>
                <li><code>$HOME/Qt/6.2</code> &rarr; <code>$HOME/Qt/6.2.4</code></li>
                <li><code>$HOME/Qt/latest</code> &rarr; <code>$HOME/Qt/5.15.2</code></li>
                <li><code>$HOME/Qt/5.15</code> &rarr; <code>$HOME/Qt/5.15.2</code></li>
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
                <li>Go 1.20.1</li>
                <li>Go 1.19.6</li>
                <li>Go 1.18.10</li>
                <li>Go 1.17.13</li>
                <li>Go 1.16.15</li>
                <li>Go 1.15.15</li>
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
                <li>OpenJDK 18</li>
                <li>OpenJDK 15</li>
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
    <tr><td>Mono 6.12.0.182</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Compilers -->
    <tr>
        <th id="compilers" class="section" colspan="4">Compilers</th>
    </tr>
    <tr><td>LLVM (Clang) 14.0.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>LLVM (Clang) 13.0.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>LLVM (Clang) 12.0.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>LLVM (Clang) 11.0.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
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
                <li>Ruby Version Manager (<code>rvm</code>) 1.29.12</li>
                <li>Ruby 3.2.1</li>
                <li>Ruby 3.1.3</li>
                <li>Ruby 3.0.0</li>
                <li>Ruby 2.7.2</li>
                <li>Ruby 2.6.6</li>
                <li>Ruby 2.5.8</li>
                <li>Ruby 2.4.10</li>
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
                <li>Python 3.11.2 (<code>$HOME/venv3.11.2</code> and <code>$HOME/venv3.11</code>)</li>
                <li>Python 3.10.10 (<code>$HOME/venv3.10.10</code> and <code>$HOME/venv3.10</code>)</li>
                <li>Python 3.9.16 (<code>$HOME/venv3.9.16</code> and <code>$HOME/venv3.9</code>)</li>
                <li>Python 3.8.16 (<code>$HOME/venv3.8.16</code> and <code>$HOME/venv3.8</code>)</li>
                <li>Python 3.7.16 (<code>$HOME/venv3.7.16</code> and <code>$HOME/venv3.7</code>)</li>
                <li>Python 3.6.15 (<code>$HOME/venv3.6.15</code> and <code>$HOME/venv3.6</code>)</li>
                <li>Python 3.5.10 (<code>$HOME/venv3.5.10</code> and <code>$HOME/venv3.5</code>)</li>
                <li>Python 3.4.10 (<code>$HOME/venv3.4.10</code> and <code>$HOME/venv3.4</code>)</li>
                <li>Python 2.7.18 (<code>$HOME/venv2.7.18</code> and <code>$HOME/venv2.7</code>)</li>
                <li>Python 2.6.9 (<code>$HOME/venv2.6.9</code> and <code>$HOME/venv2.6</code>)</li>
                <li>virtualenv 20.4.4</li>
                <li>pip 20.3.4</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Mobile -->
    <tr>
        <th id="mobile" class="section" colspan="4">Mobile SDKs</th>
    </tr>
    <tr><td>Flutter 3.7.6</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Android SDK 30.0.3</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Erlang -->
    <tr>
        <th id="erlang" class="section" colspan="4">Erlang</th>
    </tr>
    <tr><td>Erlang 25.0.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Tools -->
    <tr>
        <th id="tools" class="section" colspan="4">Tools</th>
    </tr>
    <tr><td>Yarn 1.22.5</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>p7zip 16.02 (<code>7za</code> utility is in PATH)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>tcl 8.6.0+9</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>wget</td><td class="text-center">1.19.4</td><td class="text-center">1.17.1</td><td class="text-center">1.20.3</td></tr>
    <tr><td>curl</td><td class="text-center">7.68.0</td><td class="text-center">7.47.0</td><td class="text-center">7.68.0</td></tr>
    <tr><td>AWS CLI 1.19.112</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Azure CLI 2.45.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Google Cloud SDK 392.0.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Packer 1.8.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>VirtualBox 6.1.16</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>CMake 3.25.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>vcpkg 2022-07-12</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Doxygen</td><td class="text-center">1.8.18</td><td class="text-center">1.8.17</td><td class="text-center">1.9.4</td></tr>
    <tr><td>Ninja 1.8.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Gradle 4.4.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Web browsers -->
    <tr>
        <th id="web-browsers" class="section" colspan="4">Web browsers</th>
    </tr>
    <tr><td>Firefox 110.0.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Chrome 107.0.5304.87-1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Databases -->
    <tr>
        <th id="databases" class="section" colspan="4">Databases</th>
    </tr>
    <tr><td>SQL Server 2017 15.0.4153.1-6</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>PostgreSQL 15.0-1.pgdg20.04+1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>MySQL</td><td class="text-center">5.7.35</td><td class="text-center">5.7.35</td><td class="text-center">8.0.26</td></tr>
    <tr><td>MongoDB</td><td class="text-center">4.2.21</td><td class="text-center">5.0.2</td><td class="text-center">5.0.2</td></tr>
    <!-- Services -->
    <tr>
        <th id="services" class="section" colspan="4">Services</th>
    </tr>
    <tr><td>OctoTools 9.0.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Redis 6.2.6</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>RabbitMQ 3.10.5-1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Configuration -->
    <tr>
        <th id="configuration" class="section" colspan="4">Configuration</th>
    </tr>
    <tr><td>Default Locale: LANG</td><td>C.UTF&#8209;8</td><td>C.UTF&#8209;8</td><td>C.UTF&#8209;8</td></tr>
</table>
