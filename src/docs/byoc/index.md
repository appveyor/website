---
layout: docs
title: Bring Your Own Cloud or Computer (BYOC)
---

<!-- markdownlint-disable MD022 MD032 -->
# Bring Your Own Cloud or Computer (BYOC)
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## What is BYOC

Bring Your Own Cloud (BYOC) allows running builds on your own infrastructure. Builds could be run inside VMs (Azure, AWS, GCE, Hyper-V), in Docker containers (Windows, Linux and macOS) or directly on the host (Windows, Linux and macOS). BYOC is available for hosted AppVeyor accounts and self-hosted AppVeyor Server installations.

Some of the reasons you may want to run builds on your infrastructure:

* Using under-utilized existing hardware such as an old build server under your desk;
* Using hardware with better performance like your team's workstations or VMs with better/different characteristics (GPU-enabled, memory-, disk-, network-optimized);
* Running builds on your team's Macs or a Mac hosted on Macstadium. While we are hard-working on bringing macOS support to AppVeyor service this might be a viable option for now;
* Custom/proprietary software that cannot be installed during the build;
* Security/compliance requirements like the code or build artifacts not leaving particular cloud/region/zone/network;

## How is it better than "self-hosted agents" in other CI/CD solutions?

AppVeyor really shines at running builds on dynamically provisioned VMs. While AppVeyor also supports running builds on a host directly (aka "self-hosted agent") creating VMs on demand has a number of advantages:

* **Significant savings on a monthly cloud bill**. Build VM is created for the duration of a build job and immediately deleted when this build is over. Major cloud providers have per-minute pricing and as a result you pay only for the "clean" build time. With a "self-hosted agent" approach a VM with an agent is running 24x7 waiting for your builds, consuming electricity or adding to your monthly bill. You are not going to run expensive GPU-enabled instance for 24x7x265, aren't you?
* **Pristine environment on every build**. Build VMs are provisioned from an image or snapshot and never reused for the consequent builds. You get clean and predictable environment every time you run a new build. With "self-hosted agent" approach concurrent builds are running directly on the host operating system, so they interfere, produce leftovers (files, databases, configs, registry settings) or could completely kill the host machine. It makes you think about additional guard/synchronizing/cleanup code in your builds thus wasting your time.
* **Unlimited parallelism**. A single build can be run on hudreds of VMs simoultanously for a shorter period of time. Say, you have a suite with 1,000 tests and it takes 1 hour on a single core to run them all. You can run the suite on 10 single-core VMs in parallel and reduce the overall test time to 6 minutes by paying the same amount to the cloud provider!

## What about build VM images?

We've got you covered!

[TBD]


## Getting started



* [Windows](/docs/byoc/windows/)
* [Linux](/docs/byoc/linux/)
* [Mac](/docs/byoc/mac/)
* [Docker](/docs/byoc/docker/)
* [Azure](/docs/byoc/azure/)
* [AWS](/docs/byoc/aws/)
* [GCE](/docs/byoc/gce/)
* [Hyper-V](/docs/byoc/hyper-v/)

## Routing builds to your cloud

[TBD]

## Build clusters

[TBD] - grouping multiple clouds, failover, etc.