---
layout: docs
title: Deploying to existing environment
---

# Deploying to existing environment

Environment deployment provider starts asynchronous deployment to the existing environment with specified name.  If you don't have any environments set up yet, you can create one at https://ci.appveyor.com/environments.

## Provider settings

* **Environment name** (`name`) - the name of environment to start deployment to.

Configuring in `appveyor.yml`:

    deploy:
      provider: Environment
      name: staging

## Overriding environment variables

The only required setting for "Environment" deployment provider is `name` - all other key-values are passed into environment deployment context as variables.

For example, you may have `Azure Web Sites` Web Deploy environment with `Server` setting defined as:

    https://$(website_name).scm.azurewebsites.net:443/msdeploy.axd?site=$(website_name)

Then, when deploying to that environment during the build you can define `website_name` variable for each deployment as:

    deploy:
    - provider: Environment
      name: Azure Web Sites
      website_name: Site-A

    - provider: Environment
      name: Azure Web Sites
      website_name: Site-B
