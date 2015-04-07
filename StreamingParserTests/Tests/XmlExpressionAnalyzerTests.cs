using StreamingParser.Util;
using StreamingParser.Xml;
using StreamingParserTests.Core;
using StreamParserTests.SampleClasses;
using StreamParserTests.SampleClasses.PureClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StreamParserTests
{
	public class XmlExpressionAnalyzerTests
	{
		[Fact]
		public void CheckPathFromSimpleExpression()
		{
			var result = XmlExpressionAnalyzer.GetElementNames<Root, Tier1Element>(r => r.T1E);

			ExpressionAssert.CheckElementNames(result, typeof(Tier1Element).Name);
		}

		[Fact]
		public void CheckPathFromDoubleExpression()
		{
			var result = XmlExpressionAnalyzer.GetElementNames<Root, Tier2Element>(r => r.T1E.T2E).ToList();

			ExpressionAssert.CheckElementNames(result, typeof(Tier1Element).Name, typeof(Tier2Element).Name);
		}

		[Fact]
		public void CheckPathForBasicList()
		{
			var result = XmlExpressionAnalyzer.GetElementNames<Tier1Element, List<Tier2Item>>(r => r.Tier2Items).ToList();

			ExpressionAssert.CheckElementNames(result, "Tier2Items");
		}

		[Fact]
		public void CheckHostileRoot()
		{
			var result = XmlExpressionAnalyzer.GetElementNames<RootHostile, ChildHostile>(r => r.ChildHostileProperty);

			ExpressionAssert.CheckElementNames(result, typeof(ChildHostile).Name);
		}

		[Fact]
		public void CheckHostileStruct()
		{
			var result = XmlExpressionAnalyzer.GetElementNames<RootHostile, ChildStruct>(r => r.ChildStructProperty);

			ExpressionAssert.CheckElementNames(result, typeof(ChildStruct).Name);
		}

		[Fact]
		public void CheckHostileMethodNavigation()
		{
			Assert.Throws<InvalidNavigationException>(() =>
				{
					var result = XmlExpressionAnalyzer.GetElementNames<RootHostile, ChildHostile>(r => r.ChildHostileMethod());
				}
			);
		}

		[Fact]
		public void CheckHostileRepeatedElement()
		{
			var result = XmlExpressionAnalyzer.GetElementNames<RootHostile, ChildHostile>(r => r.ChildHostileProperty.ChildHostileNested);

			ExpressionAssert.CheckElementNames(result, typeof(ChildHostile).Name, typeof(ChildHostile).Name);
		}
	}
}
