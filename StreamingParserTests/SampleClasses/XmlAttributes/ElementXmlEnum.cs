using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StreamParserTests.SampleClasses.XmlAttributes
{
	class ElementXmlEnum
	{
		public enum EmployeeStatus
		{
			[XmlEnum(Name = "Single")]
			One,
			[XmlEnum(Name = "Double")]
			Two,
			[XmlEnum(Name = "Triple")]
			Three
		}

		public EmployeeStatus EmployeeStatusValue { get; set; }
		
	}
}
