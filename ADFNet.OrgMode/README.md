# ADFNet.OrgMode

Org-mode exporter for documents represented using ADFNet.Core.

This package converts ADF document models into readable [Emacs Org-mode](https://orgmode.org/) text.

## Installation

```bash
dotnet add package ADFNet.OrgMode
```

## Usage
```csharp
var exporter = new OrgModeExporter();
string orgText = exporter.Export(adfDocument);
```
Supports:
- Paragraphs
- Text formatting (bold, italic, underline, code, etc.)
- Bullet lists and list items
- Hard breaks

## License
[Apache-2.0](https://www.apache.org/licenses/LICENSE-2.0)