---
layout: docs
title: Publishing artifacts to GitHub Releases
---

<!-- markdownlint-disable MD022 MD032 -->
# Publishing artifacts to GitHub Releases
{:.no_toc}

The `GitHub` deployment provider uploads build artifacts to an existing GitHub release
or creates a new release if one does not already exist. You can publish artifacts during
the build or use staged deployment by configuring new environment of `GitHub` type at
<https://ci.appveyor.com/environments>.

Note that the provider name `GitHub` is case sensitive (e.g. not `Github`).

Table of contents:

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->


## Usage scenarios

### Release every tag build

In this scenario, the GitHub deployment step is configured to run as part of the build process.

1. Add new tag in local repo.
2. Push tag to GitHub repo and start a new AppVeyor build.
3. AppVeyor creates a new release based on that tag and uploads artifacts.

Alternatively, you may tell AppVeyor to create a "draft" release so you can perform any final checks before making it public.


### Promoting selected tag to GitHub release

In this scenario you configure a new "Environment" of GitHub type at <https://ci.appveyor.com/environments>, then:

1. Add new tag in local repo.
2. Push tag to GitHub repo and start a new AppVeyor build.
3. Build produces artifacts.

To promote selected "tag" build to GitHub release:

1. Go to project "Deployments" tab and deploy to GitHub environment.
2. AppVeyor creates a new release and pushes selected build artifacts into it.


## Provider settings

* **Tag name** (`tag`) - Optional. If not specified build tag or version is used. You can use environment variables in tag name, for example `myproduct-v$(appveyor_build_version)`.
* **Release name** (`release`) - Optional. The name of release. If not specified tag name is used as release name. You can use environment variables in release name, for example `product release of v$(appveyor_build_version)`.
* **Release description** (`description`) - [mandatory release description](http://help.appveyor.com/discussions/problems/2975-github-deployment). If not specified, GitHub returns `422: Unprocessable entity` error.
* **GitHub authentication token** (`auth_token`) - OAuth token used for authentication against GitHub API. You can generate [Personal API access token](https://github.com/blog/1509-personal-api-tokens) at [https://github.com/settings/tokens](https://github.com/settings/tokens). Minimal token scope is `repo` or `public_repo` to release on private or public repositories respectively. Be sure to encrypt your token using the [**Account &rarr; Encrypt data**](https://ci.appveyor.com/tools/encrypt) tool.
* **Repository name** (`repository`) - Optional. Allows to deploy into repository other than project's one. Note that if this repository is under another owner, **GitHub authentication token** should be generated under that owner too. Use `owner/repo` format.
* **Artifact to deploy** (`artifact`) - Optional. Allows specifying one or more build artifacts to be uploaded as release assets. The value could be comma-delimited list of artifact's file name, deployment name or regular expression matching one of these. For example `bin\release\MyLib.zip` or `/.*\.nupkg/`. Don't forget to [package your artifact](/docs/packaging-artifacts/) first, as the deployment will fail if this value does not match `artifacts.name` or `artifacts.path` (even if the file exists.)
* **Draft release** (`draft`) - `true` if draft release should be created; default is `false`.
* **Pre-release** (`prerelease`) - `true` to mark release as "pre-release"; default is `false`.
* **Force update** (`force_update`) - `true` to overwrite files in an existing release; default is `false` which will fail deployment if the release already exists on GitHub.

### Configuring in appveyor.yml

```yaml
deploy:
  release: myproduct-v$(appveyor_build_version)
  description: 'Release description'
  provider: GitHub
  auth_token:
    secure: <your encrypted token> # your encrypted token from GitHub
  artifact: /.*\.nupkg/            # upload all NuGet packages to release assets
  draft: false
  prerelease: false
  on:
    branch: master                 # release from master branch only
    appveyor_repo_tag: true        # deploy on tag push only
```
