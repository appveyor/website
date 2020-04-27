---
layout: docs
title: Pushing to remote Git repository from a build
---

# Pushing to remote Git repository from a build

Sometimes during the build (say, on successful build) you need to make a commit to Git repo and push it back to your remote repository. This could be a new tag as well which could be used to kick off deployment or mark a release.

Running something on successful builds is not a problem. Just add to your `appveyor.yml`:

```yaml
on_success:
  - git commit ...
  - git push ...
```

**Note:** AppVeyor checks out only the last commit and not the entire branch. So you may have to check out the wanted branch: `git checkout master`

But the main question here is how to authenticate Git commands. If you try using any Git command against remote repository you'll get stuck build because Git is asking for credentials. In most cases you can't supply username/password in command line (*we are not considering the case when credentials are embedded in repo URL as this is bad*).

Two methods to access remote Git repository exist: **SSH** and **credentials store**. The scenario with custom SSH key is described in [this article](/docs/how-to/private-git-sub-modules/) (*with the only difference is that the key should be deployed under GitHub user account, not repo, to have write access*).

This article will demonstrate how to use Git **credential store** to avoid Git asking for credentials and stalling the build. We will be using GitHub as a repository provider, however described technique could be applied to any Git hosting.

At a glance the entire process consists of these steps:

1. [Creating GitHub Personal Access Token](#creating-github-personal-access-token)
2. [Configuring build secure variable with access token](#configuring-build-secure-variable-with-access-token)
3. [Enabling Git credential store](#enabling-git-credential-store)
4. [Adding access token to credential store](#adding-access-token-to-credential-store)
5. [Indicate git user name and mail](#indicate-git-user-name-and-mail)


## Creating GitHub Personal Access Token

Of course, you can use your GitHub username/password to authenticate, but there is a better approach - [Personal Access Tokens](https://github.com/blog/1509-personal-api-tokens) which:

1. Could be easily revoked from GitHub UI;
2. Have a limited scope;
3. Can be used as credentials with Git commands (*this is what we need*).

Use this [GitHub guide for creating access tokens](https://help.github.com/articles/creating-an-access-token-for-command-line-use/).

The scope needed is **public_repo** for a public repository or **repo** for a private repository.


## Configuring build secure variable with access token

Encrypt access token on "Encrypt configuration data" page in AppVeyor (**Account** &rarr; **Encrypt YAML**) and then put it as secure variable into your `appveyor.yml`, for example:

```yaml
environment:
  access_token:
    secure: zYCOwcOlgTzvbD0CjJRDNQ==
```

## Enabling Git credential store

Git doesn't preserve entered credentials between calls. However, it provides a mechanism for caching credentials called [Credential Store](https://git-scm.com/docs/git-credential-store). To enable credential store we use the following command:

    git config --global credential.helper store


## Adding access token to credential store

Default credential store keeps passwords in clear text (*this is OK for us as build worker is private and not re-used or shared between builds*). The storage represents a single `%USERPROFILE%\.git-credentials` file where each line has a form of:

```text
http[s]://<username>:<password>@<host>
```

For example, for GitHub access token it will be:

    https://<access-token>:x-oauth-basic@github.com

When authenticating with access token `x-oauth-basic` is used as a stub password.

To append that line to `.git-credentials` we use the following PowerShell command:

```yaml
ps: Add-Content "$HOME\.git-credentials" "https://$($env:access_token):x-oauth-basic@github.com`n"
```

**Note:** *$HOME* is an automatic variable available in powershell on Windows and Linux and is therefore cross-platform. If you currently use `cmd:` you can use *%USERPROFILE%* instead.

`.git-credentials` is very "sensitive" to a new line that must be `\n`.
If you try appending a line with something more "natural" like
`echo https://%access_token%:x-oauth-basic@github.com>> %USERPROFILE%\.git-credentials`
it won't work because `\r\n` will be used.

## Indicate git user name and mail

You have to indicate your git user name and mail:

    git config --global user.email "Your email"
    git config --global user.name "Your Name"

## Updating remote

If you are pushing to the same *private* repository the build was cloned from you should probably update its remote or add a new one using HTTPS protocol.

## Conclusion

Complete example for your `appveyor.yml` will look like this:

```yaml
environment:
  access_token:
    secure: zYCOwcOlgTzvbD0CjJRDNQ==

on_success:
  - git config --global credential.helper store
  - ps: Add-Content -Path "$HOME\.git-credentials" -Value "https://$($env:access_token):x-oauth-basic@github.com`n" -NoNewline
  - git config --global user.email "Your email"
  - git config --global user.name "Your Name"
  - git commit ...
  - git push ...
```
