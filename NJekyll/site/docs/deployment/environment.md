---
title: Deploying to existing environment
---

# Deploying to existing environment

Environment deployment provider starts asynchronous deployment to the existing environment with specified name.

## Provider settings

* **Environment name** (`name`) - the name of environment to start deployment to.

Configuring in `appveyor.yml`:

    deploy:
      provider: Environment
      name: staging