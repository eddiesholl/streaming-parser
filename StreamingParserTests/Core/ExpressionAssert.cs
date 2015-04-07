using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StreamingParserTests.Core
{
	internal class ExpressionAssert
	{
		/// <summary>
		/// Basically just compare two lists of strings, to ensure the correct set of navigation paths have been generated
		/// It is ugly having actaul and expected flipped, but I like supplying expected values with params syntax
		/// </summary>
		/// <param name="actualStrings"></param>
		/// <param name="expectedNames"></param>
		internal static void CheckElementNames(List<string> actualStrings, params string[] expectedNames)
		{
			Assert.NotNull(actualStrings);
			Assert.Equal(expectedNames, actualStrings.ToArray());
		}
	}
}
