---
layout: docs
title: BYOC - AWS
---

<!-- markdownlint-disable MD022 MD032 -->
# Connecting AWS
{:.no_toc}

What is that?

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Overview

You can connect your AppVeyor account (on both hosted AppVeyor and on-premise AppVeyor Server) to your own AWS account for AppVeyor to instantiate build VMs in it. It has a lot of benefits like having ability to customize your build image, select desired VM size, set custom build timeout and much more.

To simplify setup process for you, we created an automation framework which does all heavy lifting for you: it provisions necessary AWS resources, runs Hashicorp Packer to create a basic build AMI (based on Windows Server 2019), and puts all AppVeyor configuration together. As a result, you should be able to start builds on AWS immediately (and optionally customize your AWS build environment later).

**WARNING:** This automation will create AWS resources such as S3 buckets, security group and key pair. Also, Packer will create its own temporary AWS resources and leave AMI for future use by AppVeyor build VMs. Please be aware of possible charges from Amazon. If AWS account you select contains production resources, you might consider creating a separate account and run the automation against it. Additionally, a separate account is better to differentiate AWS bills for CI machines from other AWS bills.

## Requirements

[TBD]

## How to

[TBD]
