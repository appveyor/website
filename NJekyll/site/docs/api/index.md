---
title: AppVeyor REST API
---

# Overview

APIs:

* [Teams (Users, Roles, Collaborators)](/docs/api/team)
* [Projects and Builds](/docs/api/projects-builds)
* [Environments and Deployments](/docs/api/environments-deployments)

## Authentication

AppVeyor uses bearer token authentication. Token can be found on **API token** page under your AppVeyor account.

Token must be set in `Authorization` header of every request to AppVeyor REST API:

	Authorization: Bearer <token>

Default content type is JSON, but if you need to return XML set `Accept` header:

    Accept: application/xml

## Samples

### PowerShell

The following PowerShell code uses standard `Invoke-RestMethod` cmdlet to return the list of roles from authenticated account:

	$token = '<your-api-token>'
	$headers = @{
      "Authorization" = "Bearer $token"
      "Content-type" = "application/json"
    }
	Invoke-RestMethod -Uri 'https://ci.appveyor.com/api/roles' -Headers $headers -Method Get

### C&#35;

The following code sample uses `HttpClient` class from `Microsoft.AspNet.WebApi.Client` NuGet package to get the list of roles from authenticated account:

    string token = "<your-api-token>";
    using(var client = new HttpClient())
    {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // get the list of roles
        using (var response = await client.GetAsync("https://ci.appveyor.com/api/roles"))
        {
            response.EnsureSuccessStatusCode();

            var roles = await response.Content.ReadAsAsync<JToken[]>();
            foreach (var role in roles)
            {
                Console.WriteLine(role.Value<string>("name"));
            }
        }
    }



