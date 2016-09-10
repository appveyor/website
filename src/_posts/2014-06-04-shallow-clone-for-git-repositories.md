---
title: Shallow clone for Git repositories
---

AppVeyor runs every build on a new VM which is getting decommissioned right after the build finishes.
The state between consequent builds of the same project is not preserved and every time a new build
starts AppVeyor clones entire repository and then checkouts a specific commit. This becomes a challenge
for very large repositories or repositories with long history as it takes a significant time to do a clone.

We introduced a new feature called "**shallow clone**" aiming to improve the situation with large
repositories. It offers two options:

1. Setting depth of `git clone` command
2. Downloading repository as zipball using GitHub API

## Setting depth of `git clone` command

By default AppVeyor clones entire repository with all the history. You can limit the number of last
commits you'd like to clone. This feature works for Git repositories hosted at GitHub, BitBucket and Kiln.
You can set clone depth on General tab of project settings:

![Git clone depth](/assets/images/posts/shallow-clone/git-clone-depth.png)

To set clone depth in `appveyor.yml` add the following in the root of config:

```text
clone_depth: <number>
```

**Note**: Be aware that if you do a lot of commits producing queued builds and depth number is too small,
`git checkout` operation following `git clone` can fail because the requested commit is not present
in a cloned repository.

## Downloading repository using GitHub API

As title says this option is specific to GitHub. It uses GitHub API to download specific commit
of the repository as zip archive and then unpacks it on build worker machine. This feature works
for regular commits, branch commits and pull requests.

You can enable this option through UI on **General** tab of project settings:

![GitHub shallow clone](/assets/images/posts/shallow-clone/github-shallow-clone.png)

To enable it through `appveyor.yml` add the following in the root of config:

```text
shallow_clone: true
```

Enjoy!
