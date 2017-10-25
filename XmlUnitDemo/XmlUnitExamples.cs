using System;
using System.Xml;
using Org.XmlUnit.Builder;
using Xunit;

namespace XmlUnitDemo
{
    public class XmlUnitExamples
    {
        [Fact]
        public void RecognizesIdenticalXML()
        {
            AssertXml.AreIdentical(DiffBuilder
                .Compare(Input.FromString("<root>value</root>"))
                .WithTest(Input.FromString("<root>value</root>")));
        }

        [Fact]
        public void RecognizesDifferentXML_values()
        {
            AssertXml.AreDifferent(DiffBuilder
                .Compare(Input.FromString("<root>value</root>"))
                .WithTest(Input.FromString("<root>different_value</root>")));
        }

        [Fact]
        public void RecognizesDifferentXML_attributes()
        {
            AssertXml.AreDifferent(DiffBuilder
                .Compare(Input.FromString("<root attribute1='val' />"))
                .WithTest(Input.FromString("<root attribute2='val' />")));
        }

        [Fact]
        public void IgnoresWhitespace()
        {
            AssertXml.AreIdentical(DiffBuilder
                .Compare(Input.FromString("<root>\n   <child>child</child>  \n</root>"))
                .WithTest(Input.FromString("<root><child>child</child></root>")));
        }

        [Fact]
        public void IgnoresIrrelevantNodes()
        {
            AssertXml.AreIdentical(DiffBuilder
                .Compare(Input.FromString(
                    "<root><irrelevant>irrelevant value</irrelevant><relevant>relevant value</relevant></root>"))
                .WithTest(Input.FromString(
                    "<root><irrelevant>other irrelevant value</irrelevant><relevant>relevant value</relevant></root>"))
                .WithNodeFilter(EverythingExcept(n => n.Name == "irrelevant"))
                );
        }

        private Predicate<XmlNode> EverythingExcept(Func<XmlNode, bool> predicate)
        {
            return node => !predicate.Invoke(node);
        }

        [Fact]
        public void IgnoresIrrelevantAttributes()
        {
            AssertXml.AreIdentical(DiffBuilder
                .Compare(Input.FromString(
                    "<root relevant='relevant'/>"))
                .WithTest(Input.FromString(
                    "<root relevant='relevant' irrelevant='different' />"))
                .WithAttributeFilter(EverythingExcept(a => a.Name == "irrelevant")));
        }

        [Fact]
        public void CanIgnoreElementDeepInXml()
        {
            AssertXml.AreIdentical(DiffBuilder
                .Compare(Input.FromString(
                    "<root><irrelevant>irrelevant</irrelevant><relevant>relevant</relevant></root>"))
                .WithTest(Input.FromString(
                    "<root><irrelevant>irrelevant2</irrelevant><relevant>relevant</relevant></root>"))
                .WithNodeFilter(EverythingExcept(n => n.ParentNode?.Name == "root" && n.Name == "irrelevant"))
            );
        }
    }
}