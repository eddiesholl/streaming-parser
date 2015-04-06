using StreamingParser.Xml;
using StreamParserTests.SampleClasses.XmlAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using Xunit;
using StreamingParser.Util;

namespace StreamParserTests.Tests
{
	public class XmlAnyAttributeTests
	{
		[Fact]
		public void CheckXmlAnyAttributeFieldNavigationFails()
		{
			Assert.Throws<InvalidNavigationException>(
				() =>
				{
					var result =
						XmlExpressionAnalyzer.GetElementNames<RootXmlAttr, XmlAttribute[]>(r => r.XmlAnyAttributeField.XAttributesField);
				});
		}

		[Fact]
		public void CheckXmlAnyAttributePropertyNavigationFails()
		{
			Assert.Throws<InvalidNavigationException>(
				() =>
				{
					var result =
						XmlExpressionAnalyzer.GetElementNames<RootXmlAttr, XmlAttribute[]>(r => r.XmlAnyAttributeProperty.XAttributesProperty);
				});
		}
	}

	public class XmlAnyElementTests
	{
		[Fact]
		public void CheckXmlAnyElementFieldNavigationFails()
		{
			Assert.Throws<InvalidNavigationException>(
				() =>
				{
					var result =
						XmlExpressionAnalyzer.GetElementNames<RootXmlAttr, XmlElement[]>(r => r.XmlAnyElementField.XElementsField);
				});
		}

		[Fact]
		public void CheckXmlAnyElementPropertyNavigationFails()
		{
			Assert.Throws<InvalidNavigationException>(
				() =>
				{
					var result =
						XmlExpressionAnalyzer.GetElementNames<RootXmlAttr, XmlElement[]>(r => r.XmlAnyElementProperty.XElementsProperty);
				});
		}
	}
}
