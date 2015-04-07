using StreamingParser.Xml;
using StreamingParserTests.Core;
using StreamParserTests.SampleClasses.XmlAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StreamParserTests.Tests.XmlAttributes
{
	class XmlArrayTests
	{
		[Fact]
		public void CheckArrayNavElement()
		{
			var result = XmlExpressionAnalyzer.GetElementNames<ElementXmlArray, ArrayList>(r => r.ArrayOfItems).ToList();

			ExpressionAssert.CheckElementNames(result, ElementXmlArray.c_AltArrayOfItems);
		}
	}
}
