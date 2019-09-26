---
layout: docs
title: Calling deployment webhook
---

# Calling deployment webhook

You can use `Webhook` deployment provider to call arbitrary external API and pass deployment information in the request.
Webhook is called using `POST` method.

Webhook payload body will have the following format:

```json
{
   "accountName": "YourAccountName",
   "projectId": 35,
   "projectName": "ProjectName",
   "projectSlug": "project-slug",
   "buildId": 496,
   "buildNumber": 3,
   "buildVersion": "1.0.3",
   "buildJobId": "ktr0a5lb0t800000",
   "jobId": "f736vj1u3eg00000",
   "repositoryName": "owner/repo",
   "branch": "master",
   "commitId": "8942b4794a6ad8167cf1d7b9dc09642364700000",
   "commitAuthor": "John Smith",
   "commitAuthorEmail": "john@smith.com",
   "commitDate": "3/3/2018 12:43 AM",
   "commitMessage": "Initial commit",
   "commitMessageExtended": "",
   "artifacts": [
      {
         "fileName": "MyAwesomeCoreLib123.1.0.3.nupkg",
         "type": "NuGetPackage",
         "size": 3340,
         "url": "https://artifact-download-url"
      }
   ],
   "environmentVariables": {
      "appveyor": "True",
      "ci": "True",
      ...
   }
}
```

## Provider settings

* **URL** (`url`) - webhook URL.
* **"Authorization" header** (`authorization`) - optional `authorization` header added to the webhook request.
* **Request timeout** (`request_timeout`) - optional POST request timeout in minutes. Default is 1 minute.

Configuring in `appveyor.yml`:

```yaml
deploy:
  provider: Webhook
  url: https://someservice.com/webhook
  authorization: Basic aabbcc==
  request_timeout: 5
```
