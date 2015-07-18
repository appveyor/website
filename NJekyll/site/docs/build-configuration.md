---
layout: docs
title: Build configuration
---

# Build configuration

<!--TOC-->


## Build environment

Every build runs on a fresh virtual machine which is not shared with other builds and the state of which is not preserved between consequent builds. After the build is finished its virtual machine is decommissioned.

### Virtual machine configurations

<table>
  <tr>
    <th>Environment/configuration</th>
    <th>CPU</th>
    <th>Memory</th>
    <th>Disk</th>
    <th>Network</th>
  </tr>
  <tr>
    <td>Pro</td>
    <td>2 cores</td>
    <td>2.5 GB</td>
    <td>SSD 127 GB<br>(30 GB available)</td>
    <td>1 Gbps</td>
  </tr>
  <tr>
    <td>Open-Source (Free)</td>
    <td>1 core</td>
    <td>1.7 GB</td>
    <td>SSD 127 GB<br>(20 GB available)</td>
	<td>1 Gbps</td>
  </tr>
  <tr>
    <td>Azure* (Pro)</td>
    <td>2 cores<br>(Basic_A2)</td>
    <td>3.5 GB</td>
    <td>127 GB<br>(30 GB available)</td>
    <td>200 Mbps</td>
  </tr>
  <tr>
    <td>Azure* (open-source)</td>
    <td>1 core<br>(Basic_A1)</td>
    <td>1.75 GB</td>
    <td>127 GB<br>(30 GB available)</td>
    <td>100 Mbps</td>
  </tr>
</table>

> \* Builds configured to use custom image (`os` setting in `appveyor.yml` or `Operating system` on UI) run on virtual machines in Microsoft Azure data center.

### IP addresses

IP addresses assigned to build workers:

	169.54.128.0/27
	169.54.128.128/27
    75.126.44.186

IP address of AppVeyor workers (when deploying using "Environments"):

    138.91.141.243

## Build pipeline

Every build goes through the following steps:

1. Run `init` scripts
2. **Clone** repository into clone folder

    * Checkout build commit
    * `cd` to clone folder

3. Run `install` scripts
4. Patch `AssemblyInfo` files
5. Modify `hosts` files
6. Start services
7. **Build**

    * Run `before_build` scripts
    * Run msbuild
    * Run `after_build` scripts

8. **Test**

    * Run `before_test` scripts
    * Discover and run tests
    * Run `after_test` scripts

9. Call `build_success` webhooks
10. **Package** artifacts
11. **Deployment**

    * Run `before_deploy` scripts
    * Run all configured deployments
    * Run `after_deploy` scripts

12. For successful builds:

    * Call `deployment_success` webhooks
    * Run `on_success` scripts

13. For failed builds:

    * Call `build_failure` webhooks
    * Call `deployment_failure` webhooks
    * Run `on_failure` scripts

14. For both successful and failed builds:
    * Call `on_finish` scripts

### Time limitations

Free, Professional and Premium plans have a hard quota of 30 minutes on build execution time. Enterprise plans allow 50 minutes per build job.


## Configuring build

Project builds can be configured by either `appveyor.yml` or on the user interface.

Each method has pros and cons. Via the User interface one can control every aspect of the build process without ever touching the repository. On the other hand, YAML may seem more sophisticated and familiar for those coming from a Linux platform. Another thing to consider is that when you fork/clone a project with its configuration stored in `appveyor.yml`, you simply add a new project in AppVeyor referencing repo and you are good to go.

> It's worth noticing that both `appveyor.yml` and UI configuration are mutually exclusive. It's always either YAML or UI - the settings from each are not merged. If you have `appveyor.yml` in your repo it will override all settings made on the UI unless explicitly disabled by **Ignore appveyor.yml**. The only exceptions are environment variables and notification settings. Environment variables defined on UI are getting merged with those ones defined in `appveyor.yml`. Variable values with the same names are getting overridden with values from UI.


### appveyor.yml

`appveyor.yml` is a project configuration file in YAML format that should be placed in the root of your repository.
At a minimum `appveyor.yml` is just an empty file.

While working with YAML there are few important points to be aware of:

* YAML format is sensitive to indentations that must be **spaces**. Do not use tabs to indent configuration sections.
* Section names in YAML are case-sensitive, so "deploy" and "Deploy" are different things in YAML.



## Build versioning

Every time you push changes into repo or click the **New build** button AppVeyor starts a new build with an incremented build number.

You may use the **build number** for versioning purposes (assemblies version patching, naming artifacts, etc.) or just use it for reference.

AppVeyor uses the **version** value for naming builds. You can have "through" builds numbering in which major and minor parts of the version are changing and the build number is never reset or you can reset the build number for every new version. In any case, while the build number could be reset to any previously used value, the *version must be unique* across all builds.

You can specify version format in `appveyor.yml`:

    version: 1.0.{build}


## AssemblyInfo patching

AppVeyor has a built-in task for `AssemblyInfo` patching. Patching is disabled by default for newly added projects.

