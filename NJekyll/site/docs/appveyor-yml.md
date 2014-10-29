---
layout: docs
title: appveyor.yml Reference
---

# appveyor.yml reference

<!-- http://markup.su/highlighter/ style: Dawn -->

<pre style="color:#080808"><span style="color:#5a525f;font-style:italic"># Notes:</span>
<span style="color:#5a525f;font-style:italic">#   - Minimal appveyor.yml file is an empty file. All sections are optional.</span>
<span style="color:#5a525f;font-style:italic">#   - Indent each level of configuration with 2 spaces. Do not use tabs!</span>
<span style="color:#5a525f;font-style:italic">#   - All section names are case-sensitive.</span>
<span style="color:#5a525f;font-style:italic">#   - Section names should be unique on each level.</span>

<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>
<span style="color:#5a525f;font-style:italic">#      general configuration      #</span>
<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>

<span style="color:#5a525f;font-style:italic"># version format</span>
<span style="color:#0b6125"><span style="color:#bf4f24">version<span style="color:#794938">:</span></span> <span style="color:#0b6125">1.0.</span></span>{build}

<span style="color:#5a525f;font-style:italic"># branches to build</span>
<span style="color:#bf4f24">branches</span>:
  <span style="color:#5a525f;font-style:italic"># whitelist</span>
  <span style="color:#bf4f24">only</span>:
    <span style="color:#0b6125">- <span style="color:#0b6125">master</span></span>
    <span style="color:#0b6125">- <span style="color:#0b6125">production</span></span>

  <span style="color:#5a525f;font-style:italic"># blacklist</span>
  <span style="color:#bf4f24">except</span>:
    <span style="color:#0b6125">- <span style="color:#0b6125">gh-pages</span></span>

<span style="color:#5a525f;font-style:italic"># Do not build on tags (GitHub only)</span>
<span style="color:#bf4f24">skip_tags</span>: <span style="color:#0b6125">true</span>

<span style="color:#5a525f;font-style:italic"># Skipping commits with particular message or from user</span>
<span style="color:#bf4f24">skip_commits</span>:
  <span style="color:#0b6125"><span style="color:#bf4f24">message<span style="color:#794938">:</span></span> <span style="color:#0b6125">/Created.*\.(png|jpg|jpeg|bmp|gif)/</span></span>       <span style="color:#5a525f;font-style:italic"># Regex for matching commit message</span>
  <span style="color:#0b6125"><span style="color:#bf4f24">author<span style="color:#794938">:</span></span> <span style="color:#0b6125">John</span></span>        <span style="color:#5a525f;font-style:italic"># Commit author's username, name, email or regexp maching one of these.</span>

<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>
<span style="color:#5a525f;font-style:italic">#    environment configuration    #</span>
<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>

<span style="color:#5a525f;font-style:italic"># Operating system (build VM template)</span>
<span style="color:#0b6125"><span style="color:#bf4f24">os<span style="color:#794938">:</span></span> <span style="color:#0b6125">Windows Server 2012</span></span>

<span style="color:#5a525f;font-style:italic"># scripts that are called at very beginning, before repo cloning</span>
<span style="color:#bf4f24">init</span>:
  <span style="color:#0b6125">- <span style="color:#0b6125">git config --global core.autocrlf input</span></span>

<span style="color:#5a525f;font-style:italic"># clone directory</span>
<span style="color:#0b6125"><span style="color:#bf4f24">clone_folder<span style="color:#794938">:</span></span> <span style="color:#0b6125">c:\projects\myproject</span></span>

<span style="color:#5a525f;font-style:italic"># setting up etc\hosts file</span>
<span style="color:#bf4f24">hosts</span>:
  <span style="color:#bf4f24">queue-server</span>: 127.0.0.1
  <span style="color:#bf4f24">db.server.com</span>: 127.0.0.2

