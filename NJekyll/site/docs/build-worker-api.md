---
title: Build Worker API
---

# Build Worker API

AppVeyor Build Worker is the service running on build VM. It provides REST API for sending real-time messages and test results to the build console, and pushing artifacts.

To access Build Worker API from your scripts, AppVeyor provides the command-line utility `appveyor.exe` and PowerShell module.

API URL is stored in `APPVEYOR_API_URL` environment variable and it is `localhost` with a random port, e.g. `http://localhost:9023/`

* [Add message](#add-message)
* [Set variable](#set-variable)
* [Add compilation message](#add-compilation-message)
* [Add tests](#add-tests)
* [Update tests](#update-tests)
* [Push artifact](#push-artifact)
* [Update build details](#update-build-details)

<a id="add-message"></a>
## Add message to build console

![build messages](/content/docs/images/build-messages.png)

### PowerShell

	Add-AppveyorMessage -Message <string>
           [-Category <category> {Information | Warning | Error}]
           [-Details <string>]

Example:

	Add-AppveyorMessage "This is a test message"

### Command line

	appveyor AddMessage <message> [options]

Options are similar to PowerShell cmdlet parameters.

### REST

	POST api/build/messages

Request body (JSON):

	{
		"message": "This is a test message",
		"category": "warning",
		"details": "Additional information for the message"
	}





<a id="add-compilation-message"></a>
## Add compilation message to build console

### PowerShell

	Add-AppveyorCompilationMessage -Message <string>
           [-Category <category> {Information | Warning | Error}] [-Details <string>]
		   [-FileName <string>] [-Line <int>] [-Column <int>] [-ProjectName <string>]
           [-ProjectFileName <string>]

Example:

	Add-AppveyorCompilationMessage "Unreachable code detected" -Category Warning -FileName "Program.cs" -Line 1 -Column 3

### Command line

	appveyor AddCompilationMessage <message> [options]

Options are similar to PowerShell cmdlet parameters.

### REST

	POST api/build/compilationmessages

Request body (JSON):

	{
		"message": "This is a test message",
		"category": "warning",
		"details": "Additional information for the message",
		"fileName": "program.cs",
		"line": "1",
		"column": "10",
		"projectName": "MyProject",
		"projectFileName": "MyProject.csproj"
	}




<a id="set-variable"></a>
## Set environment variable in build session

### PowerShell

	Set-AppveyorBuildVariable -Name <string> -Value <string>

Example:

	Set-AppveyorBuildVariable 'MyVar1' 'This is a test message'

### Command line

	appveyor SetVariable -Name <string> -Value <string>

### REST

	POST api/build/variables

Request body (JSON):

	{
		"name": "variable_name",
		"value": "hello, world!"
	}




<a id="add-tests"></a>
## Add tests to build console

![build messages](/content/docs/images/build-tests.png)

### PowerShell

	Add-AppveyorTest -Name <string> [-Framework <string>] [-FileName <string>]
		   [-Outcome <outcome> { None | Running | Passed | Failed | Ignored | Skipped
		   | Inconclusive | NotFound |  Cancelled | NotRunnable}] [-Duration <long>]
           [-ErrorMessage <string>] [-ErrorStackTrace <string>]
           [-StdOut <string>] [-StdErr <string>]

Example:

	Add-AppveyorTest "Test A" -Outcome Passed -Duration 1000 # in milliseconds

### Command line

	appveyor AddTest <name> [options]

Options are similar to PowerShell cmdlet parameters.

### REST

To add a single test:

	POST api/tests

Request body (JSON):

	{
		"testName": "Test A",
		"testFramework": "NUnit",
		"fileName": "tests.dll",
		"outcome": "Passed",
		"durationMilliseconds": "1200",
		"ErrorMessage": "",
		"ErrorStackTrace": "",
		"StdOut": "",
		"StdErr": ""
	}

To add tests in a batch:

	POST api/tests/batch

Request body (JSON):

	[
		{
			"testName": "Test A",
			"testFramework": "NUnit",
			"fileName": "tests.dll",
			"outcome": "Passed",
			"durationMilliseconds": "1200",
			"ErrorMessage": "",
			"ErrorStackTrace": "",
			"StdOut": "",
			"StdErr": ""
		},
		{
			"testName": "Test B",
			"testFramework": "xUnit",
			"fileName": "tests.dll",
			"outcome": "Passed",
			"durationMilliseconds": "10"
		},
		...
	]

<a id="update-tests"></a>
## Update test results

### PowerShell

	Update-AppveyorTest -Name <string> [-Framework <string>] [-FileName <string>]
		   -Outcome <outcome> { None | Running | Passed | Failed | Ignored | Skipped
		   | Inconclusive | NotFound |  Cancelled | NotRunnable}] [-Duration <long>
           [-ErrorMessage <string>] [-ErrorStackTrace <string>]
           [-StdOut <string>] [-StdErr <string>]

Example:

	Add-AppveyorTest "Test A" -Outcome Running
	Start-sleep -s 10
	Update-AppveyorTest "Test A" -Outcome Passed -Duration 10000

### Command line

	appveyor UpdateTest <name> [options]

Options are similar to PowerShell cmdlet parameters.

### REST

To update a single test (test is matched by name):

	PUT api/tests

Request body (JSON):

	{
		"testName": "Test A",
		"testFramework": "NUnit",
		"fileName": "tests.dll",
		"outcome": "Passed",
		"durationMilliseconds": "1200",
		"ErrorMessage": "",
		"ErrorStackTrace": "",
		"StdOut": "",
		"StdErr": ""
	}

To update tests in a batch (tests are matched by name):

	PUT api/tests/batch

Request body (JSON):

	[
		{
			"testName": "Test A",
			"outcome": "Passed",
			"durationMilliseconds": "1200"
		},
		{
			"testName": "Test B",
			"outcome": "Passed",
			"durationMilliseconds": "10"
		},
		...
	]



<a id="push-artifact"></a>
## Push artifact

![build messages](/content/docs/images/build-artifacts.png)

### PowerShell

	Push-AppveyorArtifact <path> [-FileName <string>] [-DeploymentName <string>]
		   [-Type <type> {Auto, WebDeployPackage}]

Example:

	Push-AppveyorArtifact mypackage.nupkg

### Command line

	appveyor PushArtifact <path> [options]

Options are similar to PowerShell cmdlet parameters.

### REST

	PUT api/artifacts

Request body (JSON):

	{
		"path": "c:\projects\myproject\mypackage.nupkg",
		"fileName": "mypackage.nupkg",
		"name": null,
		"type": "NuGetPackage"
	}

Response body (JSON) contains temporary URL for uploading artifact:

    "https://ci.appveyor.com/api/artifacts/abc123/mypackage.nupkg"

Then in PowerShell:

	(New-Object System.Net.WebClient).UploadFile( `
		"https://ci.appveyor.com/api/artifacts/abc123/mypackage.nupkg", `
	    "c:\projects\myproject\mypackage.nupkg")


<a id="update-build-details"></a>
## Update build details

![build details](/content/docs/images/build-details.png)

### PowerShell

	Update-AppveyorBuild [-Version <string>] [-Message <string>]
           [-CommitId <string>] [-Committed <DateTime>]
           [-AuthorName <string>] [-AuthorEmail <string>]
           [-CommitterName <string>] [-CommitterEmail <string>]

> Be careful - if you set `Version` make sure it is unique for the current project.

Example:

	$version = Get-Date -Format "mmddyyyy-HHmm"
	Update-AppveyorBuild -Version "1.0-$version"

### Command line

	appveyor UpdateBuild [options]

Options are similar to PowerShell cmdlet parameters. Example:

	appveyor UpdateBuild -Version "1.0-$version"

### REST

	PUT api/build

Request body (JSON):

	{
		"version": "1.0.2-rc1",
		"message": "This is my custom build commit message",
		"commitId": "12345abc",
		"committed": "Sat, 22 Feb 2014 00:39:25",
		"authorName": "John",
		"authorEmail": "john@smith.com",
		"committerName": "Jack",
		"committerEmail": "jack@brown.com"
	}