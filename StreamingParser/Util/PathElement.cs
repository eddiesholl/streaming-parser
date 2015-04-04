using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingParser.Util
{
	public class PathElement
	{
		public readonly string Name;
		public readonly int Depth;

		public PathElement(string name, int depth)
		{
			Name = name;
			Depth = depth;
		}
	}
}
