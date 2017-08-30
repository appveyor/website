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

Configuring email notifications is a mandatory step.

The following providers are supported for sending email notifications from AppVeyor:

* SMTP
* [SendGrid](https://sendgrid.com/)
* [Mailgun](https://www.mailgun.com/)

To configure email notification settings login as System Administrator and go to **Settings &rarr; Email** page.

Specify **Notifications email address** and **Notifications sender name** to send *all* email messages from. These settings are mandatory.

You can optionally specify **No-reply** sender email and name to be used when sending build and deployment notifications.

Select mailing provider in **Send with** dropdown.

### SMTP

SMTP provider settings are straightforward. The only note there is that **SMTP host** setting allows specifying port, for example `smtp.gmail.com:587`.

Example settings for using GMail for sending email notifications:

* Send with: `SMTP`
* SMTP host: `smtp.gmail.com:587`
* Username: `<your-gmail-user>@gmail.com`
* Password: `<your-gmail-password>`
* Requires SSL: `Yes`

### SendGrid

[SendGrid](https://www.sendgrid.com) is *a cloud-based [SMTP provider](http://en.wikipedia.org/wiki/SMTP) that allows you to send email without having to maintain email servers. SendGrid manages all of the technical details, from scaling the infrastructure to ISP outreach and reputation monitoring to whitelist services and real time analytics.*

SendGrid is a quick option for testing AppVeyor mailouts as it doesn't require configuring/validating a real mail domain, however without real domain messages sent by AppVeyor via SendGrid would be trapping in your spam folder. SendGrid doesn't have a free plan, but free 14-day trial only.

AppVeyor uses [SendGrid API v3](https://sendgrid.com/docs/API_Reference/Web_API_v3/index.html). To create new API token login to your SendGrid account and go to `https://app.sendgrid.com/settings/api_keys` page. Click **Create API Key** button select **Restricted Access** and set "Full Access" for **Mail Send** action.

Copy API key to **API key** field on email settings in AppVeyor.

### Mailgun

[Mailgun](https://www.mailgun.com) is *Transactional Email API Service For Developers*. It has functionality similar to SendGrid. Mailgun has free tier (up to 10,000 messages per month), but for production use you should [add a real domain and verify it](https://help.mailgun.com/hc/en-us/articles/202052074-How-do-I-verify-my-domain-) - without domain verification you can send to [authorized recipients](https://help.mailgun.com/hc/en-us/articles/217531258-Authorized-Recipients) only.

You need **Active API Key** ("Secret API key" on [dashboard home page](https://app.mailgun.com/app/dashboard) or "Active API Key" on [USers & API Keys](https://app.mailgun.com/app/account/security) page) to integrate AppVeyor with Mailgun.




## Artifacts storage

* Local File System

Add `Modify` permission for `IIS_IUSRS` group on artifacts folder.

* Azure Blob Storage
* Google Storage
* Amazon S3

## Build cloud

* [Microsoft Azure](/docs/enterprise/running-builds-on-azure/)
* [Google Compute Cloud](/docs/enterprise/running-builds-on-gce/)
* [Hyper-V](/docs/enterprise/running-builds-on-hyper-v/)
* [Local process](/docs/enterprise/running-builds-as-local-process/)