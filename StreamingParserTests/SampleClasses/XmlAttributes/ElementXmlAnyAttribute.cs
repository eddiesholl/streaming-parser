using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace StreamParserTests.SampleClasses.XmlAttributes
{
	public class ElementXmlAnyAttributeField
	{
		[XmlAnyAttribute]
		public XmlAttribute[] XAttributesField;
	}

	public class ElementXmlAnyAttributeProperty
	{
		[XmlAnyAttribute]
		public XmlAttribute[] XAttributesProperty { get; set; }
	}
}
