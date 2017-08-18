# Creating Build Server on Azure VM

In Azure you can create build server on an Virtual Machine, hosted at your own domain, secured with an SSL certificate.

There is a little networking to setup, and it can all be done in the Azure Portal.

You are going to create a new VM, install the bare essentials, configure your SSL certificate, and then setup networking. 
Then you will be ready to install AppVeyor Enterprise software. 

These instructions assume you want to host your CI server at your own custom domain: `https://ci.mycompany.com`. 
Of course, you can customize these instructions for your own preferences.

## Prerequisites

* An Azure subscription
* An SSL certificate provider

## Create Azure Virtual Machine
In the Azure Portal:

* Create a new 'Compute' resource
*Select 'Windows Server 2012 R2 DataCenter' template

Fill out the form:

* Name: ci-yourcompany
* Disk: SSD
* Username: ci-username
* Password: generate a password and make a note of it
* Resource Group: ci-mycompany (or similar, or in an existing resource group)
* Location: <YourAzureRegion>
* Size: (e.g D2_V2 with at least 2-cores, this is not critical at this stage, and can be changed later)
* use all other defaults

The new VM machine, and associated netoerk resources will be created in the resource group you named above.

## Setup a Public Static IP Address

By default, when you create your VM, it will assigned a dynamic Public IP address.

This is helpful for getting started quickly, but if you ever shut down your CI server VM, the chances are it will start up with a different public IP address each time, forcing you to change your DNS recrods for your SSL certificate.

Here we are going to associate a static IP address to your CI server, so you have a fixed IP address to bind your SSL certificate to.

In the Azure Portal

Edit the 'Public IP Address' resource created for the VM above, found in the same resource group as your VM.
(should be called 'ci-yourcompany-ip' in this example)

* Click on the IP address listed in the properties of the VM
* In the Overview blade, 'Dissociate' the IP address
* In Configuration blade, change from 'Dynamic' to 'Static'
* In the Overview blade, 'Associate' the IP address again.

This will assign you a new static IP address. Make a note of it e.g. `51.255.53.185` (your will be different)

## Install IIS on VM:

Now that you have a running VM, and pulic IP address for it, you can setup SSL for your CI Server.

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

Add an 'A Record' for your static IP address from above, and map it to: ci.yourcompany.com (for example)

It may take minutes or hours for DNS records to replicate around the globe before you will see the changes.
When it does, you should be able to point your borwser to: `http://ci.yourcompany.com`, and you will see the familar start page of IIS Server on your CI Server!

## Setup SSL Certificate

If you already have an SSL certificate that will work for your custom domain (i.e. ci.mycompany.com) you can skip this step.
If you dont have a SSL cert yet, you will need to create one. You will first need to create a Certificate Signing Request (CSR). 
There are many tools available on the internet to do that, but your VM has an easy way to create one for you too. 

RDP into your VM again.
In the VM, open the IIS Manager (hit the Windows Start button, and type IIS Manager)

In IIS Manager

* select the server node in the left pane
* inthe  right pane, open the tile 'Server Certificates'
* on the far right pane, you now see a link to 'Create Certificate Request'
* click that link, and fill out the form.

For example:
* Common Name: ci.mycompany.com (or whatever your custom domain is)
* Organization: <Your Company Name>
* Organizational Unit: <Your Unit>
* City/locality: <Your City>
* State/province: <Your State>
* Country/region: <Your Country>

Leave the other defaults.

Save to a file on your desktop.

Now you can open the CSR file in notepad, and copy the contents to the clipboard.

Now you need to go and obtain an SSL certificate from one of the many providers on the internet.
Many SSL certificate sites where you will obtain your SSL cert will expect you to provide either the CSR file or the paste the contents into their site.
In the process you will generate a private key, that you **must save**, and they will issue you an SSL certificate in one form or another.

The format you want from them is a *.cer or *.crt file. (IIS may support others)

Once you have that, then RDP back into your CI Server and complete the certificate completion process.

In IIS Manager

* select the server node in the left pane
* in right pane, open the tile 'Server Certificates'.
* on the far right pane, you now see a link to 'Complete Certificate Request'
* click that link, and complete the form.

For example:
* File name: <your *.cer or *.crt file>
* Friendly name: <e.g. mycompany.com>
* Certificate Store: Personal

Then select the 'Default Web Site' site in the left pane of IIS Manager.

* Click the 'Bindings' link.
* Click 'Add'


* Type: https
* IP address: All Unassigned
* Port 443
* SSL certificate: <Your Newly installed Cert>

Close IIS Manager, and exit your RDP session.

Now you can point your browser to: `https://ci.yourcompany.com` and see the familiar start page of IIS Server on your CI Server!

Your Azure VM and network are now all set to install AppVeyor Enterprise.
