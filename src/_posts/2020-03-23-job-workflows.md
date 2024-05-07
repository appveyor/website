---
title: 'Introducing job workflows - sequential flows, fan-in, fan-out and cancellable jobs'
---

AppVeyor can run ["matrix" builds](/docs/build-configuration/#build-matrix) where you specify "dimensions" like `image`, `platform`, `configuration`, etc. and AppVeyor creates a build with multiple jobs testing all combinations of configured dimensions. All matrix build jobs are running in parallel (provided your account allows multiple concurrent jobs).

However, there could be more advanced CI/CD workflow requirements that cannot be implemented in a single build job or parallel matrix, for example:

* Building the project/tests once and then run multiple tests in parallel for the same build output;
* Deploy once all tests running in parallel are finished;
* Deploy to staging, run tests on staging, deploy to production, run tests on production.

AppVeyor allows to configure explicit jobs and their groups with dependencies between them which enables developer to run build jobs in **sequence**, implement **fan-out/fan-in** pipelines and spawn independent **cancellable** jobs.

<p class="text-center">
    <img src="/assets/img/docs/workflows/example-workflow.png" alt="Example workflow" width="922" height="252">
</p>

## Configuring job workflows

A general template for defining job workflows in `appveyor.yml`:

```yaml
# Job definitions, sections in [] are optional
environment:
  matrix:

  - job_name: <job name 1>
  [- job_group: <group name>]
  [- jobs_per_group: N]
  [- job_depends_on: <job or group name>]
  [- job_allow_cancellation: true]

  - job_name: <job name 2>
  [- job_group: <group name>]
  [- jobs_per_group: N]
  [- job_depends_on: <job or group name>]
  [- job_allow_cancellation: true]

  ...

# the first failed job cancels other jobs and fails entire build
matrix:
  fast_finish: true

# configuration common for all jobs
init:
  - appveyor version
  - echo Some common logic here...

# job-specific configurations
for:

  -
    matrix:
      only:
        - job_name: <job name 1>
        [- job_group: <job name 1>] # optionally, you can define the configuration for the entire group

    build_script:
    - echo Job 1

  -
    matrix:
      only:
        - job_name: <job name 2>

    build_script:
    - echo Job 2

  ...
```

The following example creates a build with 2 jobs building solution in parallel on Windows and Linux images:

```yaml
environment:
  matrix:

  - job_name: Windows build
    appveyor_build_worker_image: Visual Studio 2019

  - job_name: Linux build
    appveyor_build_worker_image: Ubuntu

matrix:
  fast_finish: true

init:
  - appveyor version

# job-specific configurations
for:

  -
    matrix:
      only:
        - job_name: Windows build

    build_script:
    - cmd: echo This is Windows-specific build script

  -
    matrix:
      only:
        - job_name: Linux build

    build_script:
    - sh: echo This is Linux-specific build script
```

## Sequential flow

Build can be configured to run jobs one-by-one, for example build solution, deploy to staging, run tests on staging, deploy to production, run tests on production.

<p class="text-center">
    <img src="/assets/img/docs/workflows/sequential-flow.png" alt="Sequential flow" width="162" height="272">
</p>

We use `job_depends_on` to implement sequential flow:

```yaml
environment:
  matrix:

  - job_name: Job A

  - job_name: Job B
    job_depends_on: Job A

  - job_name: Job C
    job_depends_on: Job B

...
```

## Fan-in, fan-out

Fan-out flow is when multiple jobs are spawned in parallel once a single job or a group is completed. Fan-in flow is when a single job or a group waits for a multiple jobs to complete:

<p class="text-center">
    <img src="/assets/img/docs/workflows/fan-in-fan-out-groups.png" alt="Fan-in, fan-out flows" width="462" height="322">
</p>

The is `appveyor.yml` for the scenario presented on the figure above:

```yaml
environment:
  matrix:

  - job_name: Build

  - job_name: Tests A
    job_group: Tests
    job_depends_on: Build

  - job_name: Tests B
    job_group: Tests
    job_depends_on: Build

  - job_name: Tests C
    job_group: Tests
    job_depends_on: Build

  - job_name: Deploy
    job_depends_on: Tests

...
```

## Cancellable jobs

You can have jobs which are automatically cancelled when the entire build is complete (succeeded or failed - doesn't matter). These jobs could be some sort of "monitors" or services/dependencies (such as container with Redis or database) in Docker builds.

<p class="text-center">
    <img src="/assets/img/docs/workflows/cancellable-jobs.png" alt="Cancellable jobs" width="322" height="192">
</p>

An example of cancellable job doing some work in a loop while the build is working:

```yaml
environment:
  matrix:

  - job_name: Main work

  - job_name: Monitor
    job_allow_cancellation: true

for:

  -
    matrix:
      only:
        - job_name: Main work

    build_script:
    - cmd: echo This is the main job

  -
    matrix:
      only:
        - job_name: Monitor

    build_script:
    - ps: while($true) { Write-Host 'Do some checks'; Start-Sleep -s 1; }
```