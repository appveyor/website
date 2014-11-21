---
layout: post
title: NuGet support in AppVeyor CI
---

<em><strong>NOTE</strong>: NuGet support is available in AppVeyor CI 2.0 which is currently in beta. Please <a title="AppVeyor 2.0: dedicated build VMs, parallel testing, NuGet, deployment and more" href="http://blog.appveyor.com/2014/02/19/appveyor-20-dedicated-build-vms-parallel-testing-nuget-deployment/">see this post</a> for AppVeyor 2.0 announcement and sign up information.</em>

AppVeyor CI has native NuGet support which becomes de-facto a packaging standard for .NET libraries and applications.

Every AppVeyor account comes with following built-in NuGet feeds:

<ul>
    <li><strong>Account feed</strong> - password-protected feed aggregating NuGet packages from all projects with support of publishing of your own packages</li>
    <li><strong>Project feeds</strong> - collect all NuGet packages pushed to artifacts during the build</li>
</ul>

<span style="color:#000000;font-weight:bold;font-style:inherit;line-height:1.625;">Account NuGet feed</span>

Account NuGet feed aggregates packages from all project feeds and allows publishing of your custom packages. All account feeds are password-protected. You can find account feed URL and its API key on <strong>Account → NuGet</strong> page:

<a href="/site/_posts/images/nuget-support/nuget-account.png"><img class="alignnone size-full wp-image-234" alt="nuget-account" src="/site/_posts/images/nuget-support/nuget-account.png" width="584" height="305" /></a>

You can use your AppVeyor account email/password to access password-protected NuGet feeds although we recommend creating a separate user account just for these purposes (<strong>Account → Team</strong>).

<blockquote>If you use GitHub or BitBucket button to login AppVeyor you can reset your AppVeyor account password usingForgot password link.</blockquote>

For publishing your own packages to account feed use the command:

<pre><code> nuget push &lt;your-package.nupkg&gt; -ApiKey &lt;your-api-key&gt; -Source &lt;feed-url&gt;
</code></pre>

<ul>
<li>Replace <code>&lt;your-api-key&gt;</code> and <code>&lt;feed-url&gt;</code> with values from Account <strong>→</strong> NuGet page.</li>
</ul>

<h2>Project NuGet feed</h2>

Project feed collects all NuGet packages pushed to artifacts during the build. Project feed is password-protected if the project references private GitHub or BitBucket repository; otherwise project feed has public access:

<a href="/site/_posts/images/nuget-support/nuget-project-feed1.png"><img class="alignnone size-full wp-image-241" alt="nuget-project-feed" src="/site/_posts/images/nuget-support/nuget-project-feed1.png" width="584" height="296" /></a>

<h3>Automatic publishing of NuGet projects</h3>

You can enable automatic publishing of NuGet packages during the build on <strong>Build</strong> tab of project settings. When it is enabled AppVeyor calls <code>nuget pack</code> for every project in the solution having <code>.nuspec</code> file in the root and then publishes NuGet package artifact in both project and account feeds.

To generate <code>.nuspec</code> file for your project run the following command from project root directory:

<pre><code>nuget spec
</code></pre>

<h3>Pushing NuGet packages from build scripts</h3>

To push NuGet package as artifact and publish it in both project and account feeds use this command anywhere in your build script:

<pre><code>appveyor PushArtifact &lt;your-nugetpackage.nupkg&gt;
</code></pre>

<blockquote>When you delete a project in AppVeyor its corresponding NuGet feed is deleted, however all NuGet packages from that feed remain published in account feed.</blockquote>

<h2>Configuring private NuGet feed on your development machine</h2>

<h3>Visual Studio</h3>

To configure custom feed in Visual Studio open <strong>Tools → Options → Package Manager → Package Sources</strong> and add new feed.

When you first open Manage NuGet packages dialog you will be presented with a dialog box asking for credentials:

<a href="/site/_posts/images/nuget-support/nuget-visualstudio-auth.png"><img class="alignnone size-full wp-image-238" alt="nuget-visualstudio-auth" src="/site/_posts/images/nuget-support/nuget-visualstudio-auth.png" width="584" height="389" /></a>

<h3>Command line</h3>

To configure private NuGet feed on your development machine run this command:

<pre><code>nuget sources add -Name &lt;friendly-name&gt; -Source &lt;feed-url&gt; -UserName &lt;username&gt; -Password &lt;pass</code></pre>

<h2>Configuring private feed to work with NuGet Package Restore</h2>

You may use account feed to publish your external packages that can be further referenced during AppVeyor builds.

To configure AppVeyor project to use private NuGet feed during build you can use the following approach:

<ol>
    <li>Create a separate AppVeyor account for accessing NuGet feed.</li>
    <li>On <strong>Environment</strong> tab of project settings add two environment variables <code>nuget_user</code> and <code>nuget_password</code>:<a href="/site/_posts/images/nuget-support/nuget-environment-variables.png"><img class="alignnone size-full wp-image-239" alt="nuget-environment-variables" src="/site/_posts/images/nuget-support/nuget-environment-variables.png" width="584" height="114" /></a></li>
    <li>Into <strong>Install</strong> script box add this command:
<pre><code>nuget sources add -Name MyAccountFeed -Source &lt;feed-url&gt; -UserName %nuget_user% -Password %nuget_password%</code></pre>
</li>
</ol>

<ul>
<li>Replace <code>&lt;feed-url&gt;</code> with URL of your account feed.</li>
</ul>

<h2>Explicit NuGet package restore before the build</h2>

To restore Visual Studio solution NuGet packages before build add this command to <strong>Before build script</strong> box on <strong>Build</strong> tab of project settings (provided your <code>.sln</code> file or <code>packages.config</code> are in the root of repository):

<pre><code>nuget restore
</code></pre>

otherwise if project solution is in sub-directory:

<pre><code>nuget restore &lt;solution-folder&gt;\&lt;solution.sln&gt;</code></pre>