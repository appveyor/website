---
layout: docs
title: Getting started
slug: docs
---

# Getting started

<div class="flex-video widescreen">
    <iframe width="640" height="360" src="//www.youtube.com/embed/e1rVM4_nzWw?rel=0&amp;color=white" allowfullscreen></iframe>
</div>

## Step 1 - Sign in with AppVeyor

Use **GitHub** or **Bitbucket** button to sign up with your existing developer account (OAuth) or create an AppVeyor account using your email and password.



## Step 2 - Add your project

Authorize GitHub or BitBucket to list your repositories. For open-source project developers who are using the same GitHub account for both  personal and private company repositories AppVeyor offers a choice between two scopes: **public repositories** exclusively or **public and private**.

For every project AppVeyor will configure webhooks for its repository to automatically start a build when you push the changes. For every private project AppVeyor will add an SSH public key (deployment key) to the clone repository on the build machine.



## Step 3 - Start new build

To kick-off a new build you can either push any changes to your repository or click **New build** on the project details screen.

AppVeyor will provision a new build virtual machine, clone the repository and pass the project through build, test and deploy phases (see [Build pipeline](/docs/build-configuration#build-pipeline)).




## Step 4 - Configure your project

Start from [Build configuration](/docs/build-configuration) to learn how to configure build.
