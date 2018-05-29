---
layout: docs
title: Testing with Go
---

<!-- markdownlint-disable MD022 MD032 -->
# Testing with go
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Install

Several Go versions are already preinstalled
(see [details](/docs/build-environment/#golang)).
We only need to add GOPATH and clone our project in
the right directory.

## Building

Go supports both windows and linux environments.

### Windows

```yaml
# appveyor.yml
build: off

clone_folder: c:\gopath\src\github.com\$username\$project

environment:
  GOPATH: c:\gopath

stack: go 1.10

before_test:
  - go vet ./...

test_script:
  - go test ./...
```

### Linux

```yaml
# appveyor.yml
build: off

clone_folder: /usr/go/src/github.com/$username/$project

environment:
  GOPATH: /usr/go/

stack: go 1.10

before_test:
  - go vet ./...

test_script:
  - go test ./...
```
