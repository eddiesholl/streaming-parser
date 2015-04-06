using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace StreamParserTests.SampleClasses.XmlAttributes
{
	public class ElementXmlAnyElementField
	{
		[XmlAnyElement]
		public XmlElement[] XElementsField;
	}

	public class ElementXmlAnyElementProperty
	{
		[XmlAnyElement]
		public XmlElement[] XElementsProperty { get; set; }
	}
}
