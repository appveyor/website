---
layout: post
title: Visual Studio 2015 RTM on Pro environment
---

Great news for AppVeyor customers! We've finally managed to run `Visual Studio 2015` image on Pro environment!

If you are on a paid plan use `Visual Studio 2015` image ("OS" setting on Environment tab of project settings or `os: Visual Studio 2015` in `appveyor.yml`).

It still takes around 40-50 seconds before build starts - this is the time required to boot up build worker VM with Visual Studio 2015 image. But everything is going on Pro environment and build start time is quite predictable there. We hope it's acceptable trade-off between performance and convenience.

**Those customer accounts that were moved to a "new" faster OSS environment where Visual Studio 2015 is installed on build workers by default can be moved back to Pro environment, so make sure you switch your builds to use `Visual Studio 2015` image.**