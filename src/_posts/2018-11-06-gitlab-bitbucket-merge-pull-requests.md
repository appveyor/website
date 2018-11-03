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

TBD

Best regards,<br>
AppVeyor team
