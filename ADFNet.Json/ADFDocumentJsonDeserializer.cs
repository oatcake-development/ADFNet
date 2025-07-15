// Project: ADFNet.Json
// File: ADFDocumentJsonDeserializer.cs

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

using System;
using System.Collections.Generic;
using System.Linq;
using ADFNet.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ADFNet.Json

{
    public static class ADFDocumentJsonDeserializer
    {
        public static ADFDocument FromJson(string json)
        {
            var jObject = JObject.Parse(json);
            return ParseNode(jObject) as ADFDocument
                   ?? throw new JsonException("Root JSON node is not a valid ADFDocument.");
        }

        private static ADFNode ParseNode(JObject obj)
        {
            var typeStr = obj["type"]?.ToString();
            if (!Enum.TryParse<NodeType>(typeStr, ignoreCase: true, out var type))
                return null;

            switch (type)
            {
                case NodeType.Document:
                    return ParseDocument(obj);
                case NodeType.Paragraph:
                    return ParseParagraph(obj);
                case NodeType.Text:
                    return ParseText(obj);
                case NodeType.HardBreak:
                    return new HardBreakNode();
                case NodeType.BulletList:
                    return ParseBulletList(obj);
                case NodeType.ListItem:
                    return ParseListItem(obj);
                default:
                    return null;
            }
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
            var marks = obj["marks"] as JArray ?? new JArray { };

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

        private static List<ADFNode> ParseChildren(JToken token)
        {
            var nodes = new List<ADFNode>();
            var array = token as JArray;
            if (array == null) return nodes;

            nodes.AddRange(array.OfType<JObject>().Select(ParseNode).Where(parsed => parsed != null));

            return nodes;
        }
    }
    
}