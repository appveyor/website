---
title: Artifacts retention policy
---

Artifacts functionality has been working great for our customers since we introduced it in 2013, but we collected a huge amount of artifacts which are persisting in cloud storage.

Through talking to many customers we’ve identified that, after some period of time, storing old artifacts is unnecessary.

Indeed, once the app is deployed or a release package uploaded to external storage, its underlying artifact is usually no longer needed (except for those rare moments when some previous/stable release has to be re-deployed!)

To reduce AppVeyor hosting costs and eliminate any unnecessary waste of cloud resources we decided to introduce an artifacts retention policy.

The policy states that build artifacts and NuGet packages of paid accounts older than 6 months and free accounts older than 3 months will be permanently removed from AppVeyor artifact storage.

This policy will take effect on June 7, 2018.

If you have custom requirements please let us know and we'll discuss your needs.

Best regards,<br>
AppVeyor team

Follow us on Twitter: [@appveyor](https://twitter.com/appveyor)