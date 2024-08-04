# Modding project manifest

The _modding project manifest_ is a file describing a set of mods for product
such as a video-game. The project may target more than one product (e.g.,
different regions or platforms). There may be more than one mod offering an
alternative set of features (e.g., dubbed, difficulty). The mods under the same
manifest share a unique goal, e.g. a translation or a specific hack.

Software listing mods (mod stores) may use this format to store or download an
offline copy of the information of individual projects.

## Format

The file format is a serialized JSON in
[minimized format](#minimized-json-format). The file name should have the
extension `.modproj` or `.mdp`.

The format is divided in the following sections:

- Project general information
- Description of the target products.
- List of mods.
- Manifest file signature.

### Project information

TODO...

### Product description

TODO...

### Mod description

TODO...

### Catalog signature

TODO...

### Minimized JSON format

The minimized JSON format save spaces on disk and bandwidth, essential for
preservation of large quantities of projects. This is also the format to
calculate and validate the document signature.

The rules of minimization are:

- The `signature` section must not be in the document when calculating the
  signature.
- Every JSON object properties (key/values) must appear in alphabetically order
  - The order of array is preserved.
  - Numbers do not have leading or trailing zeroes.
  - Strings only escape characters required by the JSON specification: backslash
    (`\`), double quotes (`“`) and control characters (code points below
    U+0020).
    - The hexadecimal digits in escaping controls must be in upper case.
  - Unnecessary whitespace and new lines must be removed.

## Mod packages

The manifest file may be distributed along with their referenced mods in ZIP
format. In this case the manifest file must be at the root of the zip file with
the filename `manifest.json`. The package should have the extension `.mpk`

## Example

For readability, the example is in YAML format. The structure representation is
compatible with JSON.

[Download link](./resources/example.mdp.yml)

[!code-yaml[](./resources/example.mdp.yml)]
