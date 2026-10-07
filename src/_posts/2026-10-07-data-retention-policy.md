---
title: 'Data retention policy update'
---

Back in 2018 we introduced an artifacts retention policy to reduce AppVeyor hosting costs and eliminate unnecessary waste of cloud resources. Since then the amount of build history, logs, test results and NuGet packages stored in AppVeyor has kept growing, and most of it is never accessed again. We are now extending the retention policy to all CI data.

Starting **October 26, 2026** the following retention periods apply:

* **Builds and jobs** (including logs, test results and compilation messages): 18 months for paid accounts, 6 months for free accounts. The 10 most recent builds of every project are always kept.
* **Deployments**: 18 months for paid accounts, 6 months for free accounts. The 10 most recent deployments of every environment are always kept.
* **NuGet packages** on project and account feeds: 18 months for paid accounts, 6 months for free accounts.
* **Artifacts**: 3 months for paid accounts, 1 month for free accounts (unchanged).
* **Audit log and events**: 6 months for all accounts.
* **Build cache**: entries not updated for 6 months are removed, for all accounts.

What changes for existing users:

* Builds, deployments and NuGet packages now expire. NuGet packages were previously kept indefinitely.
* The artifacts retention rule stays the same.
* The first clean-up will run on October 26, 2026 and will remove data that is already older than the periods above.

The full policy is published in our docs: [Data retention policy](/docs/data-retention-policy/). It also describes [what you can do before your data expires](/docs/data-retention-policy/#before-your-data-expires), such as copying artifacts to your own storage, publishing NuGet packages to an external feed, and downloading logs and test results via the REST API.

If you have custom requirements please let us know and we'll discuss your needs.

Best regards,<br>
AppVeyor team
