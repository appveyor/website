---
title: 'GitLab and Bitbucket Merge/Pull Requests'
---

AppVeyor team works hard to provide first class support for growing number of GitLab and Bitbucket customers. Most important part of this work is enabling proper Pull/Merge requests builds.
Pull request support for Bitbucket was [implemented recently](/blog/2018/08/22/bitbucket-pull-requests/). Now we are happy provide you with GitLab Merge request builds support.

### Importance of Pull/Merge request builds

Consider the folowing example:

`master`, base (target) branch:

```csharp
class Customer
{
   string Name { get; set; }
   string Address { get; set; }
}
```

`feature-A`, head (source) branch (created from `master`). The following class added to `feature-A`:

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

before `feature-A` merged to `master`, the following change commited to `master` (property `Address` renamed to `AddressLine`):

```csharp
class Customer
{
   string Name { get; set; }
   string AddressLine { get; set; }
}
```

`master` build can be “green”, and `feature-A` build can be “green”, but after `feature-A` merged to `master` it will fail.

PR/MR build helps to detect this kind of issues, before actual merge

### Enabling Pull/Merge Request builds

For new GitLab and Bibucket projects Pull/Merge request builds works out of the box.

To enable Merge request build for GitLab projects created before October 2018, on GitLab project page open `Settings`, select `Integrations`, find AppVeyor Webhook and press `Edit`. Check `Merge request events` and press `Save changes`.

To enable Pull request build for Bitbucket projects created before August 2018, follow instructions from [this post](/blog/2018/08/22/bitbucket-pull-requests/).

### Private fork Pull/Merge requests

If Pull/Merge requests created in private fork, it requires some additional configuration to build successfully. **Skip this** if your Pull/Merge requests are being created in the **same repository or in public forks**

#### AppVeyor needs permissions read source commit details from the source repository

##### GitLab

Add user AppVeyor authorized with GitLab to the Members of private fork projects.

* To find this user name open `https://ci.appveyor.com/account/<account>/authorizations` and select `GitLab`

![GitLab OAuth user](/assets/img/posts/gitlab-bitbucket-merge-pull-requests/gitlab-oauth-user.png)

* User’s role permission in the source repository should be at least `Reporter` role permission

![GitLab add member](/assets/img/posts/gitlab-bitbucket-merge-pull-requests/gitlab-add-member.png)

##### Bitbucket

Add user AppVeyor authorized with Bitbucket to the Members of private fork projects.

* To find this user name open `https://ci.appveyor.com/account/<account>/authorizations` and select Bitbucket

![Bitbucket OAuth user](/assets/img/posts/gitlab-bitbucket-merge-pull-requests/bitbucket-oauth-user.png)

* User’s role permission in the source repository should be at least `Read`

![Bitbucket add user](/assets/img/posts/gitlab-bitbucket-merge-pull-requests/bitbucket-add-user.png)

#### AppVeyor build needs to fetch source branch from the private fork

To achieve this, you need to add "SSH public key" from AppVeyor project setting to the source repository

##### GitLab

* Open `https://gitlab.com/<user>/<project>/settings/repository` for private fork and expand `Deploy Keys`
* Navigate to `Privately accessible deploy keys` and find `AppVeyor project <project_name>` key
* Press `Enable`

##### Bitbucket

* Copy `SSH public key` which can be found on `General` tab of AppVeyor project settings
* In private fork repo open `Settings`, navigate to `Access Keys` and add AppVeyor SSH key

Best regards,<br>
AppVeyor team
