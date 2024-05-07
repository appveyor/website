---
layout: docs
title: Deploying to Amazon S3 bucket
---

# Deploying to Amazon S3 bucket

Amazon S3 deployment provider copies all or selected artifacts to Amazon S3 storage.

## Provider settings

* **Access key ID** (`access_key_id`) - AWS account access key.
* **Secret access key** (`secret_access_key`) - AWS secret access key.
* **Bucket name** (`bucket`) - The name of bucket to which artifacts are copied.
* **Region** (`region`) - AWS region where the bucket is located.
* **Folder** (`folder`) - Name of folder to copy to.
* **Artifact** (`artifact`) - Name of artifact(s) to copy. If more than one, separate with commas (,).
* **Unpack zip artifacts and upload their contents as individual blobs** (`unzip`) - Optional. Default is `false`.
* **Remove all bucket/folder files before deployment** (`remove_files`) - Optional. Default is `false`.
* **Enable public access to published S3 objects** (`set_public`) - Optional. Default is `false`.
* **Enable server-side encryption (AES-256)** (`encrypt`) - Optional. Default is `false`.
* **Set blobs Content-Type header based on file extensions** (`set_content_type`) - set `true` to automatically configure content types based on file extensions, which default to `application/octet-stream` otherwise. Default is `false`.
* **gzip blobs on upload and set Content-Encoding header** (`gzip_files`) - compress blobs satisfying file mask(s) on upload using gzip algorithm and set `Content-Encoding: gzip` header. File mask examples:
    * `**/*.js` - gzip `.js` files in all directories recursively;
    * `js/*.js` - gzip `.js` files in `js` directory only;
    * `**/*.js, **/*.html` - gzip `.js` and `.html` files in all directories recursively.
* **Custom blob headers** (`headers`) - Optional. Allows to attach custom headers to blobs satisfying file mask(s). See custom headers specification below.
* **Use Reduced Redundancy Storage** (`reduced_redundancy`) - Optional. Default is `false`.
* **Retry attempts** (`max_error_retry`) - Optional. Number of times provider will retry after a failure. Default is `0`.

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

Example 1 - uploading all build artifacts "as is" to remote `release` directory:

```yaml
deploy:
  provider: S3
  access_key_id: <access-key>
  secret_access_key: <secret-key>
  bucket: company-downloads
  region: eu-west-1
  folder: release
```

Example 2 - deploy static site by expanding zip artifact with deployment name `MyApp` to the root of S3 bucket:

```yaml
deploy:
  provider: S3
  artifact: MyApp
  access_key_id: <access-key>
  secret_access_key: <secret-key>
  bucket: my-static-site-example
  region: eu-west-1
  unzip: true
  set_public: true
  remove_files: true
  set_content_type: true
  gzip_files: '**/*.js, **/*.html'
  headers: |
    **/*.js
      Cache-Control: max-age=3600
    **/*.zip
      Content-Disposition: attachment
```
