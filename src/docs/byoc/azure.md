---
layout: docs
title: BYOC - Running builds in Azure cloud
---

<!-- markdownlint-disable MD022 MD032 -->
# Running builds in Azure cloud
{:.no_toc}

AppVeyor BYOC allows connecting an existing Azure subscription and running builds in Windows or Linux VMs.

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Overview

You can connect your AppVeyor account (on both hosted AppVeyor and on-premise AppVeyor Server) to your own Azure subscription for AppVeyor to instantiate build VMs in it. It has a lot of benefits like having ability to customize your build image, select desired VM size, set custom build timeout and much more.

To simplify setup process for you, we created an automation framework which does all heavy lifting for you: it provisions necessary Azure resources, runs Hashicorp Packer to create a basic build image (based on Windows Server 2019), and puts all AppVeyor configuration together. As a result, you should be able to start builds on Azure immediately (and optionally customize your Azure build environment later).

**WARNING:** This automation will create Azure resources such as storage accounts, containers, virtual networks and subnets in subscription you select. Also, Packer will create its own temporary Azure resources and leave VHD blob for future use by AppVeyor build VMs. Please be aware of possible charges from Azure. If subscription you select contains production resources, you might consider creating a separate subscription and run the automation against it. Additionally, a separate subscription is better to differentiate Azure bills for CI machines from other Azure bills.

## Requirements

* Azure subscription with "Contributor" access

## Assisted setup

[TBD]
