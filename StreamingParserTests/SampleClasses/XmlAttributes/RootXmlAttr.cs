using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StreamParserTests.SampleClasses.XmlAttributes
{
	[XmlRoot(c_AltRootXmlAttr)]
	public class RootXmlAttr
	{
		public const string c_AltRootXmlAttr = "AltRootXmlAttr";

		public ElementXmlAnyAttributeField XmlAnyAttributeField { get; set; }
		public ElementXmlAnyAttributeProperty XmlAnyAttributeProperty { get; set; }

		public ElementXmlAnyElementField XmlAnyElementField { get; set; }
		public ElementXmlAnyElementProperty XmlAnyElementProperty { get; set; }

		public ElementXmlArray XmlArray { get; set; }
	}
}
