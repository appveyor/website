---
title: AppVeyor update and new pricing
---

It’s been a while since we last talked as we were working hard implementing really cool stuff
and processing user requests to make AppVeyor more stable, secure and fast.
Today we are thrilled to announce a huge update to AppVeyor CI service and start of its commercial
availability!

## Summary of what we prepared for you

* Deployment automation as part of CI process
* Kiln integration
* GitHub integration improvements
* Custom build scripts with integrated logging/testing/packaging support
* PSake support
* Custom storage for build artifacts, public access to artifacts, deployment to Azure
* Go language support
* API PowerShell library
* Status badges
* Other small improvements and bug fixes based on community feedback
* New pricing

## Deployment script

We spent long nights polishing our new [PowerShell deployment framework - AppRolla](https://github.com/AppVeyor/AppRolla).
AppRolla is extensible framework tightly integrated with AppVeyor for automating deployment
of distributed .NET applications to multi-server environments.

Just some of its features and benefits:

* Deploy Web applications to standalone IIS servers and web clusters
* Deploy Windows services to standalone back-end servers and application clusters
* Deploy Azure Cloud Services from blob storage (see custom artifacts storage below)
* Modify application configuration files upon deployment
* Extensible with your own custom deployment tasks

Checkout [web application project deployment](https://appveyor.tenderapp.com/kb/using-appveyor/web-application-project-deployment-to-staging-and-production-environments) article to learn how to:

* Setup automatic deployment of web application project to Staging server as part of CI process.
* Promote project release to Production environment with a click of a button.
* Rollback, remove deployment on Production or Staging interactively from command line.

## Kiln integration

We got numerous requests from our customers to integrate AppVeyor with [Kiln source control](http://www.fogcreek.com/kiln/)
from FogCreek Software. Though we are not affiliated with them Kiln looks compelling for projects
with private repositories because of its per-user pricing (yes, you can create any number of repositories)
and cool “Kiln harmony” feature when every single repository can be used from both Git and Mercurial,
simultaneously. Integration with AppVeyor does not require an additional Kiln user - you just create
a new “access token” and use it when creating a new project in AppVeyor.

## GitHub integration improvements

We added GitHub organizations support and revised OAuth scopes required to access your GitHub account
and repositories. AppVeyor makes sure only the minimum set of GitHub permissions is requested just to do
the job.

## Build scripts

In addition to high-level “Visual Studio solution” and “MSBuild” scenarios we added custom build scripts
with maximum control over CI process. You can author build script in PowerShell or as a batch file and
use [any build tools](https://appveyor.tenderapp.com/kb/using-appveyor/software-installed-on-appveyor-build-servers) like MSBuild, PSake, NUnit, xUnit, Node.js in your custom workflow.

While running a custom build script AppVeyor provides you integration points for:

* communicating MSBuild results back to AppVeyor
* communicating testing results back to AppVeyor
* pushing artifacts to build artifacts storage

See [creating custom build scripts](https://appveyor.tenderapp.com/kb/using-appveyor/custom-build-scripts) article for more details.

## Custom storage for build artifacts

Out of the box AppVeyor pushes build artifacts in a cloud storage which is GEO-replicated and shared between tenants. We added custom storage support which allows you to configure either Azure blob storage or Amazon S3 account to store build artifacts for your projects. Custom storage that is configured per-account gives you additional benefits:

* You can have your own folders structure and file naming.
* Artifacts can be accessed using selected cloud storage API or URLs.
* Security policy in your organization prevents you from storing build artifacts on a shared storage.
* Deploy to Windows Azure cloud services directly from a blob storage.
* Control public access to artifacts.
* You won’t be affected by artifacts retention policy (it’s not yet implemented, but coming in the future) - store as many versions as you need.

See [packaging artifacts](https://appveyor.tenderapp.com/kb/getting-started/packaging-artifacts) article for more details.

## Go language support

[Go language](https://golang.org/) from Google is gaining momentum and if you are interested in
learning/using Go and coming from Windows world AppVeyor is a great platform for doing CI for your
next Go project. Take a look at this [sample Go repository](https://bitbucket.org/appveyor/test-go/src)
(thanks [Nathan Youngman](https://twitter.com/nathany) for his help) to see simple Go project structure
and build.cmd to kick-off testing in AppVeyor CI environment.

## API

AppVeyor is a single page application (SPA) built with AngularJS (and we promise to tell about its
internals in our blog) and all its functionality is available through REST API. We created lightweight
[AppVeyor API PowerShell library](https://github.com/AppVeyor/AppVeyor-PowerShell) which is currently
used by [AppRolla deployment](https://github.com/AppVeyor/AppRolla) script and shows how to
authenticate API calls and get details about your projects. We will be adding more functions into it
over time.

## Status badges

You can put the image on your project website displaying the status of the last build. URL of this image
and markdown snippet could be found on “Status badges” tab of project settings.

## Start of commercial availability

We processed a lot of feedback from our customers which allowed us to substantially improve AppVeyor CI,
increase its security and stability. We have a strong feeling that AppVeyor is ready for prime time and
today we announce its commercial availability.

What that means for you? Starting today (September 25th), we offer you a free 30-day trial with unlimited
private repositories. During the trial period we will keep in touch with you closely to assist you with
the new functionality and work out the best plan for your current needs.

## New pricing

Our goal is to provide you outstanding service with competitive and reasonable
[pricing](/pricing/). We gave our pricing a second thought and came up with 3 simple paid plans:

* Express - $19/month (1 private repository)
* **Professional - $39/month** (10 private repositories)
* Premium - $79/month (unlimited private repositories)

Compare to $67 per month AWS or Azure small instance running TeamCity or Jenkins!

AppVeyor is **free for open-source projects** with public repositories.

## We listen you!

That’s a ton of new information that could be hard to digest. We are going to publish educational
articles in [our blog](/blog/) on various topics ranging from deployment automation to
AppVeyor development secrets. We’ve been quite active on [Twitter](https://twitter.com/appveyor)
(and this is really cool support tool), so make sure to [follow us](https://twitter.com/intent/follow?original_referer=http%3A%2F%2Fwww.appveyor.com%2Fpricing&amp;region=follow_link&amp;screen_name=appveyor&amp;tw_p=followbutton&amp;variant=2.0) to stay updated.

For now, if you have any questions please do not hesitate to drop us a message at [team@appveyor.com](mailto:team@appveyor.com),
start a new discussion on [our forums](https://appveyor.tenderapp.com/discussions) or submit a feature request
on [UserVoice](https://appveyor.uservoice.com/).

Best regards,
Feodor Fitsner, AppVeyor founder and developer
