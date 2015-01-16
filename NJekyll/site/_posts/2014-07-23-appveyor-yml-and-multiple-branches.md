---
layout: post
title: appveyor.yml and multiple branches
---

<h2>The problem</h2>
If you use <a href="http://nvie.com/posts/a-successful-git-branching-model/">git flow</a> you may want to have a different build configuration (e.g. deploying to a different environment) in a feature branch. Changing <code>appveyor.yml</code> in a feature branch becomes an issue when you merge it into master overriding <code>appveyor.yml</code> and breaking master builds.
<h2>The solution</h2>
To solve this problem AppVeyor allows having multiple per-branch configurations in a single <code>appveyor.yml</code>.

Multiple configurations are defined as a <strong>list</strong> with <code>branches</code> section in every item that:
<pre style="background:#f9f9f9;color:#080808;"><span style="color:#5a525f;font-style:italic;"># configuration for "master" branch</span>
<span style="color:#5a525f;font-style:italic;"># build in Release mode and deploy to Azure</span>
<span style="color:#794938;">-</span>
  <span style="color:#bf4f24;">branches</span>:
    <span style="color:#bf4f24;">only</span>:
      <span style="color:#0b6125;">- <span style="color:#0b6125;">master</span></span>

  <span style="color:#0b6125;"><span style="color:#bf4f24;">configuration<span style="color:#794938;">:</span></span> <span style="color:#0b6125;">Release</span></span>
  <span style="color:#bf4f24;">deploy</span>:
    <span style="color:#0b6125;"><span style="color:#bf4f24;">provider<span style="color:#794938;">:</span></span> <span style="color:#0b6125;">AzureCS</span></span>
    ...

<span style="color:#5a525f;font-style:italic;"># configuration for all branches starting from "dev-"</span>
<span style="color:#5a525f;font-style:italic;"># build in Debug mode and deploy locally for testing</span>
<span style="color:#794938;">-</span>
  <span style="color:#bf4f24;">branches</span>:
    <span style="color:#bf4f24;">only</span>:
      <span style="color:#0b6125;">- <span style="color:#0b6125;">/dev-.*/</span></span>

  <span style="color:#0b6125;"><span style="color:#bf4f24;">configuration<span style="color:#794938;">:</span></span> <span style="color:#0b6125;">Debug</span></span>
  <span style="color:#bf4f24;">deploy</span>:
    <span style="color:#0b6125;"><span style="color:#bf4f24;">provider<span style="color:#794938;">:</span></span> <span style="color:#0b6125;">Local</span></span>
    ...

<span style="color:#5a525f;font-style:italic;"># "fall back" configuration for all other branches</span>
<span style="color:#5a525f;font-style:italic;"># no "branches" section defined</span>
<span style="color:#5a525f;font-style:italic;"># do not deploy at all</span>
<span style="color:#794938;">-</span>
  <span style="color:#0b6125;"><span style="color:#bf4f24;">configuration<span style="color:#794938;">:</span></span> <span style="color:#0b6125;">Debug</span></span>
</pre>
Unlike white- and blacklisting <code>branches</code> section here works like a selector, not a filter. Configuration selection algorithm is the following:
<ul>
    <li>Check configurations with <code>branches/only</code> section defined. If branch is found in configuration's <code>only</code> section use this configuration.</li>
    <li>Check configurations with <code>branches/except</code> section defined. If branch is NOT found in configuration's <code>except</code>section use this configuration.</li>
    <li>Check configurations WITHOUT <code>branches</code> section. If such configuration found use it.</li>
    <li>If all previous steps fail build is not run.</li>
</ul>
Enjoy!