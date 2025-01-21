# Specification overview

This project introduces a standard framework to distribute mods of a software
product such as a video-game.

At this moment the distribution of mods is mostly based on publishing a file
with a binary diff and a _readme_ file describing the installation steps and
supported software. Some groups that are lucky to have a developer may be able
to create a _patcher_, a specific software to apply the mod on the game.

The main goal is to provide a common solution for distributing and installing
mods, from any platform, to any platform. This is achieved by introducing two
main new formats:

- [**Modding project manifest**](./manifest.md) (`.modproj`, `.mdp`): text file
  describing a modding project. It contains information of the game and lists
  the different mod versions available of the project.
- [**Mod installer eXtensible**](./installer.md) (`.mix`): container with the
  resources of the mod and a text file describing the automatic installation
  process.

![Formats overview](./resources/formats_overview.drawio.png)
