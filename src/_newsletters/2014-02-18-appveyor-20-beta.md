---
layout: one-column
date: 2014-02-18
title: AppVeyor 2.0 beta
---

## Meet all new AppVeyor CI


After 4 months of intensive development we are excited to announce a public beta of AppVeyor 2.0!
The new release provides you with whole new experience: build environment that is under your full
control, large projects support with build matrix and parallel testing, scriptless deployment and
release management!

You could see those great features in flagman continuous integration services for Linux such as
Travis CI, but not for Windows up until now.
Today, I'm really proud to say that AppVeyor is the only CI solution for Windows that offers
dedicated build machines with admin access, build matrix with jobs parallelization and integrated
deployment.

<p class="text-center">
    <img src="/assets/images/posts/2014-02-18/appveyor-screenshot-wireframe.png" alt="Wireframe Screenshot">
</p>


## Build matrix

<img class="right" src="/assets/images/posts/2014-02-18/build-matrix.png" alt="Build matrix">

Easily build/test for multiple configurations. Specify which operating systems, build configurations and platforms you would like to include into build matrix and AppVeyor will start a build with multiple jobs for all combinations.

Build matrix supports the following dimensions:

* Operating system
* Environment variables
* Platform, e.g. x86, x64, AnyCPU
* Configuration, e.g. Build, Debug
* Test categories

## Parallel testing

<img class="left" src="/assets/images/posts/2014-02-18/parallel-testing.png" alt="">

Large projects can contain hundreds and thousands of tests that could run for hours. AppVeyor 2.0 allows to split your tests into groups by categories, assemblies or custom criteria and run them as build jobs in parallel thus drastically reducing overall build time.

[Read more about parallel testing](/docs/parallel-testing/)

<div class="clear-both"></div>


## Deployment

<img class="right" src="/assets/images/posts/2014-02-18/deploy.png" alt="">

AppVeyor 2.0 has scriptless, repetitive one-click deployment to multiple environments! Deploy as part of the build or promote releases later - manually or through API.

Supported deployment providers:

* [Web Deploy](/docs/deployment/web-deploy/)
* [FTP](/docs/deployment/ftp/)
* [Amazon S3](/docs/deployment/amazon-s3/)
* [Azure blob](/docs/deployment/azure-blob/)
* [Azure Cloud Services](/docs/deployment/azure-cloud-service/)
* [NuGet](/docs/deployment/nuget/)
* [Deployment Agent](/docs/deployment/agent/)
* [Local](/docs/deployment/local/) (for integration testing) and Script

[Read more about deployment](/docs/deployment/)

<div class="clear-both"></div>


<h2 id="yaml">Fine-grained control over build configuration</h2>

<img class="left" src="/assets/images/posts/2014-02-18/yaml.png" alt="">

Great Windows software must provide user interface for any function it has. AppVeyor 2.0 follows this tradition and further extends project settings, so you can control build environment and inject custom script logic on any stage of build pipeline without ever touching your repository!

For command-line gurus or those coming from Linux we added fancy YAML configuration support! Add **appveyor.yml** with project configuration into root of your repository and next time you fork the repo just add a new project in AppVeyor.

[Read more about build configuration](/docs/build-configuration/)

<div class="clear-both"></div>


<h2 id="ui">All new refreshed, responsive and real-time UI</h2>

AppVeyor 2.0 has completely re-designed UI to get results faster and on the go!

AngularJS, SignalrR and Foundation helped us to build great experience we're really proud of:

* Adaptive layout and improved navigation.
* Real-time build console.
* Real-time reporting of MSBuild errors and warnings.
* Real-time reporting of test results (MSTest, NUnit and xUnit).
* Optimized for mobile screens.


## Try AppVeyor now!

<p>
    <a class="big-button" href="/pricing/">Sign up now</a>
</p>

* Free plan with support of public repositories only. [Let us know](mailto:team@appveyor.com) if you need private repositories support or want to play with parallel testing and we'll be happy to enable them for your account.


## Documentation

[https://www.appveyor.com/docs](/docs/)


## Dicsussions

[http://help.appveyor.com](http://help.appveyor.com/)

Enjoy the beta!

Feodor Fitsner,
AppVeyor founder and developer

<p class="gray">
    DISCLAIMER: This is the beta and it may contain bugs - do not use it for production projects.
    There might be interruptions as we deploy updates during the day.
    We do not backup beta database and all data will be erased when the beta is over.
</p>

<p class="gray">
    AppVeyor Systems Inc. 318 Homer St, Vancouver, BC, V6B 2V2<br/>
    To stop receiving emails from AppVeyor please use this <a href="">unsubscribe</a> link.
</p>
