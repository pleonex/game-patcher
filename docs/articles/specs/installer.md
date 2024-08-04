# Open mod installer

The _open mod installer_ is a format used to distribute a specific mod of a
product, such as a video-game. It contains instructions on how to apply the mod.
The format is extensible and compatible for many platforms via _platform
extensions_.

The installer is not intended to distribute binary or text diffs. It describes
how to apply existing diff formats like VCDIFF (xdelta) and provide a container
for their distribution with metadata.

A mod installer is specific per version and set of features. Once published, it
should not change. Installers may be incremental, based on a previous mod. If
the installer is big, splitting its content by feature group should be
considered (e.g., distribute video patches in a separate installer).

## Format

> [!NOTE]  
> Probably we would drop the idea of a custom binary format and use a plain ZIP
> file with `.omi` extension.

The content is a custom binary format of a compressed container. The file name
should have the extension `.omi`.

| Offset | Format  | Description           |
| ------ | ------- | --------------------- |
| 0x00   | char[4] | Magic stamp: `OMI1`   |
| 0x04   | long    | Total file length     |
| 0x0C   | long    | Container data offset |

Container data:

| Offset | Format  | Description              |
| ------ | ------- | ------------------------ |
| 0x00   | char[4] | Section ID: `DATA`       |
| 0x04   | long    | Section length           |
| 0x08   | byte[]  | Compressed with ZIP data |

The container has a [manifest](#manifest) file with the installation information
and a set of resources with the text or binary diffs.

### Manifest

The manifest contains information of the mod, product compatibility and
installation instructions. It is inside the container in a file named
`manifest.json`. The format is a serialized JSON in
[minimized format](./manifest.md#minimized-json-format).

The format is divided in the following sections:

- Modding project information: copy of the modding
  [project information](./manifest.md#project-information), in case the
  installer is distributed separately.
- Mod information: information of the mod and compatible products with their
  formats. The patcher software must support those formats.
- List of resources: installation information to apply the mod on a product.
- Manifest signature: signed hash of the manifest to prevent modification of the
  mod content or information and malware distribution.

#### Project information

TODO...

#### Mod information

TODO...

#### Resources installation information

TODO...

#### Manifest signature

TODO...

### Resources

TODO...

## Example

For readability, the example is in YAML format. The structure representation is
compatible with JSON.

[Download link](./resources/example.omi.yml)

[!code-yaml[](./resources/example.omi.yml)]
