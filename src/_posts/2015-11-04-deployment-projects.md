---
title: Deployment projects
---

Sometimes your deployment requirements cannot fit into AppVeyor built-in deployment providers such as FTP, Web Deploy, S3 and others, for example, you are deploying to Elastic Beanstalk, or you have to recycle application pool during the deployment.

You can write a script doing custom deployment job, however this script can be run only on build worker VM during the build. "Environments" do not support custom scripts and there is no "Script" provider. This is because "Environment" deployments run on a shared background worker severs where potentially insecure custom scripting is not allowed.

This article demonstrates how you can simulate "Script" environment with regular builds and deploy project artifacts to any environment with your own custom script.

## Solution overview

The basic idea is having two AppVeyor projects:

* **Main project** - this is your main project running tests and *producing artifacts*.
* **Deployment project** - helper project *downloading artifacts* from specific build of "Main project" and deploying them with your custom script. Here, to "deploy" the build of "Main project" you could either manually run new build of deployment project from UI or use [AppVeyor API](/docs/build-worker-api#start-new-build).

## Deployment project

The "core" of deployment project is [PowerShell script](https://github.com/appveyor/ci/blob/master/scripts/deploy.ps1) downloading artifacts. The script uses AppVeyor API to find specific build and download its artifacts. Artifacts are downloaded to the root of build directory (`%APPVEYOR_BUILD_FOLDER%`).

The following environment variables must be set for script to work:

* `api_token` - AppVeyor REST API authentication token. Can be found/generated on [this page](https://ci.appveyor.com/api-token).
* `deploy_project` - project slug to download artifacts from. Project slug can be seen in the project URL.
* `deploy_version` - build version to deploy. If not specified artifacts from the most recent version will be downloaded.
* `deploy_artifact` - file name or deployment name of artifact to download. If not specified all artifacts will be downloaded.

These environment variables can be set on *Environment* tab of deployment project settings or in `appveyor.yml`:

```yaml
environment:
  api_token:
    secure: ABC123==
  deploy_project: my-web
  deploy_version: ''            # download artifacts from latest build if no version specified
  deploy_artifact: ''           # download all artifacts if empty
```

To download artifacts add the following PS script into "Before deploy" section of your project settings or `before_deploy` scripts of `appveyor.yml`:

```yaml
before_deploy:
  - ps: iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/deploy.ps1'))
```

Your own deployment logic can be put under `deploy_script` section, for example:

```yaml
deploy_script:
  - command stopping app pool
  - '"C:\Program Files (x86)\IIS\Microsoft Web Deploy V3\msdeploy.exe" -verb:sync -source:package="%webdeploy_package%" -dest:auto,ComputerName="%webdeploy_server%",UserName="%webdeploy_username%",Password="%webdeploy_password%",AuthType="Basic" -setParam:"IIS Web Application Name"="%webdeploy_site%" -allowUntrusted'
  - ...
  - command starting app pool
```

Add these to disable automatic build and test phases:

```yaml
build: off
test: off
```

Setup notifications if required:

```yaml
notifications:
  - provider: <provider_1>
    settings: ...
```

Complete `appveyor.yml` example:

```yaml
environment:
  # AppVeyor API token for your account, project, version and artifact(s) to download
  api_token:
    secure: ABC123==
  deploy_project: my-web
  deploy_version: ''            # download artifacts from latest build if no version specified
  deploy_artifact: ''           # download all artifacts if empty

  # deployment-specific settings
  # we are going to deploy using Web Deploy, so...
  webdeploy_package: '%appveyor_build_folder%\MyWebApp.zip'
  webdeploy_server: https://test.scm.azurewebsites.net:443/msdeploy.axd?site=test
  webdeploy_site: test
  webdeploy_username: $test
  webdeploy_password:
    secure: AAABBBCCC123==

# download project artifacts
before_deploy:
  - ps: iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/deploy.ps1'))

# here is your own custom deployment code
deploy_script:
  - '"C:\Program Files (x86)\IIS\Microsoft Web Deploy V3\msdeploy.exe" -verb:sync -source:package="%webdeploy_package%" -dest:auto,ComputerName="%webdeploy_server%",UserName="%webdeploy_username%",Password="%webdeploy_password%",AuthType="Basic" -setParam:"IIS Web Application Name"="%webdeploy_site%" -allowUntrusted'

# notifications
notifications:
  - provider: <provider_1>
    settings: ...

# disable build and test phases
build: off
test: off
```

## deploy.yml

If main and deployment projects share the same repository (most probably they do) you can put YAML deployment settings to a separate `deploy.yml` file, next to `appveyor.yml` and then set "Custom configuration .yml file name" on General tab to `deploy.yml`. To disable automatic triggering of deployment project on webhook you can either remove its webhook from repository or set branch filter to some non-existent branch, for example:

```yaml
branches:
  only:
    - deployment-project
```

## Main project

Main project is the project creating application packages and uploading them to build artifacts. With artifacts in place you can call deployment project (either manually or via API) to download and deploy artifacts.

To start a new build of deployment project during the build use [`Start-AppveyorBuild`](/docs/build-worker-api#start-new-build) cmdlet:

```yaml
environment:
  api_token:
    secure: ABC123==

deploy_script:
  - ps: Start-AppveyorBuild -ApiKey $env:api_token -ProjectSlug deploy-project -EnvironmentVariables @{ "deploy_version" = $env:appveyor_build_version }
```

Enjoy!

Follow us on Twitter: [@appveyor](https://twitter.com/appveyor)
