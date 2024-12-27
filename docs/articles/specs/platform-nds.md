# Platform extension: Nintendo DS

This document describes the extension points to support the _Nintendo DS_
platform in the project and mod installer formats.

## Verification methods

### `ds-gameid`

Full game ID: game code + maker + version.

### `dsi-dec-rsa`

It doesn't require any key when comparing against the decrypted version of the
RSA signature. As the project manifest should be signed, this field is protected
against later modifications.

## Input formats

### `ds-rom`

DS header (including extended ROM format after DSi release)

- File system URI: `nds://`
- Deployment arguments: none.

### `dsi-rom`

DSi header, banner and armi

- File system URI: `dsi://`
- Deployment arguments:
  - `regenerate_hashes` (`bool`): requires keys.

## Installation methods

Generic supported:

- `xdelta`

### `ds-romheader`

- Content: yaml.
- Parameters: none.

### `armips`

- Content: assembly text file
- Parameters:
  - target: path to patch
  - parameters: key value script parameters `name` / `value`
