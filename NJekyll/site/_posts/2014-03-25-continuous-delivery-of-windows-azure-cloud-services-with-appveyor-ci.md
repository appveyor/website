---
layout: post
title: Continuous Delivery of Windows Azure Cloud Services with AppVeyor CI
---

<h2>Introduction</h2>
AppVeyor is Continuous Integration service for Windows developers to securely build and test code in parallel and deploy successful bits to on-premise or cloud environments.

In this tutorial we'll guide you through the process of setting up a continuous delivery process for sample Azure Cloud Service (Azure CS) application starting from a code push to a repository and finishing with deployment of successful build to Azure.
<h2>Note to Global Windows Azure Bootcamp attendees</h2>
On Saturday, March 29, 2014 <a href="http://global.windowsazurebootcamp.com/">Global Windows Azure Bootcamp (GWAB)</a> will take place in 141 locations across the globe. If you are not registered yet go <a href="http://global.windowsazurebootcamp.com/locations/">find a location near you</a> and do that. AppVeyor CI is one of the sponsors of this event and during that time we will be giving <strong>2 free months</strong> with the purchase of <a href="http://www.appveyor.com/pricing">any AppVeyor plan</a> to all GWAB attendees.

AppVeyor CI has tight relationship with Windows Azure platform. First of all, AppVeyor is built for Azure and it uses Azure IaaS to run your builds on dedicated virtual machines. Second, AppVeyor provides complete Continuous Delivery cycle for Azure projects, i.e. building, testing, packaging and deploying your web applications and Azure Cloud Services. GWAB training classes is a wonderful place to try AppVeyor and setup super-simple continuous integration for your lab project.
<h2>Sample project on GitHub</h2>
We've created a simple Azure Cloud Service solution created in Visual Studio 2013 and consisting of a WebRole and xUnit test projects. You can find <a href="https://github.com/FeodorFitsner/azure-cs-demo">sample project repository on GitHub</a>.

<img src="/site/_posts/images/azure-cs-ci/repository2.png" alt="" />

Note, that we don't have "NuGet Package Restore" enabled for VS solution (no .nuget folder in repository). This is not necessary in AppVeyor environment - below you'll see how to do that.
<h2>Sign up for AppVeyor account</h2>
If you don't have AppVeyor account yet you should definitely get one! Go to <a href="https://ci.appveyor.com/signup">https://ci.appveyor.com/signup</a> and use "GitHub" button to sign up for Free plan which allows you building public repositories.

<img  src="/site/_posts/images/azure-cs-ci/signup2.png" alt="signup" />
<h2>Add new project</h2>
Click New project and select GitHub repository:

<img src="/site/_posts/images/azure-cs-ci/new-project2.png" alt="" />

AppVeyor will automatically configure webhook for the repo to start a new build on every push.
<h2>Enable NuGet packages restore</h2>
To enable restore of NuGet packages during the build open project settings and add "nuget restore" command into <strong>before build script</strong>:

<img src="/site/_posts/images/azure-cs-ci/project-settings1.png" alt="" />
<h2>Start new build</h2>
Run <strong>New build</strong> and see its progress in a real-time build console:

<img src="/site/_posts/images/azure-cs-ci/build-console1.png" alt="" />

If you click Artifacts tab upon build completion you will see that AppVeyor automatically detected and packaged Azure Cloud Service project along with its configuration:

<img src="/site/_posts/images/azure-cs-ci/build-artifacts1.png" alt="" />

In just a few minutes we have a pretty decent Continuous Integration process for our Azure Cloud Service!
<h2>Add new Azure Cloud Service environment</h2>
Now let's deploy our "experimental" app to Windows Azure. Go to Environments and add new "Azure Cloud Service" environment:

<img src="/site/_posts/images/azure-cs-ci/azure-cs-settings1.png" alt="" />

The form requires 3 prerequisites:
<ul>
    <li><span style="font-style:inherit;line-height:1.625;">Your Windows Azure account </span><strong style="font-style:inherit;line-height:1.625;">subscription details</strong> (Subscription ID and certificate)<span style="font-style:inherit;line-height:1.625;">.</span></li>
    <li><strong>Storage account</strong> for uploading Azure CS package (.cspkg file produced during the build) and then deploying from it.</li>
    <li><strong>Cloud Service</strong> to deploy to.</li>
</ul>
<h2>Subscription details</h2>
<a href="https://manage.windowsazure.com/publishsettings/Index?client=vs&amp;SchemaVersion=1.0">Download Azure account publishing profile</a> and open it in text editor. Copy subscription ID and Base64 encoded subscription certificate.

<img src="/site/_posts/images/azure-cs-ci/certificate.png" alt="certificate" />
<h2>Start new deployment</h2>
Now kick off new deployment of <strong>azure-cs-demo</strong> project to <strong>azure demo</strong> environment:

<img src="/site/_posts/images/azure-cs-ci/new-deployment1.png" alt="" />

Select the build to deploy:

<img src="/site/_posts/images/azure-cs-ci/new-deployment-select-build1.png" alt="" />

Observe the progress of deployment in the real-time console:

<img src="/site/_posts/images/azure-cs-ci/azure-deployment-complete1.png" alt="" />

Azure Cloud Service package from build 1.0.2 artifacts has been deployed and you can see the results in Azure Portal:

<img src="/site/_posts/images/azure-cs-ci/azure-portal-deployment1.png" alt="" />
<h2>Deploying as part of the build</h2>
Now after we triggered deployment manually from UI we'd like to completely automate the process and deploy during successful build. To setup deployment to azure demo environment open "Deployment" tab of project properties, add new "Environment" deployment and specify "azure demo" as environment name:

<img src="/site/_posts/images/azure-cs-ci/project-deployment1.png" alt="" />

Done! Next time you push your changes into GitHub repository or start a new build on UI AppVeyor will build solution, run tests and deploy to Azure.

Enjoy!