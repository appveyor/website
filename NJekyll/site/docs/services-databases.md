---
layout: docs
title: Services and databases
---

# Services and databases

AppVeyor is an ideal platform for integration testing:

- Your build script has admin access to the build machine - you are free to do whatever you like to support your testing process: install new software, run services, even format disk drives :)
- Build machines are transient, which means the state between builds is not preserved and the next build is started on a fresh build machine - basically, you don't need any clean-up logic in your build scripts.

AppVeyor has most popular services and database engines pre-installed on all build machines:

* [SQL Server 2008](#sql-server-2008)
* [SQL Server 2012](#sql-server-2012)
* [SQL Server 2014](#sql-server-2014)
* [MySQL](#mysql)
* [PostgreSQL](#postgresql)
* [Internet Information Services](#internet-information-services)
* [Microsoft Message Queuing](#microsoft-message-queuing)

By default, their corresponding Windows services are stopped to reduce build machine boot time. On the **Environment** tab of project settings or in `appveyor.yml` you can configure which services must be started after the build machine has booted.

## SQL Server 2008

The latest version of **SQL Server 2008 Express R2 SP2 with Advanced Services** is available on AppVeyor build servers. This is a full install with Database Engine, Replication, Full-Text Search, Reporting Services and Management Studio Express enabled.

Instance name: `SQL2008R2SP2`

`sa` password: `Password12!`

Sample connection string:

    Server=(local)\SQL2008R2SP2;Database=master;User ID=sa;Password=Password12!

To start SQL Server 2008 Express in `appveyor.yml`:

    services:
      - mssql2008r2sp2

To start SQL Server and Reporting Services:

    services:
      - mssql2008r2sp2rs


## SQL Server 2012

The latest version of **SQL Server 2012 Express SP1 with Advanced Services** is available on AppVeyor build servers. This is a full install with Database Engine, Replication, Full-Text Search, Reporting Services and Management Studio Express enabled.

Instance name: `SQL2012SP1`

`sa` password: `Password12!`

Sample connection string:

    Server=(local)\SQL2012SP1;Database=master;User ID=sa;Password=Password12!

To start SQL Server 2012 Express in `appveyor.yml`:

    services:
      - mssql2012sp1

To start SQL Server and Reporting Services:

    services:
      - mssql2012sp1rs


## SQL Server 2014

The latest version of **SQL Server 2014 Express with Advanced Services** is available on AppVeyor build servers. This is a full install with Database Engine, Replication, Full-Text Search, Reporting Services and Management Studio Express enabled.

Instance name: `SQL2014`

`sa` password: `Password12!`

Sample connection string:

    Server=(local)\SQL2014;Database=master;User ID=sa;Password=Password12!

To start SQL Server 2014 Express in `appveyor.yml`:

    services:
      - mssql2014

To start SQL Server and Reporting Services:

    services:
      - mssql2014rs

### Importing your existing database

You can use the following PowerShell script to attach your own SQL Server database in `.MDF` format:

<script src="https://gist.github.com/FeodorFitsner/6a734f5ee48de949df02.js"></script>

### Enabling TCP/IP, Named Pipes and setting instance alias

<script src="https://gist.github.com/FeodorFitsner/d971c5a98782d211640d.js"></script>

## MySQL

MySQL 5.6 x64 database service is available on AppVeyor build workers.

* Path: `C:\Program Files\MySql\MySQL Server 5.6`
* Server name: `127.0.0.1` or `localhost`
* Server port: `3306`
* `root` password: `Password12!`

To start MySQL in `appveyor.yml`:

    services:
      - mysql


## PostgreSQL

PostgreSQL 9.3 x64 database service is available on AppVeyor build workers.

* Path: `C:\Program Files\PostgreSQL\9.3`
* Server name: `127.0.0.1` or `localhost`
* Server port: `5432`
* `postgres` account password: `Password12!`

To start PostgreSQL in `appveyor.yml`:

    services:
      - postgresql

This is an example how to supply PG credentials to work with command-line tools:

    SET PGUSER=postgres
    SET PGPASSWORD=Password12!
    PATH=C:\Program Files\PostgreSQL\9.3\bin\;%PATH%
    createdb YourDatabase

## Internet Information Services

Internet Information Services ("Web Server" role) 8.5 are installed on build workers. The version of IIS depends on the operating system:

To configure IIS in `appveyor.yml`:

    services:
      - iis


## Microsoft Message Queuing

Microsoft Message Queuing services are installed on build machines.

To configure MSMQ in `appveyor.yml`:

    services:
      - msmq



