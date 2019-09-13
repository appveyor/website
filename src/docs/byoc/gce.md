---
layout: docs
title: BYOC - GCE
---

<!-- markdownlint-disable MD022 MD032 -->
# Connecting GCE
{:.no_toc}

What is that?

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Overview

You can connect your AppVeyor account (on both hosted AppVeyor and on-premise AppVeyor Server) to your own Google cloud account and project for AppVeyor to instantiate build VMs in it. It has a lot of benefits like having ability to customize your build image, select desired VM size, set custom build timeout and much more.

To simplify setup process for you, we created an automation framework which does all heavy lifting for you: it provisions necessary Google Cloud Engine and Storage resources, runs Hashicorp Packer to create a basic build image (based on Windows Server 2019), and puts all AppVeyor configuration together. As a result, you should be able to start builds on Google Cloud Engine (GCE) immediately (and optionally customize your GCE build environment later).

**WARNING:** This automation will create Google resources such as network and service account, as well as GCS buckets. Also, Packer will create its own temporary GCE resources and leave VM image for future use by AppVeyor build VMs. Please be aware of possible charges from Google. If GCE project you select contains production resources, you might consider creating a separate project or even account and run the automation against it. Additionally, a separate account is better to differentiate Google bills for CI machines from other Google bills

## Requirements

[TBD]

## How to

[TBD]
