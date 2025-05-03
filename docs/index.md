# Multi-platform Open eXtensible Mod Installer

> [!CAUTION]  
> This project is still in a very early phase. It doesn't have any deliverables
> yet. No guarantees it will be ever finished.

Multi-platform framework for distributing and installing mods of a software
product such as a video-game.

- 📚 Specification of formats for distributing modding projects and installers.
- ♻️ Multi-platform mod distribution.
- 🔌 Extensible format.
- 🔒 Security features.
- 💻 Software libraries and tools to create and apply the mods.

## Projects

![C4 Level-2 diagram showing blocks for the below projects](./images/design_l2.drawio.png)

- [_Mod format standard_](./articles/specs/overview.md): standard multi-platform
  mod installation format.

- _Mod builder_: cross-platform application that helps mod teams to create a
  distributable package for their mod (`.mdp` and `.mix` files).

- [_Mod installer_](./articles/installer/overview.md): multi-platform that
  installs compatible mod packages.

- _Framework libraries_: programming libraries that provide support for the mod
  format, creating a new package and installing it. Extended for each supported
  platform.

- _Mod store_: OpenAPI specification that mod catalog webs could implement.
  Desktop / mobile applications would be able to navigate and download mods from
  different compatible webs.

## Supported target platforms

Platform support status:

| Platform | Specification | Installer | Builder |
| -------- | ------------- | --------- | ------- |
| Generic  | 🚧            | ❌        | ❌      |
| NDS      | 🚧            | ❌        | ❌      |
| DSi      | 🚧            | ❌        | ❌      |
