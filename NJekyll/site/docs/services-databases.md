---
layout: docs
title: Services and databases
---

# Services and databases

AppVeyor is an ideal platform for integration testing:

- Your build script has admin access to the build machine - you are free to do whatever you like to support your testing process: install new software, run services, even format disk drives :)
- Build machines are transient, which means the state between builds is not preserved and the next build is started on a fresh build machine - basically, you don't need any clean-up logic in your build scripts. 

AppVeyor has most popular services and database engines pre-installed on all build machines:

* [SQL Server 2008](#sql2008)
* [SQL Server 2012](#sql2012)
* [SQL Server 2014](#sql2014)
* [MySQL](#mysql)
* [PostgreSQL](#postgresql) 
* [Internet Information Services](#iis)
* [Microsoft Message Queuing](#msmq)

By default, their corresponding Windows services are stopped to reduce build machine boot time. On the **Environment** tab of project settings or in `appveyor.yml` you can configure which services must be started after the build machine has booted.

<a id="sql2008"></a>
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


<a id="sql2012"></a>
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


<a id="sql2014"></a>
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


<a id="mysql"></a>
## MySQL

MySQL 5.6 x64 database service is available on AppVeyor build workers.

* Server name: `127.0.0.1` or `localhost`
* Server port: `3306`
* `root` password: `Password12!`

To start MySQL in `appveyor.yml`:

    services:
      - mysql


<a id="postgresql"></a>
## PostgreSQL

PostgreSQL 9.3 x64 database service is available on AppVeyor build workers.

* Server name: `127.0.0.1` or `localhost`
* Server port: `5432`
* `postgres` account password: `Password12!`

To start PostgreSQL in `appveyor.yml`:

    services:
      - postgresql


<a id="iis"></a>
## Internet Information Services

Internet Information Services ("Web Server" role) are installed on build machines. The version of IIS depends on the operating system:

* Windows Server 2008 R2 - IIS 7.5
* Windows Server 2012 - IIS 8
* Windows Server 2012 R2 - IIS 8.5

To configure IIS in `appveyor.yml`:

    services:
      - iis


<a id="msmq"></a>
## Microsoft Message Queuing

Microsoft Message Queuing services are installed on build machines.

To configure MSMQ in `appveyor.yml`:

    services:
      - msmq



