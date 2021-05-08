---
title: 'Azure DevOps (VSTS) integration update'
---

AppVeyor uses [OAuth to access Azure DevOps](https://docs.microsoft.com/en-us/azure/devops/integrate/get-started/authentication/oauth?view=azure-devops) resources.
Azure DevOps removed obsolete authorization scopes from OAuth flow which broke the integration with AppVeyor. As a result some AppVeyor customers were unable to login with VSTS button or start new builds with VSTS repositories.

We deployed a fix and the customers should be able to re-authorize Azure DevOps on either "New project" or "Account > Authorization" pages. Unfortunately, we were unable to keep original OAuth app registration and had to create a new one, because scopes of existing app registration cannot be updated.

Best regards,<br>
AppVeyor team

Follow us on Twitter: [@appveyor](https://twitter.com/appveyor)