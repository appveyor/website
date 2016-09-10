# The source code of <https://www.appveyor.com/>

[![Build status](https://ci.appveyor.com/api/projects/status/a8s3e1pd8070x2y9/branch/master?svg=true)](https://ci.appveyor.com/project/AppVeyor-Website/website)
[![dependencies Status](https://david-dm.org/appveyor/website/status.svg)](https://david-dm.org/appveyor/website)

## Getting started

* Install [Node.js](https://nodejs.org/download/)
* Install grunt: `npm install -g grunt-cli`
* Install the Node.js dependencies via npm: `npm install`
* Install Ruby and Ruby DevKit; make sure you select add Ruby to `PATH`, and then run:

    ```shell
    cd C:\RubyDevKit
    ruby dk.rb init
    ruby dk.rb install
    ```

* Run `gem install bundle` and then `bundle install`
* Run `grunt build` to build the static site, `grunt` to build and watch for changes (`http://localhost:4000/`)

## Staging

The `staging` branch is published to <https://appveyor-staging.azurewebsites.net>.

### TODO:

* Fix HTML errors due to duplicate IDs in /updates/
