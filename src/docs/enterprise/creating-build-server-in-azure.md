---
layout: docs
title: Creating Build Server on Azure VM
---

# Creating Build Server on Azure VM

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

## Install IIS on VM:

Now that you have a running VM, and public IP address for it, you can setup SSL for your CI Server.

In the Azure Portal

* Select your new VM, and click 'Connect' to RDP into your machine.
* Enter your username and password from steps above.

Once logged in, open the Powershell console, and type:
`Install-WindowsFeature -name Web-Server -IncludeManagementTools`

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

Now, you can point your browser to, the public IP of your VM, from before:  `http://<yourstaticipaddress>`

## Setup Domain Name

At the website of our domain name provider (e.g. go daddy etc)

Add an 'A Record' for your static IP address from above, and map it to: `ci.yourcompany.com` (in this example)

It may take minutes or hours for DNS records to replicate around the globe before you will see the changes.
When it does, you should be able to point your browser to: `http://ci.yourcompany.com`, and you will see the familar start page of IIS Server on your CI Server!

## Setup SSL Certificate

If you already have an SSL certificate that will work for your custom domain (i.e. ci.mycompany.com) you can skip the next step that creates the SSL certificate, and go straight to the step after to install your SSL certificate.

If you don't have a SSL cert yet, you will need to create one. You will first need to create a special 'Certificate Signing Request' (CSR), which is a process that creates a private key and generates a bit of text that describes your certificate.

There are many tools available on the internet that can do that for, but your VM has an easy way to create one for you too, that avoids you worrying about the private key.

This process is outlined in detail below.

### Creating SSL Certificate

RDP into your VM again.
In the VM, open the IIS Manager (hit the Windows Start button, and type IIS Manager)

In IIS Manager

* select the server node in the left pane
* inthe  right pane, open the tile 'Server Certificates'
* on the far right pane, you now see a link to 'Create Certificate Request'
* click that link, and fill out the form.

For example:

* Common Name: ci.mycompany.com (or whatever your custom domain is)
* Organization: `<Your Company Name>`
* Organizational Unit: `<Your Unit>`
* City/locality: `<Your City>`
* State/province: `<Your State>`
* Country/region: `<Your Country>`

Click Next.

Cryptographic Service Provider: Microsoft RSA SChannel Cryptographic Provider
Bit length: 2048

Save to a file on your desktop. (e.g. `MyCSR.txt`)

Now you can open the CSR file in notepad, and copy the contents to the clipboard.

Next, you will need to go exit the VM, and obtain an SSL certificate from your SSL certificate provider. There are many providers on the internet.

Many SSL certificate sites where you will obtain your SSL certificate will expect you to either provide the CSR file or the paste the contents into their site. DO NOT generate a new CSR, use the one you just created.

Once your certificate provider has created you an SSL certificate, you need to download the certificate in the \*.cer or \*.crt format. (IIS may support others too)

Once you have that file, then RDP back into your CI Server and complete the certificate completion process.

In IIS Manager

* select the server node in the left pane
* in right pane, open the tile 'Server Certificates'.
* on the far right pane, you now see a link to 'Complete Certificate Request'
* click that link, and complete the form.

For example:

* File name: `<your *.cer or *.crt file>`
* Friendly name: `<e.g. mycompany.com>`
* Certificate Store: Personal

Then select the 'Default Web Site' site in the left pane of IIS Manager.

* Click the 'Bindings' link.
* Click 'Add'


* Type: https
* IP address: All Unassigned
* Port 443
* SSL certificate: `<Your Newly installed Cert>`

Close IIS Manager, and exit your RDP session.

Now you can point your browser to: `https://ci.yourcompany.com` and see the familiar start page of IIS Server on your CI Server!

Your Azure VM and network are now all set to install AppVeyor Enterprise.

### Installing existing SSL Certificate

If you already have an SSL certificate for your custom domain, you are ready to install it into your CI Server.

You are going to need a certificate in the file format \*.pfx, that includes the private key. If you don't have that file, you can generate it using tools like OpenSSL, but you will need the private key in another format. There are plenty of articles on the internet to show you how to do that.

RDP into your CI Server

Copy the \*.pfx file onto your server. (There are several ways you can do this, including Sharing your C drive with your host macchine in the RDP settings. Or even using a service like dropbox). (As a general security practice, a \*.pfx file is not somethign you want to  leave lying around the place for attackers to obtain)

Open Windows Explorer, and double click on the \*.pfx file.

Store Location: Local Machine
File name: <location of your \*.pfx file>
Password: <the password that was used to create your \*.pfx file>
Certificate Store: Automatically select the certificate store based on the type of certificate

The certificate will now be installed on the machine.

Open the IIS Manager (hit the Windows Start button, and type IIS Manager)

In IIS Manager

Select the 'Default Web Site' site in the left pane of IIS Manager.

* Click the 'Bindings' link.
* Click 'Add'


* Type: https
* IP address: All Unassigned
* Port 443
* SSL certificate: `<Your Newly installed Cert>`

Close IIS Manager, and exit your RDP session.

Now you can point your browser to: `https://ci.yourcompany.com` and see the familiar start page of IIS Server on your CI Server!

Your Azure VM and network are now all set to install AppVeyor Enterprise.
