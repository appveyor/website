---
layout: docs
title: Publishing SQL Server databases from SSDT packages
---

<!-- markdownlint-disable MD022 MD032 -->
# Publishing SQL Server databases from SSDT packages
{:.no_toc}

"SQL Database" (`SqlDatabase`) deployment provider allows incremental publishing of database changes from [SQL Server Data Tools](https://msdn.microsoft.com/en-us/library/hh272686(v=vs.103).aspx) (SSDT) package (`.dacpac`) to a local SQL Server instance, remote SQL Server or Azure SQL database.

In this guide:

* Comment to trigger ToC generation
{:toc}
<!-- markdownlint-enable MD022 MD032 -->


## Provider settings

* **Artifact** (`artifact`) - `.dacpac` artifact file name or deployment name or regexp matching one of these.
* **Target SQL Server connection string** (`connection_string`) - SQL connection string to the target database. **Must include database name**. For example `server=(local)\SQLEXPRESS;database=test101;Integrated security=SSPI;`
* **Register as a Data-tier Application** (`register_data_tier_application`) - default is `false`. You can read about Data-tier applications on [MSDN](https://msdn.microsoft.com/en-us/library/ee210546.aspx) and also there is a nice introductory article on [CodeProject](https://www.codeproject.com/Articles/573144/Versioning-SQL-Server-Databases-using-SSDT).
* **Block publish when database has drifted from registered version** (`block_when_drift_detected`) - default is `false`.

Deployment behaviour:

* **Deploy database properties** (`script_database_options`) - Default is `true`.
* **Always re-create database** (`create_new_database`) - `true` if database should be re-created every time you deploy. Default is `false`.
* **Block incremental deployment if data loss might occur** (`block_on_possible_data_loss`) - Prevent database changes if data loss might occur. Default is `true`.
* **Execute deployment script in single-user mode** (`deploy_database_in_single_user_mode`) - `true` to execute deployment script in single-user mode. Default is `false`.
* **Back up database before deployment** (`backup_database_before_changes`) - `true` to back up database before deployment. **Does not work when publishing to Azure SQL database**. Default is `false`.
* **DROP objects in target but not in project** (`drop_objects_not_in_source`) - `true` to remove objects in destination database that do not exist in the package. Default is `false`.
* **Do not DROP users** (`do_not_drop_users`). Set to `true` when **DROP objects in target but not in project** (`drop_objects_not_in_source`) is `true` to preserve database users. Default is `false`. **Important note**: to properly preserve users, the following settings also should be set to `true`:
    * **Ignore permissions** (`ignore_permissions`)
    * **Ignore user settings objects** (`ignore_user_settings_objects`)
    * **Ignore login SIDs** (`ignore_login_sids`)
    * **Ignore role membership** (`ignore_role_membership`)
* **Do not user ALTER ASSEMBLY statements to update CLR types** (`no_alter_statements_to_change_clr_types`) - Default is `false`.

Advanced deployment options:

* **Allow drop blocking assemblies** (`allow_drop_blocking_assemblies`) - This property is used by SqlClr deployment to cause any blocking assemblies to be dropped as part of the deployment plan. By default, any blocking/referencing assemblies will block an assembly update if the referencing assembly needs to be dropped. Default is `false`.
* **Allow incompatible platform** (`allow_incompatible_platform`) - Specifies whether to attempt the action despite incompatible SQL Server platforms. Default is `false`.
* **Comment out SetVar declarations** (`comment_out_set_var_declarations`) - Specifies whether the declaration of SETVAR variables should be commented out in the generated publish script. You might choose to do this if you plan to specify the values on the command line when you publish by using a tool such as SQLCMD.EXE. Default is `false`.
* **Compare using target collation** (`compare_using_target_collation`) - This setting dictates how the database's collation is handled during deployment; by default the target database's collation will be updated if it does not match the collation specified by the source. When this option is set, the target database's (or server’s) collation should be used. Default is `false`.
* **Disable and re-enable DDL triggers** (`disable_and_reenable_ddl_triggers`) - Specifies whether Data Definition Language (DDL) triggers are disabled at the beginning of the publish process and re-enabled at the end of the publish action. Default is `true`.
* **Do not alter Change Data Capture objects** (`do_not_alter_change_data_capture_objects`) - If enabled: true, Change Data Capture objects are not altered. Default is `true`.
* **Do not ALTER replicated objects** (`do_not_alter_replicated_objects`) - Specifies whether objects that are replicated are identified during verification. Default is `true`.
* **Drop constraints not in source** (`drop_constraints_not_in_source`) - Specifies whether constraints that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database. Default is `true`.
* **Drop DML triggers not in source** (`drop_dml_triggers_not_in_source`) - Specifies whether DML triggers that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database. Default is `true`.
* **Drop extended properties not in source** (`drop_extended_properties_not_in_source`) - Specifies whether extended properties that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database. Default is `true`.
* **Drop indexes not in source** (`drop_indexes_not_in_source`) - Specifies whether indexes that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish to a database. Default is `true`.
* **Drop permissions not in source** (`drop_permissions_not_in_source`) - Specifies whether permissions that do not exist in the database snapshot (.dacpac) file will be dropped from the target database when you publish updates to a database. Default is `false`.
* **Drop role members not defined in source** (`drop_role_members_not_in_source`) - Specifies whether role members that are not defined in the database snapshot (.dacpac) file will be dropped from the target database when you publish updates to a database. Default is `false`.
* **Generate smart defaults, when applicable** (`generate_smart_defaults`) - Automatically provides a default value when updating a table that contains data with a column that does not allow null values. Default is `false`.
* **Ignore ANSI Nulls** (`ignore_ansi_nulls`) - Specifies whether differences in the ANSI NULLS setting should be ignored or updated when you publish to a database. Default is `true`.
* **Ignore authorizer** (`ignore_authorizer`) - Specifies whether differences in the Authorizer should be ignored or updated when you publish to a database. Default is `false`.
* **Ignore column collation** (`ignore_column_collation`) - Specifies whether differences in the column collations should be ignored or updated when you publish to a database. Default is `false`.
* **Ignore comments** (`ignore_comments`) - Specifies whether differences in the comments should be ignored or updated when you publish to a database. Default is `false`.
* **Ignore cryptographic provider file path** (`ignore_cryptographic_provider_file_path`) - Specifies whether differences in the file path for the cryptographic provider should be ignored or updated when you publish to a database. Default is `true`.
* **Ignore DDL trigger order** (`ignore_ddl_trigger_order`) - Specifies whether differences in the order of Data Definition Language (DDL) triggers should be ignored or updated when you publish to a database or server. Default is `false`.
* **Ignore DDL trigger state** (`ignore_ddl_trigger_state`) - Specifies whether differences in the enabled or disabled state of Data Definition Language (DDL) triggers should be ignored or updated when you publish to a database. Default is `false`.
* **Ignore default schema** (`ignore_default_schema`) - Specifies whether differences in the default schema should be ignored or updated when you publish to a database. Default is `false`.
* **Ignore DML trigger order** (`ignore_dml_trigger_order`) - Specifies whether differences in the order of Data Manipulation Language (DML) triggers should be ignored or updated when you publish to a database. Default is `false`.
* **Ignore DML trigger state** (`ignore_dml_trigger_state`) - Specifies whether differences in the enabled or disabled state of DML triggers should be ignored or updated when you publish to a database. Default is `false`.
* **Ignore extended properties** (`ignore_extended_properties`) - Specifies whether differences in the extended properties should be ignored or updated when you publish to a database. Default is `false`.
* **Ignore file and log file path** (`ignore_file_and_log_file_path`) - Specifies whether differences in the paths for files and log files should be ignored or updated when you publish to a database. Default is `true`.
* **Ignore filegroup placement** (`ignore_filegroup_placement`) - Specifies whether differences in the placement of objects in FILEGROUPs should be ignored or updated when you publish to a database. Default is `true`.
* **Ignore file size** (`ignore_file_size`) - Specifies whether differences in the file sizes should be ignored or whether a warning should be issued when you publish to a database. Default is `true`.
* **Ignore fill factor** (`ignore_fill_factor`) - Specifies whether differences in the fill factor for index storage should be ignored or whether a warning should be issued when you publish to a database. Default is `true`.
* **Ignore full-text catalog file path** (`ignore_full_text_catalog_file_path`) - Specifies whether differences in the file path for the full-text catalog should be ignored or whether a warning should be issued when you publish to a database. Default is `true`.
* **Ignore identity seed** (`ignore_identity_seed`) - Specifies whether differences in the seed for an identity column should be ignored or updated when you publish updates to a database. Default is `false`.
* **Ignore increment** (`ignore_increment`) - Specifies whether differences in the increment for an identity column should be ignored or updated when you publish to a database. Default is `false`.
* **Ignore index options** (`ignore_index_options`) - Specifies whether differences in the index options should be ignored or updated when you publish to a database. Default is `false`.
* **Ignore index padding** (`ignore_index_padding`) - Specifies whether differences in the index padding should be ignored or updated when you publish to a database. Default is `true`.
* **Ignore keyword casing** (`ignore_keyword_casing`) - Specifies whether differences in the casing of keywords should be ignored or updated when you publish to a database. Default is `true`.
* **Ignore lock hints on indexes** (`ignore_lock_hints_on_indexes`) - Specifies whether differences in the lock hints on indexes should be ignored or updated when you publish to a database. Default is `false`.
* **Ignore login SIDs** (`ignore_login_sids`) - Specifies whether differences in the security identification number (SID) should be ignored or updated when you publish to a database. Default is `true`.
* **Ignore not for replication** (`ignore_not_for_replication`) - Specifies whether the not for replication settings should be ignored or updated when you publish to a database. Default is `false`.
* **Ignore object placement on partition schemes** (`ignore_object_placement_on_partition_scheme`) - Specifies whether an object's placement on a partition scheme should be ignored or updated when you publish to a database. Default is `true`.
* **Ignore partition schemes** (`ignore_partition_schemes`) - Specifies whether differences in partition schemes and functions should be ignored or updated when you publish to a database. Default is `false`.
* **Ignore permissions** (`ignore_permissions`) - Specifies whether differences in the permissions should be ignored or updated when you publish to a database. Default is `false`.
* **Ignore quoted identifiers** (`ignore_quoted_identifiers`) - Specifies whether differences in the quoted identifiers setting should be ignored or updated when you publish to a database. Default is `true`.
* **Ignore role membership** (`ignore_role_membership`) - Specifies whether differences in the role membership of logins should be ignored or updated when you publish to a database. Default is `false`.
* **Ignore route lifetime** (`ignore_route_lifetime`) - Specifies whether differences in the amount of time that SQL Server retains the route in the routing table should be ignored or updated when you publish to a database. Default is `true`.
* **Ignore semicolon between statements** (`ignore_semicolon_between_statements`) - Specifies whether differences in the semi-colons between T-SQL statements will be ignored or updated when you publish to a database. Default is `true`.
* **Ignore table options** (`ignore_table_options`) - Specifies whether differences in the table options will be ignored or updated when you publish to a database. Default is `false`.
* **Ignore user settings objects** (`ignore_user_settings_objects`) - Specifies whether differences in the user settings objects will be ignored or updated when you publish to a database. Default is `false`.
* **Ignore whitespace** (`ignore_whitespace`) - Specifies whether differences in white space will be ignored or updated when you publish to a database. Default is `true`.
* **Ignore with nocheck on check constraints** (`ignore_with_nocheck_on_check_constraints`) - Specifies whether differences in the value of the WITH NOCHECK clause for check constraints will be ignored or updated when you publish to a database. Default is `false`.
* **Ignore with nocheck on foreign keys** (`ignore_with_nocheck_on_foreign_keys`) - Specifies whether differences in the value of the WITH NOCHECK clause for foreign keys will be ignored or updated when you publish to a database. Default is `false`.
* **Include composite objects** (`include_composite_objects`) - Include all composite elements as part of a single publish operation. Default is `true`.
* **Include transactional scripts** (`include_transactional_scripts`) - Specifies whether transactional statements should be used where possible when you publish to a database. Default is `false`.
* **Populate files on FileGroups** (`populate_files_on_file_groups`) - Specifies whether a new file is also created when a new FileGroup is created in the target database. Default is `true`.
* **Script database collation** (`script_database_collation`) - Specifies whether differences in the database collation should be ignored or updated when you publish to a database. Default is `false`.
* **Script database compatibility** (`script_database_compatibility`) - Specifies whether differences in the database compatibility should be ignored or updated when you publish to a database. Default is `false`.
* **Script state checks** (`script_deploy_state_checks`) - Specifies whether statements are generated in the publish script to verify that the database name and server name match the names specified in the database project. Default is `false`.
* **Script file size** (`script_file_size`) - Controls whether size is specified when adding a file to a filegroup. Default is `false`.
* **Script validation for new constraints** (`script_new_constraint_validation`) - At the end of publish all of the constraints will be verified as one set, avoiding data errors caused by a check or foreign key constraint in the middle of publish. If set to enabled: false, your constraints will be published without checking the corresponding data. Default is `true`.
* **Script refresh module** (`script_refresh_module`) - Include refresh statements at the end of the publish script. Default is `true`.
* **Treat verification errors as warnings** (`treat_verification_errors_as_warnings`) - Specifies whether errors encountered during publish verification should be treated as warnings. The check is performed against the generated deployment plan before the plan is executed against your target database. Plan verification detects problems such as the loss of target-only objects (such as indexes) that must be dropped to make a change. Verification will also detect situations where dependencies (such as a table or view) exist because of a reference to a composite project, but do not exist in the target database. You might choose to do this to get a complete list of all issues, instead of having the publish action stop on the first error. Default is `false`.
* **Unmodifiable object warnings** (`unmodifiable_object_warnings`) - Specifies whether warnings should be generated when differences are found in objects that cannot be modified, for example, if the file size or file paths were different for a file. Default is `true`.
* **Verify collation compatibility** (`verify_collation_compatibility`) - Specifies whether collation compatibility is verified. Default is `true`.
* **Verify deployment** (`verify_deployment`) - Specifies whether checks should be performed before publishing that will stop the publish action if issues are present that might block successful publishing. For example, your publish action might stop if you have foreign keys on the target database that do not exist in the database project, and that will cause errors when you publish. Default is `true`.


### Configuring in appveyor.yml

At minimum you would specify just `artifact` and `connection_string` and rely on default values for database deployment settings described above. The following settings work for the most cases when deploying to SQL Server or Azure SQL:

```yaml
deploy:
  provider: SqlDatabase
  connection_string:
    secure: r5FHTTIfknKXrvMwsfqC/swG2n81GGc0PruruI5DVSMAocqz==
  script_database_options: false
  ignore_file_and_log_file_path: true
```


### SQLCMD variables

You can specify SQLCMD variables either on UI or in `appveyor.yml` by prefixing them with `sqlcmd.`, for example:

```yaml
deploy:
  provider: SqlDatabase
  connection_string:
    secure: r5FHTTIfknKXrvMwsfqC/swG2n81GGc0PruruI5DVSMAocqz==
  sqlcmd.VarName: Boo
  sqlcmd.AnotherVar: Baz
```


## Publishing to local SQL Server for integration testing

To perform integration testing of database changes you can publish SSDT package to a local SQL Server instance installed on build worker. [SQL Server 2008, SQL Server 2012 and SQL Server 2014 instances](/docs/services-databases/) are available on build worker for your tests.

By default, all SQL Server services are stopped. You can choose SQL Server service to start on "Environment" tab of AppVeyor project settings or in `appveyor.yml`. For example, to start SQL Server 2014 instance:

```yaml
services:
  - mssql2014
```

In connection string for local publishing you could either use standard SQL Server login/password or integrated security (`Integrated Security=SSPI`). Below is a complete example of `appveyor.yml` for building SSDT package, pushing it to build artifacts and then publishing to a local SQL Server:

```yaml
services:
  - mssql2014

artifacts:
  - path: MyDatabase\bin\debug\MyDatabase.dacpac
    name: MyDatabase

deploy:
  - provider: SqlDatabase
    artifact: MyDatabase
    connection_string: 'Server=(local)\SQL2014;Database=my_test_db;User ID=sa;Password=Password12!'
```


## Publishing to Azure SQL database

### Azure SQL Server firewall settings

To enable database publishing from AppVeyor environment to your Azure SQL database you should modify Azure SQL Server firewall settings and allow **Windows Azure Services** add the [following ranges of allowed IPs](/docs/build-environment/#ip-addresses).

![azure-sql-server-settings](/assets/img/docs/deployment/sql-database/azure-sql-server-settings.png)

### SSDT project settings

In Visual Studio open SSDT project properties and select **Microsoft Azure SQL Database** as a Target platform:

![ssdt-project-settings-for-azure](/assets/img/docs/deployment/sql-database/ssdt-project-settings-for-azure.png)


## Publishing to internal SQL Server with Deployment Agent

If target SQL Server instance is behind the firewall you can still publish database there by installing [AppVeyor Deployment Agent](/docs/deployment/agent/).

For instructions on setting up database publishing with Deployment Agent please see [Publishing SSDT package artifact to SQL Server section in Deployment Agent guide](/docs/deployment/agent#publishing-ssdt-package-artifact-to-sql-server).
