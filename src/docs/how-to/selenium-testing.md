---
layout: docs
title: Selenium testing
---

<!-- markdownlint-disable MD022 MD032 -->
# Selenium testing
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## .NET

### Firefox

AppVeyor build worker images are used to have the latest version of Firefox browser installed which at the time of writing was `47.0.1`.

You use `FirefoxDriver` class from [`Selenium WebDriver` library](https://www.nuget.org/packages/Selenium.WebDriver/) to run tests on Firefox.

#### Firefox 46 and below

If you need to run your tests against earlier versions of Firefox we recommend using [Chocolatey](https://chocolatey.org/packages/Firefox) for installing Firefox 46 and below.
Just add this line to `install` section of your `appveyor.yml` to remove the current Firefox and install Firefox 46.0.1:

```yaml
install:
  - choco install firefox --version 46.0.1
```

In your tests you create `FirefoxDriver` as simply:

```javascript
var driver = new FirefoxDriver();
```

#### Firefox 47 and above

Starting from Firefox 47.x the way WebDriver works has changed.

First, it requires an additional executable called [geckodriver](https://github.com/mozilla/geckodriver/releases) which must be available on `PATH`.
All AppVeyor build workers already have this executable in `C:\Tools\WebDriver` directory (`geckodriver.exe` and `wires.exe` files), so you don't need to worry about that.

Second, `FirefoxDriver` should be created like in the following example to notify WebDriver that geckodriver should be used:

```javascript
var driverService = FirefoxDriverService.CreateDefaultService();
driverService.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
driverService.HideCommandPromptWindow = true;
driverService.SuppressInitialDiagnosticInformation = true;
driver = new FirefoxDriver(driverService, new FirefoxOptions(), TimeSpan.FromSeconds(60));
```

Please do not forget to **close the browser and dispose the driver** by `driver.Quit();` in test finalize steps. Otherwise build process can hung.

## Additional resources

* [WebDriver FAQ](https://developer.mozilla.org/en-US/docs/Mozilla/QA/Marionette/WebDriver) on MDN website.
