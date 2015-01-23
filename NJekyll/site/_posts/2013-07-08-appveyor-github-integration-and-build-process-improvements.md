---
layout: post
title: AppVeyor GitHub integration and build process improvements
---

Hello,

First of all, I'd like to thank all those people participating in AppVeyor beta and providing valuable feedback! You help moving the project further!

Today we released a new AppVeyor update which includes:
<ul>
    <li>Login with GitHub</li>
    <li>Selecting projects scope when authorizing with GitHub</li>
    <li>MSBuild log saving and displaying</li>
    <li>Project integration flow stability improvements</li>
</ul>
All these changes are immediately available!
<h2>Login with GitHub</h2>
It is now possible to sign up and login using GitHub account:

<img alt="appveyor-login-with-github" src="/site/images/posts/github-integration/appveyor-login-with-github1.png" width="494" height="177">

AppVeyor uses OAuth authentication, so your GitHub account credentials are not stored in AppVeyor database.
<h2>Selecting projects scope when authorizing with GitHub</h2>
When adding a new project from GitHub you allow AppVeyor to access your GitHub repositories. It is now possible to select authorization scope: only public projects or public and private projects. This feature is a must for developers who are members of some organizations and not allowed to give outside access to their private repositories.

<img alt="tour-connect-github-bitbucket" src="/site/images/posts/github-integration/tour-connect-github-bitbucket.png" width="282" height="202">
<h2>MSBuild log saving and displaying</h2>
Detailed MSBuild log is now saved for every build and can be downloaded from project version screen:

<img alt="appveyor-download-build-log" src="/site/images/posts/github-integration/appveyor-download-build-log1.png" width="506" height="204">
<h2>Project integration flow stability improvements</h2>
We performed a very serious back-end stabilization work and re-factored communication layer between AppVeyor application and build servers. Now communication pipeline is entirely built on Azure Service Bus to be reliable for critical business applications. No more hanging builds!

If you have any questions or suggestions please <a href="mailto:team@appveyor.com">drop us email</a>, <a href="http://help.appveyor.com/discussions">start a new discussion</a> on our forums or <a href="http://appveyor.uservoice.com/">submit an idea on our uservoice</a>.