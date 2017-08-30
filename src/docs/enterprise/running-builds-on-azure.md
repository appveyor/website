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

## Create AD Service Principal

### Create Azure Active directory application

During this step you will be needed to collect number of configuration and authentication setting for later use with AppVeyor. To avoid confusion we recommend to prepare some deployment notes (it can be just notepad file in **secure** location or password manager section), with the following fields:

* AAD application name
* Client ID
* Client Secret
* Tenant ID
* Subscription ID
* VMs resource group
* Location
* Disk storage account name
* Security group name

Now let us start with creating AAD application.

* Open [old Azure portal](https://manage.windowsazure.com/), select **Active Directory** in the left pane
* Select **Default directory**, which you should be already created by default for you in Azure
* Select **APPLICATIONS** tab
* Select **Add** in the bottom. **Add application** wizard will pop up
    * Choose **Add an application that my organization is developing**
    * Name it descriptively (for example **Appveyor build**) copy it and save in your deployment notes as a **AAD application name**
    * Select **WEB APPLICATION AND/OR WEB API** type
    * **SIGN-ON URL** and **APP ID URI** are not going to be used in our scenario, so add anything which is well-recognize-able like [http://appveyor-build](http://appveyor-build)
* Select **CONFIGURE** tab
    * Navigate to **CLIENT ID**, copy it and save in your deployment notes as a **Client ID**
    * Navigate to **keys**, select duration, and press **SAVE** icon on the bottom of the screen
    * Secure key will be displayed, copy it and save in your deployment notes as a **Client Secret**
* Next save Tenant ID for future use. For that you need to copy GUID from browser address bar
    * You can see two GUIDs there if you are still in **CONFIGURE** page. You need first which is following `Directory/`
    * Save it in your deployment notes as a **Tenant ID**
* Finally save Subscription ID to complete deployment notes
    * Navigate to **Subscriptions** in the top right, choose **Manage subscription/directory**, select, copy and save your **Subscription ID** in deployment notes.

## Create new Resource Group in new Portal

* Open [new Azure Portal](https://portal.azure.com)
    * You can open it from old portal directly by selecting **Check out the new portal**
* Select **Resource groups**
    * You may need to go to **More services** in the bottom of the left menu and type **Resource groups** in Filter
* Press **Add** to create new resource groups
    * Name it consistently with what was chosen for AAD application, for example **appveyor-build-rg**
    * Complete resource group creation by pressing **Create**
    * Save resource group name in your deployment notes as **VMs resource group**
* Do not close Azure Portal

### Assign principal (AAD application) to resource group as a Contributor

* Select Resource group created before and navigate to **Access control (IAM)**
* New Azure portal's *blade* will open on the right, press **Add** on top of it
* In rightmost *blade* select **Contributor**
* Yet another blade named **Add users** will open
    * By default, it displays only Microsoft (Live Id) accounts
    * Use search to find **AAD application name** from your deployment notes
* Press **Select** and **OK** to confirm your selection
* Do not close Azure Portal

#### Useful links

* [http://blog.davidebbo.com/2014/12/azure-service-principal.html](http://blog.davidebbo.com/2014/12/azure-service-principal.html)
* [http://www.videoqe.com/videogallery/configure-azure-active-directory-application](http://www.videoqe.com/videogallery/configure-azure-active-directory-application)

**Now your Azure Active directory application (which will be used by AppVeyor as identity) has Contributor permissions in resource group which will contain resources managed by AppVeyor!**

## Create new storage account

* In Azure Portal select **Storage accounts**
    * You may need to go to **More services** in the bottom of the left menu and type **Storage accounts** in Filter
    * Ensure that you selected **Storage accounts** and not **Storage accounts (classic)**
* Press **Add**
* In **Create storage account** *blade* enter/select the following (only important settings described, please feel free to leave default value for others):
    * Descriptive name, which still satisfies storage account naming restrictions (**appveyorbuildsa** for our example).
    * Deployment model: Resource manager
    * Performance: premium (not mandatory, but recommended, however it will require **DS** series or better VMs)
    * Replication: Locally-redundant storage (LRS) is good enough for our purpose
    * Resource group: Use existing and select resource group created before (**appveyor-build-rg** in our example)
    * Location: Choose what is closer to services your build might depend on. Appveyor servers are located in West US, but it will work well with AppVeyor in any Azure region
* Save Location in your deployment notes as **Location**
* Save storage account name in your deployment notes as **Disk storage account name**
* Do not close Azure Portal

## Create Master VM

Please note that in this document we use **Windows Server 2012 R2 Datacenter** VM as most popular and most tested with AppVeyor. However, the whole point of AppVeyor's **Private cloud** feature is to give customers more freedom in build worker VM software. For AppVeyor to work we OS should be at least Windows Server 2012 (R2 preferable) with .NET framework 4.5 installed.

* In Azure Portal select **Virtual machines**
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

* Follow [those steps](/docs/enterprise/setup-master-vm/) to setup software required for build process. It is tested PowerShell scripts which can be simple copy-pasted to PowerShell window (started in privileged mode). Some notes:
    * We strongly recommend to run everything from [Basic configuration](/docs/enterprise/setup-master-vm/#basic-configuration) and [Essential 3rd-party software](/docs/enterprise/setup-master-vm/#essential-3rd-party-software)
    * You can skip one of msbuild and VS version or both if you dont need them in [Build framework](/docs/enterprise/setup-master-vm/#build-framework) step
    * You can skip test framework you do not need in [Test framework](/docs/enterprise/setup-master-vm/#test-framework) step
    * Steps [AppVeyor Build Agent](/docs/enterprise/setup-master-vm/#appveyor-build-agent) and [Tuning for Interactive mode](/docs/enterprise/setup-master-vm/#tuning-for-interactive-mode) are mandatory

* Install additional software needed

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
