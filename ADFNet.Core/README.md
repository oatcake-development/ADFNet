# ADFNet.Core

Core data structures for working with Atlassian Document Format (ADF) in .NET.

This library defines the abstract syntax tree (AST) models used to represent ADF content, including:

- `ADFDocument`
- `ParagraphNode`
- `TextNode` with formatting options (bold, italic, etc.)
- List structures, hard breaks, and more

## Installation

```bash
dotnet add package ADFNet.Core
```
## Usage
```csharp
var doc = new ADFDocument
{
    Content = new List<ADFNode>
    {
        new ParagraphNode
        {
            Content = new List<ADFNode>
            {
                new TextNode { Text = "Hello, ADF!", Bold = true }
            }
        }
    }
};
```

## License
[Apache-2.0](https://www.apache.org/licenses/LICENSE-2.0)