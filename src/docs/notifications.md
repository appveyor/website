---
layout: docs
title: Build notifications
---

<!-- markdownlint-disable MD022 MD032 -->
# Build notifications
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

Build notifications are defined on project level and triggered on build success or fail events.
You can configure notification on **Notifications** tab of project settings or in `notifications` section of `appveyor.yml`:

```yaml
notifications:
  - provider: <provider_1>
    settings: ...
      on_build_success: true|false
      on_build_failure: true|false
      on_build_status_changed: true|false

  - provider: <provider_2>
    settings: ...
```

**Warning**: Notifications defined on project settings UI are merged with notifications defined in appveyor.yml.

## Triggering notifications

When notification is configured in `appveyor.yml` and no `on_build_` triggers are specified then all triggers
are set to `true` meaning notification is sent on all three "success", "failure" and "status change" events.
For example, if you have the following setup:

```yaml
notifications:
  - provider: Email
    to:
      - some@email.com
```

it will be read by AppVeyor as:

```yaml
notifications:
  - provider: Email
    to:
      - some@email.com
    on_build_success: true
    on_build_failure: true
    on_build_status_changed: true
```

so, to not receive notifications on build success specify:

```yaml
notifications:
  - provider: Email
    to:
      - some@email.com
    on_build_success: false
```

## Global email notifications

