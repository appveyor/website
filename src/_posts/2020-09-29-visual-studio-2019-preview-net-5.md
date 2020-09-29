---
title: 'Visual Studio 2019 Preview with .NET 5.0'
---

.NET 5.0 has [reached "RC" stage](https://devblogs.microsoft.com/dotnet/announcing-net-5-0-rc-1/) which means no more features will be added
and it's time to start testing your projects with .NET 5.0!

To help you start with .NET 5.0 development, we've baked the `Visual Studio 2019 Preview` image with the latest Visual Studio 2019 Preview and .NET 5.0.<br>
In addition to VS 2019 Preview, it has the same software as on [Visual Studio 2019 image](https://www.appveyor.com/docs/windows-images-software/).

You can select `Visual Studio 2019 Preview` image in "Build worker image" dropdown on Environment tab of project settings (if project configured on UI) or configure in `appveyor.yml`:

```yaml
image: Visual Studio 2019 Preview
```

Enjoy!