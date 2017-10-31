---
layout: docs
title: Publishing Azure WebJob
---

# Publishing Azure WebJob

AppVeyor allows publishing of `.zip` artifacts as Azure WebJob.

There are two types of WebJob:

* **Continuous** job when executable or script is continuously run with 1 minute intervals;
* **Triggered** job which runs on schedule.

## WebJob artifact

WebJob artifact must be a `.zip` archive that contains either executable (`.exe`) or batch (`.cmd`, `.bat`) file.
See [this page](https://github.com/projectkudu/kudu/wiki/Web-Jobs) for detailed requirements to job archive contents.

## Publishing credentials

To publish WebJob you need to know website Web Deploy credentials (username and password). Web Deploy credentials can be found in publish profile XML downloaded from website settings page (**Download publish profile** button) in [Azure Portal](https://portal.azure.com).

## Schedule format

Triggered job schedule must be specified in **crontab** format.

For Azure WebJobs schedule **must have 6 fields**:

    *    *    *    *    *    *
    -    -    -    -    -    -
    |    |    |    |    |    |
    |    |    |    |    |    +----- day of week (0 - 6) (Sunday=0)
    |    |    |    |    +------- month (1 - 12)
    |    |    |    +--------- day of month (1 - 31)
    |    |    +----------- hour (0 - 23)
    |    +------------- min (0 - 59)
    +--------------- second (0 -59)

Azure WebJobs implementation uses [NCrontab](https://github.com/atifaziz/NCrontab) library.
You can read more about [crontab expression syntax](https://github.com/atifaziz/NCrontabwiki/CrontabExpression) implemented by this library and
some find [examples](https://github.com/atifaziz/NCrontabwiki/CrontabExamples), but remember to add `0` as the first field for seconds.

## Provider settings

* **Azure website name** (`website`) - Azure website name without `.azurewebsite.net`, e.g. `mywebsite`.
* **App Service Environment** (`appservice_environment`) - Optional. Azure website is deployed to Azure AppService environment.
* **App Service Environment Name** (`appservice_environment_name`) - Available if **App Service Environment** is checked. AppService environment website default URL part, located before `p.azurewebsites.net`.
* **Deployment slot name** (`slot`) - Optional. If not specified job will be deployed to default (production) slot. Please note that you have to use Deployment (aka User-level) and not publishing profile (aka Site-level) credentials to transparently deploy to different slots.
* **Deployment credentials username** (`username`) - Username from deployment credentials you can set in Azure Portal. Optionally you can use username and password from downloaded website publishing profile XML, but this will not work transparently with deployment slots.
* **Deployment credentials password** (`password`) - Password from deployment credentials you can set in Azure Portal. As said earlier you can credentials from downloaded website publishing profile XML, but this will not work transparently with deployment slots.
* **WebJob name** (`job_name`) - Optional. Job name - can contain alphanumerics and dashes, for example `myjob-1`.
* **WebJob schedule in crontab format** (`job_schedule`) - Optional. Job run schedule in **crontab** format. If schedule is not specified and job is not set as **Manually triggered**, job is published as *continuous* job; otherwise *triggered*.
* **Manually triggered WebJob (no schedule)** (`manually_triggered`) - Optional. If set schedule is ignored.
* **Artifact(s) to deploy** (`artifact`) - Optional. Artifact "deployment name" or filename to push. If not specified all `.zip` artifacts from selected build will be published as WebJobs. If you are publishing multiple jobs in a single deployment then omit `job_name` setting - this case job name will be extrapolated from artifact file name.
* **Retry attempts** (`retry_attempts`) - Optional. Specifies the number of times the provider will retry after a failure. The default number of retries is 0.
* **Retry interval** (`retry_interval`) - Optional. Specifies, in milliseconds, the interval between provider retry attempts. The default is 1000 (one second).

Configuring in `appveyor.yml`:

```yaml
deploy:
  - provider: AzureWebJob
    website: mywebsite
    username: $mywebsite
    password:
      secure: AAABBB33CC/DDD+EEE==
    artifact: myjob.zip
    job_name: myjob-1
    job_schedule: '* 0 * * * *'
```
