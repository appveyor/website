---
layout: docs
title: Creating Google Cloud Platform service account
---

# Creating Google Cloud Platform service account

## Create service account and download certificate

* Open Google Cloud Platform menu navigate to **IAM & Admin**
* Select **Service accounts** and press **Create service account**
    * Set descriptive **Service account name**, for example **Appveyor CI**
        * **Service account ID** will be automatically regenerated, write it down to use later in AppVeyor settings
    * Select **Project > Editor** in **Role** menu
    * Check **Furnish a new private key**
        * Select **P12**
    * Press **CREATE**
    * Close **Service account created** window
        * Leave default password unchanged
    * Certificate in P12 format should be saved to local computer
        * Remember it's location and optionally re-save certificate in some secure place

## Convert certificate to Base64 string

* Run the following PowerShell commands:

```posh
$bytes = [System.IO.File]::ReadAllBytes("<path-to-P12-file>")
$base64Str = [System.Convert]::ToBase64String($bytes)
[System.IO.File]::WriteAllText("<path-to-result-TXT-file>", $base64Str)
```

* Remember the location of the resulting TXT file.