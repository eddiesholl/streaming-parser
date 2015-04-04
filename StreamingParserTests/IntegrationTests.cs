using StreamParserTests.Core;
using StreamParserTests.SampleClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StreamParserTests
{
    public class IntegrationTests
    {
		[Theory]
		[InlineData("basic.xml")]
		public void BasicIntegrationTest(string sampleFileName)
		{
			var sut = SutFactory.CreateXmlStreamingParser<Root>(sampleFileName);
		}

		[Fact]
		public void BasicNavigationTest()
		{
			var sut = SutFactory.CreateXmlStreamingParser<Root>("basic.xml");

			var nav = sut.Navigate(r => r.T1E);
			var deserializedResult = nav.Generate(n => n.T2E);
			//sut.Generate(r => r.T1E.T2E);

			Assert.NotNull(deserializedResult);
			Assert.IsType<Tier2Element>(deserializedResult);
			//sut.Navigate
		}

		[Fact]
		public void DoubleNavigationTest()
		{
			var sut = SutFactory.CreateXmlStreamingParser<Root>("basic.xml");

			var nav = sut.Navigate(r => r.T1E.T2E);
			var deserializedResult = nav.Generate(n => n.T3E);

			Assert.NotNull(deserializedResult);
			Assert.IsType<Tier3Element>(deserializedResult);
		}

		[Fact]
		public void GenerateListTest()
		{
			var sut = SutFactory.CreateXmlStreamingParser<Root>("basic.xml");

			var nav = sut.Navigate(r => r.T1E);
			var deserializedResult = nav.GenerateElements<Tier2Item, List<Tier2Item>>(n => n.Tier2Items);

			Assert.NotNull(deserializedResult);

			var listResult = deserializedResult.ToList();
			Assert.Equal(2, listResult.Count);
		}
    }
}
