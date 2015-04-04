---
layout: docs
title: Deploying Azure Cloud Services
---

# Deploying Azure Cloud Services

<!--TOC-->

Azure Cloud Service deployment provider assumes there is only one **Azure Cloud Service package** (file with `.cspkg` extension) in build artifacts and at least one **Azure Cloud Service configuration** artifact (file with `.cscfg` extension).



## Automatic packaging

To build Azure CS project and automatically push it as artifact enable **Publish Azure Cloud Service projects** option on **Build** tab of project settings.

To enable packaging of Azure CS projects in `appveyor.yml`:

    build:
      publish_azure: true

AppVeyor will find Azure Cloud Service project (`.ccproj`) and package it. Created Cloud Service package (`<project-name>.cspkg`) and default "Cloud" configuration (`<project-name>.cscfg`) will be published to build artifacts. In addition to that **all** `.cscfg` files found in Cloud Service project folder are pushed to artifacts with names `<project-name>.<config>.cscfg`. Build artifacts page would look like below:

![azure-cloud-service-artifacts](/site/images/docs/azure-cloud-service-artifacts.png)




## Packaging from script

To build Azure Cloud Service package from the script you can use the following command:

    msbuild <azure-cs-project>.ccproj /t:Publish /p:PublishDir=%APPVEYOR_BUILD_FOLDER% /p:TargetProfile=<config>

To push package and configuration to artifacts:

    appveyor PushArtifact <azure-cs-project>.cspkg
    appveyor PushArtifact ServiceConfiguration.<config>.cscfg -FileName <azure-cs-project>.cscfg

> Replace `<azure-cs-project>` with the name of your Azure CS project and `<config>` with the name of target configuration, e.g. `Cloud`, `Local`.




## Configuring deployment

Sample `appveyor.yml` configuration:

    deploy:
      provider: AzureCS
      subscription_id: 0C8CCAAC-03F4-4659-B927-EFAB8874BE8D
      subscription_certificate:
        secure: WJjB4oIJKDRXRkaP2hB5Dnoaq1AXOPxf/d2gs0M0Pu6kW0SFBOVw ... z9SVqWcnozkHxylgwaaFA==
      storage_account_name: mystorage
      storage_access_key:
        secure: XMdn4xfPcYlZFYgvbytc8Q==
      service: myazure-service
      slot: production
      deployment_label: $(appveyor_build_version)
      artifact: myservice.cspkg
      target_profile: Cloud   # optional .cscfg configuration name to deploy with
      do_not_start_deployment: false
      recreate_deployment: false

To get `subscription_id` and `subscription_certificate` [download publish settings and subscription for your Azure account](https://manage.windowsazure.com/publishsettings)
and then grab both values from downloaded `<subscription>.publishsettings` XML file.


## Cloud Service Deploy Parametrization

When deploying Cloud Services to different environments you don't want to re-build application package every time with different configurations, but you want to deploy the same package (artifact) with some environment-specific settings configured during deployment. When using Cloud Service Deploy the problem can be easily solved by Web Deploy parametrization.

### Setting parameters during deployment
Cloud Service Deploy provider analyzes the deployment package and looks into environment variables to set parameter values with matching names.

When promoting specific build from Environment page you set variables on environment settings page:

![environment-variables](/site/images/docs/deployment/web-deploy/environment-variables.png)

When deploying during the build session environment variables are used instead. You can set build environment variables on Environment tab of project settings, `appveyor.yml` or programmatically during the build.

![project-environment-variables](/site/images/docs/deployment/web-deploy/project-environment-variables.png)

Variables defined during the build override those ones defined on Environment level.
