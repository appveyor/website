---
layout: docs
title: Skip a build
---

# How to skip a build

* [Skip directive in commit message](#skip-directive)
* [Commits filter](#commits-filter)


<a id="skip-directive"></a>
## Skip directive in commit message

Every push to a repository triggers new AppVeyor build. If you are committing minor changes, say update to a documentation or static web page, you might want to skip a build.

Add `[skip ci]` or `[ci skip]` anywhere to commit message and build won't be triggered by AppVeyor for that commit.


<a id="commits-filter"></a>
## Commits filter

You can setup a filter to skip builds for commits with specific messages or coming from specific author(s). Currently, the functionality is available through `appveyor.yml`:

    skip_commits:

      # Regex for matching commit message
      message: /Created.*\.(png|jpg|jpeg|bmp|gif)/

      # Commit author's username, name, email or regexp maching one of these.
      author: John