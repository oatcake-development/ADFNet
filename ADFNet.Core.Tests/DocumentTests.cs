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