using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamParserTests.SampleClasses
{
	public class Tier2Element
	{
		public List<Tier3Item> Tier3Items { get; set; }

		public Tier3Element T3E { get; set; }
	}
}
