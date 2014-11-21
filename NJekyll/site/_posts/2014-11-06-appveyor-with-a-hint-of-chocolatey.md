---
layout: post
title: AppVeyor with a hint of Chocolatey
---

<img align="right" src="/site/_posts/images/chocolatey/chocolatey-logo.png" alt="Chocolatey-Logo" width="279" height="53" /><a href="http://chocolatey.org/">Chocolatey</a> is a wonderful tool that allows you installing your favourite programs with a single command. Unlike regular process of installing software with interactive setup package where you keep clicking "Next", "Accept", "Finish", etc. Chocolatey does the job without questions asked. By analogy from Linux world Chocolatey is a package manager for Windows.

Chocolatey is great when you setup your development environment and it's especially great for installing custom software during the build process on AppVeyor! As you know AppVeyor offers fully-customizable build environment where you have admin rights on build machines. For example, if you need to install MongoDB for your integration tests you can do that with the following command (the latest version of Chocolatey is already installed on AppVeyor build workers):
<pre>choco install mongodb</pre>
The list of software that can be installing with Chocolatey is huge and it's growing fast. Catalog is community-driven and you can contribute your own packages or update existing ones. Behind the scene Chocolatey uses feed of NuGet packages. Each package contains install.ps1 and uninstall.ps1 PowerShell scripts. If it's so-called "portable" package application files are stored along with scripts or if it's "native" package application MSI is downloaded from the Internet and silently installed.

So, Chocolatey is well-established thing, but Chocolatey team want moving it to the next level and making Chocolatey an alternative Windows Store! They created <a href="https://www.kickstarter.com/projects/ferventcoder/chocolatey-the-alternative-windows-store-like-yum">KickStarter campaign and are asking for our support</a>! Let's help Chocolatey to do an open, community-driven Application Store for Windows.

Let's get Chocolatey!