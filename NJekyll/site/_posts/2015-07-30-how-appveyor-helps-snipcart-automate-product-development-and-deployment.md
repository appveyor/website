---
layout: post
title: How AppVeyor Helps Snipcart Automate Product Development & Deployment
---

*This is a guest blog post from Charles Ouellet of [Snipcart](https://snipcart.com). Charles is a co-founder and lead engineer at [Snipcart](https://snipcart.com), a solution empowering developers to turn any website into a customizable e-commerce platform. He likes code, scotch, and colourful socks. You can follow him on [Twitter](https://twitter.com/couellet).*

<hr>

When we launched our developer-centric e-commerce platform, finding a premium cloud-based continuous integration solution was a top priority. We used Jenkins at first, and, while we liked it, we were also looking forward to handling those operations directly in the cloud. For months, we kept our eyes and ears opened, searching for such a solution as we kept developing our product and growing our business.

A few months ago, I stumbled upon AppVeyor while exploring the Tweetosphere one night. Upon skimming through their home page and documentation, I quickly realized this was exactly what our team and product needed. Since Snipcart's API is built on top of **ASP.NET Web API**, we needed to find a web-based solution that was supporting **.NET**. And that's exactly what I found that night with AppVeyor.

The day after that, we spent maybe an hour setting it up, and it worked like a real charm.

## How we use AppVeyor exactly for our Snipcart application

One of AppVeyor's killer feature is that it gives you the ability to configure your whole build with a **YAML** file. This allows us to have the build configuration in our source control, making it very easy to maintain.

To give you a little context: Snipcart is basically a web application, a worker process that processes queued events, and two JavaScript applications that consume our API. Let's get more specific with each of these components.

### Web application

Like I mentioned earlier, our web application is an ASP.NET Web API. Once the build is completed and all our tests have passed (yep, AppVeyor supports running unit tests too!), we deploy our application to our [Azure](https://azure.microsoft.com/en-us/) web apps via WebDeploy. The build process and the deployment process are all configured in the YAML file. We just push to our `production` git branch, and it triggers a build and a deployment.

Once it's deployed, Azure takes care of spawning multiple instances when needed.

### Worker

Our worker is a Cloud Service hosted on Azure. Once the build is completed, it is automatically deployed by AppVeyor as well, which supports a wide range of deployment processes, including Cloud Services. Before making the switch to AppVeyor, this was a pain for us because our developers needed to deploy the worker through Visual Studio directly; it wasn't automated, which could have led to errors.

### JavaScript applications

All of our client applications are single page apps built on top of Backbone.js. We use [webpack](http://webpack.github.io/) to bundle our application. We have a [gulp](http://gulpjs.com/) task that does all the job, bundling the numerous JavaScript files into a single file and uglifying the output. AppVeyor also allows us to run this gulp task inside our build process, which is pretty amazing. It doesn't only allow to build .NET applications: you can use npm, gem, and any NodeJS modules.

We also have unit tests for these two applications; we run those in AppVeyor as well, using a gulp task.

### CDN

Once our JavaScript applications are built, we automatically push all of our assets on an Azure Blob Storage container. This is another deployment option provided by AppVeyor. We upload all of the static files that are included on our customers websites, such as our default stylesheet, our `snipcart.js` file and our custom web fonts.

On top of the storage, we are also using [KeyCDN](https://www.keycdn.com/). We have a pull zone that fetches the content of our Blob storage. KeyCDN takes care of the caching and everything needed to make sure our static files are served as fast as possible. We discuss it further in [this blog post](https://snipcart.com/blog/snipcart-infrastructure-upgrade-new-cdn).

When we make changes to `snipcart.js` or to any other static file that is cached by the CDN, we need to invalidate the cache. If we don't, customers would need to hit refresh multiple times, which would not make any sense. With AppVeyor, we make an HTTP request to the KeyCDN API to purge our cache zone after the Blob storage has been updated. By doing so, we are sure our customers always have the latest version of our static files.

## Conclusion? We friggin' love AppVeyor

Since we automated everything with AppVeyor, we are *much more* confident with our deployments. Sometimes, we even start a build and go play a few ping pong games at the office while it deploys. We know it'll just work. With our solid test suite in place, we trust AppVeyor won't deploy something that is broken.

As I finish writing this post, I realize that I don't see how we could work without this amazing tool today. It's a crucial part of our product development process.