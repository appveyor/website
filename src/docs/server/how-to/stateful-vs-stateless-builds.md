---
layout: docs
title: Stateful vs stateless builds
---

# Stateful vs stateless builds

AppVeyor can run builds on build workers of two types:

* **stateful** (or permanent) build workers
* **stateless** (or transient) build workers

## Stateful workers

Stateful workers are "always on" Build Agent machines for which any changes are preserved between builds. For example, any Chocolatey package installed, any NuGet package downloaded or any database created stay there and "visible" for next builds. While stateful builds can drastically reduce overall build time by having everything ready and pre-heated for consequent builds they require your build scenarios to include "setup" and "teardown" code increasing complexity of your builds. This approach is recommended for builds with minimum environment changes.

## Stateless workers

Stateless build workers are virtual machines provisioned from template or reset to the initial "clean" state and dedicated to a single build. When the build is finished machine is "decommissioned", i.e. either deleted or reverted to "clean" state and returned to the pool.

Pros:

* Dedicated pristine environment for every build
* Build workers are on during the build only, thus preserving resources and reducing costs.

Cons:

* Additional time is required for provisioning and configuring build worker machines.
