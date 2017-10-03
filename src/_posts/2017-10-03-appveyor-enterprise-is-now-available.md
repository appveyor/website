---
title: AppVeyor Enterprise is now available
---

Today we are excited to announce immediate availability of AppVeyor Enterprise!

AppVeyor is being used by more than 50,000 developers and we’ve run 10,000,000+ builds. As many companies had been asking about the possibility of installing AppVeyor behind their firewalls we started looking into just that. AppVeyor was originally born as a cloud product and tied to Azure. During the past few years we’ve been refactoring and maturing the AppVeyor codebase to fit into an on-premise scenario, and it is finally here!

AppVeyor Enterprise benefits:

* **Clean isolated environment for every build.** Each new build runs on a new pristine VM which is immediately deleted afterwards to keep your costs low. This is the feature that we’ve been evangelizing from the very beginning and which we think sets AppVeyor apart from other products.

* **Multiple clouds to provision build VMs.** You can run builds in different clouds (Azure, Google, Amazon) or on a Hyper-V server. On [ci.appveyor.com](https://ci.appveyor.com) we’re distributing load between clouds and we have multi-zone failover configurations and switch cloud providers depending on build image. All this cool stuff is available to you now!

* **Works with any language/stack.** You can easily configure build projects with versioned AppVeyor YAML. We’ve gathered so much knowledge from the AppVeyor community and now you can use all these recipes for your own projects!

* **Works with your source control.** AppVeyor can fetch the code from GitHub, Bitbucket, GitLab, Kiln or any other regular Git, Mercurial or SVN repository.

* **Built-in deployment.** With AppVeyor Enterprise you can build once and promote the same package to multiple deployment environments. Hosted AppVeyor has been used by customers more than 1,200,000 times to deploy their applications.

* **Pricing for the cloud era.** With AppVeyor Enterprise you can complete your test suite faster and with less cost. You get unlimited clouds, unlimited concurrent jobs, unlimited build and deploy agents. [Pricing](https://www.appveyor.com/pricing/) is based on the size of your team and starts from $300/month per 10 users.

[Request your free trial](https://ci.appveyor.com/enterprise-trial) of AppVeyor Enterprise today!

Best regards,<br>
AppVeyor team

Follow us on Twitter: [@appveyor](https://twitter.com/appveyor)