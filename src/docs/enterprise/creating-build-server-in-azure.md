---
layout: docs
title: Creating Build Server on Azure VM
---

<!-- markdownlint-disable MD022 MD032 -->
# Creating Build Server on Azure VM
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

In Azure you can create build server on an Virtual Machine, hosted at your own domain, secured with an SSL certificate.

There is a little networking to setup, and it can all be done in the Azure Portal.

You are going to create a new VM, install the bare essentials, configure your SSL certificate, and then setup networking.
Then you will be ready to install AppVeyor Enterprise software.

These instructions assume you want to host your CI server at your own custom domain: `https://ci.mycompany.com`.
Of course, you can customize these instructions for your own preferences.

## Prerequisites

* An Azure subscription
* A custom domain (e.g. mycompany.com), and access to create 'A records'
* An SSL certificate provider, and access to create a new SSL certificate, or access to a SSL certificate file (\*.pfx) containing the private key.

## Create Azure Virtual Machine

In the Azure Portal:

* Create a new 'Compute' resource
* Select the 'Windows Server 2012 R2 DataCenter' template

Fill out the form:

* Name: ci-yourcompany
* Disk: SSD
* Username: ci-username
* Password: generate a password and make a note of it
* Resource Group: ci-mycompany (or similar, or in an existing resource group)
* Location: `<YourAzureRegion>`
* Size: (e.g D2_V2 with at least 2-cores, this is not critical at this stage, and can be changed later)
* use all other defaults

The new VM machine will be created, along with a bunch of other networking resources, all contained in the resource group you named above.

## Setup a Public Static IP Address

By default, when you create your VM, it will assigned a dynamic Public IP address.

This is helpful for getting started quickly, but if you ever shut down your CI server VM, the chances are it will start up with a different public IP address each time, forcing you to change your DNS records for your custom domain (coming later).

Next, we are going to associate a static IP address to your CI server so that you have a fixed IP address to bind your custom domain to.

In the Azure Portal

Edit the 'Public IP Address' resource created for the VM above, found in the same resource group as your VM.
(should be called 'ci-yourcompany-ip' in this example)

* Click on the IP address listed in the properties of the VM
* In the Overview blade, 'Dissociate' the IP address
* In Configuration blade, change from 'Dynamic' to 'Static'
* In the Overview blade, 'Associate' the IP address again.

This will assign you a new static IP address. Make a note of it e.g. `51.255.53.185` (yours will be different)

## Install IIS on VM

Now that you have a running VM, and public IP address for it, you can setup SSL for your CI Server.

In the Azure Portal

* Select your new VM, and click 'Connect' to RDP into your machine.
* Enter your username and password from steps above.

Once logged in, open command prompt and run the following commands:

```cmd
dism /Online /Enable-Feature /FeatureName:IIS-WebServer /FeatureName:IIS-WebServerManagementTools /FeatureName:IIS-WebServerRole /FeatureName:IIS-ManagementConsole /FeatureName:IIS-ApplicationDevelopment /FeatureName:IIS-ASPNET /FeatureName:IIS-ASPNET45 /FeatureName:IIS-NetFxExtensibility /FeatureName:IIS-NetFxExtensibility45 /FeatureName:NetFx4Extended-ASPNET45 /FeatureName:IIS-CommonHttpFeatures /FeatureName:IIS-DefaultDocument /FeatureName:IIS-HealthAndDiagnostics /FeatureName:IIS-HttpLogging /FeatureName:IIS-LoggingLibraries /FeatureName:IIS-RequestMonitor /FeatureName:IIS-HttpCompressionStatic /FeatureName:IIS-HttpErrors /FeatureName:IIS-StaticContent /FeatureName:IIS-ISAPIExtensions /FeatureName:IIS-ISAPIFilter /FeatureName:IIS-WebSockets /FeatureName:IIS-RequestFiltering /FeatureName:IIS-Performance /FeatureName:IIS-Security /All
dism /Online /Enable-Feature /FeatureName:IIS-ASPNET45 /All
```

## Setup Network Ports

You now need open the ports 80 and 443 in the network of your VM.

In the Azure Portal

Edit the 'Network security group' resource that was created for the VM above, found in the same resource group as your VM.
(should be called 'ci-yourcompany-nsg' in this example)

* Select 'Inbound security rules'

You should see the TCP/3389 is already configured for RDP access to your VM.

Add a new rule:

* Name: http
* Port range: 80
* Action: Allow

Add another rule:

* Name: https
* Port range: 443
* Action: Allow

Now, you can point your browser to, the public IP of your VM, from before: `http://<yourstaticipaddress>`
and see the familiar start page of IIS Server on your CI Server!

Your Azure VM and network are now all set to install AppVeyor Enterprise.
