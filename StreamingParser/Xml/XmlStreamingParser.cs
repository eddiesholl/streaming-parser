using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingParser.Xml
{
	public class XmlStreamingParser<TRoot> : XmlGraphNavigator<TRoot>, IStreamingParser<TRoot>
	{
		public XmlStreamingParser(Stream s) : base(new XmlDataProvider(s))
		{
			PushPathElement(XmlExpressionAnalyzer.GetXmlPathName(typeof(TRoot)));
		}

	}
}
