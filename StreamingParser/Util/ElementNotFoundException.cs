using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingParser.Util
{
	public class ElementNotFoundException : Exception
	{
		public ElementNotFoundException(PathElement missingElement)
			: base("Could not find element '" + missingElement.Name + "' at depth " + missingElement.Depth) { }

		public ElementNotFoundException(string currentElementName, string desiredElementName)
			: base("Could not find element '" + desiredElementName + "' under current element " + currentElementName) { }
	}
}
