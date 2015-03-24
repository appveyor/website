---
layout: docs
title: Team API
---

# Team

### Users

* [Get users](#get-users)
* [Get user](#get-user)
* [Add user](#add-user)
* [Update user](#update-user)
* [Delete user](#delete-user)

### Collaborators

* [Get collaborators](#get-collaborators)
* [Get collaborator](#get-collaborator)
* [Add collaborator](#add-collaborator)
* [Update collaborator](#update-collaborator)
* [Delete collaborator](#delete-collaborator)

### Roles

* [Get roles](#get-roles)
* [Get role](#get-role)
* [Add role](#add-role)
* [Update role](#update-role)
* [Delete role](#delete-role)


## Get users

Request:

    GET /api/users

Response:

    [
       {
          "accountId":2,
          "accountName":"FeodorFitsner",
          "isOwner":false,
          "isCollaborator":false,
          "userId":2019,
          "fullName":"NuGet",
          "email":"nuget@appveyor.com",
          "roleId":5,
          "roleName":"User",
          "successfulBuildNotification":"all",
          "failedBuildNotification":"all",
          "notifyWhenBuildStatusChangedOnly":true,
          "created":"2014-02-12T19:21:15.0618564+00:00",
          "updated":"2014-03-06T22:47:44.9706252+00:00"
       }
    ]


## Get user

Request:

    GET /api/users/{userId}

Response:

    {
       "user":{
          "accountId":2,
          "accountName":"FeodorFitsner",
          "isOwner":false,
          "isCollaborator":false,
          "userId":2019,
          "fullName":"NuGet",
          "email":"nuget@appveyor.com",
          "roleId":5,
          "roleName":"User",
          "successfulBuildNotification":"all",
          "failedBuildNotification":"all",
          "notifyWhenBuildStatusChangedOnly":true,
          "created":"2014-02-12T19:21:15.0618564+00:00",
          "updated":"2014-03-06T22:47:44.9706252+00:00"
       },
       "roles":[
          {
             "roleId":4,
             "name":"Administrator",
             "isSystem":true,
             "created":"2013-09-26T19:23:39.3615105+00:00"
          },
          {
             "roleId":5,
             "name":"User",
             "isSystem":true,
             "created":"2013-09-26T19:23:39.3645117+00:00"
          }
       ]
    }


## Add user

Request:

    POST /api/users

Request body:

    {
       "fullName":"John Smith",
       "email":"john@smith.com",
       "roleId":4,
       "generatePassword":false,
       "password":"password",
       "confirmPassword":"password"
    }

Response: 204


## Update user

Request:

    PUT /api/users

Request body:

    {
       "userId":3019,
       "fullName":"John Smith",
       "email":"john@smith.com",
       "password": null,
       "roleId":4,
       "successfulBuildNotification":"all",
       "failedBuildNotification":"all",
       "notifyWhenBuildStatusChangedOnly":true
    }

Response: 204



## Delete user

Request:

    DELETE /api/users/{userId}

Response: 204






## Get collaborators

Request:

    GET /api/collaborators

Response:

    [
       {
          "accountId":2,
          "accountName":"FeodorFitsner",
          "isOwner":false,
          "isCollaborator":true,
          "userId":2018,
          "fullName":"John Smith",
          "email":"john@smith.com",
          "roleId":3040,
          "roleName":"My Role",
          "successfulBuildNotification":"all",
          "failedBuildNotification":"all",
          "notifyWhenBuildStatusChangedOnly":true,
          "created":"2014-02-03T20:29:26.6807307+00:00",
          "updated":"2014-03-07T04:26:09.1051534+00:00"
       }
    ]


## Get collaborator

Request:

    GET /api/collaborators/{userId}

Response:

    {
       "user":{
          "accountId":2,
          "accountName":"FeodorFitsner",
          "isOwner":false,
          "isCollaborator":true,
          "userId":2018,
          "fullName":"John Smith",
          "email":"john@smith.com",
          "roleId":3040,
          "roleName":"My Role",
          "successfulBuildNotification":"all",
          "failedBuildNotification":"all",
          "notifyWhenBuildStatusChangedOnly":true,
          "created":"2014-02-03T20:29:26.6807307+00:00",
          "updated":"2014-03-07T04:26:09.1051534+00:00"
       },
       "roles":[
          {
             "roleId":4,
             "name":"Administrator",
             "isSystem":true,
             "created":"2013-09-26T19:23:39.3615105+00:00"
          },
          {
             "roleId":5,
             "name":"User",
             "isSystem":true,
             "created":"2013-09-26T19:23:39.3645117+00:00"
          },
          {
             "roleId":3040,
             "name":"My Role",
             "isSystem":false,
             "created":"2014-03-18T20:12:08.4749886+00:00",
             "updated":"2014-03-18T20:16:06.8803375+00:00"
          }
       ]
    }


## Add collaborator

Request:

    POST /api/collaborators

Request body:

    {
       "email":"john@smith.com",
       "roleId":3040
    }

Response: 204

## Update collaborator

Request:

    PUT /api/collaborators

Request body:

    {
       "userId":2018,
       "roleId":3040
    }

Response: 204



## Delete collaborator

Request:

    DELETE /api/collaborators/{userId}

Response: 204






## Get roles

Request:

    GET /api/roles

Response:

    [
       {
          "roleId":4,
          "name":"Administrator",
          "isSystem":true,
          "created":"2013-09-26T19:23:39.3615105+00:00"
       },
       {
          "roleId":5,
          "name":"User",
          "isSystem":true,
          "created":"2013-09-26T19:23:39.3645117+00:00"
       }
    ]


## Get role

Request:

    GET /api/users/{roleId}

Response:

    {
       "roleId":3040,
       "name":"My Role",
       "isSystem":false,
       "created":"2014-03-18T20:12:08.4749886+00:00",
       "groups":[
          {
             "name":"Projects",
             "permissions":[
                {
                   "name":"ManageProjects",
                   "description":"Create, delete projects, update project settings",
                   "allowed":false
                },
                {
                   "name":"UpdateProjectSettings",
                   "description":"Update project settings",
                   "allowed":false
                },
                {
                   "name":"RunProjectBuild",
                   "description":"Run project builds",
                   "allowed":false
                },
                {
                   "name":"DeleteProjectBuilds",
                   "description":"Delete project builds",
                   "allowed":false
                }
             ]
          },
          {
             "name":"Environments",
             "permissions":[
                {
                   "name":"ManageEnvironments",
                   "description":"Create, delete projects, update environment settings",
                   "allowed":false
                },
                {
                   "name":"UpdateEnvironmentSettings",
                   "description":"Update environment settings",
                   "allowed":false
                },
                {
                   "name":"DeployToEnvironment",
                   "description":"Deploy to environment",
                   "allowed":false
                }
             ]
          },
          {
             "name":"Account",
             "permissions":[
                {
                   "name":"UpdateAccountDetails",
                   "description":"Update account details",
                   "allowed":false
                }
             ]
          },
          {
             "name":"Users",
             "permissions":[
                {
                   "name":"AddUser",
                   "description":"Add new user",
                   "allowed":false
                },
                {
                   "name":"UpdateUserDetails",
                   "description":"Update user details",
                   "allowed":false
                },
                {
                   "name":"DeleteUser",
                   "description":"Delete user",
                   "allowed":false
                }
             ]
          },
          {
             "name":"Roles",
             "permissions":[
                {
                   "name":"AddRole",
                   "description":"Add new role",
                   "allowed":false
                },
                {
                   "name":"UpdateRoleDetails",
                   "description":"Update role details",
                   "allowed":false
                },
                {
                   "name":"DeleteRole",
                   "description":"Delete role",
                   "allowed":false
                }
             ]
          },
          {
             "name":"User",
             "permissions":[
                {
                   "name":"ConfigureApiKeys",
                   "description":"Generate API keys",
                   "allowed":false
                }
             ]
          }
       ]
    }


## Add role

Request:

    POST /api/roles

Request body:

    {
       "name":"My Role"
    }

Response: Role details (see [Get role](#get-role))


## Update role

Request:

    PUT /api/roles

Request body:

    {
       "roleId":3040,
       "name":"My Role",
       "isSystem":false,
       "created":"2014-03-18T20:12:08.4749886+00:00",
       "groups":[
          {
             "name":"Projects",
             "permissions":[
                {
                   "name":"ManageProjects",
                   "description":"Create, delete projects, update project settings",
                   "allowed":true
                },
                {
                   "name":"UpdateProjectSettings",
                   "description":"Update project settings",
                   "allowed":true
                },
                {
                   "name":"RunProjectBuild",
                   "description":"Run project builds",
                   "allowed":false
                },
                {
                   "name":"DeleteProjectBuilds",
                   "description":"Delete project builds",
                   "allowed":false
                }
             ]
          },
          {
             "name":"Environments",
             "permissions":[
                {
                   "name":"ManageEnvironments",
                   "description":"Create, delete projects, update environment settings",
                   "allowed":false
                },
                {
                   "name":"UpdateEnvironmentSettings",
                   "description":"Update environment settings",
                   "allowed":false
                },
                {
                   "name":"DeployToEnvironment",
                   "description":"Deploy to environment",
                   "allowed":false
                }
             ]
          },
          {
             "name":"Account",
             "permissions":[
                {
                   "name":"UpdateAccountDetails",
                   "description":"Update account details",
                   "allowed":false
                }
             ]
          },
          {
             "name":"Users",
             "permissions":[
                {
                   "name":"AddUser",
                   "description":"Add new user",
                   "allowed":false
                },
                {
                   "name":"UpdateUserDetails",
                   "description":"Update user details",
                   "allowed":false
                },
                {
                   "name":"DeleteUser",
                   "description":"Delete user",
                   "allowed":false
                }
             ]
          },
          {
             "name":"Roles",
             "permissions":[
                {
                   "name":"AddRole",
                   "description":"Add new role",
                   "allowed":false
                },
                {
                   "name":"UpdateRoleDetails",
                   "description":"Update role details",
                   "allowed":false
                },
                {
                   "name":"DeleteRole",
                   "description":"Delete role",
                   "allowed":false
                }
             ]
          },
          {
             "name":"User",
             "permissions":[
                {
                   "name":"ConfigureApiKeys",
                   "description":"Generate API keys",
                   "allowed":false
                }
             ]
          }
       ]
    }

Response: Role details (see [Get role](#get-role))


## Delete role

Request:

    DELETE /api/roles/{roleId}

Response: 204
