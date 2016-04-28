---
layout: docs
title: Skip a build
---

# How to skip a build

<!--TOC-->



## Skip directive in commit message

Every push to a repository triggers new AppVeyor build. If you are committing minor changes, say update to a documentation or static web page, you might want to skip a build.

Add `[skip ci]` or `[ci skip]` anywhere to commit message and build won't be triggered by AppVeyor for that commit.
Or explicitly skip AppVeyor with `[skip appveyor]` and still allow any other CI to build the commit (eg. Travis CI). 

NOTE: The `[` and `]` brackets in `[skip ci]`, etc are required! Not just the text inside the brackets.


## Commits filter

You can setup a filter to skip builds for commits with specific messages or coming from specific author(s). Currently, the functionality is available through `appveyor.yml`:

    skip_commits:

      # Regex for matching commit message
      message: /Created.*\.(png|jpg|jpeg|bmp|gif)/

      # Commit author's username, name, email or regexp maching one of these.
      author: John

## Triggering builds on specific word in commit message

You can use commit message filter with "inverse matching" regex to trigger builds only if commit messages contain specific word/phrase.

For example, the following filter is telling AppVeyor to trigger builds only for commits having `build` in their commit messages:

    skip_commits:
      message: /^((?!build).)*$/

