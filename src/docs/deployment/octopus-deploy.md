---
layout: docs
title: Deploying to Octopus Deploy
---

<!-- markdownlint-disable MD022 MD032 -->
# Deploying to Octopus Deploy
{:.no_toc}

With AppVeyor you can package application for Octopus, push package, optionally create a release and deploy it.

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Creating package for Octopus Deploy

### Automatic packaging of ASP.NET Web Applications

During “MSBuild” phase AppVeyor can automatically detect ASP.NET Web Application projects in the solution and package them with `octo.exe` and save to build artifacts. To make it work, check `Package Web Applications for Octopus deployment` under `MSBUILD` tab in UI, or set `publish_wap_octopus: true` under `build` in YAML.

![project-settings](/assets/img/docs/deployment/octopus-deploy/project-settings.png)

The following tweak environment variables can be used to customize automatic packaging process:

* `OCTOPUS_PACKAGE_NUGET` - set to `true` to package in Nuget format, default is `Zip`.
* `OCTOPUS_PACKAGE_VERSION` - customize package version. default is AppVeyor build version. Environment variable can be used here.
* `OCTOPUS_PACKAGE_ADVANCED` - pass additional options to `octo.exe pack`.

### Custom packaging

If you build your app using a script or your app is not an ASP.NET Web Application, you can package it with a script sometime after the build and before deployment (`after_build` and `before_deploy` stages in the [build pipeline](/docs/build-configuration/#build-pipeline)).

Packaging itself can be done with `octo.exe` or `7z.exe` as both are installed on the build workers. Then you need to push the created package as an artifact:

    appveyor PushArtifact <path-to-package.zip> -Type OctopusPackage

Optionally, use `-DeploymentName` switch, which is handy when you refer to the artifact in deployment settings.

Or, you can simply set the whole folder to be packaged as an artifact and it will be zipped and pushed by AppVeyor:

![package-folder](/assets/img/docs/deployment/octopus-deploy/package-folder.png)

YAML:

```yaml
artifacts:
- path: dist
  name: MyApp
  type: OctopusPackage
```

More details at [packaging artifacts](/docs/packaging-artifacts/).

### Publishing .nupkg artifacts to to account/project feeds

By default AppVeyor publishes all .nupkg artifacts to to account/project Nuget feeds ([more details](/docs/nuget/)). Therefore, any `.nupkg` artifact of type `OctopusPackage` is published account/project Nuget feeds. If this behavior is not , please set the following in YAML:

```yaml
nuget:
  disable_publish_octopus: true
```

Or if you use UI, select `Do not publish Octopus Deploy .nupkg artifacts to account/project feeds` under **Nuget** settings.

## Understanding scenarios

There are 3 Octopus Deploy scenarios exposed in AppVeyor right now:

* [Pushing packages](https://octopus.com/docs/api-and-integration/octo.exe-command-line/pushing-packages) (`push_packages`)
* [Creating releases](https://octopus.com/docs/api-and-integration/octo.exe-command-line/creating-releases) (`create_release`)
* [Deploying releases](https://octopus.com/docs/api-and-integration/octo.exe-command-line/deploying-releases) (`deploy_release`)

The following rules apply:

* you can **Push packages** as long as you packaged an artifact of compatible artifact type (`OctopusPackage`, `NuGetPackage` or `Zip`).
* you can **Create release** after you **Push packages**.
* you can **Create release** without **Push packages**. This assumes that you set up Octopus to use [AppVeyor Nuget feeds](/docs/nuget/)
* You can **Deploy release** only if you **Create release**.

AppVeyor UI enforces those rules. In case you use YAML and, for example, set `deploy_release` without setting `create_release`, the `deploy_release` step will be ignored.

## Octopus deployment settings

Octopus deploy provider settings are specified on Deployment tab of project settings or in `appveyor.yml`.

* **Server URL** (`server`) - Octopus server URL.
* **API key** (`api_key`) - [API key](https://octopus.com/docs/api-and-integration/api/how-to-create-an-api-key). In UI, just copy-paste key in clear text. In YAML set it as [secure variable](https://www.appveyor.com/docs/build-configuration/#secure-variables) or simple use **Export YAML** menu to export Octopus Deploy settings with encrypted API key.
* **Octo execution timeout** (`octo_timeout`) - Optional. Time, in minutes, AppVeyor waits for `octo.exe` to complete operation. Default is 20 minutes.
* **Push packages** (`push_packages`) - when selected in UI or set to `true` in YAML, AppVeyor will execute `octo.exe push`.
    * **Artifact(s)** (`artifact`) - Optional. Artifact(s) to push. If omitted, all build artifacts of compatible type (`OctopusPackage`, `NuGetPackage` or `Zip`) will be pushed.
    * **Push packages advanced** (`push_packages_advanced`) - Optional. Additional options passed to `octo.exe push`.
* **Create release** (`create_release`) - when selected in UI or set to `true` in YAML, AppVeyor will execute `octo.exe create-release`.
    * **Project** (`project`) - Optional. Name of the project. Default is AppVeyor project name.
    * **Release number** (`release_number`) - Optional. Release number (version) to use for the new release. If omitted, Octopus will automatically determine the correct one.
    * **Channel** (`release_channel`) - Optional. Channel to use for the new release. If omitted, Octopus Deploy will select the best channel.
    * **Create release advanced** (`create_release_advanced`) - Optional. Additional options passed to `octo.exe create-release`.
* **Deploy release** (`deploy_release`) - when selected in UI or set to `true` in YAML, AppVeyor will execute `octo.exe deploy-release`. As said earlier, this require **Create release** (`create_release`).
    * **Environment(s)** (`environment`) - Environment to deploy to.
    * **Tenant(s)** (`deploy_tenants`) - Optional. Create a deployment for specific tenant(s). For multiple tenants it can be comma or semicolon or space separated list.
    * **Wait for completion** (`deploy_wait`) - Optional. When selected in UI or set to `true` in YAML, `octo.exe` will not exit until deployment completed.
    * **Deploy release advanced** (`deploy_release_advanced`) - Optional. Additional options passed to `octo.exe deploy-release`.