You can enable patching on the **General** tab of project settings or in `appveyor.yml`:

    assembly_info:
      patch: true
      file: MyVersion.cs
      assembly_version: '{version}'
      assembly_file_version: '{version}'
      assembly_informational_version: '{version}-rc1'

You can use environment variables substitution in file name and version formats, for example:

    assembly_version: $(appveyor_build_version)




## Clone directory

The format of the default directory on the build machine for cloning repository is `c:\projects\<project-slug>`.

If required by your project (say, if absolute paths are used to reference its parts) you can change the clone directory path on the **Environment** tab of project settings or in `appveyor.yml`:

    clone_folder: c:\projects\myproject



## Environment variables

Immediately after cloning the repo on the build worker AppVeyor sets [standard environment variables](/docs/environment-variables).

### Custom environment variables

Custom environment variables can be set on the **Environment** tab of project settings or in `appveyor.yml`:

    environment:
      variable1: value
      variable2: value

### Setting environment variables in build script

CMD:

    set MY_VARIABLE=value

PowerShell:

    $env:MY_VARIABLE=”value”




### Secure variables

When you work with OSS projects and you’d like to hide some sensitive data from everyone’s eyes you can use **secure variables** in `appveyor.yml`.

AppVeyor generates a unique encryption key for every account. To encrypt variable values go to **Account -> Encrypt data** tool.

To use encrypted variable in `appveyor.yml`:

    environment:
      my_variable:
        secure: <encrypt_value>

> "Secure" variables means you can safely put them into `appveyor.yml` that is visible to others. Other than that they are just regular environment variables in a build session that could be easily displayed in a build log by simple `Get-ChildItem env:`.

>However, secure variables are *not* decoded during Pull Request builds which prevents someone from submitting PR with malicious build script displaying those variables. In more controlled environment through with a trusted team and private GitHub repositories there is an option on General tab of project settings to allow secure variables for PRs.



## Script blocks in build configuration

There are a lot of places in configuration where you can inject your custom logic like "install" scripts, "before build", "after tests", "deploy" scripts, etc.

Every script could be authored either as a batch or PowerShell snippet.

When you set **Cmd** script on the UI its body will be split into lines and executed as separate commands with an exit code check after each line. For example, consider the following "install" script:

![install script cmd](/site/images/docs/install-script-cmd.png)

If exit code of the first command (`gem update --system`) is different from a 0 script execution will be terminated and entire build will return as failed.

When **PowerShell** is selected the entire body is treated as a single script, so you can use control flow logic inside it. For example:

![build script ps](/site/images/docs/build-script-ps.png)

PowerShell script is considered successful if it finishes without exception.

There are a few ways of configuring scripts in `appveyor.yml`.

For example, this is the same configuration as shown on the first screenshot:

    install:
      - gem update --system
      - gem install rake

By default, script line is treated as batch command, but you can specify script engine explicitly:

    before_build:
      - cmd: ECHO this is batch
      - ps: Write-Host "This is PowerShell"

To add multi-line PowerShell script covering an entire event:

	on_success:
	  - ps: |
	  if($true)
	  {
	    Write-Host "Success"
	  }


## Installing additional software

Every build runs on a pristine virtual machine that is not shared with any other builds. VM state is not preserved between builds.

You can install additional software on the build machine using Chocolatey, Web Platform Installer (Web PI) or PowerShell.

    install: <script>

or

    install:
      - cmd: rd temp
      - ps: Write-Host “Hello!”


### Chocolatey

    install:
      - cinst <package>


### PowerShell

    install:
      - ps: (new-object net.webclient).DownloadFile('https://mysite.com/mypackage.msi', 'mypackage.msi')
      - ps: msiexec /i mypackage.msi /quiet /qn /norestart /log install.log PROPERTY1=value1 PROPERTY2=value2


### WebPI Command Line

    install:
      - WebpiCmd /Install /Products:<Title or ID>



## Hosts entries

    hosts:
      localhost: 127.0.0.1
      db.server.com: 127.0.0.2




##Build matrix

Every AppVeyor *build* consists of one or more *jobs*. A build is considered successful if all jobs are successful. A build immediately fails when any of its jobs fail.

AppVeyor enables easy testing against multiple combinations of platforms, configurations and environments. Specify which operating systems, build configurations and platforms you would like to include into the build matrix
and AppVeyor will create a build with multiple jobs for all possible combinations.

Build matrix supports the following dimensions:

* **Operating system**
* **Environment variables**
* **Platform**, e.g. x86, x64, Any CPU
* **Configuration**, e.g. Build, Debug
* **Test categories**

For example, selecting **x86, Any CPU** for Platform and **Debug, Release** for Configuration yields the build with the following jobs:

* x86, Debug
* x86, Release
* Any CPU, Debug
* Any CPU, Release

