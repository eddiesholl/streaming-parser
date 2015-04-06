using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StreamParserTests.SampleClasses.XmlAttributes
{
	class ElementWithArrayItem
	{
		[XmlArrayItem(typeof(ElementInsideArrayItem))]
		[XmlArrayItem(typeof(ElementInsideArrayItemPeer))]
		public ArrayList ArrayItemItems { get; set; }
	}
}
