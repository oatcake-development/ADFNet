/*
 * Copyright 2025 Lee Williams
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using ADFNet.Core.Models;
using Newtonsoft.Json.Linq;

namespace ADFNet.Json;

public static class ADFDocumentJsonSerializer
{
    public static string ToJson(ADFDocument doc)
    {
        var jObject = SerializeNode(doc) as JObject
                      ?? throw new InvalidOperationException("Failed to serialize ADFDocument.");
        return jObject.ToString();
    }

    private static JToken? SerializeNode(ADFNode node)
    {
        return node.Type switch
        {
            NodeType.Document => new JObject
            {
                ["type"] = "document",
                ["version"] = 1,
                ["content"] = new JArray(node.As<ADFDocument>().Content.Select(SerializeNode))
            },

            NodeType.Paragraph => new JObject
            {
                ["type"] = "paragraph",
                ["content"] = new JArray(node.As<ParagraphNode>().Content.Select(SerializeNode))
            },

            NodeType.Text => SerializeTextNode(node.As<TextNode>()),

            NodeType.HardBreak => new JObject { ["type"] = "hardBreak" },

            NodeType.BulletList => new JObject
            {
                ["type"] = "bulletList",
                ["content"] = new JArray(node.As<BulletListNode>().Items.Select(SerializeNode))
            },

            NodeType.ListItem => new JObject
            {
                ["type"] = "listItem",
                ["content"] = new JArray(node.As<ListItemNode>().Content.Select(SerializeNode))
            },

            _ => null
        };
    }

    private static JObject SerializeTextNode(TextNode node)
    {
        var marks = new List<JObject>();

        if (node.Bold) marks.Add(new JObject { ["type"] = "bold" });
        if (node.Italic) marks.Add(new JObject { ["type"] = "italic" });
        if (node.Underline) marks.Add(new JObject { ["type"] = "underline" });
        if (node.Strike) marks.Add(new JObject { ["type"] = "strike" });
        if (node.Code) marks.Add(new JObject { ["type"] = "code" });

        var jText = new JObject
        {
            ["type"] = "text",
            ["text"] = node.Text
        };

        if (marks.Count > 0)
            jText["marks"] = new JArray(marks);

        return jText;
    }

    // Extension for safe casting
    private static T As<T>(this ADFNode node) where T : ADFNode =>
        node as T ?? throw new InvalidCastException($"Node is not of type {typeof(T).Name}");
}
