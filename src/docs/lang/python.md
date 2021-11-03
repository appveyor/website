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
  # - TOXENV: py27  # https://devguide.python.org/devcycle/#end-of-life-branches
  # - TOXENV: py35
  - TOXENV: py36    # https://devguide.python.org/#status-of-python-branches
  - TOXENV: py37
  - TOXENV: py38
  # - TOXENV: py39    # Not yet present in Appveyor image
  # - TOXENV: py310   # Not yet present in Appveyor image

build: off

install:
  - py -0  # Show all available versions of Python
  - py -m pip install --upgrade pip
  - py -m pip install tox

test_script:
  - py -m tox
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
envlist = py3{6,7,8}

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


### Note about dependency management

Since pipenv prints [some statements to stderr](https://github.com/pypa/pipenv/issues/2945), you should to silence it if it is the dependency manager you are using.

```yaml
# appveyor.yml
...
  - ps: python -Wignore -m pip install pipenv
  - ps: rm Pipfile.lock
  - ps: $PIPENV_QUIET="true"
  - pipenv run conditional_install
```
