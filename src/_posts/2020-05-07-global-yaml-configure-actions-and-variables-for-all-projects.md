---
title: 'Global YAML - configure actions and variables for all projects'
---

AppVeyor customers have been asking about an ability to configure environment variables that are common for all projects under account. These could be a secure variable with SSH key for accessing the repo, a password for signing certificate or some other value you don't want to include into every project's `appveyor.yml`.

We've finally got a solution for that! Global YAML is a configuration in [appveyor.yml](/docs/appveyor-yml) format which is "injected" into all projects under the account. It can be edited on **Account &rarr; Global YAML** page. The idea of using the same `appveyor.yml` syntax for global configuration worked so good and natural that it gone beyond just environment variables - you can "globalize" pretty much everything:

* Clone script overriding built-in cloning commands;
* Environment variables, `/etc/hosts` and `cache` entries;
* `init`, `install` actions;
* Before/after scripts for build, test and deploy phases;
* Artifacts;
* Deployment steps;
* Notifications;
* Build finalizers: `on_success`, `on_failure` and `on_finish`.

Consider this global configuration for example:

```yaml
environment:
  MY_SECRET_ACCOUNT:
    secure: AAAAABBBBEEEEE22==
  MY_SECRET_KEY:
    secure: AABBCC11==

init:
- ps: gcim Win32_Processor | % { "$($_.NumberOfLogicalProcessors) logical CPUs" }
- ps: gcim Win32_OperatingSystem | % { "$([int]($_.TotalVisibleMemorySize/1mb)) Gb" }

notifications:
- provider: Slack
  incoming_webhook:
    secure: AAABBB+CCC+DDD==
  channel: '#ci'
  on_build_failure: true
```

The configuration above defines two secure variables `MY_SECRET_ACCOUNT` and `MY_SECRET_KEY` that will be available in all builds under your account, outputs VM configuration (you can display any instrumental data relevant to your projects) in the beginning of each build and, finally, sends notification to a Slack channel on every build's failure.

[Read more](/docs/global-yml/) about what sections are supported and how they are merged into the projects.

Enjoy!