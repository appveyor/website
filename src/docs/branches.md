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
except:
  - /dev.*/     # You can use Regular expression to match multiple branch name(s)
  - playground
```

`gh-pages` branch is always excluded unless explicitly added in "only" list.

**Regular expressions** should be surrounded by `/`, otherise Appveyor will do simple case insensitive string comparison.

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
      - /dev-.*/

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


## Build on tags (GitHub and GitLab only)

By default AppVeyor starts a new build on any push to GitHub, whether it's a regular commit or a new tag. Repository tagging is frequently used to trigger deployment.

AppVeyor sets `APPVEYOR_REPO_TAG` environment variable to distinguish regular commits from tags - the value is `true` if tag was pushed; otherwise it's `false`. When it's `true` the name of tag is stored in `APPVEYOR_REPO_TAG_NAME`.

You can use `APPVEYOR_REPO_TAG` variable to trigger deployment on tag only, for example:

```yaml
- provider: Environment
  name: production
  on:
    branch: master
    appveyor_repo_tag: true
```

You can disable builds on new tags through UI (General tab of project settings) or in `appveyor.yml`:

```yaml
skip_tags: true
```
