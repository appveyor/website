---
title: GitHub Apps integration
---

We've just deployed a huge AppVeyor update with lots of awesome new features and improvements! The scope is quite large for a single blog post so, to keep your interest, we are going to cover all the new functionality in a few separate posts:

* GitHub Apps (this post)
* Bitbucket OAuth 2.0 and pull requests
* GitLab Enterprise, GitLab login button and merge requests
* Multi-account UI improvements
* Two-factor authentication and other security enhancements

Today we are thrilled to introduce the integration with GitHub Apps!

## Benefits of GitHub Apps

GitHub Apps enable 3rd-paty integrations to work with GitHub API in more harmonized and secure way.
From user perspective GitHub Apps have the following key advantages over the current OAuth authentication (aka OAuth Apps):

### Acts on its own behalf

GitHub App acts on its own behalf and not as GitHub user authorizing the App. Your personal GitHub identity is not exposed to AppVeyor and other AppVeyor account members and [API rate limits](https://developer.github.com/apps/building-github-apps/understanding-rate-limits-for-github-apps/) are counted towards App "installation" (read "instance").

### Has access to only selected repositories

You grant AppVeyor GitHub App access to specific organizations/repositories only with narrow permissions. No need to maintain a "bot" account as a separate GitHub user. An ability to cherry-pick specific repositories visible to AppVeyor App installation is the number one requirement for GitHub users with an access to both personal and corporate repositories.

### Requires fewer permissions

GitHub App requires fewer permissions than OAuth App:

* **Read** access to code
* **Read** access to members, metadata, and pull requests
* **Read** and **write** access to checks and commit statuses

With GitHub App repository webhooks and deploy (SSH) keys stay untouched. Instead of adding a deploy (SSH) key to the repo the App uses "installation" token for cloning the repo via HTTPS. Installation token is rotating each hour.
For starting new builds on push and pull requests AppVeyor App uses centralized webhook handler which is called all app-enabled repos.

You can read more [about GitHub Apps](https://developer.github.com/apps/about-apps/) and [their differences from OAuth Apps](https://developer.github.com/apps/differences-between-apps/).

## Migrating existing projects to GitHub Apps

Migration to GitHub Apps for the existing AppVeyor account is a straightforward process. Login to AppVeyor and go to **Settings &rarr; Authorizations** page for the selected account.

Expand **GitHub** section and click **Revoke access** button.

Click **Install AppVeyor App** button and install AppVeyor App for the selected GitHub user/org and repositories. If you are installing the App to the organization you must be the owner of it.

If you need to enable AppVeyor for more than one org/repo click **Update installations** button and enable the App for other orgs/repos.

If you have multiple AppVeyor accounts you can import existing AppVeyor installations into it.

### Webhooks and deploy keys

Once GitHub App authorization is enabled for your account repository-specific webhooks and deploy keys are no longer needed and can be removed from respective repositories.

However, if you still need to disable either "Push" or "Pull request" events for the selected project you can do so with corresponding checkboxes on "General" tab of AppVeyor project settings.

## Questions

### Can I still use OAuth authorization?

Yes, of course. All existing GitHub OAuth authorizations stay intact and both GitHub App and OAuth App integrations are available.
Though GitHub App is the recommended integration method you can always switch back to OAuth and have access to all orgs/repos authorizing GitHub user has access to.

### Can I install AppVeyor App through GitHub Marketplace?

We are going to maintain AppVeyor OAuth App in Marketplace for now as it provides more streamlined purchasing flow.
If a customer purchases through OAuth app they are getting automatically logged in into AppVeyor where they could authorize access to their repositores with either GitHub App or OAuth.

### Are GitHub Checks supported?

[GitHub Checks](https://developer.github.com/changes/2018-05-07-new-checks-api-public-beta/) is a successor to Commit Status API and depends on GitHub Apps. With finished support of GitHub Apps we paved the way to GitHub Checks integration which is coming in the following weeks.

Stay tuned! More details on other improvements will be published in the coming days!

Best regards,<br>
AppVeyor team
