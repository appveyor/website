---
title: AppVeyor Server for Mac
---

Since AppVeyor Server release in May we've received great feedback and recognized huge demand for AppVeyor for Mac.

Today we are proud to announce the availability of AppVeyor Server (on-premise) for Mac, free for unlimited projects, builds, clouds and agents. This officially makes
AppVeyor the only CI/CD solution that offers both hosted service and multi-platform on-premise installation.

AppVeyor Server for Mac enables to run CI/CD workflows on macOS directly or inside Docker containers.
It can be installed on your own Mac or a Mac hosted in a cloud (like [MacStadium](https://www.macstadium.com/) or [macincloud](https://www.macincloud.com/)) as a build server for your entire team. When installed on cloud-hosted Mac you can also run Windows and Linux builds on Azure, GCP or AWS.

## Installation and getting started

AppVeyor Server for Mac can be installed in seconds with [Homebrew](https://brew.sh/):

    brew tap appveyor/brew
    brew install appveyor-server
    brew services start appveyor-server

Once AppVeyor service is started open `http://localhost:8050` in your browser to continue with AppVeyor setup.

[Follow this guide](/docs/server/#running-builds) to get your builds up and running on your Mac.

Install AppVeyor Server for Mac today and let us know what you think!

## What's next

A new **Bring Your Own Cloud (BYOC)** feature is coming soon which will allow you connecting Mac to a hosted AppVeyor account or AppVeyor Server running on any platform. Imagine AppVeyor Server running on cheap $10 DigitalOcean Linux VM and running builds on your team Macs or Windows VMs provisioned on-demand on Azure!

We are also working on adding support for **Parallels Desktop, VMWare Fusion and VirtualBox** on Mac, so you'll be able to safely test against various configurations (different macOS releases, XCode versions, etc.) in virtualized environments.

Best regards,<br>
AppVeyor team