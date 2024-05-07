---
layout: docs
title: AppVeyor Enterprise Planning Guide
---

<!-- markdownlint-disable MD022 MD032 -->
# AppVeyor Enterprise Planning Guide
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Prerequisites summary

TLDR; of what you need for running your builds with AppVeyor Enterprise:

* The server for running AppVeyor Enterprise:
    * 2 CPU cores, 4 GB of RAM and SSD disk storage
    * Windows Server 2012 R2 or Windows Server 2016
    * Dedicated IP for SSL and (sub-)domain mapping (e.g. an SSL cert for `https://ci.mycompany.com`
* For provisioning builds worker VMs in a cloud: Azure, GCE or AWS account/subscription
* For running builds on your premises: Windows server with Hyper-V or Docker
* SSL certificate in the form that can be imported in Windows
* Sub-domain or domain with DNS record mapped to IP of the server with AppVeyor Enterprise
* [Mailgun](https://www.mailgun.com/), [SendGrid](https://sendgrid.com/) or SMTP account for sending email build notifications

## Where to install AppVeyor Enterprise

You should decide where AppVeyor Enterprise will be installed.
This could be a server on Azure, Google Cloud Engine, Amazon or any other cloud, dedicated server or a machine under your desk.
As a minimal configuration for "AppVeyor" machine we recommend at least 2 CPU cores, 4 GB of RAM and SSD storage.
The machine will run IIS, SQL Server Express 2016, Microsoft Service Bus, Redis and AppVeyor's Web and Worker roles.
Server operating system must be Windows Server 2012 R2 or Windows Server 2016.
Builds will be run on VMs in Azure, GCE or Amazon.

If you are planning to secure AppVeyor web with SSL the server should have a dedicated IP assigned and (sub-)domain mapped in DNS to that IP. This is standard stuff for having a custom domain, and assigning DNS records to its IP address.

## Where to run builds

You should decide where your builds will be run.

Currently, AppVeyor can provision "stateless" build worker VMs in the following clouds:

* Microsoft Azure
* Google Compute Engine (GCE)
* Amazon EC2 (AWS)
* Hyper-V (bare-metal or nested virtualization)
* Docker with Windows Server 2016 containers.

The beauty of this approach is you always get a clean VM for every build - this is what you get today with public AppVeyor cloud.

Alternatively, you can have process-level isolation where each build job runs in its own system process on a single "stateful" server.
This approach might be useful in some rare scenarios, but we don't recommend it and this is not exactly what was AppVeyor created for :)

We recommend Azure as the most Windows-friendly cloud. Also, Azure has per-minute billing which is good if you have many short builds.
GCE also utilizes per-minute billing, but the minimum billable amount for a provisioned VM is 10 minutes, so if your average build time is more than 10 minutes GCE is a great option too. AWS has hourly billing - that works for really lengthly builds and those companies with investment in infratstructure already running on AWS.

If you plan to run multiple build jobs in parallel make sure your cloud subscription has a large enough quota for the number of CPU cores of the size of VM you plan to run them on. By default, in most Azure subscriptions quotas are not very high.

For example, if you are running your builds on VMs in Azure, and lets say you want to run a maximum of 10x build jobs and you select a VM size of 'Standard_DS2_V2' (each with 2 CPU cores) to run each job. Then you will need to have _at least_ 20x 'Standard DSv2 Family Cores' in your Azure quota (in your selected region). Also, you may want to ensure you have 10x Public IP addresses.

Azure quotas for cores of any particular family are pretty small by default (usually about 20x) so be sure to check your quotas before you run a build, otherwise your jobs will never start. Some regions won't permit large quotas, so it pays to check first when choosing the VM size you want. Azure core quota increases may take a few days to obtain.

## Where to store build artifacts

Every build produces artifacts such as a build log, test results, zip archives and other files. AppVeyor must store them somewhere.

The following options available:

* Local file system on AppVeyor server
* Azure Blob storage
* Google storage
* Amazon S3

We strongly encourage users to store artifacts on cloud storage.

## SSL certificate

It is recommended to secure access to AppVeyor web application with SSL. Self-signed certificates are not recommended as some AppVeyor components won't be able to communicate with untrusted SSL certificates.

If you already have SSL certificate for AppVeyor web endpoint then you should import it to the server in some of the supported formats (e.g. `.pfx` file) as described in [multiple articles on the internet](https://www.google.com/search?q=windows+server+import+certificate).

If you don't already have an SSL certificate for your custom domain, you may want to create a 'Certificate Signing Request' on the IIS server on your build server. Then obtain your certificate, and then add HTTPS binding with your certificate to "Default Web Site" website. AppVeyor installer will be using "Default Web Site" for its Web role.

## Sending email notifications

AppVeyor can be configured for sending email notifications using one of the following:

* SMTP
* [Mailgun](https://www.mailgun.com/)
* [SendGrid](https://sendgrid.com/)

Both Mailgun and SendGrid have free options with quite reasonable sending limits. Though some work may be required to setup a real mail domain with them to avoid trapping messages in spam folder.

