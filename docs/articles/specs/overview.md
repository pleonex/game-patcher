# Specification overview

TODO... description of mod projects, mod packages and installers, image

## Goals

This section describes the goals of the specification for the modding projects
and installer formats.

### Multi-platform mods

TODO...

> [!NOTE]  
> Some platforms have advance requirements where a simple patching mechanism
> would not work. This may be the case of modern PC games. For those case, OMI
> offers a way to distribute the mods, to then use specialized software like
> [Nexus](https://nexus-mods.github.io/NexusMods.App/) for its installation.

### Multi-platform applications

TODO: apps that can apply mods from android for emu

### Preservation of modding projects

TODO: all-in-one package

### Publishing across mod stores

TODO...

### Ease publication of modding projects

TODO... no-code patchers, advance configuraiton

### Improve user experience installing mods

TODO...different regions, platforms (simple ports), one-click installation, no
multiple translations as different project, autodetect valid versions, advance
configuration

### Save bandwidth and storage

TODO... compression, incremental patching, shared xdeltas across versions /
regions (shared videos, change only arm9)

### Prevent unfair mods re-publishing

TODO... Youtube videos with ads, encrypted with personal info password/ip,
trusted store and app, signed content

### Prevent leaks

TODO... leaks of beta testing versions, expiration date, trusted app / store

### Prevent stealing projects by script-kiddos

TODO...binary format, encrypted, above point

### Prevent malware distribution

TODO... signed, exe, dlls, hooks, hashes, no scripting

## Out of scope

- Definition of formats to store binary differences
  - This project uses existing formats such as VCDIFF (xdelta).
- Scripting: run third-party code, scripts or programs as part of the mod
  installation.
  - Increase the complexity of the standard and software solution.
  - Introduce security risks
  - Recommendation: create custom mod patchers based on the OMI libraries that
    runs any additional step after / before applying the mod.
- Hooks: attaching custom code / scripts to a running process or library.
  - Used for some PC mods.
  - Recommendation: install as part of the mod specialized software, for
    instance, based on [Harmony](https://github.com/pardeike/Harmony) or
    [Reloaded-II](https://github.com/Reloaded-Project/Reloaded-II)
- Memory patches: modify the running game memory
  - Used by Riivolution and cheat engines
- Patch triggers: apply or re-apply a mod after a trigger like before running a
  game or when a file changes.
  - Used for
    [some PC mods](https://nexus-mods.github.io/NexusMods.App/developers/concepts/0002-datamodel-triggers/).
  - Recommendation: use specialized software may be required.

## References

Other cool projects have similar goals but on more specific use cases or
platforms:

- [Nexus project](https://nexus-mods.github.io/NexusMods.App/developers/ModWithConfidence/)
- [FOMOD format](https://fomod-docs.readthedocs.io/en/latest/specs.html)
  - More info
    [by NexusMods](https://nexus-mods.github.io/NexusMods.App/developers/misc/AboutFomod/)
- [Riivolution](https://aerialx.github.io/rvlution.net/wiki/Patch_Format/)
- [SMAPI](https://github.com/Pathoschild/SMAPI)
- [Mara NX](https://github.com/D3fau4/Mara_nx)
- [Mara](https://github.com/tradusquare/mara)
