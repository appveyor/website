---
layout: docs
title: Software pre-installed on macOS build VMs
---

<!-- markdownlint-disable MD022 MD032 -->
# Software pre-installed on macOS build VMs
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

The history of macOS image updates can be found [here](/updates/).

<div class="row">
    <div class="columns medium-4">
        <ul>
            <li><a href="#operating-system">Operating system</a></li>
            <li><a href="#powershell">PowerShell</a></li>
            <li><a href="#version-control-systems">Version control systems</a></li>
            <li><a href="#xcode">XCode</a></li>
            <li><a href="#net-framework">.NET Framework</a></li>
            <li><a href="#node-js">Node.js</a></li>
            <li><a href="#java">Java</a></li>
        </ul>
    </div>
    <div class="columns medium-4">
        <ul>
            <li><a href="#golang">Go (Golang)</a></li>
            <li><a href="#ruby">Ruby</a></li>
            <li><a href="#python">Python</a></li>
            <li><a href="#qt">Qt</a></li>
            <li><a href="#compilers">Compilers</a></li>
            <li><a href="#simulators">Simulators</a></li>
            <li><a href="#tools">Tools</a></li>
        </ul>
    </div>
    <div class="columns medium-4">
        <ul>
        </ul>
    </div>
</div>

<table class="software-list">
    <tr>
        <th>Software installed / Build worker image</th>
        <th class="rotate"><span>macos-sonoma</span></th>
        <th class="rotate"><span>macos-ventura</span></th>
        <th class="rotate"><span>macos-monterey</span></th>
    </tr>
    <tr>
        <th id="operating-system" class="section" colspan="4">Operating system</th>
    </tr>
    <tr>
        <td>macOS 14.2.1 "Sonoma"</td>
        <td class="yes"></td><td class="no"></td><td class="no"></td>
    </tr>
    <tr>
        <td>macOS 13.6.4 "Ventura"</td>
        <td class="no"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>macOS 12.7.3 "Monterey"</td>
        <td class="no"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <th id="powershell" class="section" colspan="4">PowerShell</th>
    </tr>
    <tr><td>PowerShell Core 7.4.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Version control systems -->
    <tr>
        <th id="version-control-systems" class="section" colspan="4">Version control systems</th>
    </tr>
    <tr>
        <td>Git 2.43.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Git Large File Storage (Git LFS) 3.4.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr><td>Mercurial 6.6.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Subversion 1.14.3</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- XCode -->
    <tr>
        <th id="xcode" class="section" colspan="4">XCode</th>
    </tr>
    <tr><td>XCode 15.2</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>XCode 14.3</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>XCode 14.2</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>XCode 13.4.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- .NET Framework -->
    <tr>
        <th id="net-framework" class="section" colspan="4">.NET Framework</th>
    </tr>
    <tr><td>.NET Core SDK 8.0.101</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 7.0.405</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 6.0.418</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 3.1.426</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Node.js -->
    <tr>
        <th id="node-js" class="section" colspan="4">Node.js</th>
    </tr>
    <tr><td>Node Version Manager (<code>nvm</code>) 0.35.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 21.6.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 20.11.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 19.9.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 18.19.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 14.21.3</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 10.24.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 8.17.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Java -->
    <tr>
        <th id="java" class="section" colspan="4">Java</th>
    </tr>
    <tr><td>Temurin 21</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Temurin 20</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Temurin 19</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Temurin 18</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Temurin 17</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Go -->
    <tr>
        <th id="golang" class="section" colspan="4">Go (Golang)</th>
    </tr>
    <tr><td>Go Version Manager (<code>gvm</code>) v1.0.22</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr>
        <td>
            <ul>
                <li>Go 1.21.6</li>
                <li>Go 1.20.13</li>
                <li>Go 1.19.13</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Ruby -->
    <tr>
        <th id="ruby" class="section" colspan="4">Ruby</th>
    </tr>
    <tr><td>Ruby Version Manager (<code>rvm</code>) 1.29.10<br><br>Switching between Ruby versions: <code>rvm use &lt;version&gt;</code></td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr>
        <td>
            <ul>
                <li>Ruby head</li>
                <li>Ruby 3.3.0</li>
                <li>Ruby 3.2.3</li>
                <li>Ruby 3.1.4</li>
                <li>Ruby 3.0.6</li>
                <li>Ruby 2.7.8</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Python -->
    <tr>
        <th id="python" class="section" colspan="4">Python</th>
    </tr>
    <tr><td>Python 3.12.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.11.7</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.10.13</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.9.18</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 3.8.18</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Python 2.7.18</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Qt -->
    <tr>
        <th id="qt" class="section" colspan="4">Qt</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>6.6.1 in <code>$HOME/Qt/6.6.1/macos/bin</code></li>
                <li>6.5.3 in <code>$HOME/Qt/6.5.3/macos/bin</code></li>
                <li>6.2.4 in <code>$HOME/Qt/6.2.4/macos/bin</code></li>
                <li>5.15.2 in <code>$HOME/Qt/5.15.2/clang_64/bin</code></li>
            </ul>
            <p>Links for latest and major versions:</p>
            <ul>
                <li><code>$HOME/Qt/6.6</code> &rarr; <code>$HOME/Qt/6.6.1</code></li>
                <li><code>$HOME/Qt/6.5</code> &rarr; <code>$HOME/Qt/6.5.3</code></li>
                <li><code>$HOME/Qt/6.2</code> &rarr; <code>$HOME/Qt/6.2.4</code></li>
                <li><code>$HOME/Qt/latest</code> &rarr; <code>$HOME/Qt/5.15.2</code></li>
                <li><code>$HOME/Qt/5.15</code> &rarr; <code>$HOME/Qt/5.15.2</code></li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Compilers -->
    <tr>
        <th id="compilers" class="section" colspan="4">Compilers</th>
    </tr>
    <tr><td>CMake 3.28.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 12</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 11</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 10</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Simulators -->
    <tr>
        <th id="simulators" class="section" colspan="4">Simulators</th>
    </tr>
    <tr><td>iOS 17.2</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>tvOS 17.2</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>watchOS 10.2</td><td class="yes"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>iOS 16.1</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>tvOS 16.1</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <tr><td>watchOS 9.1</td><td class="no"></td><td class="no"></td><td class="yes"></td></tr>
    <!-- Tools -->
    <tr>
        <th id="tools" class="section" colspan="4">Tools</th>
    </tr>
    <tr><td>Homebrew 4.2.7</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Curl 8.6.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
</table>