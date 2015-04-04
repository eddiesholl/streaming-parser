using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StreamingParser.Xml
{
	public class XmlExpressionAnalyzer
	{
		public static List<string> GetElementNames<TNode, TChild>(Expression<Func<TNode, TChild>> navExpression)
		{
			var result = new List<string>();
			var nextExpression = navExpression.Body as MemberExpression;
			
			while (nextExpression != null && nextExpression.NodeType == ExpressionType.MemberAccess)
			{
				result.Insert(0, GetXmlPathName(nextExpression.Type));
				nextExpression = nextExpression.Expression as MemberExpression;
			}

			return result;
		}

		public static string GetXmlPathName(Type t)
		{
			// REVISIT: Add support for XML directives etc
			return t.Name;
		}

		public string GetXmlPathName<TParent, TChild>(Expression<Func<TParent, TChild>> navigationExpression)
		{
			string result = string.Empty;

			Type parentType = navigationExpression.Parameters[0].Type;
			Type desiredReturnElement = navigationExpression.ReturnType;

			bool isEnum = desiredReturnElement
				.GetInterfaces()
				.Any(ti => ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>));

			//navigationExpression.
			//navigationExpression.
			return result;
		}
	}
}
