using StreamingParser.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace StreamingParser.Xml
{
	public class XmlGraphNavigator<TNode> : IGraphNavigator<TNode>, IEntityGenerator<TNode>
	{
		NavigationPath _currentPath;
		XmlDataProvider _dataProvider;

		public XmlGraphNavigator(XmlDataProvider dataProvider)
		{
			_dataProvider = dataProvider;

			_currentPath = new NavigationPath();
		}

		public XmlGraphNavigator(
			XmlDataProvider dataProvider,
			NavigationPath parentPath,
			params string[] childElements)
			: this(dataProvider)
		{
			_currentPath = parentPath.Clone().PlusPathElements(childElements);
		}

		public IGraphNavigator<TChild> Navigate<TChild>(Expression<Func<TNode, TChild>> navExpression)
		{
			IEnumerable<string> nextElementNames = XmlExpressionAnalyzer.GetElementNames(navExpression);

			return new XmlGraphNavigator<TChild>(_dataProvider, _currentPath, nextElementNames.ToArray());
		}

		public TChild Generate<TChild>(Expression<Func<TNode, TChild>> navExpression)
		{
			var navList = XmlExpressionAnalyzer.GetElementNames(navExpression);
			string finalElementName = XmlExpressionAnalyzer.GetXmlPathName(typeof(TChild));
			XmlTextReader sourceXmlReader = _dataProvider.GetReader();

			XmlReader subTreeForGeneration = ExecuteNavigation(_currentPath, sourceXmlReader, finalElementName);

			return DeserializeElementFromXml<TChild>(subTreeForGeneration);
		}

		public IEnumerable<TChild> GenerateElements<TChild, TChildEnumerable>(Expression<Func<TNode, TChildEnumerable>> navigationExpression)
			where TChildEnumerable : IEnumerable<TChild>
		{
			string enumElementName = XmlExpressionAnalyzer.GetXmlPathName(typeof(TChildEnumerable));
			XmlTextReader sourceXmlReader = _dataProvider.GetReader();

			XmlReader subTreeForGeneration = ExecuteNavigation(_currentPath, sourceXmlReader, enumElementName);

			string elementNameForGeneration = XmlExpressionAnalyzer.GetXmlPathName(typeof(TChild));
			string currentElementName = subTreeForGeneration.LocalName;

			if (!subTreeForGeneration.ReadToDescendant(elementNameForGeneration))
			{
				throw new ElementNotFoundException(currentElementName, elementNameForGeneration);
			}

			bool foundSibling = true;
			do
			{
				yield return DeserializeElementFromXml<TChild>(subTreeForGeneration);
				foundSibling = subTreeForGeneration.ReadToNextSibling(elementNameForGeneration);
			}
			while (foundSibling);
		}

		public static TElement DeserializeElementFromXml<TElement>(XmlReader subTreeReader)
		{
			XmlSerializer xs = new XmlSerializer(typeof(TElement));
			TElement result = (TElement)xs.Deserialize(subTreeReader);

			return result;
		}

		public static XmlReader ExecuteNavigation(NavigationPath navPath, XmlReader sourceXmlReader, params string[] finalElementNames)
		{
			NavigationPath generationPath = navPath.Clone().PlusPathElements(finalElementNames);

			if (sourceXmlReader.ReadState != ReadState.Initial)
			{
				throw new InvalidOperationException(string.Format("XmlTextReader should be in Initial state, not {0}", sourceXmlReader.ReadState));
			}

			bool firstElement = true;
			foreach (PathElement pathElement in generationPath.PathElements)
			{
				bool descendResult = false;

				if (firstElement)
				{
					sourceXmlReader.Read();

					descendResult = sourceXmlReader.IsStartElement(pathElement.Name);

					firstElement = false;
				}
				else
				{
					descendResult = sourceXmlReader.ReadToDescendant(pathElement.Name);
				}

				if (!descendResult)
				{
					throw new ElementNotFoundException(pathElement);
				}
			}

			XmlReader generateSubTree = sourceXmlReader.ReadSubtree();
			generateSubTree.Read();

			return generateSubTree;
		}

		protected void PushPathElement(string elementName)
		{
			_currentPath.AddPathElement(elementName);
		}
	}
}
