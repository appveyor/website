# This powershell script will clone the settings from one project to another via an exported YAML

$apiUrl = 'https://ci.appveyor.com/api'
$token = '[your token]'
$headers = @{
  "Authorization" = "Bearer $token"
  "Content-type" = "application/json"
}
$accountName = '[account name]'
$projectSlug = '[project slug]'

$downloadLocation = 'c:\temp'

# Get the YAML export from project settings export.  Copy and paste it works fine
$project = Invoke-RestMethod -Method Put -Uri "$apiUrl/projects/$accountName/$projectSlug/settings/yaml" -Headers $headers -Body "[paste your yaml export here the entire body"

