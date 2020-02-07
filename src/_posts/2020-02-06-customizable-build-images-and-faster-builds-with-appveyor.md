---
title: Customizable build images and faster builds with AppVeyor
---

Hosted CI/CD service is all about the right software provided on the base images. For 7 years we have tried our best to always provide you base VM images with the latest software releases and security updates which works great for most of the teams, but not for all.

If you had ever found the images are updated too fast or too slow or you need some specific software on them now we've got a solution for you!

## Customizable images

With customizable images you can install your own software and build dependencies on top of AppVeyor-provided base images (Windows, Linux) and then use the resulting image for further builds.

Custom images enable new use cases that were not possible before in a hosted CI/CD:

* update image software at your own pace - pin specific versions or be on a cutting edge;
* speed up builds drastically by preserving VM state between builds - next-gen build cache;
* securely store certificates and other secrets within the image;
* migrate on-premise CI workflows with proprietary, licensed, manually-installed, legacy software to a hosted CI.

[Check this guide](/docs/custom-images/) to see how easy it is to build your custom image!

## Faster builds

Not only customizable images can speed up your builds. You can now run your builds in a personalized cloud environment with more powerful VMs than in our own in-house cloud.

Out tests show that by using "Medium" instances on Google Cloud you can get 2-3 times faster builds!

[See the pricing and request your trial now](https://ci.appveyor.com/pricing).

Best regards,<br>
AppVeyor team