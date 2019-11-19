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
        <th class="rotate"><span>macos</span></th>
        <th class="rotate"><span>macos-mojave</span></th>
    </tr>
    <tr>
        <th id="operating-system" class="section" colspan="3">Operating system</th>
    </tr>
    <tr>
        <td>macOS 10.15 "Catalina"</td>
        <td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>macOS 10.14 "Mojave"</td>
        <td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <th id="powershell" class="section" colspan="3">PowerShell</th>
    </tr>
    <tr><td>PowerShell Core 6.2.3</td><td class="yes"></td><td class="yes"></td></tr>

    <!-- Version control systems -->
    <tr>
        <th id="version-control-systems" class="section" colspan="3">Version control systems</th>
    </tr>
    <tr>
        <td>Git 2.24.0</td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Git Large File Storage (Git LFS) 2.8.0</td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr><td>Mercurial 5.1.2</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Subversion 1.12.2</td><td class="yes"></td><td class="yes"></td></tr>

    <!-- XCode -->
    <tr>
        <th id="xcode" class="section" colspan="3">XCode</th>
    </tr>
    <tr><td>XCode 11.2.1</td><td class="yes"></td><td class="yes"></td></tr>

    <!-- .NET Framework -->
    <tr>
        <th id="net-framework" class="section" colspan="3">.NET Framework</th>
    </tr>
    <tr><td>.NET Core SDK 2.1.202</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.802</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.2.402</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 3.0.100</td><td class="yes"></td><td class="yes"></td></tr>

    <!-- Node.js -->
    <tr>
        <th id="node-js" class="section" colspan="3">Node.js</th>
    </tr>
    <tr><td>Node Version Manager (<code>nvm</code>) 0.35.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 4.9.1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 5.12.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 6.17.1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 7.10.1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 8.16.2</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 9.11.2</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 10.17.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 11.15.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 12.13.1</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 13.1.0</td><td class="yes"></td><td class="yes"></td></tr>

    <!-- Java -->
    <tr>
        <th id="java" class="section" colspan="3">Java</th>
    </tr>
    <tr><td>OpenJDK 13.0.1</td><td class="yes"></td><td class="yes"></td></tr>

    <!-- Go -->
    <tr>
        <th id="golang" class="section" colspan="3">Go (Golang)</th>
    </tr>
    <tr><td>Go Version Manager (<code>gvm</code>) v1.0.22</td><td class="yes"></td><td class="yes"></td></tr>
    <tr>
        <td>
            <ul>
                <li>Go 1.7.6</li>
                <li>Go 1.8.7</li>
                <li>Go 1.9.7</li>
                <li>Go 1.10.8</li>
                <li>Go 1.11.13</li>
                <li>Go 1.12.13</li>
                <li>Go 1.13.4</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td>
    </tr>

    <!-- Ruby -->
    <tr>
        <th id="ruby" class="section" colspan="3">Ruby</th>
    </tr>
    <tr><td>Ruby Version Manager (<code>rvm</code>) 1.29.9<br><br>Switching between Ruby versions: <code>rvm use &lt;version&gt;</code></td><td class="yes"></td><td class="yes"></td></tr>
    <tr>
        <td>Ruby 2.0.0-p648</td><td class="yes"></td><td class="yes"></td>
        <td>Ruby 2.1.10</td><td class="yes"></td><td class="yes"></td>
        <td>Ruby 2.2.10</td><td class="yes"></td><td class="yes"></td>
        <td>Ruby 2.3.8</td><td class="yes"></td><td class="yes"></td>
        <td>Ruby 2.4.6</td><td class="yes"></td><td class="yes"></td>
        <td>Ruby 2.5.5</td><td class="yes"></td><td class="yes"></td>
        <td>Ruby 2.6.3 (default)</td><td class="yes"></td><td class="yes"></td>
        <td>Ruby 2.7.0-preview1</td><td class="yes"></td><td class="yes"></td>
        <td>Ruby head</td><td class="yes"></td><td class="yes"></td>
    </tr>

    <!-- Python -->
    <tr>
        <th id="python" class="section" colspan="3">Python</th>
    </tr>
    <tr>
        <td>
            <ul>
                <li>Python 2.6.9</li>
                <li>Python 2.7.17</li>
                <li>Python 3.4.9</li>
                <li>Python 3.5.9</li>
                <li>Python 3.6.9</li>
                <li>Python 3.7.5</li>
                <li>Python 3.8.0</li>
                <li>virtualenv 16.7.5</li>
                <li>pip 19.2.3</li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td>
    </tr>

    <!-- Compilers -->
    <tr>
        <th id="compilers" class="section" colspan="3">Compilers</th>
    </tr>
    <tr><td>CMake 3.16.0-rc3</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 6.5.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 7.4.0</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 8.3.0</td><td class="yes"></td><td class="yes"></td></tr>

    <!-- Simulators -->
    <tr>
        <th id="simulators" class="section" colspan="3">Simulators</th>
    </tr>
    <tr><td>iOS 12.4</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>tvOS 12.4</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>watchOS 5.3</td><td class="yes"></td><td class="yes"></td></tr>

    <!-- Tools -->
    <tr>
        <th id="tools" class="section" colspan="3">Tools</th>
    </tr>
    <tr><td>Brew 2.1.16</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Fastlane 2.28.3</td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>curl 7.54.0</td><td class="yes"></td><td class="yes"></td></tr>
</table>