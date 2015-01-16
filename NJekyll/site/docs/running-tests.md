---
layout: docs
title: Running tests
---

# Running tests

* [Selecting assemblies to test](#assemblies-to-test)
* [Selecting categories to test](#categories-to-test)
    * [Visual Studio tests](vstest-categories)
    * [xUnit](xunit-categories)
    * [NUnit](nunit-categories)
    * [MSpec](mspec-categories)
* [Calling test runners from your custom scripts](#calling-test-runners)
* [Pushing real-time test results to build console](#test-results)

AppVeyor has tight integration with the following testing frameworks:

* [Visual Studio Unit Testing framework (MSTest)](http://msdn.microsoft.com/en-us/library/dd264975.aspx)
* [NUnit](http://www.nunit.org/)
* [xUnit](https://github.com/xunit/xunit)
* [Machine.Specifications (MSpec)](https://github.com/machine/machine.specifications)

This does not, however, mean you can't run your own favourite testing frameworks (like Jasmine, JSLint, Pester) within AppVeyor, but rather that AppVeyor provides **automatic discovery, execution and real-time reporting** for the frameworks listed above.

In most cases you are good to go with the default **Auto** testing mode. This mode tells AppVeyor to recursively search the build folder for test assemblies referencing known frameworks, run tests with corresponding test runners, and push results back to the build console.

<a id="assemblies-to-test"></a>
## Selecting assemblies to test

By default, **Auto** mode scans the entire build folder. For large projects this could be a time-consuming operation.

In the **Test assemblies** box you can specify either assembly file name or wildcard.

For example, to include tests from **all** assemblies with specific name:

    test-assembly.dll

this is the same as:

    **\*.test-assembly.dll

which means, search build folder directories recursively for `test-assembly.dll` assemblies.

To match an assembly in a specific folder:

    **\bin\debug\test-assembly.dll

or if build configuration is set in environment variable:

    **\bin\$(configuration)\test-assembly.dll

> You can substitute any existing environment variable

To match all assemblies ending with `.tests.dll`:

    **\*.tests.dll

Configuring tests in `appveyor.yml`:

    test:
      # assemblies to test - optional
      assemblies:
        - test-assembly-A.dll
        - test-assembly-B.dll

      # categories to test - optional
      categories:
        - A
        - B

    # run custom scripts before tests
    before_test:
      - script 1
      - script 2

    # run custom scripts after tests
    after_test:
      - script 1
      - script 2

> If assembly path in `appveyor.yml` starts with `*` surround the value with single quotes to make YAML parser happy:
>
    test:
      assemblies:
        - '**\*.tests.dll'

See [appveyor.yml reference](/docs/appveyor-yml) for complete syntax.


<a id="categories-to-test"></a>
## Selecting categories to test

By default, AppVeyor runs all tests from found assemblies.

You can include or exclude certain test categories from tests run on **Tests** tab of project settings on in `appveyor.yml`.

To run tests from *only* specified categories:

    test:
      categories:
        only:
          - A
          - B

To run tests from all categories *except* specified ones:

    test:
      categories:
        except:
          - A
          - B

<a id="vstest-categories"></a>
### Visual Studio unit tests (C#)

Applying category to a test method:

    [TestMethod, TestCategory("A")]
    public void TestA()
    {
    }

<a id="xunit-categories"></a>
### xUnit (C#)

Applying category to a test method:

    [Fact, Trait("Category", "A")]
    public void MyTest()
    {
    }

<a id="nunit-categories"></a>
### NUnit (C#)

Applying category to a test fixture:

    [TestFixture, Category("LongRunning")]
    public class LongRunningTests
    {
    }

Applying category to a test method:

    [TestFixture]
    public class SuccessTests
    {
        [Test, Category("Long")]
        public void VeryLongTest()
        {
        }
    }

<a id="mspec-categories"></a>
### MSpec (C#)

Applying category to MSpec test with `Tags` attribute:

    [Subject(typeof(Account), "Funds transfer")]
    [Tags("failure")]
    public class when_transferring_between_two_accounts : AccountSpecs
    {
    }


<a id="calling-test-runners"></a>
## Calling test runners from your custom scripts

The AppVeyor build environment includes runners for MSTest, NUnit and xUnit frameworks that are integrated with the build console to push real-time results while running tests.

You can install and use your own build runners for frameworks above, communication with the build console must be done via text output from those runners.

<a id="vstest"></a>
### Visual Studio Unit Testing framework

To run unit tests for Visual Studio test framework with real-time reporting use command:

    vstest.console /logger:Appveyor <assembly> [options]

`[options]` are standard [vstest.console.exe command-line options](http://msdn.microsoft.com/en-us/library/jj155796.aspx).


<a id="nunit"></a>
### NUnit

To run NUnit tests with real-time reporting use command:

    nunit-console <assembly> [options]

or for x86 assemblies:

    nunit-console-x86 <assembly> [options]


<a id="xunit"></a>
### xUnit

To run xUnit tests with real-time reporting use command:

    xunit.console <assembly> /appveyor

To run unit tests which target .NET 4.0 and later, use command:

    xunit.console.clr4 <assembly> /appveyor

<a id="mspec"></a>
### Machine.Specifications

To run MSpec tests with real-time reporting use command:

    mspec [options] <assemblies>


<a id="test-results"></a>
## Pushing real-time test results to build console

AppVeyor build console has **Tests** tab with test results updated in a real-time as tests run:

![build messages](/site/images/docs/build-tests.png)

So, how to get there from a script if you use your own test runner or a different non-supported testing framework?

### Build Worker API

You can use [Build Worker API](/docs/build-worker-api) REST methods, command-line utility and PowerShell scripts to push test results in a real-time one-by-one or in batch.

### Uploading XML test results

Testing frameworks can produce XML report with test results. Upload these XML files from your build script to **Test results endpoint** and they will be parsed and test results added to **Tests** tab of build console.

Test results endpoint URL has the following format:

    https://ci.appveyor.com/api/testresults/{resultsType}/{jobId}

where:

* `resultsType` - test framework name to parse test results; supported parsers: `mstest`, `xunit` and `nunit`.
* `jobId` - build job ID that is currently running; can be read from `APPVEYOR_JOB_ID` environment variable.

Example build script in PowerShell that runs xUnit tests and then uploads results in XML format:

<pre style="background:#f9f9f9;color:#080808"><span style="color:#5a525f;font-style:italic"># run tests</span>
xunit.console .\path\to\test-assembly.dll /xml .\xunit-results.xml

<span style="color:#5a525f;font-style:italic"># upload results to AppVeyor</span>
<span style="color:#234a97">$wc</span> = New-Object <span style="color:#0b6125">'System.Net.WebClient'</span>
<span style="color:#234a97">$wc</span>.UploadFile(<span style="color:#0b6125">"https://ci.appveyor.com/api/testresults/xunit/<span style="color:#234a97">$(</span><span style="color:#234a97">$env</span>:APPVEYOR_JOB_ID)"</span>, (Resolve-Path .\xunit-results.xml))
</pre>

> XML files must be uploaded as `multipart/form-data`.

See also:

* [Parallel testing](/docs/parallel-testing)
* [Pushing test results from scripts using Build Worker API](docs/build-worker-api#add-tests)
