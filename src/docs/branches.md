---
layout: docs
title: Branches and Tags
---

<!-- markdownlint-disable MD022 MD032 -->
# Branches and Tags
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->


AppVeyor has built-in multi-branch support.

**Default branch** which can be specified on the General tab of project settings is built whenever a new build is started from *Projects UI*, *schedule* or *API*. When you do a push to the repository AppVeyor will start a new build of the branch in the last commit of the push data.



## White- and blacklisting

All branches are built by default. You can either [manually skip a build](/docs/how-to/filtering-commits/) or setup included/excluded branches on the **General** tab of project settings or in `appveyor.yml`:

To specify the list of allowed branches:

```yaml
branches:
  only:
    - master
    - production
```

To specify the list of branches that must be ignored:

```yaml
branches:
  except:
    - /dev/     # You can use Regular expression to match multiple branch name(s)
    - playground
```

`gh-pages` branch is always excluded unless explicitly added in "only" list.

**Regular expressions** should be surrounded by `/`, otherwise Appveyor will do simple case insensitive string comparison.

Please note, `only` **does not** exclude pull requests if they are based on that branch.

Despite the option name, `only` and `except` is applied to tag names too, so the above example using `only` would cause tags not trigger the build. For example to enable builds for a tag version scheme like `v1.0.0` you would need:

```yaml
branches:
  only:
    - master
    - production
    - /v\d+\.\d+\.\d+/
```

## Conditional build configuration

If you use [git flow](http://nvie.com/posts/a-successful-git-branching-model/) you may want to have a different build configuration (e.g. deploying to a different environment) in a feature branch. Changing `appveyor.yml` in a feature branch becomes an issue when you merge it into master overriding `appveyor.yml` and breaking master builds.

To solve this problem AppVeyor allows having multiple per-branch configurations in a single `appveyor.yml`.

Multiple configurations are defined as a list with `branches` section in every item that:

```yaml
# configuration for "master" branch
# build in Release mode and deploy to Azure
-
  branches:
    only:
      - master

  configuration: Release
  deploy:
    provider: AzureCS
    ...

# configuration for all branches starting from "dev-"
# build in Debug mode and deploy locally for testing
-
  branches:
    only:
      - /dev-/

  configuration: Debug
  deploy:
    provider: Local
    ...

# "fall back" configuration for all other branches
# no "branches" section defined
# do not deploy at all
-
  configuration: Debug
```


Unlike white- and blacklisting `branches` section here works like a selector, not a filter. Configuration selection algorithm is the following:

* Check configurations with `branches/only` section defined. If branch is found in configuration's `only` section use this configuration.
* Check configurations with `branches/except` section defined. If branch is NOT found in configuration's `except` section use this configuration.
* Check configurations WITHOUT `branches` section. If such configuration found use it.
* If all previous steps fail build is not run.


## Build on tags (GitHub, BitBucket, GitLab, Gitea)

By default AppVeyor starts a new build on any push to a repository, whether it's a regular commit or a new tag. Repository tagging is frequently used to trigger deployment.

AppVeyor sets `APPVEYOR_REPO_TAG` environment variable to distinguish regular commits from tags - the value is `true` if tag was pushed; otherwise it's `false`. When it's `true` the name of tag is stored in `APPVEYOR_REPO_TAG_NAME`.

You can use `APPVEYOR_REPO_TAG` variable to trigger deployment on tag only, for example:

```yaml
- provider: Environment
  name: production
  on:
    APPVEYOR_REPO_TAG: true
```

However, please note that in the case of an **annotated** tag, `branch` and `APPVEYOR_REPO_TAG` are mutually exclusive. This is because, for a webhook created as a result of an **annotated** tag, there is no practical, reliable way to recognize which branch the tag was created from. Therefore, with this setting, deployment will happen only for the master branch:

```yaml
- provider: Environment
  name: production
  on:
    branch: master # only this will work
    APPVEYOR_REPO_TAG: true # condition will never be evaluated
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
      APPVEYOR_REPO_TAG: true
```

You can disable builds on new tags through UI (General tab of project settings) or in `appveyor.yml`:

```yaml
skip_tags: true
```

## Sharing common configuration between branches

AppVeyor allows sharing common configuration between branches in a single `appveyor.yml`.

There is `for` node with a list of branch-specific configurations **overriding** common configuration defined on the top most level, for example:

```yml

# common configuration for ALL branches
environment:
  MY_VAR1: value-A

init:
- do_something_on_init.cmd

install:
- do_something_on_install.cmd

configuration: Debug

# here we are going to override common configuration
for:

# override settings for `master` branch
-
  branches:
    only:
      - master

  configuration: Release

  deploy:
    provider: FTP
    ...

# override settings for `dev-*` branches
-
  branches:
    only:
      - /dev-/

  environment:
    MY_VAR2: value-B

  deploy:
    provider: Local
    ...
```

In the example above we define `environment`, `init` and `install` sections for all branches as well as stating that default `configuration` is `Debug`.
Then, for `master` branch we override default settings by changing `configuration` to `Release` and adding deployment with `FTP` provider.
For `dev-*` branches we define a second environment variable `MY_VAR2` and enable deployment to `Local` environment.

Configuration merging rules:

* Scalar values such as `image`, `version`, `configuration`, `platform`, etc. defined on branch level override default ones;
* Script sections such `init`, `install`, `before_build`, `test_script`, etc. defined on branch level override default ones;
* Environment variables defined in `environment` sections are merged (new) and overridden (existing);
* Build matrix defined on branch level merges with default one;
* `deploy`, `artifacts`, `notifications` section can be either overridden or extended.

For example, consider the following configuration:

```yaml
artifacts:
- path: bin

deploy:
- provider: Local
  ...

notifications:
- provider: Email
  ...

for:
-
  branches:
    only:
      - master

  artifacts:
  - path: docs

  deploy: off

  notifications:
    provider: Slack
    ...
```

In the example above we do the following:

* For `master` branch we *adding* `docs` folder to artifacts definition, so both `bin` and `docs` folders collected. Both default and branch-specific collections were merged.
* For `master` branch we *disable* any deployment. `off` or `none` on branch-level clears default collection.
* For `master` branch we *replace* all notifications on default level with a single `Slack` notification.
