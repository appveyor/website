---
layout: docs
title: Deploying to Amazon Elastic Beanstalk
---

# Deploying to Amazon Elastic Beanstalk

Amazon Elastic Beanstalk deployment provider deploys selected artifact to the Elastic Beanstalk application specified.

## Provider settings

* **Access key ID** (`access_key_id`) - AWS account access key.
* **Secret access key** (`secret_access_key`) - AWS secret access key.
* **Application name** (`application_name`) - Name of application (which should already exist) to update.
* **Environment name** (`environment_name`) - Name of the environment which is part of the above application.
* **Region** (`region`) - AWS region where the application is located.
* **Artifact** (`artifact`) - Name of artifact(s) to use for update.
* **Health Check Url** (`healthcheck`) - Optional (default is '/healthcheck'). Lets you set path to which ELB sends an HTTP GET request to determine instance health.
* **Retry attempts** (`max_error_retry`) - Optional (defualt is 0). Number of times provider will retry after a failure. Default is `0`.

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