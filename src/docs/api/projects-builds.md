---
layout: docs
title: Projects and builds API
---

# Projects

* [Get projects](#get-projects)
* [Get project last build](#get-project-last-build)
* [Get project last branch build](#get-project-last-branch-build)
* [Get project build by version](#get-project-build-by-version)
* [Get project history](#get-project-history)
* [Get project deployments](#get-project-deployments)
* [Get project settings](#get-project-settings)
* [Get project settings in YAML](#get-project-settings-in-yaml)
* [Get project environment variables](#get-project-environment-variables)
* [Add project](#add-project)
* [Update project](#update-project)
* [Update project settings in YAML](#update-project-settings-in-yaml)
* [Update project environment variables](#update-project-environment-variables)
* [Update project build number](#update-project-build-number)
* [Delete project build cache](#delete-project-build-cache)
* [Delete project](#delete-project)

# Builds

* [Start build of branch most recent commit](#start-build-of-branch-most-recent-commit)
* [Start build of specific branch commit](#start-build-of-specific-branch-commit)
* [Re-run build](#re-run-build)
* [Start build of Pull Request (GitHub only)](#start-build-of-pull-request-github-only)
* [Cancel build](#cancel-build)
* [Delete build](#delete-build)

# Build Jobs

* [Download build log](#download-build-log)

## Get projects

Request:

    GET /api/projects

Response:

```text
[
   {
      "projectId":19096,
      "accountId":2,
      "accountName":"FeodorFitsner",
      "builds":[
         {
            "buildId":23864,
            "jobs":[

            ],
            "buildNumber":3,
            "version":"1.0.3",
            "message":"replaced with command [skip ci]",
            "branch":"master",
            "commitId":"c2892a70d60c96c1b65a7c665ab806b7731fea8a",
            "authorName":"Feodor Fitsner",
            "authorUsername":"FeodorFitsner",
            "committerName":"Feodor Fitsner",
            "committerUsername":"FeodorFitsner",
            "committed":"2014-08-15T22:05:54+00:00",
            "messages":[

            ],
            "status":"success",
            "started":"2014-08-15T22:36:38.1757886+00:00",
            "finished":"2014-08-15T22:37:00.6171479+00:00",
            "created":"2014-08-15T22:33:15.9833328+00:00",
            "updated":"2014-08-15T22:37:00.6171479+00:00"
         }
      ],
      "name":"appveyor-artifact-test",
      "slug":"appveyor-artifact-test",
      "repositoryType":"gitHub",
      "repositoryScm":"git",
      "repositoryName":"FeodorFitsner/appveyor-artifact-test",
      "repositoryBranch":"master",
      "isPrivate":false,
      "skipBranchesWithoutAppveyorYml":false,
      "nuGetFeed":{
         "id":"appveyor-artifact-test-j8kk0o",
         "name":"Project appveyor-artifact-test",
         "publishingEnabled":false,
         "created":"2014-08-15T22:04:21.3111546+00:00"
      },
      "created":"2014-08-15T22:04:19.2868375+00:00"
   }
]
```


## Get project last build

Request:

    GET /api/projects/{accountName}/{projectSlug}

Response:

```text
{
   "project":{
      "projectId":38907,
      "accountId":2,
      "accountName":"appvyr",
      "builds":[

      ],
      "name":"nuget-test",
      "slug":"nuget-test",
      "repositoryType":"gitHub",
      "repositoryScm":"git",
      "repositoryName":"FeodorFitsner/nuget-test",
      "repositoryBranch":"master",
      "isPrivate":false,
      "skipBranchesWithoutAppveyorYml":false,
      "nuGetFeed":{
         "id":"nuget-test-23spw2w",
         "name":"Project nuget-test",
         "publishingEnabled":false,
         "created":"2014-07-19T10:23:05.5160273+00:00"
      },
      "securityDescriptor":{
      },
      "created":"2014-07-19T10:23:03.8005134+00:00",
      "updated":"2014-08-01T05:25:15.4119745+00:00"
   },
   "build":{
      "buildId":134173,
      "jobs":[
         {
            "jobId":"9r2qufuu8",
            "name":"",
            "allowFailure":false,
            "messagesCount":0,
            "compilationMessagesCount":0,
            "compilationErrorsCount":0,
            "compilationWarningsCount":0,
            "testsCount":0,
            "passedTestsCount":0,
            "failedTestsCount":0,
            "artifactsCount":0,
            "status":"success",
            "started":"2014-08-14T05:41:49.1061831+00:00",
            "finished":"2014-08-14T05:42:45.1102797+00:00",
            "created":"2014-08-14T05:39:27.3902557+00:00",
            "updated":"2014-08-14T05:42:45.477213+00:00"
         }
      ],
      "buildNumber":45,
      "version":"1.0.45",
      "message":"AssemblyInfo patching",
      "branch":"master",
      "commitId":"85da1fb810ae89744abad83e75c13483dd740258",
      "authorName":"Feodor Fitsner",
      "authorUsername":"FeodorFitsner",
      "committerName":"Feodor Fitsner",
      "committerUsername":"FeodorFitsner",
      "committed":"2014-07-03T07:56:15+00:00",
      "messages":[

      ],
      "status":"success",
      "started":"2014-08-14T05:41:49.1686804+00:00",
      "finished":"2014-08-14T05:42:45.5709599+00:00",
      "created":"2014-08-14T05:39:26.2946368+00:00",
      "updated":"2014-08-14T05:42:45.5709599+00:00"
   }
}
```

## Get project last branch build

Request:

    GET /api/projects/{accountName}/{projectSlug}/branch/{buildBranch}

Response:

Response is the same as [Get project last build](#get-project-last-build)


## Get project build by version

Request:

    GET /api/projects/{accountName}/{projectSlug}/build/{buildVersion}

Response:

Response is the same as [Get project last build](#get-project-last-build)


## Get project history

Request:

    GET /api/projects/{accountName}/{projectSlug}/history?recordsNumber={records-per-page}[&startBuildId={buildId}&branch={branch}]

Response:

```text
{
   "project":{
      "projectId":42438,
      "accountId":2,
      "accountName":"appvyr",
      "builds":[

      ],
      "name":"wix-test",
      "slug":"wix-test",
      "repositoryType":"gitHub",
      "repositoryScm":"git",
      "repositoryName":"FeodorFitsner/wix-test",
      "isPrivate":false,
      "skipBranchesWithoutAppveyorYml":false,
      "created":"2014-08-09T00:30:43.3327131+00:00"
   },
   "builds":[
      {
         "buildId":134174,
         "jobs":[

         ],
         "buildNumber":5,
         "version":"1.0.5",
         "message":"Enabled diag mode",
         "branch":"master",
         "commitId":"d19740243e3ec5497345de0f7d828e66a7cd1a6b",
         "authorName":"Feodor Fitsner",
         "authorUsername":"FeodorFitsner",
         "committerName":"Feodor Fitsner",
         "committerUsername":"FeodorFitsner",
         "committed":"2014-08-10T14:08:16+00:00",
         "messages":[

         ],
         "status":"success",
         "started":"2014-08-14T05:42:17.2696755+00:00",
         "finished":"2014-08-14T05:43:47.4732355+00:00",
         "created":"2014-08-14T05:39:30.8845902+00:00",
         "updated":"2014-08-14T05:43:47.4732355+00:00"
      },
      {
         "buildId":129289,
         "jobs":[

         ],
         "buildNumber":3,
         "version":"1.0.3",
         "message":"Added appveyor.yml",
         "branch":"master",
         "commitId":"28c6eec932c0e21eca5bb5571a722f850aa8bf6f",
         "authorName":"Feodor Fitsner",
         "authorUsername":"FeodorFitsner",
         "committerName":"Feodor Fitsner",
         "committerUsername":"FeodorFitsner",
         "committed":"2014-08-09T00:33:34+00:00",
         "messages":[

         ],
         "status":"success",
         "started":"2014-08-09T15:42:45.7878479+00:00",
         "finished":"2014-08-09T15:44:15.5828009+00:00",
         "created":"2014-08-09T15:42:38.8315273+00:00",
         "updated":"2014-08-09T15:44:15.5828009+00:00"
      }
   ]
}
```


## Get project deployments

Request:

    GET /api/projects/{accountName}/{projectSlug}/deployments[?recordsNumber={records-per-page}&startDeploymentId={deploymentId}]

Note: maximum and default `recordsNumber` is 20.

Response:

```text
{
   "project":{
      "projectId":22321,
      "accountId":2,
      "accountName":"appvyr",
      "builds":[

      ],
      "name":"simple-web",
      "slug":"simple-web",
      "repositoryType":"gitHub",
      "repositoryScm":"git",
      "repositoryName":"AppVeyor/simple-web",
      "isPrivate":false,
      "skipBranchesWithoutAppveyorYml":false,
      "securityDescriptor":{
      },
      "created":"2014-05-08T18:38:57.9163293+00:00",
      "updated":"2014-07-14T10:16:26.9351867+00:00"
   },
   "deployments":[
      {
         "environment":{
            "deploymentEnvironmentId":27,
            "name":"agent test",
            "provider":"Agent",
            "created":"2014-04-01T17:56:41.30982+00:00",
            "updated":"2014-08-12T22:35:51.9723883+00:00"
         },
         "deployment":{
            "deploymentId":19475,
            "build":{
               "buildId":132746,
               "jobs":[

               ],
               "buildNumber":38,
               "version":"1.0.38",
               "message":"Removed Start-Website",
               "branch":"master",
               "commitId":"c397ba5d17dd17b994375405f560e4922207da1e",
               "authorName":"Feodor Fitsner",
               "authorUsername":"FeodorFitsner",
               "committerName":"Feodor Fitsner",
               "committerUsername":"FeodorFitsner",
               "committed":"2014-08-12T22:56:00+00:00",
               "messages":[

               ],
               "status":"success",
               "started":"2014-08-12T22:56:25.8575967+00:00",
               "finished":"2014-08-12T22:58:05.7595508+00:00",
               "created":"2014-08-12T22:56:09.9208493+00:00",
               "updated":"2014-08-12T22:58:05.7595508+00:00"
            },
            "environment":{
               "deploymentEnvironmentId":27,
               "name":"agent test",
               "provider":"Agent",
               "created":"2014-04-01T17:56:41.30982+00:00",
               "updated":"2014-08-12T22:35:51.9723883+00:00"
            },
            "jobs":[
               {
                  "jobId":"jnpbcc77s4w278e4",
                  "name":"Deployment",
                  "messagesCount":0,
                  "status":"success",
                  "started":"2014-08-12T23:06:10.8776088+00:00",
                  "finished":"2014-08-12T23:06:24.3361102+00:00",
                  "created":"2014-08-12T23:06:07.9009315+00:00",
                  "updated":"2014-08-12T23:06:24.9390847+00:00"
               },
               {
                  "jobId":"nbgyf7pn65d4agyr",
                  "name":"TEST-AGENT-DEPL",
                  "messagesCount":0,
                  "status":"success",
                  "started":"2014-08-12T23:06:14.8148958+00:00",
                  "finished":"2014-08-12T23:06:21.9647266+00:00",
                  "created":"2014-08-12T23:06:14.8148958+00:00",
                  "updated":"2014-08-12T23:06:22.3768791+00:00"
               }
            ],
            "status":"success",
            "started":"2014-08-12T23:06:10.8776088+00:00",
            "finished":"2014-08-12T23:06:25.0502019+00:00",
            "created":"2014-08-12T23:06:07.9009315+00:00",
            "updated":"2014-08-12T23:06:25.0502019+00:00"
         }
      }
   ]
}
```


## Get project settings

Request:

    GET /api/projects/{accountName}/{projectSlug}/settings

Response:

```text
{
   "project":{
      "projectId":22321,
      "accountId":2,
      "accountName":"appvyr",
      "builds":[

      ],
      "name":"simple-web",
      "slug":"simple-web",
      "repositoryType":"gitHub",
      "repositoryScm":"git",
      "repositoryName":"AppVeyor/simple-web",
      "isPrivate":false,
      "skipBranchesWithoutAppveyorYml":false,
      "securityDescriptor":{
         "accessRightDefinitions":[
            {
               "name":"View",
               "description":"View"
            },
            {
               "name":"RunBuild",
               "description":"Run build"
            },
            {
               "name":"Update",
               "description":"Update settings"
            },
            {
               "name":"Delete",
               "description":"Delete project"
            }
         ],
         "roleAces":[
            {
               "roleId":4,
               "name":"Administrator",
               "isAdmin":true,
               "accessRights":[
                  {
                     "name":"View",
                     "allowed":true
                  },
                  {
                     "name":"RunBuild",
                     "allowed":true
                  },
                  {
                     "name":"Update",
                     "allowed":true
                  },
                  {
                     "name":"Delete",
                     "allowed":true
                  }
               ]
            },
            {
               "roleId":5,
               "name":"User",
               "isAdmin":false,
               "accessRights":[
                  {
                     "name":"View"
                  },
                  {
                     "name":"RunBuild"
                  },
                  {
                     "name":"Update"
                  },
                  {
                     "name":"Delete"
                  }
               ]
            }
         ]
      },
      "created":"2014-05-08T18:38:57.9163293+00:00",
      "updated":"2014-07-14T10:16:26.9351867+00:00"
   },
   "settings":{
      "projectId":22321,
      "accountId":2,
      "accountName":"appvyr",
      "builds":[

      ],
      "name":"simple-web",
      "slug":"simple-web",
      "versionFormat":"1.0.{build}",
      "nextBuildNumber":41,
      "repositoryType":"gitHub",
      "repositoryScm":"git",
      "repositoryName":"AppVeyor/simple-web",
      "repositoryBranch":"master",
      "webhookId":"k783di7br",
      "webhookUrl":"https://ci.appveyor.com/api/github/webhook?id=k783di7br",
      "isPrivate":false,
      "ignoreAppveyorYml":false,
      "skipBranchesWithoutAppveyorYml":false,
      "configuration":{
          "initScripts":[],
          "includeBranches":[],
          "excludeBranches":[],
          "onBuildSuccessScripts":[],
          "onBuildErrorScripts":[],
          "patchAssemblyInfo":false,
          "assemblyInfoFile":"**\\AssemblyInfo.*",
          "assemblyVersionFormat":"{version}",
          "assemblyFileVersionFormat":"{version}",
          "assemblyInformationalVersionFormat":"{version}",
          "operatingSystem":[],
          "services":[],
          "shallowClone":false,
          "environmentVariables":[],
          "environmentVariablesMatrix":[],
          "installScripts":[],
          "hostsEntries":[],
          "buildMode":"msbuild",
          "platform":[],
          "configuration":[],
          "packageWebApplicationProjects":false,
          "packageWebApplicationProjectsXCopy":false,
          "packageAzureCloudServiceProjects":false,
          "packageNuGetProjects":false,
          "msBuildVerbosity":"minimal",
          "buildScripts":[],
          "beforeBuildScripts":[],
          "afterBuildScripts":[],
          "testMode":"auto",
          "testAssemblies":[],
          "testCategories":[],
          "testCategoriesMatrix":[],
          "testScripts":[],
          "beforeTestScripts":[],
          "afterTestScripts":[],
          "deployMode":"providers",
          "deployments":[],
          "deployScripts":[],
          "beforeDeployScripts":[],
          "afterDeployScripts":[],
          "matrixFastFinish":false,
          "matrixAllowFailures":[],
          "artifacts":[],
          "notifications":[]
      },
      "nuGetFeed":{
         "id":"simple-web-0r50wgb1st6q",
         "name":"Project simple-web",
         "publishingEnabled":false,
         "created":"2014-05-08T18:38:59.2455842+00:00"
      },
      "securityDescriptor":{
         "accessRightDefinitions":[
            {
               "name":"View",
               "description":"View"
            },
            {
               "name":"RunBuild",
               "description":"Run build"
            },
            {
               "name":"Update",
               "description":"Update settings"
            },
            {
               "name":"Delete",
               "description":"Delete project"
            }
         ],
         "roleAces":[
            {
               "roleId":4,
               "name":"Administrator",
               "isAdmin":true,
               "accessRights":[
                  {
                     "name":"View",
                     "allowed":true
                  },
                  {
                     "name":"RunBuild",
                     "allowed":true
                  },
                  {
                     "name":"Update",
                     "allowed":true
                  },
                  {
                     "name":"Delete",
                     "allowed":true
                  }
               ]
            },
            {
               "roleId":5,
               "name":"User",
               "isAdmin":false,
               "accessRights":[
                  {
                     "name":"View"
                  },
                  {
                     "name":"RunBuild"
                  },
                  {
                     "name":"Update"
                  },
                  {
                     "name":"Delete"
                  }
               ]
            }
         ]
      },
      "created":"2014-05-08T18:38:57.9163293+00:00",
      "updated":"2014-07-14T10:16:26.9351867+00:00"
   },
   "images":[
      {
         "name":"test-win2012-r2"
      },
      {
         "name":"Windows Server 2012"
      },
      {
         "name":"Windows Server 2012 R2"
      }
   ]
}
```


## Get project settings in YAML

Request:

    GET /api/projects/{accountName}/{projectSlug}/settings/yaml

Response (`plain/text`):

```text
version: 1.0.{build}
build:
  project: MySolution.sln
  verbosity: minimal
  publish_wap: true
  ...
```


## Get project environment variables

Request:

    GET /api/projects/{accountName}/{projectSlug}/settings/environment-variables

Response:

```text
[
   {
      "name":"api_key",
      "value":{
         "isEncrypted":true,
         "value":"very-secret-key-in-clear-text"
      }
   },
   {
      "name":"var1",
      "value":{
         "isEncrypted":false,
         "value":"current-value"
      }
   }
]
```


## Add project

Request:

    POST /api/projects

Request body:

```text
{
   "repositoryProvider":"gitHub",
   "repositoryName":"FeodorFitsner/demo-app"
}
```

Where `repositoryProvider` is one of:

* `gitHub`
* `bitBucket`
* `vso` (Visual Studio Online)
* `gitLab`
* `kiln`
* `stash`
* `git`
* `mercurial`
* `subversion`

Response:

```text
{
   "projectId":43682,
   "accountId":2,
   "accountName":"appvyr",
   "builds":[

   ],
   "name":"demo-app",
   "slug":"demo-app-335",
   "repositoryType":"gitHub",
   "repositoryScm":"git",
   "repositoryName":"FeodorFitsner/demo-app",
   "isPrivate":false,
   "skipBranchesWithoutAppveyorYml":false,
   "created":"2014-08-16T00:52:15.6604826+00:00"
}
```

## Update project

Request:

    PUT /api/projects

Request body:

```text
{
   "projectId":43682,
   "accountId":2,
   "accountName":"appvyr",
   "builds":[],
   "name":"demo-app",
   "slug":"demo-app-335",
   "versionFormat":"1.0.{build}",
   "nextBuildNumber":1,
   "repositoryType":"gitHub",
   "repositoryScm":"git",
   "repositoryName":"FeodorFitsner/demo-app",
   "repositoryBranch":"master",
   "webhookId":"rca5vb5qqu",
   "webhookUrl":"https://ci.appveyor.com/api/github/webhook?id=rca5vb5qqu",
   "isPrivate":false,
   "ignoreAppveyorYml":false,
   "skipBranchesWithoutAppveyorYml":false,
   "configuration":{
      "initScripts":[],
      "includeBranches":[],
      "excludeBranches":[],
      "onBuildSuccessScripts":[],
      "onBuildErrorScripts":[],
      "patchAssemblyInfo":false,
      "assemblyInfoFile":"**\\AssemblyInfo.*",
      "assemblyVersionFormat":"{version}",
      "assemblyFileVersionFormat":"{version}",
      "assemblyInformationalVersionFormat":"{version}",
      "operatingSystem":[],
      "services":[],
      "shallowClone":false,
      "environmentVariables":[],
      "environmentVariablesMatrix":[],
      "installScripts":[],
      "hostsEntries":[],
      "buildMode":"msbuild",
      "platform":[],
      "configuration":[],
      "packageWebApplicationProjects":false,
      "packageWebApplicationProjectsXCopy":false,
      "packageAzureCloudServiceProjects":false,
      "packageNuGetProjects":false,
      "msBuildVerbosity":"minimal",
      "buildScripts":[],
      "beforeBuildScripts":[],
      "afterBuildScripts":[],
      "testMode":"auto",
      "testAssemblies":[],
      "testCategories":[],
      "testCategoriesMatrix":[],
      "testScripts":[],
      "beforeTestScripts":[],
      "afterTestScripts":[],
      "deployMode":"providers",
      "deployments":[],
      "deployScripts":[],
      "beforeDeployScripts":[],
      "afterDeployScripts":[],
      "matrixFastFinish":false,
      "matrixAllowFailures":[],
      "artifacts":[],
      "notifications":[]
   },
   "nuGetFeed":{
      "id":"demo-app-tw5iw2wk3bl1",
      "name":"Project demo-app",
      "publishingEnabled":false,
      "created":"2014-08-16T00:52:16.9886427+00:00"
   },
   "securityDescriptor":{
      "accessRightDefinitions":[
         {
            "name":"View",
            "description":"View"
         },
         {
            "name":"RunBuild",
            "description":"Run build"
         },
         {
            "name":"Update",
            "description":"Update settings"
         },
         {
            "name":"Delete",
            "description":"Delete project"
         }
      ],
      "roleAces":[
         {
            "roleId":4,
            "name":"Administrator",
            "isAdmin":true,
            "accessRights":[
               {
                  "name":"View",
                  "allowed":true
               },
               {
                  "name":"RunBuild",
                  "allowed":true
               },
               {
                  "name":"Update",
                  "allowed":true
               },
               {
                  "name":"Delete",
                  "allowed":true
               }
            ]
         },
         {
            "roleId":5,
            "name":"User",
            "isAdmin":false,
            "accessRights":[
               {
                  "name":"View"
               },
               {
                  "name":"RunBuild"
               },
               {
                  "name":"Update"
               },
               {
                  "name":"Delete"
               }
            ]
         }
      ]
   },
   "created":"2014-08-16T00:52:15.6604826+00:00"
}
```

Response: 204


## Update project settings in YAML

Request:

    PUT /api/projects/{accountName}/{projectSlug}/settings/yaml

Request body (`plain/text`):

```text
version: 1.0.{build}
build:
  project: MySolution.sln
  verbosity: minimal
  publish_wap: true
  ...
```

Response: 204


## Update project environment variables

Request:

    PUT /api/projects/{accountName}/{projectSlug}/settings/environment-variables

Request body (`application/json`):

```text
[
   {
      "name":"api_key",
      "value":{
         "isEncrypted":true,
         "value":"very-secret-key-encrypted"
      }
   },
   {
      "name":"var1",
      "value":{
         "isEncrypted":false,
         "value":"new-value"
      }
   }
]
```

Response: 204


## Update project build number

Request:

    PUT /api/projects/{accountName}/{projectSlug}/settings/build-number

Request body (`application/json`):

```text
{
   "nextBuildNumber": 35
}
```

Response: 204


## Delete project build cache

Request:

    DELETE /api/projects/{accountName}/{projectSlug}/buildcache

Response: 204


## Delete project

Request:

    DELETE /api/projects/{accountName}/{projectSlug}

Response: 204



### Start build of branch most recent commit

Request:

    POST /api/builds

Request body:

```text
{
    "accountName": "your-account-name",
    "projectSlug": "project-slug-from-url",
    "branch": "master",
    "environmentVariables": {
       "my_var": "value",
       "another_var": "another value"
    }
}
```

Response:

```text
{
   "buildId":136709,
   "jobs":[

   ],
   "buildNumber":7,
   "version":"1.0.7",
   "message":"replaced with command [skip ci]",
   "branch":"master",
   "commitId":"c2892a70d60c96c1b65a7c665ab806b7731fea8a",
   "authorName":"Feodor Fitsner",
   "authorUsername":"FeodorFitsner",
   "committerName":"Feodor Fitsner",
   "committerUsername":"FeodorFitsner",
   "committed":"2014-08-15T22:05:54+00:00",
   "messages":[

   ],
   "status":"queued",
   "created":"2014-08-16T00:40:38.1703914+00:00"
}
```

### Start build of specific branch commit

Request:

    POST /api/builds

Request body:

```text
{
    "accountName": "your-account-name",
    "projectSlug": "project-slug-from-url",
    "branch": "develop",
    "commitId": "3e9d9468"
}
```

### Re-run build

Request:

    PUT /api/builds

Request body:

```text
{
    buildId: {buildId},
    reRunIncomplete: {True/False}
}
```

Set `reRunIncomplete` set to `False` (default value) for full buildre-run. Set it set to `True` to rerun only failed or cancelled jobs in multijob build.

### Start build of Pull Request (GitHub only)

Request:

    POST /api/builds

Request body:

```text
{
    accountName: 'your-account-name',
    projectSlug: 'project-slug-from-url',
    pullRequestId: 3
}
```

Response body:

```text
{
    "buildId":642688,
    "jobs":[],
    "buildNumber":308,
    "version":"308",
    "message":"...",
    "branch":"master",
    "isTag":false,
    "commitId":"3a9e50dfb4dbf3c463b57ffd6453bf9cc103bcff",
    "authorName":"...",
    "authorUsername":"...",
    "committerName":"...",
    "committerUsername":"...",
    "committed":"2015-03-29T06:59:41+00:00",
    "pullRequestId":"86",
    "pullRequestName":"...",
    "messages":[],
    "status":"queued",
    "created":"2015-03-29T19:49:00.2126257+00:00"
}
```

### Cancel build

Request:

    DELETE /api/builds/{accountName}/{projectSlug}/{buildVersion}

Response: 204

### Delete build

Request:

    DELETE /api/builds/{buildId}

Response: 204

### Download build log

Request:

    GET /api/buildjobs/{jobId}/log

Response:

Text file with the log.
