---
title: Downloading AppVeyor build artifacts (PowerShell)
layout: docs
---

# Downloading AppVeyor build artifacts (PowerShell)

<!-- HTML generated using http://markup.su/highlighter -->

<pre><span style="color:#234a97">$apiUrl</span> = <span style="color:#0b6125">'https://ci.appveyor.com/api'</span>
<span style="color:#234a97">$token</span> = <span style="color:#0b6125">'&lt;your-api-token>'</span>
<span style="color:#234a97">$headers</span> = @{
  <span style="color:#0b6125">"Authorization"</span> = <span style="color:#0b6125">"Bearer <span style="color:#234a97">$token</span>"</span>
  <span style="color:#0b6125">"Content-type"</span> = <span style="color:#0b6125">"application/json"</span>
}
<span style="color:#234a97">$accountName</span> = <span style="color:#0b6125">'&lt;your-account-name>'</span>
<span style="color:#234a97">$projectSlug</span> = <span style="color:#0b6125">'&lt;your-project-slug>'</span>

<span style="color:#234a97">$downloadLocation</span> = <span style="color:#0b6125">'C:\projects'</span>

<span style="color:#5a525f;font-style:italic"># get project with last build details</span>
<span style="color:#234a97">$project</span> = Invoke-RestMethod -Method Get -Uri <span style="color:#0b6125">"<span style="color:#234a97">$apiUrl</span>/projects/<span style="color:#234a97">$accountName</span>/<span style="color:#234a97">$projectSlug</span>"</span> -Headers <span style="color:#234a97">$headers</span>

<span style="color:#5a525f;font-style:italic"># we assume here that build has a single job</span>
<span style="color:#5a525f;font-style:italic"># get this job id</span>
<span style="color:#234a97">$jobId</span> = <span style="color:#234a97">$project</span>.build.<span style="color:#693a17">jobs</span>[0].jobId

<span style="color:#5a525f;font-style:italic"># get job artifacts (just to see what we've got)</span>
<span style="color:#234a97">$artifacts</span> = Invoke-RestMethod -Method Get -Uri <span style="color:#0b6125">"<span style="color:#234a97">$apiUrl</span>/buildjobs/<span style="color:#234a97">$jobId</span>/artifacts"</span> -Headers <span style="color:#234a97">$headers</span>

<span style="color:#5a525f;font-style:italic"># here we just take the first artifact, but you could specify its file name</span>
<span style="color:#5a525f;font-style:italic"># $artifactFileName = 'MyWebApp.zip'</span>
<span style="color:#234a97">$artifactFileName</span> = <span style="color:#234a97">$artifacts</span>[0].fileName

<span style="color:#5a525f;font-style:italic"># artifact will be downloaded as </span>
<span style="color:#234a97">$localArtifactPath</span> = <span style="color:#0b6125">"<span style="color:#234a97">$downloadLocation</span><span style="color:#696969;font-weight:700">\$</span>artifactFileName"</span>

<span style="color:#5a525f;font-style:italic"># download artifact</span>
<span style="color:#5a525f;font-style:italic"># -OutFile - is local file name where artifact will be downloaded into</span>
Invoke-RestMethod -Method Get -Uri <span style="color:#0b6125">"<span style="color:#234a97">$apiUrl</span>/buildjobs/<span style="color:#234a97">$jobId</span>/artifacts/<span style="color:#234a97">$artifactFileName</span>"</span> <span style="color:#0b6125">`
     -OutFile <span style="color:#234a97">$localArtifactPath</span> -Headers <span style="color:#234a97">$headers</span>
</span></pre>