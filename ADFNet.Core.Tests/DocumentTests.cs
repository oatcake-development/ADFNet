// using ADFNet.Core.Models;
// using System.Collections.Generic;
//
// namespace ADFNet.Core.Tests;
//
// [TestFixture]
// public class DocumentTests
// {
//     private ADFDocument _validDoc;
//
//     [SetUp]
//     public void Setup()
//     {
//         _validDoc = new ADFDocument
//         {
//             Content = new List<ADFNode>
//             {
//                 new ADFNode { Type = "paragraph", Text = "Hello world" }
//             }
//         };
//     }
//
//     [Test]
//     public void ADFDocument_WithValidContent_IsValid()
//     {
//         Assert.IsTrue(_validDoc.IsValid());
//     }
//
//     [Test]
//     public void ADFDocument_WithoutContent_IsInvalid()
//     {
//         var emptyDoc = new ADFDocument();
//         Assert.IsFalse(emptyDoc.IsValid());
//     }
//
//     [Test]
//     public void ADFNode_WithTextContent_AssignsCorrectValues()
//     {
//         var node = new ADFNode
//         {
//             Type = "text",
//             Text = "Hello World"
//         };
//
//         Assert.That(node.Type, Is.EqualTo("text"));
//         Assert.That(node.Text, Is.EqualTo("Hello World"));
//     }
// }