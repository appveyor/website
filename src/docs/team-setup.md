---
layout: docs
title: Set up your team
---

# Set up your team
{:.no_toc}

* Comment to trigger ToC generation
{:toc}

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

> Once certain email is added as a user into AppVeyor account that user cannot have their own account. If you need to move a user to his/her own account delete this user from your account, allow them to register their own AppVeyor account and then add that email back as collaborator.

### Collaborator

When existing user is assigned to another account it becomes *collaborator* in that account. When adding collaborator to an account a user with specified email should already exist in AppVeyor. Collaborator is assigned a role from account he/she is being added into.

### Role

*Role* is a set of permissions. Role belongs to an account and account can have multiple roles. Every account has two built-in roles: "Administrator" and "User". Singular form of default role names underlines the fact that **each user or collaborator can have only one role** assigned. Basically, in AppVeyor we say not *"user belongs to Administrators role"*, but *"user **is** Administrator"*.

### Permissions

*Permissions* in AppVeyor can be account-wide and entity-specific. Account-wide permissions are defined on role level and limit user access to certain AppVeyor functionality or allow certain actions.

Permissions can be defined on project level and limit user access to that particular project. Project-level permission has three states: `Allow`, `Deny` and `Inherit`. "Inherit" means applying permissions defined on role level. Both "Allow" and "Deny" overrides permissions defined on role level.

By default, all project permissions for "Administrator" role are "Allow" and for "User" are "Inherit".

## Switching between accounts

A user can be assigned to multiple accounts with different roles. Existing user can be added to another account as collaborator (see above).

To switch between accounts user must first **sign out**. While signing in again user will be presented with a dropdown listing all accounts he/she is assigned:

![select-account-on-signin](/assets/images/docs/select-account-on-signin.png)

## Leaving account

You can remove yourself as a collaborator from a certain account on [profile page](https://ci.appveyor.com/profile).

## GitHub teams

### GitHub integration notes

* works only with "GitHub" sign in button. If you login with email/password teams membership is not applied.
* does not translate/use GitHub repository permissions. AppVeyor uses your teams membership to determine which AppVeyor accounts you have access to and with what role.
* user or collaborator with email from GitHub account you are logging in with should not exist in target AppVeyor account. When you logging in with GitHub sign in button AppVeyor implicitly creates hidden collaborator in the target account.

### Understanding relationship between GitHub team and AppVeyor role

Your access as a GitHub user to a certain AppVeyor account is controlled by `GitHub team <-> AppVeyor role` relationship which is 1-to-1 under given AppVeyor account. That means that GitHub team can be mapped to a single AppVeyor role only. Under different account the same GitHub team can be mapped to a different role from that account.

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