<span style="color:#5a525f;font-style:italic"># environment variables</span>
<span style="color:#bf4f24">environment</span>:
  <span style="color:#0b6125"><span style="color:#bf4f24">my_var1<span style="color:#794938">:</span></span> <span style="color:#0b6125">value1</span></span>
  <span style="color:#0b6125"><span style="color:#bf4f24">my_var2<span style="color:#794938">:</span></span> <span style="color:#0b6125">value2</span></span>
  <span style="color:#5a525f;font-style:italic"># this is how to set encrypted variable. Go to "Encrypt data" page in account menu to encrypt data.</span>
  <span style="color:#bf4f24">my_secure_var1</span>:
    <span style="color:#0b6125"><span style="color:#bf4f24">secure<span style="color:#794938">:</span></span> <span style="color:#0b6125">FW3tJ3fMncxvs58/ifSP7w==</span></span>

<span style="color:#5a525f;font-style:italic"># environment:</span>
<span style="color:#5a525f;font-style:italic">#  global:</span>
<span style="color:#5a525f;font-style:italic">#    connection_string: server=12;password=13;</span>
<span style="color:#5a525f;font-style:italic">#    service_url: https://127.0.0.1:8090</span>
<span style="color:#5a525f;font-style:italic">#</span>
<span style="color:#5a525f;font-style:italic">#  matrix:</span>
<span style="color:#5a525f;font-style:italic">#  - db: mysql</span>
<span style="color:#5a525f;font-style:italic">#    provider: mysql</span>
<span style="color:#5a525f;font-style:italic">#</span>
<span style="color:#5a525f;font-style:italic">#  - db: mssql</span>
<span style="color:#5a525f;font-style:italic">#    provider: mssql</span>
<span style="color:#5a525f;font-style:italic">#    password:</span>
<span style="color:#5a525f;font-style:italic">#      secure: $#(JFDA)jQ@#$</span>

<span style="color:#5a525f;font-style:italic"># this is how to allow failing jobs in the matrix</span>
<span style="color:#bf4f24">matrix</span>:
  <span style="color:#0b6125"><span style="color:#bf4f24">fast_finish<span style="color:#794938">:</span></span> <span style="color:#0b6125">true     </span></span><span style="color:#5a525f;font-style:italic"># set this flag to immediately finish build once one of the jobs fails.</span>
  <span style="color:#bf4f24">allow_failures</span>:
    <span style="color:#0b6125">- platform<span style="color:#794938">:</span> <span style="color:#0b6125">x86</span></span>
      <span style="color:#0b6125"><span style="color:#bf4f24">configuration<span style="color:#794938">:</span></span> <span style="color:#0b6125">Debug</span></span>
    <span style="color:#0b6125">- platform<span style="color:#794938">:</span> <span style="color:#0b6125">x64</span></span>
      <span style="color:#0b6125"><span style="color:#bf4f24">configuration<span style="color:#794938">:</span></span> <span style="color:#0b6125">Release</span></span>

<span style="color:#5a525f;font-style:italic"># enable service required for build/tests</span>
<span style="color:#bf4f24">services</span>:
  <span style="color:#0b6125">- <span style="color:#0b6125">mssql2012sp1        </span></span><span style="color:#5a525f;font-style:italic"># start SQL Server 2012 SP1 Express</span>
  <span style="color:#0b6125">- <span style="color:#0b6125">mssql2012sp1rs      </span></span><span style="color:#5a525f;font-style:italic"># start SQL Server 2012 SP1 Express and Reporting Services</span>
  <span style="color:#0b6125">- <span style="color:#0b6125">mssql2008r2sp2      </span></span><span style="color:#5a525f;font-style:italic"># start SQL Server 2008 R2 SP2 Express</span>
  <span style="color:#0b6125">- <span style="color:#0b6125">mssql2008r2sp2rs    </span></span><span style="color:#5a525f;font-style:italic"># start SQL Server 2008 R2 SP2 Express and Reporting Services</span>
  <span style="color:#0b6125">- <span style="color:#0b6125">mysql               </span></span><span style="color:#5a525f;font-style:italic"># start MySQL 5.6 service</span>
  <span style="color:#0b6125">- <span style="color:#0b6125">postgresql          </span></span><span style="color:#5a525f;font-style:italic"># start PostgreSQL 9.3 service</span>
  <span style="color:#0b6125">- <span style="color:#0b6125">iis                 </span></span><span style="color:#5a525f;font-style:italic"># start IIS</span>
  <span style="color:#0b6125">- <span style="color:#0b6125">msmq                </span></span><span style="color:#5a525f;font-style:italic"># start Queuing services</span>

