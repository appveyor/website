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

Azure WebJobs implementation uses [NCrontab](https://code.google.com/p/ncrontab/) library.
You can read more about [crontab expression syntax](https://code.google.com/p/ncrontab/wiki/CrontabExpression) implemented by this library and
some find [examples](https://code.google.com/p/ncrontab/wiki/CrontabExamples), but remember to add `0` as the first field for seconds.

## Provider settings

* **Azure website name** (`website`) - Azure website name without `.azurewebsite.net`, e.g. `mywebsite`.
* **Web Deploy username** (`username`) - Web Deploy username.
* **Web Deploy password** (`password`) - Web Deploy password.
* **WebJob name** (`job_name`) - Optional. Job name - can contain alphanumerics and dashes, for example `myjob-1`.
* **WebJob schedule in crontab format** (`job_schedule`) - Optional. Job run schedule in **crontab** format. If job schedule is specified job will be published as *triggered*; otherwise as *continuous*. Schedule 
* **Artifact(s) to deploy** (`artifact`) - Optional. Artifact "deployment name" or filename to push. If not specified all `.zip` artifacts from selected build will be published as WebJobs. If you are publishing multiple jobs in a single deployment then omit `job_name` setting - this case job name will be extrapolated from artifact file name. 

Configuring in `appveyor.yml`:

    deploy:
    - provider: AzureWebJob
      website: mywebsite
      username: $mywebsite
      password:
        secure: AAABBB33CC/DDD+EEE==
      artifact: myjob.zip
      job_name: myjob-1
      job_schedule: '* 0 * * * *'
