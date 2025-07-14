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
using System.Collections.Generic;

namespace ADFNet.Core.Tests;

[TestFixture]
public class DocumentTests
{
    [Test]
    public void Document_HasCorrectType()
    {
        var doc = new ADFDocument();
        Assert.That(doc.Type, Is.EqualTo(NodeType.Document));
    }

    [Test]
    public void Document_CanContainParagraphs()
    {
        var para = new ParagraphNode();
        var doc = new ADFDocument(new[] { para });

        Assert.That(doc.Content, Has.Count.EqualTo(1));
        Assert.That(doc.Content[0], Is.InstanceOf<ParagraphNode>());
    }
}