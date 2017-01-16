---
layout: docs
title: Building Xamarin projects
---

<!-- markdownlint-disable MD022 MD032 -->
# Building Xamarin projects
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Introduction

`Visual Studio 2015` build image (which is default image for OSS plans) has [Xamarin Platform](https://www.xamarin.com/)
pre-installed and allows building Android and iOS libraries.

Building `.ipa` application packages requires Mac computer and is not currently supported.

## Restoring Xamarin components

To restore Xamarin components on build worker you use `xamarin-component.exe` tool.
The tool is available at the following location: [https://components.xamarin.com/submit/xpkg](https://components.xamarin.com/submit/xpkg)

Rename the downloaded file to `xpkg.zip` and unzip to extract the tool.

The main challenge of using this tool on a build server is that to restore components it requires authentication with your Xamarin credentials,
however `login` action prompts for password interactively thus blocking the build.

Fortunately, it's possible to copy cached credentials ("cookie jar") from your local development machine to a build worker and it will work.

On your local development machine open command prompt at the location with `xamarin-component.exe` tool and run the following command to authenticate (from Windows):

    xamarin-component.exe login <your-xamarin@email.com>

Or, if you're running on a Mac:

    mono xamarin-component.exe login <your-xamarin@email.com>
    
Cached credentials are stored in `C:\Users\<user>\.xamarin-credentials` file (or, on a Mac: `/Users/zachi/.xamarin-credentials`). Open that file and copy the number after `xamarin.com,` to a clipboard.

Open AppVeyor project settings and add a new environment variable called `XAMARIN_COOKIE` with the value from clipboard.

To download and configure packaging restore tool add these PowerShell commands to "Install" section of your build (`install` section in `appveyor.yml`):

```powershell
$zipPath = "$($env:APPVEYOR_BUILD_FOLDER)\xpkg.zip"
(New-Object Net.WebClient).DownloadFile('https://components.xamarin.com/submit/xpkg', $zipPath)
7z x $zipPath | Out-Null
Set-Content -path "$env:USERPROFILE\.xamarin-credentials" -value "xamarin.com,$env:XAMARIN_COOKIE"
```

To restore components used in the solution add this command to "Before build" section (`before_build` section in `appveyoryml`):

    xamarin-component.exe restore <path\to-you-solution.sln>

## External links

* [Learn more about Xamarin platform](https://www.xamarin.com/)
