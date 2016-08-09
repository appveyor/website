---
layout: docs
title: NuGet packages - Publishing libraries to NuGet galleries
---

# NuGet packages - Publishing you libraries to NuGet galleries

*Dislaimer: This only works against GitHub or GitLab repositories)*

These days, developers are leveraging NuGet as a distribution model for their libraries. Thse libraries are packaged up into a nuget package and are then made available to developers via nuget *feeds* (eg. NuGet.org, MyGet.org or even AppVeyor's two types of private feeds). 

These packages can be either public libraries (such as public OSS repositories on GitHub, etc) or private libraries used in closed source projects (like private company repositories on Github, etc).
Either way, AppVeyor makes it easy to automatically push these packages to any type of NuGet feed.

## (Nearly) Git-flow to the rescue!

Git-Flow is a popular branching model people use when using git. The following scenario is _very similar_ to Git-Flow but is very slightly made simpler:

You have 2 main branches and then all feature/bugfix branches:
- `master`
- `dev`
- `<anything else>` e.g. `feature/new-users`, `bug/user-createdOn-defaults-to-UTC`, `this-is-my-new-fancy-feature`, etc..

Then...
- Create a branch (off `dev`). Do some code. Commit & Push.
- Create a PR'd up into the `dev` branch.
- All code that is *merged* into `dev` creates a new nuget package which is pushed up to a `staging/dev` nuget feed (e.g. MyGet or the AppVeyor nuget feeds). All nugets in this feed are tagged as *pre-release* nugets. The version number here isn't really important. Only the *pre-release* tag, is important.
- When the package has been tested and approved, then the `dev` branch is PR'd up to `master`.
- `master` accepts the PR. **No nuget packages are made, though**.
- Now, you **tag** the `master` branch with a semver tag. (e.g. `1.0.0`, `1.2.0`, etc). This will then create a new nuget package and publishes this your `master/release` nuget feed. (e.g. NuGet.org (for public packages) or another feed like Myget or AppVeyor's nuget feed for private packages). **Note: the version is extreamly important here - as this is what the nuget package will be versioned as**.

So this means:
- `master` branch is what is used for production ready code.
- `dev` branch is the preparation/testing branch which keeps creating nuget packages for testing.
- **Tag** your master branch when you're ready to push up a new nuget package for release.

## Sample `.yml` file

The key points in this .yml file are:
- Setting up the correct build number based on which part of the *flow* you're up to.
- Setting up [assemblyinfo patching](http://www.appveyor.com/docs/build-configuration#assemblyinfo-patching).
- Creating a nuget artifact.
- Setting up [conditional nuget deployment](http://www.appveyor.com/docs/deployment#conditional-deployment) including a [tag based conditional deployment](http://www.appveyor.com/docs/deployment#deploy-on-tag-github-and-gitlab-only).

```
version: '{build}.0.0-dev'
configuration: Release
os: Visual Studio 2015
pull_requests:
  do_not_increment_build_number: true

# Override the 'version' if this is a GH-tag-commit.
init:
  - ps: if ( ($env:APPVEYOR_REPO_BRANCH -eq 'master') -and ($env:APPVEYOR_REPO_TAG -eq $TRUE) ) { Update-AppveyorBuild -Version "$env:APPVEYOR_REPO_TAG_NAME" }
  - ps: iex ((new-object net.webclient).DownloadString('https://gist.githubusercontent.com/PureKrome/0f79e25693d574807939/raw/4a00b443bd9ad908768593244fc08061ec8e1ccc/appveyor-build-info.ps'))
  
before_build:
  - nuget restore

assembly_info:
  patch: true
  file: AssemblyInfo.*
  assembly_version: $(appveyor_build_version)
  assembly_file_version: $(appveyor_build_version)
  assembly_informational_version: $(appveyor_build_version)

build:
  parallel: true
  verbosity: minimal
  publish_nuget: true
  publish_nuget_symbols: false
  include_nuget_references: false
  
deploy:
  - provider: NuGet
    server: <YOUR *STAGING/DEV* NUGET FEED, e.g. https://www.myget.org/F/<someFeed>/api/v2/package>
    api_key:
      secure: <SNIP - PLACE YOUR ENCRYPTED SECRET HERE>
    skip_symbols: true
    artifact: /.*\.nupkg/
    on:
      branch: dev
  - provider: NuGet
    server: <YOUR *MASTER/RELEASE* NUGET FEED, e.g. https://www.myget.org/F/<someFeed>/api/v2/package>
    api_key:
      secure: <SNIP - PLACE YOUR ENCRYPTED SECRET HERE>
    skip_symbols: true
    artifact: /.*\.nupkg/
    on:
      branch: master
      appveyor_repo_tag: true

cache:
  - packages -> **\packages.config
```
