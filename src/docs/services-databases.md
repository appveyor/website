---
layout: docs
title: Services and databases
---

# Services and databases

AppVeyor is an ideal platform for integration testing:

* Your build script has admin access to the build machine - you are free to do whatever you like to support your testing process: install new software, run services, even format disk drives :)
* Build machines are transient, which means the state between builds is not preserved and the next build is started on a fresh build machine - basically, you don't need any clean-up logic in your build scripts.

AppVeyor has most popular services and database engines pre-installed on all build machines:

* [SQL Server 2008](#sql-server-2008)
* [SQL Server 2012](#sql-server-2012)
* [SQL Server 2014](#sql-server-2014)
* [SQL Server 2016](#sql-server-2016)
* [MySQL](#mysql)
* [PostgreSQL](#postgresql)
* [MongoDB](#mongodb)
* [Internet Information Services](#internet-information-services)
* [Microsoft Message Queuing](#microsoft-message-queuing)

By default, their corresponding Windows services are stopped to reduce build machine boot time. On the **Environment** tab of project settings or in `appveyor.yml` you can configure which services must be started after the build machine has booted.

## SQL Server 2008

The latest version of **SQL Server 2008 Express R2 SP2 with Advanced Services** is available on AppVeyor build servers. This is a full install with Database Engine, Replication, Full-Text Search, Reporting Services and Management Studio Express enabled.

Instance name: `SQL2008R2SP2`

`sa` password: `Password12!`

Sample connection string:

    Server=(local)\SQL2008R2SP2;Database=master;UID=sa;PWD=Password12!

To start SQL Server 2008 Express in `appveyor.yml`:

```yaml
services:
  - mssql2008r2sp2
```

To start SQL Server and Reporting Services:

```yaml
services:
  - mssql2008r2sp2rs
```


## SQL Server 2012

The latest version of **SQL Server 2012 Express SP1 with Advanced Services** is available on AppVeyor build servers. This is a full install with Database Engine, Replication, Full-Text Search, Reporting Services and Management Studio Express enabled.

Instance name: `SQL2012SP1`

`sa` password: `Password12!`

Sample connection string:

    Server=(local)\SQL2012SP1;Database=master;User ID=sa;Password=Password12!

To start SQL Server 2012 Express in `appveyor.yml`:

```yaml
services:
  - mssql2012sp1
```

To start SQL Server and Reporting Services:

```yaml
services:
  - mssql2012sp1rs
```


## SQL Server 2014

The latest version of **SQL Server 2014 Express with Advanced Services** is available on AppVeyor build servers. This is a full install with Database Engine, Replication, Full-Text Search, Reporting Services and Management Studio Express enabled.

Instance name: `SQL2014`

`sa` password: `Password12!`

Sample connection string:

    Server=(local)\SQL2014;Database=master;User ID=sa;Password=Password12!

To start SQL Server 2014 Express in `appveyor.yml`:

```yaml
services:
  - mssql2014
```

To start SQL Server and Reporting Services:

```yaml
services:
  - mssql2014rs
```

## SQL Server 2016

The latest version of **SQL Server 2016 Express** is available on AppVeyor build servers.

Instance name: `SQL2016`

`sa` password: `Password12!`

Sample connection string:

    Server=(local)\SQL2016;Database=master;User ID=sa;Password=Password12!

To start SQL Server 2016 Express in `appveyor.yml`:

```yaml
services:
  - mssql2016
```

### Importing your existing database

You can use the following PowerShell script to attach your own SQL Server database in `.MDF` format:

```powershell
$startPath = "$($env:appveyor_build_folder)\path-to-your-bin"
$sqlInstance = "(local)\SQL2012SP1"
$dbName = "MyDatabase"

# replace the db connection with the local instance
$config = join-path $startPath "MyTests.dll.config"
$doc = (gc $config) -as [xml]
$doc.SelectSingleNode('//connectionStrings/add[@name="store"]').connectionString = "Server=$sqlInstance; Database=$dbName; Trusted_connection=true"
$doc.Save($config)

# attach mdf to local instance
$mdfFile = join-path $startPath "store.mdf"
$ldfFile = join-path $startPath "store_log.ldf"
sqlcmd -S "$sqlInstance" -Q "Use [master]; CREATE DATABASE [$dbName] ON (FILENAME = '$mdfFile'),(FILENAME = '$ldfFile') for ATTACH"
```


### Enabling TCP/IP, Named Pipes and setting instance alias

```powershell
[reflection.assembly]::LoadWithPartialName("Microsoft.SqlServer.Smo") | Out-Null
[reflection.assembly]::LoadWithPartialName("Microsoft.SqlServer.SqlWmiManagement") | Out-Null

$serverName = $env:COMPUTERNAME
$instanceName = 'SQL2008R2SP2'
$smo = 'Microsoft.SqlServer.Management.Smo.'
$wmi = new-object ($smo + 'Wmi.ManagedComputer')

# Enable TCP/IP
$uri = "ManagedComputer[@Name='$serverName']/ServerInstance[@Name='$instanceName']/ServerProtocol[@Name='Tcp']"
$Tcp = $wmi.GetSmoObject($uri)
$Tcp.IsEnabled = $true
$TCP.alter()

# Enable named pipes
$uri = "ManagedComputer[@Name='$serverName']/ ServerInstance[@Name='$instanceName']/ServerProtocol[@Name='Np']"
$Np = $wmi.GetSmoObject($uri)
$Np.IsEnabled = $true
$Np.Alter()

# Set Alias
New-Item HKLM:\SOFTWARE\Microsoft\MSSQLServer\Client -Name ConnectTo | Out-Null
Set-ItemProperty -Path HKLM:\SOFTWARE\Microsoft\MSSQLServer\Client\ConnectTo -Name '(local)' -Value "DBMSSOCN,$serverName\$instanceName" | Out-Null

# Start services
Set-Service SQLBrowser -StartupType Manual
Start-Service SQLBrowser
Start-Service "MSSQL`$$instanceName"
```

<br/>

```bat
sqlcmd -S "(local)" -U "sa" -P "Password12!" -Q "SELECT * FROM information_schema.tables;" -d "master"
```


## MySQL

MySQL 5.7 x64 database service is available on AppVeyor build workers.

* Path: `C:\Program Files\MySql\MySQL Server 5.7`
* Server name: `127.0.0.1` or `localhost`
* Server port: `3306`
* `root` password: `Password12!`

To start MySQL in `appveyor.yml`:

```yaml
services:
  - mysql
```

This is an example how to supply MySql credentials to work with PowerShell tools:

```powershell
$env:MYSQL_PWD="Password12!"
$cmd = '"C:\Program Files\MySql\MySQL Server 5.7\bin\mysql" -e "create database YourDatabase;" --user=root'
iex "& $cmd"
```

## PostgreSQL

PostgreSQL 9.3 and 9.4 x64 database services are available on AppVeyor build workers.

* Path:
    * `C:\Program Files\PostgreSQL\9.3`
    * `C:\Program Files\PostgreSQL\9.4`
    * `C:\Program Files\PostgreSQL\9.5`
* Server name: `127.0.0.1` or `localhost`
* Server port: `5432`
* `postgres` account password: `Password12!`

To start PostgreSQL 9.4 in `appveyor.yml`:

```yaml
services:
  - postgresql  # or postgresql94
```

To start PostgreSQL 9.3 in `appveyor.yml`:

```yaml
services:
  - postgresql93
```

This is an example how to supply PG credentials to work with command-line tools:

```bat
SET PGUSER=postgres
SET PGPASSWORD=Password12!
PATH=C:\Program Files\PostgreSQL\9.3\bin\;%PATH%
createdb YourDatabase
```

## MongoDB

MongoDB 3.0.4 database service is pre-installed on build workers.

* Install directory: `C:\mongodb`
* Config: `C:\mongodb\mongod.cfg`
* Data path: `C:\mongodb\data\db`

To start MongoDB in `appveyor.yml`:

```yaml
services:
  - mongodb
```


## Internet Information Services

Internet Information Services ("Web Server" role) 8.5 are installed on build workers. The version of IIS depends on the operating system:

To configure IIS in `appveyor.yml`:

```yaml
services:
  - iis
```


## Microsoft Message Queuing

Microsoft Message Queuing services are installed on build machines.

To configure MSMQ in `appveyor.yml`:

```yaml
services:
  - msmq
```
