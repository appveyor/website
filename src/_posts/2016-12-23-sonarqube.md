---
title: SonarQube Analysis
---
This is a guest post by [Cedd Burge](https://github.com/ceddlyburge), Software Developer Lead at [RES](http://resgroup.github.io/).

SonarQube / SonarSource analyses code, highlights quality issues and calculates metrics such as technical debt. More information is available on [SonarSource.com](https://www.sonarsource.com/).

This post is written from the point of view of someone (me) who is already proficient in C#, and had even used SonarQube, but was new to AppVeyor and integrating SonarQube with GitHub.

It contains from scratch steps to run the SonarQube analysis on a sample project and to publish the results to the publicly available Nemo instance of SonarQube. You can [look at the repo I created to test this post](https://github.com/ceddlyburge/sonarqube-nemo-on-appveyor) if you get stuck.

## Create a new repository on GitHub

If you are new to GitHub, see [this getting started guide](https://guides.github.com/activities/hello-world/), otherwise simply create a new repo and git clone it somewhere convenient.

## Create a new project

In my version of Visual Studio (Community 2015), you can do this by clicking on *"File - New - Project"* on the main menu, then *"Class Library"* from *"Templates - Visual C#"*. Give it a interesting name, which I will assume to be **YourProjectName** for the rest of this post.

Add some code that has some quality issues (eg a variable that is declared but never used). You can use the [the full list of SonarQube C# issues](http://dist.sonarsource.com/reports/coverage/rules/csharpsquid_rules_coverage.html) for inspiration. Alternatively you can copy and paste [some of mine](https://github.com/ceddlyburge/sonarqube-nemo-on-appveyor/blob/master/ExampleSonarQubeIssues.cs).

Install the [SonarLint Visual Studio Plugin](https://marketplace.visualstudio.com/items?itemName=SonarSource.SonarLintforVisualStudio). This highlights quality issues in your code as you type and gives you a chance to fix them before comitting.

## Integrate with AppVeyor

You will need to link an AppVeyor account to your GitHub one, so let's do that:

* Navigate to your repo in GitHub
* Click "Settings" on the repo
* Click "Integrations and services"
* Click "Browse Directory"
* Click "AppVeyor"
* Click "Configure"
* Click "Grant Access"

Now Log in to [AppVeyor.com](https://ci.appveyor.com), probably using your GitHub account

* Click "Projects"
* Click "New Project"
* Choose your GitHub repository and click "Add"

## Sign up with SonarQube and generate an Authentication Token

* Create an account at [sonarqube.com/sessions/new](https://sonarqube.com/sessions/new)
* Click on [sonarqube.com/account/security/](https://sonarqube.com/account/security/)
* Enter a token name and click "Generate"
* Make a note of the generated token (**YourSonarQubeToken** from now on)

## Run SonarQube Analysis Locally

When working with appveyor, it always makes sense to test on your own computer first. The feedback is immediate and you iterate very quickly. It takes a lot longer to modify the appveyor.yml file, push it and wait for a build to go through. Also, if it works locally but doesn't work on AppVeyor, you know the problem is a configuration difference between your computer and the AppVeyor environment (eg a different version of msbuild).

Instead of committing SonarQube executables to the repo, we will download them during the build using Chocolatey.

### Install chocolatey

* [Install Chocolatey](https://chocolatey.org/install) from an administrator command prompt / powershell.
* Close the command prompt

### Install SonarQube MSBuild Runner

* Open a new administrator command prompt / powershell.
* ```choco install "msbuild-sonarqube-runner" -y```

### Analyse and upload to SonarQube

* `MSBuild.SonarQube.Runner.exe begin /k:"**YourUniqueProjectName**" /d:"sonar.host.url=https://sonarqube.com" /d:"sonar.login=**YourSonarQubeToken**"`
* `"**YourPathToMSBuild**\MSBuild.exe" "**YourProjectName**.sln"`
* `MSBuild.SonarQube.Runner.exe end /d:"sonar.login=**YourSonarQubeToken**"`

When finished, you will be able to see the results at [sonarqube.com/](https://sonarqube.com/). If it isn't working, make sure you are using MSBuild 14 and [Java 1.8 or later](http://stackoverflow.com/questions/40249947/msbuild-sonarqube-runner-exe-cant-access-https-sonarqube-com). The SonarQube [Getting Started](https://about.sonarqube.com/get-started/) page is excellent if these instructions become out of date.

## Run SonarQube Analysis on AppVeyor

Now that this is working locally, we can run it on AppVeyor.

Add and commit an **appveyor.yml** file to the root of the repository as follows

```yaml
before_build:
  - nuget restore
build_script:
  - choco install "msbuild-sonarqube-runner" -y
  - MSBuild.SonarQube.Runner.exe begin /k:"YourUniqueProjectName" /d:"sonar.host.url=https://sonarqube.com" /d:"sonar.login=YourSonarQubeToken"
  - msbuild "YourProjectName.sln"
  - MSBuild.SonarQube.Runner.exe end /d:"sonar.login=YourSonarQubeToken"
```

Again, you can check the results at [sonarqube.com](https://sonarqube.com/).

## Add a SonarQube badge to the repo

There are are variety of [Quality Gate](https://github.com/QualInsight/qualinsight-plugins-sonarqube-badges/wiki/Quality-Gate-status-badges) and [Metrics](https://github.com/QualInsight/qualinsight-plugins-sonarqube-badges/wiki/Measure-badges) badges available.

To add a standard [![Quality Gate](https://sonarqube.com/api/badges/gate?key=SonarQubeNemoOnAppveyor)](https://sonarqube.com/dashboard/index/SonarQubeNemoOnAppveyor) badge, add the following to readme.md.

`[![Quality Gate](https://sonarqube.com/api/badges/gate?key=YourUniqueProjectName)](https://sonarqube.com/dashboard/index/YourUniqueProjectName)`

## Integrate SonarQube with Pull Requests

SonarQube can analyse Pull Requests for quality issues, which you can see on [this pull request](https://github.com/ceddlyburge/sonarqube-nemo-on-appveyor/pull/3).

This requires a GitHub authentication token, which must be secured, secure variables to be enabled in pull requests and a differential build for Pull Requests.

### Get a GitHub Authentication token

Go to your profile and click *"Edit Profile"*. Click on *"Personal access tokens"* in the *"Developer settings"* section. Give the token any name and tick on the *"public_repo"* scope. Make a note of the created token (**GitHubAuthToken** from now on)

### Secure the GitHub Authentication token

Anyone with access to this token can alter your data, contact information and billing data, so we don't want that.

On [AppVeyor](https://ci.appveyor.com), click your user name in the top right hand corner and then click *"Encrypt data"* from the drop down menu. Enter **GitHubAuthToken** in to *"Value to encrypt"* and click *"Encrypt"*. AppVeyor will then display a token which you can use in place of the real value (**EncryptedGitHubAuthToken** from now on).

### Allowing Secure Variables in Pull Requests

Normally AppVeyor will not decrypt secure variables in Pull Requests, as in this case a Hacker could send you a PR and then read all of your secure data. However, for SonarQube to analyse Pull Requests, it is necessary. You need to decide whether you can live with this.

If you can, go to [AppVeyor](https://ci.appveyor.com), click on your project, click *"Settings"*, tick *"Enable secure variables in Pull Requests from the same repository only"* and click *"Save"*.

### Create a Pull Request Build

Modify **AppVeyor.yml** to ask SonarQube to publish results on standard builds, and to integrate with pull request builds. To achieve this, extra parameters are given to the SonarQube runner when `if ($env:APPVEYOR_PULL_REQUEST_NUMBER)` detects a Pull Request build.

```yaml
environment:
  github_auth_token:
    secure: EncryptedGitHubAuthToken
before_build:
  - nuget restore
build_script:
  - choco install "msbuild-sonarqube-runner" -y
  - ps: if ($env:APPVEYOR_PULL_REQUEST_NUMBER) { MSBuild.SonarQube.Runner.exe begin /k:"YourUniqueProjectName" /d:"sonar.host.url=https://sonarqube.com" /d:"sonar.login=YourSonarQubeToken" /d:"sonar.analysis.mode=preview" /d:"sonar.github.pullRequest=$env:APPVEYOR_PULL_REQUEST_NUMBER" /d:"sonar.github.repository=YourRepositoryUrl" /d:"sonar.github.oauth=$env:EncryptedGitHubAuthToken" }
  - ps: if (-Not $env:APPVEYOR_PULL_REQUEST_NUMBER) { MSBuild.SonarQube.Runner.exe begin /k:"YourUniqueProjectName" /d:"sonar.host.url=https://sonarqube.com" /d:"sonar.login=YourSonarQubeToken" }
  - msbuild "YourProjectName.sln"
  - MSBuild.SonarQube.Runner.exe end /d:"sonar.login=YourSonarQubeToken"
```

## Conclusion

SonarQube is maturing fast and is becoming industry standard, and happily it is easy to integrate Open Source projects with the publicly available SonarQube server and AppVeyor. The [SonarLint Visual Studio Plugin](https://marketplace.visualstudio.com/items?itemName=SonarSource.SonarLintforVisualStudio) is fantastic at spotting problems before you commit them, and the [GitHub integration](http://docs.sonarqube.org/display/PLUG/GitHub+Plugin) allows you to control the quality of contributions.

Best regards,<br>
Cedd Burge

Follow Cedd on Twitter: [@cuddlyburger](https://twitter.com/cuddlyburger)<br>
Follow AppVeyor on Twitter: [@appveyor](https://twitter.com/appveyor)
