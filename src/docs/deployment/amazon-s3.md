---
layout: docs
title: Deploying to Amazon S3 bucket
---

# Deploying to Amazon S3 bucket

Amazon S3 deployment provider copies all or selected artifacts to Amazon S3 storage.

## Provider settings

* **Access key ID** (`access_key_id`) - AWS account access key.
* **Secret access key** (`secret_access_key`) - AWS secret access key.
* **Bucket name** (`bucket`) - the name of bucket to copy artifacts to.
* **Region** (`region`) - AWS region where the bucket is located.
* **Folder** (`folder`) - name of folder to copy to.
* **Artifact** (`artifact`) - name of artifact to copy.

Configuring in `appveyor.yml`:

```yaml
deploy:
  provider: S3
  access_key_id:
  secret_access_key:
  bucket:
  region: eu-west-1
  set_public: true|false (disabled by default)
  folder:
  artifact:
```
