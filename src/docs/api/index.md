---
layout: docs
title: AppVeyor REST API
---

# Overview

APIs:

* [Teams (Users, Roles, Collaborators)](/docs/api/team/)
* [Projects and Builds](/docs/api/projects-builds/)
* [Environments and Deployments](/docs/api/environments-deployments/)

Samples:

* [Downloading build artifact (PowerShell - basic example)](/docs/api/samples/download-artifacts-ps/)
* [Downloading build artifact (PowerShell - advanced example)](/docs/api/samples/download-artifacts-advanced-ps/)
* [Downloading build artifact (ShellScript - advanced example)](/docs/api/samples/download-artifacts-sh/)

## Authentication

AppVeyor uses bearer token authentication. Token can be found on [API token page](https://ci.appveyor.com/api-keys) under your AppVeyor account.

Token must be set in `Authorization` header of every request to AppVeyor REST API:

    Authorization: Bearer <token>

Default content type is JSON, but if you need to return XML set `Accept` header:

    Accept: application/xml

**Note.** If using a user-level API key (v2) which allows working with any account user has access to, then
API calls must be prepended with /api/account/<account-name>/ in order to disambiguate which account is being accessed.

## Samples

### PowerShell

The following PowerShell code uses standard `Invoke-RestMethod` cmdlet to return the list of roles from authenticated account:

```powershell
$token = '<your-api-token>'
$headers = @{}
$headers['Authorization'] = "Bearer $token"
$headers["Content-type"] = "application/json"
Invoke-RestMethod -Uri 'https://ci.appveyor.com/api/roles' -Headers $headers -Method Get
```

<!-- markdownlint-disable MD003 MD022 -->
### C\#
<!-- markdownlint-enable MD003 MD022 -->

The following code sample uses `HttpClient` class from `Microsoft.AspNet.WebApi.Client` NuGet package to get the list of roles from authenticated account:

```csharp
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
```

### curl

The following command uses `curl` to get the list of roles from authenticated account:

```bash
export APPVEYOR_TOKEN="<your-api-token>"
curl -H "Authorization: Bearer $APPVEYOR_TOKEN" -H "Content-Type: application/json" https://ci.appveyor.com/api/roles
```

**Note.** If you plan to download artifacts with curl you should update curl up to 7.58.0. Otherwise curl won't be able to download artifacts due to CVE-2018-1000007.
