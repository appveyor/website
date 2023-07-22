---
layout: docs
title: Setting up commits filtering
---

<!-- markdownlint-disable MD022 MD032 -->
# Setting up commits filtering
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Introduction

Every push to a repository triggers new AppVeyor build. If you are committing minor changes, say update to a documentation or static web page, you might want to skip a build.
This article explains how to make AppVeyor starting new builds based on the contents of commit message, commit author or files affected by commit.

Currently, commits filtering functionality is available in `appveyor.yml` configuration only.

Commits filtering does not apply to builds started with UI, API or scheduled build.

## Commit message

### Skip commits

`skip_commits.message` setting allows skipping commits with a message title matching regular expression.

For example, the following configuration won't start new builds on commits with `[chore]` in their message:

```yaml
skip_commits:
  message: /\[chore\]/
```

Regular expressions should be surrounded by /, otherwise Appveyor will do simple case insensitive string comparison.

NOTE: AppVeyor searches for skip commit string in the commit message title (text before first empty line) only. This is done to prevent confusion when squashed commits are not being built in case older commit (in the squashed commits list) has skip commit string.

AppVeyor searches for [skip ci] / [ci skip] / [skip appveyor] in the commit message title only. This avoids confusion when squashing commits, in particular with the GitHub squash-and-merge feature.

### Skip directive in commit message

AppVeyor supports "standard" directives to skip a build of particular commit.

Add `[skip ci]` or `[ci skip]` to commit message title and build won't be triggered by AppVeyor for that commit.
Or explicitly skip AppVeyor only with `[skip appveyor]` and still allow any other CI to build the commit (eg. Travis CI).

NOTE: The `[` and `]` brackets in `[skip ci]`, etc are required! Not just the text inside the brackets.

NOTE: Similarly to custom skip commit message, AppVeyor searches for [skip ci] / [ci skip] / [skip appveyor] in the commit message title only. This avoids confusion when squashing commits, in particular with the GitHub squash-and-merge feature.

### Include commits

`only_commits.message` setting allows triggering a new build *only* if commit message matches regular expression.

For example, to start a new build only if commit message contains string `build` use this configuration:

```yaml
only_commits:
  message: /build/
```


## Commit author

### Skip commits

You can setup a filter to skip builds coming from specific author(s).
`author` value can be author's username, name, email or regular expression matching one of these.

For example, to skip all commits from author with name `John Smith` add this:

```yaml
skip_commits:
  author: John Smith
```

You can use regular expression to skip commits from several persons:

```yaml
skip_commits:
  author: /John|Jack/
```

### Include commits

`only_commits.author` setting allows triggering a new build *only* if commit message comes from specific author(s), for example:

```yaml
only_commits:
  author: /Alice|Mark/
```


## Commit files (GitHub and Bitbucket only)

### Skip commits

`skip_commits.files` allows skipping AppVeyor build if **all of the files** modified in push's **head commit** match **any** of the file matching rules.

For example, if `appveyor.yml` contains the following rules:

```yaml
skip_commits:
  files:
    - docs/*
    - '**/*.html'
```

and the pushed commit modified two files: `docs/index.md` and `project-A/mysolution.sln`, the build will be started. As there is no rule matching `project-A/mysolution.sln`.

For the same set of rules a commit modifying `docs/index.md` and `site/views/index.html` won't start a build. As both files match their respective rules.

### Include commits

`only_commits.files` allows starting a new AppVeyor build if **any file** modified in the push's **head commit** matches **any** of the file matching rules:

For example `appveyor.yml` contains these rules:

```yaml
only_commits:
  files:
    - Project-A/
    - Project-B/
```

Which means the build will be started only if one of the modified files was inside either the `Project-A` or `Project-B` folder.


### File matching rules

* `dir/*` - all files within directory, non-recursive
* `dir/**/*` - all files within directory, recursive
* `dir/` - all files within directory, recursive
* `test/readme` - specific file
* `full/path.txt` - specific file
* `'**/*.html'` - all `.html` files within repository, recursive

Notes:

* both `\` and `/` slashes are allowed as directory separators.
* surround a value with single quotes if it starts with a `*`, e.g. `'*.txt'`

The following example triggers a new build for changes in the `src\ProjectA` folder only:

```yaml
only_commits:
  files:
    - src\ProjectA\
```
