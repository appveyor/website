---
layout: docs
title: Testing with Ruby
---

# Testing with Ruby

<!--TOC-->

## Installation

Several Ruby verions is already preinstalled (see [details](/docs/installed-software#ruby)).
We only need to add one of it to a PATH.

    # appveyor.yml
    install:
      - set PATH=C:\Ruby22\bin;%PATH%
      - ruby -v
      - gem -v
      - bundle -v
      - bundle config --local path vendor/bundle
      - bundle

    build: off

    test_script:
      - bundle exec rake

## Build matrix
