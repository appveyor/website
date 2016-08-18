---
title: AppVeyor premium build environment and new pricing
---

Based on feedback from our customers we've been working on improving AppVeyor performance
and got amazing results!

## New super-fast environment

For the last couple of months we’ve been experimenting with running builds on new "Premium" environment.
It’s based on Hyper-V and hosted on a dedicated hardware with SSD drives and faster CPUs.

We moved most of our existing customers to this new environment and they were very satisfied
with the results. Builds start almost instantly, run 2-3 times faster with greater stability!

We still have Azure environment for open-source projects and "Basic" plan.


## New Pricing

With the introduction of the new environment we decided to review our plans once again
to make them more flexible for companies with different business needs.

There is a new entry-level plan for individual developers and small teams with 1 private project
and 1 concurrent job building on Azure. There is an upgraded "Pro" plan now with **unlimited** number
of projects and **super-fast builds**. For those teams actively using AppVeyor new "Premium" plan
now offers 3 concurrent jobs on fast environment.

Also, we introduce yearly pricing for "Pro" and "Premium" plans giving you **2 months free**!

<table style="width:70%;max-width:1042px;margin: 2rem auto;" class="no-borders centered pricing-post-table">
    <tr>
        <td style="width: 33%; background-color: #25AAE3; color: #fff; font-size: 130%;">Basic</td>
        <td>&nbsp;</td>
        <td style="width: 33%; background-color: #0b99d6; color: #fff; font-size: 130%;">Pro</td>
        <td>&nbsp;</td>
        <td style="width: 33%; background-color: #0684BA; color: #fff; font-size: 130%;">Premium</td>
    </tr>
    <tr>
        <td style="background-color:#f0f0f0;" rowspan="2"><span style="font-size:220%;">$19</span>/month</td>
        <td></td>
        <td style="background-color:#f0f0f0;"><span style="font-size:220%;">$59</span>/month</td>
        <td></td>
        <td style="background-color:#f0f0f0;"><span style="font-size:220%;">$159</span>/month</td>
    </tr>
    <tr>
        <td></td>
        <td style="background-color:#f0f0f0;color:#666;"><span style="font-size:90%;">$590</span>/year - 2 months free</td>
        <td></td>
        <td style="background-color:#f0f0f0;color:#666;"><span style="font-size:90%;">$1590</span>/year - 2 months free</td>
    </tr>
    <tr>
        <td style="padding: 15px 0; border: dotted 1px #ddd;border-top:none;"><strong>1</strong> private project</td>
        <td></td>
        <td style="padding: 15px 0; border: dotted 1px #ddd;border-top:none;"><strong>Unlimited</strong> private projects</td>
        <td></td>
        <td style="padding: 15px 0; border: dotted 1px #ddd;border-top:none;"><strong>Unlimited</strong> private projects</td>
    </tr>
    <tr>
        <td style="padding: 15px 0; border: dotted 1px #ddd;border-top:none;"><strong>1</strong> concurrent job</td>
        <td></td>
        <td style="padding: 15px 0; border: dotted 1px #ddd;border-top:none;"><strong>1</strong> concurrent job</td>
        <td></td>
        <td style="padding: 15px 0; border: dotted 1px #ddd;border-top:none;"><strong>3</strong> concurrent jobs</td>
    </tr>
    <tr>
        <td style="padding: 15px 0; border: dotted 1px #ddd;border-top:none;">-</td>
        <td></td>
        <td style="padding: 15px 0; border: dotted 1px #ddd;border-top:none;">Super-fast build environment</td>
        <td></td>
        <td style="padding: 15px 0; border: dotted 1px #ddd;border-top:none;">Super-fast build environment</td>
    </tr>
    <tr>
        <td style="padding: 15px 0; border: dotted 1px #ddd;border-top:none;">-</td>
        <td></td>
        <td style="padding: 15px 0; border: dotted 1px #ddd;border-top:none;">Instant build start</td>
        <td></td>
        <td style="padding: 15px 0; border: dotted 1px #ddd;border-top:none;">Instant build start</td>
    </tr>
    <tr>
        <td style="padding: 15px 0; border: dotted 1px #ddd;border-top:none;">Forums support</td>
        <td></td>
        <td style="padding: 15px 0; border: dotted 1px #ddd;border-top:none;">Priority technical support</td>
        <td></td>
        <td style="padding: 15px 0; border: dotted 1px #ddd;border-top:none;">Priority technical support</td>
    </tr>
</table>

All existing plans are honored.

If you are a student, educational organization or open-source project
looking for more calculation power or concurrent jobs we provide **50% discount** on all plans.


## AppVeyor on-premise

You may have noticed that we don’t have "Enterprise" plan anymore.
This is because "Enterprise" is reserved for AppVeyor on-premise edition
that will be available in January 2015!

If you are interested to be a beta tester just reply to this message and we’ll
add you to the "AppVeyor Enterprise early bird" mailing list.
We'll be publishing more information and roadmap for on-premise in the coming weeks.


## Updated website

AppVeyor has gathered many great open-source projects, such as Mono, Julia, Grunt, Redis, nodegit,
Chocolatey, JSON.net just to mention a few.
People contribute their priceless knowledge and experience on [AppVeyor forums](http://help.appveyor.com/discussions).

To more actively engage the community in shaping AppVeyor we decided to host our entire website
with documentation on GitHub where everyone could contribute by sending a pull request.

<p class="text-center">
    <a href="https://github.com/appveyor/website">https://github.com/appveyor/website</a>
</p>

Website runs on a new [Jekyll](http://jekyllrb.com/)-like engine (we called it NJekyll),
so you can grab it and use for your own website :)