<span style="color:#5a525f;font-style:italic"># scripts that run after cloning repository</span>
<span style="color:#bf4f24">install</span>:
  <span style="color:#5a525f;font-style:italic"># by default, all script lines are interpreted as batch</span>
  <span style="color:#0b6125">- <span style="color:#0b6125">echo This is batch</span></span>
  <span style="color:#5a525f;font-style:italic"># to run script as a PowerShell command prepend it with ps:</span>
  <span style="color:#0b6125">- ps<span style="color:#794938">:</span> <span style="color:#0b6125">Write-Host 'This is PowerShell'</span></span>
  <span style="color:#5a525f;font-style:italic"># batch commands start from cmd:</span>
  <span style="color:#0b6125">- cmd<span style="color:#794938">:</span> <span style="color:#0b6125">echo This is batch again</span></span>
  <span style="color:#0b6125">- cmd<span style="color:#794938">:</span> <span style="color:#0b6125">set MY_VAR=12345</span></span>

<span style="color:#5a525f;font-style:italic"># enable patching of AssemblyInfo.* files</span>
<span style="color:#bf4f24">assembly_info</span>:
  <span style="color:#0b6125"><span style="color:#bf4f24">patch<span style="color:#794938">:</span></span> <span style="color:#0b6125">true</span></span>
  <span style="color:#0b6125"><span style="color:#bf4f24">file<span style="color:#794938">:</span></span> <span style="color:#0b6125">AssemblyInfo.*</span></span>
  <span style="color:#0b6125"><span style="color:#bf4f24">assembly_version<span style="color:#794938">:</span></span> <span style="color:#0b6125">"2.2.{build}"</span></span>
  <span style="color:#0b6125"><span style="color:#bf4f24">assembly_file_version<span style="color:#794938">:</span></span> <span style="color:#0b6125">"{version}"</span></span>
  <span style="color:#0b6125"><span style="color:#bf4f24">assembly_informational_version<span style="color:#794938">:</span></span> <span style="color:#0b6125">"{version}"</span></span>


<span style="color:#5a525f;font-style:italic"># Automatically register private account and/or project AppVeyor NuGet feeds.</span>
<span style="color:#bf4f24">nuget</span>:
  <span style="color:#0b6125"><span style="color:#bf4f24">account_feed<span style="color:#794938">:</span></span> <span style="color:#0b6125">true</span></span>
  <span style="color:#0b6125"><span style="color:#bf4f24">project_feed<span style="color:#794938">:</span></span> <span style="color:#0b6125">true</span></span>

<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>
<span style="color:#5a525f;font-style:italic">#       build configuration       #</span>
<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>

<span style="color:#5a525f;font-style:italic"># build platform, i.e. x86, x64, Any CPU. This setting is optional.</span>
<span style="color:#0b6125"><span style="color:#bf4f24">platform<span style="color:#794938">:</span></span> <span style="color:#0b6125">Any CPU</span></span>

<span style="color:#5a525f;font-style:italic"># to add several platforms to build matrix:</span>
<span style="color:#5a525f;font-style:italic">#platform:</span>
<span style="color:#5a525f;font-style:italic">#  - x86</span>
<span style="color:#5a525f;font-style:italic">#  - Any CPU</span>

