# Product mod catalog

The _product mod catalog_ is a file describing a set of mods available for a
product such as a videogame. It contains detailed information of the product,
but general information of the mods. Website listing modding projects may use
this format to store or download an offline copy of the information of those
projects.

The file format is a serialized JSON in minimized format. The file name should
have the extension `.pmc`.

> [!NOTE]  
> The catalog file may not list all available mods of the given product. It may
> be distributed by individuals or groups to list their different mods on a
> specific product. There may be multiple catalogs for the same product on a
> _mod store_.

## Format

The format is divided in the following sections:

- Description of the target product.
- List of mods for the product.
- Catalog file signature.

### Product description

TODO... multiple products?

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

The catalog file may be distributed along with their referenced mods in ZIP
format. In this case the catalog file must be at the root of the zip file with
the filename `catalog.json`. The package should have the extension `.mpk`

## Example

For readability pourposes the example is in YAML format. This representation is
compatible with JSON.

```yaml
product:
  id: "PSL_EU_VPYP01_00"
  name: "Pokémon Conquest"
  company: "Tecmo Koei"
  publisher: "Nintendo / The Pokémon Company"
  release_date: "17.03.2012"
  region: "Europe"
  genre:
    - "RPG"
  platform: "NDS"
  description:
    es_ES: >
      Pokémon Conquest para Nintendo DS es un spin-off de la saga Pokémon que
      ofrece una nueva modalidad de juego. Se desarrolla en un mundo feudal
      japonés llamado Ransei donde también habitan los Pokémon.
  linked_products:
    regions:
      - id: "PSL_VPYJ2P_00"
        name: "ポケモン＋ノブナガの野望 (Pokémon + Nobunaga's Ambition)"
        region: "Japan"
      - id: "PSL_E_VPYT01_00"
        name: "Pokémon Conquest"
        region: "USA"
    saga:
  buy_info:
    - store: "Physical"
      discontinued: true
      link:
      comments:

mods:
  - id: "spanish_v1.0"
    version: "1.0.0"
    name: "Traducción al español"
    description: >
      Traducción al español de España. Incluye todos los textos, imágenes y
      fuentes. Incluye soporte de los misiones descargables con servidor WFC
      propio. El único elemento sin traduccir es el vídeo de presentación.
    compatibility:
      - product_id: "PSL_EU_VPYP01_00"
        variant_name: "Original dump region Europe"
        verification_method: "dsi-dec-rsa"
        hash: "lB8RA+0gvnHT50wmPQp3j7VRG9VnVoqDYp7jmVhYf/FmthzG+D2yr0iNcob1FHenKqWyTcKAOAJgDjDN8dBg1flvZW8M5ovG6c49/Yuqsepd1qaNvxOm1LuAgWsj4WqG7XuPGx5h6MPRFp01nIr4Ko7Y5utWO/HVtTu4R6E/DCo="
      - product_id: "PSL_E_VPYT01_00"
        variant_name: "Original dump region USA"
        verification_method: "file-sha256"
        hash: "Zc47YEF9Y61gxQvVMBzdkcI1GE7/tbz/Lahn71P4Qu8="
      - product_id: "PSL_EU_VPYS01_01"
        variant_name: "Spanish beta patch v0.1"
        verification_method: "ds-gameid"
        hash: "PSL_EU_VPYS01_01"
    target_language: "es_ES"
    mod_content:
      href: "content://spanish_v1.0.omi" # also https://
      format: "omi-v1"
      hash_algorithm: "sha256"
      hash_value: "5dbrSZngg8H9hw9OcHV3sMOnb62ORcY8CtkIdvfQZPc="
    valid_until: "01-01-2024" # from this date the patcher refuses to apply it. Used for betas / prevent leaks.
    encryption:
      # Key that encrypts the mod file and the file encryption algorithm.
      # This key is the same for everyone but it's encrypted here with the user-key
      # Files encrypted in the backend storage with the key.
      content_key:
        algorithm: "aes256-cbc"
        key: "mk5qdYn7RDN2DoyZTm+HGfKvIh9HLwsc3iiv91y7PWDHho9ndRpbaMpUN4gRN2yTK4W6ptJghKqAlqScKOv+VA=="
      # This key changes on each download of this catalog file, it's per user.
      # It's generated from a passphrase only the user knows. The key check verifies the validity of the key,
      # by decrypting its content which should match this mods ID.
      user_key:
        generation_algorithm: "sha256"
        key_check: "zGBhE1X6bXPtUL1n9Yh81AVDqdlAFx6FeUeMbanPYGz3C01WMyHGHniAKZYcZGFA/9B08i+AE23CrROH1nkkwg=="
        hint: "Your account password in TraduSquare"

# Signature prevents malware distribution by verifying this file was not modified and comes from a trusted source
signature:
  algorithm: "rsa-sha256"
  certificate: "" # apps contains the root certificate and must verify this one is trusted
  value: ""
```
