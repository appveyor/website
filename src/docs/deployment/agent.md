---
layout: docs
title: Deploying to remote servers with AppVeyor Deployment Agent
---

<!-- markdownlint-disable MD022 MD032 -->
# Deploying to remote servers with AppVeyor Deployment Agent
{:.no_toc}

AppVeyor Deployment Agent (Deployment Agent) is a service running on remote server and helping to deploy select artifact as IIS website or Windows application/service.

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->


## Software requirements

The following is required on the server to run Deployment Agent:

* Windows Server 2012 (Windows 8) or newer
* .NET Framework 4.5.2 or newer
* Web Role (IIS) is installed if you are deploying web site


## Installing AppVeyor Deployment Agent

1. Add new environment with **Agent** provider selected. Open environment settings and copy **Environment access key**.
2. Download AppVeyor Deployment Agent (.msi)
    * [**AppVeyor Deployment Agent - latest** (v6.3.3)]({{ site.url }}/downloads/deployment-agent/latest/AppveyorDeploymentAgent.msi)
    * [AppVeyor Deployment Agent v6.3.3]({{ site.url }}/downloads/deployment-agent/6.3.3/AppveyorDeploymentAgent.msi)
    * [AppVeyor Deployment Agent v6.3.2]({{ site.url }}/downloads/deployment-agent/6.3.2/AppveyorDeploymentAgent.msi)
    * [AppVeyor Deployment Agent v6.1.0]({{ site.url }}/downloads/deployment-agent/6.1.0/AppveyorDeploymentAgent.msi)
    * [AppVeyor Deployment Agent v5.5.0]({{ site.url }}/downloads/deployment-agent/5.5.0/AppveyorDeploymentAgent.msi)
    * [AppVeyor Deployment Agent v5.3.0]({{ site.url }}/downloads/deployment-agent/5.3.0/AppveyorDeploymentAgent.msi)
    * [AppVeyor Deployment Agent v5.0.8]({{ site.url }}/downloads/deployment-agent/5.0.8/AppveyorDeploymentAgent.msi)
    * [AppVeyor Deployment Agent v3.52.0]({{ site.url }}/downloads/deployment-agent/3.52.0/AppveyorDeploymentAgent.msi)
3. Specify **Environment access key** during Deployment Agent installation.
4. Server is ready for deployment.


## Unattended Deployment Agent installation

Run the following in PowerShell console:

```powershell
(new-object net.webclient).DownloadFile('https://www.appveyor.com/downloads/deployment-agent/latest/AppveyorDeploymentAgent.msi', 'AppveyorDeploymentAgent.msi')
msiexec /i AppveyorDeploymentAgent.msi /quiet /qn /norestart /log install.log ENVIRONMENT_ACCESS_KEY=<your_access_key> DEPLOYMENT_GROUP=<your_deployment_group>
```

Replace `<your_access_key>` and `<your_deployment_group>` with your environment access key and your deployment group respectively.


## What artifacts can be deployed

Deployment Agent recognizes artifacts of two types which may contain either web application or Windows application/service:

* Zip archive
* Web Deploy package

To be deployable with Deployment Agent *artifact must have a name*.
Name should not have any spaces. All unnamed artifacts are skipped by Deployment Agent provider.


## How to get named artifacts

There are few possible ways of packaging artifact deployable by Agent:

1. When **Package Web Application projects** option is enabled on **Build** tab of project settings AppVeyor automatically publishes (applies web config transforms)
   and uploads VS.NET Web Application projects as artifacts named after the name of VS.NET project.
2. Specify **Deployment name** while adding artifact entry on **Artifacts** tab of project settings.
3. From script, for example **After build script**:

    ```text
    appveyor PushArtifact <zip_path> -DeploymentName MyApp
    ```

## Configuring deployment settings

Use **Provider settings** of Agent environment to configure *which* artifacts should be deployed by Agent and *how*. By default, nothing configured - nothing deployed.

Settings have format `<artifact_name>.<setting_name>` where `<artifact_name>` is artifact's **Deployment name**.

