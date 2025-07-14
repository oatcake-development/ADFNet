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
using NUnit.Framework;

namespace ADFNet.Json.Tests;

[TestFixture]
public class ADFDocumentJsonRoundTripTests
{
    [Test]
    public void RoundTrip_SimpleDocument_PreservesStructureAndValues()
    {
        var original = new ADFDocument
        {
            Content =
            [
                new ParagraphNode
                {
                    Content =
                    [
                        new TextNode { Text = "Hello", Bold = true },
                        new HardBreakNode(),
                        new TextNode { Text = "World", Italic = true }
                    ]
                },
                new BulletListNode
                {
                    Items =
                    [
                        new ListItemNode { Content = [ new TextNode { Text = "One" } ] },
                        new ListItemNode { Content = [ new TextNode { Text = "Two" } ] }
                    ]
                }
            ]
        };

        var json = ADFDocumentJsonSerializer.ToJson(original);
        var parsed = ADFDocumentJsonDeserializer.FromJson(json);

        Assert.That(parsed.Type, Is.EqualTo(NodeType.Document));
        Assert.That(parsed.Content.Count, Is.EqualTo(original.Content.Count));

        // Further assertions can be refined or abstracted to a structural comparison method
        var para = parsed.Content[0] as ParagraphNode;
        Assert.That(para, Is.Not.Null);
        Assert.That(para!.Content[0], Is.TypeOf<TextNode>());
        Assert.That(((TextNode)para.Content[0]).Bold, Is.True);

        var list = parsed.Content[1] as BulletListNode;
        Assert.That(list, Is.Not.Null);
        Assert.That(list!.Items.Count, Is.EqualTo(2));
        Assert.That(((TextNode)list.Items[1].Content[0]).Text, Is.EqualTo("Two"));
    }
}