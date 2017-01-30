---
title: Build worker improvements
---

While we are working on some new exciting stuff we continue improving things to make your AppVeyor
experience even more smooth and streamlined. One of those recent improvements is a new build worker
provisioning engine. Every build in AppVeyor runs on a dedicated virtual machine hosted in
[Microsoft Azure](https://azure.microsoft.com/en-us/) (yes, now it's called "Microsoft Azure").
Azure is a great platform - their VMs are considerably faster than competitors' and creation of a new
VM is a blast (usually around 3 minutes). Simplified build flow is the following:

* Build starts
* A new VM is provisioning from "master" image (VHD) or being taken from "pre-heated" cache.
* Build Agent installed on VM runs the build and "talks" to AppVeyor via Web Sockets (SignalR).
* VM is being deleted after the build

## How it was before

We were creating a new VM and then passing build details and starting Build Agent service through
PowerShell remoting. That approach had a number of disadvantages:

* Open WinRM endpoint accessible from the same network.
* Constantly "pinging" starting up VM until it's ready to accept WinRM connection. That solution had very high fault rate.
* Once assigned to a build it was hard to switch build to another VM if the first one is unhealthy.


## New approach

The new provisioning mechanism works "inside out", i.e. Build Agent is started on VM boot and is
"listening" for incoming command from AppVeyor using Web Sockets.

![build-no-provisioning](/assets/img/posts/build-workers/build-no-provisioning.png)

This new approach gives a **number of benefits**:

* Build worker **VM is completely isolated**. It doesn't have an external IP and no endpoints are configured - all inbound connections are prohibited. Even if you know its internal IP address (which is not a secret if you read NIC properties) you still can't knock it as it sits in a separate private network (Azure cloud service).
* No more starring at "Provisioning build worker..." or "pending" builds. Now it's just "Queued" and then "Running". We got some feedback that knowing VM is in provisioning state doesn't feel very good and make customers nervous :)
* If there is pre-heated VM available (we maintain between 3-4 VMs) your **build starts almost instantly**!
* If there are no pre-heated VMs it would take around **3 minutes to provision new VM** from scratch.
* Whenever AppVeyor detects there is unhealthy VM worker it will re-schedule the build to another healthy instance, so generally it means being a bit longer in "Queued" state.

We also optimized worker "master" image (VHD) itself to make sure only minimum set of services is
enabled to **boot fast, eliminate lags and free more memory for your builds**. Of course,
you can still configure required services such as [IIS and SQL Server databases](/docs/services-databases/).

Oh, and we introduced **Windows Server 2012 R2** image which has almost identical configuration to
Windows Server 2012 (except it doesn't have VS 2010 installed).

Enjoy!
