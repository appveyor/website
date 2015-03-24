---
layout: docs
title: Building Xamarin projects
---

# Building Xamarin projects
{:.no_toc}

* Comment to trigger ToC generation
{:toc}

## Introduction

`Visual Studio 2015` build image (which is default image for OSS plans) has [Xamarin Platform](https://www.xamarin.com/) pre-installed and allows building Android and iOS libraries.

> Building `.ipa` application packages requires Mac computer and is not currently supported.

However, building Xamarin projects with MSBuild command line requires a valid Xamarin license (trial, commercial or open-source) which, once activated, is bound to specific computer. The license can be activated on a limited number of computers. **You should bring your own license (BYOL) to build Xamarin projects on AppVeyor platform.**

AppVeyor has a built-in tool for activating and deactivating Xamarin licenses. AppVeyor build workers are stateless which means their state is not preserved between builds. The license is activated on build start and deactivated on build finish.

## Setting up Xamarin account credentials

You can securely specify your Xamarin account credentials on "Xamarin" tab of project settings or in `appveyor.yml`:

```yaml
xamarin:
  email: your@email.com
  password:
    secure: ABC123==
  android: true
  ios: true
```

`email` value can be secure string too:

```yaml
xamarin:
  email:
    secure: AFBC12345678==
  password:
    secure: ABC123==
  android: true
  ios: true
```

> Even if you use `appveyor.yml` for configuring your projects you can still set Xamarin account credentials on project UI and remove them from `appveyor.yml`.

## Restoring Xamarin components

To restore Xamarin components on build worker you use `xamarin-component.exe` tool. The tool is available at the following location: [https://components.xamarin.com/submit/xpkg](https://components.xamarin.com/submit/xpkg)
* rename downloaded file to `xpkg.zip` and unzip to extract the tool.

The main challenge of using this tool on a build server is that to restore components it requires authentication with your Xamarin credentials,
however `login` action prompts for password interactively thus blocking the build.

Fortunately, it's possible to copy cached credentials ("cookie jar") from your local development machine to a build worker and it will work.

On your local development machine open command prompt at the location with `xamarin-component.exe` tool and run the following command to authenticate:

    xamarin-component.exe login <your-xamarin@email.com>

Cached credentials are stored in `C:\Users\<user>\.xamarin-credentials` file. Open that file and copy the number after `xamarin.com,` to a clipboard.

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
* [Request Xamarin license for your open-source project](https://resources.xamarin.com/open-source-contributor.html)
