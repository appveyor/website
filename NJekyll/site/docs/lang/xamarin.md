---
layout: docs
title: Building Xamarin projects
---

# Building Xamarin projects

<!--TOC-->

## Quick start

Put your Xamarin account email and password to project environment variables (either UI or in `appveyor.yml` - use [secure variable](/docs/build-configuration#secure-variables) for password) as `xamarin_email` and `xamarin_password` respectively.

Add this command to "Before build script" (`before_build`) section of your project to activate Xamarin license on build worker machine:

    appveyor RegisterXamarinLicense -Email %xamarin_email% -Password %xamarin_password% -Product Android

Add this command to "On build finish script" (`on_finish`) section of your project to deactivate Xamarin license:

    appveyor UnregisterXamarinLicense -Email %xamarin_email% -Password %xamarin_password%


## Introduction

Open-source build environment ("Free" plans) has [Xamarin Platform](https://xamarin.com/) pre-installed and allows building Android and iOS libraries.

> Building `.ipa` application packages requires Mac computer and is not currently supported.

However, building Xamarin projects with MSBuild command line requires a valid Xamarin license (trial, commercial or open-source) which, once activated, is bound to specific computer. The license can be activated on a limited number of computers. **You should bring your own license (BYOL) to build Xamarin projects on AppVeyor platform.**

AppVeyor provides a tool for activating and deactivating Xamarin licenses. AppVeyor build workers are stateless which means their state is not preserved between builds. You add a command to activate the license on build start and then deactivate on build finish.

> If you are on a paid plan and would like to build Xamarin apps please [contact us](http://www.appveyor.com/support) to move your account to OSS environment. 

## Activating Xamarin license

Both activating and deactivating commands requires Xamarin account email and password. We recommend to put them into project environment variables and have password stored as [secure variable](/docs/build-configuration#secure-variables).

> Secure variables are not decrypted during pull request (GitHub only) builds of public projects. This is made intentionally to avoid leaking your credentials by submitting PR with malicious build script displaying environment variables.

Suppose your Xamarin account email and password are stored in `xamarin_email` and `xamarin_password` variables respectively. To activate Xamarin license for Android apps add this command to "Before build script" (`before_build`) section of your project to activate Xamarin license on build worker machine:

    appveyor RegisterXamarinLicense -Email %xamarin_email% -Password %xamarin_password% -Product Android

PowerShell equivalent:

    Register-XamarinLicense -Email $env:xamarin_email -Password $env:xamarin_password -Product Android

To activate iOS license:

    appveyor RegisterXamarinLicense -Email %xamarin_email% -Password %xamarin_password% -Product iOS

the same in PowerShell:

    Register-XamarinLicense -Email $env:xamarin_email -Password $env:xamarin_password -Product iOS

## Deactivating the license

Xamarin license can be activated on a limited number of computers. The best place for license deactivation is "Build finish" phase (`on_finish` in `appveyor.yml`) which is called for both succeeded and failed builds.

To deactivate the license add this command to "On build finish script" (`on_finish`) section of your project to deactivate Xamarin license:

    appveyor UnregisterXamarinLicense -Email %xamarin_email% -Password %xamarin_password%

and PowerShell equivalent:

    Unregister-XamarinLicense -Email $env:xamarin_email -Password $env:xamarin_password

## External links

* [Learn more about Xamarin platform](http://xamarin.com)  
* [Request Xamarin license for your open-source project](http://resources.xamarin.com/open-source-contributor.html)
