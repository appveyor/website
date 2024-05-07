---
layout: docs
title: AppVeyor Enterprise Installation Guide
---

<!-- markdownlint-disable MD022 MD032 -->
# AppVeyor Enterprise Installation Guide
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Prerequisites

* Windows Server 2012 R2 or Windows Server 2016 or Windows Server 2019
* .NET Framework 4.5.2

## Creating AppVeyor server

* [Creating AppVeyor build server in Azure](/docs/enterprise/creating-build-server-in-azure)

## Setting up the server

* Login via RDP into AppVeyor server.
* [Set PowerShell execution policy to unrestricted](https://github.com/appveyor/ci/blob/master/scripts/enterprise/enable_powershell_unrestricted.ps1).
* [Disable IE ESC](https://github.com/appveyor/ci/blob/master/scripts/enterprise/disable_ie_esc.ps1).

## Install SQL Server 2016 Express

Download SQL Server 2016 SP1 Express Edition: [download link](https://www.microsoft.com/en-us/download/details.aspx?id=54284)

* Select "Custom" installation type.
* On "Feature selection" step select these features only:
    * Database Engine Services
    * Client Tools Connvectivity
* On "Database engine configuration" step:
    * Select Mixed mode
    * Add local "Administrators" group to "Specify SQL Server administrators"
* Install SSMS 17.x (optional): [download link](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
* Delete `C:\SQLServer2016Media` directory.

## Install IIS

Login to the server via RDP, open command prompt and run the following commands:

```cmd
dism /Online /Enable-Feature /FeatureName:IIS-WebServer /FeatureName:IIS-WebServerManagementTools /FeatureName:IIS-WebServerRole /FeatureName:IIS-ManagementConsole /FeatureName:IIS-ApplicationDevelopment /FeatureName:IIS-ASPNET /FeatureName:IIS-ASPNET45 /FeatureName:IIS-NetFxExtensibility /FeatureName:IIS-NetFxExtensibility45 /FeatureName:NetFx4Extended-ASPNET45 /FeatureName:IIS-CommonHttpFeatures /FeatureName:IIS-DefaultDocument /FeatureName:IIS-HealthAndDiagnostics /FeatureName:IIS-HttpLogging /FeatureName:IIS-LoggingLibraries /FeatureName:IIS-RequestMonitor /FeatureName:IIS-HttpCompressionStatic /FeatureName:IIS-HttpErrors /FeatureName:IIS-StaticContent /FeatureName:IIS-ISAPIExtensions /FeatureName:IIS-ISAPIFilter /FeatureName:IIS-WebSockets /FeatureName:IIS-RequestFiltering /FeatureName:IIS-Performance /FeatureName:IIS-Security /All
dism /Online /Enable-Feature /FeatureName:IIS-ASPNET45 /All
```


* Run `Get-WindowsFeature` and make sure ASP.NET 4.5 and WebSockets are enabled.
* Make sure `localhost` is opening.

## Install AppVeyor

* Login to `https://ci.appveyor.com` and click **Enterprise** in top menu. On that page you can see
  AppVeyor Enterprise license that is required during installation.
* Grab `Installation URL` and run it in PowerShell console on AppVeyor server.
* You will be walked through the installation wizard.
* Exit your RDP session, open browser and navigate to `http://<server-ip-address>` to complete AppVeyor
  installation.

## Setup Domain Name

At the website of our domain name provider (e.g. go daddy etc):

* Add an 'A Record' for your static IP address from above, and map it to: `ci.yourcompany.com` (in this example).

It may take minutes or hours for DNS records to replicate around the globe before you will see the changes.
When it does, you should be able to point your browser to: `http://ci.yourcompany.com`, and you will see the familiar start page of IIS Server on your CI Server!

## Setup SSL Certificate

If you already have an SSL certificate that will work for your custom domain (i.e. ci.mycompany.com) you can go to the "Installing existing SSL Certificate" section
for certificate importing instructions.

If you don't have a SSL cert yet, you will need to create one. There are a few options available:

1. You can create a self-signed certificate - it will do the job by encrypting the traffic between AppVeyor VM and your browser, but it won't be trusted
   and you'll be seeing a security warning in the browser all the time. We are not going to review this method here.
2. Getting free [Let's Encrypt](https://letsencrypt.org/) certificate.
3. Purchasing SSL certificate from commercial CA.

### Getting free Let’s Encrypt SSL Certificate

[Let's Encrypt](https://letsencrypt.org/) is a **free**, **automated**, and **open** Certificate Authority.

You can get a free certificate from "Let's Encrypt" Certificate Authority (CA) - it's an absolutely functional certificate and trusted by all major browser.
The only "gotcha" is that it's issued for 60 days only and after that it must be renewed. Fortunately, there is a tool that automates all the steps from
requesting and installing a new certificate on an IIS website to its automatic renewal upon expiration.

[letsencrypt-win-simple](https://github.com/Lone-Coder/letsencrypt-win-simple) is an open-source tool written in .NET and working with Let's Encrypt API (so called ACME client).
The tool automates provisioning of SSL certificates to IIS web sites.

**IMPORTANT NOTE:** The tool requires that the website you are going to install certificate on should have a binding with **host header** set to your domain name configured in the previous step. By default, "Default Web Site" in IIS has "all IPs" on port 80 binding without a host header.

To add a binding with host header open **IIS Manager** and expand server node, then **Sites**.

Select **Default Web Site** and click **Bindings...** on the right action pane. Click **Add...** and enter:

* Type: `http`
* IP address: `All unassigned`
* Port: `80`
* Host name: `ci.yourcompany.com`

Now the site is ready for SSL certificate installation with the **letsencrypt-win-simple** tool.

Just [follow the simple usage instructions for letsencrypt-win-simple tool](https://github.com/Lone-Coder/letsencrypt-win-simple/wiki/Basic-Usage#steps) and enjoy the results by opening `https://ci.yourcompany.com` in your browser and seeing IIS start page!

### Purchasing SSL Certificate

To purchase certificate from a certificate authority (CA) you will first need to create a special 'Certificate Signing Request' (CSR), which is a process that creates a private key and generates a bit of text that describes your certificate.

There are many tools available on the internet that can do that for you, but your VM has an easy way to create one for you too which relieves you from having to worry about the private key.

This process is outlined in detail below.

RDP into your VM again.
In the VM, open the IIS Manager (hit the Windows Start button, and type IIS Manager)

In IIS Manager:

* Select the server node in the left pane.
* In the  right pane, open the tile **Server Certificates**.
* On the far right pane, you now see a link to **Create Certificate Request**.
* Click that link, and fill out the form.

For example:

* Common Name: `ci.mycompany.com` (or whatever your custom domain is)
* Organization: `<Your Company Name>`
* Organizational Unit: `<Your Unit>`
* City/locality: `<Your City>`
* State/province: `<Your State>`
* Country/region: `<Your Country>`

Click **Next**.

Cryptographic Service Provider: `Microsoft RSA SChannel Cryptographic Provider`
Bit length: `2048`

Save to a file on your desktop (e.g. `MyCSR.txt`).

Now you can open the CSR file in notepad, and copy the contents to the clipboard.

Next, you will need to exit the VM, and obtain an SSL certificate from your SSL certificate provider. There are many providers on the internet.

Many SSL certificate sites where you will obtain your SSL certificate will expect you to either provide the CSR file or paste the contents into their site. DO NOT generate a new CSR, use the one you just created.

Once your certificate provider has created an SSL certificate for you, you will need to download the certificate in the `*.cer` or `*.crt` format (IIS may support others too).

Once you have that file, then RDP back into your CI Server and complete the certificate completion process.

In IIS Manager:

* Select the server node in the left pane.
* In right pane, open the tile **Server Certificates**.
* On the far right pane, you now see a link to **Complete Certificate Request**.
* Click that link, and complete the form.

For example:

* File name: `<your *.cer or *.crt file>`
* Friendly name: `<e.g. mycompany.com>`
* Certificate Store: `Personal`

Then select the 'Default Web Site' site in the left pane of IIS Manager.

* Click **Bindings** link.
* Click **Add**.


* Type: `https`
* IP address: `All Unassigned`
* Port `443`
* SSL certificate: `<Your Newly installed Cert>`

Close IIS Manager, and exit your RDP session.

Now you can point your browser to: `https://ci.yourcompany.com` and see the familiar start page of IIS Server on your CI Server!

Your Azure VM and network are now all set to install AppVeyor Enterprise.

### Installing existing SSL Certificate

If you already have an SSL certificate for your custom domain, you are ready to install it into your CI Server.

You are going to need a certificate in the file format \*.pfx, that includes the private key. If you don't have that file, you can generate it using tools like OpenSSL, but you will need the private key in another format. There are plenty of articles on the internet to show you how to do that.

RDP into your CI Server.

Copy the `*.pfx` file onto your server. There are several ways you can do this, including sharing your C drive with your host machine in the RDP settings. Or even using a service like Dropbox. As a general security practice, a `*.pfx` file is not something you want to leave lying around for attackers to obtain.

Open Windows Explorer, and double click on the `*.pfx` file.

* Store Location: `Local Machine`
* File name: `<location of your \*.pfx file>`
* Password: `<the password that was used to create your \*.pfx file>`
* Certificate Store: Automatically select the certificate store based on the type of certificate

The certificate will now be installed on the machine.

Open the IIS Manager (hit the Windows Start button, and type IIS Manager).

In IIS Manager:

Select the 'Default Web Site' site in the left pane of IIS Manager.

* Click **Bindings** link.
* Click **Add** button.


* Type: `https`
* IP address: `All Unassigned`
* Port `443`
* SSL certificate: `<Your Newly installed Cert>`

Close IIS Manager, and exit your RDP session.

Now you can point your browser to `https://ci.yourcompany.com` and see the AppVeyor CI login page!

Login to AppVeyor and go to **Account menu &rarr; Settings &rarr; General**.
Change **Application public URL** to `https://ci.yourcompany.com` and enable **Enforce secure HTTPS connections**.
