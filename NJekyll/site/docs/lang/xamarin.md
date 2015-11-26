---
layout: docs
title: Building Xamarin projects
---

# Building Xamarin projects


## Introduction

`Visual Studio 2015` build image (which is default image for OSS plans) has [Xamarin Platform](https://xamarin.com/) pre-installed and allows building Android and iOS libraries.

> Building `.ipa` application packages requires Mac computer and is not currently supported.

However, building Xamarin projects with MSBuild command line requires a valid Xamarin license (trial, commercial or open-source) which, once activated, is bound to specific computer. The license can be activated on a limited number of computers. **You should bring your own license (BYOL) to build Xamarin projects on AppVeyor platform.**

AppVeyor has a built-in tool for activating and deactivating Xamarin licenses. AppVeyor build workers are stateless which means their state is not preserved between builds. The license is activated on build start and deactivated on build finish.

## Setting up Xamarin account credentials

You can securely specify your Xamarin account credentials on "Xamarin" tab of project settings or in `appveyor.yml`:

	xamarin:
	  email: your@email.com
	  password:
	    secure: ABC123==
	  android: true
	  ios: true

`email` value can be secure string too:

	xamarin:
	  email:
	    secure: AFBC12345678==
	  password:
	    secure: ABC123==
	  android: true
	  ios: true

> Even if you use `appveyor.yml` for configuring your projects you can still set Xamarin account credentials on project UI and remove them from `appveyor.yml`.

## External links

* [Learn more about Xamarin platform](http://xamarin.com)  
* [Request Xamarin license for your open-source project](http://resources.xamarin.com/open-source-contributor.html)
