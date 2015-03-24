---
layout: docs
title: Accessing build worker via Remote Desktop (RDP)
---

# Accessing build worker via Remote Desktop

AppVeyor starts every build on clean dedicated build worker VM. Sometimes the best way to troubleshoot broken build is looking into build VM via Remote Desktop. During the build you have full "administrator" access to that VM and can access it via Remote Desktop (RDP).

To see RDP details to current build worker add this line to `init` phase of your build:

{% highlight yaml %}
init:
- ps: iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-rdp.ps1'))
{% endhighlight %}

Remote Desktop connection details will be displayed and build will continue. Displaying RDP connection during `init` phase helps troubleshooting stuck builds.


If you need to investigate worker on build finish add `$blockRdp = $true;` to display Remote Desktop connection details and pause the build until a special "lock" file on VM desktop is deleted:

{% highlight yaml %}
on_finish:
- ps: $blockRdp = $true; iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/appveyor/ci/master/scripts/enable-rdp.ps1'))
{% endhighlight %}

> Your RDP session is limited by overall build time (60 min).
