---
title: 'How to skip build matrix jobs (conditionally)'
---

Most of our customers are already familiar with [build matrix](/docs/build-configuration/#build-matrix) and [commits filtering](/docs/how-to/filtering-commits/). Both concepts are common in the Continuous Integration world. The build matrix is used to run multiple scenarios as part of the same build (and against the same commit). Commits filtering allows a user to define conditions (for example commit message or file changed) under which the build should be skipped.

Now they come together (if needed). Also [white/black-listing of branches](/docs/branches#white--and-blacklisting) and filtering of tags with `skip_tags: true` or `skip_non_tags: true` can be added to the mix.

Please check some examples (all scenarios are taken from real customer's feature requests). Unrelated build configuration sections are omitted in favor of simplicity.

**Scenario**: normally, a build should run on `Visual Studio 2017` and `Ubuntu`. However, when files changed only in `docs` folder and its subfolders, only `Visual Studio 2017` build should be executed.

**YAML**:

```yaml
image:
- Visual Studio 2017
- Ubuntu

for:
-
  matrix:
    except:
      - image: Visual Studio 2017
  skip_commits:
    files:
      - docs/**/*
```

**Scenario**: when a commit is tagged, only `ReleaseAzure` configuration should be built. Otherwise AppVeyor should start build for all 4 configurations.

**YAML**:

```yaml
configuration:
  - ReleaseAzure
  - DebugAzure
  - ReleaseOnPrem
  - DebugOnPrem

for:
-
  matrix:
    except:
      - configuration: ReleaseAzure

  skip_tags: true
```

**Scenario**: the same configuration set as in the previous example, but here `ReleaseAzure` is skipped for all branches except `master`. All other configurations never skipped.

**YAML**:

```yaml
configuration:
  - ReleaseAzure
  - DebugAzure
  - ReleaseOnPrem
  - DebugOnPrem

for:
-
  matrix:
    only:
      - configuration: ReleaseAzure

  branches:
    only:
    - master
```

Another good thing is that with commits filtering you are not blind. You do not need to open YAML and re-think why some build or build job has been skipped. Just open `EVENTS` tab on your project page (`https://ci.appveyor.com/project/{accountName}/{projectSlug}/events`) and check respective warning!

Note that this feature is YAML only (not exposed in UI) now.

Best regards,<br>
AppVeyor team
