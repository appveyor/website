---
layout: docs
title: Selenium testing
---

# Selenium testing

<!--TOC-->

## .NET

### FireFox

AppVeyor build worker images are used to have the latest version of FireFox browser installed which at the time of writing was `47.0.1`.

You use `FirefoxDriver` class from [`Selenium WebDriver` library](https://www.nuget.org/packages/Selenium.WebDriver/) to run tests on FireFox.

**FireFox 46 and below**

If you need to run your tests against earlier versions of FireFox we recommend using [Chocolatey](https://chocolatey.org/packages/Firefox) for installing FireFox 46 and below.
Just add this line to `install` section of your `appveyor.yml` to remove the current FireFox and install FireFox 46.0.1:

    install:
    - choco install firefox -version 46.0.1

In your tests you create `FirefoxDriver` as simply:

    var driver = new FirefoxDriver(); 

**FireFox 47 and above**

Starting from FireFox 47.x the way WebDriver works has changed.

First, it requires an additional executable called [geckodriver](https://github.com/mozilla/geckodriver/releases) which must be available on `PATH`.
All AppVeyor build workers already have this executable in `C:\Tools\WebDriver` directory (`geckodriver.exe` and `wires.exe` files), so you don't need to worry about that.

Second, `FirefoxDriver` should be created like in the following example to notify WebDriver that geckodriver should be used:

    var driverService = FirefoxDriverService.CreateDefaultService();
    driverService.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
    driverService.HideCommandPromptWindow = true;
    driverService.SuppressInitialDiagnosticInformation = true;
    driver = new FirefoxDriver(driverService, new FirefoxOptions(), TimeSpan.FromSeconds(60)); 


## Additional resources

* [WebDriver FAQ](https://developer.mozilla.org/en-US/docs/Mozilla/QA/Marionette/WebDriver) on MDN website.