<span style="color:#5a525f;font-style:italic"># build Configuration, i.e. Debug, Release, etc.</span>
<span style="color:#0b6125"><span style="color:#bf4f24">configuration<span style="color:#794938">:</span></span> <span style="color:#0b6125">Release</span></span>

<span style="color:#5a525f;font-style:italic"># to add several configurations to build matrix:</span>
<span style="color:#5a525f;font-style:italic">#configuration:</span>
<span style="color:#5a525f;font-style:italic">#  - Debug</span>
<span style="color:#5a525f;font-style:italic">#  - Release</span>

<span style="color:#bf4f24">build</span>:
  <span style="color:#0b6125"><span style="color:#bf4f24">project<span style="color:#794938">:</span></span> <span style="color:#0b6125">MyTestAzureCS.sln      </span></span><span style="color:#5a525f;font-style:italic"># path to Visual Studio solution or project</span>
  <span style="color:#0b6125"><span style="color:#bf4f24">publish_wap<span style="color:#794938">:</span></span> <span style="color:#0b6125">true               </span></span><span style="color:#5a525f;font-style:italic"># package Web Application Projects (WAP) for Web Deploy</span>
  <span style="color:#0b6125"><span style="color:#bf4f24">publish_wap_xcopy<span style="color:#794938">:</span></span> <span style="color:#0b6125">true         </span></span><span style="color:#5a525f;font-style:italic"># package Web Application Projects (WAP) for XCopy deployment</span>
  <span style="color:#0b6125"><span style="color:#bf4f24">publish_azure<span style="color:#794938">:</span></span> <span style="color:#0b6125">true             </span></span><span style="color:#5a525f;font-style:italic"># package Azure Cloud Service projects and push to artifacts</span>
  <span style="color:#0b6125"><span style="color:#bf4f24">publish_nuget<span style="color:#794938">:</span></span> <span style="color:#0b6125">true             </span></span><span style="color:#5a525f;font-style:italic"># package projects with .nuspec files and push to artifacts</span>
  <span style="color:#0b6125"><span style="color:#bf4f24">publish_nuget_symbols<span style="color:#794938">:</span></span> <span style="color:#0b6125">true     </span></span><span style="color:#5a525f;font-style:italic"># generate and publish NuGet symbol packages</span>

  <span style="color:#5a525f;font-style:italic"># MSBuild verbosity level</span>
  <span style="color:#0b6125"><span style="color:#bf4f24">verbosity<span style="color:#794938">:</span></span> <span style="color:#0b6125">quiet|minimal|normal|detailed</span></span>


<span style="color:#5a525f;font-style:italic"># scripts to run before build</span>
<span style="color:#bf4f24">before_build</span>:

<span style="color:#5a525f;font-style:italic"># scripts to run after build</span>
<span style="color:#bf4f24">after_build</span>:

<span style="color:#5a525f;font-style:italic"># to run your custom scripts instead of automatic MSBuild</span>
<span style="color:#bf4f24">build_script</span>:

<span style="color:#5a525f;font-style:italic"># to disable automatic builds</span>
<span style="color:#5a525f;font-style:italic">#build: off</span>

<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>
<span style="color:#5a525f;font-style:italic">#       tests configuration       #</span>
<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>

<span style="color:#bf4f24">test</span>:
  <span style="color:#bf4f24">assemblies</span>:
    <span style="color:#0b6125">- <span style="color:#0b6125">asm1.dll</span></span>
    <span style="color:#0b6125">- <span style="color:#0b6125">asm2.dll</span></span>

  <span style="color:#bf4f24">categories</span>:
    <span style="color:#0b6125">- <span style="color:#0b6125">UI</span></span>
    <span style="color:#0b6125">- <span style="color:#0b6125">E2E</span></span>

