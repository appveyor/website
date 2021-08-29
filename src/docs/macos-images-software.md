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
        <th class="rotate"><span>macos-bigsur</span></th>
        <th class="rotate"><span>macos</span></th>
        <th class="rotate"><span>macos-mojave</span></th>
    </tr>
    <tr>
        <th id="operating-system" class="section" colspan="4">Operating system</th>
    </tr>
    <tr>
        <td>macOS 11.5.2 "Big Sur"</td>
        <td class="yes"></td><td class="no"></td><td class="no"></td>
    </tr>
    <tr>
        <td>macOS 10.15.7 "Catalina"</td>
        <td class="no"></td><td class="yes"></td><td class="no"></td>
    </tr>
    <tr>
        <td>macOS 10.14.6 "Mojave"</td>
        <td class="no"></td><td class="no"></td><td class="yes"></td>
    </tr>
    <tr>
        <th id="powershell" class="section" colspan="4">PowerShell</th>
    </tr>
    <tr><td>PowerShell Core 7.1.4</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Version control systems -->
    <tr>
        <th id="version-control-systems" class="section" colspan="4">Version control systems</th>
    </tr>
    <tr>
        <td>Git 2.33.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr>
        <td>Git Large File Storage (Git LFS) 2.13.3</td><td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <tr><td>Mercurial 5.9</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Subversion 1.14.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- XCode -->
    <tr>
        <th id="xcode" class="section" colspan="4">XCode</th>
    </tr>
    <tr><td>XCode 12.5.1</td><td class="yes"></td><td class="no"></td><td class="no"></td></tr>
    <tr><td>XCode 12.3</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>XCode 11.7</td><td class="no"></td><td class="yes"></td><td class="no"></td></tr>
    <tr><td>XCode 11.3.1</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>XCode 10.3</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>XCode 9.4.1</td><td class="no"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- .NET Framework -->
    <tr>
        <th id="net-framework" class="section" colspan="4">.NET Framework</th>
    </tr>
    <tr><td>.NET Core SDK 2.1.202</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.1.804</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 2.2.402</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 3.0.103</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 3.1.412</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>.NET Core SDK 5.0.400</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Node.js -->
    <tr>
        <th id="node-js" class="section" colspan="4">Node.js</th>
    </tr>
    <tr><td>Node Version Manager (<code>nvm</code>) 0.35.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 4.9.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 5.12.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 6.17.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 7.10.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 8.17.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 9.11.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 10.24.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 11.15.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 12.22.5</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 13.14.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 14.17.5</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 15.14.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Node.js 16.8.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Java -->
    <tr>
        <th id="java" class="section" colspan="4">Java</th>
    </tr>
    <tr><td>AdoptOpenJDK 8,242</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>AdoptOpenJDK 9,181</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>AdoptOpenJDK 10.0.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>AdoptOpenJDK 11.0.6</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>AdoptOpenJDK 12.0.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>AdoptOpenJDK 13.0.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>AdoptOpenJDK 14.0.2</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>AdoptOpenJDK 15.0.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Go -->
    <tr>
        <th id="golang" class="section" colspan="4">Go (Golang)</th>
    </tr>
    <tr><td>Go Version Manager (<code>gvm</code>) v1.0.22</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr>
        <td>
            <ul>
                <li>Go 1.13.15</li>
                <li>Go 1.14.15</li>
                <li>Go 1.15.15</li>
                <li>Go 1.16.7</li>
                <li>Go 1.17</li>
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
                <li>Ruby 2.0.0-p648</li>
                <li>Ruby 2.1.10</li>
                <li>Ruby 2.2.10</li>
                <li>Ruby 2.3.8</li>
                <li>Ruby 2.4.10</li>
                <li>Ruby 2.5.8</li>
                <li>Ruby 2.6.6 (default)</li>
                <li>Ruby 2.7.2</li>
                <li>Ruby head</li>
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
                <li>Python 2.7.18 (<code>~/venv2.7.18</code> and <code>~/venv2.7</code>)</li>
                <li>Python 3.5.10 (<code>~/venv3.5.10</code> and <code>~/venv3.5</code>)</li>
                <li>Python 3.6.14 (<code>~/venv3.6.14</code> and <code>~/venv3.6</code>)</li>
                <li>Python 3.7.11 (<code>~/venv3.7.11</code> and <code>~/venv3.7</code>)</li>
                <li>Python 3.8.11 (<code>~/venv3.8.11</code> and <code>~/venv3.8</code>)</li>
                <li>Python 3.9.6 (<code>~/venv3.9.6</code> and <code>~/venv3.9</code>)</li>
                <li>virtualenv 20.0.21</li>
                <li>pip 20.1.1</li>
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
                <li>6.1.2 in <code>$HOME/Qt/6.1.2/clang_64/bin</code></li>
                <li>6.0.4 in <code>$HOME/Qt/6.0.4/clang_64/bin</code></li>
                <li>5.15.2 in <code>$HOME/Qt/5.15.2/clang_64/bin</code></li>
                <li>5.14.2 in <code>$HOME/Qt/5.14.2/clang_64/bin</code></li>
            </ul>
            <p>Links for latest and major versions:</p>
            <ul>
                <li><code>$HOME/Qt/6.1</code> &rarr; <code>$HOME/Qt/6.1.2</code></li>
                <li><code>$HOME/Qt/6.0</code> &rarr; <code>$HOME/Qt/6.0.4</code></li>
                <li><code>$HOME/Qt/latest</code> &rarr; <code>$HOME/Qt/5.15.2</code></li>
                <li><code>$HOME/Qt/5.15</code> &rarr; <code>$HOME/Qt/5.15.2</code></li>
                <li><code>$HOME/Qt/5.14</code> &rarr; <code>$HOME/Qt/5.14.2</code></li>
            </ul>
        </td>
        <td class="yes"></td><td class="yes"></td><td class="yes"></td>
    </tr>
    <!-- Compilers -->
    <tr>
        <th id="compilers" class="section" colspan="4">Compilers</th>
    </tr>
    <tr><td>CMake 3.20.1</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 6.5.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 7.4.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>GCC 8.3.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Simulators -->
    <tr>
        <th id="simulators" class="section" colspan="4">Simulators</th>
    </tr>
    <tr><td>iOS 12.4</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>tvOS 12.4</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>watchOS 5.3</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <!-- Tools -->
    <tr>
        <th id="tools" class="section" colspan="4">Tools</th>
    </tr>
    <tr><td>Brew 3.2.9</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>Fastlane 2.192.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
    <tr><td>curl 7.78.0</td><td class="yes"></td><td class="yes"></td><td class="yes"></td></tr>
</table>