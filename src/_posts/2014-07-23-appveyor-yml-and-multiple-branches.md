---
title: appveyor.yml and multiple branches
---

## The problem

If you use [git flow](http://nvie.com/posts/a-successful-git-branching-model/) you may want to have
a different build configuration (e.g. deploying to a different environment) in a feature branch.
Changing `appveyor.yml` in a feature branch becomes an issue when you merge it into master overriding
`appveyor.yml` and breaking master builds.

## The solution

To solve this problem AppVeyor allows having multiple per-branch configurations in a single `appveyor.yml`.

Multiple configurations are defined as a **list** with `branches` section in every item that:

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

Unlike white- and blacklisting `branches` section here works like a selector, not a filter.
Configuration selection algorithm is the following:

* Check configurations with `branches/only` section defined.
  If branch is found in configuration's `only` section use this configuration.
* Check configurations with `branches/except` section defined.
  If branch is NOT found in configuration's `except`section use this configuration.
* Check configurations **without** `branches` section. If such configuration is found use it.
* If all previous steps fail, the build is not run.

Enjoy!