For example, let the build has the following artifacts:

![Artifacts](/assets/img/docs/agent-deploy-artifacts.png)

In order for Deployment Agent to deploy that artifact as IIS web site **Provider settings** will be:

![Artifacts](/assets/img/docs/agent-provider-settings.png)

## Global settings

There are few settings which have format `<setting_name>` instead of `<artifact_name>.<setting_name>`. Those settings are global and apply to each artifact deployment.

`agents_expected` - Number of remote agents expected to start deployment. Deployment marked as failed if number of agents started deployment is less than expected. Default is 1. Set to 0 to disable this check.

`agents_timeout` - Time in seconds to wait for remote agents to start deployment. Default is 10 seconds. Minimum is 5 seconds. Maximum is 60 seconds. Increase in case default value of 10 seconds appears to be not enough. Decrease if observe agents always pick up job much faster.

## Overriding settings while deploying from build

You can use environment variables for setting values on environment configuration, for example:

![agent-settings-with-variables](/assets/img/docs/deployment/agent/agent-settings-with-variables.png)

where `site_name` is environment variable. At the bottom of that screen we are defining its "default" value,
i.e. the value used when you deploy from Environments and build environment variables are not present.

However, when you deploy from a build you can override those environment variables like:

```yaml
deploy:
  - provider: Environment
    name: test-pc
    site_name: www.site-to-deploy.com
```

Alternatively, that site name can be deployed somewhere during the build, so the following construction is also possible:

```yaml
environment:
  site_to_deploy: www.site-to-deploy.com

deploy:
  - provider: Environment
    name: test-pc
    site_name: $(site_to_deploy)
```


## Deploying artifact package as IIS web site

    <artifact_name>.deploy_website: true

Other settings:

* `site_id` - Optional. Site numeric ID.
* `site_name` - The name of existing or new website, e.g. "Default Web Site".
* `application_name` - optional web application name (IIS virtual directory) to deploy web app into.
* `application_path` - optional root directory for web application (IIS virtual directory).
* `apppool_name` - the name of IIS application pool. If pool does not exist it will be created.
* `aspnet_core` - Optimize application pool for ASP.NET Core. No needed for classic websites, add and set to `true` for ASP.NET Core.
* `port` - Port of website binding.
* `ip` - IP address of website binding.
* `hostname` - Host header value of website binding.
* `protocol` - Protocol value of website binding. Could be `http` (default if not specified) or `https` or `net.tcp`.
* `certificate` - Certificate associated with `https` binding. This value could be certificate name or thumbprint, for example `*.mydomain.com` or `0B2D18387549968CB4CC30F21D6CC4C0830B679B`. If certificate specified *protocol* is changed to `https`.
* `write_access` - When set to `true` Agent sets **Modify** permissions for application pool identity on website root directory.
* `path` - Website root directory on the target server. If not specified and website already exists its root directory is not changed.

    If not specified and website does not exists default directory path is `c:\appveyor\applications\<artifact_name>`.

* `remove_files` - Agent uses Web Deploy to synchronize website folder contents. By default, it only adds new files and modifies existing.

    When `remove_files` is set to `true` Agent performs full content synchronization, i.e. deletes files at destination that don't exist in the package.

