---
title: 'How to skip build matrix jobs (conditionally)'
---

Most of our customers are already familiar with [build matrix](/docs/build-configuration/#build-matrix) and [commits filtering](/docs/how-to/filtering-commits/). Both concepts are common in Continuous Integration world. Build matrix is being used to run multiple scenarios as part of the same build (and against the same commit). Commits filtering allows to define condition (for example commit message or file changed) when build should be skipped.

Now they come together (if needed). Also [branches white- and blacklisting](/docs/branches#white--and-blacklisting) and tags filtering with `skip_tags: true` or `skip_non_tags: true` can be added to the mix.

Please check some examples (all scenarios are real customers feature requests). Unrelated build configuration parts are skipped for simplicity.

**Scenario**: normally build should run on `Visual Studio 2017` and `Ubuntu`. However, when files changed only in `docs` folder and its subfolders, only `Visual Studio 2017` build should be executed.

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

**Scenario**: normally build runs 4 test categories in [parallel](/docs/parallel-testing/) (which is also the case of build matrix). Developers need the option to run only `Smoke` tests for minor changes. They can add `[only smoke]` to commit message for that.

**YAML**:

```yaml
test:
  categories:
  - - Smoke
  - - DNS
  - - SSL
  - - Storage

for:
-
  matrix:
    except:
      - Tests: Smoke
  skip_commits:
    message: /[only smoke]/

```

**Scenario**: when commit tagged, only `ReleaseAzure` configuration should be built. Otherwise AppVeyor should start build for all 4 configurations.

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

**Scenario**: the same configuration set as in previous example. `ReleaseAzure` is skipped for all branches except `master`. All other configurations never skipped.

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
