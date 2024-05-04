---
layout: docs
title: Testing with Node.js
---

<!-- markdownlint-disable MD022 MD032 -->
# Testing with Node.js
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Quick start

Put this simple `appveyor.yml` to the root of your repository and it should work for most Node.js projects out there:

```yaml
# Test against the latest version of this Node.js version
image:
  - Visual Studio 2022

environment:
  nodejs_version: "20"

# Install scripts. (runs after repo cloning)
install:
  # Get the latest stable version of Node.js
  - ps: Install-Product node $env:nodejs_version
  # install modules
  - npm install

# Post-install test scripts.
test_script:
  # Output useful info for debugging.
  - node --version
  - npm --version
  # run tests
  - npm test

# Don't actually build.
build: off
```


## Line endings

By default, Git on build workers is configured with `git config --global core.autocrlf input` which means your repo is cloned "as is" without fixing new lines on Windows.
If you have a file with a string `abc\n line2` it will be checked out exactly as `abc\n line2` and if there is `line1 \r\n line2` in the repo
you'll get the same on checkout. See this SO answer explaining in details [`core.autocrlf` modes](https://stackoverflow.com/questions/1249932/git-1-6-4-beta-on-windows-msysgit-unix-or-dos-line-termination/1250133#1250133).

However, if you expect Git to fix line endings on Windows and checkout *all* strings with `\r\n` you could tell Git doing that during `init`
phase of your build which occurs *before* cloning command:

```yaml
init:
  - git config --global core.autocrlf true
```

