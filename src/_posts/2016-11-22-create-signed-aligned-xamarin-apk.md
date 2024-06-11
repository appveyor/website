---
title: Creating a Signed and ZipAligned APK (for Google Play) from Xamarin
---

## Introduction

This is a guest post by [Cedd Burge](https://github.com/ceddlyburge), Software Developer Lead at [RES](https://medium.com/res-software-team).

This post is written from the point of view of someone (me) who is already proficient in C#, but was new to Xamarin, Mobile phone development, and AppVeyor.

It contains from scratch steps to create a Xamarin Android application (in Visual Studio), to build it on AppVeyor and to publish it to the Play Store. You can [look at the repo I created to test this post](https://github.com/ceddlyburge/create-signed-zipaligned-xamarin-apk-on-appveyor) if you get stuck.

First, install Xamarin from [https://www.xamarin.com/download](https://www.xamarin.com/download).

## Create a new repository on GitHub

If you are new to GitHub, see [this getting started guide](https://guides.github.com/activities/hello-world/), otherwise simply create a new repo and git clone it somewhere convenient.

## Create a new Xamarin Portable Class Library (PCL) project

In my version of Visual Studio (Community 2015), this is done by clicking *"File - New - Project"* on the main menu and then selecting *"Blank App (Xamarin.Forms Portable)"* from *"Templates - Visual C# - Cross-Platform"*. Give it a interesting name, which I will assume to be **YourAppName** for the rest of this post.

## Run the app!

Select the **YourAppName.Droid** project and run it. This should show the bare bones app in an emulator.

If you have Hyper-V enabled (maybe you use Docker), then you might get an Deployment Error when doing this. [Disable Hyper-V and restart your machine to fix this](https://stackoverflow.com/questions/31613607/visual-studio-2015-emulator-for-android-not-working-xde-exe-exit-code-3).

You might also run in to compile errors due to [ridiculous dependency weirdness](https://stackoverflow.com/questions/40081826/system-missingmethodexception-method-android-support-v4-widget-drawerlayout-ad).

## Create APK file manually

To tell Google about your app, you have to make some changes to the *Properties\AndroidManifest.xml* file of the **YourAppName.Droid** project.

* Add a `package="com.yourappname"` (or similar) attribute to the root `manifest` node. The package name must be unique on Google Play and must follow normal [java package name conventions](https://en.wikipedia.org/wiki/Java_package#Package_naming_conventions). Most people use their url in reverse (eg com.yourappname instead of yourappname.com) and stick to lower case.
* Add an `android:versionCode="1"` attribute to the root `manifest` node. This is an integer and it must be incremented every time you upload an apk on Google Play.
* Add an `android:versionName="0.1` attribute to the root `manifest` node. This value can be anything you like and is displayed in Google Play.
* Change the `label` attribute on the `application` node to YourAppName.

Visual studio has some tools to create an APK, and they seem to be in constant churn, but at the time of writing, the process is as follows.

* Change the *"Build Configuration"* to *"App Store"*.
* Select the **YourAppName.droid** project and click *"Build - Archive"* from the main menu.
* The archive manager window will appear and build your app.
* Click *"Distribute"*, which will pop up the Distribute window.
* Initially you won't have a signing identity, so click on the green plus button and fill in the details to create one.
* Once created, double click on it and note where it is on disk (**YourKeyStoreFilename** from now on)
* Click *"Save As"* to create an APK.

## Upload the APK to Google Play

* Create a Developer Account on [Google Play Developer Console](https://play.google.com/apps/publish) (this costs $25)
* Click *"Add New Application"* and add **YourAppName**
* Upload the APK that you saved in the previous step

## Publish the App on Google Play (probably just to Alpha or Beta)

There are some requirements when publishing an application to Google Play, and these are likely to change, but happily google tells you what they all are. If you click on *"Why can't I publish?"*, near the top right corner of the page, you will get a list of things to do.

It's all simple stuff that can be done within the Developer Console. Some screenshots and pictures are required. If you just want to get a test version up quickly, then [feel free to use mine](https://github.com/ceddlyburge/CanoePoloLeagueOrganiser/tree/master/CanoePoloLeagueOrganiserXamarin/screenshots-etc) temporarily.

There are a lot of optional things you can do as well, which can be worthwhile if you want to publish a killer app. The [Google Launch Checklist](https://developer.android.com/distribute/tools/launch-checklist.html), is comprehensive, but takes a long time to read.

## Automate APK creation locally

When working with appveyor, it always makes sense to test on your own computer first. The feedback is immediate and you iterate very quickly. It takes a lot longer to modify the appveyor.yml file, push it and wait for a build to go through. Also, if it works locally but doesn't work on AppVeyor, you know the problem is a configuration difference between your computer and the AppVeyor environment (eg a different version of msbuild).

Being as we are making a new version of the apk, we need to increment `android:versionCode` in *Properties/AndroidManifest.xml*.

There are some [Xamarin MSBuild targets](https://developer.xamarin.com/guides/android/under_the_hood/build_process/#22-build-targets), which we can use to create a Signed and ZipAligned apk as below.

There are 2 passwords required in the command because [java KeyStores](https://docs.oracle.com/javase/7/docs/api/java/security/KeyStore.html) can contain multiple Alias'. So the first password is to access the KeyStore, and the second one is to access the specific alias. Visual studio hides this complexity from you and assigns the same password to both places.

I do a lot of work in GIT Bash, but this statement only works in Batch (the windows command line), I think because of parameter escaping.

```batch
MSBuild "/t:SignAndroidPackage" "/p:Configuration=Release" "/p:AndroidKeyStore=true" "/p:AndroidSigningKeyAlias=YourKeyAlias" "/p:AndroidSigningKeyPass=YourKeyStorePassword" "/p:AndroidSigningKeyStore=YourKeyStoreFilename" "/p:AndroidSigningStorePass=YourKeyStorePassword" "YourAppName.csproj"
```

This will create **com.yourappname-Signed.apk** in the *bin\release* folder. Upload this to Google Play to make sure that everything is working properly.

## Automate APK creation on AppVeyor

You will need to link an AppVeyor account to your GitHub one, so let's do that:

* Navigate to your repo in GitHub
* Click "Settings" on the repo
* Click "Integrations and services"
* Click "Browse Directory"
* Click "AppVeyor"
* Click "Configure"
* Click "Grant Access"

Now Log in to [AppVeyor.com](https://ci.appveyor.com), probably using your GitHub account

* Click "Projects"
* Click "New Project"
* Choose your GitHub repository and click "Add"

MSBuild needs to access your KeyStore file in order to sign the apk, so copy **YourKeyStoreFilename** in to the folder of **YourAppName.Droid** project (called **YourKeyStoreLocalFilename** from now on).

When we created the apk from the command line, we entered in some passwords, and we obviously can't save these passwords to a public Git repository. Happily AppVeyor have thought of this, and you can convert passwords in to tokens that can be exposed publicly.

To do this click **Account** &rarr; **Encrypt YAML** from the drop down menu. Enter **YourKeyStorePassword** in to *"Value to encrypt"* and click *"Encrypt"*. AppVeyor will then display a token which you can use in place of the real value.

Now that we have everything we need, add an *appveyor.yml* file to the root of your repository as below. Note that **YourLocalKeyStoreFilename** is relative to the csproj file being built (the **YourAppName.Droid** folder below).

```yaml
environment:
  keystore-password:
    secure: DSwAr4fYt3Q35Sjob5qAN5uj # YourPassword for keystore
before_build:
  - nuget restore
build_script:
  - msbuild "/t:SignAndroidPackage" "/p:Configuration=Release" "/p:AndroidKeyStore=true" "/p:AndroidSigningKeyAlias=YourKeyAlias" "/p:AndroidSigningKeyPass=%keystore-password%" "/p:AndroidSigningKeyStore=YourLocalKeyStoreFilename" "/p:AndroidSigningStorePass=%keystore-password%"  "YourAppName.Droid\YourAppName.csproj"
artifacts:
  - path: YourAppName.Droid\bin\Release\com.yourappname-Signed.apk
```

Remember to update `android:versionCode` and then push to GitHub. This will trigger a build on AppVeyor. The `build_script` section will call msbuild to create the signed and zipaligned apk file, and the `artifacts` section will archive the apk file so we can download it later.

Go to [AppVeyor.com](https://ci.appveyor.com), click on your project, click on *"Artifacts"*, download the apk file, and then upload it to google to check that everything has worked properly.

## Conclusion

There is a lot to learn to get everything working, and I couldn't find a single source for all of these things, but having done it once, the process is actually quite simple, and the tools and services involved are generally a pleasure to work with.

Best regards,<br>
Cedd Burge

Follow Cedd on Twitter: [@cuddlyburger](https://twitter.com/cuddlyburger)<br>
Follow AppVeyor on Twitter: [@appveyor](https://twitter.com/appveyor)
