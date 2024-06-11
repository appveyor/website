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
* **Unpack zip artifacts and upload their contents as individual blobs** (`unzip`) - set `true` to unpack `Zip` artifacts before uploading to storage. Default is `false`.
* **Remove all bucket/folder files before deployment** (`remove_files`) - Optional. Default is `false`.
* **Set blobs Content-Type header based on file extensions** (`set_content_type`) - set `true` to automatically configure content types based on file extensions, which default to `application/octet-stream` otherwise. Default is `false`.
* **gzip blobs on upload and set Content-Encoding header** (`gzip_files`) - compress blobs satisfying file mask(s) on upload using gzip algorithm and set `Content-Encoding: gzip` header. File mask examples:
    * `**/*.js` - gzip `.js` files in all directories recursively;
    * `js/*.js` - gzip `.js` files in `js` directory only;
    * `**/*.js, **/*.html` - gzip `.js` and `.html` files in all directories recursively.
* **Custom blob headers** (`headers`) - Optional. Allows to attach custom headers to blobs satisfying file mask(s). See custom headers specification below.

## Custom headers specification

Custom headers field has the following format:

```text
glob_1
  Header-Name: header value
  ...
glob_2
  Header-Name: header value
...
```

For example:

```text
**/*.js
  Cache-Control: max-age=3600
**/*.zip
  Content-Disposition: attachment
```

and the same in `appveyor.yml`:

```yaml
deploy:
  provider: S3
  ...
  headers: |
    **/*.js
      Cache-Control: max-age=3600
    **/*.zip
      Content-Disposition: attachment
```

## appveyor.yml configuration

Example 1 - uploading all build artifacts "as is" to remote `{project-slug}\{build-version}` directory:

```yaml
deploy:
  provider: AzureBlob
  storage_account_name: mystorage
  storage_access_key:
    secure: ZZbm8KKD1lLCi9btF1fDkQ==
  container: builds
  folder: $(APPVEYOR_PROJECT_SLUG)\$(APPVEYOR_BUILD_VERSION)
```

Example 2 - deploy static site by expanding zip artifact with deployment name `MyApp` to the root of `$web` container:

```yaml
deploy:
  provider: AzureBlob
  storage_account_name: mystorage
  storage_access_key:
    secure: ZZbm8KKD1lLCi9btF1fDkQ==
  container: $web
  unzip: true
  remove_files: true
  set_content_type: true
  gzip_files: '**/*.js, **/*.html'
  headers: |
    **/*.js
      Cache-Control: max-age=3600
    **/*.zip
      Content-Disposition: attachment
```
