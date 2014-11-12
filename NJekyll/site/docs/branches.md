---
layout: docs
title: Branches and Tags
---

# Branches and Tags

* [White- and blacklisting](#white-and-blacklisting)
* [Conditional build configuration](#conditional-build-configuration)
* [Build on tags (GitHub only)](#build-on-tags)

AppVeyor has built-in multi-branch support.

**Default branch** which can be specified on the General tab of project settings is built whenever a new build is started from *Projects UI*, *schedule* or *API*. When you do a push to the repository AppVeyor will start a new build of the branch in the last commit of the push data.



<a id="white-and-blacklisting"></a>
## White- and blacklisting

All branches are built by default. You can either [manually skip a build](/docs/how-to/skip-build) or setup included/excluded branches on the **General** tab of project settings or in `appveyor.yml`:

To specify the list of allowed branches:

	branches:
	  only:
	    - master
	    - production

To specify the list of branches that must be ignored:

	  except:
	    - /dev.*/      # You can use Regex expression to match multiple branch name(s)
	    - playground

> `gh-pages` branch is always excluded unless explicitly added in "only" list.



<a id="conditional-build-configuration"></a>
## Conditional build configuration

If you use [git flow](http://nvie.com/posts/a-successful-git-branching-model/) you may want to have a different build configuration (e.g. deploying to a different environment) in a feature branch. Changing `appveyor.yml` in a feature branch becomes an issue when you merge it into master overriding `appveyor.yml` and breaking master builds.

To solve this problem AppVeyor allows having multiple per-branch configurations in a single `appveyor.yml`.

Multiple configurations are defined as a list with `branches` section in every item that:

	
<pre style="background:#f9f9f9;color:#080808"><span style="color:#5a525f;font-style:italic"># configuration for "master" branch</span>
<span style="color:#5a525f;font-style:italic"># build in Release mode and deploy to Azure</span>
<span style="color:#794938">-</span>
  <span style="color:#bf4f24">branches</span>:
    <span style="color:#bf4f24">only</span>:
      <span style="color:#0b6125">- <span style="color:#0b6125">master</span></span>

  <span style="color:#0b6125"><span style="color:#bf4f24">configuration<span style="color:#794938">:</span></span> <span style="color:#0b6125">Release</span></span>
  <span style="color:#bf4f24">deploy</span>:
    <span style="color:#0b6125"><span style="color:#bf4f24">provider<span style="color:#794938">:</span></span> <span style="color:#0b6125">AzureCS</span></span>
    ...

<span style="color:#5a525f;font-style:italic"># configuration for all branches starting from "dev-"</span>
<span style="color:#5a525f;font-style:italic"># build in Debug mode and deploy locally for testing</span>
<span style="color:#794938">-</span>
  <span style="color:#bf4f24">branches</span>:
    <span style="color:#bf4f24">only</span>:
      <span style="color:#0b6125">- <span style="color:#0b6125">/dev-.*/</span></span>

  <span style="color:#0b6125"><span style="color:#bf4f24">configuration<span style="color:#794938">:</span></span> <span style="color:#0b6125">Debug</span></span>
  <span style="color:#bf4f24">deploy</span>:
    <span style="color:#0b6125"><span style="color:#bf4f24">provider<span style="color:#794938">:</span></span> <span style="color:#0b6125">Local</span></span>
    ...

<span style="color:#5a525f;font-style:italic"># "fall back" configuration for all other branches</span>
<span style="color:#5a525f;font-style:italic"># no "branches" section defined</span>
<span style="color:#5a525f;font-style:italic"># do not deploy at all</span>
<span style="color:#794938">-</span>
  <span style="color:#0b6125"><span style="color:#bf4f24">configuration<span style="color:#794938">:</span></span> <span style="color:#0b6125">Debug</span></span>
</pre>


Unlike white- and blacklisting `branches` section here works like a selector, not a filter. Configuration selection algorithm is the following:

- Check configurations with `branches/only` section defined. If branch is found in configuration's `only` section use this configuration.
- Check configurations with `branches/except` section defined. If branch is NOT found in configuration's `except` section use this configuration.
- Check configurations WITHOUT `branches` section. If such configuration found use it.
- If all previous steps fail build is not run.


<a id="build-on-tags"></a>
## Build on tags (GitHub only)

By default AppVeyor starts a new build on any push to GitHub whether it's regular commit or a new tag. Repository tagging frequently used to trigger deployment.

AppVeyor sets `APPVEYOR_REPO_TAG` environment variable to distinguish regular commits from tags - the value is `True` if tag was pushed; otherwise it's `False`. When it's `True` the name of tag is stored in `APPVEYOR_REPO_BRANCH`.

You can use `APPVEYOR_REPO_TAG` variable to trigger deployment on tag only, for example:

	- provider: Environment
	  name: production
	  on:
	    branch: master
        appveyor_repo_tag: true

You can disable builds on new tags through UI (General tab of project settings) or in `appveyor.yml`:

    skip_tags: true