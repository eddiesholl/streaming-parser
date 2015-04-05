using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
				result.Insert(0, GetXmlPathName(nextExpression));
				nextExpression = nextExpression.Expression as MemberExpression;
			}

			return result;
		}

		public static string GetXmlPathName(Type t)
		{
			// REVISIT: Add support for XML directives etc
			return t.Name;
		}

		public string GetXmlPathName<TParent, TChild>(Expression<Func<TParent, TChild>> navExpression)
		{
			string result = string.Empty;

			Type parentType = navExpression.Parameters[0].Type;
			Type desiredReturnElement = navExpression.ReturnType;

			bool isEnum = desiredReturnElement
				.GetInterfaces()
				.Any(ti => ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>));

			return result;
		}

		public static string GetXmlPathName(MemberExpression navExpression)
		{
			string result = string.Empty;

			var navPropertyInfo = navExpression.Member as PropertyInfo;

			if (navPropertyInfo == null)
			{
				// Need to fail here, not a property navigation
			}
			else
			{
				// parentType will be important once we are interrogating xml directives
				Type parentType = navPropertyInfo.DeclaringType;

				Type desiredReturnElement = navPropertyInfo.PropertyType;

				// Check if the property being travsersed is a collection of items
				// If so, modify our desired element name
				bool isEnum = desiredReturnElement
					.GetInterfaces()
					.Any(ti => ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>));

				if (isEnum)
				{
					result = navPropertyInfo.Name;
				}
				else
				{
					result = GetXmlPathName(desiredReturnElement);
				}
			}
			
			return result;
		}
	}
}
