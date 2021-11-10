---
title: 'Visual Studio 2022 image is now available'
---

A new `Visual Studio 2022` image with Visual Studio 2022 and .NET 6 RTM is now available to all AppVeyor accounts:

* The image is based on Windows Server 2019.
* The [software on the image](https://www.appveyor.com/docs/windows-images-software/) is mostly identical to the one installed on "Visual Studio 2019" image.
* WiX toolset is not installed on the image ([related issue](https://github.com/wixtoolset/issues/issues/6493)).
* Visual Studio 2022 is now 64-bit with installation location at `C:\Program Files\Microsoft Visual Studio\2022\Community`.
* Python 3.10 is now default in `PATH`.
* Ruby 3.0 is now default in `PATH`.

## New and updated software

* Visual Studio 2022 Community Edition
* .NET Core 3.1.415, 5.0.403, 6.0.100
* Git 2.33.1
* Git LFS 3.0.2
* JDK 16.0.1, JDK 17.0.1
* PowerShell Core 7.2.0

Give `Visual Studio 2022` image a try and let us know how it worked for you!

Best regards,<br>
AppVeyor team