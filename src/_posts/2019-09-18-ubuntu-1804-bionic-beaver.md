---
title: 'Run your tests on Ubuntu 18.04 (Bionic Beaver)'
---

Great news for AppVeyor customers already running their Linux builds on AppVeyor as well as those ones looking for a reason to start testing their Linux builds on AppVeyor!

Today we are thrilled to announce the immediate availability of Ubuntu 18.04 (Bionic Beaver) image for build worker VMs! AppVeyor is the only hosted CI currently offering full non-containerized Ubuntu 18.04!

[Ubuntu 18.04 Bionic Beaver](https://wiki.ubuntu.com/BionicBeaver/ReleaseNotes) is the latest LTS release of Ubuntu operating system and many projects and companies have already started moving onto it, so it's crucial to make sure your software packages work as expected on this OS.

To enable building on Ubuntu 18.04 use `ubuntu1804` in your `.appveyor.yml`:

    image: ubuntu1804

Current `ubuntu` image is still pointing to Ubuntu 16.04 (Xenial Xerus) and, additionally, you can use `ubuntu1604` "alias" image to specifically target 16.04 as in the future when 18.04 gets more stable `ubuntu` image will point to the latest LTS.

Here you can find [the list of software pre-installed on both Ubuntu images](/docs/linux-images-software/).



Best regards,<br>
AppVeyor team
