---
layout: docs
title: Deploying to Amazon Elastic Beanstalk
---

# Deploying to Amazon Elastic Beanstalk

Amazon Elastic Beanstalk deployment provider deploys selected artifact to the Elastic Beanstalk application specified.

## Provider settings

* **Access key ID** (`access_key_id`) - AWS account access key.
* **Secret access key** (`secret_access_key`) - AWS secret access key.
* **Application name** (`application_name`) - the name of application (which should already exist) to update.
* **Environment name** (`environment_name`) - the name of the environment which is part of the above application.
* **Region** (`region`) - AWS region where the application is located.
* **Artifact** (`artifact`) - name of artifact(s) to use for update.
* **Health Check Url** (`healthcheck`) - lets you set path to which ELB sends an HTTP GET request to determine instance health.
* **Retry attempts** (`max_error_retry`) - Number of times provider will retry after a failure. Default is `0`.

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
  artifact: myEbApp.zip
  max_error_retry: 2
  on:
    branch: main # deploy from master branch only
```