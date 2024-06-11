---
title: Octopus Deploy comes to AppVeyor
---

> This is a guest post by Robert Erez (Twitter: [@no_erez](https://twitter.com/no_erez)), Program Manager at [Octopus Deploy](https://octopus.com/).

<p><img src="/assets/img/posts/octopus-deploy-comes-to-appveyor/blogimage-appveyor.png" alt="Continuous Delivery in the cloud with Octopus and AppVeyor"></p>

<p>The fantastic team at AppVeyor have recently added built-in support for pushing and deploying your projects (.NET, Java, JavaScript etc) with Octopus Deploy. What is Octopus Deploy I hear you ask? Octopus Deploy is a friendly deployment automation tool that makes it easy to automate your application deployments in a fast, repeatable and reliable manner. Octopus takes over where your build server ends, enabling you to easily automate even the most complicated application deployments, whether on-prem or in the cloud. Deploying your application through environments like DEV, TEST and PRODUCTION requires the assurance that the each releases are identical where it counts, but with configuration that can be injected to change from environment to environment.</p>

<p>Let's take a look at how this new partnership between AppVeyor and Octopus Deploy can help you build a complete devliery pipeline in the cloud.</p>

<p>The public <a href="https://github.com/OctopusSamples/RandomQuotes-aspmvc4" rel="nofollow">OctopusSamples/RandomQuotes-aspmvc4</a> repository provides a basic ASP.NET MVC app to display a bunch of wise quotes. Our goal is to set up a delivery pipeline to deploy this website to our IIS server for both a staging and then production environment.</p>

<h2 id="appveyor-octopus-plugin">AppVeyor Octopus Plugin</h2>

<p>Starting with the build of our project, I've added the <code>OctopusSamples/RandomQuotes-aspmvc4</code> GitHub repository as the source of a new AppVeyor project.</p>

<h3 id="build-pack">Build &amp; Pack</h3>

<p>Looking at the build phase, you should notice the new <code>Package Applications for Octopus Deployment</code>:</p>

<p><img src="/assets/img/posts/octopus-deploy-comes-to-appveyor/appveyor_build_step.png" alt="AppVeyor Build Step"></p>

<p>This flag ensures that once the build has completed, the contents are zipped up into a package that can be pushed to Octopus Deploy. Although Octopus will accept any NuGet, zip, or tar package this flag will use the <code>octo.exe</code> tool to <a href="https://octopus.com/docs/packaging-applications/creating-packages/creating-zip-packages">create a zip</a>, named using the application name and version.</p>

<p>Advanced features in Octopus-like <a href="https://octopus.com/docs/deployment-process/channels">Channels</a> allow you to configure custom rules to prevent pre-release versioned packages from getting pushed to production, or ensure that version requirements are met for any linked packages as part of that deployment.</p>

<h3 id="push">Push</h3>

<p>In the <code>Deployment</code> configuration of the AppVeyor project, select the new <code>Octopus Deploy</code> deployment provider. This feature performs all the appropriate calls to pass the package into Octopus and create a related Octopus Release.</p>

<p>In Octopus, a <a href="https://octopus.com/docs/deployment-process/releases">Release</a> ensures that each versioned build artifact will progress through its various environment phases with the same snapshotted deployment process, even if that project process is modified while the release is progressing. Reliable, repeatable deployments are our mantra.</p>

<p><img src="/assets/img/posts/octopus-deploy-comes-to-appveyor/appveyor_build_deployment.png" alt="AppVeyor Deployment Step"></p>

<p>After adding your Octopus Server URL and API Key, tick the <code>Push Packages</code> option to allow AppVeyor to auto-detect the Octopus package built in the previous step. AppVeyor will then push the package to the Octopus <a href="https://octopus.com/docs/packaging-applications/package-repositories/pushing-packages-to-the-built-in-repository">built-in NuGet feed</a>. Although Octopus supports <a href="https://octopus.com/docs/deployment-process/releases/automatic-release-creation">automatic release creation</a> when a new package is available, in this scenario we will trigger it through AppVeyor. Click the <code>Create Release</code> checkbox and provide the name of the project, <code>RandomQuotes</code> which we will later set-up in Octopus and which AppVeyor will programmatically trigger. Octopus was built <a href="https://octopus.com/docs/api-and-integration/api">API first</a> and as such <em>every</em> feature and behavior can be configured and triggered via HTTP endpoints to integrate into <em>any</em> existing CI/CD pipeline!).</p>

<p>With our AppVeyor build pipeline set up, let's now jump into our Octopus Server and get this website deployed.</p>

<h2 id="continuing-deployment-through-octopus">Continuing Deployment Through Octopus</h2>

<p>With a dead simple Octopus Server <a href="https://octopus.com/docs/installation">installation</a> (which, can naturally itself be <a href="https://octopus.com/docs/installation/automating-installation">automated</a>) we are ready to add our new <code>RandomQuotes</code> project through the Octopus Web Portal. <a href="https://octopus.com/cloud">Hosted Octopus</a> is an exciting new option which will be available soon that allows you to use Octopus Deploy without any on-premise infrastructure. We will manage your servers for you!</p>

<h3 id="octopus-projects">Octopus Projects</h3>

<p>After configuring our <a href="https://octopus.com/docs/infrastructure">infrastructure</a>, go to the <code>Projects</code> section, click <code>Add Project</code>, and give it the name <code>RandomQuotes</code> that we specified earlier in our AppVeyor deploy step. This project contains all the deployment steps and configuration variables that define how this application is deployed.</p>

<p>For our simple deployment scenario, we will first go to the <code>Process</code> section and add a new IIS step. Octopus will handle all the complicated interactions to configure our IIS website with just a few inputs from us. There is a wide range of pre-built steps available for use in almost any deployment, so you don't need to write (or support) a single line of code. On top of this, we have an active <a href="https://octopus.com/docs/deployment-process/steps/community-step-templates">community library</a> with 100's more, and you can build and share your <a href="https://octopus.com/docs/deployment-process/steps/community-step-templates">own steps</a> between teams.</p>

<p><img src="/assets/img/posts/octopus-deploy-comes-to-appveyor/octopus_many_steps.png" alt="Octopus Deploy Steps"></p>

<p>You can also include <a href="https://octopus.com/docs/deploying-applications/custom-scripts">custom scripts</a> in a variety of languages if you have a process in mind that doesn't quite fit any of the provided steps.</p>

<h3 id="deployment-step-details">Deployment Step Details</h3>

<p>After selecting the <code>Deploy to IIS</code> step, we will add a few settings to provide Octopus information to enable creating and configuring the IIS website.</p>

<p><img src="/assets/img/posts/octopus-deploy-comes-to-appveyor/octopus_iis_step.png" alt="Octopus Deploy IIS"></p>

<p>Setting the Role under <code>Execution Plan</code> defines which machine(s) the website will be deployed to. A discussion on how Octopus can handle multiple environments each with different machine roles is a discussion in itself which we skip over in this demo. Check out our <a href="https://octopus.com/docs/infrastructure/environments">docs</a> for more details.</p>

<p>Next, we will configure which package will be used for this step. Using the built-in feed (which AppVeyor will be pushing to) we can provide the PackageId <code>RandomQuotes</code>.</p>

<p>Configuring the website itself, which, at its simplest consists of setting just two additional values. The <code>Website name</code> and the <code>AppPool</code>. For this example, we will host both <code>Staging</code> and <code>Production</code> on the same machine (not the best idea for a real project), so we will provide a different website name based on the environment being deployed. The <code>#{Octopus.Environment.Name}</code> section of the name will be replaced at deploy time with the name of the environment.</p>

<h3 id="variables">Variables</h3>

<p>This introduces us to one of the other awesome features of Octopus Deploy, <a href="https://octopus.com/docs/deployment-process/variables">variables</a>. Using a templating syntax, you can provide configuration values, scripts, or even packages that all make use of variables that can be provided from Octopus itself or even user defined! In addition to the <code>Website Name</code> we have also decided to provide a different binding port between  <code>Staging</code> and <code>Production</code>. This value <code>#{CustomPort}</code> is set in the <code>Variables</code> section of the project and can be scoped to a different value based on various combinations of deployment contexts like environment, machine or <a href="https://octopus.com/docs/deployment-patterns/multi-tenant-deployments">tenant</a>, to name just a few.</p>

<p><img src="/assets/img/posts/octopus-deploy-comes-to-appveyor/octopus_variables.png" alt="Octopus Deploy variables"></p>

<p>A common pattern is to define variables in Octopus for the different environments which are replaced in configuration files used by the application at run time. Using them during the deployment process opens up a wide range of advanced scenarios.</p>

<p>For our <code>RandomQuotes</code> project, we have a config transformation file for each of our environments. The <code>Web.Production.config</code> transformation that looks like:</p>

<pre><code class="language-xml">&lt;?xml version="1.0"?&gt;
&lt;configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform"&gt;
  &lt;appSettings&gt;
    &lt;add key="ReleaseVersion" value="#{Octopus.Release.Number}" xdt:Transform="SetAttributes" xdt:Locator="Match(key)"/&gt;
    &lt;add key="EnvironmentName" value="#{Octopus.Environment.Name}" xdt:Transform="SetAttributes" xdt:Locator="Match(key)"/&gt;
    &lt;add key="BackgroundColor" value="#1e8822" xdt:Transform="SetAttributes" xdt:Locator="Match(key)"/&gt;
  &lt;/appSettings&gt;
  &lt;system.web&gt;
    &lt;compilation xdt:Transform="RemoveAttributes(debug)" /&gt;
  &lt;/system.web&gt;
&lt;/configuration&gt;
</code></pre>

<p>Notice the value for <code>ReleaseVersion</code> includes a template pattern During a deployment. (jump to the end of this post if you can't stand the suspense and what to see what this looks like).</p>

<h2 id="commit-and-enjoy">Commit and Enjoy</h2>

<p>We now have our automated CI/CD pipeline configured. When we commit a change to our project, AppVeyor will automatically pick up the changes, build the project, and push it to our Octopus Server. From this point on, Octopus Deploy takes over and deploys it to our staging environment. Once we are happy with this release, we can deploy to production with the click of a button. The same built package that has been tested will then be pushed to our production environment using new values provided by our variables.</p>

<p><img src="/assets/img/posts/octopus-deploy-comes-to-appveyor/logs_together.png" alt="Logs Together"></p>

<p>When the deployment occurs, Octopus will apply any web.config transformations in your project and perform variable replacements so that the same built artifact is run in each environment, ensuring that the code that you test is the code that you run in production.</p>

<p><strong>Staging</strong></p>

<p><img src="/assets/img/posts/octopus-deploy-comes-to-appveyor/app_staging.png" alt="Deployed Staging"></p>

<p>With the staging version of our application available we can inspect and test it before kicking off a deployment to production...</p>

<p><img src="/assets/img/posts/octopus-deploy-comes-to-appveyor/octopus_deploying.png" alt="Running Deployment"></p>

<p><strong>Production</strong></p>

<p><img src="/assets/img/posts/octopus-deploy-comes-to-appveyor/app_production.png" alt="Deployed Production"></p>

<p>Notice how the transformation has been applied changing the colour of the navbar, while the port and other variables have been updated based on the environment being deployed to.</p>

<h2 id="appveyor-octopus-deploy-any-time">AppVeyor + Octopus = Deploy Any Time</h2>

<p>AppVeyor in combination with Octopus Deploy offers a new and exciting way to automate your continuous delivery pipeline in a repeatable, reliable manner. Say goodbye to hand rolled custom scripting solutions which break down at 5pm on a Friday. Flex the powers of AppVeyor's new feature today with a <a href="https://octopus.com/licenses/trial">free Octopus trial</a>.</p>

<p>Happy Deployments!</p>