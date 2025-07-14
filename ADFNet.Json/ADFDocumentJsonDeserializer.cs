// Project: ADFNet.Json
// File: ADFDocumentJsonDeserializer.cs

using ADFNet.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ADFNet.Json;

public static class ADFDocumentJsonDeserializer
{
    public static ADFDocument FromJson(string json)
    {
        var jObject = JObject.Parse(json);
        return ParseNode(jObject) as ADFDocument
               ?? throw new JsonException("Root JSON node is not a valid ADFDocument.");
    }

    private static ADFNode? ParseNode(JObject obj)
    {
        var typeStr = obj["type"]?.ToString();
        if (!Enum.TryParse<NodeType>(typeStr, ignoreCase: true, out var type))
            return null;

        return type switch
        {
            NodeType.Document => ParseDocument(obj),
            NodeType.Paragraph => ParseParagraph(obj),
            NodeType.Text => ParseText(obj),
            NodeType.HardBreak => new HardBreakNode(),
            NodeType.BulletList => ParseBulletList(obj),
            NodeType.ListItem => ParseListItem(obj),
            _ => null
        };
    }

    private static ADFDocument ParseDocument(JObject obj)
    {
        var content = ParseChildren(obj["content"]);
        return new ADFDocument { Content = content };
    }

    private static ParagraphNode ParseParagraph(JObject obj)
    {
        var content = ParseChildren(obj["content"]);
        return new ParagraphNode { Content = content };
    }

    private static TextNode ParseText(JObject obj)
    {
        var text = obj["text"]?.ToString() ?? string.Empty;
        var marks = obj["marks"] as JArray ?? [];

        var node = new TextNode { Text = text };
        foreach (var mark in marks.OfType<JObject>())
        {
            var markType = mark["type"]?.ToString();
            switch (markType)
            {
                case "bold": node.Bold = true; break;
                case "italic": node.Italic = true; break;
                case "underline": node.Underline = true; break;
                case "strike": node.Strike = true; break;
                case "code": node.Code = true; break;
            }
        }
        return node;
    }

    private static BulletListNode ParseBulletList(JObject obj)
    {
        var items = ParseChildren(obj["content"]).OfType<ListItemNode>().ToList();
        return new BulletListNode { Items = items };
    }

    private static ListItemNode ParseListItem(JObject obj)
    {
        var content = ParseChildren(obj["content"]);
        return new ListItemNode { Content = content };
    }

    private static List<ADFNode> ParseChildren(JToken? token)
    {
        var nodes = new List<ADFNode>();
        if (token is not JArray array) return nodes;

        foreach (var child in array.OfType<JObject>())
        {
            var parsed = ParseNode(child);
            if (parsed != null) nodes.Add(parsed);
        }

        return nodes;
    }
}
