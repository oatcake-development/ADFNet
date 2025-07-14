using ADFNet.Core.Models;
using ADFNet.OrgMode.Exporters;

namespace ADFNet.OrgMode.Tests;

[TestFixture]
public class OrgModeExporterTests
{
    private OrgModeExporter _exporter;

    [SetUp]
    public void Setup()
    {
        _exporter = new OrgModeExporter();
    }

    [Test]
    public void Export_TextNode_ReturnsPlainText()
    {
        var node = new TextNode { Text = "Hello Org!" };
        var output = _exporter.Export(node);
        Assert.That(output, Is.EqualTo("Hello Org!"));
    }

    [Test]
    public void Export_HardBreak_ReturnsNewline()
    {
        var node = new HardBreakNode();
        var output = _exporter.Export(node);
        Assert.That(output, Is.EqualTo("\n"));
    }

    [Test]
    public void Export_Paragraph_ReturnsTextWithNewline()
    {
        var paragraph = new ParagraphNode
        {
            Content =
            [
                new TextNode { Text = "This is a paragraph." },
                new HardBreakNode(),
                new TextNode { Text = "Continued." }
            ]
        };

        var output = _exporter.Export(paragraph);
        var expected = "This is a paragraph.\nContinued.\n";
        Assert.That(output, Is.EqualTo(expected));
    }

    [Test]
    public void Export_BulletList_ReturnsOrgModeList()
    {
        var list = new BulletListNode
        {
            Items =
            [
                new ListItemNode { Content = new List<ADFNode>() { new TextNode { Text = "First item" }}},
                new ListItemNode { Content = new List<ADFNode>() { new TextNode { Text = "Second item" }}},
            ]
        };
        
        var output = _exporter.Export(list);
        var expected = "- First item\n- Second item\n";
        Assert.That(output, Is.EqualTo(expected));
    }
    
    [Test]
    public void RendersPlainText()
    {
        var node = new TextNode { Text = "plain" };
        Assert.That(_exporter.Export(node), Is.EqualTo("plain"));
    }

    [Test]
    public void RendersBoldText()
    {
        var node = new TextNode { Text = "bold", Bold = true };
        Assert.That(_exporter.Export(node), Is.EqualTo("*bold*"));
    }

    [Test]
    public void RendersItalicText()
    {
        var node = new TextNode { Text = "italic", Italic = true };
        Assert.That(_exporter.Export(node), Is.EqualTo("/italic/"));
    }

    [Test]
    public void RendersUnderlineText()
    {
        var node = new TextNode { Text = "underline", Underline = true };
        Assert.That(_exporter.Export(node), Is.EqualTo("_underline_"));
    }

    [Test]
    public void RendersStrikeText()
    {
        var node = new TextNode { Text = "strike", Strike = true };
        Assert.That(_exporter.Export(node), Is.EqualTo("+strike+"));
    }

    [Test]
    public void RendersCodeText()
    {
        var node = new TextNode { Text = "code", Code = true };
        Assert.That(_exporter.Export(node), Is.EqualTo("~code~"));
    }

    [Test]
    public void RendersMultipleStyles()
    {
        var node = new TextNode { Text = "styled", Bold = true, Italic = true };
        var result = _exporter.Export(node);
        Assert.That(result, Does.Contain("styled"));
        Assert.That(result, Does.Contain("*") & Does.Contain("/"));
    }
}
