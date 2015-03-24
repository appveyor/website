---
layout: docs
title: Status badges
---

# Status badges

A Project status badge is a dynamically generated image displaying the status of the last build. You can put a status badge on the home page of your GitHub project or intranet portal:

![Grunt status](https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva)

You can see a badge image URL and sample markup on the **Badges** tab of project settings. Badge URLs for both private and public projects contain a security token.

## Display badge for specific branch

The default badge URL you see on the Badges tab returns the status of the last build of *any branches*.

To display the status of a specific branch append `/branch/<branch-name>` to that URL, for example:

{% raw %}
    https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva/branch/master
{% endraw %}

To link specific branch with the project add `/branch/<branch-name>` to project URL, for example:

{% raw %}
    https://ci.appveyor.com/project/myaccount/myproject/branch/master
{% endraw %}

## Retina support

To get a hi-res badge image for retina displays append `retina=true` parameter to the badge image URL, as follows:

[https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva?retina=true](https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva?retina=true)

![Grunt status](https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva?retina=true)

## SVG badges

To get badge image in SVG format append `svg=true` parameter to the image URL:

{% highlight markdown %}
[https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva?svg=true](https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva?svg=true)
{% endhighlight %}

![Grunt status](https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva?svg=true)

and of course SVG badge could be easily scaled:

{% highlight html %}
<img src="https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva?svg=true" alt="Project Badge" width="300">
{% endhighlight %}

<img src="https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva?svg=true" alt="Project Badge" width="300">

### Customizing titles

You can customize SVG badge titles for *pending*, *failing* and *passing* states with `pendingText`, `failingText` and `passingText` query parameters respectively.

For example:

{% highlight html %}
https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva?svg=true&passingText=master%20-%20OK
{% endhighlight %}

<img src="https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva?svg=true&passingText=master%20-%20OK">

## Badges for projects with public repositories on GitHub and Bitbucket

You can infer badge URL for **projects with public repositories**, well, provided there is only a single project with this repository in AppVeyor.

The format of badge status image URL:

{% raw %}
    https://ci.appveyor.com/api/projects/status/{github|bitbucket}/{repository}
{% endraw %}

For example: https://ci.appveyor.com/api/projects/status/github/gruntjs/grunt

Optional parameters:

* `branch={name}` - the name of branch to display status for;
* `retina=true` - status image scaled up for retina display;
* `svg=true` - status image in SVG format;

Example:

* [https://ci.appveyor.com/api/projects/status/github/gruntjs/grunt?branch=master&svg=true](https://ci.appveyor.com/api/projects/status/github/gruntjs/grunt?branch=master&svg=true)
* ![Grunt status](https://ci.appveyor.com/api/projects/status/github/gruntjs/grunt?branch=master&svg=true)

**Note**: The `retina` and `svg` parameters are mutually exclusive; only the first parameter specified is used.
