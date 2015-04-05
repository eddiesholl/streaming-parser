using StreamingParser.Xml;
using StreamParserTests.SampleClasses;
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

			Assert.NotNull(result);
			Assert.Equal(1, result.ToList().Count);
			Assert.Equal(typeof(Tier1Element).Name, result.First());
		}

		[Fact]
		public void CheckPathFromDoubleExpression()
		{
			var result = XmlExpressionAnalyzer.GetElementNames<Root, Tier2Element>(r => r.T1E.T2E).ToList();

			Assert.NotNull(result);
			Assert.Equal(2, result.Count);
			Assert.Equal(typeof(Tier1Element).Name, result.First());
			Assert.Equal(typeof(Tier2Element).Name, result.Last());
		}

		[Fact]
		public void CheckPathForBasicList()
		{
			var result = XmlExpressionAnalyzer.GetElementNames<Tier1Element, List<Tier2Item>>(r => r.Tier2Items).ToList();

			Assert.NotNull(result);
			Assert.Equal(1, result.Count);
			Assert.Equal("Tier2Items", result.First());
		}
	}
}
