using StreamingParser.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StreamingParser.Xml
{
	public class XmlExpressionAnalyzer
	{
		public static List<string> GetElementNames<TNode, TChild>(Expression<Func<TNode, TChild>> navExpression)
		{
			var result = new List<string>();
			var nextMemberExpression = CheckMoveNextExpression(navExpression.Body);

			while (nextMemberExpression != null && nextMemberExpression.NodeType == ExpressionType.MemberAccess)
			{
				result.Insert(0, GetXmlPathName(nextMemberExpression));
				nextMemberExpression = CheckMoveNextExpression(nextMemberExpression.Expression);
			}

			return result;
		}

		public static MemberExpression CheckMoveNextExpression(Expression nextExpression)
		{
			if (nextExpression == null)
			{
				return null;
			}
			else
			{
				var nextAsMemberExpression = nextExpression as MemberExpression;
				var nextAsParameterExpression = nextExpression as ParameterExpression;

				// If a navigation expression has been supplied that is not a member reference, throw
				// The one exception to this rule is a parameter, which must occur at the root.
				if (nextAsMemberExpression == null && nextAsParameterExpression == null)
				{
					throw new InvalidNavigationException(nextExpression);
				}
				else
				{
					return nextAsMemberExpression;
				}
			}
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
				throw new InvalidNavigationException(navExpression);
			}
			else
			{
				CheckXmlAttributes(navPropertyInfo);

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

		public static void CheckXmlAttributes(PropertyInfo propInfo)
		{
			// Fail if someone is trying to navigate to a property with attribute XmlAnyAttribute
			// This will not exist as a concrete source in the underlying XML.
			// It would be necessary to generate one level up.
			ThrowInvalidIfAttribute<XmlAnyAttributeAttribute>(propInfo);

			// Likewise for XmlAnyElement
			ThrowInvalidIfAttribute<XmlAnyElementAttribute>(propInfo);
			
		}

		protected static void ThrowInvalidIfAttribute<TAttr>(PropertyInfo propInfo)
		{
			if (Attribute.IsDefined(propInfo, typeof(TAttr)))
			{
				throw new InvalidNavigationException(typeof(TAttr));
			}
		}
	}
}
