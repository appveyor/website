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
* **Unzip artifacts before uploading to Amazon S3 Storage** (`unzip`) - Optional. Default is `false`.
* **Enable public access to published S3 objects** (`set_public`) - Optional. Default is `false`.
* **Enable server-side encryption (AES-256)** (`encrypt`) - Optional. Default is `false`.
* **Use Reduced Redundancy Storage** (`reduced_redundancy`) - Optional. Default is `false`.
* **Retry attempts** (`max_error_retry`) - Optional. Number of times provider will retry after a failure. Default is `0`.

Configuring in `appveyor.yml`:

```yaml
deploy:
  provider: S3
  access_key_id:
  secret_access_key:
  bucket:
  region: eu-west-1
  unzip: true|false (disabled by default)
  set_public: true|false (disabled by default)
  folder:
  artifact:
```
