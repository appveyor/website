---
layout: post
title: Shallow clone for Git repositories
---

AppVeyor runs every build on a new VM which is getting decommissioned right after the build finishes. The state between consequent builds of the same project is not preserved and every time a new build starts AppVeyor clones entire repository and then checkouts a specific commit. This becomes a challenge for very large repositories or repositories with long history as it takes a significant time to do a clone.

We introduced a new feature called "<strong>shallow clone</strong>" aiming to improve the situation with large repositories. It offers two options:
<ol>
    <li>Setting depth of git clone command.</li>
    <li>Downloading repository as zipball using GitHub API</li>
</ol>
<h2>Setting depth of git clone command</h2>
By default AppVeyor clones entire repository with all the history. You can limit the number of last commits you'd like to clone. This feature works for Git repositories hosted at GitHub, BitBucket and Kiln. You can set clone depth on General tab of project settings:

<img src="/site/_posts/images/shallow-clone/git-clone-depth.png" alt="git-clone-depth">

To set clone depth in appveyor.yml add the following in the root of config:
<pre>clone_depth: &lt;number&gt;</pre>
<strong>Be aware that if you do a lot of commits producing queued builds and depth number is too small git checkout operation following git clone can fail because requested commit is not present in a cloned repository.</strong>
<h2>Downloading repository using GitHub API</h2>
As title says this option is specific to GitHub. It uses GitHub API to download specific commit of the repository as zip archive and then unpacks it on build worker machine. This feature works for regular commits, branch commits and pull requests.

You can enable this option through UI on <strong>General</strong> tab of project settings:

<img src="/site/_posts/images/shallow-clone/github-shallow-clone.png" alt="github-shallow-clone">

To enable it through appveyor.yml add the following in the root of config:
<pre>shallow_clone: true</pre>
Enjoy!