---
layout: docs
title: Running builds on Azure
---

<!-- markdownlint-disable MD022 MD032 -->
# Running builds on Azure
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Introduction

During the following steps you will need to collect a number of configuration and authentication settings to configure Azure builds in AppVeyor. For better productivity we recommend making some deployment notes (it can be just a notepad file in **secure** location or password manager section) with the following fields:

* AAD application name
* Client ID
* Client Secret
* Tenant ID
* Subscription ID
* VMs resource group
* Location
* Disk storage account name
* Security group name
* Virtual network name

## Create Azure Active Directory (AAD) service account

Now, let's start from creating AAD application.

In [Azure Portal](https://portal.azure.com) navigate to **Azure Active Directory** blade.

Select **App registrations**.

Click **New application registration**.

Specify application details:

* Name: `appveyor-enterprise` (this is **AAD application name** in your notes)
* Application type: `Web app/API`
* Sign-on URL: `http://appveyor-enterprise`

On application details page copy **Application ID** and save it as **Client ID** in your notes.

Open **Keys** page and add a new key:

* Description: `appveyor-enterprise-key`
* Expires: `Never expires` (or whatever is suggested by your company policy)

Save and copy generated key value - this is **Client secret** in your notes.

To find your **Tenant ID** navigate back to **Azure Active Directory** blade.

Select **Properties**.

Copy **Directory ID** and save it as **Tenant ID** in your notes.

Finally, to get **Subscription ID** navigate to **Subscription** blade.

After this step you should have the following values:

* AAD application name
* Client ID
* Client secret
* Tenant ID
* Subscription ID

Related materials:

* [Create an Azure Active Directory application](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal#create-an-azure-active-directory-application)

## Create Resource Group

Login to [Azure Portal](https://portal.azure.com)

Select **Resource groups**. You may need to go to **More services** in the bottom of the left menu and type **Resource groups** in Filter.

Click **Add** to create new resource groups. Name it consistently with what was chosen for AAD application, for example `appveyor-enterprise`.

Save resource group name in your deployment notes as **VMs resource group**.

To assign AAD application created above to the resource group as a Contributor open resource group details blade and select **Access control (IAM)**.

Click **Add** button to add new permissions:

* Role: `Contributor`
* Select: `appveyor-enterprise` (this is AAD application name)

Make sure the application was found.

Click **Save**.

Now AppVeyor application will be able to create/modify/delete Azure resources in `appveyor-enterprise` resource group only.


## Create Storage Account

Azure storage account will be used for storing master and build worker VM disks and, optionally, build artifacts.

Login to Azure Portal and select **Storage accounts** (you may need to go to **More services** in the bottom of the left menu and type **Storage accounts** in Filter).

Click **Add** to open **Create storage account** blade. Enter the following details (only important settings described, please feel free to leave default value for others):

* Name: `<yourcompany>appveyorenterprise` - the name must be unique across Azure. Save this name as **Disk storage account name** in your notes.
* Deployment model: `Resource manager`.
* Performance: `Premium` - recommended if you are going to use **DS** VM series to run your builds.
* Replication: `Locally-redundant storage (LRS)`
* Secure transfer required: `Disabled`
* Resource group: Use existing - `appveyor-enterprise`
* Location: Choose whatever is closer to services your builds might depend on. Save selected location as **Location** in your notes.


## Create Master VM

*Master* VM is used as a template for creating new build worker VMs. On that VM you install all software required by AppVeyor and any additional software required by your builds. You do a snapshot (copy) of VM's OS disk and then us it as a template (*image* in AppVeyor terms) to provision new build worker VMs.

Please note, that in this document we use **Windows Server 2012 R2 Datacenter** OS as the most popular and most tested with AppVeyor. However, AppVeyor supports both **Windows Server 2012 R2** with .NET framework 4.5 installed and **Windows Server 2016**.

Login to Azure Portal and navigate to **Virtual machines** blade.

Click **Add** and on **Compute** blade select **Windows Server**.

Click **Windows Server 2012 R2 Datacenter**:

* Select a deployment model: `Resource Manager`
* Click **Create** button.

On **Basics** step:

* Name: `appveyor-master`
* VM disk type: `SSD`
* User name: `appveyor`
* Password: `<your password>`
* Subscription: `<your subscription>`
* Resource group: Use existing - `appveyor-enterprise`
* Location: `<the same location as for storage account>`

Click **OK**.

On **Size** step:

* We recommend at least `DS2_V2`, but you are free to select any size working for your workloads.
* Click **Select**.

On **Settings** step:

* Availability set: `None`
* Storage:
    * Use managed disks: `No`
    * Storage account: `<storage account name from created above>`
* Network:
    * Virtual network: `appveyor-enterprise-vnet` (save in your notes as **Virtual network name**)
    * Subnet: `default (10.0.0.0/24)`
    * Public IP address: leave default
    * Network security group: Create new - `appveyor-enterprise-nsg` (save in your notes as **Security group name**)
* Extensions: `No extensions`
* Auto-shutdown: `Off`
* Monitoring:
    * Boot diagnostics: `Enabled`
    * Guest OS diagnostics: `Disabled`
* Diagnostics storage account: leave default

On **Purchase** step:

* Validate VM details and click **Purchase** button.

Wait unti VM is deployed and click **Connect** button to download RDP file. Verify that you can connect via RDP with **User Name** and **Password** created earlier in this section.



## Setup Master VM

Login into master VM via RDP.

Follow [these steps](/docs/server/setup-master-vm/) to configure VM and install software required for your build process. It is tested PowerShell scripts which can be simply copy-pasted to PowerShell window (started in privileged mode). Specifically:

* [Basic configuration](/docs/server/setup-master-vm/#basic-configuration) and [Essential 3rd-party software](/docs/enterprise/setup-master-vm/#essential-3rd-party-software) - we strongly recommend to install everything from those sections.
* [Build framework](/docs/server/setup-master-vm/#build-framework) - you can skip one of MSBuild and Visual Studio versions or both if you don't need them.
* [Test framework](/docs/server/setup-master-vm/#test-framework) - you can skip that step if you are running your own custom test script/framework.
* [AppVeyor Build Agent](/docs/server/setup-master-vm/#appveyor-build-agent) and [Configuring agent to run in "Interactive" mode](/docs/enterprise/setup-master-vm/#configuring-agent-to-run-in-interactive-mode) - these steps are mandatory.

Install any additional software required for your builds.

Do not sysprep master VM!

Shutdown VM from either RDP session or **Stop** it from Azure Portal (this will fully deallocate its resources and avoiding excessive charges).


## Create an image from Master VHD

In Azure Portal open VM details blade and select **Disks**.

Click OS disk details and copy its **VHD URI**.

Return back to VM details and click **Delete** to delete VM. While VM will be deleted its OS disk's VHD will stay intact. You should delete VM to release a "lease" from OS disk's VHD, so we can copy it.

Now, when VM is completely deleted let's copy its OS disk's VHD to a different location thus making a snapshot or *image*.

To run the following script you should [install Azure PowerShell](https://docs.microsoft.com/en-us/powershell/azure/install-azurerm-ps?view=azurermps-4.3.1) (that article suggests installing Azure PowerShell via PowerShell Gallery, however, alternatively, you can install Azure PowerShell modules with MSI from [Azure PowerShell releases](https://github.com/Azure/azure-powershell/releases) page).

Next, open PowerShell ISE console and run the [script creating destination container and copying master image into it](https://github.com/appveyor/ci/blob/master/scripts/enterprise/copy-master-image-azure.ps1) [[raw](https://raw.githubusercontent.com/appveyor/ci/master/scripts/enterprise/copy-master-image-azure.ps1)]. You will be prompted for source VHD UI, source and destination account credentials.

To get storage account access key navigate to **Storage accounts** blade then click storage details.

On storage details screen select **Access keys** and copy **key1** value.


## Add build cloud in AppVeyor

Login to AppVeyor web console and navigate to **Account Settings menu &rarr; Build environment** page.

Click **Add cloud** and select cloud type **Azure**.

Fill the following mandatory settings:

* **Name**: Name for your private build cloud. Make it meaningful and short to be able to use in YAML configuration
* **Client ID**: *Client ID* from deployment notes
* **Client secret**: *Client Secret* from deployment notes
* **Tenant ID**: *Tenant ID* from deployment notes
* **Subscription ID**: *Subscription ID* from deployment notes
* **VMs resource group**: *VMs resource group* from deployment notes
* **Location**: Select the same location as *Location* from deployment notes (storage account location)
* **Machine size**: Select **DS** series or better (assuming you selected **premium performance** when created storage account)
* **Disk storage account name**: *Disk storage account name* from deployment notes
* **Disk storage container**: Storage container where VMs will be created, for example **build-vms**
* **Virtual network name**: Name of auto-created VNET for your VM resource group. It should look like `<vm_resource_group>-vnet`. Please open Azure Portal, navigate resource of type **Virtual Network** and ensure that name you entered is correct.
* **Subnet name**: In Azure Portal under **Virtual Networks** navigate to **Subnets** and copy-paste subnet name. Usually it is **default**. Optionally you can create separate subnet for AppVeyor build VMs.
* **Security group name**: *Security group name* from deployment notes
* Images
    * **IMAGE NAME**: Image name as you want to see it in AppVeyor UI and YAML, for example **VS2013 with WMF3**
    * **VHD BLOB PATH** Path to master VHD within storage account without storage account name (**images/master2017-01-05.vhd** in our example)
* Expand **Failure strategy** section and set the following (needed to overcome Azure VM provisioning latency):
    * **Job start timeout, seconds**: 1200 (wait for 20 minutes for Azure VM to be created) or more
    * **Provisioning attempts**: 2 or more


## Add build worker image in AppVeyor

In AppVeyor web console navigate to **Account Settings menu &rarr; Build environment &rarr; Build worker images**.

Click **Add image** and enter the following details:

* Image name: `<image name from steps above>`
* OS type: `Windows`
* Build cloud: `<cloud name from steps above>`



## Set default worker image and build cloud

Navigate to **Account Settings menu &rarr; Build environment**.

Update the following settings:

* Default build cloud: `<cloud name from steps above>`
* Default build worker image: `<image name from steps above>`

Click **Save**.


## Creating Master VM from existing VHD

To update Master VHD and create a new image for AppVeyor you should create a new Azure VM from existing VHD.

On your computer run [PowerShell script to create Azure VM from existing VHD](https://github.com/appveyor/ci/blob/master/scripts/enterprise/create_master_vm_from_vhd.ps1) ([raw](https://raw.githubusercontent.com/appveyor/ci/master/scripts/enterprise/create_master_vm_from_vhd.ps1)). This script depends on [Azure PowerShell cmdlets](https://docs.microsoft.com/en-us/powershell/azure/install-azurerm-ps?view=azurermps-4.3.1). You can copy all the data this script requires from Azure cloud details in AppVeyor.


## Routing build to your own cloud

At **project** level:

* **UI**:
    * **Settings** > **Environment** > **Build cloud**: Select your private build cloud name from drop-down
    * **Settings** > **Environment** > **Build worker image**: Select your build worker image from drop-down
* **YAML**:

```yaml
build_cloud: <private_build_cloud_name>
image: <private_build_cloud_image>
```

At **project** level:

* Set environment variable "APPVEYOR_BUILD_WORKER_CLOUD" to your private build cloud name
    * This assumes that default and custom build clouds have build worker image with the same name (for example **Visual Studio 2015**)
