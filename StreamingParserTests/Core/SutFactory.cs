using StreamingParser.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StreamParserTests.Core
{
	class SutFactory
	{
		internal static XmlStreamingParser<T> CreateXmlStreamingParser<T>(string sampleFileName)
		{
			var testInput = LoadEmbeddedSampleFile(sampleFileName);

			return new XmlStreamingParser<T>(testInput);
		}

		protected static Stream LoadEmbeddedSampleFile(string fileName)
		{
			var assembly = Assembly.GetExecutingAssembly();
			string resourceName = string.Format("{0}.SampleFiles.{1}", assembly.GetName().Name, fileName);

			Stream result = assembly.GetManifestResourceStream(resourceName);
			Assert.NotNull(result);

			return result;
		}
	}
}
