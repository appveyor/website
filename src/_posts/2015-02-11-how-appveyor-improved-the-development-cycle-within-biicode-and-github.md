---
title: How AppVeyor improved the development cycle within biicode and GitHub
---

*This is a guest blog post from María de Antón of [biicode](https://www.biicode.com/) - the maker of C/C++ Dependency Manager.*

---

Biicode is a C and C++ dependency manager. Continuous Integration with AppVeyor closes the developing cycle with GitHub and biicode.

At biicode we use AppVeyor Continuous Integration to automatically build and publish our new commits and releases to biicode.

<p class="text-center">
    <img src="/assets/img/posts/biicode/biicode-logo.png" alt="Biicode Logo" width="250" height="75">
</p>

Most of our users are pretty familiar with VCS like github or bitbucket, and lately they've been asking for a full workflow to develop their projects with GitHub and [biicode](http://www.biicode.com/).

We started using AppVeyor to test our Windows builds, but once we realized about its possibilities we couldn't let it go. We had to make a full workflow example for users to test their apps in Windows and automatically publish the successful versions to biicode.

With a focus on the long-term success of this solution, we realized this solution worked perfectly for DEV and/or untagged versions but didn't have a desired outcome while working with tagged or STABLE versions.

Publishing a new STABLE block to biicode - a block is where your sources are located in biicode, each block follows the same standard structure - increases by one the value of the latest published version in biicode.

This meant that whenever publishing an STABLE version you had to remember to update your biicode parent version the biicode.conf file and it's curious how you remember just when your build fails because you forgot to update your parents.

> ERROR: You are outdated, you are modifying username/blockname: 2
> but last version is username/blockname: 3
> you can ...

As someone who loves time, I really needed to find a solution for this. Well, AppVeyor makes this no longer a problem.  With help of their full docs, support and the many possibilities available within the environment variables and build configuration we got what we needed.

Now biicode workflow relies fully on GitHub thanks to AppVeyor. Once you use AppVeyor to test, build and publish a new version to biicode, it will automatically update your parents and commit  and push them to github skipping builds whose commits match our automatic commit via the [appveyor.yml file](/docs/appveyor-yml/).

Here's a guide about [how to pushing to a remote Git repository from an AppVeyor build](/docs/how-to/git-push/).

## Check it out

Post's original marterial is:

* [Forked cpp-expresion-parser repo in GitHub](https://github.com/MariadeAnton/cpp-expression-parser)  from the [original repo by Brandon Amos](https://github.com/bamos/cpp-expression-parser)
* [cpp-expression-parser builds in AppVeyor](https://ci.appveyor.com/project/MariadeAnton/cpp-expression-parser) Continuous Integration and Deployment
* [cpp-expression parser biicode block](http://www.biicode.com/amalulla/cpp-expression-parser) and with its automatically published releases
* Blog post at biicode with full detail about this feature.
