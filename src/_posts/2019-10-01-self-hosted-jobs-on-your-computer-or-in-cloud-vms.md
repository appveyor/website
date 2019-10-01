---
title: Self-hosted jobs on your computer or in cloud VMs
---

The new **Bring Your Own Cloud** feature enables you to run builds on your own infrastructure. Builds could be run inside VMs (**Azure, AWS, GCE, Hyper-V**), in **Docker** containers (Windows, Linux and macOS) or directly on the host (**Windows, Linux** and **macOS**). BYOC is available for hosted AppVeyor accounts and self-hosted AppVeyor Server installations.

<p class="text-center">
  <img src="/assets/img/docs/byoc/add-cloud-wizard.png" alt="Add new build cloud wizard" width="417" height="237">
</p>

## Use cases

* **Local testing** - connect your Windows or Mac development machine to AppVeyor and run build/tests there.
* **Custom build images** - build your own custom build VM images optimized for your needs. [We'll help you with that](/docs/byoc/#what-about-build-vm-images)!
* **Custom VM sizes/types** - use servers or VMs with better/different characteristics (GPU-enabled, memory-, disk-, network-optimized).
* **Per-minute pricing** - Build VM is created for the duration of a build job and immediately deleted when this build is over. AWS, GCE and Azure have per-minute pricing so you pay for the "clean" build time only.
* **Security/compliance requirements** - the code or build artifacts not leaving particular cloud/region/zone/network.

## Advantages

While AppVeyor BYOC supports running builds on a host directly (aka "self-hosted agent") it really shines at running builds on dynamically provisioned VMs. Creating VMs on demand has a number of advantages:

* **Significant savings on a monthly cloud bill** - build VM is created for the duration of a build job and you pay for "clean" build time. "Self-hosted agent" is installed on VM running 24x7 and waiting for your builds.
* **Pristine environment on every build** - build VMs are provisioned from an image and never reused for the consequent builds. You get pristine environment for every build. "Self-hosted agent" runs builds directly on the host operating system thus pulluting it with leftovers.
* **Multiple parallelism**. A single build can be run on hudreds of VMs simoultanously for a shorter period of time. Say, you have a suite with 1,000 tests and it takes 1 hour on a single core to run them all. You can run the suite on 10 single-core VMs in parallel and reduce the overall test time to 6 minutes by paying the same amount to the cloud provider!

## Included in all accounts for free

We are excited to announce that we've added **5 self-hosted jobs to all free and paid accounts**.

## Getting started

To configure your own build environment select **Self-hosted jobs** in the top menu and click **Add cloud**. There is a wizard that will guide you through the process and give you commands based on your selection that should be run on your computer.

Read more about BYOC in our [docs](/docs/byoc/). Give it a try and let us know what you think!

Best regards,<br>
AppVeyor team