<span style="color:#5a525f;font-style:italic"># to run tests from different categories as separate jobs in parallel</span>
<span style="color:#5a525f;font-style:italic">#test:</span>
<span style="color:#5a525f;font-style:italic">#  categories:</span>
<span style="color:#5a525f;font-style:italic">#    - A            # A category common for all jobs</span>
<span style="color:#5a525f;font-style:italic">#    - [UI]         # 1st job</span>
<span style="color:#5a525f;font-style:italic">#    - [DAL, BL]    # 2nd job</span>

<span style="color:#5a525f;font-style:italic"># scripts to run before tests</span>
<span style="color:#bf4f24">before_test</span>:
  <span style="color:#0b6125">- <span style="color:#0b6125">echo script1</span></span>
  <span style="color:#0b6125">- ps<span style="color:#794938">:</span> <span style="color:#0b6125">Write-Host "script1"</span></span>

<span style="color:#5a525f;font-style:italic"># scripts to run after tests</span>
<span style="color:#bf4f24">after_test</span>:

<span style="color:#5a525f;font-style:italic"># to run your custom scripts instead of automatic tests</span>
<span style="color:#bf4f24">test_script</span>:
  <span style="color:#0b6125">- <span style="color:#0b6125">echo This is my custom test script</span></span>

<span style="color:#5a525f;font-style:italic"># to disable automatic tests </span>
<span style="color:#5a525f;font-style:italic">#test: off</span>


<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>
<span style="color:#5a525f;font-style:italic">#      artifacts configuration    #</span>
<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>

<span style="color:#bf4f24">artifacts</span>:

  <span style="color:#5a525f;font-style:italic"># pushing a single file</span>
  <span style="color:#0b6125">- path<span style="color:#794938">:</span> <span style="color:#0b6125">test.zip</span></span>

  <span style="color:#5a525f;font-style:italic"># pushing a single file with environment variable in path and "Deployment name" specified</span>
  <span style="color:#0b6125">- path<span style="color:#794938">:</span> <span style="color:#0b6125">MyProject\bin\$(configuration)</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">name<span style="color:#794938">:</span></span> <span style="color:#0b6125">myapp</span></span>

  <span style="color:#5a525f;font-style:italic"># pushing entire folder as a zip archive</span>
  <span style="color:#0b6125">- path<span style="color:#794938">:</span> <span style="color:#0b6125">logs</span></span>

  <span style="color:#5a525f;font-style:italic"># pushing all *.nupkg files in directory</span>
  <span style="color:#0b6125">- path<span style="color:#794938">:</span> <span style="color:#0b6125">out\*.nupkg</span></span>


<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>
<span style="color:#5a525f;font-style:italic">#     deployment configuration    #</span>
<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>

