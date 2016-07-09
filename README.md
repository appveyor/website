# www.appveyor.com website

## Getting started

[![Windows Build status](https://img.shields.io/appveyor/ci/FeodorFitsner/website/master.svg?label=Windows%20build)](https://ci.appveyor.com/project/FeodorFitsner/website/branch/master)
[![devDependency Status](https://img.shields.io/david/dev/FeodorFitsner/website.svg)](https://david-dm.org/FeodorFitsner/website#info=devDependencies)

* Install [Node.js](https://nodejs.org/download/)
* Install grunt: `npm install -g grunt-cli`
* Install the Node.js dependencies via npm: `npm install`
* Install Ruby; See [this](http://jekyll-windows.juthilo.com/) guide
* Run `gem install bundle` and then `bundle install`
* Run `grunt build` to build the static site, `grunt` to build and watch for changes (http://localhost:4000/)

## Staging

"Staging" branch is published to this location: https://appveyor-staging.azurewebsites.net
