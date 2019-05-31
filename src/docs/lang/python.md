---
layout: docs
title: Testing with Python
---

<!-- markdownlint-disable MD022 MD032 -->
# Testing with Python
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Installation

Several Python versions are already preinstalled:

* [Windows images](/docs/windows-images-software/#python)
* [Ubuntu images](/docs/linux-images-software/#python)

## Quick start

Add an `appveyor.yml` file to your repository root:

```yaml
# appveyor.yml
---
environment:
  matrix:
  - TOXENV: py27
  - TOXENV: py35
  - TOXENV: py36
  - TOXENV: py37

build: off

install:
- pip install tox

test_script:
- tox
```

### Test setup

A popular approach in the Python community for test setups and automation is
to use [Tox](https://tox.readthedocs.io/). This has a number of advantages:

* It is self-contained, allowing you to encapsulate all dependencies for running your tests.
* You can run the entire test suite on your developer machine upfront, with a single command.
* It creates isolated environments for running your tests using `virtualenv`, automatically.
* It makes you independent from implementation details of any CI service.

With the AppVeyor configuration from above you can now put your entire test
setup into a `tox.ini` file in your repository root:

```ini
# tox.ini

[tox]
envlist = py{27,35,36,37}

[testenv]
description = Unit tests
deps = pytest
commands = pytest
```

For any Tox environment you want to run on AppVeyor you need to add a
`TOXENV: ...` item to your environment matrix in `appveyor.yml`.

See [usage examples](https://tox.readthedocs.io/en/latest/example/basic.html)
and [configuration specs](https://tox.readthedocs.io/en/latest/config.html)
for more information on how to use Tox.

## Testing against PyPy

PyPy and PyPy3 are not yet supported on AppVeyor out-of-the-box. You can,
however, install them yourself as additional interpreters in your `appveyor.yml`.

See the "[How do I install PyPy on AppVeyor](
https://stackoverflow.com/questions/30822873/how-do-i-install-pypy-on-appveyor
)" question on StackOverflow for a possible strategy.
