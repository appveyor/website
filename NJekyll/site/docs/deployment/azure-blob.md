---
layout: docs
title: Deploying to Azure blob storage
---

# Deploying to Azure blob storage

Azure blob storage provider copies all or selected artifacts to Windows Azure storage.

## Provider settings

* **Storage account name** (`storage_account_name`) - Azure storage account name.
* **Storage access key** (`storage_access_key`) - storage account access key.
* **Container name** (`container`) - the name of storage container to copy to. Container name length must be between 3 and 63 symbols.
* **Folder** (`folder`) - name of folder to copy to.
* **Artifact** (`artifact`) - name of artifact to copy.

Configuring in `appveyor.yml`:

    deploy:
      provider: AzureBlob
      storage_account_name: mystorage
      storage_access_key:
        secure: ZZbm8KKD1lLCi9btF1fDkQ==
      container: builds
      folder: $(APPVEYOR_PROJECT_SLUG)\$(APPVEYOR_BUILD_VERSION)
      artifact: myapp
