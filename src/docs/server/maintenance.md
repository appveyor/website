---
layout: docs
title: AppVeyor Server Maintenance Guide
---

<!-- markdownlint-disable MD022 MD032 -->
# AppVeyor Server Maintenance Guide
{:.no_toc}

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->

## Upgrading AppVeyor Server

### Windows

To check current installer version of AppVeyor, navigate to `http://<appveyor-url>/system/settings/license` page. Alternatively, installation version can be seen on Windows "Add or remove programs" screen.

To upgrade AppVeyor download the latest [AppVeyor Server installer]({{ site.url }}/downloads/appveyor/appveyor-server.msi) (`appveyor-server.msi`). Right-click it in Windows Explorer and check the version on dialog "Details" tab. Run the installer to upgrade AppVeyor. Once the service is started and the web interface is available, navigate to "License" settings page to verify the version.

### Ubuntu/Debian

Download the latest [AppVeyor Server Debian package]({{ site.url }}/downloads/appveyor/appveyor-server.deb) using the following command:

    curl -L {{ site.url }}/downloads/appveyor/appveyor-server.deb -o appveyor-server_{{ site.data.versions.appveyor_version }}_amd64.deb

Upgrade AppVeyor installation by running:

    sudo dpkg -i appveyor-server_{{ site.data.versions.appveyor_version }}_amd64.deb

Verify the version by running:

    /opt/appveyor/server/appveyor version

### macOS

Stop AppVeyor Server service:

    brew services stop appveyor-server

Upgrade AppVeyor Server package and start the service:

    brew upgrade appveyor-server
    brew services start appveyor-server

## Backup/restore AppVeyor Server

### Windows

Default AppVeyor Server installation keeps all application data (database, artifacts and certificates) in `%PROGRAMDATA%\AppVeyor\Server` directory and all application settings under `HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Server` registry key.

To backup an AppVeyor installation, export `HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Server` registry key and then copy both the exported `.reg` file and the entire data directory to a safe location.

To restore AppVeyor, re-install it with the latest `.msi`, import backup `.reg` file by overwriting existing values and restore contents of data directory.

### Linux

On Linux all AppVeyor data (SQLite database, SSL certificate and artifacts) is stored in `/var/opt/appveyor/server` directory ("data") - back it up entirely.

AppVeyor settings with encryption keys are stored in `/etc/opt/appveyor/server` directory ("settings") - back it up as well. Remember, without correct encryption keys the database will be unusable and you won't be able to login to AppVeyor!

To restore AppVeyor Server just re-install AppVeyor from scratch and then overwrite "data" and "settings" directories from backup.

## Migrating database engine

By default, AppVeyor Server stores data in SQLite database. Though SQLite database is going to be OK for the most installations there might be a point when your database outgrows SQLite and you need to migrate to a full-featured database engine such as SQL Server or PostgreSQL.

AppVeyor Server installation includes database migration tool which can be found at:

* Windows: `C:\Program Files\AppVeyor\Server\appveyor-db-migrator.exe`
* Linux: `/opt/appveyor/server/appveyor-db-migrator`

AppVeyor database migrator allows moving database data from/to the following database engines:

* SQLite
* SQL Server
* PostgreSQL

AppVeyor database migrator runs with the following command:

    appveyor-db-migrator.exe|appveyor-db-migrator --src-provider=<provider> --src-connection-string=<provider-connection-string> \
        --dest-provider=<provider> --dest-connection-string=<provider-connection-string>

where `<provider>` must be one of the following: `SQLite`, `SqlServer` or `PostgreSQL`.

Depending on database provider connection string must have the following format:

* SQLite: `Data source=<path-to-file.db>`
* SQL Server: `Server=<ip-or-host>;Database=<database>;User ID=<user>;Password=<password>`
* PostgreSQL: `Host=<host-or-ip>;Port=5432;Database=<database>;Username=<user>;Password=<password>`

Destination database must be pre-created and clean before running the migrator.