<span style="color:#5a525f;font-style:italic"># providers: Local, FTP, WebDeploy, AzureCS, AzureBlob, S3, NuGet, Environment</span>
<span style="color:#5a525f;font-style:italic"># provider names are case-sensitive!</span>
<span style="color:#bf4f24">deploy</span>:

    <span style="color:#5a525f;font-style:italic"># FTP deployment provider settings</span>
  <span style="color:#0b6125">- provider<span style="color:#794938">:</span> <span style="color:#0b6125">FTP</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">server<span style="color:#794938">:</span></span> <span style="color:#0b6125">ftp.myserver.com</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">username<span style="color:#794938">:</span></span> <span style="color:#0b6125">admin</span></span>
    <span style="color:#bf4f24">password</span>:
      <span style="color:#0b6125"><span style="color:#bf4f24">secure<span style="color:#794938">:</span></span> <span style="color:#0b6125">eYKZKFkkEvFYWX6NfjZIVw==</span></span>
    <span style="color:#bf4f24">folder</span>:
    <span style="color:#bf4f24">application</span>:
    <span style="color:#0b6125"><span style="color:#bf4f24">active_mode<span style="color:#794938">:</span></span> <span style="color:#0b6125">false</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">enable_ssl<span style="color:#794938">:</span></span> <span style="color:#0b6125">false</span></span>

    <span style="color:#5a525f;font-style:italic"># Amazon S3 deployment provider settings</span>
  <span style="color:#0b6125">- provider<span style="color:#794938">:</span> <span style="color:#0b6125">S3</span></span>
    <span style="color:#bf4f24">access_key_id</span>:
      <span style="color:#0b6125"><span style="color:#bf4f24">secure<span style="color:#794938">:</span></span> <span style="color:#0b6125">ABcd==</span></span>
    <span style="color:#bf4f24">secret_access_key</span>:
      <span style="color:#0b6125"><span style="color:#bf4f24">secure<span style="color:#794938">:</span></span> <span style="color:#0b6125">ABcd==</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">bucket<span style="color:#794938">:</span></span> <span style="color:#0b6125">my_bucket</span></span>
    <span style="color:#bf4f24">folder</span>:
    <span style="color:#bf4f24">artifact</span>:
    <span style="color:#0b6125"><span style="color:#bf4f24">set_public<span style="color:#794938">:</span></span> <span style="color:#0b6125">false</span></span>

    <span style="color:#5a525f;font-style:italic"># Azure Blob storage deployment provider settings</span>
  <span style="color:#0b6125">- provider<span style="color:#794938">:</span> <span style="color:#0b6125">AzureBlob</span></span>
    <span style="color:#bf4f24">storage_account_name</span>:
      <span style="color:#0b6125"><span style="color:#bf4f24">secure<span style="color:#794938">:</span></span> <span style="color:#0b6125">ABcd==</span></span>
    <span style="color:#bf4f24">storage_access_key</span>:
      <span style="color:#0b6125"><span style="color:#bf4f24">secure<span style="color:#794938">:</span></span> <span style="color:#0b6125">ABcd==</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">container<span style="color:#794938">:</span></span> <span style="color:#0b6125">my_container</span></span>
    <span style="color:#bf4f24">folder</span>:
    <span style="color:#bf4f24">artifact</span>:

    <span style="color:#5a525f;font-style:italic"># Web Deploy deployment provider settings</span>
  <span style="color:#0b6125">- provider<span style="color:#794938">:</span> <span style="color:#0b6125">WebDeploy</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">server<span style="color:#794938">:</span></span> <span style="color:#0b6125">http://www.deploy.com/myendpoint</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">website<span style="color:#794938">:</span></span> <span style="color:#0b6125">mywebsite</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">username<span style="color:#794938">:</span></span> <span style="color:#0b6125">user</span></span>
    <span style="color:#bf4f24">password</span>:
      <span style="color:#0b6125"><span style="color:#bf4f24">secure<span style="color:#794938">:</span></span> <span style="color:#0b6125">eYKZKFkkEvFYWX6NfjZIVw==</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">ntlm<span style="color:#794938">:</span></span> <span style="color:#0b6125">false</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">remove_files<span style="color:#794938">:</span></span> <span style="color:#0b6125">false</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">app_offline<span style="color:#794938">:</span></span> <span style="color:#0b6125">false</span></span>
	<span style="color:#0b6125"><span style="color:#bf4f24">skip_dirs<span style="color:#794938">:</span></span> <span style="color:#0b6125">\\App_Data</span></span>
	<span style="color:#0b6125"><span style="color:#bf4f24">skip_files<span style="color:#794938">:</span></span> <span style="color:#0b6125">web.config</span></span>
    <span style="color:#bf4f24">on</span>:
      <span style="color:#0b6125"><span style="color:#bf4f24">branch<span style="color:#794938">:</span></span> <span style="color:#0b6125">release</span></span>
      <span style="color:#0b6125"><span style="color:#bf4f24">platform<span style="color:#794938">:</span></span> <span style="color:#0b6125">x86</span></span>
      <span style="color:#0b6125"><span style="color:#bf4f24">configuration<span style="color:#794938">:</span></span> <span style="color:#0b6125">debug</span></span>

    <span style="color:#5a525f;font-style:italic"># Deploying to Azure Cloud Service</span>
  <span style="color:#0b6125">- provider<span style="color:#794938">:</span> <span style="color:#0b6125">AzureCS</span></span>
    <span style="color:#bf4f24">subscription_id</span>:
      <span style="color:#0b6125"><span style="color:#bf4f24">secure<span style="color:#794938">:</span></span> <span style="color:#0b6125">fjZIVw==</span></span>
    <span style="color:#bf4f24">subscription_certificate</span>:
      <span style="color:#0b6125"><span style="color:#bf4f24">secure<span style="color:#794938">:</span></span> <span style="color:#0b6125">eYKZKFkkEv...FYWX6NfjZIVw==</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">storage_account_name<span style="color:#794938">:</span></span> <span style="color:#0b6125">my_storage</span></span>
    <span style="color:#bf4f24">storage_access_key</span>:
      <span style="color:#0b6125"><span style="color:#bf4f24">secure<span style="color:#794938">:</span></span> <span style="color:#0b6125">ABcd==</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">service<span style="color:#794938">:</span></span> <span style="color:#0b6125">my_service</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">slot<span style="color:#794938">:</span></span> <span style="color:#0b6125">Production</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">target_profile<span style="color:#794938">:</span></span> <span style="color:#0b6125">Cloud</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">artifact<span style="color:#794938">:</span></span> <span style="color:#0b6125">MyPackage.cspkg</span></span>

    <span style="color:#5a525f;font-style:italic"># Deploying to NuGet feed</span>
  <span style="color:#0b6125">- provider<span style="color:#794938">:</span> <span style="color:#0b6125">NuGet</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">server<span style="color:#794938">:</span></span> <span style="color:#0b6125">https://my.nuget.server/feed</span></span>
    <span style="color:#bf4f24">api_key</span>:
      <span style="color:#0b6125"><span style="color:#bf4f24">secure<span style="color:#794938">:</span></span> <span style="color:#0b6125">FYWX6NfjZIVw==</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">skip_symbols<span style="color:#794938">:</span></span> <span style="color:#0b6125">false</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">symbol_server<span style="color:#794938">:</span></span> <span style="color:#0b6125">https://your.symbol.server/feed</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">artifact<span style="color:#794938">:</span></span> <span style="color:#0b6125">MyPackage.nupkg</span></span>

    <span style="color:#5a525f;font-style:italic"># Deploying to a named environment</span>
  <span style="color:#0b6125">- provider<span style="color:#794938">:</span> <span style="color:#0b6125">Environment</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">name<span style="color:#794938">:</span></span> <span style="color:#0b6125">staging</span></span>
    <span style="color:#bf4f24">on</span>:
      <span style="color:#0b6125"><span style="color:#bf4f24">branch<span style="color:#794938">:</span></span> <span style="color:#0b6125">staging</span></span>
      <span style="color:#0b6125"><span style="color:#bf4f24">env_var1<span style="color:#794938">:</span></span> <span style="color:#0b6125">value1</span></span>
      <span style="color:#0b6125"><span style="color:#bf4f24">env_var2<span style="color:#794938">:</span></span> <span style="color:#0b6125">value2</span></span>

