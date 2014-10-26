---
title: AppVeyor Deployment options
layout: docs
categories: [deploy, kb]
---

## Deployment options

title: {{ page.title }}


{% for item in site.data.testimonials -%}
  {{ item.customer }}
    {% for link in item.links -%} {{ link }} {% endfor -%}
{% endfor -%}

title: {{site.page.title}}
permalink: {{site.page.permalink}}


content: {{site.page.content}}

{% for kbPage in site.categories.kb -%}
  kb page title: {{ kbPage.title }}
{% endfor -%}