Email notification preferences sent for all projects are unique for every user and can be changed on [Notifications](https://ci.appveyor.com/notifications) page.

You can limit the amount of email notifications you receive:

* Notifications from all builds
* Only notifications for builds with your commits
* Do not send notifications at all
* Send only if build status has changed

![email notifications](/assets/images/docs/notifications/email-notifications.png)



## Project email notifications

You can define email notifications specific to a project.

### Subject template

If not specified default subject template:

```text
{% raw %}
Build {{status}}: {{projectName}} {{buildVersion}}
{% endraw %}
```


### Message template

Default message body template:

```html
{% raw %}
<div style="font-family:'Segoe UI',Arial,Sans-Serif;font-size:10pt;">
    {{#passed}}
    <h1 style="font-size: 150%;font-weight:normal; color:#078DC7;"><a href="{{buildUrl}}" style="color:#078DC7;">Build {{projectName}} {{buildVersion}} completed</a></h1>{{/passed}}
    {{#failed}}
    <h1 style="font-size: 150%;font-weight:normal; color:#ff3228;"><a href="{{buildUrl}}" style="color:#ff3228;">Build {{projectName}} {{buildVersion}} failed</a></h1>{{/failed}}
    <p style="color: #888;">
        Commit <a href="{{commitUrl}}">{{commitId}}</a> by <a href="mailto:{{commitAuthorEmail}}">{{commitAuthor}}</a> on {{commitDate}}:
        <br />
        <span style="font-size: 110%;color:#222;">{{commitMessage}}</span>
    </p>
    <p><a href="{{notificationSettingsUrl}}" style="font-size:85%;color:#999;">Configure your notification preferences</a></p>
</div>
{% endraw %}
```

[How to customize message template](#customizing-message-template)

### appveyor.yml configuration

```yaml
{% raw %}
notifications:
  - provider: Email
    to:
      - user1@email.com
      - user2@email.com
    subject: 'Build {{status}}'                  # optional
    message: "{{template_variable_1}}, {{template_variable_2}}, ..."    # optional
    on_build_success: true|false
    on_build_failure: true|false
    on_build_status_changed: true|false
{% endraw %}
```



## Slack

Slack sends notifications as message attachments which allows differentiating successful/failed builds/deployments with color.

![slack-build](/assets/images/docs/notifications/slack-build.png)

Slack notifications can be configured to use either *Authentication token* and *channel name* or *Incoming webhook*.

### Authentication token with channel name

When authentication token is used channel name must be specified.
Slack API authentication token can be generated on this page (**Authentication** section):
[https://api.slack.com/web](https://api.slack.com/web)

When you specify channel name make sure it starts with `#` sign if it's public channel, for example `#builds`.
In `appveyor.yml` you need to quote channel name to pass YAML validation, for example:

```yaml
- provider: Slack
  auth_token:
    secure: AAABBB+CCC==
  channel: '#channel'
```

### Incoming webhook

Slack notifications can be configured to use [Incoming webhook](https://api.slack.com/incoming-webhooks).
When webhook is used channel name can be omitted as it's encoded into webhook URL.

Follow **incoming webhook integration** link on [Incoming webhooks](https://api.slack.com/incoming-webhooks) page to
generate new incoming webhook.

In `appveyor.yml` webhook URL can be set as secure string, for example:

```yaml
- provider: Slack
  incoming_webhook:
    secure: AAABBB+CCC+DDD==
```

### Message template

Default Slack message template:

```text
{% raw %}
<{{buildUrl}}|Build {{projectName}} {{buildVersion}} {{status}}>
Commit <{{commitUrl}}|{{commitId}}> by {{commitAuthor}} on {{commitDate}}:
_{{commitMessage}}_
{% endraw %}
```

* [How to customize message template](#customizing-message-template)
* [Slack message formatting guide](https://api.slack.com/docs/formatting)


### appveyor.yml configuration

```yaml
{% raw %}
notifications:
  - provider: Slack
    auth_token:
      secure: kBl9BlxvRMr9liHmnBs14A==
    channel: development
    template: "{{template_variable_1}}, {{template_variable_2}}, ..."
{% endraw %}
```

Encrypt authentication token on [this page](https://ci.appveyor.com/tools/encrypt).


## GitHub Pull Request

GitHub Pull Request notifications is a perfect way to notify all developers working on a pull request.
AppVeyor can post a new comment with build results to a pull request being built. GitHub will send email notifications
to all subscribed developers.

![githubpullrequest-notifications](/assets/images/docs/notifications/githubpullrequest-notifications.png)

A new comment can be made on behalf of [AppVeyorBot](https://github.com/AppVeyorBot) GitHub account (public repositories only) or any
custom GitHub account account ("bot") having access to your repositories.


### Message template

Default GitHub Pull Request comment template:

```text
{% raw %}{{#passed}}:white_check_mark:{{/passed}}{{#failed}}:x:{{/failed}} [Build {{&projectName}} {{buildVersion}} {{status}}]({{buildUrl}}) (commit {{commitUrl}} by @{{&commitAuthorUsername}}){% endraw %}
```

[How to customize message template](#customizing-message-template)

### appveyor.yml configuration

```yaml
{% raw %}
notifications:
  - provider: GitHubPullRequest
    auth_token:
      secure: kBl9BlxvRMr9liHmnBs14A==
    template: "{{#passed}}:white_check_ ..."
{% endraw %}
```

Encrypt authentication token on [this page](https://ci.appveyor.com/tools/encrypt).


## HipChat

HipChat has two versions of API: v1 and v2. AppVeyor supports both versions though notifications sent using API v1 allow specifying "from" field:

![hipchat-build-passed-api-v1](/assets/images/docs/notifications/hipchat-build-passed-api-v1.png)

![hipchat-build-failed-api-v1](/assets/images/docs/notifications/hipchat-build-failed-api-v1.png)

while API v2 always sends messages on behalf API token issuer:

![hipchat-build-passed-api-v2](/assets/images/docs/notifications/hipchat-build-passed-api-v2.png)

![hipchat-build-failed-api-v2](/assets/images/docs/notifications/hipchat-build-failed-api-v2.png)


### Authentication token

You can generate API v1 token on this page (you must be a group admin):

```text
https://<your_account>.hipchat.com/admin/api
```

**Notification** token type is enough for AppVeyor to post message to a room.

You can generate API v2 token on this page:

```text
https://<your_account>.hipchat.com/account/api
```

### Message template

Default HipChat message template:

```text
{% raw %}
<a href=""{{buildUrl}}"">Build {{projectName}} {{buildVersion}} {{status}}</a><br/>
Commit <a href=""{{commitUrl}}"">{{commitId}}</a> by {{commitAuthor}} on {{commitDate}}:
<i>{{commitMessage}}</i>
{% endraw %}
```

[How to customize message template](#customizing-message-template)

### appveyor.yml configuration

```yaml
{% raw %}
notifications:
  - provider: HipChat
    auth_token:
      secure: RbOnSMSFKYzxzFRrxM1+XA==
    room: ProjectA
    template: "{{template_variable_1}}, {{template_variable_2}}, ..."
{% endraw %}
```

Encrypt authentication token on [this page](https://ci.appveyor.com/tools/encrypt).


## Campfire

### Authentication token

Campfire API authentication token can be generated on **My info** page:

```text
https://<your_account>.campfirenow.com/member/edit
```

### Message template

Default Campfire message template:

```text
{% raw %}
Build {{projectName}} {{buildVersion}} {{status}}: {{buildUrl}}
Commit #{{commitId}} by {{commitAuthor}} on {{commitDate}}: {{commitMessage}}
{% endraw %}
```

[How to customize message template](#customizing-message-template)

### appveyor.yml configuration

```yaml
{% raw %}
notifications:
  - provider: Campfire
    account: appveyor
    auth_token:
      secure: RifLRG8Vfyol+sNhj9u2JA==
    room: ProjectA
    template: "{{template_variable_1}}, {{template_variable_2}}, ..."
{% endraw %}
```

Encrypt authentication token on [this page](https://ci.appveyor.com/tools/encrypt).


## VSO Team Rooms

### Authentication

Visual Studio Online team notifications are being sent on someone's behalf and that user should have *alternate credentials* enabled for their VSO account. To enabled alternate credentials login to your VSO account and then click your name at the top right corner, then select "My profile" and click "Credentials" tab.

### Message template

Default VSO message template

```text
{% raw %}
Build {{projectName}} {{buildVersion}} {{status}}: {{buildUrl}}
Commit #{{commitId}} by {{commitAuthor}} on {{commitDate}}: {{commitMessage}}
{% endraw %}
```

### appveyor.yml configuration

```yaml
{% raw %}
notifications:
  - provider: VSOTeamRoom
    account: <account-name>
    username: <alternate-username>
    password: <your-password>
    room: ProjectA
    template: "{{template_variable_1}}, {{template_variable_2}}, ..."
{% endraw %}
```

## Webhooks

By default webhook notification makes `POST` request, but this can be changed to `GET` with `method` setting.

Configuring webhooks in `appveyor.yml`:

```yaml
notifications:

  - provider: Webhook
    url: http://www.myhook1.com
    method: GET

  - provider: Webhook
    url: http://www.myhook2.com
    headers:
      User-Agent: myapp 1.0
      Authorization:
        secure: GhD+5xhLz/tkYY6AO3fcfQ==
    on_build_success: false
    on_build_failure: true
    on_build_status_changed: true
```

You can use Mustache variables (explained in the section below) in URL and header values, for example:

```text
{% raw %}
- provider: Webhook
  url: http://requestb.in/test?appveyor_build_version={{buildVersion}}&appveyor_commit_id={{commitId}}
  method: GET
  headers:
    APPVEYOR-PROJECT-NAME: '{{projectName}}'
    APPVEYOR-BUILD-VERSION: '{{buildVersion}}'
    APPVEYOR-COMMIT-ID: '{{commitId}}'
{% endraw %}
```

### Webhook payload (default)

When webhook notification triggers AppVeyor makes POST request to the webhook URL and passes JSON data in the body:

```json
{
   "eventName":"build_success",
   "eventData":{
      "passed":true,
      "failed":false,
      "status":"Success",
      "started":"2014-04-14 7:57 PM",
      "finished":"2014-04-14 7:58 PM",
      "duration":"00:01:30.3741236",
      "projectId":12064,
      "projectName":"test-web",
      "buildId":14636,
      "buildNumber":26,
      "buildVersion":"1.0.26",
      "repositoryProvider":"gitHub",
      "repositoryScm":"git",
      "repositoryName":"JohnSmith/test-web",
      "branch":"master",
      "commitId":"ad2366f0c4",
      "commitAuthor":"John Smith",
      "commitAuthorEmail":"john@smith.com",
      "commitDate":"2014-04-14 1:54 AM",
      "commitMessage":"Some changes to appveyor.yml",
      "committerName":"John Smith",
      "committerEmail":"john@smith.com",
      "isPullRequest":true,
      "pullRequestId":1,
      "buildUrl":"https://ci.appveyor.com/project/JohnSmith/test-web/build/1.0.26",
      "notificationSettingsUrl":"https://ci.appveyor.com/notifications",
      "messages":[],
      "jobs":[
         {
            "id":"es941edratul5jm3",
            "name":"",
            "passed":true,
            "failed":false,
            "status":"Success",
            "started":"2014-04-14 7:57 PM",
            "finished":"2014-04-14 7:58 PM",
            "duration":"00:01:27.9060155",
            "messages":[

            ],
            "compilationMessages":[
               {
                  "category":"warning",
                  "message":"Found conflicts between different versions of the same dependent assembly....",
                  "details":"MSB3247",
                  "fileName":"..\\..\\Program%20Files%20(x86)\\MSBuild\\12.0\\bin\\Microsoft.Common.CurrentVersion.targets",
                  "line":1635,
                  "column":5,
                  "projectName":"MyWebApp",
                  "projectFileName":"MyWebApp\\MyWebApp.csproj",
                  "created":"2014-04-14T19:57:54.0838622+00:00"
               }
            ],
            "artifacts":[
               {
                  "fileName":"MyWebApp.zip",
                  "name":"MyWebApp",
                  "type":"WebDeployPackage",
                  "size":3491576,
                  "url":"https://ci.appveyor.com/api/buildjobs/es941edratul5jm3/artifacts/token/261761baaaa8337f0a13fa8b5587451ff2d13e4cff095c74e6eabb5d5dea0909/MyWebApp.zip"
               }
            ]
         }
      ]
   }
}
```

`eventName` can be either `build_success` or `build_failure`

### Webhook payload (custom)

Webhook request body can be customized for cases like notifying New Relic.

Configuring custom webhook request body with default content type (`application/json`) in `appveyor.yml`:

```yaml
notifications:
- provider: Webhook
  url: http://requestb.in/p7cjenp7
  method: POST
  headers:
    X-Api-Key:
     secure: 9vNz+tHM2nuER4MTf+iIbg==
  body: >-
    {
        "foo":  "bar"
    }
```

You can set custom Content-Type header for non-JSON content in `appveyor.yml`:

```yaml
notifications:
- provider: Webhook
  url: http://requestb.in/1hnkf4y1
  method: POST
  content_type: application/xml
  body: >-
    <?xml version="1.0" encoding="UTF-8"?>

    <Objects>
       <Object Type="System.Collections.Hashtable">
          <Property Name="Key" Type="System.String">foo</Property>
          <Property Name="Value" Type="System.String">bar</Property>
       </Object>
    </Objects>
```

## Customizing message template

Message template is a [Mustache template](http://mustache.github.io/mustache.5.html) with the same data as `eventData` field in webhook JSON payload above, for example:

```text
{% raw %}
{{#passed}} Build {{projectName}} {{buildVersion}} passed {{/passed}}
{{#failed}} Build {{projectName}} {{buildVersion}} failed {{/failed}}
{% endraw %}
```

## Adding custom value to notification

Sometimes `eventdata` from webhook JSON payload is not enough and you need to pass some custom value into notification. One possible trick to do this is to alter build job message. 

For example, you need to add some custom warning which appears because of some special condition. Let's say you want to send specific message in email notification if variable `a` is greater than `b`. In this case you can add the following into your appveyor.yml:

```yaml
test_script:
- ps: if ($a -gt $b) {Add-AppveyorMessage "My custom warning"}
```

And add the following to notification template:

```html
<p>Build messages:</p>  
    <ul>
    {{#jobs}}
      {{#messages}}
        <li>{{message}}</li>
      {{/messages}}
    {{/jobs}}
    </ul>
```

## Build status in tray - Catlight

[Catlight](https://catlight.io) is a free application that runs on Windows and OS X. It shows current status of AppVeyor builds in tray (or menu bar on OS X). In addition, it will show a toaster notification when a build starts or completes.

![catlight-build-started-notification](/assets/images/docs/notifications/catlight-build-started-notification.png)

[Using Catlight build status notifications with AppVeyor](https://catlight.io/a/appveyor-build-status-notifications)