* `skip_dirs` - semicolon list of regular expressions specifying the list of directories to skip while synchronizing web site contents, for example `\\App_data;\\uploads`.
* `skip_files` - semicolon list of regular expressions specifying the list of files to skip while synchronizing web site contents, for example `web.config` (all web.configs) or only the root config for MVC apps `^((?!Views).)*web\.config$` (thanks to [this blog post](http://keza.net/2011/11/15/skipping-mvc-web-config-files-with-msdeploy/)).
* `app_offline` - places app_offline.htm page into the root of web application before sync to take app offline and then remove the page when deployment has finished.
* `group` - Deployment group.
* `deploy_order` - Optional. Allows changing the deployment order of artifacts. Artifacts are deployed in ascending order. Deployment order is set to `0` if not specified.
* `skip_acl` - Optional. If `true`, deployment agent skips step of setting ACLs on site folder for application pool account (can be time consuming for large sites).

When deploying web app from Web Deploy package you can use
[Web Deploy parametrization](/docs/deployment/web-deploy#web-deploy-parametrization)
with environment variables.

You can specify multiple bindings in `hostname`, `ip` and `port` separated by semi-colon. Below is an example of how 3 bindings can be configured:

* http \*:80:mysite.com
* http \*:80:www.mysite.com
* https \*:443: cert=\*.mysite.com

![agent-multiple-bindings](/assets/img/docs/deployment/agent/agent-multiple-bindings.png)


## Deploying artifact package as a Windows application

    <artifact_name>.deploy_app: true

Other properties:

* `path` - Application root directory. If not specified default application path is `c:\appveyor\applications\<artifact_name>`.
* `remove_files` - Remove additional files at destination.
* `group` - Deployment group.
* `skip_dirs` - semicolon list of regular expressions specifying the list of directories to skip while synchronizing application folder contents, for example `\\Logs;\\files`.
* `skip_files` - semicolon list of regular expressions specifying the list of files to skip while synchronizing application folder contents.
* `deploy_order` - Optional. Allows changing the deployment order of artifacts. Artifacts are deployed in ascending order. Deployment order is set to `0` if not specified.


## Deploying artifact package as a Windows service

    <artifact_name>.deploy_service: true

Other properties:

* `service_executable` - File name of Windows service executable, e.g. `myapp.service.exe`. If not specified the first executable found in application directory will be used.
* `service_name` - The name of Windows service. If specified Windows service will be created.
* `service_display_name` - Display name of Windows service. If not specified `service_name` will be used.
* `service_username` - The name of user account to run service under. If not specific `LocalSystem` is used.
* `service_password` - User account password if `service_username` is set.
* `path` - Application root directory. If not specified default application path is `c:\appveyor\applications\<artifact_name>`.
* `remove_files` - Remove additional files at destination.
* `group` - Deployment group.
* `skip_dirs` - semicolon list of regular expressions specifying the list of directories to skip while synchronizing application folder contents, for example `\\Logs;\\files`.
* `skip_files` - semicolon list of regular expressions specifying the list of files to skip while synchronizing application folder contents.
* `deploy_order` - Optional. Allows changing the deployment order of artifacts. Artifacts are deployed in ascending order. Deployment order is set to `0` if not specified.
* `do_not_start` - Optional. Set to `true` if Windows Service should not be started after installation.


## Publishing SSDT package artifact to SQL Server

Deployment Agent supports publishing of SSDT package artifacts (with `.dacpac` extension) to SQL Server instance.

To direct AppVeyor that named artifact should be treated as DACPAC package:

    <artifact_name>.deploy_database: true

Other properties:

* `connection_string` - SQL connection string to the target database, for example `Server=(local)\SQLEXPRESS;Database=my_app;User ID=myuser;Password=password`
* `<artifact_name>.<deploy_setting>` where `<deploy_setting>` is a setting described in [Publishing SQL Server databases from SSDT packages](/docs/deployment/sql-database-ssdt/).
* `<artifact_name>.sqlcmd.<variable_name>` - format for specifying SQLCMD variables.

For example, given `.dacpac` artifact's deployment name is `MyDatabase`:

    MyDatabase.deploy_database                  true
    MyDatabase.connection_string                Server=(local)\SQLEXPRESS;Database=my_app;Integrated security=SSPI;
    MyDatabase.sqlcmd.MYVAR                     hello, world!
    MyDatabase.backup_database_before_changes   true

* `deploy_order` - Optional. Allows changing the deployment order of artifacts. Artifacts are deployed in ascending order. Deployment order is set to `0` if not specified.


## Installing MSI package artifact on remote machine

With AppVeyor Deployment Agent you can run the installation of MSI artifact (with `.msi` extension) on the remote machine. Agent uses `msiexec` command-line utility to install the package. MSI package should support silent mode (`/quiet` switch). We recommend using [WiX](http://wixtoolset.org/) for building application installation packages.

    <artifact_name>.deploy_msi: true

Other properties:

* `uninstall_application` - if this setting is set the agent will try to uninstall the previous version of the application before installing a new one. Application name should be the same as you see in "Add/Remove Programs" control panel snap-in.
* `<artifact_name>.<property_name>` - set `<property_name>` MSI custom property while installing the app. All properties are addedt to `msiexec` command as `PROPERTY="VALUE" PROPERTY="VALUE" ...`.


For example, given `.msi` artifact's deployment name is `MyAppInstall`:

    MyAppInstall.deploy_msi                 true
    MyAppInstall.uninstall_application      My Application



## Running PowerShell scripts on target server during deployment

`before-deploy.ps1` PowerShell script in the root of application folder will be called **before** every deployment.
`deploy.ps1` PowerShell script in the root of application folder will be called **after** every successful deployment.

During scripts execution the following environment variables are available:

Job details:

* `APPVEYOR` - script runs in AppVeyor environment
* `CI` - script runs in AppVeyor environment
* `APPVEYOR_PROJECT_ID` - Unique system ID of project
* `APPVEYOR_PROJECT_NAME` - Project display name
* `APPVEYOR_PROJECT_SLUG` - Project slug that you can see in URL, e.g. myproject-123
* `APPVEYOR_BUILD_ID` - Unique system ID of build
* `APPVEYOR_BUILD_NUMBER` - Build number of deploying artifact
* `APPVEYOR_BUILD_VERSION` - Build version on deploying artifact
* `APPVEYOR_BUILD_JOB_ID` - Unique system ID of a build job being deployed
* `APPVEYOR_JOB_ID` - Unique system ID of deployment job
* `APPVEYOR_REPO_NAME` - Repository name in the form `owner-name/repo-name`
* `APPVEYOR_REPO_BRANCH` - Build branch
* `APPVEYOR_REPO_TAG` - `true` if build has started by pushed tag; otherwise `false`.
* `APPVEYOR_REPO_TAG_NAME` - contains tag name for builds started by tag; otherwise this variable is undefined.
* `APPVEYOR_REPO_COMMIT` - Build commit ID (SHA)
* `APPVEYOR_REPO_COMMIT_AUTHOR` - Commit author
* `APPVEYOR_REPO_COMMIT_AUTHOR_EMAIL` - Commit author's email
* `APPVEYOR_REPO_COMMIT_TIMESTAMP` - Commit timestamp
* `APPVEYOR_REPO_COMMIT_MESSAGE` - Commit message

Application details:

* `APPLICATION_NAME` - application name (artifact deployment name)
* `APPLICATION_DEPLOY_WEBSITE` - `true` if artifact deployed as IIS web site
* `APPLICATION_PATH` - application or website root folder
* `APPLICATION_REMOVE_FILES` - perform full sync of package/application folder contents, i.e. remove additional files at destination
* `APPLICATION_SITE_NAME` - IIS web site name

Artifact details:

* `ARTIFACT_FILENAME` - artifact file name as in cloud storage
* `ARTIFACT_LOCALPATH` - local file name of downloaded artfact package
* `ARTIFACT_NAME` - artifact deployment name
* `ARTIFACT_SIZE` - package size in bytes
* `ARTIFACT_TYPE` - artifact type
* `ARTIFACT_URL` - artifact package download URL valid for 10 minutes



### Calling script block once per deployment

<!--[TBD - explain some usage scenarios]-->

In your `before-deploy.ps1` or `deploy.ps1` use the following code to run once per deployment/per cluster:

```powershell
if (Enter-OncePerDeployment "block_name")
{
    # your code that must be run once per cluster
}
```

Replace `block_name` with some value identifying operations inside the block, e.g. "install_sql"


## Troubleshooting

Open Event Viewer, expand **Applications and Services Logs** node and navigate to **Deployment Agent** event log.
