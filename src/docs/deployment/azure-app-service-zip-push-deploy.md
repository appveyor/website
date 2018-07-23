---
layout: docs
title: Publishing to Azure App Service with Zip Push Deploy
---

# Publishing to Azure App Service with Zip Push Deploy

AppVeyor allows publishing of `.zip` artifacts to Azure App Service with [Zip Push Deploy](https://blogs.msdn.microsoft.com/appserviceteam/2017/10/16/zip-push-deployment-for-web-apps-functions-and-webjobs/).

## Packaging artifact

For ASP.NET Web application please set `publish_wap_xcopy: true` or select `Package Web Applications for XCopy deployment` under **Build** if you use UI.
For other kind of application, simple add folder with all site assets (for example `\src\dist`) to `artifacts` section in YAML (or **Artifacts** tab in UI) and AppVeyor will zip it.

## Publishing credentials

Please use deployment credentials you can set in Azure Portal. Optionally you can use username and password from downloaded website publishing profile XML, but this will not work transparently with deployment slots.

## Provider settings

* **App Service site name** (`website`) - App Service site name without `.azurewebsite.net`, e.g. `mywebsite`.
* **App Service Environment** (`appservice_environment`) - Optional. Azure website is deployed to Azure AppService environment.
* **App Service Environment Name** (`appservice_environment_name`) - Available if **App Service Environment** is checked. AppService environment website default URL part, located before `p.azurewebsites.net`.
* **Deployment slot name** (`slot`) - Optional. If not specified artifact will be deployed to default (production) slot. Please note that you have to use Deployment (aka User-level) and not publishing profile (aka Site-level) credentials to transparently deploy to different slots.
* **Deployment credentials username** (`username`) - Username from deployment credentials you can set in Azure Portal. Optionally you can use username and password from downloaded website publishing profile XML, but this will not work transparently with deployment slots.
* **Deployment credentials password** (`password`) - Password from deployment credentials you can set in Azure Portal. As said earlier you can use credentials from downloaded website publishing profile XML, but this will not work transparently with deployment slots.
* **Artifact(s) to deploy** (`artifact`) - Optional. Artifact "deployment name" or filename to push.
* **Async deployment** (`async_deploy`) - Optional. When selected in UI or set to `true` in YAML, deployment finished as soon as the file is uploaded. URL that can be queried for realtime deployment status will be returned.
* **Retry attempts** (`retry_attempts`) - Optional. Specifies the number of times the provider will retry after a failure. The default number of retries is 0.
* **Retry interval** (`retry_interval`) - Optional. Specifies, in milliseconds, the interval between provider retry attempts. The default is 1000 (one second).

Configuring in `appveyor.yml`:

```yaml
deploy:
- provider: AzureAppServiceZipDeploy
  website: mywebsite
  username: myDeploymentUsername
  password:
    secure: dNPsSiN7aAwAe2K7Aw+IVw==
```
