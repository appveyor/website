---
layout: docs
title: Running builds on Azure cloud
---

<!-- markdownlint-disable MD022 MD032 -->
# Running builds on Azure cloud
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Introduction

During the following steps you will need to collect a number of configuration and authentication settings to configure Azure builds in AppVeyor. To avoid confusion we recommend doing some deployment notes (it can be just a notepad file in **secure** location or password manager section) with the following fields:

* AAD application name
* Client ID
* Client Secret
* Tenant ID
* Subscription ID
* VMs resource group
* Location
* Disk storage account name
* Security group name


## Configuring AD service accont

### Create Azure Active Directory application

Now, let's start from creating AAD application.

In [Azure Portal](https://portal.azure.com) navigate to **Azure Active Directory** blade.

Select **App registrations**.

Click **New application registration**.

Specify application details:

* Name: `appveyor-ci-enterprise` (this is **AAD application name** in your notes)
* Application type: `Web app/API`
* Sign-on URL: `http://appveyor-ci-enterprise`

On application details page copy **Application ID** and save it as **Client ID** in your notes.

Open **Keys** page and add a new key:

* Description: `appveyor-ci-enterprise`
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

## Create new Resource Group

Login to [Azure Portal](https://portal.azure.com)

Select **Resource groups**. You may need to go to **More services** in the bottom of the left menu and type **Resource groups** in Filter.

Press **Add** to create new resource groups. Name it consistently with what was chosen for AAD application, for example **appveyor-build-rg**.

Save resource group name in your deployment notes as **VMs resource group**.

### Assign principal (AAD application) to resource group as a Contributor

On resource group details blade select **Access control (IAM)**.

Click **Add** button to add new permissions:

* Role: `Contributor`
* Select: `appveyor-ci-enterprise` (this is AAD application name)

Make sure the application was found. Click **Save**.

**Now your Azure Active Directory application (which will be used by AppVeyor as identity) has Contributor permissions in resource group which will contain resources managed by AppVeyor!**


## Create new storage account

Azure storage account will be used for storing master and build worker VM disks and, optionally, build artifacts.

Login to Azure Portal and select **Storage accounts** (you may need to go to **More services** in the bottom of the left menu and type **Storage accounts** in Filter).

Click **Add** to open **Create storage account** blade. Enter the following details (only important settings described, please feel free to leave default value for others):

* Name: `<yourcompany>appveyorci` - the name must be unique across Azure. Save this name as **Disk storage account name** in your notes.
* Deployment model: `Resource manager`.
* Performance: `Premium` - recommended if you are going to use **DS** VM series to run your builds.
* Replication: `Locally-redundant storage (LRS)`
* Secure transfer required: `Disabled`
* Resource group: Use existing - `appveyor-build-rg`
* Location: Choose whatever is closer to services your builds might depend on. AppVeyor servers are located in West US, but it will work well with AppVeyor in any Azure region. Save selected location as **Location** in your notes.


## Create Master VM

*Master* VM is used as a template for creating new build worker VMs. On that VM you install all software required by AppVeyor and any additional software required by your builds. You do a snapshot (copy) of VM's OS disk and then us it as a template (*image* in AppVeyor terms) to provision new build worker VMs.

Please note, that in this document we use **Windows Server 2012 R2 Datacenter** OS as the most popular and most tested with AppVeyor. However, AppVeyor supports both **Windows Server 2012 R2** with .NET framework 4.5 installed and **Windows Server 2016**.

Login to Azure Portal and select **Virtual machines**.


* Ensure that you selected **Virtual machines** and not **Virtual machines (classic)**
* Press **Add**
* In **Compute** *blade* select **Windows Server**
* In **Windows Server** *blade* select **Windows Server 2012 R2 Datacenter**
* In **Windows Server 2012 R2 Datacenter** *blade* ensure that deployment model is **Resource Manager**, and press **Create**
* Enter Name consistent with selected naming, which still satisfies VM naming restrictions (like **appveyor-build** for our example)
    * We strongly recommend to leave **SSD** VM disk type
    * Enter **User Name** and **Password**
    * Resource group: Use existing and select resource group created before (**appveyor-build-rg** in our example)
    * Location: the same as selected for Storage Account in previous section
* Press **OK**
* In **Choose a size** *blade* select VM size. We recommend **DS** series or better.
* In **Settings** *blade*
    * Select storage account created earlier (**appveyorbuildsa** in our example)
    * Save Network security group (should look like **appveyor-build-nsg** in our example) in your deployment notes as **Security group name**
    * Leave other settings as they set by default and press **OK**
* After validation passed, press **OK** and wait while VM deployed
* After VM deployed, press **Connect** to download RDP file and ensure that you can connect over RDP with **User Name** and **Password** created earlier in this section
* Now you can close Azure Portal, it will not be needed for next couple of steps

## Setup Master VM

Login into master VM via RDP.

Follow [these steps](/docs/enterprise/setup-master-vm/) to configure VM and install software required for your build process. It is tested PowerShell scripts which can be simply copy-pasted to PowerShell window (started in privileged mode). Specifically:

* [Basic configuration](/docs/enterprise/setup-master-vm/#basic-configuration) and [Essential 3rd-party software](/docs/enterprise/setup-master-vm/#essential-3rd-party-software) - we strongly recommend to install everything from those sections. 
* [Build framework](/docs/enterprise/setup-master-vm/#build-framework) - you can skip one of MSBuild and Visual Studio versions or both if you don't need them.
* [Test framework](/docs/enterprise/setup-master-vm/#test-framework) - you can skip that step if you are running your own custom test script/framework.
* [AppVeyor Build Agent](/docs/enterprise/setup-master-vm/#appveyor-build-agent) and [Tuning for Interactive mode](/docs/enterprise/setup-master-vm/#tuning-for-interactive-mode) - these steps are mandatory.

Install any additional software required for your builds.

Do not sysprep master VM!

**Stop** VM from Azure Portal to fully deallocate its resources and avoiding excessive charges.

## Prepare master VHD

* Shutdown VM from within RDP session
    * You can use `Ctrl-Alt-End` to reach shutdown menu or run `Stop-Computer` from PowerShell
* Open new Azure Portal, navigate to **Virtual machines**, and wait until VM is fully stopped
* Delete VM with portal (this will leave VHD with all software installed before)
* Check master VHD location:
    * Open storage account created before (**appveyorbuildsa** for our example)
    * Navigate to **vhds** container
    * Ensure that it contains VHD blob named after your master VM numbers based on machine creation time. For our example it shpuld look like **appveyor-build20161226160003.vhd**. However if you happen to create another VMs in the same storage account, your VHD blob will be found in another storage account named like **your_storage_account_name** + **disks** + **some_number**. For our example it might look like **appveyorbuildrgdisks148**
* Write down storage account and VHD blob name to be ready to copy-paste when asked by the following script
* Navigate to **Access keys** for storage account and keep it open to be ready to copy-paste to the following script as well.
* [Create storage container for master disks and copy VHDS blob remained after master VM to that container](https://github.com/appveyor/ci/blob/master/scripts/enterprise/copy-master-image-azure.ps1)
* Optionally update **Disk storage account name** in our deployment notes (of destination storage account happen to be different when you run VHDS blob copy script above)

## Setting up custom cloud and images in AppVeyor

* Login to AppVeyor portal
* Navigate to your account name on the top right and select **Build environment** option from drop-down menu
    * If **Build environment** option is not available, please contact team@appveyor.com and ask to enable **Private build clouds** feature
* Press **Add cloud**, select cloud type **Azure**

**Complete the following mandatory settings**:

* **Name**: Name for your private build cloud. Make it meaningful and short to be able to use in YAML configration
* **Client ID**: *Client ID* from deployment notes
* **Client secret**: *Client Secret* from deployment notes
* **Tenant ID**: *Tenant ID* from deployment notes
* **Subscription ID**: *Subscription ID* from deployment notes
* **VMs resource group**: *VMs resource group* from deployment notes
* **Location**: Select the same location as *Location* from deployment notes (storage account location)
* **Machine size**: Select **DS** series or better (assuming you selected **premium performance** when created storage account)
* **Disk storage account name**: *Disk storage account name* from deployment notes
* **Disk storage container**: Storage container where VMs will be created, for example **vms**
* **Virtual network name**: Name of auto-created VNET for your VM resource group. It should look like `<vm_resource_group>-vnet`. Please open Azure Portal, navigate resource of type **Virtual Network** and ensure that name you entered is correct.
* **Subnet name**: In Azure Portal under **Virtual Networks** navigate to **Subnets** and copy-paste subnet name. Usually it is **default**. Optionally you can create separate subnet for AppVeyor build VMs.
* **Security group name**: *Security group name* from deployment notes
* Images
    * **IMAGE NAME**: Image name as you want to see it in AppVeyor UI and YAML, for example **VS2013 with WMF3**
    * **VHD BLOB PATH** Path to master VHD within storage account without storage account name (**images/master2017-01-05.vhd** in our example)
* Open **Failure strategy** and set the following (needed to overcome Azure VM provisioning latency):
    * **Job start timeout, seconds**: 1200 or more
    * **Provisioning attempts**: 2 or more

## Make build worker image available for configuration

* Navigate to **Build environment** > **Build worker images**
* Press **Add image**
* Enter what you set as **IMAGE NAME** in previous step
* Select your **OS Type**
* In **Build cloud** enter what you set as **Name** in previous step

## How to route build to your own cloud

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
