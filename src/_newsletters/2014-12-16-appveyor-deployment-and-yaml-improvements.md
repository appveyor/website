---
layout: one-column
date: 2014-12-16
title: AppVeyor Deployment and YAML improvements
---

<div style="font-family:'Segoe UI',Arial,Sans-Serif;font-size:10pt;width:100%; max-width:1042px;margin: 0 auto;">

  <style>
    a {
    color: #0066CC;
    }
  </style>

  <p>
    We are continuously improving AppVeyor platform and doing a couple of changes/deployments during the week. To give you more information about ongoing/upcoming changes and status updates we introduced a new "Technical updates" mailing list. It will be sent approximately two-three times a week.
  </p>

  <p>
    All existing customers can <a href="https://ci.appveyor.com/profile">subscribe to this mailing list on Profile page</a>. If you decide not to subscribe to technical updates you'll still be receiving this monthly newsletter.
  </p>

  <p>
    Now, back to deployment improvements. Deployment has always been a strong part of AppVeyor and we are committed to make AppVeyor a single shop for your entire continuous delivery. Also, YAML configs worked amazingly well for AppVeyor customers and we continue to invest into this area with a new features based on your feedback.
  </p>

  <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">New SQL Database deployment provider</h2>

  <p>
    Your AppVeyor builds may produce <a href="https://msdn.microsoft.com/en-us/library/hh272686(v=vs.103).aspx">SSDT</a> packages (.dacpac files) describing application database changes. Publishing SSDT project from Visual Studio is a trivial task, but it's been always a challenge of doing that on a build server. Most common tools for synchronizing DACPAC packages were SqlPackage.exe and MSDeploy.exe with built-in DacFx provider.
  </p>

  <p>
    Now AppVeyor offers a new <a href="https://www.appveyor.com/docs/deployment/sql-database-ssdt">SQL Database deployment provider</a> for incremental publishing of SSDT packages to a local SQL Server instance, remote SQL Server or Azure SQL databases.
  </p>

  <p style="margin:2rem 0;">
    <img src="/assets/images/newsletters/2014-12-16/sql-database-provider-settings.png" style="width:800px;">
  </p>

  <p>
    SQL Database provider uses SQL Server Data-tier Application Framework (DacFx) and as most of AppVeyor deployment providers it can be used during the build for staging deployment as well as a new "environment" for production deployments. <a href="https://www.appveyor.com/docs/deployment/sql-database-ssdt">Read more</a>
  </p>



  <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">SFTP support</h2>
  <p>
    We added SFTP (SSH File Transfer Protocol) support into FTP deployment provider. Don't mess it with FTPS which is also supported - it's a completely different thing though it organically complements a new "unified" FTP provider. <a href="https://www.appveyor.com/docs/deployment/ftp">Read more</a>
  </p>



  <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">Install MSI packages with Deployment Agent</h2>
  <p>
    With improved AppVeyor Deployment Agent it's now possible to <a href="https://www.appveyor.com/docs/deployment/agent#installing-msi">install MSI packages</a> on staging and production environments behind the firewall. With MSI added you can use Agent to deploy various types of workloads: web applications, windows services, console apps, SQL Databases and MSI packages. <a href="https://www.appveyor.com/docs/deployment/agent">Read more</a>
  </p>


  <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">New GitHub Releases provider</h2>
  <p>
    This is definitely a great news for open-source projects hosted on GitHub and using AppVeyor for their CI! GitHub deployment provider allows to publish build artifacts as assets to your repository release. <a href="https://www.appveyor.com/docs/deployment/github">Read more</a>
  </p>


  <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">YAML configuration validation</h2>
  <p>
    We re-factored appveyor.yml configuration parser to make it work in "strict" mode, so you get immediate feedback if there is something wrong with project config and as a bonus there is a <a href="https://ci.appveyor.com/tools/validate-yaml">new page for validating appveyor.yml</a> instead of try-and-fail process:
  </p>

  <p style="margin:2rem 0;">
    <img src="/assets/images/newsletters/2014-12-16/validate-yaml.png" style="width: 400px;">
  </p>

  <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">Export project configuration in YAML</h2>
  <p>
    You can easily switch your projects to YAML and benefit from portable and versioned configuration. There is a new tab on AppVeyor project settings which allows you to see how project changes made through UI would look in appveyor.yml:
  </p>

  <p style="margin:2rem 0;">
    <img src="/assets/images/newsletters/2014-12-16/export-yaml.png" style="width: 600px;">
  </p>

  <h2 style="font-size:170%;font-weight:normal;color:#333;margin: 20px 0 5px 0;">New REST API for configuring project with YAML</h2>
  <p>
    It's been a challenge to configure project settings through REST API as their request/response JSON format was, well, derived from UI and not suitable for processing by humans. With all these parsing and exporting improvements in YAML config we also added two new API calls: <a href="https://www.appveyor.com/docs/api/projects-builds#get-project-settings-yaml">get project settings in YAML</a> and <a href="https://www.appveyor.com/docs/api/projects-builds#update-project-settings-yaml">update project setting in YAML</a>.
  </p>

  <p>
    <br/>
    Holidays are coming and we would like to wish all our customers more green builds and less bugs! Merry Christmas and Happy New Year!
  </p>

  <p>
    Feodor Fitsner, <br />
    AppVeyor founder and developer
  </p>

  <p>
    Follow us on Twitter: <a href="https://twitter.com/appveyor">@appveyor</a>
  </p>
</div>
