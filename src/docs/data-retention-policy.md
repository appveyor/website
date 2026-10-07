---
layout: docs
title: Data retention policy
---

<!-- markdownlint-disable MD022 MD032 -->
# Data retention policy
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

> This policy applies to the hosted AppVeyor service at [ci.appveyor.com](https://ci.appveyor.com) and is effective as of **October 26, 2026**.
> It does not apply to self-hosted [AppVeyor Server](/docs/server/), which has its own configurable [builds retention policy](/docs/server/configuration/#builds-retention-policy).

Storage within AppVeyor is an intermediary step in the continuous integration and deployment process, not an archival storage solution.
To keep hosting costs down and avoid wasting cloud resources on data nobody uses, AppVeyor automatically removes CI data after the retention periods described below.


## Summary

<table class="centered">
<tr>
    <th>Data</th>
    <th>Free accounts</th>
    <th>Paid accounts</th>
</tr>
<tr>
    <td>Builds and jobs</td>
    <td>6 months</td>
    <td>18 months</td>
</tr>
<tr>
    <td>Deployments</td>
    <td>6 months</td>
    <td>18 months</td>
</tr>
<tr>
    <td>NuGet packages</td>
    <td>6 months</td>
    <td>18 months</td>
</tr>
<tr>
    <td>Artifacts</td>
    <td>1 month</td>
    <td>3 months</td>
</tr>
<tr>
    <td>Audit log and events</td>
    <td>6 months</td>
    <td>6 months</td>
</tr>
<tr>
    <td>Build cache</td>
    <td>6 months</td>
    <td>6 months</td>
</tr>
</table>

The 10 most recent builds of every project and the 10 most recent deployments of every environment are always kept, regardless of their age.

Data older than the retention period is **permanently removed** and cannot be restored.


## Builds and jobs

Builds older than the retention period are permanently deleted together with all their data:

* Build jobs
* Console logs
* Test results
* Compilation messages
* Build artifacts

The 10 most recent builds of each project are kept regardless of their age, so the project history is never empty.

Having one or more deployments does not exempt a build from this rule. Deployment records are retained according to the [Deployments](#deployments) section below.


## Deployments

Deployment records (deployment status, logs and settings) older than the retention period are permanently deleted.

The 10 most recent deployments of each environment are kept regardless of their age.


## NuGet packages

NuGet packages published to project and account feeds that are older than the retention period are permanently removed from the feed.

> Previously, NuGet packages were not affected by the artifacts retention policy and were kept indefinitely. Starting October 26, 2026 they expire according to the table above.

If you need to keep packages for longer, publish them to an external feed such as NuGet.org, MyGet, Azure Artifacts or GitHub Packages using [NuGet deployment](/docs/deployment/nuget/).


## Artifacts

Build artifacts older than the retention period are permanently removed from AppVeyor artifact storage. Artifacts are also removed when their build is deleted under the [Builds and jobs](#builds-and-jobs) rule.

Artifact retention periods have not changed:

* Paid accounts: 3 months
* Free accounts: 1 month

See [Packaging artifacts](/docs/packaging-artifacts/#artifacts-retention-policy) for ways to copy artifacts to your own storage.


## Audit log and events

Account audit log entries and system events older than 6 months are permanently deleted. This applies to both free and paid accounts.


## Build cache

[Build cache](/docs/build-cache/) entries that have not been updated for 6 months are permanently deleted. This applies to both free and paid accounts.

Because a cache entry is refreshed every time a build updates it, actively built projects are not affected.


## Free and paid accounts

An account is considered *paid* while it has an active paid subscription. When a subscription expires or the account is downgraded to the Free plan, the free account retention periods apply to all data in the account.


## Before your data expires

AppVeyor does not keep backups of removed data. If you need to keep build output or history beyond the retention period, export it to your own storage:

* [Copy artifacts to your own storage during the build](/docs/packaging-artifacts/#copying-artifacts-to-external-storage-during-the-build) using inline deployment to FTP, Azure Blob, Amazon S3 or GitHub Releases.
* [Copy artifacts of finished builds](/docs/packaging-artifacts/#copying-artifacts-of-the-finished-builds-to-external-storage) using an Environment deployment.
* [Re-build a commit](/docs/packaging-artifacts/#re-build-last-successful-commit) to produce artifacts that have already expired.
* Download build logs and test results with the [AppVeyor REST API](/docs/api/).
* Publish NuGet packages to an external feed with [NuGet deployment](/docs/deployment/nuget/).


## Policy history

* [May 24, 2018](/blog/2018/05/24/artifacts-retention-policy/) - artifacts retention policy introduced.
* [June 5, 2018](/blog/2018/06/05/artifacts-retention-policy-update/) - artifacts retention policy updated.
* [March 30, 2021](/blog/2021/03/30/artifacts-retention-policy-update/) - artifacts retention periods changed to 3 months for paid and 1 month for free accounts.
* [October 7, 2026](/blog/2026/10/07/data-retention-policy/) - data retention policy extended to builds, deployments, NuGet packages, audit log and build cache, effective October 26, 2026.

If you have custom retention requirements please [contact us](/support/) and we'll discuss your needs.
