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