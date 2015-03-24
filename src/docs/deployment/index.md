---
layout: docs
title: Deployment
---

# Deployment
{:.no_toc}

* Comment to trigger ToC generation
{:toc}


## Overview

The fact AppVeyor deployment requires build artifacts on input makes deployment process predictable and repeatable.

AppVeyor allows you to deploy using multiple providers **as part of the build process** (inline deployment) as well as **promote builds to existing environments** (environment deployment).

**Inline deployment** runs as the last phase in the [build pipeline](/docs/build-configuration#build-pipeline) and allows configuring multiple deployments running synchronously one-by-one with results in build console.

**Environment deployment** is triggered manually or through API to deploy "green" build to existing environment. A new deployment is registered within a project/environment with results in deployment console. If you don't have any existing environments, you can create one at https://ci.appveyor.com/environments.

The table below summarizes key differences between two modes with lists of deployment providers available in each mode:

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
    <td><a href="/docs/deployment/agent">Deployment Agent</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/ftp">FTP, SFTP</a></td>
    <td><a href="/docs/deployment/ftp">FTP, SFTP</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/web-deploy">Web Deploy</a></td>
    <td><a href="/docs/deployment/web-deploy">Web Deploy</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/sql-database-ssdt">SQL Database (SSDT)</a></td>
    <td><a href="/docs/deployment/sql-database-ssdt">SQL Database (SSDT)</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/azure-webjob">Azure WebJob</a></td>
    <td><a href="/docs/deployment/azure-webjob">Azure WebJob</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/azure-blob">Azure Blob</a></td>
    <td><a href="/docs/deployment/azure-blob">Azure Blob</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/azure-cloud-service">Azure Cloud Service</a></td>
    <td><a href="/docs/deployment/azure-cloud-service">Azure Cloud Service</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/amazon-s3">Amazon S3</a></td>
    <td><a href="/docs/deployment/amazon-s3">Amazon S3</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/nuget">NuGet</a></td>
    <td><a href="/docs/deployment/nuget">NuGet</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/github">GitHub Releases</a></td>
    <td><a href="/docs/deployment/github">GitHub Releases</a></td>
</tr>
<tr>
    <td><a href="/docs/deployment/bintray">Bintray</a></td>
    <td><a href="/docs/deployment/bintray">Bintray</a></td>
</tr>
<tr>
    <td>Script</td>
    <td>-</td>
</tr>
<tr>
    <td><a href="/docs/deployment/environment">Environment</a></td>
    <td>-</td>
</tr>
</table>



## Environment variables in provider settings

You can use standard and custom environment variables in provider settings, for example you can set remote FTP folder as `$(appveyor_build_version)\artifacts` where `$(appveyor_build_version)` will be replaced with current build version.



## Conditional deployment

When you deploy as part of the build process you can control under which conditions deployment should be run.

By default, AppVeyor deploys from all branches, but you can include only specific branches. Also, you can use any environment variables to have more complex conditions.

For example, to deploy from "master" branch and only artifacts built for "x86" platform (`platform` is the name of environment variable):

{% highlight yaml %}
- provider: Environment
  name: production
  on:
    branch: master
    platform: x86
{% endhighlight %}


## Deploy on tag (GitHub and GitLab only)

By default AppVeyor starts a new build on any push to GitHub whether it's regular commit or a new tag. Repository tagging frequently used to trigger deployment.

AppVeyor sets `APPVEYOR_REPO_TAG` environment variable to distinguish regular commits from tags - the value is `true` if tag was pushed; otherwise it's `false`. When it's `true` the name of tag is stored in `APPVEYOR_REPO_TAG_NAME`.

You can use `APPVEYOR_REPO_TAG` variable to trigger deployment on tag only, for example (for Environment provider):

{% highlight yaml %}
- provider: Environment
  name: production
  on:
    branch: master
    appveyor_repo_tag: true
{% endhighlight %}

You can disable builds on new tags through UI (General tab of project settings) or in `appveyor.yml`:

{% highlight yaml %}
skip_tags: true
{% endhighlight %}
