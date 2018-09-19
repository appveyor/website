---
layout: docs
title: Testing with Ruby
---

<!-- markdownlint-disable MD022 MD032 -->
# Testing with Ruby
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Installation

Several Ruby versions are already preinstalled:

* [Windows images](/docs/windows-images-software/#ruby)
* [Ubuntu images](/docs/linux-images-software/#ruby)

We only need to add one of it to a PATH.

```yaml
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
```

## Build matrix

Rubies can be used with build matrix:

```yaml
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
```

Add 64-bit Rubies as `200-x64`, `21-x64` etc. Do not use `Platform: x64`.

## Caching

Ruby's gems can be easily cached by AppVeyor,
but they are installed into long and complex path,
like `C:\Ruby21\lib\ruby\gems\2.1.0\gems`.

One should better force Bundler to put gems into well know path, eg:

```yaml
version: 1.0.{build}-{branch}

cache:
  - vendor/bundle

environment:
  matrix:
    - RUBY_VERSION: 22
    - RUBY_VERSION: 21
    - RUBY_VERSION: 200
    - RUBY_VERSION: 193

install:
  - set PATH=C:\Ruby%RUBY_VERSION%\bin;%PATH%
  - bundle config --local path vendor/bundle
  - bundle install

build: off

before_test:
  - ruby -v
  - gem -v
  - bundle -v

test_script:
  - bundle exec rake
```

## Build Worker API

AppVeyor offers [API](/docs/build-worker-api) for reporting during build process.

It was implemented in
[appveyor-worker](https://rubygems.org/gems/appveyor-worker) gem.

Just include it into your project's Gemfile.
Progress of your tests will be automagically displayed on project build page
in real time and stack traces (for failed tests)
will become available thru web-interface.

You can also send messages and set environment variables from your tests.

## Windows Ruby Build Types & Info

Ruby builds for Windows have been built using two different systems.  Ruby versions 2.3 and previous are built with RubyInstaller, Ruby versions 2.4 and later are built with RubyInstaller2.  Both RubyInstaller (RI) and RubyInstaller2 (RI2) are open source projects.

Both RI & RI2 builds will set paths for their respective compile tools by requiring devkit, hence, the common `-rdevkit` option.

Information about several Appveyor Ruby builds is shown at <https://ci.appveyor.com/project/MSP-Greg/appveyor-ruby>.  Note that most have bundler installed, rubygems upgraded, etc.

### Tools & Compiler (used to build extension gems, or gems with c source)

RI builds use what is referred to as 'DevKit', which is a proprietary package of MSYS tools and compilers.  Additional packages are often called knapsack packages, or have knapsack in their download path.

RI2 builds use the MSYS2 / MinGW system for tools and compilers.  These are used by several languages that support Windows, and hence, have reasonably good support.  Of note, some versions of Windows and Git for Windows have exe files that match files in the MSYS2 / MinGW system.  This may cause issues with testing in Appveyor, depending on `PATH`.

Both DevKit and the MSYS2 / MinGW system are pre-installed on Appveyor.

#### DevKit

When using RI builds, you may require additional knapsack packages for compiling your project.  64 bit packages are listed at <https://dl.bintray.com/oneclick/OpenKnapsack/x64/>, 32 bit at <https://dl.bintray.com/oneclick/OpenKnapsack/x86/>.  For an example of using one, see ruby/openssl [appveyor.yml](https://github.com/ruby/openssl/blob/master/appveyor.yml).

#### MSYS2 /MinGW

As with RI builds, you may need to install packages for compiling when using RI2 builds.  A list of packages installed on Appveyor can be found at <https://ci.appveyor.com/project/MSP-Greg/appveyor-ruby>.

## OpenSSL Verification

OpenSSL needs a cert file to verify a site's certificate.  First, Ruby looks to `ENV['SSL_CERT_FILE']`, and then looks to the `OpenSSL::X509::DEFAULT_CERT_FILE` constant.  If neither point to a valid cert file, no verification can be performed.

Ruby RI builds do not properly set the constant, and the ENV variable is not set in the default Appveyor environment.

Conversely, Ruby RI2 builds are packaged with a cert file, and the constant is properly set.

Hence, to allow for SSL verification using RI builds (2.3 and earlier), you can either:

* Use `set SSL_CERT_FILE=C:/ruby24-x64/ssl/cert.pem`

* Download <https://curl.haxx.se/ca/cacert.pem> and store it somewhere, then set `SSL_CERT_FILE` to its full path.

## Ruby appveyor.yml examples

Now that Ruby has many std-lib items separated as default gems, and many are tested in Appveyor, reviewing their `appveyor.yml` files may be helpful.  The `Ruby/spec` test suite also has an [appveyor.yml](https://github.com/ruby/spec/blob/master/appveyor.yml) file.  Most popular gems test with Appveyor.
