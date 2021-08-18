---
layout: docs
title: Running tests
---

<!-- markdownlint-disable MD022 MD032 -->
# Running tests
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

AppVeyor has tight integration with the following testing frameworks:

* [Visual Studio Unit Testing framework (MSTest)](https://docs.microsoft.com/visualstudio/test/unit-test-your-code)
* [NUnit](http://nunit.org/)
* [xUnit.net](https://github.com/xunit/xunit)
* [Machine.Specifications (MSpec)](https://github.com/machine/machine.specifications)

This does not, however, mean you can't run your own favourite testing frameworks (like Jasmine, JSLint, Pester) within AppVeyor, but rather that AppVeyor provides **automatic discovery, execution and real-time reporting** for the frameworks listed above.

In most cases you are good to go with the default **Auto** testing mode. This mode tells AppVeyor to recursively search the build folder for test assemblies referencing known frameworks, run tests with corresponding test runners, and push results back to the build console.


## Selecting assemblies and/or categories to test

By default, **Auto** mode scans the entire build folder. For large projects this could be a time-consuming operation.

In the **Test assemblies** box you can specify one of the following (as **Only assemblies below** or **All except assemblies below**):

1. *Exact* path to an assembly relative to build root folder, for example `myproject\bin\debug\myassembly.dll`.
2. Assembly file name without a path - this case AppVeyor will perform recursive search of all assemblies with the given name.
3. Wildcard. If wildcard is used it should be full relative path. For example, to scan all folders recursively for `test-assembly.dll` specify `**\*.test-assembly.dll`.

To match an assembly in a specific folder:

    **\bin\debug\test-assembly.dll

or if build configuration is set in environment variable:

    **\bin\$(configuration)\test-assembly.dll

You can substitute any existing environment variable

To match all assemblies ending with `.tests.dll`:

    **\*.tests.dll

Configuring *only* specified test assemblies and/or categories in `appveyor.yml`(or use **Only assemblies below** and **Only categories below** in UI):

```yaml
test:
  # only assemblies to test
  assemblies:
    only:
      - test-assembly-A.dll
      - '**\*.tests.dll'

  # only categories to test
  categories:
    only:
      - A
      - B
```

To run tests from all assemblies and/or categories *except* specified ones (or use **All except assemblies below** and **All except categories below** in UI):

```yaml
test:
  # all except assemblies to test
  assemblies:
    except:
      - test-assembly-A.dll
      - '**\*.tests.dll'

  # all except categories to test
  categories:
    except:
      - A
      - B
```

This syntax will work too and will be treated as *only* (backward compatibility with configuration made before *except* functionality was introduced):

```yaml
test:
  # only assemblies to test
  assemblies:
    - test-assembly-A.dll
    - '**\*.tests.dll'

  # only categories to test
  categories:
    - A
    - B
```

Test scripts:

```yaml
# run custom scripts before tests
before_test:
  - script 1
  - script 2

# run custom scripts instead of automatic tests
# ex.1
test_script:
  - echo This is my custom test script

# ex.2 with Command Prompt
test_script:
  - custom-script-to-run-tests.cmd

#ex.3 with .NET CLI
test_script:
  - dotnet test "{the path to test assembly}.csproj" --configuration Release--no-build

#ex.4 with .NET CLI multiple and filters
test_script:
  - dotnet test "{test assembly path}.csproj" --configuration Release--no-build
  - dotnet test "{test assembly path}.csproj" --configuration Release--no-build --filter "Category!=A"

# run custom scripts after tests
after_test:
  - script 1
  - script 2
```

If assembly path in `appveyor.yml` starts with `*` surround the value with single quotes to make YAML parser happy:

```yaml
test:
  assemblies:
    - '**\*.tests.dll'
```

See [appveyor.yml reference](/docs/appveyor-yml/) for complete syntax.


## Applying category in different test frameworks

### Visual Studio unit tests (C#)

Applying category to a test method:

```csharp
[TestMethod, TestCategory("A")]
public void TestA()
{
}
```

### xUnit.net (C#)

Applying category to a test method:

```csharp
[Fact, Trait("Category", "A")]
public void MyTest()
{
}
```


### NUnit (C#)

Applying category to a test fixture:

```csharp
[TestFixture, Category("LongRunning")]
public class LongRunningTests
{
}
```

Applying category to a test method:

```csharp
[TestFixture]
public class SuccessTests
{
    [Test, Category("Long")]
    public void VeryLongTest()
    {
    }
}
```


### MSpec (C#)

Applying category to MSpec test with `Tags` attribute:

```csharp
[Subject(typeof(Account), "Funds transfer")]
[Tags("failure")]
public class when_transferring_between_two_accounts : AccountSpecs
{
}
```


## Calling test runners from your custom scripts

The AppVeyor build environment includes runners for MSTest, NUnit and xUnit.net frameworks that are integrated with the build console to push real-time results while running tests.

You can install and use your own build runners for frameworks above, communication with the build console must be done via text output from those runners.


### Visual Studio Unit Testing framework

To run unit tests for Visual Studio test framework with real-time reporting use command:

    vstest.console /logger:Appveyor <assembly> [options]

`[options]` are standard [VSTest.Console.exe command-line options](https://docs.microsoft.com/visualstudio/test/vstest-console-options).



### NUnit 2.x

To run NUnit tests with real-time reporting use command:

    nunit-console <assembly> [options]

or for x86 assemblies:

    nunit-console-x86 <assembly> [options]


### NUnit 3.x

To run NUnit tests with reporting test results to AppVeyor use command:

    nunit3-console <assembly> [options] --result=myresults.xml;format=AppVeyor

### xUnit.net

To run xUnit.net tests with real-time reporting use command:

    xunit.console <assembly> /appveyor

To run unit tests which target .NET 4.0 and later, use command:

    xunit.console.clr4 <assembly> /appveyor

### xUnit 2.x

To run xUnit 2.x tests with real-time reporting use command:

    %xunit20%\xunit.console <assembly> -appveyor [options]

or for x86 assemblies:

    %xunit20%\xunit.console.x86.exe <assembly> -appveyor [options]

### Machine.Specifications

To run MSpec tests with real-time reporting use command:

    mspec [options] <assemblies>


## Pushing real-time test results to build console

AppVeyor build console has **Tests** tab with test results updated in a real-time as tests run:

![build messages](/assets/img/docs/build-tests.png)

So, how to get there from a script if you use your own test runner or a different non-supported testing framework?

### Build Worker API

You can use [Build Worker API](/docs/build-worker-api/) REST methods, command-line utility and PowerShell scripts to push test results in a real-time one-by-one or in batch.

### Uploading XML test results

Testing frameworks can produce XML report with test results. Upload these XML files from your build script to **Test results endpoint** and they will be parsed and test results added to **Tests** tab of build console.

Test results endpoint URL has the following format:

    https://ci.appveyor.com/api/testresults/{resultsType}/{jobId}

where:

* `resultsType` - test framework name to parse test results; supported parsers:
    * `mstest`
    * `xunit`
    * `nunit`
    * `nunit3`
    * `junit`.
* `jobId` - build job ID that is currently running; can be read from `APPVEYOR_JOB_ID` environment variable.

Example build script in PowerShell that runs xUnit.net tests and then uploads results in XML format:

```powershell
# run tests
xunit.console .\path\to\test-assembly.dll /xml .\xunit-results.xml

# upload results to AppVeyor
$wc = New-Object 'System.Net.WebClient'
$wc.UploadFile("https://ci.appveyor.com/api/testresults/xunit/$($env:APPVEYOR_JOB_ID)", (Resolve-Path .\xunit-results.xml))
```

XML files must be uploaded as `multipart/form-data`.

The following example illustrates how you can collect all JUnit test results and upload them using `bash`, `find` and `curl`:

```yaml
on_finish:
  - sh: |
      find "$APPVEYOR_BUILD_FOLDER" -type f -name 'TEST*.xml' -print0 | xargs -0 -I '{}' curl -F 'file=@{}' "https://ci.appveyor.com/api/testresults/junit/$APPVEYOR_JOB_ID"
```

### Transform CTest results to JUnit

If using CTest you can still upload your XML test results. With CTest v3.21 or newer, use the [`--output-junit <file>` command line option](https://cmake.org/cmake/help/latest/manual/ctest.1.html) or the [`OUTPUT_JUNIT <file>` argument to `ctest_test()`](https://cmake.org/cmake/help/latest/command/ctest_test.html).

For older versions of CTest, transform `Test.xml` into `JUnit.xml` with the help of [this](https://github.com/rpavlik/jenkins-ctest-plugin/blob/master/ctest-to-junit.xsl) open source template. See the example script below.

```powershell
$XSLInputElement = New-Object System.Xml.Xsl.XslCompiledTransform
# you can choose to check this file into your repository instead of downloading for every build.
$XSLInputElement.Load("https://raw.githubusercontent.com/rpavlik/jenkins-ctest-plugin/master/ctest-to-junit.xsl")

#transform test results from ctest to junit format
$XSLInputElement.Transform((Resolve-Path .\<your_test_results_xml>), (Join-Path (Resolve-Path .) "ctest-to-junit-results.xml"))

#upload results
$wc = New-Object 'System.Net.WebClient'
$wc.UploadFile("https://ci.appveyor.com/api/testresults/junit/$($env:APPVEYOR_JOB_ID)", (Resolve-Path .\ctest-to-junit-results.xml))

```

See also:

* [Parallel testing](/docs/parallel-testing/)
* [Pushing test results from scripts using Build Worker API](/docs/build-worker-api#add-tests)
