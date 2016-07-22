---
layout: docs
title: Setting up commits filtering
---

# Setting up commits filtering
{:.no_toc}

* Comment to trigger ToC generation
{:toc}

## Introduction

Every push to a repository triggers new AppVeyor build. If you are committing minor changes, say update to a documentation or static web page, you might want to skip a build.
This article explains how to make AppVeyor starting new builds based on the contents of commit message, commit author or files affected by commit.

> Currently, commits filtering functionality is available in `appveyor.yml` configuration only.

## Commit message

### Skip commits

`skip_commits.message` setting allows skipping commits with a message matching regular expression:

For example, the following configuration won't start new builds on commits with `[chore]` in their message:

{% highlight yaml %}
skip_commits:
  message: /\[chore\]/
{% endhighlight %}

**Skip directive in commit message**

AppVeyor supports "standard" directives to skip a build of particular commit.

Add `[skip ci]` or `[ci skip]` anywhere to commit message and build won't be triggered by AppVeyor for that commit.
Or explicitly skip AppVeyor only with `[skip appveyor]` and still allow any other CI to build the commit (eg. Travis CI).

NOTE: The `[` and `]` brackets in `[skip ci]`, etc are required! Not just the text inside the brackets.

### Include commits

`only_commits.message` setting allows triggering a new build *only* if commit message matches regular expression.

For example, to start a new build only if commit message contains string `build` use this configuration:

{% highlight yaml %}
only_commits:
  message: /build/
{% endhighlight %}


## Commit author

### Skip commits

You can setup a filter to skip builds coming from specific author(s).
`author` value can be author's username, name, email or regular expression matching one of these.

For example, to skip all commits from author with name `John Smith` add this:

{% highlight yaml %}
skip_commits:
  author: John Smith
{% endhighlight %}

You can use regular expression to skip commits from several persons:

{% highlight yaml %}
skip_commits:
  author: /John|Jack/
{% endhighlight %}

### Include commits

`only_commits.author` setting allows triggering a new build *only* if commit message comes from specific author(s), for example:

{% highlight yaml %}
only_commits:
  author: /Alice|Mark/
{% endhighlight %}


## Commit files (GitHub only)

### Skip commits

`skip_commits.files` allows skipping AppVeyor build if **all of the files** modified in push's **head commit** match *any* of the file matching rules (`AND` logic applied to multiple values):

{% highlight yaml %}
skip_commits:
  files:
  - dir/*
  - dir/*.md
  - full/path.txt
  - another/dir/here/
  - '**/*.html'
{% endhighlight %}

For example, if `appveyor.yml` contains the following rules:

{% highlight yaml %}
skip_commits:
  files:
  - docs/*
  - '**/*.html'
{% endhighlight %}

and push commit modified two files: `docs/index.md` and `project-A/mysolution.sln` the build will be started as there is no rule matching `project-A/mysolution.sln`.

For the same set of rules commit modifying `docs/index.md` and `site/views/index.html` files won't start a build as both files match their respective rules.

### Include commits

`only_commits.files` allows starting a new AppVeyor build if only **some of the files** modified in push's **head commit** match their respective rules (`OR` logic applied to multiple values):

For example `appveyor.yml` contains these rules:

{% highlight yaml %}
only_commits:
  files:
  - Project-A/
  - Project-B/
{% endhighlight %}

which means the build will be started only if one of the modified files was inside either `Project-A` or `Project-B` folder.


### File matching rules

- `dir/*` - all files within directory, non-recursive
- `dir/**/*` - all files within directory, recursive
- `dir/` - all files within directory, recursive
- `test/readme` - specific file
- `full/path.txt` - specific file
- `'**/*.html'` - all `.html` files within repository, recursive

>
  * both `\` and `/` slashes are allowed.
  * surround value with single quotes if starts from `*`, e.g. `'*.txt'`

The following example triggers new build for changes in `src\ProjectA` folder only:

{% highlight yaml %}
only_commits:
  files:
  - src\ProjectA\
{% endhighlight %}