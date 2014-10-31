---
title: AppVeyor scheduled upgrade to v2.0
---
Dear customer,

We want to notify you of an upcoming AppVeyor upgrade to version 2.0.
The update will occur on **Friday, March 7 at 10:00 am PST** (March 7 at 6:00 pm UTC).

Upgrade includes three stages and will take approximately 2 hours.


## Stage 1 - Upgrade AppVeyor deployment to 2.0

New VM instances must be deployed for AppVeyor 2.0, so there will be service
interruption during that time for about 10-15 minutes.


## Stage 2 - Projects configuration migration

After AppVeyor installation is updated to 2.0 we are going to run migration scripts
to convert projects configuration data to a new format. This stage takes
approximately 5 minutes and AppVeyor is ready to run new builds upon its completion.
However, you won’t see the history of your projects yet.


## Stage 3 - Projects history migration

Projects history migration takes about 40 minutes and during that time you will start
seeing history gradually appearing under your projects.

We hope you will like AppVeyor 2.0! In case you missed our last newsletter announcing
it you can read what’s new on this page: [{{site.url}}/beta]({{site.url}}/beta)

We apologize in advance for any inconveniences caused by this update.
Please let us know if you have any questions or concerns.

Best regards,<br/>
AppVeyor team