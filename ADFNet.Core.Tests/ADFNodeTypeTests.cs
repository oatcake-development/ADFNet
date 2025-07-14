using ADFNet.Core;
using ADFNet.Core.Models;

[TestFixture]    
public class ADFNodeTypeTests
    {
        private void AssertNodeType(ADFNode node, string expectedType)
        {
            Assert.That(node.Type.ToADFString(), Is.EqualTo(expectedType), 
                $"Expected Type to be '{expectedType}' but got '{node.Type}'.");
        }

        [Test]
        public void ParagraphNode_HasCorrectType()
        {
            var node = new ParagraphNode { Content = new List<ADFNode>() };
            AssertNodeType(node, "paragraph");
        }

        [Test]
        public void TextNode_HasCorrectType()
        {
            var node = new TextNode { Text = "Hello world"};
            AssertNodeType(node, "text");
        }

        [Test]
        public void BulletListNode_HasCorrectType()
        {
            var node = new BulletListNode();
            AssertNodeType(node, "bulletList");
        }

        [Test]
        public void ListItemNode_HasCorrectType()
        {
            var node = new ListItemNode { Content = new List<ADFNode>()};
            AssertNodeType(node, "listItem");
        }

        [Test]
        public void HardBreakNode_HasCorrectType()
        {
            var node = new HardBreakNode();
            AssertNodeType(node, "hardBreak");
        }
    }