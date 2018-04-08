---
layout: docs
title: Deploying to Amazon Elastic Beanstalk
---

# Deploying to Amazon Elastic Beanstalk

Amazon Elastic Beanstalk deployment provider deploys single artifact to the Elastic Beanstalk application specified.

## Artifact Packaging

The packaging of the artifact to deploy depends on the type of project being built:

* For projects with an "MSBuild" phase a `publish_wap_beanstalk` variable should be set to true in appveyor.yml or the appropriate option selected in the UI, as below.

![package-ebs-option](/assets/img/docs/deployment/amazon-ebs/package-ebs-option.png)
  
* For other projects a zip artifact (which can be as simple as a multi argument cmd invocation of 7z to a complicated webpack or gulp toolchain) of `Elastic Beanstalk package` type is added in the UI under artifacts section or as below for yaml configuration
  
```yaml
artifacts:
- path: <path to artifact>
  type: ElasticBeanstalkPackage
```

## Provider settings

* **Access key ID** (`access_key_id`) - AWS account access key.
* **Secret access key** (`secret_access_key`) - AWS secret access key.
* **Application name** (`application_name`) - Name of application (which should already exist) to update.
* **Environment name** (`environment_name`) - Name of the environment which is part of the above application.
* **Region** (`region`) - AWS region where the application is located.
* **Artifact** (`artifact`) - Optional. Name of artifact(s) to use for update.
* **Health Check Url** (`healthcheck`) - Optional. Lets you set path to which ELB sends an HTTP GET request to determine instance health. Default is '/healthcheck'
* **Retry attempts** (`max_error_retry`) - Optional. Number of times provider will retry after a failure. Default is `0`.

Branch and other deployment conditions can be added as in other deployment providers

Configuring in `appveyor.yml`:

```yaml
deploy:
- provider: ElasticBeanstalk
  access_key_id:
  secret_access_key:
    secure:
  application_name: myEbApp
  environment_name: myEbAbb-env
  region: us-east-1
  healthcheck: /Home
  max_error_retry: 2
  on:
    branch: main # deploy from master branch only
```

*Note*: Unlike the AWS Toolkit for Visual Studio which can be used to create an application and environment from within the IDE, this deployment relies on a pre-existing Elastic Beanstalk application and environment.