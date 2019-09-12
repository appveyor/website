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

While "self-hosted agent" solution is relatively easy to setup and configure it has a number of drawbacks:

* Computer or VM with self-hosted agent is running 24x7 waiting for your builds and thus consuming electricity or adding to your monthly bill;
* Concurrent builds are running directly on the host operating system, so they interfere, produce leftovers (files, databases, configs, registry settings) or could completely kill the host machine. It makes you think about additional guard/synchronizing/cleanup code in your builds thus wasting your time;

AppVeyor BYOC also allows you to run build jobs directly on the host, but it really **shines at provisioning build VMs on demand**!

* Don't have to run 24x7 machine with the agent.
* Clean environment on every build.
* We manage build images for you!


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