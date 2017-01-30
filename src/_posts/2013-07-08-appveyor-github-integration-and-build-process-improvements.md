---
title: AppVeyor GitHub integration and build process improvements
---

Hello,

First of all, I'd like to thank all those people participating in AppVeyor beta and providing
valuable feedback! You help moving the project further!

Today we released a new AppVeyor update which includes:

* Login with GitHub
* Selecting projects scope when authorizing with GitHub
* MSBuild log saving and displaying
* Project integration flow stability improvements

All these changes are immediately available!

## Login with GitHub

It is now possible to sign up and login using GitHub account:

![AppVeyor login with GitHub](/assets/img/posts/github-integration/appveyor-login-with-github1.png)

AppVeyor uses OAuth authentication, so your GitHub account credentials are not stored
in AppVeyor database.

## Selecting projects scope when authorizing with GitHub

When adding a new project from GitHub you allow AppVeyor to access your GitHub repositories.
It is now possible to select authorization scope: only public projects or public and private
projects. This feature is a must for developers who are members of some organizations and not
allowed to give outside access to their private repositories.

![Connect GitHub/BitBucket](/assets/img/posts/github-integration/tour-connect-github-bitbucket.png)

## MSBuild log saving and displaying

Detailed MSBuild log is now saved for every build and can be downloaded from project version screen:

![appveyor-download-build-log](/assets/img/posts/github-integration/appveyor-download-build-log1.png)

## Project integration flow stability improvements

We performed a very serious back-end stabilization work and re-factored communication layer between
AppVeyor application and build servers. Now communication pipeline is entirely built on Azure Service
Bus to be reliable for critical business applications. No more hanging builds!

If you have any questions or suggestions please [drop us an email](mailto:team@appveyor.com),
[start a new discussion](http://help.appveyor.com/discussions) on our forums or
[submit an idea on our UserVoice](https://appveyor.uservoice.com/).
