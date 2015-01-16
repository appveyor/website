---
layout: docs
title: Building private GitHub repositories with sub-modules
---

# Building private GitHub repositories with sub-modules

> The following article was written specifically for GitHub, but some of these techniques could be applied to other Git hosting platforms as well.

## How AppVeyor is cloning private repos

AppVeyor uses SSH to clone private Git repositories. When you add a project in AppVeyor a new RSA key-pair is generated which consists of private and public keys. Public key is deployed to a remote Git repository using GitHub (or BitBucket) API and private key is pushed to build worker during the build. For SSH protocol to work on Windows private key should be located in `%USERPROFILE%\.ssh\id_rsa` file.

## The problem with private sub-modules

Git has [submodules](http://www.git-scm.com/book/en/v2/Git-Tools-Submodules) support and this is a wonderful tool for organizing large projects or reusing some code. While building your solution on AppVeyor you need to checkout sub-modules as part of your build. Well, you can use the following command during `install` phase which [occurs between `clone` and `build`](http://www.appveyor.com/docs/build-configuration#build-pipeline):

    git submodule update --init --recursive

The problem arises when sub-modules refer private Git repositories which cannot be cloned without authentication and as a result you get stalled build. This is because sub-module repository does not contain SSH public key used to authenticate main repo, so Git is asking for credentials:

![sub-modules-stalled-build](/site/images/docs/how-to/sub-modules-stalled-build.png)


## The solution

**A custom SSH key could be used to checkout repository private sub-modules**. The rest of this article explains how to generate SSH key and setup AppVeyor project to use it.


## Check sub-modules path

First of all you have to check sub-modules URLs in `.gitmodules` to make sure they are in SSH format. For GitHub it should be something like:

    url = git@github.com:{owner}/{repo}.git

## Generate SSH key

Now, let's generate a new SSH key that will be used to fetch sub-modules.

In command prompt type the following command:

    ssh-keygen -t rsa

When prompted enter key file name, say `submodules` and empty passphrase.

> `ssh-keygen.exe` utility is part of Git installation for Windows and *typically* it's located in `C:\Program Files (x86)\Git\bin` directory.

In the current directory you'll find two files: `submodules` which contains private key and `submodules.pub` with public key.


## Add SSH public key to GitHub

> If you have only one sub-module in your main repository you can add public key directly to sub-module repo, however if there are multiple dependencies GitHub won't allow you to add the same key again.

Open `submodules.pub` file and copy its contents to clipboard.

Navigate to [SSH Keys](https://github.com/settings/ssh) under your GitHub profile and add a new SSH Key with contents from clipboard and any title.


## Configure AppVeyor project to use SSH key

Next, during the build on the worker machine we have to put private key contents into `%USERPROFILE%\.ssh\id_rsa` *before* running `git submodule update --init --recursive` command.

We'll store contents of private key in environment variable.

### UI

Open "Environment" tab of project settings in AppVeyor and add a new environment variable called `priv_key`. Open `submodules` file with private key and copy base-64 body of the key between `-----BEGIN RSA PRIVATE KEY-----` and `-----END RSA PRIVATE KEY-----` into clipboard:

![rsa-private-key](/site/images/docs/how-to/rsa-private-key.png)

Paste contents of clipboard into value field of environment variable. New lines will be changed to spaces - that's OK - we'll turn them back to new lines with PowerShell script shown below.

**Mark variable as "secure" by clicking "lock" icon next to it** - this will prevent it from being decoded during pull requests (see explanation below).

In `Install script` field paste the following code:

    $fileContent = "-----BEGIN RSA PRIVATE KEY-----`n"
    $fileContent += $env:priv_key.Replace(' ', "`n")
    $fileContent += "`n-----END RSA PRIVATE KEY-----`n"
    Set-Content c:\users\appveyor\.ssh\id_rsa $fileContent
    git submodule -q update --init --recursive

### appveyor.yml

Copy the contents of private key to clipboard as shown above and open [Encrypt data](https://ci.appveyor.com/tools/encrypt) tool in AppVeyor. Encrypt the value of clipboard using that page.

Add this to your `appveyor.yml`:

    environment:
      priv_key:
        secure: <encryped-value>

    install:
      - ps: $fileContent = "-----BEGIN RSA PRIVATE KEY-----`n"
      - ps: $fileContent += $env:priv_key.Replace(' ', "`n")
      - ps: $fileContent += "`n-----END RSA PRIVATE KEY-----`n"
      - ps: Set-Content c:\users\appveyor\.ssh\id_rsa $fileContent
      - git submodule update --init --recursive


## Security considerations

"Secure" variables means you can safely put them into `appveyor.yml` that is visible to others. Other than that they are just regular environment variables in a build session that could be easily displayed in a build log by simple `Get-ChildItem env:`.

However, secure variables are *not* decoded during Pull Request builds which prevents someone from submitting PR with malicious build script displaying those variables. In more controlled environment through with a trusted team and private GitHub repositories there is an option on General tab of project settings to allow secure variables for PRs.

> **If you accidentally submitted any sensitive information into public repo or displayed it in a public build log don't wait - invalidate/change/re-generate that data immediately!**