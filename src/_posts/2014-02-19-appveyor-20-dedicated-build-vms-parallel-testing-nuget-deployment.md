---
layout: post
title: 'AppVeyor 2.0: dedicated build VMs, parallel testing, NuGet, deployment and more'
---

<p>After 4 months of intensive development we are excited to announce a public beta of AppVeyor 2.0! The new release provides you with whole new experience: build environment that is under your full control, large projects support with build matrix and parallel testing, scriptless deployment and release management!</p>
You could see those great features in flagman continuous integration services for Linux such as Travis CI, but not for Windows up until now. Today, I'm really proud to say that AppVeyor is the only CI solution for Windows that offers dedicated build machines with admin access, build matrix with jobs parallelization and integrated deployment.
<p class="text-center"><a href="/assets/images/posts/appveyor-20/appveyor-screenshot-wireframe.png"><img style="width:100%;max-width:759px;" src="/assets/images/posts/appveyor-20/appveyor-screenshot-wireframe.png" alt=""></a></p>

<h2>What’s new at a glance</h2>
<ul>
    <li><a href="#vms">Builds run on dedicated Virtual Machines</a></li>
    <li><a href="#nuget">NuGet hosting</a></li>
    <li><a href="#build-matrix">Build matrix</a></li>
    <li><a href="#parallel-testing">Parallel testing</a></li>
    <li><a href="#deployment">Deployment to multiple environments</a></li>
    <li><a href="#yaml">Build configuration in YAML</a></li>
    <li><a href="#ui">Responsive UI</a></li>
</ul>
<h2 id="vms">Builds run on dedicated Virtual Machines</h2>
<img style="width:100%;max-width:286px;float:right;" src="/assets/images/posts/appveyor-20/Windows_Azure_logo.png" alt="">In AppVeyor 2.0 we are moving away from shared build servers to dedicated VMs. Every build job runs on pristine VM with admin rights! This was probably the main reason for kicking-off this release.
<ul>
    <li>With dedicated build VM you get guaranteed performance (CPU and I/O).</li>
    <li>Increased security with confidence that your code downloaded to isolated environment that is immediately decommissioned after build completes.</li>
    <li>Having admin rights on build server gives you unlimited possibilities: from installing additional software with Chocolatey or Web PI for supporting your build and headless browser testing to deploying to the same build server for integration testing and BVTs.</li>
    <li>Being hosted on Windows Azure AppVeyor can offer you build infrastructure with unlimited scale.</li>
</ul>
<h2 id="nuget">NuGet hosting</h2>
AppVeyor 2.0 has built-in hosting for private and public NuGet feeds.

<img style="width:100%;max-width:120px;float:left;" src="/assets/images/posts/appveyor-20/nuget-logo.png" alt="">

Every account comes with a private password-protected NuGet feed aggregating packages from all projects and enabling publishing of your custom packages.

Projects have separate NuGet feeds with all NuGet packages pushed as artifacts.

<a href="http://www.appveyor.com/docs/nuget">Read more about NuGet support</a>
<h2 id="build-matrix">Build matrix</h2>
<img style="width:100%;max-width:395px;float:right;" src="/assets/images/posts/appveyor-20/build-matrix.png" alt="">

Easily build/test for multiple configurations. Specify which operating systems, build configurations and platforms you would like to include into build matrix and AppVeyor will start a build with multiple jobs for all combinations.

Build matrix supports the following dimensions:
<ul>
    <li>Operating system</li>
    <li>Environment variables</li>
    <li>Platform, e.g. x86, x64, AnyCPU</li>
    <li>Configuration, e.g. Build, Debug</li>
    <li>Test categories</li>
</ul>
<h2 id="parallel-testing">Parallel testing</h2>
<img style="width:100%;max-width:313px;float:left;" src="/assets/images/posts/appveyor-20/parallel-testing.png" alt="">

Large projects can contain hundreds and thousands of tests that could run for hours. AppVeyor 2.0 allows to split your tests into groups by categories, assemblies or custom criteria and run them as build jobs in parallel thus drastically reducing overall build time.

<a href="http://www.appveyor.com/docs/parallel-testing">Read more about parallel testing</a>
<h2 id="deployment">Deployment</h2>
<img style="width:100%;max-width:133px;float:right;" src="/assets/images/posts/appveyor-20/deploy.png" alt="">

AppVeyor 2.0 has scriptless, repetitive one-click deployment to multiple environments! Deploy as part of the build or promote releases later - manually or through API.

Supported deployment providers: <a href="http://www.appveyor.com/docs/deployment/web-deploy">Web Deploy</a>, <a href="http://www.appveyor.com/docs/deployment/ftp">FTP</a>, <a href="http://www.appveyor.com/docs/deployment/amazon-s3">Amazon S3</a>, <a href="http://www.appveyor.com/docs/deployment/azure-blob">Azure blob</a>, <a href="http://www.appveyor.com/docs/deployment/azure-cloud-service">Azure Cloud Services</a>, <a href="http://www.appveyor.com/docs/deployment/nuget">NuGet</a>, <a href="http://www.appveyor.com/docs/deployment/agent">Deployment Agent</a>, <a href="http://www.appveyor.com/docs/deployment/local">Local</a> (for integration testing) and Script.

<a href="http://www.appveyor.com/docs/deployment">Read more about deployment</a>
<h2 id="yaml">Fine-grained control over build configuration</h2>
<img style="width:100%;max-width:203px;float:left;" src="/assets/images/posts/appveyor-20/yaml.png" alt="">

Great Windows software must provide user interface for any function it has. AppVeyor 2.0 follows this tradition and further extends project settings, so you can control build environment and inject custom script logic on any stage of build pipeline without ever touching your repository!

For command-line gurus or those coming from Linux we added fancy YAML configuration support! Add <b>appveyor.yml</b> with project configuration into root of your repository and next time you fork the repo just add a new project in AppVeyor.

<a href="http://www.appveyor.com/docs/build-configuration">Read more about build configuration</a>
<div></div>
<h2 id="ui">All new refreshed, responsive and real-time UI</h2>
AppVeyor 2.0 has completely re-designed UI to get results faster and on the go!

AngularJS, SignalrR and Foundation helped us to build great experience we're really proud of:
<ul>
    <li>Adaptive layout and improved navigation.</li>
    <li>Real-time build console.</li>
    <li>Real-time reporting of MSBuild errors and warnings.</li>
    <li>Real-time reporting of test results (MSTest, NUnit and xUnit).</li>
    <li>Optimized for mobile screens.</li>
</ul>
<h2>Try AppVeyor now!</h2>
<p style="margin:2rem 0;"><a style="font-size:14pt;color:#fff;text-decoration:none;background-color:#2cba2c;padding:.7rem 2rem;border-radius:4px;" href="http://www.appveyor.com/pricing">Sign up now</a></p>

<ul>
    <li>Free plan with support of public repositories only. <a href="mailto:team@appveyor.com">Let us know</a> if you need private repositories support or want to play with parallel testing and we'll be happy to enable them for your account.</li>
</ul>
<h2>Documentation</h2>
<a href="http://www.appveyor.com/docs">http://www.appveyor.com/docs</a>
<h2>Dicsussions</h2>
<a href="http://help.appveyor.com/">http://help.appveyor.com</a>

Enjoy the beta!

Feodor Fitsner,
AppVeyor founder and developer