To configure build matrix in `appveyor.yml`:

    environment:
      # these variables are common to all jobs
      common_var1: value1
      common_var2: value2

      matrix:
      # first group
      - db: mysql
        provider: mysql

      # second group
      - db: mssql
        provider: mssql
        password:
          secure: DHEU39J6X9VD376==

    platform:
      - x86
      - Any CPU

    configuration:
      - Debug
      - Release

    matrix:
      fast_finish: true

### Failing strategy

By default AppVeyor **runs all** build jobs. If at least one job has failed the entire build is marked as failed. Sometimes, you want the build fail immediately once one of the job fails. To enable **fast fail** strategy add `fast_finish` setting into `appveyor.yml`:

    matrix:
      fast_finish: true

### Allow failing jobs

You can configure AppVeyor to allow certain build matrix rows to fail and still continue with the rest of the build. *The result of these failing jobs is not counted towards build status*. This may be useful if you are experimenting with running tests on the latest version of a platform or framework, e.g. Node.js 0.11.

To allow failing jobs add `matrix.allow_failures` section into `appveyor.yml` (the feature is not available on UI):

    matrix:
      allow_failures:
        - <condition>: <value>

`<condition>` can be `os`, `configuration`, `platform`, `test_category` or the name of environment variable.

For example, to allow job failing on Node.js 0.11 (TBD - add link to Node.js instructions):

    matrix:
      allow_failures:
        - nodejs_version: 0.11

The following example allows failure for `platform=x86, configuration=Debug` and `platform=x64, configuration=Release` jobs:

    matrix:
      allow_failures:
        - platform: x86
          configuration: Debug
        - platform: x64
          configuration: Release

The matrix is already optimized for fast failing. The logic is as follows:

- If a job that does not allow failure has failed the build fails.
- If a job that does allow failure has failed and the rest of jobs allow failures the build fails.

See [complete appveyor.yml reference](/docs/appveyor-yml) for full syntax.

## Rolling builds

"Rolling builds" are great for very active OSS projects with lengthy queue. Whenever you do a new commit to the same branch *OR* pull request all current queued/running builds for that branch or PR are cancelled and the new one is queued. Other words, rolling builds make sure that only the most recent commit is built.

For example, you do commit `A` to `master` branch - it's being queued and then run. Then you do `B` commit to `master` branch while `A` is running - build of `A` will be cancelled immediately and `B` is queued. If you do another `C` commit while `B` is queued it will be cancelled and `C` queued.

## Scheduled builds

AppVeyor uses [NCrontab library](https://code.google.com/p/ncrontab/) to calculate a build schedule.

> Schedule hour values should be UTC.

External links:

* [Crontab expression syntax](https://code.google.com/p/ncrontab/wiki/CrontabExpression)
* [Crontab examples](https://code.google.com/p/ncrontab/wiki/CrontabExamples)


## Build queue

Sometimes you may wonder why your build is not being run immediately or it's "queued" state longer than usual.

This is how builds scheduling works.

First, the maximum number of builds running simultaneously is defined by account plan. For Free and Professional it's 1 concurrent job, for Premium it's 2. This is per account.

If there are no running builds under your account then there might be other reasons why the build is still in Queued state. First, VM assigned to a build is still being provisioned. We maintain some "cache" of pre-heated VMs, and if a build is assigned to pre-provisioned machine it should start almost instantly. If there are no machines in there then VM is creating from scratch. Usually, it should take no longer than 3-4 minutes.

However, sometimes (in rare occasions) due to Azure conditions provisioning could take longer, up to 10 minutes, but AppVeyor waits 5 minutes for machine being online and if it's not the build is rescheduled to another machine. Sometimes, machine is failed to report "online" state to AppVeyor and it's also getting replaced with a new one. Another reason could be Service Bus queue which is lagging some times.

We are constantly working on monitoring, understanding and mitigating those Azure-related issues to make AV architecture more robust and resilient.

However, technically there are no limitations on how many VMs can be run at the same time and we are not enforcing the maximum number of concurrent builds across all accounts.

### Builds prioritization

When a lot of commits are made by your team during the day there are multiple builds awaiting in "queued" state. It is possible to move up and down certain builds in a queue by changing their projects' priority. **Build priority** setting is located on General tab of project settings. When priority is not set builds are not prioritized. When priority is set builds in a queue are in ascending order. The higher number - the lower priority; the highest priority is 1. Builds with priority has precedence over those ones without it.

As a real-world example, suppose we have three projects: A, B and C. We want builds of project A always come first, then builds of project B and the rest in FIFO order. We set project A build priority to 1 (the highest priority) and project B priority to 2. Then we did commits in the following order (provided our account allows only one concurrent job and first build of project C is still running):

    C (no priority) - 1 - running
    C (no priority) - 2 - queued
    B (priority 2)  - 1 - queued
    A (priority 1)  - 1 - queued
    B (priority 2)  - 2 - queued

Build queue will look as below:

    C (no priority) - 1 - running
    A (priority 1)  - 1 - queued   <-- this build comes next
    B (priority 2)  - 1 - queued
    B (priority 2)  - 2 - queued
    C (no priority) - 2 - queued
