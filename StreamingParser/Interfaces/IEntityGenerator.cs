﻿using StreamingParser.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StreamingParser
{
	public interface IEntityGenerator<TNode>
	{
		TChild Generate<TChild>(Expression<Func<TNode, TChild>> navigationExpression);

		IEnumerable<TChild> GenerateElements<TChild>(Expression<Func<TNode, IEnumerable<TChild>>> navExpression);
	}
}
