---
layout: docs
title: Shallow clone of repositories
---

# Shallow clone of repositories

* [Setting depth of `git clone` command](#git-depth)
* [Downloading repository via GitHub or Bitbucket API](#download-via-api)

AppVeyor runs every build on a new VM which is getting decommissioned right after the build finishes. The state between consequent builds of the same project is not preserved and every time a new build starts AppVeyor clones entire repository and then checkouts a specific commit. This becomes a challenge for very large repositories or repositories with long history as it takes a significant time to do a clone.

The feature called "shallow clone" aims to improve the situation with large repositories. It offers two options:

<!--TOC-->



## Setting depth of git clone command

By default AppVeyor clones entire repository with all the history. You can limit the number of last commits you’d like to clone. This feature works for Git repositories hosted at GitHub and Bitbucket. You can set clone depth on General tab of project settings or in `appveyor.yml`:

    clone_depth: <number>

> Be aware that if you do a lot of commits producing queued builds and depth number is too small git checkout operation following git clone can fail because requested commit is not present in a cloned repository.


## Downloading repository via GitHub or Bitbucket API

As title says this option is specific to GitHub and Bitbucket only. It uses their respective REST API to download specific commit of the repository as zip archive and then unpacks it on build worker machine. This feature works for regular commits, branch commits and pull requests.

You can enable this option through UI on General tab of project settings or in `appveyor.yml`:

    shallow_clone: true

If both `clone_depth` and `shallow_clone` exist in the config `clone_depth` is ignored.
