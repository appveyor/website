---
layout: docs
title: Publishing to NuGet feed
---

# Publishing to NuGet feed

NuGet deployment provider publishes artifacts of type **NuGet package** to remote NuGet feed.

## Provider settings

* **NuGet server URL** (`server`) - NuGet feed URL, e.g. https://nugetserver.com/nuget/feed. If server is not specified package will be pushed to NuGet.org.
* **API key** (`api_key`) - your API key
* **Symbol server URL** (`symbol_server`) - Publishing URL for symbol server, If server is not specified symbol package will be pushed to SymbolSource.org.
* **Do not publish symbol packages** (`skip_symbols`) - skip publishing of symbol packages.
* **Artifact(s)** (`artifact`) - artifact name or filename to push. If not specified all artifacts of type **NuGet package** will be pushed. This can be a regexp, e.g. `/.*\.nupkg/`

Configuring in `appveyor.yml`:

    deploy:
      provider: NuGet
      server:                  # remove to push to NuGet.org
      api_key:
        secure: m49OJ7+Jdt9an3jPcTukHA==
      skip_symbols: false
      symbol_server:           # remove to push symbols to SymbolSource.org
      artifact: /.*\.nupkg/

Your NuGet API key should be encrypted using this tool: https://ci.appveyor.com/tools/encrypt.
