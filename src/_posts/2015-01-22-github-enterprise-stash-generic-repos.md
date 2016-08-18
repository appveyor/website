---
title: AppVeyor adds support for GitHub Enterprise, Atlassian Stash and any external repository
---

We've just made AppVeyor available to even more developer teams! We are thrilled to announce new additions to AppVeyor supported source control providers:

* **GitHub Enterprise**
* **Atlassian Stash**
* Git
* Mercurial
* Subversion

Now you can use AppVeyor practically with any repository out there whether it is hosted in a cloud or on your own premises!

## GitHub Enterprise

<img src="/assets/images/posts/github-enterprise-stash/github.png" class="left" style="margin: 0 3rem 1rem 0;" alt="GitHub logo">

All beloved GitHub features such as branch builds, Pull Requests and webhooks are available in [GitHub Enterprise](https://enterprise.github.com/) integration. AppVeyor can use both OAuth and Personal Access tokens to authenticate against your GitHub Enterprise repositories.

<div class="clear-both"></div>

## Atlassian Stash

<img src="/assets/images/posts/github-enterprise-stash/stash.png" class="right" style="margin-left: 2rem;" alt="Atlassian Stash logo">

AppVeyor provides complete and seamless integration with [Atlassian Stash](https://www.atlassian.com/software/stash). Both OAuth and Basic authentications are supported. Whenever you add a new project in AppVeyor webhook and repository SSH key are automatically set.

<div class="clear-both"></div>

## Git, Mercurial and Subversion repositories

<img src="/assets/images/posts/github-enterprise-stash/git-mercurial-subversion.png" class="left" style="margin: 0 3rem 1rem 0;" alt="Git - Mercurial - SVN logo">

Now you can specify a URL to any repository hosted on the Internet! AppVeyor supports credentials and SSH authentication against those repositories.

<div class="clear-both"></div>

<div class="panel radius text-center">
    <h4>Do we support your repository now?</h4>
    <p><a href="mailto:team@appveyor.com">Send us a message</a> if you want to re-evaluate AppVeyor!</p>
</div>


## Having repository behind the firewall?

Of course, the repository should be accessible over the Internet. However, hold on! As a next milestone we are going to release AppVeyor on-premise edition which you can install behind your firewall. On-premise edition will provide even more tight integration with GitHub Enterprise and Stash such as "Sign in with GHE or Stash" buttons.

## Other news

In case you missed that:

* [We added test build worker image with Visual Studio 2015 CTP and SDK](/blog/2015/01/20/visual-studio-2015-ctp-image/)
* [io.js was deployed to build workers and "Testing with Node.js and io.js" guide published](/docs/lang/nodejs-iojs/)
