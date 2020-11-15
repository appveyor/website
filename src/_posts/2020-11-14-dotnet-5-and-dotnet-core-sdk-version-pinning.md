---
title: '.NET 5 and .NET Core SDK version pinning'
---

With the [recent update of Visual Studio 2019 image](/updates/2020/11/14/) .NET 5 is now part of Visual Studio 2019 16.8 installation. Despite there is no more "Core" in ".NET 5", technically .NET 5 is the next **major** release of .NET Core.

If your project does not have [global.json](https://docs.microsoft.com/en-us/dotnet/core/tools/global-json?tabs=netcore3x) then the latest .NET Core SDK is used which is now .NET 5. In theory, always using the latest version of .NET Core SDK should work, but if your build was broken or you are in the process of migrating to .NET 5 you may need to stick to a previous .NET Core SDK.

To build your projects with the latest .NET Core 3.1 add `global.json` to the root of project repository (or working folder from which `dotnet` or `msbuild` commands are run) with the following contents:

```json
{
  "sdk": {
    "version": "3.1.102",
    "rollForward": "latestFeature"
  }
}
```

And remember, there is `Previous Visual Studio 2019` image with Visual Studio 2019 version 16.7.6 and without .NET 5. You can use it in case of any issues with the current image:

```yaml
image: Previous Visual Studio 2019
```