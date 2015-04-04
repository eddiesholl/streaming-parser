using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingParser.Util
{
	public class NavigationPath
	{
		List<PathElement> _pathItems;

		public NavigationPath()
		{
			_pathItems = new List<PathElement>();
		}

		public NavigationPath(List<PathElement> existingPath)
		{
			_pathItems = new List<PathElement>(existingPath);
		}

		public void AddPathElement(string pathElement)
		{
			PathElement nextElement = new PathElement(pathElement, _pathItems.Count);
			_pathItems.Add(nextElement);
		}

		public NavigationPath PlusPathElement(string pathElement)
		{
			AddPathElement(pathElement);
			return this;
		}

		public NavigationPath PlusPathElements(params string [] pathElements)
		{
			foreach (string pathElement in pathElements)
			{
				AddPathElement(pathElement);
			}
			return this;
		}

		public IReadOnlyCollection<PathElement> PathElements { get { return _pathItems.AsReadOnly(); } }

		public NavigationPath Clone()
		{
			return new NavigationPath(_pathItems);
		}
	}
}
