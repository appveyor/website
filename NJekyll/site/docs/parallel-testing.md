---
layout: docs
title: Parallel testing
---

# Parallel testing

Parallel testing means splitting all build tests (categorizing tests) into groups and running each group as a build job on a separate VM. It does not assume running tests on several cores of a multi-core CPU.

In this section we'll describe two ways of running tests in parallel:

* Test categories
* Environment variables

## Requirements

Parallel testing works for [Premium and Enterprise plans]({{site.url}}/pricing) with 2 and 4 concurrent jobs respectively.

The number of concurrent jobs is defined **per account**, so, for example, if you run 2 builds with 4 jobs each (8 jobs in total) then you will 4 of those jobs will run at the same time. When a job from project A finishes then the first job of project B will start. This behaviour is illustrated below:

* Project A: 2 jobs finished, 2 jobs running.
* Project B: 2 jobs running, 2 jobs pending.

It's possible to assign a custom plan to your account if you need more concurrent jobs. Please [contact us for a quote](mailto:team@appveyor.com).

> You can split tests into jobs on the Free plan, but jobs will run in series as the Free plan allows 1 concurrent job per account.

## Test categories

.NET unit-testing frameworks allow for assigning categories to a test. By specifying which categories we'd like to run for each job one can drastically reduce the overall build time. The diagram below illustrates the idea:

![parallel testing diagram](/site/images/docs/parallel-testing-diagram.png)

### Categorizing tests
To assign a category to **Visual Studio** test apply the `TestCategory` attribute to a testing method:

    [TestMethod, TestCategory("A")]
    public void MyTest()
    {
    }

The **NUnit** testing framework has a `Category` attribute that can be assigned to both test fixtures and test methods. To apply category to a test fixture:

    [TestFixture, Category("LongRunning")]
    public class LongRunningTests
    {
    }

To apply category to a test method:

    [TestFixture]
    public class SuccessTests
    {
        [Test, Category("Long")]
        public void VeryLongTest()
        {
        }
    }

**xUnit** has a multi-purpose `Trait` attribute for applying various meta-data to tests. To assign category to an xUnit test:

    [Fact, Trait("Category", "A")]
    public void MyTest()
    {
    }

### Configuring project to run tests in parallel

Add **parallel testing groups** on the **Tests** tab of project settings. For example, a project with the following configuration will have builds with 2 jobs each running tests of category A and B respectively:

![test categories](/site/images/docs/test-categories.png)

To configure parallel testing groups in `appveyor.yml`:

    test:
      categories:
        - Common       # A category common for all jobs
        - [UI]         # 1st job
        - [DAL, BL]    # 2nd job

Configuration above will produce two jobs:

![test categories](/site/images/docs/parallel-testing-jobs.png)


## Environment variables

By setting groups of environment variables you can split tests using your own convention or algorithm.

For example, using the fact that the **Test assemblies** field on the **Tests** tab supports substitution of environment variables, we could split tests by assemblies.

On the **Environment** tab, add two groups of variables with `test_assembly` (you can use any name) in each:

![test categories](/site/images/docs/environment-variables-groups.png)

Then on the **Tests** tab specify which assemblies to test using the environment variable:

![test categories](/site/images/docs/test-assemblies.png)

The same configuration in `appveyor.yml`:

    environment:
      matrix:
        - test_assembly: DAL.Tests.dll
        - test_assembly: BLL.Tests.dll

    test:
      assemblies:
        - $(test_assembly)

## Imperative approach

If tests cannot be categorized by either categories or assemblies you can use environment variables to trigger internal logic of your tests. This could be anything from the names of directories with input data to a range of values to test for.

For example, to specify a range of numbers for each job we can have the following configuration:

    environment:
      matrix:
        - test_start: 0
          test_end: 49
        - test_start: 50
          test_end: 100

Then in the test code (C#):

    int testStart = Int32.Parse(Environment.GetEnvironmentVariable("test_start"));
    int testEnd = Int32.Parse(Environment.GetEnvironmentVariable("test_end"));

    for(int i = testStart; i < testEnd; i++)
    {
        // do something
    }