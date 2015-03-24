---
layout: post
title: AppVeyor update and new pricing
---

It’s been a while since we last talked as we were working hard implementing really cool stuff and processing user requests to make AppVeyor more stable, secure and fast. Today we are thrilled to announce a huge update to AppVeyor CI service and start of its commercial availability!

<h2>Summary of what we prepared for you</h2>
<ul>
    <li>Deployment automation as part of CI process</li>
    <li>Kiln integration</li>
    <li>GitHub integration improvements</li>
    <li>Custom build scripts with integrated logging/testing/packaging support</li>
    <li>PSake support</li>
    <li>Custom storage for build artifacts, public access to artifacts, deployment to Azure</li>
    <li>Go language support</li>
    <li>API PowerShell library</li>
    <li>Status badges</li>
    <li>Other small improvements and bug fixes based on community feedback</li>
    <li>New pricing</li>
</ul>
<h2>Deployment</h2>
We spent long nights polishing our new <a href="https://github.com/AppVeyor/AppRolla">PowerShell deployment framework - AppRolla</a>. AppRolla is extensible framework tightly integrated with AppVeyor for automating deployment of distributed .NET applications to multi-server environments.

Just some of its features and benefits:
<ul>
    <li>Deploy Web applications to standalone IIS servers and web clusters</li>
    <li>Deploy Windows services to standalone back-end servers and application clusters</li>
    <li>Deploy Azure Cloud Services from blob storage (see custom artifacts storage below)</li>
    <li>Modify application configuration files upon deployment</li>
    <li>Extensible with your own custom deployment tasks</li>
</ul>
Checkout <a href="http://help.appveyor.com/kb/using-appveyor/web-application-project-deployment-to-staging-and-production-environments">web application project deployment</a> article to learn how to:
<ul>
    <li>Setup automatic deployment of web application project to Staging server as part of CI process.</li>
    <li>Promote project release to Production environment with a click of a button.</li>
    <li>Rollback, remove deployment on Production or Staging interactively from command line.</li>
</ul>
<h2>Kiln integration</h2>
We got numerous requests from our customers to integrate AppVeyor with <a href="http://www.fogcreek.com/kiln/">Kiln source control</a> from FogCreek Software. Though we are not affiliated with them Kiln looks compelling for projects with private repositories because of its per-user pricing (yes, you can create any number of repositories) and cool “Kiln harmony” feature when every single repository can be used from both Git and Mercurial, simultaneously. Integration with AppVeyor does not require an additional Kiln user - you just create a new “access token” and use it when creating a new project in AppVeyor.
<h2>GitHub integration improvements</h2>
We added GitHub organizations support and revised OAuth scopes required to access your GitHub account and repositories. AppVeyor makes sure only the minimum set of GitHub permissions is requested just to do the job.
<h2>Build scripts</h2>
In addition to high-level “Visual Studio solution” and “MSBuild” scenarios we added custom build scripts with maximum control over CI process. You can author build script in PowerShell or as a batch file and use <a href="http://help.appveyor.com/kb/using-appveyor/software-installed-on-appveyor-build-servers">any build tools</a> like MSBuild, PSake, NUnit, xUnit, Node.js in your custom workflow.

While running a custom build script AppVeyor provides you integration points for:
<ul>
    <li>communicating MSBuild results back to AppVeyor</li>
    <li>communicating testing results back to AppVeyor</li>
    <li>pushing artifacts to build artifacts storage</li>
</ul>
See <a href="http://help.appveyor.com/kb/using-appveyor/custom-build-scripts">creating custom build scripts</a> article for more details.
<h2>Custom storage for build artifacts</h2>
Out of the box AppVeyor pushes build artifacts in a cloud storage which is GEO-replicated and shared between tenants. We added custom storage support which allows you to configure either Azure blob storage or Amazon S3 account to store build artifacts for your projects. Custom storage that is configured per-account gives you additional benefits:
<ul>
    <li>You can have your own folders structure and file naming.</li>
    <li>Artifacts can be accessed using selected cloud storage API or URLs.</li>
    <li>Security policy in your organization prevents you from storing build artifacts on a shared storage.</li>
    <li>Deploy to Windows Azure cloud services directly from a blob storage.</li>
    <li>Control public access to artifacts.</li>
    <li>You won’t be affected by artifacts retention policy (it’s not yet implemented, but coming in the future) - store as many versions as you need.</li>
</ul>
See <a href="http://help.appveyor.com/kb/getting-started/packaging-artifacts">packaging artifacts</a> article for more details.
<h2>Go language support</h2>
<a href="http://golang.org/">Go language</a> from Google is gaining momentum and if you are interested in learning/using Go and coming from Windows world AppVeyor is a great platform for doing CI for your next Go project. Take a look at this <a href="https://bitbucket.org/appveyor/test-go/src">sample Go repository</a> (thanks <a href="https://twitter.com/nathany">Nathan Youngman</a> for his help) to see simple Go project structure and build.cmd to kick-off testing in AppVeyor CI environment.
<h2>API</h2>
AppVeyor is a single page application (SPA) built with AngularJS (and we promise to tell about its internals in our blog) and all its functionality is available through REST API. We created lightweight <a href="https://github.com/AppVeyor/AppVeyor-PowerShell">AppVeyor API PowerShell library</a> which is currently used by <a href="https://github.com/AppVeyor/AppRolla">AppRolla deployment</a> script and shows how to authenticate API calls and get details about your projects. We will be adding more functions into it over time.
<h2>Status badges</h2>
You can put the image on your project website displaying the status of the last build. URL of this image and markdown snippet could be found on “Status badges” tab of project settings.
<h2>Start of commercial availability</h2>
We processed a lot of feedback from our customers which allowed us to substantially improve AppVeyor CI, increase its security and stability. We have a strong feeling that AppVeyor is ready for prime time and today we announce its commercial availability.

What that means for you? Starting today (September 25th), we offer you a free 30-day trial with unlimited private repositories. During the trial period we will keep in touch with you closely to assist you with the new functionality and work out the best plan for your current needs.
<h2>New pricing</h2>
Our goal is to provide you outstanding service with competitive and reasonable <a href="http://www.appveyor.com/pricing">pricing</a>. We gave our pricing a second thought and came up with 3 simple paid plans:
<ul>
    <li>Express - $19/month (1 private repository)</li>
    <li><strong>Professional - $39/month</strong> (10 private repositories)</li>
    <li>Premium - $79/month (unlimited private repositories)</li>
</ul>
Compare to $67 per month AWS or Azure small instance running TeamCity or Jenkins!

AppVeyor is <strong>free for open-source projects</strong> with public repositories.
<h2>We listen you!</h2>
That’s a ton of new information that could be hard to digest. We are going to publish educational articles in <a href="http://blog.appveyor.com/">our blog</a> on various topics ranging from deployment automation to AppVeyor development secrets. We’ve been quite active on <a href="https://twitter.com/appveyor">Twitter</a> (and this is really cool support tool), so make sure to <a href="https://twitter.com/intent/follow?original_referer=http%3A%2F%2Fwww.appveyor.com%2Fpricing&amp;region=follow_link&amp;screen_name=appveyor&amp;tw_p=followbutton&amp;variant=2.0">follow us</a> to stay updated.

For now, if you have any questions please do not hesitate to drop us a message by just replying to this message or writing at <a href="mailto:team@appveyor.com">team@appveyor.com</a>, start a new discussion on <a href="http://help.appveyor.com/discussions">our forums</a> or submit feature request on <a href="http://appveyor.uservoice.com/">uservoice</a>.

Best regards,
Feodor Fitsner, AppVeyor founder and developer
