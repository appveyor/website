---
title: 'GitLab and Bitbucket Merge/Pull Requests'
---

The AppVeyor team works hard to provide first class support for a growing number of GitLab and Bitbucket customers. The most important part of this work is enabling proper Pull/Merge requests builds.
Pull request support for Bitbucket was [implemented recently](/blog/2018/08/22/bitbucket-pull-requests/). Now we are happy to provide you with GitLab Merge request builds support as well!

## The importance of Pull/Merge request builds

Consider the following example:

In `master`, base (target) branch:

```csharp
class Customer
{
   string Name { get; set; }
   string Address { get; set; }
}
```

In `feature-A`, head (source) branch (created from `master`). The following class was added to `feature-A`:

```csharp
class Order
{
    Customer Customer { get; set; }
    DateTime Date { get; set; }
}

var order = new Order {
    Customer = new Customer { Name = "John", Address = "123 Street" }
}
```

Before `feature-A` is merged into `master`, the following change was committed into `master` (property `Address` renamed to `AddressLine`):

```csharp
class Customer
{
   string Name { get; set; }
   string AddressLine { get; set; }
}
```

If we separately test `master` and `feature-A` branches they will be both “green”, but once `feature-A` is merged into `master` the build of `master` branch will fail.

The problem in the example above happens at build stage, but more complicated issues can be exposed at unit or only even at end-to-end tests stages. Pull/Merge request builds help to detect this kind of issues, before the actual merge of head (source) branch into the base (target) one.

## Enabling Pull/Merge Request builds

For new GitLab and Bibucket projects, Pull/Merge request builds works out of the box.

To enable Merge request builds for GitLab projects created before October 2018, on GitLab project page open `Settings`, select `Integrations`, find AppVeyor Webhook and press `Edit`. Check `Merge request events` and press `Save changes`.

To enable Pull request builds for Bitbucket projects created before August 2018, follow instructions from [this post](/blog/2018/08/22/bitbucket-pull-requests/).

## Pull/Merge requests from a fork

If Pull/Merge request is originating from a private fork, some additional configuration is required to build successfully.

### Enabling AppVeyor to read commit details from the head repository

(and optionally set commit statuses)

#### GitLab (private and public forks)

Note: you can slip this section for public forks in case you do not need for AppVeyor to set up Merge request status.

Add GitLab user AppVeyor authorized with to the Members of private fork projects.

* To find this user name open `https://ci.appveyor.com/account/<account>/authorizations` and select `GitLab`:

![GitLab OAuth user](/assets/img/posts/gitlab-bitbucket-merge-pull-requests/gitlab-oauth-user.png)

* User’s role permission in the source repository should be at least `Reporter` role permission. To allow AppVeyr to to set up Merge request status, assign this user at least `Developer` role permission:

![GitLab add member](/assets/img/posts/gitlab-bitbucket-merge-pull-requests/gitlab-add-member.png)

#### Bitbucket (private forks only)

Add Bitbucket user AppVeyor authorized with to the Members of private fork projects.

* To find this user name open `https://ci.appveyor.com/account/<account>/authorizations` and select `Bitbucket`:

![Bitbucket OAuth user](/assets/img/posts/gitlab-bitbucket-merge-pull-requests/bitbucket-oauth-user.png)

* User’s role permission in the source repository should be at least `Read`:

![Bitbucket add user](/assets/img/posts/gitlab-bitbucket-merge-pull-requests/bitbucket-add-user.png)

### Enabling AppVeyor to fetch head branch from the private fork

To achieve this you need to add "SSH public key" from AppVeyor project setting to the source repository.

#### GitLab (private forks only)

* Open `https://gitlab.com/<user>/<project>/settings/repository` for private fork and expand `Deploy Keys`
* Navigate to `Privately accessible deploy keys` and find `AppVeyor project <project_name>` key
* Press `Enable`

![GitLab deploy keys](/assets/img/posts/gitlab-bitbucket-merge-pull-requests/gitlab-deploy-keys.png)

#### Bitbucket (private forks only)

* Copy `SSH public key` which can be found on `General` tab of AppVeyor project settings:

![AppVeyor SSH key](/assets/img/posts/gitlab-bitbucket-merge-pull-requests/appveyor-ssh-key.png)

* In private fork repo open `Settings`, navigate to `Access Keys` and add AppVeyor SSH key:

![Bitbucket Access keys](/assets/img/posts/gitlab-bitbucket-merge-pull-requests/bitbucket-access-keys.png)

Best regards,<br>
AppVeyor team
