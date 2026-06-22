# The source code of <https://www.appveyor.com/>

[![Build status](https://ci.appveyor.com/api/projects/status/a8s3e1pd8070x2y9/branch/master?svg=true)](https://ci.appveyor.com/project/AppVeyor-Website/website)
[![dependencies Status](https://david-dm.org/appveyor/website/status.svg)](https://david-dm.org/appveyor/website)


## Getting started

* Install [Node.js](https://nodejs.org/download/)
* Install Ruby matching `.ruby-version`
* Install the Node.js dependencies via npm: `npm install`
* Install Ruby dependencies: `bundle install`
* Build the static site: `npm run build`

The generated site is written to `_site/`.

## Staging

The `staging` branch is published to <https://appveyor-staging.azurewebsites.net>.

### TODO:

* Fix HTML errors due to duplicate IDs in /updates/
