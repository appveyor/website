---
layout: one-column
title: AppVeyor Security
description: AppVeyor Security
---

# AppVeyor Security

## Employee access

## Source code

The code is accessed on (cloned to) build VM only. For every new build a clean VM is being provisioned which is immediately decommissioned once the build finishes. Build VMs are never re-used. To authenticate during the clone of private repo either SSH key is used or OAuth token for “shallow” or HTTPS clones. Both SSH key and OAuth token are stored in the database in encrypted form and delivered to build VM during the build.

On shared AppVeyor cloud only appveyor.yml is being fetched from the repo via respective SCM API – this is required con configure the build. OAuth token is used to authenticate against GitHub/Bitbucket/VSTS/GitLab API. Token is stored in encrypted form in the database.


## Contact us

If you have any question, comment or concern about AppVeyor security please [contact us](mailto:team@appveyor.com).