# ADFNet.Json

Provides JSON serialisation and deserialisation for Atlassian Document Format (ADF) using [ADFNet.Core](https://www.nuget.org/packages/ADFNet.Core).

This library allows conversion between the ADF JSON structure and the ADFNet C# model.

## Installation

```bash
dotnet add package ADFNet.Json
```

## Usage
### Deserialize from JSON
```csharp
var adf = ADFDocumentJsonDeserializer.FromJson(json);
```
### Serialize to JSON
```csharp
var json = ADFDocumentJsonSerializer.ToJson(adfDocument);
```

## License
[Apache-2.0](https://www.apache.org/licenses/LICENSE-2.0)