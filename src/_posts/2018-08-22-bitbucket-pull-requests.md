---
title: 'Update: Bitbucket Pull Requests'
---

Pull Requests builds are useful in answering the question “What if this PR merged into target (base) branch”. Feature branch (other terms are `source` or `head` branch) build is not enough to answer this question, because while PR was developed some other commits could be made to the `target` (or `base`) branch.

During PR build AppVeyor merges PR with target (`source` or `head`) branch safely in transient build environment and runs required build scenario. Scenario itself can be altered in many ways for Pull Requests builds with project settings (like `Pull Requests do not increment build number`) and specific PR-related environment variables (like `APPVEYOR_PULL_Requests_HEAD_REPO_BRANCH`).

AppVeyor supports GitHub Pull Requests builds for years. Now we added Bitbucket Pull Requests support. For new projects it should work out-of-the box. For existing ones AppVeyor project webhook should be updated so the following `Pull Requests` events added: `Created`, `Updated`, `Merged`, `Declined`. To set this, navigate to your Bitbucket repository Settings tab, select Webhooks, find `AppVeyor project webhook` and press `Edit`.

If you do not need Bitbucket PR builds, uncheck those Webhook setting, so AppVeyor will not be notified of PR creations and changes.

Best regards,<br>
AppVeyor team
