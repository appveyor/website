---
title: Converting a Mercurial repository to Git on Windows
---

Vast majority of AppVeyor customers uses Git, specifically GitHub. Historically, we've been using Mercurial for AppVeyor source control, but recently due to the growing popularity and ecosystem of Git and GitHub we thought maybe it's time to jump Git train and start using it for AppVeyor projects.

The first problem we faced with was converting existing Mercurial repositories to Git with preserving all history and authors. If you search in Google for <a href="https://www.google.ca/search?q=converting+mercurial+to+git+windows&amp;oq=converting+mercurial+to+git+windows">converting mercurial to git on windows</a> you will find some (mostly similar) posts on SO, but none of them worked for us if followed "as is" on Windows. After many trials we found the way that 100% works and could be easily reproduced - everything was done from scratch on a clean Windows Server 2012 machine.

This article specifically describes migration of project from BitBucket's Mercurial repository (<a href="https://bitbucket.org/appveyor/demoapp">original</a>) to GitHub (<a href="https://github.com/AppVeyor/DemoApp">migrated</a>) using <a href="http://repo.or.cz/w/fast-export.git"><strong>hg-fast-export</strong></a> tool.

## Install required software

We assume you start from a clean Windows Server 2012 machine.

* <a href="https://git-scm.com/">Git for Windows</a> (install the latest version, select "Run Git from the Windows Command Prompt" while installing Git)
* <a href="https://www.mercurial-scm.org/downloads">Mercurial 2.9 MSI installer - x86 Windows</a>
* <a href="https://www.python.org/downloads/">Python 2.7.6</a>
* add `c:\Python27` to `PATH`

Open command line prompt and make sure all tools are available in the `PATH`: hg, git, python.

## Migration

We will be doing everything in `c:\projects` directory.

Open command prompt.

```text
cd c:\projects
```

Clone source Mercurial repository:

```text
hg clone https://bitbucket.org/appveyor/demoapp
```

Create empty GitHub repository and clone it to `demoapp_git` directory:

```text
git clone https://github.com/AppVeyor/DemoApp.git demoapp_git
```

Clone `hg-fast-export` repository:

```text
git clone http://repo.or.cz/r/fast-export.git
```

Open `c:\projects\fast-export\hg-fast-export.py` in Notepad and replace highlighted region with the code below:

<img alt="hg-fast-export" src="/assets/images/posts/hg-to-git/hg-fast-export.png">

```python
#!/usr/bin/env python

# Copyright (c) 2007, 2008 Rocco Rutte <pdmef@gmx.net> and others.
# License: MIT <http://www.opensource.org/licenses/mit-license.php>

import sys

# import mercurial libraries from zip:
sys.path.append(r'C:\Program Files (x86)\Mercurial\library.zip')

from mercurial import node
from hg2git import setup_repo, fixup_user, get_branch, get_changeset
from hg2git import load_cache, save_cache, get_git_sha1, set_default_branch, set_origin_name
from optparse import OptionParser
import re
import os
```

Copy content of `fast-export` to `demoapp_git` ignoring .git folder.

Switch to `demoapp_git` directory:

```text
cd demoapp_git
```

If you need to map authors to new repository with different name/email create `authors.txt` with one mapping per line (old=new), like below:

```text
Feodor Fitsner <feodor@appveyor.com>=Feodor Fitsner <feodor@fitsner.com>
```

Run Python script to import Mercurial repo into Git one (you are running this script from Git repository directory):

```text
hg-fast-export.sh -r c:\projects\demoapp -A authors.txt
```

Checkout HEAD to check that everything looks good:

```text
git checkout HEAD
```

Remove conversion files:

```text
git clean -f
del /Q hg2git.pyc
```

Rename .hgignore to .gitignore:

```text
ren .hgignore .gitignore
git add .gitignore
git commit -m ".hgignore renamed to .gitignore"
```

Push Git repo to GitHub:

```text
git push -u origin master
```

Enjoy!
