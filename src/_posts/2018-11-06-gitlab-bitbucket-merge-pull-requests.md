---
title: 'GitLab and Bitbucket Merge/Pull Requests'
---

AppVeyor team works hard to provide first class support for growing number of GitLab and Bitbucket customers. Most important part of this work is enabling proper Pull/Merge requests builds.
Pull request support for Bitbucket was [implemented recently](/blog/2018/08/22/bitbucket-pull-requests/). Now we are happy provide you with GitLab Merge request builds support.

#### Importance of Pull/Merge request builds can be described with the following example:

Base (target) branch:
`master`:

```csharp
class Customer
{
   string Name { get; set; }
   string Address { get; set; }
}
```

Head (source) branch created from Base (target) branch and the following class added:
`feature-A`:

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

Then before `feature-A` merged to `master`, the following change happens to `master` (property `Address` renamed to ` AddressLine`):

```csharp
class Customer
{
   string Name { get; set; }
   string AddressLine { get; set; }
}
```

`master` build can be “green”, and `feature-A` build can be “green”, but after `feature-A` merged to `master` it will fail. PR/MR build helps to detect this kind of issues, before actual merge.

TBD

Best regards,<br>
AppVeyor team
