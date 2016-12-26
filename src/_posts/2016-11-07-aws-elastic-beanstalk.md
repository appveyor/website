---
title: AWS Elastic Beanstalk
---

Appveyor does not support AWS Elastic Beanstalk deployment out of the box right now.
However it can be automated in Appveyor with help of some scripting.
Here is small guide based on [this support forum discussion](https://github.com/appveyor/ci/issues/45#issuecomment-165571187).

* In the root folder of your web application create text file named `awsdeploy.txt`.
* Add the following to `awsdeploy.txt`:

  ```text
  Template = ElasticBeanstalk
  Container.ApplicationHealthcheckPath = /healthcheck
  ```

* Add `AWSAccessKeyId` as environment variable and `AWSSecretKey` as secure environment variable.
* Set **Package Web Applications for XCopy deployment** in build stage.
* Set the following as a deployment script:

```text
$packageweb = $artifacts.values | Where-Object { $_.path -like '*WebApplication1.zip' }
$exe = "C:\Program Files (x86)\AWS Tools\Deployment Tool\awsdeploy.exe"
&$exe -r "-DDeploymentPackage=$($packageweb.path)" "-DEnvironment.Name=MyAppWeb-test123" "-DApplication.Name=MyAppWeb123" "-DRegion=eu-west-1" "-DAWSAccessKey=$env:AWSAccessKeyId" "-DAWSSecretKey=$env:AWSSecretKey" "C:\projects\WebApplication1\awsdeploy.txt"
```

Note that this script assumes that application was already deployed at least once to Beanstalk, otherwise you need to replace -r switch with -w for single first deployment.

Here is an example YAML (only relevant parts):

```yaml
environment:
  AWSAccessKeyId: AKIAIODIUCY3ETD6TEST
  AWSSecretKey:
    secure: LEvzbXpiLkWVvswonFHnAYV9ZS6fEFL3wswjTcIQ6ZXC5j1nynd6N0Bs/VFtest
build:
  publish_wap_xcopy: true
deploy_script:
- ps: >-
    $packageweb = $artifacts.values | Where-Object { $_.path -like '*WebApplication1.zip' }

    $exe = "C:\Program Files (x86)\AWS Tools\Deployment Tool\awsdeploy.exe"

    &$exe -r "-DDeploymentPackage=$($packageweb.path)" "-DEnvironment.Name=MyAppWeb-test123" "-DApplication.Name=MyAppWeb123" "-DRegion=eu-west-1" "-DAWSAccessKey=$env:AWSAccessKeyId" "-DAWSSecretKey=$env:AWSSecretKey" "C:\projects\WebApplication1\awsdeploy.txt"
```

Here is example web application folder structure:

```text
Directory of C:\Projects\WebApplication1

09/15/2016  02:48 AM    <DIR>          .
09/15/2016  02:48 AM    <DIR>          ..
07/17/2016  12:34 PM               505 .gitattributes
07/17/2016  12:34 PM             2,858 .gitignore
09/15/2016  02:40 AM                80 awsdeploy.txt
08/30/2016  03:22 PM    <DIR>          WebApplication1
07/21/2016  06:51 PM             1,012 WebApplication1.sln
```

Enjoy!
