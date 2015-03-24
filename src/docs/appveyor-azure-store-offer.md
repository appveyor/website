---
layout: docs
title: AppVeyor for Azure Store offer
---

# AppVeyor for Azure Store offer

## What does this offer include?

This Azure Store offer is a free "Continuous Integration in a box" solution for Windows developers. It includes AppVeyor CI installed on a single Azure virtual machine and enables running multiple builds in parallel. The virtual machine has pre-installed software to work with Git, Mercurial and Subversion repositories as well as build and test .NET projects. You can install additional software to support your own stack: Node.js, Ruby, Python, Java, etc.


## Installation notes

* For better results we recommend installing to at least "A2" instance.
* When virtual machine is provisioned and started login into it via RDP – AppVeyor installation script will be started in PowerShell console.
* Follow instructions of AppVeyor installation script. The script will install all AppVeyor dependencies and additional software. The time required to complete the installation depends on selected options and virtual machine instance type.
* When installation script is done open `http://<vm-name>.cloudapp.net` in the browser, create AppVeyor admin login and start building your projects!


## Troubleshooting

* When [contacting AppVeyor support](https://www.appveyor.com/support) we may ask you to provide AppVeyor logs. AppVeyor logs can be seen in "Event Viewer" under `Applications and Services Logs/AppVeyor`.


## High-availability or multi-agent configurations

AppVeyor supports high-availability scenarios running "stateless" builds on multiple Azure or Hyper-V VMs. Please [contact](mailto:team@appveyor.com) us to discuss your needs and get a quote.
