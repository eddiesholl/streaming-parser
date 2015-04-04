using StreamingParser.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace StreamingParser.Xml
{
	public class XmlDataProvider
	{
		Stream _xmlContentStream;

		public XmlDataProvider(Stream xmlContentStream)
		{
			_xmlContentStream = xmlContentStream;
			_xmlContentStream.Seek(0, SeekOrigin.Begin);
		}

		public XmlTextReader GetReader()
		{
			var result = new XmlTextReader(_xmlContentStream);
			//bool readresult = result.Read();

			return result;
		}
	}
}