We do not recommend relying on `core.autocrlf` value set on AppVeyor build workers
and always explicitly change this setting during the build or
[have it configured per-repository](https://help.github.com/articles/dealing-with-line-endings/).


## Selecting Node.js version

Build workers have the most recent versions of Node.js pre-installed - both `x86` and `x64`.
If you do nothing your scripts will run under `Node.js 22.1.x (x86)` (at the time of writing).

To switch to a different version of Node.js use the following PowerShell command:

    Install-Product node <version> [x86|x64]

`<version>` can be specified as `MAJOR` to install *absolute latest* version of Node.js,
`MAJOR.MINOR` to install *the latest* Node.js build or `MAJOR.MINOR.BUILD` to install *specific* build.

For example to switch runtime to the **latest version of Node.js** use this command (at the time of writing this is the latest 22.x branch):

    Install-Product node ''

To switch Node.js to the latest available 20.x branch use:

    Install-Product node 20

To select specific x64 version of Node v21.7.3:

    Install-Product node 21.7.3 x64

In `appveyor.yml` you should prefix the command with `ps` to tell AppVeyor it's PowerShell:

```yaml
install:
  - ps: Install-Product node 22
```

### How that works

Pre-installed Node.js distributives are stored in `C:\avvm\node` folder. When you run `Install-Product node` command it's,
basically, moving `nodejs` folder from there to `Program Files`, so the switch is very quick.
To check what versions are available on build worker you could do `dir C:\avvm\node`.

The following paths are always added to `PATH` environment variable:

    C:\Program Files (x86)\nodejs
    C:\Program Files\nodejs
    C:\Users\appveyor\AppData\Roaming\npm

### Installing *any* version of Node.js

Sometimes pre-installed versions of Node.js are little behind, but what if you need to test under
bleeding edge. You could use another PowerShell cmdlet which does full re-install of Node.js to a
selected version.

    Update-NodeJsInstallation <version> [x86|x64]

Here `<version>` is **exact version** of Node.js.

For example, the following command will download and install Node.js v21.7.3:

    Update-NodeJsInstallation 21.7.3

There is a helper cmdlet checking the remote dist directory of Node.js to determine the absolute latest build available:

    Get-NodeJsLatestBuild <major>.<minor>

It could be used together with the previous cmdlet to always install the latest build, for example:

    Update-NodeJsInstallation (Get-NodeJsLatestBuild 1.0)



## Testing under multiple versions of Node.js

AppVeyor enables easy testing against multiple combinations of platforms, configurations, and environments with [build matrix](/docs/build-configuration#build-matrix).

To test under the latest version of Node.js you can specify in `appveyor.yml`:

```yaml
environment:
  matrix:
    - nodejs_version: "18"
    - nodejs_version: "20"
    - nodejs_version: "22"

install:
  - ps: Install-Product node $env:nodejs_version
```

To allow failing jobs for bleeding-edge Node.js versions:

```yaml
matrix:
  allow_failures:
    - nodejs_version: "22"
```

What if you need to test under a specific version of Node.js for both x86 and x64 platforms?
You could add another "dimension" to the matrix, for example:

```yaml
environment:
  matrix:
    - nodejs_version: "18"
    - nodejs_version: "20"

platform:
  - x86
  - x64

install:
  - ps: Install-Product node $env:nodejs_version $env:platform
```

This configuration will produce a build with 4 jobs for all combinations of node version and platform:

    Node.js 18.x   x86
    Node.js 18.x   x64
    Node.js 20.x   x86
    Node.js 20.x   x64


## Deploying a Node.js website to Azure

You can use Web Deploy to [deploy your Node.js website](/docs/deployment/web-deploy#using-web-deploy-with-a-nodejs-website) to Azure.

## Authenticating NPM for publishing packages

For publishing packages to NPM registry during the build `npm` client should be authenticated.
On your development machine, you run `npm adduser` command to set npm credentials.

On AppVeyor build worker you don't need to run `npm adduser`, but you can just move npm `authToken` from your local machine to a build VM. This is what you have to do:

1. Do `npm adduser` on your local dev machine.
2. Copy `authToken` value from `%userprofile%\.npmrc` on your local machine.
3. In AppVeyor, on Environment tab of project settings add a new environment variable with name `npm_auth_token` and the valued copied on step 2. Click "lock" icon to mark the variable as "secure" (so it won't be available for PR builds).
4. Add to your `appveyor.yml`:

```yaml
install:
- ps: '"//registry.npmjs.org/:_authToken=$env:npm_auth_token`n" | out-file "$env:userprofile\.npmrc" -Encoding ASCII'
- npm whoami
```

`npm whoami` should display your username.

## Known issues

### Wrong output encoding

When running `npm install` (or any other Node.js program writing to a console) you may notice that Unicode symbols written to build console look wrong:

![node-output-wrong](/assets/img/docs/lang/node-output-wrong.png)

This is because you are calling that program from PowerShell:

```yaml
- ps: npm install
```

To fix that run it in "shell" mode:

```yaml
- npm install
```

![node-output-correct](/assets/img/docs/lang/node-output-correct.png)

At the moment it seems to be an issue with PowerShell and the way it redirects output from console apps to a custom PowerShell host.


### Garbled or missing output

Sometimes you may notice that output of some Node.js programs (especially those ones actively writing to both StdOut and StdErr) is garbled or missing.


### Locking errors (EPERM, EEXIST, tgz.lock)

Sometimes you may encounter sporadic "lock" errors that might look like:

    npm ERR! Error: EPERM, open 'C:\Users\appveyor\AppData\Roaming\npm-cache{something}-package-tgz.lock'
    npm ERR! EEXIST, open '{some-path}\npm-cache{something}-package-tgz.lock'

More info about [npm troubleshooting on Windows](https://github.com/npm/npm/wiki/Troubleshooting#upgrading-on-windows).


### Unexpected error messages with a successful run

Sometimes `npm` commands/tasks will show errors in the build log, but the build will succeed as if there weren't any. This can happen with `npm ci` where all packages are resolved and installed, but an error pointing back to the build script may show.

This is because you're calling the script from PowerShell:

```yaml
- ps: ./build.ps1
```

To fix this run it in "shell" mode:

```yaml
- powershell ./build.ps1
- cmd: powershell ./build.ps1
```

**Note** that this can happen even when calling `npm` from other build tools such as [Cake](https://cakebuild.net) and the [Cake.Npm](https://github.com/cake-contrib/Cake.Npm) addin.
