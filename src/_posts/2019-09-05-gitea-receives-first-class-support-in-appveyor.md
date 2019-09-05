---
title: Gitea receives first-class support in AppVeyor CI
---

[Gitea](https://gitea.io) is a fantastic self-hosted Git service! It's free, open-source, very lightweight (a single file to download and run on any platform), feature-rich and has an extensive GitHub-like API. [AppVeyor Server](https://www.appveyor.com/on-premise/) is a self-hosted CI/CD service which is also free, multi-platform and lightweight (absolutely no dependencies) - it's a perfect CI/CD companion for your Gitea installation!

Until today, developers have been adding Gitea repositories in AppVeyor as "generic" Git projects thus missing essential CI/CD conveniences such as automatic builds triggering, private repositories, `appveyor.yml` support, commit statuses and pull request builds.

Today we are thrilled to announce Gitea to extend AppVeyor family of built-in source control providers!

## Gitea integration highlights

* Pick Gitea repository for a new project.
* Repository webhook is configured for you to automatically trigger builds on code pushes and pull requests.

<p class="text-center">
  <img src="/assets/img/posts/gitea/select-gitea-repository.png" alt="Choose Gitea repository for a new project" width="665" height="319">
</p>

* Configure your builds with `appveyor.yml` in the root of the repo. You can still configure on web UI too.
* Commit statuses for branch and PR builds with links to corresponding AppVeyor builds.

<p class="text-center">
  <img src="/assets/img/posts/gitea/yaml-and-commit-status.png" alt="YAML configuration and commit statuses" width="413" height="235">
</p>

* GitHub-like testing of pull requests by automatic merging of head into base on the clone.

<p class="text-center">
  <img src="/assets/img/posts/gitea/testing-pr.png" alt="Testing Gitea PR in AppVeyor" width="558" height="233">
</p>

Lightweight and easy-to-configure AppVeyor is a perfect CI/CD match for your Gitea installation.

[Install AppVeyor](/on-premise/#download) on your favorite platform, connect to Gitea and enjoy your builds!

Best regards,<br>
AppVeyor team