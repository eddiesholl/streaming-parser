using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StreamingParser.Util
{
	public class InvalidNavigationException : Exception
	{
		public InvalidNavigationException(Expression invalidExpression)
			: base(string.Format(
						"The navigation expression {0} is of type {1}. Please only navigate via Properties",
						invalidExpression.ToString(),
						invalidExpression.NodeType)) {}

		public InvalidNavigationException(Type invalidAttributeType)
			: base(string.Format("An attempt to navigate to a property with attribute {0} is not possible.", invalidAttributeType.Name))
		{ }
	}
}
