---
layout: docs
title: Environments and deployments API
---

# Environments API

### Environments

* [Get environment](#get-environments)
* [Get environment settings](#get-environment-settings)
* [Get environment deployments](#get-environment-deployments)
* [Add environment](#add-environment)
* [Update environment](#update-environment)
* [Delete environment](#delete-environment)

### Deployments

* [Get deployment](#get-deployment)
* [Start deployment](#start-deployment)
* [Cancel deployment](#cancel-deployment)

## Get environments

Request:

    GET /api/environments

Response:

    [
        {
            "deploymentEnvironmentId": 14,
            "name": "azure-blob-1",
            "provider": "AzureBlob",
            "created": "2014-01-23T18:13:52.2268502+00:00",
            "updated": "2014-06-02T18:13:32.5106126+00:00"
        },
        {
            "deploymentEnvironmentId": 12,
            "name": "azure-deploy-test",
            "provider": "AzureCS",
            "created": "2014-01-23T06:46:31.1215919+00:00",
            "updated": "2014-08-11T18:48:06.2002179+00:00"
        }
    ]



## Get environment settings

Request:

    GET /api/environments/{deploymentEnvironmentId}/settings

Response:

    {
        "environment": {
            "deploymentEnvironmentId": 14,
            "name": "azure-blob-1",
            "provider": "AzureBlob",
            "environmentAccessKey": "aaabbb12345",
            "settings": {
                "providerSettings": [
                    {
                        "name": "storage_account_name",
                        "value": {
                            "isEncrypted": false,
                            "value": "myaccount"
                        }
                    },
                    {
                        "name": "storage_access_key",
                        "value": {
                            "isEncrypted": true,
                            "value": "4sc1c7/Qp5buQcZ8N486Ks46mLFbXJVqcJjyv98w=="
                        }
                    },
                    {
                        "name": "container",
                        "value": {
                            "isEncrypted": false,
                            "value": "test"
                        }
                    },
                    {
                        "name": "folder",
                        "value": {
                            "isEncrypted": false,
                            "value": "$(appveyor_build_version)"
                        }
                    },
                    {
                        "name": "artifact",
                        "value": {
                            "isEncrypted": false
                        }
                    }
                ],
                "environmentVariables": []
            },
            "created": "2014-01-23T18:13:52.2268502+00:00",
            "updated": "2014-06-02T18:13:32.5106126+00:00"
        }
    }



## Get environment deployments

Request:

    GET /api/environments/{deploymentEnvironmentId}/deployments

Response:

    {
       "environment":{
          "deploymentEnvironmentId":14,
          "name":"azure-blob-1",
          "provider":"AzureBlob",
          "created":"2014-01-23T18:13:52.2268502+00:00",
          "updated":"2014-06-02T18:13:32.5106126+00:00"
       },
       "deployments":[
          {
             "project":{
                "projectId":15072,
                "accountId":2,
                "accountName":"FeodorFitsner",
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
                "created":"2014-05-06T16:38:14.7788393+00:00",
                "updated":"2014-06-02T21:37:30.9378043+00:00"
             },
             "deployment":{
                "deploymentId":4120,
                "build":{
                   "buildId":18665,
                   "jobs":[

                   ],
                   "buildNumber":25,
                   "version":"1.0.25",
                   "message":"Merge pull request #3 from FeodorFitsner/master",
                   "messageExtended":"Changes to AccountController",
                   "branch":"master",
                   "commitId":"ed40bd27f732d162b2185d75921b1cd57191f83b",
                   "authorName":"Feodor Fitsner",
                   "authorUsername":"FeodorFitsner",
                   "committerName":"Feodor Fitsner",
                   "committerUsername":"FeodorFitsner",
                   "committed":"2014-05-08T19:11:38+00:00",
                   "messages":[

                   ],
                   "status":"success",
                   "started":"2014-05-22T20:12:11.4475134+00:00",
                   "finished":"2014-05-22T20:12:33.7806881+00:00",
                   "created":"2014-05-22T20:09:53.759355+00:00",
                   "updated":"2014-05-22T20:12:33.7806881+00:00"
                },
                "environment":{
                   "deploymentEnvironmentId":14,
                   "name":"azure-blob-1",
                   "provider":"AzureBlob",
                   "created":"2014-01-23T18:13:52.2268502+00:00",
                   "updated":"2014-06-02T18:13:32.5106126+00:00"
                },
                "jobs":[
                   {
                      "jobId":"1696fh3a2w5ng99y",
                      "name":"Deployment",
                      "messagesCount":0,
                      "status":"success",
                      "started":"2014-07-27T09:59:58.3955159+00:00",
                      "finished":"2014-07-27T10:00:11.5995296+00:00",
                      "created":"2014-07-27T09:59:57.0171035+00:00",
                      "updated":"2014-07-27T10:00:16.163082+00:00"
                   }
                ],
                "status":"success",
                "started":"2014-06-02T18:20:07.9871288+00:00",
                "finished":"2014-06-02T18:20:25.11916+00:00",
                "created":"2014-06-02T18:20:07.2833871+00:00",
                "updated":"2014-06-02T18:20:25.11916+00:00"
             }
          }
       ]
    }


## Add environment

Request:

    POST /api/environments

Request body:

    {
       "name":"production",
       "provider":"FTP",
       "settings":{
          "providerSettings":[
             {
                "name":"server",
                "value":{
                   "value":"ftp.myserver.com",
                   "isEncrypted":false
                }
             },
             {
                "name":"username",
                "value":{
                   "value":"ftp-user",
                   "isEncrypted":false
                }
             },
             {
                "name":"password",
                "value":{
                   "value":"password",
                   "isEncrypted":true
                }
             }
          ],
          "environmentVariables":[
             {
                "name":"my-var",
                "value":{
                   "value":"123",
                   "isEncrypted":false
                }
             }
          ]
       }
    }

Response:

    {
       "deploymentEnvironmentId":3018,
       "name":"production",
       "provider":"FTP",
       "environmentAccessKey":"gi3ttevuk7a123",
       "settings":{
          "providerSettings":[
             {
                "name":"server",
                "value":{
                   "isEncrypted":false,
                   "value":"ftp.myserver.com"
                }
             },
             {
                "name":"username",
                "value":{
                   "isEncrypted":false,
                   "value":"ftp-user"
                }
             },
             {
                "name":"password",
                "value":{
                   "isEncrypted":true,
                   "value":"password"
                }
             }
          ],
          "environmentVariables":[
             {
                "name":"my-var",
                "value":{
                   "isEncrypted":false,
                   "value":"123"
                }
             }
          ]
       },
       "created":"2014-08-15T23:57:16.1585268+00:00"
    }



## Update environment

Request:

    PUT /api/environments

Request body:

    {
       "deploymentEnvironmentId":3018,
       "name":"production",
       "environmentAccessKey":"gi3ttevuk7123",
       "settings":{
          "providerSettings":[
             {
                "name":"server",
                "value":{
                   "isEncrypted":false,
                   "value":"ftp.myserver.com"
                }
             },
             {
                "name":"username",
                "value":{
                   "isEncrypted":false,
                   "value":"ftp-user"
                }
             },
             {
                "name":"password",
                "value":{
                   "isEncrypted":true,
                   "value":"password"
                }
             }
          ],
          "environmentVariables":[
             {
                "name":"my-var",
                "value":{
                   "isEncrypted":false,
                   "value":"123"
                }
             }
          ],
          "provider":"FTP"
       }
    }

Response:

    {
       "deploymentEnvironmentId":3018,
       "name":"production",
       "provider":"FTP",
       "environmentAccessKey":"gi3ttevuk7123",
       "settings":{
          "providerSettings":[
             {
                "name":"server",
                "value":{
                   "isEncrypted":false,
                   "value":"ftp.myserver.com"
                }
             },
             {
                "name":"username",
                "value":{
                   "isEncrypted":false,
                   "value":"ftp-user"
                }
             },
             {
                "name":"password",
                "value":{
                   "isEncrypted":true,
                   "value":"password"
                }
             }
          ],
          "environmentVariables":[
             {
                "name":"my-var",
                "value":{
                   "isEncrypted":false,
                   "value":"123"
                }
             }
          ]
       },
       "created":"2014-08-15T23:57:16.1585268+00:00",
       "updated":"2014-08-16T00:00:37.6079863+00:00"
    }



## Delete environment

Request:

    DELETE /api/environments/{deploymentEnvironmentId}

Response: 204


### Get deployment

Request:

    GET /api/deployments/{deploymentId}

Response:

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
             }
          ],
          "status":"success",
          "started":"2014-08-12T23:06:10.8776088+00:00",
          "finished":"2014-08-12T23:06:25.0502019+00:00",
          "created":"2014-08-12T23:06:07.9009315+00:00",
          "updated":"2014-08-12T23:06:25.0502019+00:00"
       }
    }



### Start deployment

Request:

    POST /api/deployments

Request body:

    {
        environmentName: 'environment-to-deploy',
        accountName: 'your-account-name',
        projectSlug: 'project-slug-from-url',
        buildVersion: '1.2.0',                      # build to deploy
        buildJobId: 'sfke9239ydzf',                 # optional job id with artifacts if build contains multiple jobs
        environmentVariables: {
           server: 'myserver.com',
           another_var: 'another value'
        }
    }

Response:

    {
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
          }
       ],
       "status":"success",
       "started":"2014-08-12T23:06:10.8776088+00:00",
       "finished":"2014-08-12T23:06:25.0502019+00:00",
       "created":"2014-08-12T23:06:07.9009315+00:00",
       "updated":"2014-08-12T23:06:25.0502019+00:00"
    }


### Cancel deployment

Request:

    PUT /api/deployments/stop

Request body:

    {
        deploymentId: 123
    }

Response: 204