---
title: Sharing configuration between branches in appveyor.yml
---

## The problem

There are two options to have branch-specific configuration with `appveyor.yml`:

1. Commit `appveyor.yml` into each branch with branch-specific settings.
2. Use [Conditional build configuration](/docs/branches/#conditional-build-configuration) where `appveyor.yml` has *a list* of configurations for different branches.

The problem with 1st approach is merging as you are overriding `appveyor.yml` in the base branch with one from the branch being merged.

2nd approach requires `appveyor.yml` of the following format:

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

While this approach works great in the most cases there is one inconvenience though - with large configuration and many branches `appveyor.yml` becomes really unmanageable and error-prone as you have to repeat (copy-paste) entire configuration for every branch.

## The solution

We just deployed an update to AppVeyor that allows sharing common configuration between branches in a single `appveyor.yml`!

There is new `for` node with a list of branch-specific configurations **overriding** common configuration defined on the top most level, for example:

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
      - /dev-.*/

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

Best regards,<br>
AppVeyor team

Follow us on Twitter: [@appveyor](https://twitter.com/appveyor)
