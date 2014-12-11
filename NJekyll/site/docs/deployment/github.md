---
layout: docs
title: Publishing artifacts to GitHub Releases
---

# Publishing artifacts to GitHub Releases

`GitHub` deployment provider uploads build artifacts to the existing GitHub release or creates a new release if not exists. You can publish artifacts during the build or use staged deployment by configuring new environment of `GitHub` type.

Table of contents:

* [Usage scenarios](#usage-scenarios)
* [Provider settings](#provider-settings)

<a id="usage-scenarios"></a>
## Usage scenarios

### Release every tag build

In this scenario GitHub deployment step is configured to run as part of the build process.

1. Add new tag in local repo.
2. Push tag to GitHub repo and start a new AppVeyor build.
3. AppVeyor creates a new release based on that tag and uploads artifacts.

Alternatively, you may tell AppVeyor to create "draft" release to do final check before making it public.


### Promoting selected tag to GitHub release

In this scenario you configure a new "Environment" of GitHub type.

1. Add new tag in local repo. 
2. Push tag to GitHub repo and start a new AppVeyor build.
3. Build produces artifacts.

To promote selected "tag" build to GitHub release:

1. Go to project "Deployments" tab and deploy to GitHub environment.
2. AppVeyor creates a new release and pushes selected build artifacts into it.  



<a id="provider-settings"></a>
## Provider settings

* **GitHub authentication token** (`auth_token`) - OAuth token used for authentication against GitHub API. You can generate [Personal API access token](https://github.com/blog/1509-personal-api-tokens) at [https://github.com/settings/applications](https://github.com/settings/applications). Minimal token scope is `repo` or `public_repo` to release on private or public repositories respectively. 

* **Artifact to deploy** (`artifact`) - Optional. Allows specifying one or more build artifacts to be uploaded as release assets. The value could be comma-delimited list of artifact's file name, deployment name or regular expression matching one of these. For example `bin\release\MyLib.zip` or `/.*\.nupkg/`.

* **Draft release** (`draft`) - `true` if draft release should be created; default is `false`.

* **Pre-release** (`prerelease`) - `true` to mark release as "pre-release"; default is `false`.

### Configuring in appveyor.yml

	deploy:
	  provider: GitHub
	  artifact: /.*\.nupkg/           # upload all NuGet packages to release assets
      draft: false
      prerelease: false
      on:
        branch: master                # release from master branch only
        appveyor_repo_tag: true       # deploy on tag push only

