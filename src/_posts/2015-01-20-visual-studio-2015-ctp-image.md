---
title: Test image with Visual Studio 2015 CTP and SDK
---

We've just added a new build worker image with Visual Studio 2015 CTP 5 installed!

Both Visual Studio Ultimate 2015 CTP and Visual Studio 2015 SDK CTP were installed from [official download page](https://support.microsoft.com/kb/2967191).

Build worker image is called `Visual Studio 2015 CTP`. You can select it on Environment tab of project settings (if you configure project on UI):

![project-environment-tab](/assets/images/posts/vs2015/project-environment-tab.png)

or specify in `appveyor.yml`:

```yaml
os: Visual Studio 2015 CTP
```

> Please note builds using this image run on Azure environment which means there is a few minutes delay before build starts required to provision build worker VM.

Add this command to `install` section of your build config if you need `msbuild` command to call MSBuild 14.0 by default:

```bat
set PATH=C:\Program Files (x86)\MSBuild\14.0\Bin;%PATH%
```

> There is an image with previous Visual Studio 2015 release called `Visual Studio 2015 Preview`. Starting from today we will be updating only new `Visual Studio 2015 CTP` image.

Enjoy!
