---
layout: docs
title: Set up your team
---

<!-- markdownlint-disable MD022 MD032 -->
# Set up your team
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Introduction

[AppVeyor Team](https://ci.appveyor.com/team) page allows you adding new users or collaborators to your AppVeyor account as well as configure integration with GitHub teams.

## Definitions

### Account

*Account* is an entity grouping all other resources like projects, environments and users. Account can have multiple projects and each project belong to a single account. Account may have a plan and subscription assigned. Another analogy for an account is *organization*.

*Account name* is the part you see in project URL:

    https://ci.appveyor.com/project/<account-name>/<project-slug>

Account name can be changed on [account details page](https://ci.appveyor.com/account).

### Account owner

Each account has *account owner* - a user who registered that account. Owner belongs to "Administrator" role and cannot be deleted.

### User

*User* is authenticating entity or *login* which is uniquely identified by email address. User is created under certain account and belongs to that account. User can have only one role assigned.

Once certain email is added as a user into AppVeyor account that user cannot have their own account.
If you need to move a user to his/her own account delete this user from your account,
allow them to register their own AppVeyor account and then add that email back as collaborator.

### Collaborator

When existing user is assigned to another account it becomes *collaborator* in that account. When adding collaborator to an account a user with specified email should already exist in AppVeyor. Collaborator is assigned a role from account he/she is being added into.

### Role

*Role* is a set of permissions. Role belongs to an account and account can have multiple roles. Every account has two built-in roles: "Administrator" and "User". Singular form of default role names underlines the fact that **each user or collaborator can have only one role** assigned. Basically, in AppVeyor we say not *"user belongs to Administrators role"*, but *"user **is** Administrator"*.

### Permissions

*Permissions* in AppVeyor can be account-wide and entity-specific. Account-wide permissions are defined on role level and limit user access to certain AppVeyor functionality or allow certain actions.

Permissions can be defined on entity (project or deployment environment) level and limit user access to that particular entity. Entity-level permission has three states: `Allow`, `Deny` and `Inherit`. "Inherit" means applying permissions defined on role level. Both "Allow" and "Deny" overrides permissions defined on role level.

By default, all entity permissions for "Administrator" role are "Allow" and for "User" are "Inherit".

### 'Deny all' approach

Default “Inherit” permission is handy when new entity (project or deployment environment) added and users with certain role automatically have required access to this new entity. However, in some cases (like having vendors or interns with AppVeyor access) access to all entities should be denies to the specific role, unless explicitly allowed on specific entity level.

To solve this problem, select `Deny all projects and environments, unless explicitly allowed` in role permissions. Add assign this role to users that need to be restricted. Then navigate to specific entity and explicitly allow specific permissions for this role.

## Switching between accounts

A user can be assigned to multiple accounts with different roles. Existing user can be added to another account as collaborator (see above).

To switch between accounts user must first **sign out**. While signing in again user will be presented with a dropdown listing all accounts he/she is assigned:

![select-account-on-signin](/assets/img/docs/select-account-on-signin.png)

## Leaving account

You can remove yourself as a collaborator from a certain account on [profile page](https://ci.appveyor.com/profile).

## GitHub integration

### Limitations

* works only with "GitHub" sign in button. If you login with email/password teams membership is not applied.
* does not translate/use GitHub repository permissions. AppVeyor uses your teams membership to determine which AppVeyor accounts you have access to and with what role.
* user or collaborator with email from GitHub account you are logging in with should not exist in target AppVeyor account. When you logging in with GitHub sign in button AppVeyor implicitly creates hidden collaborator in the target account.

### Understanding relationship between GitHub team and AppVeyor role

Your access as a GitHub user to a certain AppVeyor account is controlled by *GitHub team &harr; AppVeyor* role relationship which is 1-to-1 under given AppVeyor account. That means that GitHub team can be mapped to a single AppVeyor role only. Under different account the same GitHub team can be mapped to a different role from that account.

**Important note:** If GitHub user belongs to two or more teams defined in AppVeyor there is no way for AppVeyor to resolve account role the user should be assigned to. For example, suppose you added mapping for two GitHub teams:

    GitHub team          AppVeyor role
    ------------         -----------------
    MyOrg/Owners         Administrator
    MyOrg/Developers     User

and, say, GitHub user `John` belongs to both `MyOrg/Owners` and `MyOrg/Developers`. When he is signing in with GitHub button AppVeyor won't be able to precisely determine his role as he is a member of both teams, but in AppVeyor user can have only one role assigned.

To resolve this limitation we recommend creating another GitHub team, for example "AppVeyor Admins", that maps to "Administrator" role in AppVeyor:

    GitHub team                   AppVeyor role
    ------------                  -----------------
    MyOrg/AppVeyor Admins         Administrator
    MyOrg/AppVeyor Developers     Developer


### Setting up AppVeyor account for GitHub organization

When you've initially signed up for AppVeyor with "GitHub" button on either [Sign up](https://ci.appveyor.com/signup) or [Login](https://ci.appveyor.com/login) pages
a new AppVeyor account is automatically created for you. This account is named after your GitHub username and can be considered as a "personal" account.

However, in most cases you want to have "organizational" account bound to some GitHub organization and GitHub users assigned to that organization can manage projects under that AppVeyor account.
Also, "Organizational" account allows having project URLs containing organization name, for example `https://ci.appveyor.com/project/{organization}/{project}`.

You can easily create an "organizational" account in AppVeyor.

#### Register a new AppVeyor account with email/password

You should have a separate email address which does not exist in AppVeyor and will be used as "organizational" account owner, for example {% raw %}<code>{organization}-appveyor@your-domain.com</code>{% endraw %}.
Go to [Sign up](https://ci.appveyor.com/signup) page and register a new account with that email and password. For "Your name" use something like "{organization} admin".

Next, navigate to [Account details](https://ci.appveyor.com/account) page and check/rename "Account name" - this is {% raw %}<code>{organization}</code>{% endraw %} name that will be used in project URLs.
On the same page authorize AppVeyor to access GitHub. A new OAuth token will be generated and used for accessing organization's repositories and teams on behalf of your GitHub user.
Make sure your GitHub user has owner/admin rights on the repositories you are going to add later as AppVeyor projects.

#### Add yourself as co-administrator

Go to [Team](https://ci.appveyor.com/team) page and add a new collaborator with primary email address of your GitHub account and *Administrator* role.
**Sign out from AppVeyor**. Login again, but now with "GitHub" button - you will be presented with dropdown displaying two accounts - your personal
account and {% raw %}<code>{organization}</code>{% endraw %} account you just created. Select {% raw %}<code>{organization}</code>{% endraw %} account and click "GitHub" sign in button again.
Now you are logged in with your GitHub user and are managing "organizational" account.

#### Add your co-workers

Setup GitHub teams or add individual collaborators/users as described above in this document.
