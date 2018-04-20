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

The folloing tweak environment variables can be used to customize automatic packaging process:

* `OCTOPUS_PACKAGE_NUGET` - set to `true` to package in Nuget format, default is `Zip`.
* `OCTOPUS_PACKAGE_VERSION` - customize package version. default is AppVeyor build version. Environment variable can be used here.
* `OCTOPUS_PACKAGE_ADVANCED` - pass additional options to `octo.exe push`.

### Custom packaging

If you build your app using a script or your app is not ASP.NET Web Application, you can package it with a script sometime after build and before deploy (`after_build` and `before_deploy` stages in the [build pipeline](/docs/build-configuration/#build-pipeline).

Packaging itself can be done with `octo.exe` or `7z.exe`, both are installed on the build workers. Then you need to push created package as an artifact:

    appveyor PushArtifact <path-to-package.zip> -Type OctopusPackage

Optionally, use `-DeploymentName` switch, which is handy when you refer artifact in deployment settings.

Or, if you can simple set the whole folder to be packged and an artifact and it will be zipped and pushed by AppVeyor:

![package-folder](/assets/img/docs/deployment/octopus-deploy/package-folder.png)

YAML:

```
artifacts:
- path: dist
  name: MyApp
  type: OctopusPackage
```

## Octopus deployment settings

Web deploy provider settings are specified on Deployment tab of project settings, `appveyor.yml` or on environment settings page.

* **Server** (`server`) - server name with remote agent service installed or URL of Web Deploy handler.
* **Website name** (`website`) - web site name to deploy to, e.g. Default Web Site or myserver.com
* **Username** (`username`)
* **Password** (`password`)
* **NTLM authentication** (`ntlm`) - NTLM authentication is primarily used by Remote Agent Service. Usually, IIS 7 and up web servers use Web Deploy Handler approach with Basic authentication.
* **Remove additional files at destination** (`remove_files`) - when set selected provider performs full content synchronization, i.e. deletes files at destination that don't exist in the package.
* **Skip directories** (`skip_dirs`) - semicolon list of regular expressions specifying the list of directories to skip while synchronizing web site contents, for example `\\App_data;\\uploads`.
* **Skip files** (`skip_files`) - semicolon list of regular expressions specifying the list of files to skip while synchronizing web site contents, for example `web.config` (all web.configs) or only the root config for MVC apps `^((?!Views).)*web\.config$` (thanks to [this blog post](http://keza.net/2011/11/15/skipping-mvc-web-config-files-with-msdeploy/)).
* **Take ASP.NET application offline during deployment** (`app_offline`) - places app_offline.htm page into the root of web application before sync to take app offline and then remove the page when deployment has finished.
* **Artifact to deploy** (`artifact`) - artifact name containing application package to deploy.
