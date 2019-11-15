---
title: 'Run your tests on Ubuntu 18.04 (Bionic Beaver)'
---

Great news for AppVeyor customers who are either looking for a reason to start testing, or who are already running their Linux builds on AppVeyor!

Today we are thrilled to announce the immediate availability of an Ubuntu 18.04 (Bionic Beaver) image for build worker VMs! AppVeyor is the only hosted CI currently offering a full, non-containerized Ubuntu 18.04!

[Ubuntu 18.04 Bionic Beaver](https://wiki.ubuntu.com/BionicBeaver/ReleaseNotes) is the latest LTS release of the Ubuntu operating system and many projects and companies have already started moving onto it, so it's crucial to make sure your software packages work as expected on this OS.

To enable building on Ubuntu 18.04 use `Ubuntu1804` in your `.appveyor.yml`:

    image: Ubuntu1804

The current `Ubuntu` image is still pointing to Ubuntu 16.04 (Xenial Xerus) and, additionally, you can use `Ubuntu1604` "alias" image to specifically target 16.04 since, in the future, when 18.04 gets more stable the `Ubuntu` image will point to the latest LTS.

Here you can find [the list of software pre-installed on both Ubuntu images](/docs/linux-images-software/).



Best regards,<br>
AppVeyor team
