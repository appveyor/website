---
layout: docs
title: Deploying to BinTray
---

# Deploying to BinTray

AppVeyor BinTray deployment provider uploads all or selected artifacts to [BinTray](https://bintray.com/) **generic repository**. 

## Provider settings

* **BinTray username** (`username`) - your BinTray account name.
* **BinTray API key** (`api_key`) - AWS secret access key.
* **Subject** (`subject`) - your BinTray account name.
* **Repository** (`repo`) - name of repository to upload artifacts to. Only "Generic" repositories are supported at the moment.
* **Package** (`package`) - name of package to upload artifacts to. Deployment will fail if specified package does not exists.
* **Version** (`version`) - Optional. Version to upload files under. If `version` is not specified build version will be used. If version does not exists it will be automatically created.
* **Publish** (`publish`) - Optional. `true` if uploaded artifacts must be marked as "published". Default is `false` - artifacts await publishing upon upload.
* **Override** (`override`) - Optional. `true` if existing files can be overwritten. Default is `false` and deployment will fail if the file already exists on BinTray.
* **Explode** (`explode`) - Optional. `true` if archive artifacts must be unpacked (exploded) during upload. Default is `false`. Files with the following extensions can be unpacked during upload: `.zip`, `.tar`, `.gz`, `.gzip`, `.boz`, `.bz2`
* **Artifact** (`artifact`) - Optional. Artifact "deployment name" or filename to push. If not specified all artifacts from selected build will be uploaded. This can be a regexp, e.g. `/.*\.zip/`

Configuring in `appveyor.yml`:

    deploy:
    - provider: BinTray
      username: johnsmith
      api_key:
        secure: AABBCC+DDD==
      subject: johnsmith
      repo: myrepo
      package: mypackage
      version: version
      publish: true
      override: true
      explode: true

