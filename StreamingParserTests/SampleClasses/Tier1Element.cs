using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamParserTests.SampleClasses
{
	public class Tier1Element
	{
		public List<Tier2Item> Tier2Items { get; set; }

		public Tier2Element T2E { get; set; }
	}
}
