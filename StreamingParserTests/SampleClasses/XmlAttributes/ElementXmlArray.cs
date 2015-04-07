using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StreamParserTests.SampleClasses.XmlAttributes
{
	public class ElementXmlArray
	{
		public const string c_AltArrayOfItems = "AltArrayOfItems";

		[XmlArray(c_AltArrayOfItems)]
		public ArrayList ArrayOfItems { get; set; }
	}
}
