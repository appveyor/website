---
title: Downloading AppVeyor build artifacts (PowerShell)
layout: docs
---

# Downloading AppVeyor build artifacts (PowerShell - advanced example)

```powershell
Get-AppVeyorArtifacts ACCOUNT PROJECT -Token TOKEN -Flat `
    -DownloadDirectory $env:Temp `
-Proxy http://proxy.example.com:8080/ -ProxyUseDefaultCredentials
```

---

```powershell
function Get-AppVeyorArtifacts
{
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Low')]
    param(
        [parameter(Mandatory = $true)]
        [string]$Account,
        [parameter(Mandatory = $true)]
        [string]$Project,
        [parameter(Mandatory = $true)]
        [string]$Token,
        [string]$DownloadDirectory,
        [switch]$Flat,
        [string]$Proxy,
        [switch]$ProxyUseDefaultCredentials)

    $apiUrl = 'https://ci.appveyor.com/api'

    $headers = @{
        'Authorization' = "Bearer $token"
        'Content-type' = 'application/json'
    }

    # Prepare proxy args to splat to Invoke-RestMethod

    $proxyArgs = @{}
    if (-not [string]::IsNullOrEmpty($proxy)) {
        $proxyArgs.Add('Proxy', $proxy)
    }
    if ($proxyUseDefaultCredentials.IsPresent) {
        $proxyArgs.Add('ProxyUseDefaultCredentials', $proxyUseDefaultCredentials)
    }

    $downloadDirectory = @($downloadDirectory, '.')[[string]::IsNullOrEmpty($downloadDirectory)]
    $errorActionPreference = 'Stop'

    $projectObject = Invoke-RestMethod -Method Get -Uri "$apiUrl/projects/$account/$project" `
                                       -Headers $headers @proxyArgs

    $jobId = $projectObject.build.jobs[0].jobId # assume build has a single job

    $artifacts = Invoke-RestMethod -Method Get -Uri "$apiUrl/buildjobs/$jobId/artifacts" `
                                   -Headers $headers @proxyArgs
    $artifacts `
    | ? { $psCmdlet.ShouldProcess($_.fileName) } `
    | % {

        $type = $_.type

        $localArtifactPath = $_.fileName -split '/' | % { [Uri]::UnescapeDataString($_) }
        if ($flat.IsPresent) {
            $localArtifactPath = ($localArtifactPath | select -Last 1)
        } else {
            $localArtifactPath = $localArtifactPath -join [IO.Path]::DirectorySeparatorChar
        }
        $localArtifactPath = Join-Path $downloadDirectory $localArtifactPath

        $artifactUrl = "$apiUrl/buildjobs/$jobId/artifacts/$($_.fileName)"
        Write-Verbose "Downloading $artifactUrl to $localArtifactPath"

        Invoke-RestMethod -Method Get -Uri $artifactUrl -OutFile $localArtifactPath @proxyArgs

        New-Object PSObject -Property @{
            'Source' = $artifactUrl
            'Type'   = $type
            'Target' = $localArtifactPath
        }
    }
}
```
