---
layout: docs
title: AppVeyor Enterprise Configuration Guide
---

<!-- markdownlint-disable MD022 MD032 -->
# AppVeyor Enterprise Configuration Guide
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Email notifications

**Configuring email notifications is a mandatory step.**

The following providers are supported for sending email notifications from AppVeyor:

* SMTP
* [SendGrid](https://sendgrid.com/)
* [Mailgun](https://www.mailgun.com/)

To configure email notification settings login as System Administrator and go to **Account menu &rarr; Settings &rarr; Email** page.

Specify **Notifications email address** and **Notifications sender name** to send *all* email messages from. These settings are mandatory.

You can optionally specify **No-reply** sender email and name to be used when sending build and deployment notifications.

Select mailing provider in **Send with** dropdown.

### SMTP

SMTP provider settings are straightforward but note that there is an **SMTP host** setting for specifying port, for example `smtp.gmail.com:587`.

Example settings for using GMail for sending email notifications:

* Send with: `SMTP`
* SMTP host: `smtp.gmail.com:587`
* Username: `<your-gmail-user>@gmail.com`
* Password: `<your-gmail-password>`
* Requires SSL: `Yes`

### SendGrid

[SendGrid](https://www.sendgrid.com) is *a cloud-based [SMTP provider](https://en.wikipedia.org/wiki/SMTP) that allows you to send email without having to maintain email servers. SendGrid manages all of the technical details, from scaling the infrastructure to ISP outreach and reputation monitoring to whitelist services and real time analytics.*

SendGrid is a quick option for testing AppVeyor mailouts as it doesn't require configuring/validating a real mail domain, however without a real domain, messages sent by AppVeyor via SendGrid would be trapped in your spam folder. SendGrid doesn't have a free plan, but a free 14-day trial only.

AppVeyor uses [SendGrid API v3](https://sendgrid.com/docs/API_Reference/Web_API_v3/index.html). To create new API token login to your SendGrid account and go to `https://app.sendgrid.com/settings/api_keys` page. Click **Create API Key** button select **Restricted Access** and set "Full Access" for **Mail Send** action.

Copy API key to **API key** field on email settings in AppVeyor.

### Mailgun

[Mailgun](https://www.mailgun.com) is a *Transactional Email API Service For Developers*. It has functionality similar to SendGrid. Mailgun has free tier (up to 10,000 messages per month), but for production use you should [add a real domain and verify it](https://help.mailgun.com/hc/en-us/articles/202052074-How-do-I-verify-my-domain-) - without domain verification you can send to [authorized recipients](https://help.mailgun.com/hc/en-us/articles/217531258-Authorized-Recipients) only.

You need **Active API Key** ("Secret API key" on [dashboard home page](https://app.mailgun.com/app/dashboard) or "Active API Key" on [USers & API Keys](https://app.mailgun.com/app/account/security) page) to integrate AppVeyor with Mailgun.


## Artifact storage

**Configuring artifact storage is a mandatory step.**

Artifact storage is used to store build logs and artifact files uploaded during the build. At least one "system" artifact storage must be configured before running your first build on AppVeyor.

The following types of artifact storage are supported:

* File system
* Azure storage
* Google storage
* Amazon S3

To configure artifact storage login to AppVeyor as System Administrator and go to **Account menu &rarr; Settings &rarr; Build environment &rarr; Artifacts storages** page.

Click **Add storage** button and select storage type.

### File system

"File system" storage allows storing build artifacts on either local disk or networking share.

During AppVeyor installation `%LocalAppData%\AppVeyor\Artifacts` directory is automatically created and correct ICALs for AppVeyor Web role's app pool identity configured.

When adding "File system" storage **Path** should not contain environment variables. You can expand environment variables with the following command:

    echo %LocalAppData%\AppVeyor\Artifacts

If you are going to use a custom directory or UNC share for artifact storage then `Modify` permission for `IIS_IUSRS` group should be added for that directory.

Click **Add** button to save the settings and add a new storage.

Go to **Account menu &rarr; Settings &rarr; Build environment** page and type added storage name in **System artifact storage**. Click **Save** button.



### Azure storage

> IMPORTANT NOTE: "Premium" storage accounts cannot be used as artifact storage as they can hold page blobs only used for VM disks. [More information about Premium storage accounts](https://docs.microsoft.com/en-us/azure/storage/common/storage-premium-storage).

Login to [Azure Portal](https://portal.azure.com) and navigate to **Storage accounts** page.

Click storage details and then **Access keys** under "Settings" section. Copy **Storage account name**
and **Primary key** to AppVeyor storage settings.

Click **Add** button to save the settings and add a new storage.

Go to **Account menu &rarr; Settings &rarr; Build environment** page and type added storage name in **System artifact storage**. Click **Save** button.



### Google storage

* Open Google Cloud Platform menu and select existing or create new project to use for AppVeyor build environment
* [Create Google Cloud Platform service account and obtain certificate](/docs/enterprise/creating-gcp-service-account/)

In the main console menu navigate to **Storage &rarr; Browser** and create a new bucket.

Get back to AppVeyor storage setting screen and type **Service account email**, paste the contents of
.txt file with Base64-encoded certificate in **Service account certificate in Base64 format** field and
type **Bucket name**.

Click **Add** button to save the settings and add a new storage.

Go to **Account menu &rarr; Settings &rarr; Build environment** page and type added storage name in **System artifact storage**. Click **Save** button.



### Amazon S3

[TBD]


## Build cloud

AppVeyor can be configured to run builds in any of the following clouds:

* [Microsoft Azure](/docs/enterprise/running-builds-on-azure/)
* [Google Compute Cloud](/docs/enterprise/running-builds-on-gce/)
* [AWS](/docs/enterprise/running-builds-on-aws/)
* [Hyper-V](/docs/enterprise/running-builds-on-hyper-v/)
* [Local process](/docs/enterprise/running-builds-as-local-process/)


## Build cache

THe following services can be configured as [Build cache](/docs/build-cache/) storage:

* Azure Storage
* Google Storage
* Amazon S3
* AppVeyor storage

### AppVeyor storage

AppVeyor storage is a web service that stores/serves files to/from a local disk storage.
It can be used in the situations when cache data should not leave boundaries of your company firewall.

To host AppVeyor storage you will need IIS 7.5 or higher and .NET Framework 4.6.
The website running AppVeyor storage should be accessible from both AppVeyor machine (`External Storage Server URL` in cache settings) and build VMs (`Internal Storage Server URL`). If everything is on the same network "external" and "internal" addresses will be the same. Deploying SSL certificate is recommended; self-signed certificate will be fine.

To install AppVeyor storage web app:

* Add "Web Server" role on the machine where app will be installed. Alternatively, you can deploy it on AppVeyor VM.
* Create a local directory where cache files will be stored, for example `C:\cache-data`.
* Create new web site for AppVeyor storage. We recommend creating a new user for AppVeyor storage's dedicated app pool. This user should have "Modify" access to the local directory where cache files (blobs) will be stored.
* Modify website bindings to work with your environment; configure SSL binding.
* Download [AppVeyor storage installation package](https://www.appveyor.com/downloads/appveyor-blob-storage/1.0.0/appveyor-blob-storage.zip) and unpack it to website root directory.
* Generate private and public keys for AppVeyor storage authentication using the following PowerShell script:

```posh
$rsa = New-Object System.Security.Cryptography.RSACryptoServiceProvider -ArgumentList 2048
$privKeyXml = $rsa.ToXmlString($true)
$pubKeyXml = $rsa.ToXmlString($false)

Write-Host "=== Private Key ==="
[System.Convert]::ToBase64String([system.Text.Encoding]::UTF8.GetBytes($privKeyXml))

Write-Host "`n=== Public Key ==="
[System.Convert]::ToBase64String([system.Text.Encoding]::UTF8.GetBytes($pubKeyXml))
```

Copy-paste *Public key* to `SignaturePublicKey` app setting in `web.config` and you will have to specify *Private key* on cache settings UI in AppVeyor.

* In `web.config` update `StorageDirectory` to local cache directory path.
* Create new Windows event log and the source by running the following PowerShell command:

```posh
New-EventLog -Source "AppVeyor Blob Storage" -LogName "AppVeyor"
```

Open AppVeyor web interface and go to **Account menu** -> **Build environment** -> **Build caches**.

Add new cache of **AppVeyor storage** type.

External and internal storage server URLs should have the format `https://<storage-website-ip>` (or `http://`).

Insert **private key** generated with PowerShell script above.

Click **Add** button to save cache settings.

Open **Build clouds** page and then click details of the build cloud for which cache should be configured. Specify cache name in **Build cache name** field under **Storage and cache overrides** section.

## Build job settings

[TBD]

## Builds retention policy

Builds retention policy automatically deletes old builds and their artifacts to reclaim artfiact storage.

Builds retention policy can be enabled on account level and applied to all projects or it can be enabled (or overridden) on the individual project level.

To enable builds retention policy on account level go to **Account menu &rarr; Retention policy**.

Specify the minimum "age" of builds that should be deleted. The cut-off date is compared with the build creation date.

You can also specify multiple conditions for builds to determine exclusion, for example you can skip tagged builds or builds with `[RELEASE]` word in commit message.

Save retention policy.

Now, to execute retention policy you should additionally configure two scheduled tasks:

* Execute retention policy (`ExecuteRetentionPolicy`)
* Purge deleted builds (`PurgeDeletedBuilds`)

`ExecuteRetentionPolicy` should run first. It goes through project builds and those satisfying the criteria are marked for deletion.

`PurgeDeletedBuilds` completely deletes builds marked for deletion. All build artifacts such as logs, uploaded artifacts, test results and NuGet packages are also deleted.

To add scheduled tasks go **Account menu &rarr; Scheduled tasks**.

Add new schedule with the following details:

* Name: `ExecuteRetentionPolicy`
* Crontab expression: `0 11 * * *` (run every day at 11:00 am UTC)
* Job type: `ExecuteRetentionPolicy`
* Job parameters (JSON): `{}`
* Is active: checked

Add second schedule with the following details:

* Name: `PurgeDeletedBuilds`
* Crontab expression: `15 11 * * *` (run every day at 11:15 am UTC)
* Job type: `PurgeDeletedBuilds`
* Job parameters (JSON): `{}`
* Is active: checked

You may want to play with `ExecuteRetentionPolicy` first by scheduling it for the nearest time possible and make sure you are OK with the results (right builds were deleted), then enable `PurgeDeletedBuilds` to finally deleted builds as this operation is irreversible.