For PostgreSQL database `citext` extension must be installed manually with the following command:

    CREATE EXTENSION IF NOT EXISTS citext;

## Troubleshooting

### Windows

#### Installation location

AppVeyor Server is a 64-bit application and can be run on a 64-bit OS only.

AppVeyor Server is installed into `%PROGRAMFILES%\AppVeyor\Server` directory.

The new `Appveyor.Server` Windows service is created pointing to `appveyor-server.exe` executable. The service runs under `appveyor` user account which is created by the installer and added to local `Administrators` group. The name of the service account can be customized with `APPVEYOR_USER_NAME` and `APPVEYOR_USER_PASSWORD` installer variables.

#### Settings

AppVeyor settings are stored under `HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Server` registry key. The values under that key are preserved during AppVeyor Server updates.

#### Data

AppVeyor database, SSL certificate (if configured), build artifacts and other data are stored in `%PROGRAMDATA%\AppVeyor\Server` directory. The data directory stays intact on AppVeyor updates and after uninstall. Data directory path can be customized with `APPVEYOR_DATA_DIR` installer variable.

By default, AppVeyor stores data in SQLite database `%PROGRAMDATA%\AppVeyor\Server\appveyor-server.db`. All sensitive data in the database is encrypted with "master key". Master key and its salt are stored in `HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Server\Security.MasterKey` and `HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Server\Security.MasterKeySalt` registry values respectively. These values are automatically generated on the first AppVeyor install and preserved during updates, however they will be deleted on AppVeyor uninstall. Both keys can be customized during installation by `APPVEYOR_MASTER_KEY` and `APPVEYOR_MASTER_KEY_SALT` installer variables.

> NOTE: If master key and its salt are lost, AppVeyor database will be unusable!

#### SQL Server database

AppVeyor Server can be configured to store its data in a SQL Server database. During the installation, specify the following installer variables:

* `APPVEYOR_DATABASE_PROVIDER=SQLServer`
* `APPVEYOR_SQLSERVER_CONNECTION_STRING=Server=<ip-or-host>;Database=<database>;User ID=<user>;Password=<password>`

After installation, provider and connection string can be changed in the registry values `Database.Provider` and `Database.SQLiteConnectionString` respectively.

#### PostgreSQL database

AppVeyor Server can be configured to store its data in PostgreSQL database. During the installation specify the following installer variables:

* `APPVEYOR_DATABASE_PROVIDER=PostgreSQL`
* `APPVEYOR_POSTGRESQL_CONNECTION_STRING=Host=<host-or-ip>;Port=5432;Database=<database>;Username=<user>;Password=<password>`

After creating Postgres database and before running the installer you have to install `citext` extension:

    CREATE EXTENSION IF NOT EXISTS citext;

After installation, provider and connection string can be changed in the registry values `Database.Provider` and `Database.PostgreSqlConnectionString` respectively.

#### Logs

AppVeyor Server logs are written to Windows Event log. They can be viewed in Event Viewer under **Applications and Services Logs -> AppVeyor**. It's recommended to increase the size of AppVeyor event log (for example, to 10Mb) to keep more logging data.

#### Web bindings

By default, AppVeyor web app tries to bind to `80` and `443` ports for `http` and `https` protocols respectively. If one of these ports is taken by another application, AppVeyor will bind to `8050` and `8051` ports. Binding is created on all (`*`) interfaces, so AppVeyor web can be accessed with both loopback (e.g. `localhost`) and external (if configured) addresses. AppVeyor web interface ports can be customized with `APPVEYOR_HTTP_PORT` and `APPVEYOR_HTTPS_PORT` installer variables. After installation, ports can be changed in the registry under `HKEY_LOCAL_MACHINE\SOFTWARE\Appveyor\Server` key.

A new Windows advanced firewall rule - `AppVeyor Server` - is configured by the AppVeyor installer. which allows access to `%PROGRAMFILES%\AppVeyor\Server\appveyor-server.exe` application.

#### Custom installation

