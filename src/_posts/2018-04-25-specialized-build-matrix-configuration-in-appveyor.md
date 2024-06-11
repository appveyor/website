---
title: Advanced build matrix configuration in AppVeyor
---

## AppVeyor config evolution

AppVeyor configuration has grown incrementally more sophisiticated in order to accommodate not only a wide range of build requirements,
but also AppVeyor's feature set, and most pertinently to this post, the addition of linux build machines. This welcome addition to the AppVeyor platform means that users
may want to build a cross-platform repository but may not want to manage separate configuration files. This is no longer a problem if we make use of
the `for:` node in our appveyor configuration. This key was added previously to allow users to share common configuration between all branches while overriding
parts of that configuration in specific branches. But along with the added features comes increased possibility of confusion regarding expected results of configuration.

The aim of this post is to give some more detail about advanced configuration options in which the potential for confusion is highest.

First, lets consider the following example:

```yaml

# Configuration shared by all build jobs
platform:
  - x86
  - x64
configuration:
  - Debug
  - Release
environment:
  my_var1: value1
  my_var2: value2
test_script:
  - ps: Write-Host "common test script"
for:
  -
    branches:
      only:
        - master
    configuration: Release
    environment:
      my_var1: not-value1
      my_var3: new-value

  -
    branches:
      only:
        - /dev-.*/ #regular expressions can also be used to match several branches
      platform: Any CPU
      test_script:
        - echo "overriding common test script"

```

In the above configuration we see familiar keys being set that will be shared by all jobs calculated by the matrix. In the `for:` section we specify
some overrides to those keys, and also some additions. If you're not already familiar with the rules for configuration merging they can be found [in the docs](https://www.appveyor.com/docs/branches/#sharing-common-configuration-between-branches).

One potential source of confusion is understanding the number of jobs that will be calculated given the build matrix. In the example above,
commits to unspecified branches (i.e. _not_ 'master' branches and _not_ any branch matching the `/dev-.*/` regexp) result in a build matrix calculated to have 4 jobs (2-configuration \* 2-platform)
Meanwhile, commits to master branch result in 2 jobs since the configuration key is overridden and set to a scalar value instead of a list (1-configuration \* 2-platform). Ditto for commits to any branch matching
the `/dev-*/` regexp, except its the platform being overridden this time (2-configuration \* 1-platform).

But the `for:` node functionality also allows the user to manipulate the build job matrix in order to specify platform specific configuration. Which is precisely where
it serves cross-platform repos best.

For example:

```yaml

version: 1.0.{build}

image:
  - Visual Studio 2017
  - Ubuntu
configuration:
  - Debug
  - Release
environment:
  my_var1: value1
  my_var2: value2
  matrix:
    - my_var3: value3
    - my_var4: value4
test_script:
  - ps: Write-Host "common test script"
matrix:
  - fast_finish: true
for:
  -
    matrix:
      only:
        - configuration: Release
          my_var3: value3
    environment:
      my_var1: overriden-value1
    platform: Any CPU # this setting is ignored
    test_script:
      - ps: write-host "for-matrix override test script 1"

  -
    matrix:
      only:
        - image: Ubuntu
          my_var4: value4
      pull_requests:
        do_not_increment_build_number: true # this setting is ignored
      allow_failures:
        - platform: x64 # this setting is ignored
    environment:
      my_var2: overridden-value2
    test_script:
      - sh: echo for-matrix override build script

```

Here we are able to create the desired matrix which consists of 8 jobs (2-image / 2-configuration / 2-environment variable groups), then in the `for:` node, specify
special conditions for each job.
Another important thing to note is the three matrix configuration keys within the `for:` node that are ignored. This makes sense intuitively, since these are meant to be, in a
sense, 'global' configuration of the job matrix behaviour. A list of settings that are ignored can be found [here](https://www.appveyor.com/docs/build-configuration/#specializing-matrix-job-configuration)

To keep your configuration code as slim as possible, its a good idea to utilize a 'fall through' as a default config.
Consider this simple example:

```yaml

version: 1.0.{build}

configuration:
  - Debug
  - Test

for:
  -
    matrix:
      only:
        - configuration: Debug
    build_script:
      - ps: "Debug build script"

  -
    matrix:
      only:
        - configuration: Test
    build_script:
      - echo "Test build script"

```

This configuration can be simplified to the following, allowing the 'Debug' configuration to be the default:

```yaml

version: 1.0.{build}

configuration:
  - Debug
  - Test
build_script:
  - ps: "Debug build script"
for:
  -
    matrix:
      only:
        - configuration: Test
    build_script:
      - echo "Test build script"

```

AppVeyor's new configuration capabilities give the user fine grained control allowing for virtually any imaginable configuration. But use these capabilities
wisely and take a step back to make sure your build matrix is well formed and sensible.
