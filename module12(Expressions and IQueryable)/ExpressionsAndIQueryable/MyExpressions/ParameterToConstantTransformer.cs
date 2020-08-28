using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyExpressions
{
    public class ParameterToConstantTransformer: ExpressionVisitor
    {
        private readonly Dictionary<string, int> _dictionary;

        public ParameterToConstantTransformer(Dictionary<string, int> dictionary)
        {
            _dictionary = dictionary;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node.NodeType == ExpressionType.Parameter)
            {
                return Expression.Constant(_dictionary[node.Name]);
            }

            return base.VisitParameter(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
           
            Expression body = this.Visit(node.Body);
            //var parameters = node.Parameters;
            if (body != node.Body && node.Parameters != null)
                return Expression.Lambda(body, false, node.Parameters);
            return base.VisitLambda(node);
        }

    }
}
