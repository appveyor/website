---
title: 'Update: Artifacts retention policy'
---

Since we announced [Artifacts retention policy](/blog/2018/05/24/artifacts-retention-policy/) on May 24th, we have heard your concerns and are making the following changes to the policy:

* NuGet packages will not be deleted.
* Retention policy for artifacts will be 6 months for both open-source and private accounts without exclusions.
* Some projects have requirements to store artifacts beyond 6 months period. As they will not be stored on AppVeyor we have prepared two guides:
    * [Copying artifacts of the finished builds to an external storage](/docs/packaging-artifacts/#copying-artifacts-of-the-finished-builds-to-an-external-storage)
    * [Copying artifacts to an external storage during the build](/docs/packaging-artifacts/#copying-artifacts-to-an-external-storage-during-the-build)
* To give customers more time to export important artifacts (older than 6 months) we are moving effective date of the policy to June 17th, 2018.

Best regards,<br>
AppVeyor team