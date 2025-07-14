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
using ADFNet.Json;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ADFNet.Json.Tests;

[TestFixture]
public class ADFDocumentJsonDeserializerTests
{
    [Test]
    public void FromJson_ValidDocumentWithParagraphAndText_ReturnsADFDocument()
    {
        var json = """
        {
          "type": "document",
          "content": [
            {
              "type": "paragraph",
              "content": [
                {
                  "type": "text",
                  "text": "Hello ADF",
                  "marks": [
                    { "type": "bold" },
                    { "type": "italic" }
                  ]
                }
              ]
            }
          ]
        }
        """;

        var result = ADFDocumentJsonDeserializer.FromJson(json);

        Assert.That(result, Is.TypeOf<ADFDocument>());
        Assert.That(result.Content, Has.Count.EqualTo(1));
        var paragraph = result.Content[0] as ParagraphNode;
        Assert.That(paragraph, Is.Not.Null);
        Assert.That(paragraph!.Content[0], Is.TypeOf<TextNode>());

        var textNode = (TextNode)paragraph.Content[0];
        Assert.That(textNode.Text, Is.EqualTo("Hello ADF"));
        Assert.That(textNode.Bold, Is.True);
        Assert.That(textNode.Italic, Is.True);
    }

    [Test]
    public void FromJson_HandlesHardBreak()
    {
        var json = """
        {
          "type": "document",
          "content": [
            {
              "type": "paragraph",
              "content": [
                { "type": "text", "text": "Line 1" },
                { "type": "hardBreak" },
                { "type": "text", "text": "Line 2" }
              ]
            }
          ]
        }
        """;

        var doc = ADFDocumentJsonDeserializer.FromJson(json);

        var paragraph = doc.Content[0] as ParagraphNode;
        Assert.That(paragraph!.Content, Has.Count.EqualTo(3));
        Assert.That(paragraph.Content[1], Is.TypeOf<HardBreakNode>());
    }

    [Test]
    public void FromJson_HandlesBulletList()
    {
        var json = """
        {
          "type": "document",
          "content": [
            {
              "type": "bulletList",
              "content": [
                {
                  "type": "listItem",
                  "content": [
                    { "type": "text", "text": "First" }
                  ]
                },
                {
                  "type": "listItem",
                  "content": [
                    { "type": "text", "text": "Second" }
                  ]
                }
              ]
            }
          ]
        }
        """;

        var doc = ADFDocumentJsonDeserializer.FromJson(json);
        Assert.That(doc.Content[0], Is.TypeOf<BulletListNode>());

        var list = (BulletListNode)doc.Content[0];
        Assert.That(list.Items, Has.Count.EqualTo(2));
        Assert.That(list.Items[0].Content[0], Is.TypeOf<TextNode>());
        Assert.That(((TextNode)list.Items[1].Content[0]).Text, Is.EqualTo("Second"));
    }

    [Test]
    public void FromJson_InvalidNodeType_ReturnsEmptyDocument()
    {
        var json = """
        {
          "type": "document",
          "content": [
            { "type": "notARealType" }
          ]
        }
        """;

        var result = ADFDocumentJsonDeserializer.FromJson(json);
        Assert.That(result.Content, Is.Empty);
    }

    [Test]
    public void FromJson_InvalidRoot_ThrowsJsonException()
    {
        var json = """
        {
          "type": "paragraph",
          "content": [
            { "type": "text", "text": "Not a document" }
          ]
        }
        """;

        Assert.Throws<JsonException>(() =>
        {
            var _ = ADFDocumentJsonDeserializer.FromJson(json);
        });
    }
}
