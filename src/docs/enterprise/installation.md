---
layout: docs
title: AppVeyor Enterprise Installation Guide
---

<!-- markdownlint-disable MD022 MD032 -->
# AppVeyor Enterprise Installation Guide
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Prerequisites

* WWindows Server 2012 R2 (Windows 8.1 (x64)) or higher
* .NET Framework 4.5.2

## Setting up the server

* [Set PowerShell execution policy to unrestricted](https://github.com/appveyor/ci/blob/master/scripts/enterprise/enable_powershell_unrestricted.ps1)
* [Disable IE ESC](https://github.com/appveyor/ci/blob/master/scripts/enterprise/disable_ie_esc.ps1)

## Install SQL Server 2016 Express

Download SQL Server 2016 SP1 Express Edition: [download link](https://www.microsoft.com/en-us/download/details.aspx?id=54284)

* Select "Custom" installation type
* On "Feature selection" step select these features only:
    * Database Engine Services
    * Client Tools Connvectivity
* On "Database engine configuration" step:
    * Select Mixed mode
    * Add local "Administrators" group to "Specify SQL Server administrators"
* Install SSMS 17.1: [download link](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
* Delete `C:\SQLServer2016Media` directory.

## Install IIS

```posh
Write-Host "Installing IIS..." -ForegroundColor Cyan

cmd /c start /wait dism /Online /Enable-Feature /FeatureName:IIS-WebServer /FeatureName:IIS-WebServerManagementTools /FeatureName:IIS-WebServerRole /FeatureName:IIS-ManagementConsole /FeatureName:IIS-ApplicationDevelopment /FeatureName:IIS-ASPNET /FeatureName:IIS-ASPNET45 /FeatureName:IIS-NetFxExtensibility /FeatureName:IIS-NetFxExtensibility45 /FeatureName:NetFx4Extended-ASPNET45 /FeatureName:IIS-CommonHttpFeatures /FeatureName:IIS-DefaultDocument /FeatureName:IIS-DirectoryBrowsing /FeatureName:IIS-HealthAndDiagnostics /FeatureName:IIS-HttpLogging /FeatureName:IIS-LoggingLibraries /FeatureName:IIS-RequestMonitor /FeatureName:IIS-HttpCompressionStatic /FeatureName:IIS-HttpErrors /FeatureName:IIS-HttpRedirect /FeatureName:IIS-IIS6ManagementCompatibility /FeatureName:IIS-ISAPIExtensions /FeatureName:IIS-ISAPIFilter /FeatureName:IIS-WebSockets /FeatureName:IIS-RequestFiltering /FeatureName:IIS-Performance /FeatureName:IIS-Security /FeatureName:IIS-StaticContent /FeatureName:WAS-ConfigurationAPI /FeatureName:WAS-NetFxEnvironment /FeatureName:WAS-ProcessModel /FeatureName:WAS-WindowsActivationService /All
cmd /c start /wait dism /Online /Enable-Feature /FeatureName:IIS-ASPNET45 /All

Write-Host "IIS installed" -ForegroundColor Green
```

* Run `Get-WindowsFeature` and make sure ASP.NET 4.5 and WebSockets are enabled
* Make sure [http://localhost](http://localhost) is opening

## Install Redis

* Use AppVeyor installation script

## Install and configure Service Bus

* Use AppVeyor installation script

