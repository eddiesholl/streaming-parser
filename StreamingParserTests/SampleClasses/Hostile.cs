using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamParserTests.SampleClasses
{
	public class RootHostile
	{
		// Should be fine
		public ChildHostile ChildHostileProperty { get; set; }

		// Should be fine
		public ChildStruct ChildStructProperty { get; set; }

		// Should not be possible to navigate here
		public ChildHostile ChildHostileMethod()
		{
			return new ChildHostile();
		}
	}

	public class ChildHostile
	{
		// Should behave correctly
		public ChildHostile ChildHostileNested { get; set; }
	}

	public struct ChildStruct
	{

	}
}
