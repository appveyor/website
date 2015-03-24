---
layout: docs
title: Integration testing with Local deployment
---

# Integration testing with Local deployment

Local deployment provider deploys artifacts containing web and Windows application packages to build server locally for integration testing.

## Provider settings

Local deployment provider has exactly the same behavior and configuration settings as [Agent Deployment provider](/docs/deployment/agent) with the only difference is that applications
are being installed locally on the build server.

## Installing self-signed SSL certificate to a website

Add `InstallSelfSignedCert.ps1` PowerShell script into your repository:

{% highlight powershell %}
$cert = New-SelfSignedCertificate -DnsName ("localtest.me","*.localtest.me") -CertStoreLocation cert:\LocalMachine\My
$rootStore = Get-Item cert:\LocalMachine\Root
$rootStore.Open("ReadWrite")
$rootStore.Add($cert)
$rootStore.Close();
Import-Module WebAdministration
Set-Location IIS:\SslBindings
New-WebBinding -Name "Default Web Site" -IP "*" -Port 443 -Protocol https
$cert | New-Item 0.0.0.0!443
{% endhighlight %}

Then if using `appveyor.yml` call it like that (provided the script is in root of repo):

{% highlight yaml %}
before_deploy:
  - PowerShell .\InstallSelfSignedCert.ps1
{% endhighlight %}
