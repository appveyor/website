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
        <th class="rotate"><span>Ubuntu2004</span></th>
        <th class="rotate"><span>Ubuntu2404</span></th>
    </tr>
    <tr>
        <th id="operating-system" class="section" colspan="4">Operating system</th>
    </tr>
    <tr>
        <td>Ubuntu 22.04 LTS (Jammy Jellyfish)</td>
        <td class="yes"></td>
        <td class="no"></td>
        <td class="no"></td>
    </tr>
    <tr>
        <td>Ubuntu 20.04 LTS (Focal Fossa)</td>
        <td class="no"></td>
        <td class="yes"></td>
        <td class="no"></td>
    </tr>
    <tr>
        <td>Ubuntu 24.04 LTS (Noble Numbat)</td>
        <td class="no"></td>
        <td class="no"></td>
        <td class="yes"></td>
    </tr>
    <tr>
        <th id="powershell" class="section" colspan="4">PowerShell</th>
    </tr>
    <tr><td>PowerShell Core 7.5.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Docker -->
    <tr>
        <th id="docker" class="section" colspan="4">Docker</th>
    </tr>
    <tr><td>Docker</td><td class="text-center">29.5.1</td><td class="text-center">27.5.1</td><td class="text-center">29.5.1</td></tr>
    <!-- Version control systems -->
    <tr>
        <th id="version-control-systems" class="section" colspan="4">Version control systems</th>
    </tr>
    <tr><td>Git 2.48.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Git Large File Storage (Git LFS) 3.4.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Mercurial</td><td class="text-center">6.1.1</td><td class="text-center">6.1.1</td><td class="text-center">6.1.1</td></tr>
    <tr><td>Subversion</td><td class="text-center">1.14.1</td><td class="text-center">1.14.1</td><td class="text-center">1.14.1</td></tr>
    <!-- .NET Framework -->
    <tr>
        <th id="net-core" class="section" colspan="4">.NET Core</th>
    </tr>
    <tr><td>.NET SDK 10.0.300 (10.0.8 runtime)</td><td class="yes"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET SDK 9.0.314 (9.0.16 runtime)</td><td class="yes"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>.NET SDK 8.0.421 (8.0.27 runtime)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET SDK 7.0.410 (7.0.20 runtime)</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET SDK 6.0.428 (6.0.36 runtime)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET SDK 5.0.408 (5.0.17 runtime)</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET SDK 3.1.420 (3.1.26 runtime) - LTS</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>.NET SDK 3.0.103 (3.0.3 runtime) - EOL</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Node.js -->
    <tr>
        <th id="node-js" class="section" colspan="4">Node.js</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Node Version Manager (<code>nvm</code>)</li>
                <li>Node.js 24.15.0</li>
                <li>Node.js 23.11.1</li>
                <li>Node.js 22.22.3 (default)</li>
                <li>Node.js 21.7.3</li>
                <li>Node.js 20.20.2</li>
                <li>Node.js 19.9.0</li>
                <li>Node.js 18.20.8</li>
                <li>Node.js 17.9.1</li>
                <li>Node.js 16.20.2</li>
                <li>Node.js 15.14.0</li>
                <li>Node.js 14.21.3</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Node.js 13.14.0</li>
                <li>Node.js 12.22.12</li>
                <li>Node.js 11.15.0</li>
                <li>Node.js 10.24.1</li>
                <li>Node.js 9.11.2</li>
                <li>Node.js 8.17.0</li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <!-- Qt -->
    <tr>
        <th id="qt" class="section" colspan="4">Qt</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>6.9.1 in <code>$HOME/Qt/6.9.1/gcc_64/bin</code></li>
                <li>6.8.3 in <code>$HOME/Qt/6.8.3/gcc_64/bin</code></li>
                <li>6.5.3 in <code>$HOME/Qt/6.5.3/gcc_64/bin</code></li>
                <li>5.15.2 in <code>$HOME/Qt/5.15.2/gcc_64/bin</code></li>
            </ul>
            <p>Links for latest and major versions:</p>
            <ul>
                <li><code>$HOME/Qt/6.9</code> &rarr; <code>$HOME/Qt/6.9.1</code></li>
                <li><code>$HOME/Qt/6.8</code> &rarr; <code>$HOME/Qt/6.8.3</code></li>
                <li><code>$HOME/Qt/6.5</code> &rarr; <code>$HOME/Qt/6.5.3</code></li>
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
                <li>Go 1.25.10</li>
                <li>Go 1.24.13</li>
                <li>Go 1.23.12</li>
                <li>Go 1.22.12</li>
                <li>Go 1.21.13</li>
            </ul>
        </td>
        <td class="yes"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Go Version Manager (<code>gvm</code>) v1.0.22</li>
                <li>Go 1.24.5</li>
                <li>Go 1.23.11</li>
                <li>Go 1.22.12</li>
                <li>Go 1.21.12</li>
                <li>Go 1.20.14</li>
                <li>Go 1.19.13</li>
                <li>Go 1.18.10</li>
                <li>Go 1.17.13</li>
                <li>Go 1.16.15</li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <!-- Java -->
    <tr>
        <th id="java" class="section" colspan="4">Java SE Development Kit (JDK)</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>OpenJDK 26</li>
                <li>OpenJDK 25</li>
                <li>OpenJDK 21</li>
                <li>OpenJDK 17</li>
                <li>OpenJDK 11</li>
            </ul>
        </td>
        <td class="yes"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>
            <ul>
                <li>OpenJDK 23</li>
                <li>OpenJDK 22</li>
                <li>OpenJDK 21</li>
                <li>OpenJDK 18</li>
                <li>OpenJDK 15</li>
                <li>OpenJDK 14</li>
                <li>OpenJDK 13 (13.0.2)</li>
                <li>OpenJDK 12 (12.0.2)</li>
                <li>OpenJDK 11 build 11+28</li>
                <li>OpenJDK 10 build 10+44</li>
                <li>OpenJDK 9 (9.0.4)</li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>
            <ul>
                <li>OpenJDK 8 (1.8.0_212)</li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <!-- mono -->
    <tr>
        <th id="mono" class="section" colspan="4">Mono</th>
    </tr>
    <tr><td>Mono 6.12.0.200</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Compilers -->
    <tr>
        <th id="compilers" class="section" colspan="4">Compilers</th>
    </tr>
    <tr><td>LLVM (Clang) 22</td><td class="yes"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>LLVM (Clang) 21</td><td class="yes"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>LLVM (Clang) 20</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>LLVM (Clang) 19</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>LLVM (Clang) 18</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>LLVM (Clang) 17</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>LLVM (Clang) 16</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>LLVM (Clang) 15</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>LLVM (Clang) 14</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>LLVM (Clang) 13</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>GCC 14</td><td class="yes"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>GCC 13</td><td class="yes"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>GCC 12</td><td class="yes"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>GCC 11</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 10</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 9</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 8.4.0</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>GCC 7.5.0</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Ruby -->
    <tr>
        <th id="ruby" class="section" colspan="4">Ruby</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Ruby 3.4.5</li>
                <li>Ruby 3.3.9</li>
                <li>Ruby 3.2.9</li>
                <li>Ruby 3.1.5</li>
            </ul>
        </td>
        <td class="yes"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Ruby 3.0.6</li>
                <li>Ruby 2.7.8</li>
                <li>Ruby 2.6.10</li>
                <li>Ruby 2.5.9</li>
                <li>Ruby 2.4.10</li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <!-- Python -->
    <tr>
        <th id="python" class="section" colspan="4">Python</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Python 3.14.5</li>
                <li>Python 3.13.13</li>
                <li>Python 3.12.13</li>
                <li>Python 3.11.15</li>
                <li>Python 3.10.20</li>
            </ul>
        </td>
        <td class="yes"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Python 3.13.5</li>
                <li>Python 3.12.11</li>
                <li>Python 3.11.13</li>
                <li>Python 3.10.15</li>
                <li>Python 3.9.20</li>
                <li>Python 3.8.19</li>
                <li>Python 3.7.17</li>
                <li>Python 3.6.15</li>
            </ul>
        </td>
        <td class="no"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <!-- Mobile -->
    <tr>
        <th id="mobile" class="section" colspan="4">Mobile SDKs</th>
    </tr>
    <tr><td>Flutter 3.41.5</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>Flutter 3.32.8</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Android SDK command-line tools 14742923, build-tools 34/35/36</td><td class="yes"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>Android SDK 30.0.3</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <!-- Erlang -->
    <tr>
        <th id="erlang" class="section" colspan="4">Erlang</th>
    </tr>
    <tr><td>Erlang 25.0.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Tools -->
    <tr>
        <th id="tools" class="section" colspan="4">Tools</th>
    </tr>
    <tr><td>Yarn 1.22.19</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>p7zip 16.02 (<code>7za</code> utility is in PATH)</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>tcl 8.6.0+9</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>wget</td><td class="text-center">1.21.2</td><td class="text-center">1.20.3</td><td class="text-center">1.21.2</td></tr>
    <tr><td>curl</td><td class="text-center">7.81.0</td><td class="text-center">7.81.0</td><td class="text-center">7.81.0</td></tr>
    <tr><td>AWS CLI</td><td class="text-center">1.37.22</td><td class="text-center">1.37.22</td><td class="text-center">1.37.22</td></tr>
    <tr><td>Azure CLI</td><td class="text-center">2.69.0</td><td class="text-center">2.69.0</td><td class="text-center">2.69.0</td></tr>
    <tr><td>Google Cloud SDK</td><td class="text-center">485.0.0</td><td class="text-center">485.0.0</td><td class="text-center">485.0.0</td></tr>
    <tr><td>Packer 1.8.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>VirtualBox 7.1.x</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>CMake 3.29.7</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>vcpkg 2023-08-09</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Doxygen</td><td class="text-center">1.9.5</td><td class="text-center">1.9.5</td><td class="text-center">1.9.5</td></tr>
    <tr><td>Ninja 1.10.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Gradle 4.4.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Web browsers -->
    <tr>
        <th id="web-browsers" class="section" colspan="4">Web browsers</th>
    </tr>
    <tr><td>Firefox</td><td class="text-center">141.0.2</td><td class="text-center">141.0.1</td><td class="text-center">141.0.2</td></tr>
    <tr><td>Chrome</td><td class="text-center">Chrome 139.0.7258.66</td><td class="text-center">Chrome 139.0.7258.66</td><td class="text-center">Chrome 139.0.7258.66</td></tr>
    <!-- Databases -->
    <tr>
        <th id="databases" class="section" colspan="4">Databases</th>
    </tr>
    <tr><td>SQL Server 2025 17.0.4040.1-1</td><td class="yes"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>SQL Server 2019 15.0.4420.2</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>PostgreSQL 18</td><td class="yes"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>PostgreSQL 17.5-1</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>MySQL</td><td class="text-center">8.0.41</td><td class="text-center">8.0.41</td><td class="text-center">8.0.41</td></tr>
    <tr><td>MongoDB 6.0.16</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>MongoDB 8.0</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <!-- Services -->
    <tr>
        <th id="services" class="section" colspan="4">Services</th>
    </tr>
    <tr><td>OctoTools 9.1.7</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Redis 7.0.12</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>RabbitMQ 3.13.6-1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Configuration -->
    <tr>
        <th id="configuration" class="section" colspan="4">Configuration</th>
    </tr>
    <tr><td>Default Locale: LANG</td><td>C.UTF&#8209;8</td><td>C.UTF&#8209;8</td><td>C.UTF&#8209;8</td></tr>
</table>