<span style="color:#5a525f;font-style:italic"># scripts to run before deployment</span>
<span style="color:#bf4f24">before_deploy</span>:

<span style="color:#5a525f;font-style:italic"># scripts to run after deployment</span>
<span style="color:#bf4f24">after_deploy</span>:

<span style="color:#5a525f;font-style:italic"># to run your custom scripts instead of provider deployments</span>
<span style="color:#bf4f24">deploy_script</span>:

<span style="color:#5a525f;font-style:italic"># to disable deployment</span>
<span style="color:#5a525f;font-style:italic">#deploy: off</span>

<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>
<span style="color:#5a525f;font-style:italic">#        global handlers          #</span>
<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>

<span style="color:#5a525f;font-style:italic"># on successful build</span>
<span style="color:#bf4f24">on_success</span>:
  <span style="color:#0b6125">- <span style="color:#0b6125">do something</span></span>

<span style="color:#5a525f;font-style:italic"># on build failure</span>
<span style="color:#bf4f24">on_failure</span>:
  <span style="color:#0b6125">- <span style="color:#0b6125">do something</span></span>

<span style="color:#5a525f;font-style:italic"># after build failure or success</span>
<span style="color:#bf4f24">on_finish</span>:
  <span style="color:#0b6125">- <span style="color:#0b6125">do something</span></span>

