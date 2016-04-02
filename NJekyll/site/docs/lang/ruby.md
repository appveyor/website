---
layout: docs
title: Testing with Ruby
---

# Testing with Ruby

<!--TOC-->

## Installation

Several Ruby verions are already preinstalled (see [details](/docs/installed-software#ruby)).
We only need to add one of it to a PATH.

    # appveyor.yml
    install:
      - set PATH=C:\Ruby22\bin;%PATH%
      - bundle install

    build: off

    before_test:
      - ruby -v
      - gem -v
      - bundle -v

    test_script:
      - bundle exec rake

## Build matrix

Rubies can be used with build matrix:

    version: 1.0.{build}-{branch}

    environment:
      matrix:
        - RUBY_VERSION: 22
        - RUBY_VERSION: 21
        - RUBY_VERSION: 200
        - RUBY_VERSION: 193

    install:
      - set PATH=C:\Ruby%RUBY_VERSION%\bin;%PATH%
      - bundle install

    build: off

    before_test:
      - ruby -v
      - gem -v
      - bundle -v

    test_script:
      - bundle exec rake

Add 64-bit Rubies as `200-x64`, `21-x64` etc. Do not use `Platform: x64`.
