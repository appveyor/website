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

**NOTE:** If the current build is based on a regular commit then a new tag is created (either explicitly or automatically named in `tag` field of deployment settings as described below) along with the release. This means that, unless you have `skip_tags` set to true, the creation of the tag will also send a webhook to Appveyor, and the deployment process will begin again, and so on.

To avoid this possible cycle, you can set `skip_tags` to true. However, you may be using `on:` key to conditionally build and deploy only on tags you create manually. In this case, you can specify `tag: $(APPVEYOR_REPO_TAG_NAME)` in deployment settings without skipping tags, and the manually created tag will be updated instead of a new one being created.

## Provider settings

* **Tag name** (`tag`) - Optional. If not specified build tag or version is used. You can use environment variables in tag name, for example `myproduct-v$(APPVEYOR_BUILD_VERSION)`.
* **Release name** (`release`) - Optional. The name of release. If not specified tag name is used as release name. You can use environment variables in release name, for example `product release of v$(APPVEYOR_BUILD_VERSION)`.
* **Release description** (`description`) - [mandatory release description](https://help.appveyor.com/discussions/problems/2975-github-deployment). If not specified, GitHub returns `422: Unprocessable entity` error.
* **GitHub authentication token** (`auth_token`) - OAuth token used for authentication against GitHub API. You can generate [Personal API access token](https://github.com/blog/1509-personal-api-tokens) at [https://github.com/settings/tokens](https://github.com/settings/tokens). Minimal token scope is `repo` or `public_repo` to release on private or public repositories respectively.  The permissions `repo:status`, `repo_deployment` are necessary for successful deployment of artifacts to GitHub.  Be sure to encrypt your token using "Encrypt configuration data" page in AppVeyor (**Account** &rarr; **Encrypt YAML**).
* **Repository name** (`repository`) - Optional. Allows to deploy into repository other than project's one. Note that if this repository is under another owner, **GitHub authentication token** should be generated under that owner too. Use `owner/repo` format.
* **Artifact to deploy** (`artifact`) - Optional. Allows specifying one or more build artifacts to be uploaded as release assets. The value could be comma-delimited list of artifact's file name, deployment name or regular expression matching one of these. For example `bin\release\MyLib.zip` or `/.*\.nupkg/`. Don't forget to [package your artifact](/docs/packaging-artifacts/) first, as the deployment will fail if this value does not match `artifacts.name` or `artifacts.path` (even if the file exists.)
* **Draft release** (`draft`) - `true` if draft release should be created; default is `false`.
* **Pre-release** (`prerelease`) - `true` to mark release as "pre-release"; default is `false`.
* **Force update** (`force_update`) - `true` to overwrite files in an existing release; default is `false` which will fail deployment if the release already exists on GitHub.

### Configuring in appveyor.yml

```yaml
deploy:
  release: myproduct-v$(APPVEYOR_BUILD_VERSION)
  description: 'Release description'
  provider: GitHub
  auth_token:
    secure: <your encrypted token> # your encrypted token from GitHub
  artifact: /.*\.nupkg/            # upload all NuGet packages to release assets
  draft: false
  prerelease: false
  on:
    branch: master                 # release from master branch only
    APPVEYOR_REPO_TAG: true        # deploy on tag push only
```
