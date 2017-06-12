---
layout: docs
title: Deployment
---

<!-- markdownlint-disable MD022 MD032 -->
# Deployment
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Overview

The fact that AppVeyor deployment requires build artifacts on input makes the deployment
process predictable and repeatable.

AppVeyor allows you to deploy using multiple providers **as part of the build process**
(inline deployment) as well as **promote builds to existing environments** (environment deployment).

**Inline deployment** runs as the last phase in the [build pipeline](/docs/build-configuration#build-pipeline)
and allows the configuration of multiple deployments running synchronously one-by-one with results in the build console.

**Environment deployment** is triggered manually or through the API to deploy a "green" build to an existing environment.
A new deployment is registered within a project/environment with results in the deployment console.
If you don't have any existing environments, you can create one at <https://ci.appveyor.com/environments>.

The table below summarizes the key differences between the two modes with lists of deployment
providers available in each mode:

<table class="centered">
<tr>
    <th>Inline deployment</th>
    <th>Environment deployment</th>
</tr>
<tr>
    <td>Synchronous</td>
    <td>Asynchronous</td>
</tr>
<tr>
    <td>Deploys local artifact files</td>
    <td>Deploys artifacts from cloud storage</td>
</tr>
<tr>
    <td>Build console</td>
    <td>Deployment console</td>
</tr>
<tr>
    <td>Do not register deployment</td>
    <td>Register new deployment</td>
</tr>
<tr>
    <td>Local provider</td>
    <td>-</td>
</tr>
<tr>
    <td>-</td>
    <td><a href="/docs/deployment/agent/">Deployment Agent</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/ftp/">FTP, SFTP</a></td>
    <td><a href="/docs/deployment/ftp/">FTP, SFTP</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/web-deploy/">Web Deploy</a></td>
    <td><a href="/docs/deployment/web-deploy/">Web Deploy</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/sql-database-ssdt/">SQL Database (SSDT)</a></td>
    <td><a href="/docs/deployment/sql-database-ssdt/">SQL Database (SSDT)</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/azure-webjob/">Azure WebJob</a></td>
    <td><a href="/docs/deployment/azure-webjob/">Azure WebJob</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/azure-blob/">Azure Blob</a></td>
    <td><a href="/docs/deployment/azure-blob/">Azure Blob</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/azure-cloud-service/">Azure Cloud Service</a></td>
    <td><a href="/docs/deployment/azure-cloud-service/">Azure Cloud Service</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/amazon-s3/">Amazon S3</a></td>
    <td><a href="/docs/deployment/amazon-s3/">Amazon S3</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/nuget/">NuGet</a></td>
    <td><a href="/docs/deployment/nuget/">NuGet</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/github/">GitHub Releases</a></td>
    <td><a href="/docs/deployment/github/">GitHub Releases</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/bintray/">Bintray</a></td>
    <td><a href="/docs/deployment/bintray/">Bintray</a></td>
</tr>
<tr>
    <td>Script</td>
    <td>-</td>
</tr>
<tr>
    <td><a href="/docs/deployment/environment/">Environment</a></td>
    <td>-</td>
</tr>
</table>


## Environment variables in provider settings

You can use standard and custom environment variables in provider settings, for example
you can set a remote FTP folder as `$(appveyor_build_version)\artifacts` where `$(appveyor_build_version)`
will be replaced with the current build version.


## Conditional deployment

When you deploy as part of the build process you can control the conditions under which a
deployment should be run.

By default, AppVeyor deploys from all branches, but you can include only specific branches.
Also, you can use any environment variables to have more complex conditions.

For example, to deploy from the "master" branch and only artifacts built for the "x86" platform
(`platform` is the name of an environment variable):

```yaml
- provider: Environment
  name: production
  on:
    branch: master
    platform: x86
```

## Deploy on tag (GitHub and GitLab only)

By default AppVeyor starts a new build on any push to GitHub, whether it's a regular commit or a new tag.
Repository tagging is frequently used to trigger deployment.

AppVeyor sets the `APPVEYOR_REPO_TAG` environment variable to distinguish regular commits from tags - the value is `true` if the tag was pushed; otherwise it's `false`. When it's `true` the name of the tag is stored in `APPVEYOR_REPO_TAG_NAME`.

You can use the `APPVEYOR_REPO_TAG` variable to trigger deployment on tag only, for example:

```yaml
- provider: Environment
  name: production
  on:
    appveyor_repo_tag: true
```

However, please note that `branch` and `appveyor_repo_tag` are mutually exclusive. This is because, in the case of tag, it replaces the branch in the webhook content and there is no practical reliable way to recognize from what branch the tag was created. Therefore with this setting deployment will happen only for the master branch:

```yaml
- provider: Environment
  name: production
  on:
    branch: master # only this will work
    appveyor_repo_tag: true # condition will never be evaluated
```

So if you need to deploy on both branch and tag, please create two `provider` sections under `deploy` like this:

```yaml
deploy:
  - provider: Environment
    name: production
    on:
      branch: master

  - provider: Environment
    name: production
    on:
      appveyor_repo_tag: true
```

You can disable builds on new tags through the UI (General tab of project settings) or in `appveyor.yml`:

```yaml
skip_tags: true
```

