---
title: Faster build environment for open-source projects and Xamarin support
---

Dear customers,

We have some great news for AppVeyor open-source community!

Today we are announcing availability of a new faster build environment for open-source projects! The new environment is based on Hyper-V and features latest Intel processors and SSD drives. Builds start almost instantly and run faster on new environment.


## Migration plan

* New "Free" accounts go to the new environment.
* Existing "Free" accounts using "default" image (Windows Server 2012 R2) will be moved in batches starting from the most busy ones within the next few weeks.
* On request basis. If you are in rush to try the new environment - let us know.
* All "Basic" accounts go to Pro environment.


## Custom images

If your project is using custom image for MinGW, Cygwin, Qt or Visual Studio 2015 RC we encourage you to switch to "default" image (remove "os:" setting from appveyor.yml) as build workers on a new OSS environment have all these software installed.

Our ultimate goal is to have a single "all-in-one" build worker image for open-source (and, eventually, Pro customers) with all Visual Studio versions and other tools installed. Custom images offer will be re-worked (self-manage interface, faster Azure VMs) and be available as a paid option. That means at some point after most of OSS accounts are migrated to a new environment, custom images (Unstable, MinGW, VS 2015, etc.) will become unavailable to free accounts.


## Technical specs

OSS build environment VMs have ~1 CPU core, 1.7 GB of memory and 1 GBs network connection.


## Heads up

* Some projects could have issues in the environment where Visual Studio 2015 is installed. Let us know about such cases.
* New environment has limited capacity (though we estimate it should cover current OSS projects) and if all its workers are busy the build will be run on Azure (as of now).


## Xamarin support

Open-source build workers have Xamarin Platform pre-installed. However, you should have Xamarin license (either commercial or open-source) to run your Xamarin builds. Read more about building Xamarin projects on AppVeyor in this article: [/docs/lang/xamarin](/docs/lang/xamarin/)

Hope your CI experience will get better with this exciting news!

Feodor Fitsner,<br>
AppVeyor founder and developer

Follow us on Twitter: [@appveyor](https://twitter.com/appveyor)
