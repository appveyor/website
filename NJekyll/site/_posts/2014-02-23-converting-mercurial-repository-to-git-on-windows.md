---
layout: post
title: Converting a Mercurial repository to Git on Windows
---

Vast majority of AppVeyor customers uses Git, specifically GitHub. Historically, we've been using Mercurial for AppVeyor source control, but recently due to the growing popularity and ecosystem of Git and GitHub we thought maybe it's time to jump Git train and start using it for AppVeyor projects.

The first problem we faced with was converting existing Mercurial repositories to Git with preserving all history and authors. If you search in Google for <a href="https://www.google.ca/search?q=converting+mercurial+to+git+windows&amp;oq=converting+mercurial+to+git+windows">converting mercurial to git on windows</a> you will find some (mostly similar) posts on SO, but none of them worked for us if followed "as is" on Windows. After many trials we found the way that 100% works and could be easily reproduced - everything was done from scratch on a clean Windows Server 2012 machine.

This article specifically describes migration of project from BitBucket's Mercurial repository (<a href="https://bitbucket.org/appveyor/demoapp">original</a>) to GitHub (<a href="https://github.com/AppVeyor/DemoApp">migrated</a>) using <a href="http://repo.or.cz/w/fast-export.git"><strong>hg-fast-export</strong></a> tool.
<h2>Install required software</h2>
We assume you start from a clean Windows Server 2012 machine.
<ul>
    <li><a href="http://git-scm.com/">Git for Windows</a> (install the latest version, select "Run Git from the Windows Command Prompt" while installing Git)</li>
    <li><a href="http://mercurial.selenic.com/downloads/">Mercurial 2.9 MSI installer - x86 Windows</a></li>
    <li><a href="http://www.python.org/downloads/">Python 2.7.6</a></li>
    <li>add <code>c:\Python27</code> to <code>PATH</code></li>
</ul>
<span style="font-style:inherit;line-height:1.625;">Open command line prompt and make sure all tools are available in the <code>PATH</code>: </span>hg, git, python.
<h2>Migration</h2>
We will be doing everything in <code>c:\projects</code> directory.

Open command prompt.
<pre>cd c:\projects</pre>
Clone source Mercurial repository:
<pre>hg clone https://bitbucket.org/appveyor/demoapp</pre>
Create empty GitHub repository and clone it to <code>demoapp_git</code> directory:
<pre>git clone https://github.com/AppVeyor/DemoApp.git demoapp_git</pre>
Clone <code>hg-fast-export</code> repository:
<pre>git clone http://repo.or.cz/r/fast-export.git</pre>
Open <code>c:\projects\fast-export\hg-fast-export.py</code> in Notepad and replace highlighted region with the code below:

<img alt="hg-fast-export" src="/site/_posts/images/hg-to-git/hg-fast-export.png">
<pre>#!/usr/bin/env python

# Copyright (c) 2007, 2008 Rocco Rutte &lt;pdmef@gmx.net&gt; and others.
# License: MIT &lt;http://www.opensource.org/licenses/mit-license.php&gt;

import sys

# import mercurial libraries from zip:
sys.path.append(r'C:\Program Files (x86)\Mercurial\library.zip')

from mercurial import node
from hg2git import setup_repo,fixup_user,get_branch,get_changeset
from hg2git import load_cache,save_cache,get_git_sha1,set_default_branch,set_origin_name
from optparse import OptionParser
import re
import os</pre>
Copy content of <code>fast-export</code> to <code>demoapp_git</code> ignoring .git folder.

Switch to <code>demoapp_git</code> directory:
<pre>cd demoapp_git</pre>
If you need to map authors to new repository with different name/email create <code>authors.txt</code> with one mapping per line (old=new), like below:
<pre>Feodor Fitsner &lt;feodor@appveyor.com&gt;=Feodor Fitsner &lt;feodor@fitsner.com&gt;</pre>
<span style="font-style:inherit;line-height:1.625;">Run Python script to import Mercurial repo into Git one (you are running this script from Git repository directory):</span>
<pre>hg-fast-export.sh -r c:\projects\demoapp -A authors.txt</pre>
Checkout HEAD to check that everything looks good:
<pre>git checkout HEAD</pre>
Remove conversion files:
<pre>git clean -f
del /Q hg2git.pyc</pre>
Rename .hgignore to .gitignore:
<pre><span style="font-style:inherit;line-height:1.625;">ren .hgignore .gitignore
</span>git add .gitignore
git commit -m ".hgignore renamed to .gitignore"</pre>
Push Git repo to GitHub:
<pre>git push -u origin master</pre>
Enjoy!