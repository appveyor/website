---
layout: docs
title: Global YAML
---

<!-- markdownlint-disable MD022 MD032 -->
# Global YAML
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Use cases

Global YAML is a configuration in [appveyor.yml](/docs/appveyor-yml) format which is injected into all projects under the account.

Some of the use cases for global configuration:

* Common environment variables for all projects;
* Setting up debug RDP/SSH/VNC access for projects in case of failure;
* Configuring global notifications/webhooks for all projects;

## Merging rules

### Boolean values

Boolean value set to `true` in global config overrides the value defined on project level, for example:

Global YAML:

```yaml
shallow_clone: true
```

Project `appveyor.yml`:

```yaml
shallow_clone: false # if the setting is omitted it defaults to "false"
```

Resulting config:

```yaml
shallow_clone: true
```

### String values

String values defined in global config overrides values defined on project level, for example:

Global YAML:

```yaml
dotnet_csproj:
  file: '**\*.*proj'
```

Project `appveyor.yml`:

```yaml
dotnet_csproj:
  file: '**\*.csproj'
```

Resulting config:

```yaml
dotnet_csproj:
  file: '**\*.*proj'
```

### Lists

All list elements from Global YAML are inserted in the beginning of project list in the same order, for example:

Global YAML:

```yaml
init:
- appveyor version
- ps: iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-rdp.ps1'))
```

Project `appveyor.yml`:

```yaml
init:
- ps: $env:appveyor_build_worker_image
```

Resulting config:

```yaml
init:
- appveyor version
- ps: iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-rdp.ps1'))
- ps: $env:appveyor_build_worker_image
```

## Supported configuration sections

The following sections can be configured in Global YAML:

### Cloning

`shallow_clone: true` - enables shallow clone (one commit only, via SCM API) for all projects.

`clone_depth: <number>` - cloning depth for all projects.

`clone_script` - custom repository cloning script overriding built-in cloning method for all projects, for example:

```yaml
clone_script:
- ps: |
    if(-not $env:appveyor_pull_request_number) {
      git clone --depth=1 -q -c filter.lfs.smudge= -c filter.lfs.required=false --branch=$env:appveyor_repo_branch git@github.com:$env:appveyor_repo_name.git $env:appveyor_build_folder
      git checkout -qf $env:appveyor_repo_commit
      git lfs pull
    } else {
      git clone --depth=1 -q -c filter.lfs.smudge= -c filter.lfs.required=false git@github.com:$env:appveyor_repo_name.git $env:appveyor_build_folder
      git fetch -q origin +refs/pull/$env:appveyor_pull_request_number/merge:
      git checkout -qf FETCH_HEAD
      git lfs pull
    }
```

### Environment

`init` - scripts to run on build start *before* repository cloning.

`install` - scripts to run *after* repository is cloned.

`hosts` - `/etc/hosts` records common for all projects.

`cache` - build cache records applied to all projects.

`nuget` - enable project/account NuGet feeds for all projects; modify publish behavior:

```yaml
nuget:
  project_feed: true
  account_feed: true
  disable_publish_on_pr: true
```

`environment` - configure environment variables common for all projects. Environment variable with the same name defined on project level overrides the one defined in global configuration. `secure` variables are supported too. For example, the following global YAML enables RDP access to build VM in case of build failure for all projects:

```yaml
environment:
  APPVEYOR_RDP_PASSWORD:
    secure: encrypted-password==

on_failure:
- ps: $blockRdp = $true; iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-rdp.ps1'))
```

### Version patching

The following sections can be defined in Global YAML that will be applied to all projects.

`AssemblyInfo.*` patching:

```yaml
assembly_info:
  patch: true
  file: AssemblyInfo.*
  assembly_version: "2.2.{build}"
  assembly_file_version: "{version}"
  assembly_informational_version: "{version}"
```

.NET Core `*.*proj` files patching:

```yaml
dotnet_csproj:
  patch: true
  file: '**\*.csproj'
  version: '{version}'
  version_prefix: '{version}'
  package_version: '{version}'
  assembly_version: '{version}'
  file_version: '{version}'
  informational_version: '{version}'
```

### Build phase

`build_script` - custom scripts to run on build phase.

`before_build` - scripts to run before build phase.

`before_package` - scripts to run on built-in artifacts packaging phase.

`after_build` - scripts to run after build phase.

> Global build phase configuration has no effect if build phase on project level is explicitly disabled with <span style="white-space: nowrap">`build: off`</span>.

### Test phase

`test_script` - custom scripts to run on test phase.

`before_test` - scripts to run before test phase.

`after_test` - scripts to run after test phase.

> Global test phase configuration has no effect if test phase on project level is explicitly disabled with <span style="white-space: nowrap">`test: off`</span>.

### Artifacts

`artifacts` - define the list of common artifacts that will be collected for all projects. It's safe to provide nonexistent path here as the build won't fail if artifact
with specified path criteria was not found, for example:

```yaml
artifacts:
- path: Logs\*.txt
```

### Deployment phase

`deploy` - deployment providers to run on all projects. Providers defined in Global YAML are inserted before the ones defined on project level.

`deploy_script` - custom scripts to run on deploy phase.

`before_deploy` - scripts to run before deploy phase.

`after_deploy` - scripts to run after deploy phase.

> Global deploy phase configuration has no effect if deploy phase on project level is explicitly disabled with <span style="white-space: nowrap">`deploy: off`</span>.

### Notifications

`notifications` - global build notifications for all projects, for example:

```yaml
notifications:
- provider: Slack
  incoming_webhook:
    secure: AAABBB+CCC+DDD==
  channel: '#ci'
  on_build_failure: true
```

### Build finalizers

`on_success` - scripts to run on successful builds of all projects.

`on_failure` - scripts to run on failed builds of all projects, e.g. enable RDP/SSH access to the worker, or push crash dumps to artifacts.

`on_finish` - scripts to run on both successful and failed builds of all projects.