---
layout: one-column
date: 2014-02-18
title: AppVeyor 2.0 beta
---

<div style="font-family:'Segoe UI',Arial,Sans-Serif;font-size:10pt;">

    <style>
        a {
            color: #0066CC;
        }
    </style>

    <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">Meet all new AppVeyor CI</h2>

    <p>
        After 4 months of intensive development we are excited to announce a public beta of AppVeyor 2.0!
        The new release provides you with whole new experience: build environment that is under your full control, large projects support with build matrix and parallel testing,
        scriptless deployment and release management!
    </p>

    <p>
        You could see those great features in flagman continuous integration services for Linux such as Travis CI, but not for Windows up until now.
        Today, I'm really proud to say that AppVeyor is the only CI solution for Windows that offers dedicated build machines with admin access,
        build matrix with jobs parallelization and integrated deployment.
    </p>

    <p class="text-center">
        <img src="/assets/images/newsletters/2014-02-18/appveyor-screenshot-wireframe.png" style="width:100%;max-width:759px;">
    </p>


    <!-- At a glance -->
    <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">What’s new at a glance</h2>
    <ul>
        <li style="margin-bottom:0.3rem;">Builds run on dedicated Virtual Machines</li>
        <li style="margin-bottom:0.3rem;">NuGet hosting</li>
        <li style="margin-bottom:0.3rem;">Build matrix</li>
        <li style="margin-bottom:0.3rem;">Parallel testing</li>
        <li style="margin-bottom:0.3rem;">Deployment to multiple environments</li>
        <li style="margin-bottom:0.3rem;">Build configuration in YAML</li>
        <li style="margin-bottom:0.3rem;">Responsive UI</li>
    </ul>


    <!-- Dedicated build machines -->
    <a id="vms"></a>
    <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">Builds run on dedicated Virtual Machines</h2>
    <p>
        In AppVeyor 2.0 we are moving away from shared build servers to dedicated VMs. Every build job runs on pristine VM with admin rights!
        This was probably the main reason for kicking-off this release.
    </p>

    <img src="/assets/images/newsletters/2014-02-18/Windows_Azure_logo.png" style="width:100%;max-width:286px;float:right;">

    <ul>
        <li style="margin-bottom:0.3rem;">With dedicated build VM you get guaranteed performance (CPU and I/O).</li>
        <li style="margin-bottom:0.3rem;">Increased security with confidence that your code downloaded to isolated environment that is immediately decommissioned after build completes.</li>
        <li style="margin-bottom:0.3rem;">Having admin rights on build server gives you unlimited possibilities: from installing additional software with Chocolatey or Web PI
            for supporting your build and headless browser testing to deploying to the same build server for integration testing and BVTs.</li>
        <li>Being hosted on Windows Azure AppVeyor can offer you build infrastructure with unlimited scale.</li>
    </ul>


    <!-- NuGet -->
    <a id="nuget"></a>
    <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">NuGet hosting</h2>

    <p>AppVeyor 2.0 has built-in hosting for private and public NuGet feeds.</p>

    <img src="/assets/images/newsletters/2014-02-18/nuget-logo.png" style="width:100%;max-width:120px;float:left;">

    <p>Every account comes with a private password-protected NuGet feed aggregating packages from all projects and enabling publishing of your custom packages.</p>
    <p>Projects have separate NuGet feeds with all NuGet packages pushed as artifacts.</p>

    <p style="clear:both;"><a href="/docs/nuget">Read more about NuGet support</a></p>



    <!-- Build matrix -->
    <a id="build-matrix"></a>
    <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">Build matrix</h2>

    <p class="text-center">
        <img src="/assets/images/newsletters/2014-02-18/build-matrix.png" style="width:100%;max-width:395px;float:right;">
    </p>

    <p>
        Easily build/test for multiple configurations. Specify which operating systems, build configurations and platforms you would like to include into build matrix
        and AppVeyor will start a build with multiple jobs for all combinations.
    </p>

    <p>Build matrix supports the following dimensions:</p>
    <ul>
        <li>Operating system</li>
        <li>Environment variables</li>
        <li>Platform, e.g. x86, x64, AnyCPU</li>
        <li>Configuration, e.g. Build, Debug</li>
        <li>Test categories</li>
    </ul>

    <div style="clear:both;"></div>

    <!-- Parallel testing with real-time reporting -->
    <a id="parallel-testing"></a>
    <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">Parallel testing</h2>

    <p class="text-center">
        <img src="/assets/images/newsletters/2014-02-18/parallel-testing.png" style="width:100%;max-width:313px;float:left;">
    </p>

    <p>
        Large projects can contain hundreds and thousands of tests that could run for hours.
        AppVeyor 2.0 allows to split your tests into groups by categories, assemblies or custom criteria and run them as build jobs in parallel thus drastically reducing overall build time.
    </p>

    <p style="clear:both;"><a href="/docs/parallel-testing">Read more about parallel testing</a></p>



    <!-- Deployment -->
    <a id="deployment"></a>
    <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">Deployment</h2>

    <p class="text-center">
        <img src="/assets/images/newsletters/2014-02-18/deploy.png" style="width:100%;max-width:133px;float:right;">
    </p>

    <p>
        AppVeyor 2.0 has scriptless, repetitive one-click deployment to multiple environments!
        Deploy as part of the build or promote releases later - manually or through API.
    </p>
    <p>
        Supported deployment providers:
        <a href="/docs/deployment/web-deploy">Web Deploy</a>,
        <a href="/docs/deployment/ftp">FTP</a>,
        <a href="/docs/deployment/amazon-s3">Amazon S3</a>,
        <a href="/docs/deployment/azure-blob">Azure blob</a>,
        <a href="/docs/deployment/azure-cloud-service">Azure Cloud Services</a>,
        <a href="/docs/deployment/nuget">NuGet</a>,
        <a href="/docs/deployment/agent">Deployment Agent</a>,
        <a href="/docs/deployment/local">Local</a> (for integration testing) and
        Script.
    </p>

    <p>
        <a href="/docs/deployment">Read more about deployment</a>
    </p>


    <!-- Fine-grained control over build configuration -->
    <a id="configuration"></a>
    <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">Fine-grained control over build configuration</h2>

    <img src="/assets/images/newsletters/2014-02-18/yaml.png" style="width:100%;max-width:203px;float:left;">

    <p>
        Great Windows software must provide user interface for any function it has.
        AppVeyor 2.0 follows this tradition and further extends project settings, so you can control build environment and inject custom script logic on any stage of build pipeline
        without ever touching your repository!
    </p>
    <p>
        For command-line gurus or those coming from Linux we added fancy YAML configuration support! Add <b>appveyor.yml</b> with project configuration into root of your repository
        and next time you fork the repo just add a new project in AppVeyor.
    </p>

    <p>
        <a href="/docs/build-configuration">Read more about build configuration</a>
    </p>

    <div style="clear:both;"></div>

    <!-- All new refreshed, responsive and real-time UI -->
    <a id="ui"></a>
    <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">All new refreshed, responsive and real-time UI</h2>

    <p>AppVeyor 2.0 has completely re-designed UI to get results faster and on the go!</p>

    <p>AngularJS, SignalrR and Foundation helped us to build great experience we're really proud of:</p>
    <ul>
        <li>Adaptive layout and improved navigation.</li>
        <li>Real-time build console.</li>
        <li>Real-time reporting of MSBuild errors and warnings.</li>
        <li>Real-time reporting of test results (MSTest, NUnit and xUnit).</li>
        <li>Optimized for mobile screens.</li>
    </ul>


    <!-- Try beta now -->
    <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">Try beta now!</h2>

    <p style="margin: 2rem 0;">
        <a style="font-size: 14pt;color:#fff;text-decoration:none; background-color:#2CBA2C;padding:0.7rem 2rem;border-radius:4px;" href="https://ci-beta.appveyor.com/signup">Sign up for Beta</a>
    </p>

    <p>* Free plan with support of public repositories only. <a href="mailto:team@appveyor.com">Let us know</a> if you need private repositories support or want to play with parallel testing and we'll be happy to enable them for your account.</p>


    <!-- Documentation -->
    <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">Documentation</h2>

    <p>
        <a style="color:#0066CC;font-size:12pt;" href="/docs">/docs</a>
    </p>


    <!-- Dicsussions -->
    <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">Dicsussions</h2>

    <p>
        <a style="color:#0066CC;font-size:12pt;" href="http://help.appveyor.com">http://help.appveyor.com</a>
    </p>

    <p>
        Enjoy the beta! If you have any questions or suggestions - just reply to this email.
        We are looking forward to your feedback!
    </p>

    <p>
        Feodor Fitsner, <br />
        AppVeyor founder and developer
    </p>

    <p style="color: #666;">
        DISCLAIMER: This is the beta and it may contain bugs - do not use it for production projects.
        There might be interruptions as we deploy updates during the day.
        We do not backup beta database and all data will be erased when the beta is over.
    </p>

    <p style="font-size: 85%;color:#777;">AppVeyor Systems Inc. 318 Homer St, Vancouver, BC, V6B 2V2<br />
        To stop receiving emails from AppVeyor please use this <a href="">unsubscribe</a> link.</p>
</div>
