---
layout: docs
title: Enabling HTTP proxy on AppVeyor build workers
---

# Enabling HTTP proxy on AppVeyor build workers
{:.no_toc}

* Comment to trigger ToC generation
{:toc}

## Enabling HTTP proxy

Your builds may depend on various external dependencies such as installation files, archives, npm packages and others.
Those files are usually hosted on highly-available servers with high-speed connections and CDN in front of them, so downloading them during the buils is not an issue.

But sometimes network configuration may change and there might be intermittent networking issues failing/hanging your builds while [downloading](/docs/how-to/download-file) some external dependencies.

In such situations we recommend trying to enable HTTP proxy during the build.

To enable HTTP proxy that will work for clients using WinINet library (IE, PowerShell, .NET, etc.) add the following command into `install` or `init` phase of your build:

{% highlight yaml %}
install:
  - ps: iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-http-proxy.ps1'))
{% endhighlight %}

All commands following this script will start using HTTP proxy.

AppVeyor HTTP proxy details will be stored in environment variables:

- `APPVEYOR_HTTP_PROXY_IP` - proxy IP address
- `APPVEYOR_HTTP_PROXY_PORT` - proxy port

## Maven

To setup Maven builds to use HTTP proxy create `set-maven-proxy.ps1` PowerShell script to your repository with the following contents:

{% highlight powershell %}
$mavenConfig = '<settings xmlns="http://maven.apache.org/SETTINGS/1.0.0"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xsi:schemaLocation="http://maven.apache.org/SETTINGS/1.0.0
                  http://maven.apache.org/xsd/settings-1.0.0.xsd">
  <proxies>
    <proxy>
      <active>true</active>
      <protocol>http</protocol>
      <host>' + $env:APPVEYOR_HTTP_PROXY_IP + '</host>
      <port>' + $env:APPVEYOR_HTTP_PROXY_PORT + '</port>
    </proxy>
  </proxies>
</settings>'

New-Item "$env:USERPROFILE\.m2" -ItemType Directory -Force | Out-Null
Set-Content "$env:USERPROFILE\.m2\settings.xml" -Value $mavenConfig
{% endhighlight %}

Then add a call that script in `appveyor.yml`:

{% highlight yaml %}
install:
  - ps: iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-http-proxy.ps1'))
  - ps: .\set-maven-proxy.ps1
{% endhighlight %}

## Node.js and NPM

Add these lines to `appveyor.yml` to enable AppVeyor HTTP proxy for npm:

{% highlight yaml %}
install:
  - ps: iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-http-proxy.ps1'))
  - npm config set proxy http://%APPVEYOR_HTTP_PROXY_IP%:%APPVEYOR_HTTP_PROXY_PORT%
  - npm config set https-proxy http://%APPVEYOR_HTTP_PROXY_IP%:%APPVEYOR_HTTP_PROXY_PORT%
{% endhighlight %}
