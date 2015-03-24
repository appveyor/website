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
* **Unzip artifacts before uploading to Azure Storage** (`unzip`) - set `true` to unpack `Zip` artifacts before uploading to storage. Default is `false`.
* **Set blobs *Content Type* based on file extensions** (`set_content_type`) - set `true` to automatically configure content types based on file extensions, which default to `application/octet-stream` otherwise. Default is `false`.

Configuring in `appveyor.yml`:

{% highlight yaml %}
deploy:
  provider: AzureBlob
  storage_account_name: mystorage
  storage_access_key:
    secure: ZZbm8KKD1lLCi9btF1fDkQ==
  container: builds
  folder: $(APPVEYOR_PROJECT_SLUG)\$(APPVEYOR_BUILD_VERSION)
  artifact: myapp
  unzip: false
  set_content_type: false
{% endhighlight %}
