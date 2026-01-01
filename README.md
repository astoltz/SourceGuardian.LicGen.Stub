# SourceGuardian License Generator Stub

This is a mock implementation of the SourceGuardian `licgen` CLI tool. It is intended for use in unit testing and development environments where the actual SourceGuardian binary is not available or licensing generation needs to be simulated without side effects.

This tool mimics the command-line interface of the real `licgen` tool, accepting the same arguments and flags. Instead of generating a real encrypted license file, it produces a readable text file containing a dump of the provided arguments and any included text content.

## Usage

The CLI accepts standard SourceGuardian arguments:

```bash
licgen [options] output.lic
```

### Supported Options

- `--expire <dd/mm/yyyy>` or `<time>`: Set expiration.
- `--domain <domain>`: Bind to domain.
- `--ip <ip>`: Bind to IP.
- `--mac <mac>`: Bind to MAC address.
- `--projid <value>`: Set project ID.
- `--projkey <value>`: Set project key.
- `--const name=value`: Set custom constants.
- `--text "text"|@file`: Add plain text to the license.
- `-v`: Display version.
- `-h`: Display help.

## Example Output

When running the tool, it generates a file (e.g., `output.lic`) with content similar to:

```text
--- SourceGuardian License Stub ---
Generated: 01/01/2026 15:03:34
Output File: /tmp/licj7ubcb85ghovckV0qd1

--- Arguments ---
--projid: 11df7aa3d2d0cea5203c621f39f75dd9ea35fb4cfec34098aa67d8f3fe124500
--projkey: 275fabdb439b735e591555a8c7d817aaa35b03716655754d79eeb95c01ad38b5
--const: "VERSION=1.0"
--const: "EDITION=Developer"
--const: "CUSTOMER_NAME=Acme Corp"
--text: @/tmp/txtk9hg6rsup3kfcsgAENF
  >>> Content of /tmp/txtk9hg6rsup3kfcsgAENF:
PHP Standalone Sample
Developer Edition
Version 1.0
Licensed to: Acme Corp
  <<< End Content
```

## Related Projects

This stub is used to test the integration in:
[https://github.com/astoltz/sourceguardian-licenser](https://github.com/astoltz/sourceguardian-licenser)

## Disclaimer

**MOCK VERSION - NOT REAL SOURCEGUARDIAN**
This software is not affiliated with SourceGuardian Ltd. It is a testing utility.
