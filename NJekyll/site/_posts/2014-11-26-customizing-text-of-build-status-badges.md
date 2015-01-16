---
layout: post
title: Custom text on SVG build status badges
---

SVG is great! We've just added a small, but very neat feature that allows you customizing badge text.

This looks really great for batch-specific status badges put in one line (these are statuses for [Grunt.js project](https://ci.appveyor.com/project/gruntjs/grunt/history)):

<img src="https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva?svg=true&passingText=master%20-%20OK"> <img src="https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva/branch/osx-travis?svg=true&failingText=osx-travis%20-%20Fails"> <img src="https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva/branch/legacy-log?svg=true&passingText=legacy-log%20-%20OK">

To customize SVG badge titles for *pending*, *failing* and *passing* states add `pendingText`, `failingText` and `passingText` query parameters respectively.

For example:

    https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva?svg=true&passingText=master%20-%20OK&failingText=master%20-%20Fails

Read more about status badges in [AppVeyor documentation](http://www.appveyor.com/docs/status-badges).

Enjoy!