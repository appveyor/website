---
title: Build worker improvements
---

When deploying web application to different environments you don’t want to re-build application package every time with different configurations, but you want to deploy the same package (artifact) with some environment-specific settings configured during deployment. When using Web Deploy the problem can be easily solved by Web Deploy parametrization.

### Usage scenarios

Most common use cases for Web Deploy parametrization is updating node/attribute value in XML files or replacing a token in text files, for example:

* appSettings in `web.config`
* connection strings in `web.config`
* WCF endpoints
* Paths to log files
* Database name in SQL install script

### Parameters.xml

To enable Web Deploy parametrization add `parameters.xml` file in the root of your web application.

![vs-solution-explorer](/assets/img/docs/deployment/web-deploy/vs-solution-explorer.png)

`Parameters.xml` contains the list of parameters required (or supported) by your Web Deploy package. In the example below we introduce two parameters - one to update path to log file in `appSettings` section of `web.config` and another one to set database name in SQL script.

Parameter element describes the name, default value and the places where and how this parameter must be applied.

`Parameters.xml` for our example:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<parameters>
  <parameter name="LogsPath" defaultValue="logs">
    <parameterEntry kind="XmlFile" scope="\\web.config$" match="/configuration/appSettings/add[@key='LogsPath']/@value" />
  </parameter>
  <parameter name="DatabaseName">
    <parameterEntry kind="TextFile" scope="\\Database\\install_db.sql$" match="@@database_name@@" />
  </parameter>
```

When Web Deploy package is built you can open it in the explorer and see `parameters.xml` in the root:

![webdeploy-package](/assets/img/docs/deployment/web-deploy/webdeploy-package.png)

Resulting `parameters.xml` combines your custom parameters and system ones such as `IIS Web Application Name`. You don’t have to set `IIS Web Application Name` parameter explicitly - AppVeyor does that for you.

Read more about defining parameters: <https://technet.microsoft.com/en-us/library/dd569084(v=ws.10).aspx>

### Setting parameters during deployment

Web Deploy provider in AppVeyor analyzes Web Deploy package and looks into **environment variables** to set parameter values with matching names.

When promoting specific build from Environment page you set variables on environment settings page:

![environment-variables](/assets/img/docs/deployment/web-deploy/environment-variables.png)

When deploying during the build session environment variables are used instead. You can set build environment variables on Environment tab of project settings, `appveyor.yml` or programmatically during the build.

![project-environment-variables](/assets/img/docs/deployment/web-deploy/project-environment-variables.png)

Variables defined during the build override those ones defined on Environment level.

Web Deploy parametrization is supported by [Deployment Agent](/docs/deployment/agent/) too when deploying from Web Deploy package.

Related articles:

* [Deploying using Web Deploy](/docs/deployment/web-deploy/)
* [Deploying to remote servers with AppVeyor Deployment Agent](/docs/deployment/agent/)
