---
title: The new build cache
---

AppVeyor runs every build on a clean virtual machine. Virtual machine state is not preserved between builds which means every build downloads sources,
installs NuGet packages, Node.js modules, Ruby gems or pulls dependencies.
*Build cache* allows you to preserve contents of selected directories and files between project builds.

AppVeyor was the first hosted CI to introduce a build cache and over the time it became a very popular build tool and important infrastructure component.
There were some limitations though, such as maximum cache entry size of 500 MB and intermittent save/restore lags due to ever changing networking conditions.

Increasing requirements from larger customers running builds with "heavy" dependencies made us to re-visit cache architecture and thus the new and updated build cache was born!
The new build cache lives close to build worker VMs, it's fast and offers virtually unlimited possibilities for scale and has lower update/restore times.

## Cache size

With the introduction of the new cache we are also changing the way it's metered.

The total size of build cache is limited per account and depends on the plan:

<table class="centered">
<tr>
    <th>Free</th>
    <th>Basic</th>
    <th>Pro</th>
    <th>Premium</th>
</tr>
<tr>
    <td>1 GB</td>
    <td>1 GB</td>
    <td>5 GB</td>
    <td>20 GB</td>
</tr>
</table>

It's a hard quota which means the build will fail while trying to upload cache item exceeding the quota.
The maximum size of a single cache entry cannot be larger than the size of cache.

## Cache speed vs size

The new cache uses `7z` to compress/uncompress files before transferring them to the cache storage.
We chose `7z` over built-in .NET compression library because it's generally faster, produces smaller archives and works with hidden files out-of-the-box.

While compressing cache item, by default AppVeyor uses `7z` with `zip` algorithm and compression level `1` ("Fastest") thus producing archive faster, but with larger size (`-tzip -mx=1` args).
However, you can change compression behavior of `7z` by providing your own command line args in `APPVEYOR_CACHE_ENTRY_ZIP_ARGS` environment variable.
For example, to enable `LZMA` compression method with the highest possible compression ratio set this variable to `-t7z -m0=lzma -mx=9`.

## Availability

The new build cache is currently in beta. It's automatically enabled for all new accounts.

We are rolling out new cache to existing accounts in batches while observing performance.
If you want participate in beta sooner or noticed any issues with the build cache please
[let us know](mailto:team@appveyor.com).

Best regards,<br>
AppVeyor team

Follow us on Twitter: [@appveyor](https://twitter.com/appveyor)