<span style="color:#b52a1d;">  </span>
<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>
<span style="color:#5a525f;font-style:italic">#         notifications           #</span>
<span style="color:#5a525f;font-style:italic">#---------------------------------#</span>
<span style="color:#b52a1d;">  </span>
<span style="color:#bf4f24">notifications</span>:

  <span style="color:#5a525f;font-style:italic"># HipChat</span>
  <span style="color:#0b6125">- provider<span style="color:#794938">:</span> <span style="color:#0b6125">HipChat</span></span>
    <span style="color:#bf4f24">auth_token</span>:
      <span style="color:#0b6125"><span style="color:#bf4f24">secure<span style="color:#794938">:</span></span> <span style="color:#0b6125">RbOnSMSFKYzxzFRrxM1+XA==</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">room<span style="color:#794938">:</span></span> <span style="color:#0b6125">ProjectA</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">template<span style="color:#794938">:</span></span> <span style="color:#0b6125">"{message}, {commitId}, ..."</span></span>

  <span style="color:#5a525f;font-style:italic"># Slack</span>
  <span style="color:#0b6125">- provider<span style="color:#794938">:</span> <span style="color:#0b6125">Slack</span></span>
    <span style="color:#bf4f24">auth_token</span>:
      <span style="color:#0b6125"><span style="color:#bf4f24">secure<span style="color:#794938">:</span></span> <span style="color:#0b6125">kBl9BlxvRMr9liHmnBs14A==</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">channel<span style="color:#794938">:</span></span> <span style="color:#0b6125">development</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">template<span style="color:#794938">:</span></span> <span style="color:#0b6125">"{message}, {commitId}, ..."</span></span>
<span style="color:#b52a1d;">    </span>
  <span style="color:#5a525f;font-style:italic"># Campfire</span>
  <span style="color:#0b6125">- provider<span style="color:#794938">:</span> <span style="color:#0b6125">Campfire</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">account<span style="color:#794938">:</span></span> <span style="color:#0b6125">appveyor</span></span>
    <span style="color:#bf4f24">auth_token</span>:
      <span style="color:#0b6125"><span style="color:#bf4f24">secure<span style="color:#794938">:</span></span> <span style="color:#0b6125">RifLRG8Vfyol+sNhj9u2JA==</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">room<span style="color:#794938">:</span></span> <span style="color:#0b6125">ProjectA</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">template<span style="color:#794938">:</span></span> <span style="color:#0b6125">"{message}, {commitId}, ..."</span></span>

  <span style="color:#5a525f;font-style:italic"># Webhook</span>
  <span style="color:#0b6125">- provider<span style="color:#794938">:</span> <span style="color:#0b6125">Webhook</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">url<span style="color:#794938">:</span></span> <span style="color:#0b6125">http://www.myhook2.com</span></span>
    <span style="color:#bf4f24">headers</span>:
      <span style="color:#bf4f24">User-Agent</span>: myapp 1.0
      <span style="color:#bf4f24">Authorization</span>:
        <span style="color:#0b6125"><span style="color:#bf4f24">secure<span style="color:#794938">:</span></span> <span style="color:#0b6125">GhD+5xhLz/tkYY6AO3fcfQ==</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">on_build_success<span style="color:#794938">:</span></span> <span style="color:#0b6125">false</span></span>
    <span style="color:#0b6125"><span style="color:#bf4f24">on_build_failure<span style="color:#794938">:</span></span> <span style="color:#0b6125">true</span></span>
</pre>