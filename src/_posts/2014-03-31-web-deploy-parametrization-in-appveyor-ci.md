---
layout: post
title: Build worker improvements
---

When deploying web application to different environments you don’t want to re-build application package every time with different configurations, but you want to deploy the same package (artifact) with some environment-specific settings configured during deployment. When using Web Deploy the problem can be easily solved by Web Deploy parametrization.
<h3>Usage scenarios</h3>
Most common use cases for Web Deploy parametrization is updating node/attribute value in XML files or replacing a token in text files, for example:
<ul>
    <li>appSettings in <code>web.config</code></li>
    <li>connection strings in <code>web.config</code></li>
    <li>WCF endpoints</li>
    <li>Paths to log files</li>
    <li>Database name in SQL install script</li>
</ul>
<h3>Parameters.xml</h3>
To enable Web Deploy parametrization add <code>parameters.xml</code> file in the root of your web application.

<img src="/assets/images/posts/web-deploy/vs-solution-explorer.png" alt="vs-solution-explorer">

<code>Parameters.xml</code> contains the list of parameters required (or supported) by your Web Deploy package. In the example below we introduce two parameters - one to update path to log file in <code>appSettings</code> section of <code>web.config</code> and another one to set database name in SQL script.

Parameter element describes the name, default value and the places where and how this parameter must be applied.

<code>Parameters.xml</code> for our example:

{% highlight xml %}
<?xml version="1.0" encoding="utf-8" ?>
<parameters>
  <parameter name="LogsPath" defaultValue="logs">
    <parameterEntry kind="XmlFile" scope="\\web.config$" match="/configuration/appSettings/add[@key='LogsPath']/@value" />
  </parameter>
  <parameter name="DatabaseName">
    <parameterEntry kind="TextFile" scope="\\Database\\install_db.sql$" match="@@database_name@@" />
  </parameter>
{% endhighlight %}

When Web Deploy package is built you can open it in the explorer and see <code>parameters.xml</code> in the root:

<img src="/assets/images/posts/web-deploy/webdeploy-package.png" alt="webdeploy-package">

Resulting <code>parameters.xml</code> combines your custom parameters and system ones such as <code>IIS Web Application Name</code>. You don’t have to set <code>IIS Web Application Name</code> parameter explicitly - AppVeyor does that for you.

Read more about defining parameters: <a href="http://technet.microsoft.com/en-us/library/dd569084(v=ws.10).aspx">http://technet.microsoft.com/en-us/library/dd569084(v=ws.10).aspx</a>
<h3>Setting parameters during deployment</h3>
Web Deploy provider in AppVeyor analyzes Web Deploy package and looks into <strong>environment variables</strong> to set parameter values with matching names.

When promoting specific build from Environment page you set variables on environment settings page:

<img src="/assets/images/posts/web-deploy/environment-variables.png" alt="environment-variables">

When deploying during the build session environment variables are used instead. You can set build environment variables on Environment tab of project settings, <code>appveyor.yml</code> or programmatically during the build.

<img src="/assets/images/posts/web-deploy/project-environment-variables.png" alt="project-environment-variables">

Variables defined during the build override those ones defined on Environment level.

Web Deploy parametrization is supported by <a href="http://www.appveyor.com/docs/deployment/agent">Deployment Agent</a> too when deploying from Web Deploy package.

Related articles:
<ul>
    <li><a href="http://www.appveyor.com/docs/deployment/web-deploy">Deploying using Web Deploy</a></li>
    <li><a href="http://www.appveyor.com/docs/deployment/agent">Deploying to remote servers with AppVeyor Deployment Agent</a></li>
</ul>
