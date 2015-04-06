using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StreamParserTests.SampleClasses.PureClasses
{
	public class Root
	{
		public List<Tier1Item> Tier1Items { get; set; }

		public Tier1Element T1E { get; set; }
	}
}
