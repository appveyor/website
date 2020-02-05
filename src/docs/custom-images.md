---
layout: docs
title: Custom images
---

# Custom images

You can install your own software and build dependencies on top of AppVeyor-provided base images (Windows, Linux) and then use resulting image for further builds.
By baking dependencies into the image you can drastically reduce the duration of your builds.

Custom images enable new use cases that were not possible before in a hosted CI/CD:

* speed up builds drastically by preserving VM state between builds;
* update image software at your own pace - pin specific versions or be on a cutting edge;
* securely store certificates and other secrets within the image;
* migrate on-premise CI workflows with proprietary, licensed, manually-installed, legacy software to a hosted CI.

## Creating custom images

You can establish a separate repository and AppVeyor project to build an image or the project-specific image can be constantly updated during the build (build snapshots).

### Shared image in a dedicated project

You can decide to build a few images with a maximum of pre-installed products/tools/libraries that will be shared across multiple projects or teams.

For the shared image it is recommended having a separate repository with all installation scripts and `appveyor.yml`.

Below is the sample `appveyor.yml` to build custom image with `win-sqlsrv-2019` name:

```yaml
image: Visual Studio 2019

environment:
  APPVEYOR_BAKE_IMAGE: win-sqlsrv-2019

install:
- choco install sql-server-express
```

In the example above we use `Visual Studio 2019` as a base image, install SQL Server 2019 Express using Chocolatey and then bake a new image named `win-sqlsrv-2019`. Defining `APPVEYOR_BAKE_IMAGE` environment variable triggers image bake process. `APPVEYOR_BAKE_IMAGE` could be defined statically to bake a new image every time or dynamically in a build script.

To use that new image with SQL Server 2019 pre-installed in other projects specify its name in `appveyor.yml`:

```yaml
image: win-sqlsrv-2019
```

As you see the process of building a new image is not different from regular builds. You can use all [appveyor.yml features](/docs/build-configuration/), [run tests](/docs/running-tests/) to make sure the software is installed properly, [write messages using API](/docs/build-worker-api/) and [push artifacts](/docs/packaging-artifacts/) with installation logs.

### Project-specific build snapshots

Project-specific images (build spanshots) could serve as the next-gen build cache. However, instead of downloading zip with cache contents and unpacking it on every build all dependencies are baked into the image itself.



## Using custom image


