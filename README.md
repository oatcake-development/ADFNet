# ADFNet

ADFNet is a modern .NET library for working with [Atlassian Document Format (ADF)](https://developer.atlassian.com/cloud/jira/platform/apis/document/structure/). It provides a simple, extensible object model and tooling to:

- Parse ADF JSON into strongly-typed .NET objects
- Serialize .NET objects back to ADF-compliant JSON
- Export ADF documents to other formats such as Org Mode
- Provide extensibility points to allow contributions to export ADF to other formats e.g. Markdown, HTML
- Construct ADF documents programmatically in code

## ✅ MVP Features

- ✅ ADF → Org Mode exporter
- ✅ ADF → .NET object deserialization
- ✅ .NET object → ADF JSON serialization
- ✅ Support for core ADF nodes:
    - `text`, `paragraph`, `hardBreak`, `bulletList`, `listItem`
- ✅ Support for inline formatting:
    - `bold`, `italic`, `underline`, `strike`, `code`
- ✅ Fully unit tested with NUnit
- ✅ NuGet-ready build process using Make

## ✨ Getting Started

Install via NuGet (coming soon):

```bash
dotnet add package ADFNet
```
Use it in your application:

```csharp
using ADFNet.Json;
using ADFNet.OrgMode.Exporters;

// Parse JSON
var document = ADFDocumentJsonDeserializer.FromJson(json);

// Export to Org-mode
var exporter = new OrgModeExporter();
string orgOutput = exporter.Export(document);
```

## 🧩 Project Structure
| Project                | Description                                       |
|------------------------|---------------------------------------------------|
| `ADFNet.Core`          | Core data structures (ADFNode, ADFDocument, etc.) |
| `ADFNet.Core.Tests`    | Unit tests for the core and exporter features     |
| `ADFNet.OrgMode`       | Org-mode exporter implementation                  |
| `ADFNet.OrgMode.Tests` | Org-mode exporter tests                           |
| `ADFNet.Json`          | JSON (de)serialisation for ADF                    |
| `ADFNet.Json.Tests`    | Unit tests for ADFNet.Json                        |


## 🚧 Roadmap
Future milestones include:

* Markdown exporter
* Validation layer for ADF document structure
* Round-trip consistency testing
* HTML export (stretch goal)

## 🛠 Development
If you have make installed on your system there is a set of make commands you can run to support the development process.

This is merely a helper on top of the relevant `dotnet` tooling - but I find it helps smooth the experience.

```bash
make build       # Build all projects
make test        # Run all tests
make package     # Create your local NuGet package
make clean       # Run dotnet clean and clean output folder
```

## MVP Goals
* [x] Deserialize ADF JSON to internal C# document model
* [x] Export to Org-mode
* [x] Re-serialized model back to valid ADF JSON
* [ ] Markdown export (stretch)
* [ ] Domain-Specific Language builder for ADF (stretch)

