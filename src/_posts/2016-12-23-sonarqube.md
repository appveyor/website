---
title: SonarQube Analysis
---

This is a guest post by [Cedd Burge](https://github.com/ceddlyburge), Software Developer Lead at [RES](https://medium.com/res-software-team). Last updated and verified working December 2020.

SonarQube / SonarSource analyzes code, highlights quality issues and calculates metrics such as technical debt. More information is available on [SonarSource.com](https://www.sonarsource.com/).

This post is written from the point of view of someone (me) who is already proficient in C#, and had used SonarQube, but was new to AppVeyor and integrating SonarQube with GitHub.

It contains from scratch steps to run the SonarQube analysis on a sample project and to publish the results to [SonarCloud.io](https://SonarCloud.io). You can [look at the repo I created to test this post](https://github.com/ceddlyburge/sonarqube-nemo-on-appveyor) if you get stuck.

## Create a new GitHub repository

If you are new to GitHub, see [this getting started guide](https://guides.github.com/activities/hello-world/), otherwise simply create a new repo and git clone it somewhere convenient.

## Create a new project

In my version of Visual Studio (Community 2017), you can do this by clicking on *"File - New - Project"* on the main menu, then one of the Class Library options (.NET Framework for example) from the *"Visual C#"* section. Give it a interesting name, which I will assume to be **YourProjectName** for the rest of this post.

Add some code that has some quality issues (e.g. a variable that is declared but never used). You can use the [the full list of SonarQube C# issues](https://rules.sonarsource.com/csharp) for inspiration. Alternatively you can copy and paste [some of mine](https://github.com/ceddlyburge/sonarqube-nemo-on-appveyor/blob/master/ExampleSonarQubeIssues.cs).

Install the [Sonar Visual Studio Plugin](https://marketplace.visualstudio.com/items?itemName=SonarSource.SonarLintforVisualStudio2017). This highlights quality issues in your code as you type and gives you a chance to fix them before committing.

## Integrate GitHub with AppVeyor

Log in to [AppVeyor.com](https://ci.appveyor.com), probably using your GitHub account

* Click "Projects"
* Click "New Project"
* Choose your GitHub repository and click "Add"

## Sign up with SonarQube and generate an Authentication Token

* Create an account on [SonarCloud.io](https://sonarcloud.io/sessions/new)
* Make a note of your Organisation Key ([ceddlyburge-github for me](https://sonarcloud.io/organizations/ceddlyburge-github) (**YourSonarQubeOrganisationKey** from now on)
* Navigate to the [Tokens page  (My Account and then Security)](https://sonarcloud.io/account/security/)
* Enter a token name and click "Generate"
* Make a note of the generated token (**YourSonarQubeToken** from now on)

## Run SonarQube Analysis Locally

When working with AppVeyor, it always makes sense to test on your own computer first. The feedback is immediate and you iterate very quickly. It takes a lot longer to modify the `appveyor.yml` file, push it and wait for a build to go through. Also, if it works locally but doesn't work on AppVeyor, you know the problem is a configuration difference between your computer and the AppVeyor environment (e.g. a different version of msbuild).

Instead of committing SonarQube executables to the repo, we will download them during the build using Chocolatey.

### Install chocolatey

* [Install Chocolatey](https://chocolatey.org/install) from an administrator command prompt / powershell.
* Close the command prompt

### Install SonarQube MSBuild Runner

* Open a new administrator command prompt / powershell.
* `choco install "sonarscanner-msbuild-net46" -y`

### Analyze and upload to SonarQube

```batch
SonarScanner.MSBuild.exe begin /k:"**YourUniqueProjectName**" /d:"sonar.host.url=https://sonarqube.com" /d:"sonar.login=**YourSonarQubeToken**" /o:"**YourSonarQubeOrganisationKey**"
"**YourPathToMSBuild**\MSBuild.exe" "**YourProjectName**.sln"
SonarScanner.MSBuild.exe end /d:"sonar.login=**YourSonarQubeToken**"
```

**YourUniqueProjectName** can be anything you like, as long as it is unique.

When finished, you will be able to see the results at [sonarcloud.io/](https://sonarcloud.io). If it isn't working, make sure you are using MSBuild 14 and [Java 1.8 or later](https://stackoverflow.com/questions/40249947/msbuild-sonarqube-runner-exe-cant-access-https-sonarqube-com). The SonarQube [Getting Started](https://about.sonarqube.com/get-started/) page is excellent if you need to troubleshoot.

## Run SonarQube Analysis on AppVeyor

Now that this is working locally, we can run it on AppVeyor.

Add and commit an **appveyor.yml** file to the root of the repository as follows

```yaml
before_build:
 - nuget restore
build_script:
 - choco install "sonarscanner-msbuild-net46" -y
 - SonarScanner.MSBuild.exe begin /k:"**YourUniqueProjectName**" /d:"sonar.host.url=https://sonarcloud.io" /o:"**YourSonarQubeOrganisationKey**" /d:"sonar.login=**YourSonarQubeToken**"
 - msbuild /verbosity:quiet "**YourProjectName**.sln"
 - SonarScanner.MSBuild.exe end /d:"sonar.login=**YourSonarQubeToken**"
```

Again, you can check the results at [sonarcloud.io](https://sonarcloud.io/).

It is possible that the default version of Java on the build machine is different to the one required by SonarQube, in which case [SonarQube will show an error](https://github.com/ceddlyburge/sonarqube-nemo-on-appveyor/issues/5). In this case add the correct version of Java to the path in the `build_script`, as below.

```yaml
...
build_script:
 - set JAVA_HOME=C:\Program Files\Java\jdk11
 - set PATH=%JAVA_HOME%\bin;%PATH%
...
```

## Add a SonarQube badge to the repo

There are are variety of badges available. If you navigate to your project on SonarCloud there is a "Get Project Badges" button. It's a bit hard to find but is on the bottom right of the page at the time of writing.

To add a standard [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SonarQubeNemoOnAppveyor&metric=alert_status)](https://sonarcloud.io/dashboard?id=SonarQubeNemoOnAppveyor) badge, add the following to readme.md.

```markdown
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=**YourUniqueProjectName**&metric=alert_status)](https://sonarcloud.io/dashboard?id=**YourUniqueProjectName**)
```

## Integrate SonarQube with Pull Requests

SonarQube can analyze Pull Requests for quality issues, but sadly this feature is no longer in the free version. You can see more on the [Sonar Website](https://sonarcloud.io/documentation/analysis/pull-request/).


## Wrapping Up

SonarQube is maturing fast and is becoming industry standard, and happily it is easy to integrate Open Source projects with the publicly available SonarQube server and AppVeyor. The [Sonar Visual Studio Plugin](https://marketplace.visualstudio.com/items?itemName=SonarSource.SonarLintforVisualStudio2017) is fantastic at spotting problems before you commit them, and the (paid for) [pull request integration](https://sonarcloud.io/documentation/analysis/pull-request/) allows you to control the quality of contributions.

Best regards,<br>
Cedd Burge

Follow Cedd on Twitter: [@cuddlyburger](https://twitter.com/cuddlyburger)<br>
Follow AppVeyor on Twitter: [@appveyor](https://twitter.com/appveyor)