To run AppVeyor Server installer with custom parameters and create an installation log use the following command:

    msiexec /i appveyor-server.msi /L*V appveyor-install.log PARAM_1=VAL_1 PARAM_2=VAL_2 ...

For example, to install AppVeyor with SQL Server database:

    msiexec /i appveyor-server.msi /L*V appveyor-install.log APPVEYOR_DATABASE_PROVIDER=SQLServer APPVEYOR_SQLSERVER_CONNECTION_STRING=<connection-string>

Full list of installer variables and their default values:

* `APPVEYOR_USER_NAME`: `appveyor`
* `APPVEYOR_USER_PASSWORD`: `<random>`
* `APPVEYOR_DATA_DIR`: `%ProgramData%\AppVeyor\Server`
* `APPVEYOR_DATABASE_PROVIDER`: `SQLite`
* `APPVEYOR_SQLITE_CONNECTION_STRING`: `Data Source=[DataDir]\appveyor-server.db`
* `APPVEYOR_POSTGRESQL_CONNECTION_STRING`: `<empty>`
* `APPVEYOR_SQLSERVER_CONNECTION_STRING`: `<empty>`
* `APPVEYOR_MASTER_KEY`: `<random>`
* `APPVEYOR_MASTER_KEY_SALT`: `<random>`
* `APPVEYOR_HTTP_PORT`: `80`
* `APPVEYOR_HTTPS_PORT`: `443`

### Linux

#### Installation location

AppVeyor Server is installed into `/opt/appveyor/server` directory.

The new `appveyor-server` systemd service is created pointing to `/opt/appveyor/server/appveyor-server` executable. The service runs under `appveyor` user account which is created by the installer and added to sudoers.

#### Settings

AppVeyor settings are stored in `/etc/opt/appveyor/server` directory. Settings are preserved during AppVeyor Server updates.

#### Data

AppVeyor database, SSL certificate (if configured), build artifacts and other data are stored in `/var/opt/appveyor/server` directory. The data directory stays intact on AppVeyor updates and after uninstall.

By default, AppVeyor stores data in SQLite database `/var/opt/appveyor/server/appveyor-server.db`. All sensitive data in the database is encrypted with "master key". Master key and its salt are stored in `/etc/opt/appveyor/server/appsettings.json`. These values are automatically generated on the first AppVeyor install and preserved during updates.

> NOTE: If the master key and its salt are lost, AppVeyor database will be unusable!

#### Logs

AppVeyor service logs are stored in journal and can be viewed with the following command:

    journalctl -u appveyor-server


### Docker builds

#### Known issues on Windows

* `nanoserver`-based build images do not contain Mercurial (`hg.exe`) and Subversion (`svn.exe`). For Bitbucket Mercurial repositories you can set `shallow_clone: true`;
* `ssh` utility is not working on `nanoserver`-based build image preventing cloning of private repositories via SSH - only cloning via HTTPS is working;
* Network "blips" with `net::ERR_NETWORK_CHANGED` error in Chrome. Disable Proxy "Automatic detect settings" - ADD SCREENSHOT.
* Windows Server 2016 is currently unable to run Docker builds.

* [Windows 10 release history](https://docs.microsoft.com/en-us/windows/windows-10/release-information)
* [Windows Container Version Compatibility](https://docs.microsoft.com/en-us/virtualization/windowscontainers/deploy-containers/version-compatibility)

#### Known issues on Linux

* If AppVeyor was installed *before* Docker `appveyor` user should be added to `docker` group:

```bash
sudo usermod -aG docker appveyor
```

Restart AppVeyor to take effect:

```bash
systemctl restart appveyor-server
```

#### Running Docker commands (Linux only)

If you need to run `docker` commands inside Docker builds you have to map Docker daemon socket and `docker` CLI executable into the build container.

You should modify Docker cloud settings under **Account &rarr; Build** environment and put the following into Custom Docker command arguments:

    -v /var/run/docker.sock:/var/run/docker.sock -v /usr/bin/docker:/usr/bin/docker